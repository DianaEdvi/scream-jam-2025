using UnityEngine;
using System;

public class Events : MonoBehaviour
{

    public static Action<GameObject> depositEvidence;
    public static Action onTick;
    public static Action<GameObject> OnInteract;
    public static Action OnMothmanIsNear;
    public static Action OnMothmanIsFar;
    public static Action OnGameOver;

}
