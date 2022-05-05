using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody playerRb;
    public float playerSpeed = 10.0f;
    public float defaultSpeed = 10.0f;

    public float pickUpRange = 1.5f;
    public bool pickedUp = false;
    private GameObject heldObj;
    public Transform holdParent;
    public SphereCollider playerCollider;
    private GameManager gameManagerScript;

    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        //playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManagerScript.isGameActive)
        {
            MovePlayer();

            if (Input.GetKey(KeyCode.Space))
            {
                if (heldObj == null)
                {
                    RaycastHit hit;
                    Vector3 centerOfSphere1 = transform.position;

                    if (Physics.SphereCast(centerOfSphere1, playerCollider.radius, transform.forward, out hit, pickUpRange) && hit.transform.CompareTag("Toys"))
                    {

                        PickupObject(hit.transform.gameObject);
                    }

                }

                if (heldObj != null)
                {
                    MoveObject();
                }
            }
            else if (heldObj != null)
            {
                DropObject();
            }


            
        }
         
    }


    private void MovePlayer()
    {
        Vector3 pInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        playerRb.MovePosition(transform.position + pInput * playerSpeed * Time.deltaTime);
        if (pInput != Vector3.zero)
        {
            transform.forward = pInput;
            playerAnim.SetFloat("Speed_f", 1.0f);
        }
        else if (pInput == Vector3.zero)
        {
            playerAnim.SetFloat("Speed_f", 0);
        }
        
    }

    void PickupObject(GameObject pickObj)
    {
        pickedUp = true;
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();


            //Destroy(objRig);

            objRig.isKinematic = true;
            objRig.useGravity = false;


            objRig.transform.parent = holdParent;
            heldObj = pickObj;

        }
    }

    void MoveObject()
    {

        //heldObj.transform.position = (transform.position + transform.forward + transform.up);
        heldObj.transform.position = (holdParent.position);

    }

    void DropObject()
    {
        pickedUp = false;
        playerSpeed = defaultSpeed;
        Rigidbody heldRig = heldObj.GetComponent<Rigidbody>();
        heldRig.isKinematic = false;
        heldRig.useGravity = true;
        

        heldObj.transform.parent = null;
        heldObj = null;
    }
}
