using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ShootingArrow : MonoBehaviour
{
    public float Force;
    private bool _hitTarget;

    private void Start()
    {
        //transform.Rotate(new Vector3(0, 0, 90f), Space.Self);
        _hitTarget = false;
        Destroy(gameObject, 5f);
    }

    public void Rotate(int zModifer)
        => transform.Rotate(new Vector3(0, 0, 90f * zModifer), Space.Self);

    public void Shoot(Vector3 target)
        => GetComponent<Rigidbody2D>()
        .AddForce(CalculateFuckingShootingVector(target, transform.position) * Force, ForceMode2D.Impulse);
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && _hitTarget == false)
            collision.gameObject.GetComponent<Player>().Hurt();
        
        _hitTarget = true;
    }

    private Vector3 CalculateFuckingShootingVector(Vector3 target, Vector3 origin)
    {
        var fuckingVector = (target - origin) * Force;
        //fuckingVector.y *= 0.6f;
        fuckingVector.y += 1;
        return fuckingVector;
    }
}
