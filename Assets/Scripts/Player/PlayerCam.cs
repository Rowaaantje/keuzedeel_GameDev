using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation; //stores the direction you are facing

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HandleMouseInput();

    }

    void OnGUI()
    {
        float crosshairSize = 5f;
        float xMin = (Screen.width / 2) - (crosshairSize / 2);
        float yMin = (Screen.height / 2) - (crosshairSize / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairSize, crosshairSize), Texture2D.whiteTexture);
    }

    void HandleMouseInput()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _yRotation += mouseX; //add the x input to your y rotation

        _xRotation -= mouseY; //substract y input from the x rotation

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); //max camera rotation in the x axis

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
