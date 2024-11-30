using UnityEngine;

public class PlayerCam : MonoBehaviour
{
   public float sensX;
   public float sensY;

   public Transform orientation; //stores the direction you are facing 

   float xRotation;
   float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MyInput();
      
    }
    void MyInput()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;


        yRotation += mouseX; //add the x input to your y rotation 

        xRotation -= mouseY; //substract y input from the x rotation

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //max camera rotation in the x axis

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
