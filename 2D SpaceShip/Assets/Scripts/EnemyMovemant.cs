using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMovemant : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Transform _player;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 dir = _player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion r = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, r, _rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //move the enemy (push the rigidbody)
        _rb.AddRelativeForce(new Vector3(_moveSpeed * Time.fixedDeltaTime, 0f, 0f));
    }
}
