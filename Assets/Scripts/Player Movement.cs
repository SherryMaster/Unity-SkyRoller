using Unity.VisualScripting;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public float swipeForce = 100f; // Force applied when swiping
    public GameObject cam;
    private Rigidbody rb;
    private Vector3 touchStartPos; // Position where the touch startedchiy
    float time_count = 0;
    float max_time = 0.5f;
    bool isTimeAllow = false;

    bool canMove = false;


    float x = 0f;
    float z = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        canMove = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log("Can Move: " + canMove);
        if (canMove)
        DetectSwipe();
        if (isTimeAllow)
        {
            time_count += Time.deltaTime;
        }
        if (time_count > max_time)
        {
            rb.velocity /= 2f;
        }
    }

    void DetectSwipe()
    {
        //Debug.Log(cam.transform.right.x);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                //isTimeAllow = true;
            }
            if (touch.phase == TouchPhase.Ended) {
                time_count = 0f;
                //isTimeAllow = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                //isTimeAllow = false;
                Vector3 swipeDelta = touch.position - new Vector2(touchStartPos.x, touchStartPos.y);
                float swipeMagnitude = swipeDelta.magnitude;
                // Calculate the force vector based on the swipe direction
                Vector3 force = new Vector3(swipeDelta.x, 0, swipeDelta.y).normalized * swipeForce;
                // Apply the force to the object

                x = force.x * cam.transform.right.x - (force.z * cam.transform.right.z);
                z = force.z * cam.transform.right.x + (force.x * cam.transform.right.z);

                rb.AddForce(x * swipeMagnitude, 0, z * swipeMagnitude);
                //rb.AddForce(force.x - (force.z * Mathf.Abs(cam.transform.right.z)), 0, force.z - (force.x * Mathf.Abs(cam.transform.right.x)));
                touchStartPos = touch.position;                

            }
        }
    }
}
