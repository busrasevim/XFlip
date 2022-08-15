using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody _motorbikeRB;
    private float _moveSpeed = 10f;
    private float motorTorque = 1000f;
    private float boostMultiple = 1f;
    public Transform rotatePoint;

    private int _flipCount;
    private float _totalFlipAngle;

    public WheelCollider fWheel;
    public WheelCollider bWheel;

    private bool onBoost;
    private float boostTimer;
    private float defaultBoostTime = 2f;
    private float plusBoostTime = 1f;
    protected void Construct()
    {

    }

    internal protected void MoveForward()
    {
        //  motorbikeRB.transform.position += Vector3.forward * Time.deltaTime * 15f;
        // _motorbikeRB.velocity = new Vector3(0f, _motorbikeRB.velocity.y, _moveSpeed);


        fWheel.motorTorque = motorTorque * boostMultiple;
        bWheel.motorTorque = motorTorque * boostMultiple;
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
        transform.RotateAround(rotatePoint.position, Vector3.right, 100f * Time.deltaTime);
        _totalFlipAngle += 1000f * Time.deltaTime;

        if (_totalFlipAngle >= 360f)
        {
            _totalFlipAngle -= 360f;
            SetFlipCount(1);
        }
    }

    internal protected void SetFlipCount(int count, bool isReset = false)
    {
        _flipCount += count;

        if (isReset) _flipCount = 0;

        if (_flipCount > 1)
        {
            //boost açýlacak
            //eðer boost açýksa süresine ekleme yapýlacak
            StartCoroutine(Boost());
        }
    }

    internal protected virtual void EndLevel(bool isWin)
    {

    }

    private IEnumerator Boost()
    {
        if (onBoost)
        {
            boostTimer += plusBoostTime;
            yield break;
        }

        onBoost = true;
        boostTimer = defaultBoostTime;

        boostMultiple = 2f;

        while (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;
            yield return null;
        }

        DOTween.To(() => boostMultiple, x => boostMultiple = x, 1f, 1f);

    }
}
