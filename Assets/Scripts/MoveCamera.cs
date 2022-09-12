
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    float mouseSensitivity = 10000f;
    float rotation = 0;
    public GameObject crossHair;
    float xRotation;
    float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        xRotation = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        yRotation = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        
                player.transform.Rotate(Vector3.up, xRotation);
                rotation -= yRotation;
                rotation = Mathf.Clamp(rotation, -90, 90);
                transform.localRotation = Quaternion.Euler(rotation, 0, 0);

    }

}
