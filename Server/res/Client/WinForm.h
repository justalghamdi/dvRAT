#pragma once



typedef struct data {
	SOCKET* s;
	HINSTANCE* hInstance;
} DATA;

DWORD CALLBACK show_chat_form(DATA *_data);