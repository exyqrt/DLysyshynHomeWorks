using UnityEngine;

public class BasicScripts2 : MonoBehaviour
{
    public Light Mylight;
    public Vector3 MyVector;

    void Start()
    {
        Mylight.intensity = 1.5f; 
        Mylight.color = Color.red; 

        
        MyVector = new Vector3(20f, 24.77f, 89.871f);
    }
}