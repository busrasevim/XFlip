using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public abstract class Character : MonoBehaviour
{
    [SerializeField] internal GameObject motorBike;
    [SerializeField] internal Rigidbody _motorbikeRB;
    private float _moveSpeed = 10f;
    protected float motorTorque = 3000f;
    protected float boostMultiple = 1f;
    public Transform rotatePoint;
    private float rotateSpeed = 250f;

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
    protected bool isFinished;
    internal protected bool isSucceed;

    private bool frontWheel;
    private bool backWheel;
    [SerializeField] private Animator frontWheelAnimator;
    [SerializeField] private Animator backWheelAnimator;

    [SerializeField] private TextMeshPro _orderText;

    internal protected void Construct()
    {
        LevelManager.Instance.StartAction += StartGame;
        _motorbikeRB.isKinematic = true;

        backWheelAnimator.speed = 0f;
        frontWheelAnimator.speed = 0f;
    }

    protected virtual void StartGame()
    {
        _motorbikeRB.isKinematic = false;
        MoveForward();
        _orderText.gameObject.SetActive(true);
    }
    internal protected void MoveForward()
    {
        SetWheelTorque(motorTorque);
    }

    internal protected bool IsTouchGround()
    {
        WheelHit hit;

        frontWheel = fWheel.GetGroundHit(out hit);
        backWheel = bWheel.GetGroundHit(out hit);

        bool isTouch = frontWheel || backWheel;

        return isTouch;
    }

    internal protected void SetWheelAnimationsSpeed()
    {
        SetWheelAnimationSpeed(frontWheelAnimator, frontWheel);
        SetWheelAnimationSpeed(backWheelAnimator, backWheel);
    }

    private void SetWheelAnimationSpeed(Animator wheelAnimator, bool wheelBool)
    {
        if (wheelBool)
        {
            wheelAnimator.speed = _motorbikeRB.velocity.z;
        }
        else
        {
            wheelAnimator.speed = Mathf.Lerp(wheelAnimator.speed, 0f, 0.1f);
        }
    }

    internal protected void RotateMotorbike()
    {
            transform.RotateAround(rotatePoint.position, Vector3.right, -rotateSpeed * Time.deltaTime);
        _totalFlipAngle += (rotateSpeed * Time.deltaTime);

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
        isSucceed = isWin;
        isFinished = true;

        boostFinishTween?.Kill();
        SetWheelTorque(0);

        if (!isWin)
        {
            _motorbikeRB.constraints = RigidbodyConstraints.None;

            Vector3 velocity = _motorbikeRB.velocity;
            DOTween.To(() => velocity, x => velocity = x, Vector3.zero, 1f)
                .OnUpdate(() => {
                    _motorbikeRB.velocity = velocity;
                });
        }
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

        SetWheelTorque();

        while (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
            yield return null;
        }

        onBoost = false;
        boostFinishTween = DOTween.Sequence().Append(DOTween.To(() => boostMultiple, x => boostMultiple = x, 1f, 1f)).OnUpdate(() =>
        {
            SetWheelTorque();
        }).OnComplete(() =>
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
        if (isFinished && isSucceed)
        {
            finishDistance = 0f;
            return;
        }

        //yol hesaplanacak kalan, finish le aralarýndaki uzaklýk alýnabilir
        finishDistance = GameManager.Instance.finishObject.position.z - motorBike.transform.position.z;
    }

    public void SetAngularVelocity()
    {
        _motorbikeRB.angularVelocity = Vector3.zero;
    }

    public void SetWheelTorque()
    {
        fWheel.motorTorque = motorTorque * boostMultiple;
        bWheel.motorTorque = motorTorque * boostMultiple;
    }

    public void SetWheelTorque(float torqueValue)
    {
        fWheel.motorTorque = torqueValue * boostMultiple;
        bWheel.motorTorque = torqueValue * boostMultiple;
    }

    public void SetCharacterOrderText()
    {
        if (isSucceed) return;

        _orderText.text = characterOrder.ToString();
    }

    internal protected void ClampVelocity()
    {
        _motorbikeRB.velocity = new Vector3(0f, _motorbikeRB.velocity.y, Mathf.Clamp(_motorbikeRB.velocity.z, 1f, _motorbikeRB.velocity.z));

    }
}
