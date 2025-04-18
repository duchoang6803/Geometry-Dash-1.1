using UnityEngine;

public class PlayerBallMovement : PlayerAgent
{

    [SerializeField]
    private float rotateVelocity;
    [SerializeField]
    private SpriteRenderer ballSprite;

    private RaycastHit2D hitGround;

    private void Start()
    {
        JumpGravity = (int)CurrentGravity;
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        BallMovement();
        BallSwitchGravity();
        CheckJumpPointBehavior();
        CheckObstacle(this.gameObject,0.8f,playerData.whatIsObstacle);
        BallRotate();
    }

    private void BallMovement()
    {
        _rb.velocity = new Vector2(speedValues[(int)CurrentSpeed], _rb.velocity.y);
        var afterImage = PlayerAfterImagePool.Instance.GetFormPool();
        afterImage.SetUp(ballSprite.sprite, ballSprite.transform.position, ballSprite.transform.rotation);
    }

    private void BallRotate()
    {
        this.transform.Rotate(JumpGravity == 1 ? Vector3.back * rotateVelocity : Vector3.forward * rotateVelocity);
    }

    private void BallSwitchGravity()
    {
        if (OnGround(this.gameObject,hitGround))
        {
            if (Input.GetMouseButtonDown(0))
            {
                JumpGravity *= -1;
                _rb.gravityScale = 16 * JumpGravity;
            }
        }
    }

    private void CheckJumpPointBehavior()
    {
        if (IsNearGroundJumpPoint(this.gameObject) || (IsNearAirJumpPoint(this.gameObject) && Input.GetMouseButtonDown(0)))
        {
            _rb.velocity = JumpGravity == 1 ? new Vector2(_rb.velocity.x, playerData.allObjectJumpForce) : new Vector2(_rb.velocity.x, -playerData.allObjectJumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 0.8f);
    }
}
