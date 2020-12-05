using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MobPatrol : MonoBehaviour
{
    private Vector3 targetPosition;

    [SerializeField] private List<Transform> _patrolPoints;
    private int _targetPoint;
    private Rigidbody2D _rigidbody2D;
    
    public float speed = 3;
    public bool lookRight = true;

    private Animator animator;

    void Start()
    {
        _targetPoint = 0;
        targetPosition = _patrolPoints[_targetPoint].position;
        TryGetComponent<Animator>(out animator);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (transform.position == targetPosition)
        {
            if (_targetPoint++ > _patrolPoints.Count)
                _targetPoint = 0;
            try
            {
                targetPosition = _patrolPoints[_targetPoint].position;
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Debug.Log("Ужс");
            }
        }

        if (targetPosition.x > transform.position.x && !lookRight)
            Flip();
        if (targetPosition.x < transform.position.x && lookRight)
            Flip();

        var p = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (animator != null)
            animator.SetFloat("speed", (transform.position - p).magnitude / Time.deltaTime);
    }

    public void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        lookRight = !lookRight;
    }
}
