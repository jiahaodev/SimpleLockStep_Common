/****************************************************
	�ļ���MyDelegates.cs
	���ߣ�JiahaoWu
	����: jiahaodev@163.ccom
	���ڣ�2020/02/10 22:15   	
	���ܣ�Action�ص�����
*****************************************************/

public delegate void ActionCallback();

public delegate void ActionCallback<in T>(T value);
