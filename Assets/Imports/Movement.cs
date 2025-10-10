using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject camObject;
    public GameObject gunHolder;
    private CharacterController cc;
    float rotationX = 0;
    float rotationY = 0;
    public float moveSpeed;
    Vector3 currentMoveVelocity = Vector3.zero;
    Vector3 currentForceVelocity = Vector3.zero;
    public float gravity;
    Vector3 moveDampVelocity;
    public float MoveSmoothTime;
    Vector3 decayingForce = Vector3.zero;

    Vector3 impulseForceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        camControls();
        basicMovement();
        forceDecay();
    }

    void camControls()
    {
        rotationX = Input.GetAxis("Mouse X");
        rotationY -= Input.GetAxis("Mouse Y");
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        gameObject.transform.Rotate(0, rotationX, 0);
        camObject.transform.rotation = Quaternion.Euler(rotationY, gameObject.transform.rotation.eulerAngles.y, 0);
        
    }

    void basicMovement()
    {
        float x = Input.GetAxisRaw("Horizontal"); 
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.TransformDirection(new Vector3(x, 0, z));

        currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, moveDir * moveSpeed, ref moveDampVelocity, MoveSmoothTime);


        Debug.DrawRay(transform.position - new Vector3(0, cc.height / 2, 0), Vector3.down * 0.2f);

        //   **** jump mechanics and gravity ****    //
        RaycastHit hit;
        if (Physics.Raycast(transform.position - new Vector3(0, cc.height / 2, 0),Vector3.down,out hit,0.2f))
        {

            currentMoveVelocity.y = 0f;//prevent character from bouncing

            currentForceVelocity.y = -0.03f;

            decayingForce.y = 0f;
            cc.enabled = false;
            //transform.position = new Vector3(transform.position.x, hit.point.y + cc.height *0.9f, transform.position.z);
            cc.enabled = true;

            if (Input.GetButtonDown("Jump"))
            {
                impulseForce(new Vector3(0, 20, 0));
            }
        }
        else
        {
            //gunHolder.transform.Rotate(new Vector3(-1,0,0));
            currentForceVelocity.y -= gravity * Time.deltaTime;
        }
        //      ********    //

        //  ***** wall climb ****   //
  /*      if (Physics.Raycast(transform.position, transform.forward,0.7f) && Input.GetAxisRaw("Vertical") >=0.5f)
        {
           
            currentForceVelocity.y = 12f;
        }*/
        //      ********    //
        cc.Move(currentMoveVelocity * Time.deltaTime);
        cc.Move(currentForceVelocity*Time.deltaTime);



    }

    void impulseForce(Vector3 force)
    {
        decayingForce += force;
    }

    void forceDecay()
    {
        //Debug.Log(decayingForce);
        decayingForce = Vector3.MoveTowards(decayingForce,Vector3.zero, 0.1f); 
        cc.Move(decayingForce*Time.deltaTime);
    }

}
