#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <winsock2.h>

#pragma comment(lib, "Ws2_32.lib")

#include "_http_request.h"


//TODO: move function to main
char* get_via_socket(char* request, char* ip) {
    WSADATA wsa;
    SOCKET s;
    int recv_size = 0;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
    {
        return NULL;
    }
    if ((s = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET)
    {
        return NULL;
    }
    struct sockaddr_in server;
    server.sin_addr.s_addr = inet_addr(ip);
    server.sin_family = AF_INET;
    server.sin_port = htons(80);
    char* response = calloc(1024,sizeof(char*));
    if (connect(s, (struct sockaddr*)&server, sizeof(server)) < 0)
    {
        return NULL;
    }
    if (send(s, request, strlen(request), 0) < 0)
    {
        return NULL;
    }
    if (recv_size = recv(s, response, 1024 + 1, 0) == SOCKET_ERROR)
    {
        return NULL;
    }
    char* content = strstr(response, "\r\n\r\n");
    if (content != NULL) {
        content += 4; 
    }
    else {
        content = response;
    }
    return content;
}
