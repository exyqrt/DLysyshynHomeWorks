using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;

    void Start()
    {
        // Виключаємо першу та другу камери
        camera1.enabled = false;
        camera2.enabled = false;

        // Вмикаємо третю камеру
        camera3.enabled = true;
    }
}
