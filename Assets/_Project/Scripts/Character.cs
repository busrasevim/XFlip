using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
   [SerializeField] private Rigidbody _motorbikeRB;
    private float _moveSpeed = 10f;
    public Transform rotatePoint;

    private int _flipCount;

    public WheelCollider fWheel;
    public WheelCollider bWheel;

    protected void Construct()
    {
        
    }

    internal protected void MoveForward()
    {
      //  motorbikeRB.transform.position += Vector3.forward * Time.deltaTime * 15f;
       // _motorbikeRB.velocity = new Vector3(0f, _motorbikeRB.velocity.y, _moveSpeed);


        fWheel.motorTorque = 1000f;
        bWheel.motorTorque = 1000f;
        //duruma göre hareket ilerleme kodu deðiþebilir
    }

    internal protected bool IsTouchGround()
    {
        WheelHit hit;
        bool isTouch = fWheel.GetGroundHit(out hit) || bWheel.GetGroundHit(out hit);

        return isTouch;
    }

    internal protected void RotateMotorbike()
    {
        transform.RotateAround(rotatePoint.position, Vector3.right, 100f*Time.deltaTime);
    }

    internal protected void SetFlipCount(int count,bool isReset=false)
    {
        _flipCount += count;

        if (isReset) _flipCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("triggerenter");
    }

    private void OnTriggerExit(Collider other)
    {
        print("triggerexit");
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collisionenter");
    }

    private void OnCollisionExit(Collision collision)
    {
        print("collisionexit");
    }

    
    
}
