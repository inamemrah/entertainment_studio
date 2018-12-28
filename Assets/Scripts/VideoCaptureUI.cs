using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace RockVR.Video.Demo
{
    public class VideoCaptureUI : MonoBehaviour
    {
        public GameObject ButtonPanel;
        public GameObject YesGreenCam;
        public GameObject YesGreenButton;
        public GameObject NoGreenCam;
        public GameObject NoGreenButton;
        public GameObject complatedText;
        
        public InputField time;

        private bool isPlayVideo = false;

        float timeLeft = 2.0f;


        // SS
        private int screenshotCount = 0;

        string ActiveUrl;

        Texture2D screenCap;
        Texture2D border;
        private bool shot = false;

        
        public GameObject SettingsPanel;

        string UnityClass = "com.unity3d.player.UnityPlayer";
        private string filePath;
        private string movePath;






        public void YesBackgroundCam()
        {
            YesGreenCam.SetActive(true);
            NoGreenButton.SetActive(false);
            YesGreenButton.SetActive(false);
        }

        public void NoBackgroundCam()
        {
            NoGreenCam.SetActive(true);
            YesGreenButton.SetActive(false);
            NoGreenButton.SetActive(false);
            

        }

       

        private void Awake()
        {
            Application.runInBackground = true;
            isPlayVideo = false;
        }




        void Start()
        {
            screenCap = new Texture2D(300, 200, TextureFormat.RGB24, false); // 1
            border = new Texture2D(2, 2, TextureFormat.ARGB32, false); // 2
            border.Apply();

            


            YesGreenCam.SetActive(false);
            NoGreenCam.SetActive(false);
            complatedText.SetActive(false);
        }


    /*    public void ScreenShot()
        {
           if (Input.GetKeyUp(KeyCode.Mouse0))
            {
               SettingsPanel.SetActive(false);
                captureScreenshot("true");


            }
        }
        */
       

        void TenSecontSS()
        {

        }


        void Update()
        {


           


            timeLeft -= Time.deltaTime;

                if (timeLeft < 0.0f)
                {
                    VideoCaptureCtrl.instance.StopCapture();
                    
                }

            int itimeleft = Mathf.FloorToInt(timeLeft);

            for(int i = 0; i<= itimeleft; i++)
            {
                if(itimeleft % 10 == 0)
                {
                    captureScreenshot("true");
                }
            }

       /*     if (itimeleft == 6)
                {
                    captureScreenshot("true");
                }

        */


            // Rec Start
            if (Input.GetKeyDown(KeyCode.R) && VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.NOT_START)
            {





                ButtonPanel.SetActive(false);

                VideoCaptureCtrl.instance.StartCapture();


            }



            // Rec Stop
            if (Input.GetKeyDown(KeyCode.Z) && VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {

                VideoCaptureCtrl.instance.StopCapture();


                if (VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.PAUSED)
                {
                    VideoCaptureCtrl.instance.StopCapture();
                }


            }



            // Pause
            if (Input.GetKeyDown(KeyCode.P) && VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {

                VideoCaptureCtrl.instance.ToggleCapture();

            }



            //Contunie
            if (Input.GetKeyDown(KeyCode.C) && VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.PAUSED)
            {
                VideoCaptureCtrl.instance.ToggleCapture();

            }

            if(VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
            {
           
                complatedText.SetActive(true);
                ButtonPanel.SetActive(true);
            }



            //Next(new)Video
            if (Input.GetKeyDown(KeyCode.N) && VideoCaptureCtrl.instance.status == VideoCaptureCtrl.StatusType.FINISH)
            {

               

                //kayıt süresi dışarıdan girilen sayıya bağlı.
                timeLeft = float.Parse(time.text);
                timeLeft -= Time.deltaTime;
            
               
                if (timeLeft < 0.0f)
                {
                    
                    VideoCaptureCtrl.instance.StopCapture();
                    

                }

               
                

                if (!isPlayVideo)
                {
                    
                }
                else
                {
                    

                    // Turn to next video.
                    VideoPlayer.instance.NextVideo();

                    
                    // Play capture video.
                    VideoPlayer.instance.PlayVideo();

                }
               
                complatedText.SetActive(true);
                ButtonPanel.SetActive(true);


                VideoCaptureCtrl.instance.StartCapture();

               
                complatedText.SetActive(false);
                ButtonPanel.SetActive(false);

            }

        }

        void captureScreenshot(string result)
        {
            if (result == "true")
            {

                StartCoroutine(Capture());
            }
        }



        public IEnumerator Capture()
        {
            yield return new WaitForEndOfFrame();
            screenCap.ReadPixels(new Rect(198, 98, 298, 198), 0, 0);
            screenCap.Apply();

            // Encode texture into PNG
            byte[] bytes = screenCap.EncodeToPNG();
            //Object.Destroy(screenCap);

            // For testing purposes, also write to a file in the project folder

            string screenshotFilename = "FigPixEntertaiment_" + System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".JPG";

            ScreenCapture.CaptureScreenshot(screenshotFilename);


            if (Application.platform == RuntimePlatform.Android)
            {

                string myFolderLocation = "/sdcard/DCIM/Asd/";


                if (!System.IO.Directory.Exists(myFolderLocation))
                {
                    System.IO.Directory.CreateDirectory(myFolderLocation);
                }

                movePath = myFolderLocation + screenshotFilename;



            }


            filePath = screenshotFilename;

            shot = true;

            








        }


    }
}