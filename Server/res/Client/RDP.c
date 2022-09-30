#include "rdp.h"




/* START server recv tags */
#define _RDP_MOUSE_MOVE "rdp_mm;"
#define _RDP_PRESS_LEFT_MOUSE "rdp_pl;" // press left mouse
#define _RDP_RELEASE_LEFT_MOUSE "rdp_rl;"// release left mouse
#define _RDP_PRESS_RIGHT_MOUSE "rdp_pr;" // press right mouse
#define _RDP_RELEASE_RIGHT_MOUSE "rdp_rr;"// release right mouse
#define _RDP_END "rdpend;"
/* END server recv tags */


IStream* ScreenShot() {

	DWORD dwWidth = GetSystemMetrics(SM_CXSCREEN);
	DWORD dwHeight = GetSystemMetrics(SM_CYSCREEN);
	HDC hdcScreen = GetDC(NULL);
	HDC hdcMemDC = CreateCompatibleDC(hdcScreen);
	HBITMAP hbmScreen = NULL;
	hbmScreen = CreateCompatibleBitmap(hdcScreen, dwWidth, dwHeight);
	SelectObject(hdcMemDC, hbmScreen);
	BitBlt(hdcMemDC, 0, 0, dwWidth, dwHeight, hdcScreen, ERROR, ERROR, SRCCOPY);
	CURSORINFO cursor = { sizeof(cursor) };
	GetCursorInfo(&cursor);
	if (cursor.flags == CURSOR_SHOWING) {
		RECT rect;
		GetWindowRect(GetDesktopWindow(), &rect);
		ICONINFO info = { sizeof(info) };
		GetIconInfo(cursor.hCursor, &info);
		const int x = cursor.ptScreenPos.x - rect.left - rect.left - info.xHotspot;
		const int y = cursor.ptScreenPos.y - rect.top - rect.top - info.yHotspot;
		BITMAP bmpCursor = { 0 };
		GetObject(info.hbmColor, sizeof(bmpCursor), &bmpCursor);
		DrawIconEx(hdcMemDC, x, y, cursor.hCursor, bmpCursor.bmWidth, bmpCursor.bmHeight,
			0, NULL, DI_NORMAL);
	}

	// START 

	// I toke this part of code from https://stackoverflow.com/a/48708505/18500036 // I just change the encode type from GUID_ContainerFormatBmp to GUID_ContainerFormatPng and IWICStream_InitializeFromFilename to IWICStream_InitializeFromIStream .

	//Convert To %PNG
	CoInitializeEx(NULL, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE); // init COM

	BITMAP bitmap = { 0 };
	GetObject(hbmScreen, sizeof(BITMAP), &bitmap);



	IWICImagingFactory* factory = NULL;
	CoCreateInstance(&CLSID_WICImagingFactory, NULL, CLSCTX_INPROC_SERVER, &IID_IWICImagingFactory, &factory);


	IWICBitmap* wic_bitmap = NULL;
	IWICImagingFactory_CreateBitmapFromHBITMAP(factory, hbmScreen, NULL, WICBitmapIgnoreAlpha, &wic_bitmap);


	IWICStream* stream = NULL;
	IWICImagingFactory_CreateStream(factory, &stream);

	IStream* pIstream = NULL;
	CreateStreamOnHGlobal(NULL, TRUE, &pIstream);
	IWICStream_InitializeFromIStream(stream, pIstream);


	IWICBitmapEncoder* encoder = NULL;
	IWICImagingFactory_CreateEncoder(factory, &GUID_ContainerFormatPng, NULL, &encoder); // Coding

	IWICBitmapEncoder_Initialize(encoder, (IStream*)stream, WICBitmapEncoderNoCache);


	IWICBitmapFrameEncode* frame = NULL;

	IWICBitmapEncoder_CreateNewFrame(encoder, &frame, NULL);

	IWICBitmapFrameEncode_Initialize(frame, NULL);

	IWICBitmapFrameEncode_SetSize(frame, bitmap.bmWidth, bitmap.bmHeight);

	GUID pixel_format = GUID_WICPixelFormat24bppBGR;
	IWICBitmapFrameEncode_SetPixelFormat(frame, &pixel_format);


	IWICBitmapFrameEncode_WriteSource(frame, (IWICBitmapSource*)wic_bitmap, NULL); // init



	IWICBitmapFrameEncode_Commit(frame);
	IWICBitmapEncoder_Commit(encoder); // final

	// clean up
	if (frame)
		IWICBitmapFrameEncode_Release(frame);
	if (encoder)
		IWICBitmapEncoder_Release(encoder);
	if (stream)
		IWICStream_Release(stream);
	if (wic_bitmap)
		IWICBitmap_Release(wic_bitmap);
	if (factory)
		IWICImagingFactory_Release(factory);
	//END
	CoUninitialize();
	DeleteObject(hbmScreen);
	DeleteObject(hdcMemDC);
	ReleaseDC(NULL, hdcScreen);

	return pIstream;
}

void FRAMES_THREAD(SOCKETDATA*sd) {
	kill_rdp = FALSE;
	SOCKET s = sd->s;
	BYTE* ImageBytes, *Buffer,*p;
	IStream* ImageStream;
	LARGE_INTEGER offset;
	ULARGE_INTEGER _size_;
	int size_ = 0;
	int size = 0;
	int _offset = 0;
	int fulllen = 0;

	CreateThread(NULL, 0, RDP_THREAD, (void*)sd, 0, NULL);

	loop{
		if (kill_rdp) {
			break;
		}
		ImageStream = ScreenShot();


		offset.QuadPart = STREAM_SEEK_SET;
		IStream_Seek(ImageStream, offset, STREAM_SEEK_END, &_size_);
		size_ = _size_.QuadPart;
		fulllen = size_ + strlen("rdframe;\n") + 1;
		ImageBytes = calloc(size_ , sizeof(BYTE));
		Buffer = calloc(fulllen, sizeof(BYTE));
		IStream_Seek(ImageStream, offset, STREAM_SEEK_SET, &_size_);
		IStream_Read(ImageStream, ImageBytes, size_);
		strcat((BYTE*)Buffer, (BYTE*)"rdframe;\n");
		_offset = strlen("rdframe;\n");
		p = ImageBytes;
		for (int i = 0; i < size_; ++i) {
			Buffer[_offset + i] = *p++;
		}
		if (send_dv(s, Buffer, fulllen, 0) < 0)
		{
			kill_rdp = TRUE;
			break;
		}
		free(ImageBytes);
		free(Buffer);
		IStream_Release(ImageStream);
	}

}

 DWORD CALLBACK RDP_THREAD(SOCKETDATA* sd) {
	SOCKET s = sd->s;
	char** tags,*srvr_recv = NULL;
    loop {
		if (kill_rdp) {
			break;
		}

        srvr_recv = calloc(1024, sizeof(char*));
        if ((recv(s, srvr_recv, 1024 - 1, 0)) == SOCKET_ERROR)
        {
			kill_rdp = TRUE;
            break;
        }

		if (strstr(srvr_recv, _RDP_END) != NULL) {
			free(srvr_recv);
			kill_rdp = TRUE;
			break;
		}
        
		if (strstr(srvr_recv, _RDP_MOUSE_MOVE)) {
            tags = split(srvr_recv, "\n");
            float x = (float)atoi(tags[1]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CXSCREEN));
            float y = (float)atoi(tags[2]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CYSCREEN));
            INPUT input;
            input.type = INPUT_MOUSE;
            input.mi.mouseData = 0;
            input.mi.dx = x;
            input.mi.dy = y;
            input.mi.dwFlags = MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE;
            SendInput(1, &input, sizeof(input));
            free(tags);
        }
        else if (strstr(srvr_recv, _RDP_PRESS_LEFT_MOUSE) != NULL) {
            tags = split(srvr_recv, "\n");
            float x = (float)atoi(tags[1]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CXSCREEN));
            float y = (float)atoi(tags[2]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CYSCREEN));
            INPUT input;
            input.type = INPUT_MOUSE;
            input.mi.mouseData = 0;
            input.mi.dx = x;
            input.mi.dy = y;
            input.mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP;
            SendInput(1, &input, sizeof(input));
            free(tags);
        }
        else if (strstr(srvr_recv, _RDP_RELEASE_LEFT_MOUSE) != NULL) {
            tags = split(srvr_recv, "\n");
            float x = (float)atoi(tags[1]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CXSCREEN));
            float y = (float)atoi(tags[2]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CYSCREEN));
            INPUT input;
            input.type = INPUT_MOUSE;
            input.mi.mouseData = 0;
            input.mi.dx = x;
            input.mi.dy = y;
            input.mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP;
            SendInput(1, &input, sizeof(input));
            free(tags);
        }
		else if (strstr(srvr_recv, _RDP_PRESS_RIGHT_MOUSE) != NULL) {
			tags = split(srvr_recv, "\n");
			float x = (float)atoi(tags[1]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CXSCREEN));
			float y = (float)atoi(tags[2]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CYSCREEN));
			INPUT input;
			input.type = INPUT_MOUSE;
			input.mi.mouseData = 0;
			input.mi.dx = x;
			input.mi.dy = y;
			input.mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP;
			SendInput(1, &input, sizeof(input));
			free(tags);
		}
		else if (strstr(srvr_recv, _RDP_RELEASE_LEFT_MOUSE) != NULL) {
			tags = split(srvr_recv, "\n");
			float x = (float)atoi(tags[1]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CXSCREEN));
			float y = (float)atoi(tags[2]) * (float)(65536.0 / (float)GetSystemMetrics(SM_CYSCREEN));
			INPUT input;
			input.type = INPUT_MOUSE;
			input.mi.mouseData = 0;
			input.mi.dx = x;
			input.mi.dy = y;
			input.mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTUP;
			SendInput(1, &input, sizeof(input));
			free(tags);
		}
		
        free(srvr_recv);

    }
}