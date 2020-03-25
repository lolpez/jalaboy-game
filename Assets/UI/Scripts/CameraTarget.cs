using UnityEngine;

public class CameraTarget : MonoBehaviour
{

    /// <summary>
    /// Step #1
    /// We need a simple reference of joystick in the script
    /// that we need add it.
    /// </summary>
	[SerializeField] private Movement Joystick = null;//Joystick reference for assign in inspector
    [SerializeField] private float Speed = 5;   
    [SerializeField] private float maxLookUp = 270f;
    [SerializeField] private float maxLookDown = 85f;

    void Update()
    {
        //Step #2
        //Change Input.GetAxis (or the input that you using) to Joystick.Vertical or Joystick.Horizontal
        float v = Joystick.Vertical; //get the vertical value of joystick
        float h = Joystick.Horizontal;//get the horizontal value of joystick

        //in case you using keys instead of axis (due keys are bool and not float) you can do this:
        //bool isKeyPressed = (Joystick.Horizontal > 0) ? true : false;
        if ((transform.eulerAngles.x - v < maxLookDown) || (transform.eulerAngles.x - v > maxLookUp))
        {
            Vector3 translate = (new Vector3(-v, 0.0f, 0.0f) * Time.deltaTime) * Speed;
            transform.Rotate(translate, Space.Self);
            transform.parent.Rotate((new Vector3(0.0f, h, 0.0f) * Time.deltaTime) * Speed, Space.Self);
        }
        
    }
}