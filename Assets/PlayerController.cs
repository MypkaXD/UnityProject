using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject cam;
    
    Quaternion StartingRotation;

    float Ver, Hor, Jump, RotatorHor, RotatorVer;
    bool isGround;
    public float speed = 10, JumpSpeed = 200, sensivity = 5;

    private void Start()
    {
        StartingRotation = transform.rotation;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    private void FixedUpdate()
    {
        RotatorHor += Input.GetAxis("Mouse X") * sensivity;
        RotatorVer += Input.GetAxis("Mouse Y") * sensivity;

        RotatorVer = Mathf.Clamp(RotatorVer, -60, 60);
        Quaternion RotY = Quaternion.AngleAxis(RotatorHor, Vector3.up);
        Quaternion RotX = Quaternion.AngleAxis(-RotatorVer, Vector3.right);

        cam.transform.rotation = StartingRotation * transform.rotation * RotX;
        transform.rotation = StartingRotation * RotY;

        if (isGround)
        {
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            Jump = Input.GetAxis("Jump") * Time.deltaTime * JumpSpeed;
            GetComponent<Rigidbody>().AddForce(transform.up * Jump, ForceMode.Impulse);
        }

        transform.Translate(new Vector3(Hor,0, Ver));
    }
}
