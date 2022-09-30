#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <stdlib.h>
#include <string.h>


#pragma comment(lib, "ws2_32.lib")

#include "Shared.h"

#define START_HEADER "----------START-DEVIL-PROTOCOL----------\r\n"
#define END_HEADER "\r\n----------END-DEVIL-PROTOCOL----------\r\n"
#define CONTENT_LENGTH_HEADER "tent-ln:"


int send_dv(SOCKET,void* , int,int );