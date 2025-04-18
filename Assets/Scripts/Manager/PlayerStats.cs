using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private PlayerAgent playerAgent;
    [SerializeField]
    private GameManager gameManager;

    private void Update()
    {
     
    }
    //public void Die()
    //{
    //    if (playerAgent.isHitObstacle == false) return;
    //    // Instantiate Particle Effect
    //    gameManager.respawnTime = Time.time;
    //    Destroy(gameObject);
    //    //Destroy(_particle.gameObject);
    //    playerAgent._rb.velocity = Vector2.zero;
    //    playerAgent._rb.gravityScale = 0f;
    //}
}
