using UnityEngine;
public class InputSystemScript : MonoBehaviour 
    { public GameObject Player; 
void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.Q)) { 
            Player = GameObject.Find("Player"); 
            Player.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f); 
            Debug.Log("Q KeyCode button clicked!"); } 

        if (Input.GetKeyDown(KeyCode.E)) {
            Player = GameObject.Find("Player");
            Player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f); 
            Debug.Log("E KeyCode button clicked!"); } 

        if (Input.GetKeyDown(KeyCode.F1)) { 
            Player = GameObject.Find("Player"); 
            Destroy(Player); 
            Debug.Log("F1 KeyCode button clicked!"); 
        } 
    } 
}