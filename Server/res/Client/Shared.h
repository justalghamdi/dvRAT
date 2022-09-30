#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <shlwapi.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>


// START define size_s
#define _1KB 0x400
#define _2KB _1KB * 0x2
#define _4KB _1KB * 0x4
#define _9KB _1KB * 0x9
// END define size_s

// START define keywords
#define loop for (;;)
// END define keywords

char** split(const char* , const char* );
char* wchar_to_char(const wchar_t*);
wchar_t* charToWChar(const char* );
char* inttostr(int );