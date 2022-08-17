using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{
    private float holdTimer;
    private float holdTime;
    private float waitTime = 3f;
    private float minHoldTime = 0.1f;
    private float maxHoldTime = 5f;
    private float minWaitTime = 0.1f;
    private float maxWaitTime = 5f;

    private bool onHold;
    private bool onWait;

    private bool gameIsStart;

    private float minMotorTorque = 2800f;
    private float maxMotorTorque = 3100f;

    private void Start()
    {

        motorTorque = Random.Range(minMotorTorque, maxMotorTorque);
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
    }

    protected override void StartGame()
    {
        base.StartGame();

        StartCoroutine(Hold());
        gameIsStart = true;
    }

    private IEnumerator Hold()
    {
        onWait = true;

        yield return new WaitForSeconds(waitTime);

        waitTime = Random.Range(minWaitTime, maxWaitTime);
        onWait = false;

        onHold = true;
        holdTime = Random.Range(minHoldTime, maxHoldTime);

        while (holdTimer < holdTime && !IsTouchGround())
        {
            holdTimer += Time.deltaTime;
            RotateMotorbike();
            yield return null;
        }
        holdTimer = 0f;

        SetFlipCount(0, true);
        onHold = false;

        StartCoroutine(Hold());
    }
}
