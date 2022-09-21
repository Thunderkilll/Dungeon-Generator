using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    private float deceleration = 5f;
    private Vector3 moveDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        RandomDirection();
    }

    private void RandomDirection()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        PieceMovement();
    }

    private void PieceMovement()
    {
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
    }
}
