using System.Collections;
using UnityEngine;

public class BreakPlatform : PlatformBase
{
    [SerializeField] GameObject breakAnimation;

    public override void OnPlayerLanding()
    {
        gameObject.SetActive(false);
        Instantiate(breakAnimation, transform.position, Quaternion.identity);
        Destroy(breakAnimation, 5);
    }

}
