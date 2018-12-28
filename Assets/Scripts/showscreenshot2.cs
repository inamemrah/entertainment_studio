using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class showscreenshot2 : MonoBehaviour {

	private int screenshotCount = 0;

	string ActiveUrl;

	Texture2D screenCap;
	Texture2D border;
	private bool shot = false;

	public GameObject ssButton;
    public GameObject SettingsPanel;

	string UnityClass = "com.unity3d.player.UnityPlayer";
	private string filePath;
	private string movePath;


    
    



    void Start () {
		screenCap = new Texture2D(300, 200, TextureFormat.RGB24, false); // 1
		border = new Texture2D(2, 2, TextureFormat.ARGB32, false); // 2
		border.Apply();

		ssButton.SetActive (true);
		
	}

	public void ScreenShot()
	{
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
            SettingsPanel.SetActive(false);
			captureScreenshot ("true");
			

		}
	}

/*	public void Update()
	{
        


		if (shot == true) 
		{
			if (File.Exists (Application.persistentDataPath + "/" + filePath)) 
			{
				if (Application.platform == RuntimePlatform.Android) {
					File.Move (Application.persistentDataPath + "/" + filePath, movePath);
                    
				}
				shot = false;

				if (Application.platform == RuntimePlatform.Android) {
					
				}
			}
		}
	}
 */	



	void captureScreenshot(string result){
		if (result == "true") {
			StartCoroutine(Capture());
		} 
	}


 public	IEnumerator Capture(){
		yield return new WaitForEndOfFrame();
		screenCap.ReadPixels(new Rect(198, 98, 298, 198), 0, 0);
		screenCap.Apply();

		// Encode texture into PNG
		byte[] bytes = screenCap.EncodeToPNG();
		//Object.Destroy(screenCap);

		// For testing purposes, also write to a file in the project folder

		string screenshotFilename = "Green_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".JPG";

		ScreenCapture.CaptureScreenshot (screenshotFilename);


		if (Application.platform == RuntimePlatform.Android) {

			string myFolderLocation = "/sdcard/DCIM/Asd/";


			if (!System.IO.Directory.Exists(myFolderLocation))
			{
				System.IO.Directory.CreateDirectory(myFolderLocation);
			}

			movePath = myFolderLocation + screenshotFilename;



		} 


		filePath = screenshotFilename;

		shot = true;

        SettingsPanel.SetActive(true);
		


		




	}







	


	

}