using UnityEngine;

public class BasicScript1 : MonoBehaviour
{
    public Camera Mycamera;
    public Transform Mycube;

    void Start()
    {
        
        Mycamera.fieldOfView = 60f;
        Mycamera.backgroundColor = Color.blue;

        
        Mycube.localScale = new Vector3(2f, 2f, 2f); 
        Mycube.position = new Vector3(0f, 1f, 0f);
    }
}