using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public abstract class Character : MonoBehaviour
{
    [SerializeField] internal GameObject motorBike;
    [SerializeField] private Rigidbody _motorbikeRB;
    private float _moveSpeed = 10f;
    protected float motorTorque = 3000f;
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
    private bool isFinished;

    [SerializeField] private TextMeshPro _orderText;

    internal protected void Construct()
    {
        LevelManager.Instance.StartAction += StartGame;
    }

    protected virtual void StartGame()
    {
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
            //boost a��lacak
            //e�er boost a��ksa s�resine ekleme yap�lacak
            StartCoroutine(Boost());
        }
    }

    internal protected virtual void EndLevel(bool isWin)
    {
        isFinished = true;
        SetWheelTorque(0);
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
        if (isFinished)
        {
            finishDistance = 0f;
            return;
        }

        //yol hesaplanacak kalan, finish le aralar�ndaki uzakl�k al�nabilir
        finishDistance = Vector3.Distance(motorBike.transform.position, GameManager.Instance.finishObject.position);
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
        _orderText.text = characterOrder.ToString();
    }
}
