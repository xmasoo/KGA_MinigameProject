using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : PlatformBase
{
    public float boostJumpForce = 20f;

    public override void OnPlayerLanding()
    {
        Rigidbody2D playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, boostJumpForce);
        }
    }
}
