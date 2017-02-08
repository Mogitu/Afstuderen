﻿using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenLinkJSPlugin()
	{
		#if !UNITY_EDITOR
		openWindow("sitemap_help.pdf");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);



}