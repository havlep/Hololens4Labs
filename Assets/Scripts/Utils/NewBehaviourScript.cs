using HoloLens4Labs.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField]
    private ScrollableListPopulator scrollableListPopulator = default;
    // Start is called before the first frame update
    void Start()
    {
        scrollableListPopulator.MakeScrollingList();

    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
