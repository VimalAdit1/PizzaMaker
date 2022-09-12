
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float strafeSpeed = 2f;
    float move;
    float strafe;
    public Rigidbody rb;
    public bool isPaused;
    public MoveCamera camera;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            strafe = Input.GetAxis("Horizontal");
            move = Input.GetAxis("Vertical");
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * move * Time.deltaTime * moveSpeed );
        Vector3 side = Vector3.right * strafe * Time.deltaTime * strafeSpeed;
        transform.Translate(side);
    }
   

}

