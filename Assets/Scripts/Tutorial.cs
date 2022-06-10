using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class Tutorial : MonoBehaviour
{
    public NPCConversation myConversation;
    void Start()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }
    void Update()
    {
        
    }
}
