using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Robot : MonoBehaviour, IColored
{
    public NPCConversation myConversation;
    //public NPCConversation myLastWords;
    [SerializeField] private Material _robotColor;
    [SerializeField] private Material _robotGrey;
    [SerializeField] public LightManager.MyLight _defaultColor;
   // [SerializeField] private float _endDistance;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = _robotGrey;
    }

    private void Update()
    {
        Died();
    }

    public void Color(LightManager.MyLight col)
    {
        if (gameObject.GetComponent<MeshRenderer>().sharedMaterial == _robotColor) return;
        if (_defaultColor == col) gameObject.GetComponent<MeshRenderer>().material = _robotColor;
        ConversationManager.Instance.StartConversation(myConversation);
       // GameManager.instance.WonRobot(col);
        
    }

    public void Died()
    {
        // if (transform.position.y >= _endDistance)
        // {
        //    ConversationManager.Instance.StartConversation(myLastWords);
        // }
    }
    //
    // public void Death()
    // {
    //     ConversationManager.Instance.Invoke(OnDeath());
    // }
    
    //-17.12827 yellow
}
