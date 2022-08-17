using System.Collections;
using UnityEngine;

public class AICharacter : Character
{
    //Input variables
    private float holdTimer;
    private float holdTime;
    private float waitTime = 3f;
    private float minHoldTime = 0.1f;
    private float maxHoldTime = 5f;
    private float minWaitTime = 0.1f;
    private float maxWaitTime = 5f;

    private bool gameIsStart;

    private void Start()
    {
        float defaultTorque = motorTorque;
        motorTorque = Random.Range(defaultTorque - 200f, defaultTorque + 200f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsStart) return;

        if (!IsTouchGround())
        {
            SetAngularVelocity();
        }

        SetCharacterOrderText();
        SetWheelAnimationsSpeed();
        ClampVelocity();
    }

    protected override void StartGame()
    {
        base.StartGame();

        StartCoroutine(Hold());
        gameIsStart = true;
    }

    private IEnumerator Hold()
    {
        if (isFinished) yield break;


        yield return new WaitForSeconds(waitTime);

        waitTime = Random.Range(minWaitTime, maxWaitTime);

        holdTime = Random.Range(minHoldTime, maxHoldTime);

        while (holdTimer < holdTime && !IsTouchGround())
        {
            holdTimer += Time.deltaTime;
            RotateMotorbike();
            yield return null;
        }
        holdTimer = 0f;

        SetFlipCount(0, true);

        StartCoroutine(Hold());
    }
}
