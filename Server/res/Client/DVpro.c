#include "DVpro.h"


int send_dv(SOCKET s,void* buffer, int buffersize, int flag) {
	int start_header_len = strlen(START_HEADER);
	int end_header_len = strlen(END_HEADER);
	int content_length_header_len = strlen(CONTENT_LENGTH_HEADER);
	char* content_length = inttostr(buffersize);
	int content_length_len = strlen(content_length);
	char* NL = "\r\n\r\n";
	int NL_len = strlen(NL);
	int full_data_length = buffersize + start_header_len  + content_length_header_len + content_length_len + NL_len ;
	BYTE* _buffer = calloc(full_data_length , sizeof(BYTE));
	strncat((BYTE*)_buffer, (BYTE*)START_HEADER, start_header_len);
	strncat((BYTE*)_buffer, (BYTE*)CONTENT_LENGTH_HEADER, content_length_header_len);
	strncat((BYTE*)_buffer, (BYTE*)content_length, content_length_len);
	strncat((BYTE*)_buffer, NL, NL_len);
	int offset = strlen(_buffer);
	BYTE* p = buffer;
	for (int i = 0; i < buffersize; i++) {
		_buffer[offset + i] = p[i];

	}
	int length = full_data_length;
	BYTE* ptr = _buffer;
	while (length > 0)
	{
		int i = send(s, ptr, full_data_length,flag);
		if (i < 1) { 
			return i;
		}
		ptr += i;
		length -= i;
	}
	free(_buffer);
	return send(s, END_HEADER, end_header_len, flag);
}