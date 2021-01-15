using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private MovementConfig movementConfig;
    private float speed => movementConfig.Speed;
    private float rotateSpeed => movementConfig.RotateSpeed;
    private float minAngleToTurn => movementConfig.MinAngleToTurn;
    private float maxAngleToTurn => movementConfig.MaxAngleToTurn;

    public float movementTargetAngle;
    public float currentAngle;


    private void OnEnable()
    {
        changedByPlayer = false;
        TurnToCenter();
        currentAngle = movementTargetAngle;
        StopAllCoroutines();
        StartCoroutine(StartEndlessChangesDirections());
    }

    private void FixedUpdate()
    {
        MoveInCurrentDirection();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        //GameState.FixedUpdated -= GameState_FixedUpdated;
    }


    private void GameState_FixedUpdated()
    {
        if (enabled == false) return;
        MoveInCurrentDirection();
    }

    private void MoveInCurrentDirection()
    {
        currentAngle = Mathf.LerpAngle(currentAngle, movementTargetAngle, rotateSpeed * Time.fixedDeltaTime);
        Vector2 dir = GetDirection();
        transform.position = ((Vector2)transform.position + dir * speed * Time.fixedDeltaTime);
    }

    private Vector2 GetDirection()
    {
        return Quaternion.Euler(Vector3.zero.WithZ(currentAngle)) * Vector3.up;
    }

    public void TurnToCenter()
    {
        movementTargetAngle = (Quaternion.LookRotation(Vector3.forward, Vector3.zero - transform.position)).eulerAngles.z;
    }


    bool changedByPlayer;
    public void ChangeDirection(float angle)
    {
        changedByPlayer = true;
        currentAngle = movementTargetAngle = angle;
    }

    private IEnumerator StartEndlessChangesDirections()
    {
        yield return new WaitForSeconds(.5f);
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 0.8f));
            ChangeDirectionToRandom();
            yield return new WaitForSeconds(Random.Range(1f,1.5f));
            if (changedByPlayer == false) TurnToCenter();
        }
    }

    private void ChangeDirectionToRandom()
    {
        float turnDirection = Random.value > 0.5 ? -1 : 1;
        movementTargetAngle = movementTargetAngle + Random.Range(minAngleToTurn, maxAngleToTurn) * turnDirection;
    }


}
