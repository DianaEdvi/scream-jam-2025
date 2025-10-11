using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TickManager : MonoBehaviour
{
    [SerializeField] private int ticks = 0;

    private bool nextTick = true;

    private void FixedUpdate()
    {
        if (nextTick) {

            Events.onTick?.Invoke();
            ticks++;
            nextTick = false;
            StartCoroutine(tickBreak());

        }
    }

    private IEnumerator tickBreak() {

        yield return new WaitForSeconds(1.1f);
        nextTick = true;

    }
}
