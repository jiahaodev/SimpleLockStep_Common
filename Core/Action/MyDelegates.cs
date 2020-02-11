/****************************************************
	文件：MyDelegates.cs
	作者：JiahaoWu
	邮箱: jiahaodev@163.ccom
	日期：2020/02/10 22:15   	
	功能：Action回调声明
*****************************************************/

public delegate void ActionCallback();

public delegate void ActionCallback<in T>(T value);
