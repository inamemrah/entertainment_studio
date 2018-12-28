using UnityEngine;
using UnityEngine.UI;


internal class FigpixCamera : MonoBehaviour
{

    public RawImage Camera_;
    private static WebCamTexture CameraTex_;


    private static int width = 1920;
    private static int height = 1080;
    private static int fps = 60;


    void Start()
    {
        

        CameraTex_ = new WebCamTexture(width, height, fps);
       

        Camera_.texture = CameraTex_;
        Camera_.material.mainTexture = CameraTex_;


        CameraTex_.Play();
    }




    public static void DestroyCamera()
    {
        CameraTex_.Stop();
    }

}
