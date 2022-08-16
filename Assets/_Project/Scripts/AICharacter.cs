using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : Character
{
    private float holdTimer;
    private float holdTime;
    private float waitTime;
    private float minHoldTime = 0.1f;
    private float maxHoldTime = 5f;
    private float minWaitTime = 0.1f;
    private float maxWaitTime = 5f;

    private bool onHold;
    private bool onWait;

    private bool gameIsStart;

    private void Start()
    {
        StartCoroutine(Hold());
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsStart) return;

        if (IsTouchGround())
        {
            SetWheelTorque();
        }
        else
        {
            SetAngularVelocity();
            SetWheelTorque(0f);
        }

        SetCharacterOrderText();
    }

    protected override void StartGame()
    {
        base.StartGame();

        gameIsStart = true;
    }

    private IEnumerator Hold()
    {
        onHold = true;
        holdTime = Random.Range(minHoldTime, maxHoldTime);

        while (holdTimer<holdTime && !IsTouchGround())
        {
            holdTimer += Time.deltaTime;
            RotateMotorbike();
            yield return null;
        }
        holdTimer = 0f;

        onHold = false;
        onWait = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        yield return new WaitForSeconds(waitTime);
        onWait = false;
        StartCoroutine(Hold());
    }
}
