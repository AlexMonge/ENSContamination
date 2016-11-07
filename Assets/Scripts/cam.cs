using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour
{
    public Camera Camera1;
    public Camera Camera2;
    public Camera Camera3;

    int active = 1;

    void Start()
    {
        Camera1.enabled = true;
        Camera2.enabled = false;
        Camera3.enabled = false;  
    }


    void Update()
    {

        //use whatever button you want to toggle
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (active)
            {
                case 1:
                    Camera1.enabled = false;
                    Camera3.enabled = true;
                    active = 3;
                    break;

                case 2:
                    Camera2.enabled = false;
                    Camera1.enabled = true;
                    active = 1;
                    break;

                case 3:
                    Camera3.enabled = false;
                    Camera2.enabled = true;
                    active = 2;
                    break;  
            }   
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (active)
            {
                case 1:
                    Camera1.enabled = false;
                    Camera2.enabled = true;
                    active = 2;
                    break;

                case 2:
                    Camera2.enabled = false;
                    Camera3.enabled = true;
                    active = 3;
                    break;

                case 3:
                    Camera3.enabled = false;
                    Camera1.enabled = true;
                    active = 1;
                    break;
            }
        }
    }
}