#pragma once



typedef struct data {
	SOCKET* s;
	HINSTANCE* hInstance;
} DATA;

DWORD WINAPI show_chat_form(DATA *_data);