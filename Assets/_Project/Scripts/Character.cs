using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Character : MonoBehaviour
{
    [SerializeField] internal GameObject motorBike;
    [SerializeField] internal Rigidbody _motorbikeRB;
    private float _moveSpeed = 10f;
    protected float motorTorque = 5000f;
    protected float boostMultiple = 1f;
    public Transform rotatePoint;

    private int _flipCount;
    private float _totalFlipAngle;

    public WheelCollider fWheel;
    public WheelCollider bWheel;

    private bool onBoost;
    private float boostTimer;
    private float defaultBoostTime = 2f;
    private float plusBoostTime = 1f;
    private Tween boostFinishTween;

    [SerializeField] private Animator characterAnimator;
    private string onSkyAnimIntParameter = "OnSkyAnim";
    private string onSkyAnimBoolParameter = "OnSky";

    public int characterOrder;
    public float finishDistance;

    internal protected void Construct()
    {
        LevelManager.Instance.StartAction += MoveForward;
    }

    internal protected void MoveForward()
    {
        fWheel.motorTorque = motorTorque * boostMultiple;
        bWheel.motorTorque = motorTorque * boostMultiple;
    }

    internal protected bool IsTouchGround()
    {
        WheelHit hit;
        bool isTouch = fWheel.GetGroundHit(out hit) || bWheel.GetGroundHit(out hit);

        return isTouch;
    }

    internal protected void RotateMotorbike()
    {
        transform.RotateAround(rotatePoint.position, Vector3.right, -100f * Time.deltaTime);
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

        boostFinishTween?.Kill();
        boostMultiple = 2f;

        while (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
            yield return null;
        }

        onBoost = false;
        boostFinishTween = DOTween.Sequence().Append(DOTween.To(() => boostMultiple, x => boostMultiple = x, 1f, 1f)).OnComplete(() =>
        {
            boostFinishTween = null;
        });

    }

    public void SetOnSkyAnimation(bool isOnSky)
    {
        if (isOnSky)
        {
            int randomParameterIndex = Random.Range(0, 3);
            characterAnimator.SetInteger(onSkyAnimIntParameter, randomParameterIndex);
        }

        characterAnimator.SetBool(onSkyAnimBoolParameter, isOnSky);
    }

    public void ComputeDistance()
    {
        //yol hesaplanacak kalan, finish le aralarýndaki uzaklýk alýnabilir
        finishDistance = Vector3.Distance(motorBike.transform.position, GameManager.Instance.finishObject.position);
    }

   
}
