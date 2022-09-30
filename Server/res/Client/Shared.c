#include "Shared.h"

char** split(const char* str, const char* delim) {
	char* s = _strdup(str);
	if (strtok(s, delim) == 0) {
		return NULL;
	}
	int nw = 1;
	while (strtok(NULL, delim) != 0)
		nw += 1;
	strcpy(s, str);
	char** v = malloc((nw + 1) * sizeof(char*));
	int i;
	v[0] = _strdup(strtok(s, delim));
	for (i = 1; i != nw; ++i) {
		v[i] = _strdup(strtok(NULL, delim));
	}
	v[i] = NULL;
	free(s);
	return v;
}

char* wchar_to_char(const wchar_t* pwchar)
{
	char szTo[_1KB];
	szTo[lstrlenW(pwchar)] = '\0';
	WideCharToMultiByte(CP_ACP, 0, pwchar, -1, szTo, (int)lstrlenW(pwchar), NULL, NULL);
	char* chr = _strdup(szTo);
	return chr;
}


wchar_t* charToWChar(const char* text)
{
	wchar_t *wszTo = calloc(strlen(text) + 2 , sizeof(wchar_t));
	wszTo[strlen(text)] = L'\0';
	MultiByteToWideChar(CP_ACP, 0, text, -1, wszTo, (int)strlen(text));
	wchar_t* wchr = StrDupW(wszTo);
	return wchr;
}

char* inttostr(int n) {
	char* result = calloc(4096,sizeof(char));
	sprintf(result, "%d", n);
	return result;
}


