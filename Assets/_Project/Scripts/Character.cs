using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
   [SerializeField] private Rigidbody _motorbikeRB;
    private float _moveSpeed = 10f;
    public Transform rotatePoint;

    private int _flipCount;

    protected void Construct()
    {
        
    }

    internal protected void MoveForward()
    {
      //  motorbikeRB.transform.position += Vector3.forward * Time.deltaTime * 15f;
        _motorbikeRB.velocity = new Vector3(0f, _motorbikeRB.velocity.y, _moveSpeed);

        //duruma göre hareket ilerleme kodu deðiþebilir
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
}
