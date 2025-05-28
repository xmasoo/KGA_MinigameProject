using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : PlatformBase
{
    public override void OnPlayerLanding()
    {
        StartCoroutine(BreakRoutine());
    }

    IEnumerator BreakRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        poolReference.ReturnPlatform(gameObject);
    }
}
