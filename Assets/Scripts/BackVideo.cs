using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
//using UnityEditor;
using SFB;
using System;


public class BackVideo : MonoBehaviour {

	string path;
	public GameObject gameee;
	public Camera cam;
    public VideoPlayer vPlayer;

	public void OpenExplorer()
	{
        //path = EditorUtility.OpenFilePanel ("Video", "", "mp4");

        var extensions = new[] {
            new ExtensionFilter("Video", "", "mp4" )
        };

        var Bg = StandaloneFileBrowser.OpenFilePanel("Select Background", PlayerPrefs.GetString("_BackgroundPath"), extensions, false);
        path = Bg[0];

        GetVideo ();
	}

	void GetVideo()
	{
		if (path != null) 
		{
			UpdateVideo ();
		}

	}


	void UpdateVideo ()
	{
	    vPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
		vPlayer.url = "file://" + path;
		vPlayer.isLooping = true;
        vPlayer.playbackSpeed = 1f;
		vPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraFarPlane;
		vPlayer.targetCamera = cam;
		vPlayer.targetCameraAlpha = 1f;
		vPlayer.prepareCompleted += Prepared;
		vPlayer.Prepare();
        
	}


	void Prepared(UnityEngine.Video.VideoPlayer vPlayer)
    {
		Debug.Log("Prepared...");
		vPlayer.Play();
     
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            vPlayer.Stop();
            Debug.Log("Stop...");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            
            vPlayer.Pause();
            Debug.Log("pause..");
        }

        else if(Input.GetKeyDown(KeyCode.S))
        {
            vPlayer.Play();
            Debug.Log("play..");
        }

        else if(Input.GetKeyDown(KeyCode.D))
        {
            Destroy(vPlayer);
            Debug.Log("destroy...");
        }
    }





}