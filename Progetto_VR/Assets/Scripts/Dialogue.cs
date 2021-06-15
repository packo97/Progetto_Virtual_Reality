using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextAsset[] dialogue_text_asset;
    private int indexDialogue;
    private int indexSentence;

    private List<(string, string)> dialogue;
    // Start is called before the first frame update
    void Start()
    {
        indexDialogue = 0;
        LoadDialogue(indexDialogue);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadDialogue(int indexDialogue)
    {
        indexSentence = 0;
        string text = dialogue_text_asset[indexDialogue].text;
        string[] lines = text.Split('\n');
      
        dialogue = new List<(string, string)>();
        foreach (string line in lines)
        {
            //Debug.Log(line);
            string[] split = line.Split('\t');
            (string, string) whoTalk_sentence = (split[0], split[1]);
            //Debug.Log(whoTalk_sentence.Item1 + ": " + whoTalk_sentence.Item2);
            dialogue.Add(whoTalk_sentence);
            
            if (split[0].Equals("End"))
                break;
        } 
    }

    public (string, string) GetCurrentSentence()
    {
        return dialogue[indexSentence];
    }

    public void UpdateCurrentIndex()
    {
        /*
        if (indexSentence < dialogue.Count - 1)
            indexSentence++;
        */
        indexSentence = (indexSentence + 1) % dialogue.Count;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("NextDialogue"))
        {
            if (indexDialogue < dialogue_text_asset.Length - 1)
            {
                indexDialogue++;
                LoadDialogue(indexDialogue);
            }
                
        }
    }
}
