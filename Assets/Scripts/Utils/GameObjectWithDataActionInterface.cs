using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace HoloLens4Labs.Scripts.Utils
{
    [System.Serializable]
    public class GameObjectWithDataActionInterface<T> : UnityEvent<GameObject, T>
    {
    }
}