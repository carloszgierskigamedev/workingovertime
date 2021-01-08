using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    HingeJoint doorHingeJoint;
    Rigidbody rigidBody;
    [SerializeField] private bool closedDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        closedDoor = true;
        doorHingeJoint = GetComponent<HingeJoint>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {  
        JointLimits hingeJointLimits = doorHingeJoint.limits;
        float yRotation = transform.rotation.eulerAngles.y;
        
        OpenAndCloseCycle(yRotation, doorHingeJoint, hingeJointLimits);
        LockDoorOnPositionZero(yRotation, doorHingeJoint, hingeJointLimits);
    }

    void OpenAndCloseCycle(float yRotation, HingeJoint hingeJoint, JointLimits hingeJointLimits)
    {
        if (closedDoor && (yRotation > 5f && yRotation <= 90f))
        {
            Debug.Log(closedDoor);
            Debug.Log(yRotation);
            hingeJointLimits.min = 0f;
            hingeJointLimits.max = 90f;
            hingeJoint.limits = hingeJointLimits;
            closedDoor = false;
        }
        else if (closedDoor && (yRotation >= 270f && yRotation < 355))
        {
            hingeJointLimits.min = -90f;
            hingeJointLimits.max = 0f;
            hingeJoint.limits = hingeJointLimits;
            closedDoor = false;
        } 
    }

    void LockDoorOnPositionZero(float yRotation, HingeJoint hingeJoint, JointLimits hingeJointLimits)
    {
        if (!closedDoor && (yRotation <= 4f || yRotation >= 356f))
        {
            Debug.Log(yRotation);
            hingeJointLimits.max = 0f;
            hingeJointLimits.min = 0f;
            hingeJoint.limits = hingeJointLimits;            
            
            yRotation = Mathf.Round(yRotation);

            if (yRotation == 0f || yRotation == 360f)
            {
                hingeJointLimits.max = 90f;
                hingeJointLimits.min = -90f;
                hingeJoint.limits = hingeJointLimits;
                closedDoor = true;
            }
        }
    }
}
