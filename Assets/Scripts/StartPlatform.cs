using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : PlatformBase
{
    public override void OnPlayerLanding()
    {
        Destroy(gameObject,5f);
    }
}
