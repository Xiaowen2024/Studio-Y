using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    // Canvas objects
    public GameObject GameLoadScreen;
    public GameObject ChooseMode;
    public GameObject Learning;
    public GameObject Battle;
    public GameObject LessonComplete;
    public GameObject BattleVictory;

    // bools that matter
    public bool thumbsUp;
    public bool lessonCompleted;
    public bool battleCompleted;

    // buttons
    public GameObject ButtonLeft;
    public GameObject ButtonRight;
    
    //parent table gameobject that the buttons are on, only active in specific cases
    public GameObject ButtonTable;
    
    public GameObject[] tutorialPages;
    private int currentIndex = 0;
    
    void Start()
    {
        /*
        GameLoadScreen = GameObject.Find("GameLoadScreen");
        ChooseMode = GameObject.Find("CanvasChooseMode");
        Learning = GameObject.Find("CanvasLearningMode");
        Battle = GameObject.Find("CanvasBattleMode");
        LessonComplete = GameObject.Find("CanvasLessonComplete");
        BattleVictory = GameObject.Find("CanvasBattleVictory");
        */

        thumbsUp = false;
        lessonCompleted = false;
        battleCompleted = false;

        //ButtonTable = GameObject.Find("ButtonTable");
        //ButtonLeft = ButtonTable.transform.Find("BigButton1").gameObject;
        //ButtonRight = ButtonTable.transform.Find("BigButton2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: if thumb up is detected, disable this gameObject and enable ChooseMode.
        if (thumbsUp && GameLoadScreen.activeSelf)
        {
            GameLoadScreen.SetActive(false);
            if (!ChooseMode.activeSelf) ChooseMode.SetActive(true);
        }

        if (ChooseMode.activeSelf || Learning.activeSelf || Battle.activeSelf)
        {
            ButtonTable.SetActive(true);
        }
        
        else if (GameLoadScreen.activeSelf || LessonComplete.activeSelf || BattleVictory.activeSelf)
        {
            ButtonTable.SetActive(false);
        }

        if (battleCompleted)
        {
            //disable all other canvas
            GameLoadScreen.SetActive(false);
            ChooseMode.SetActive(false);
            Learning.SetActive(false);
            Battle.SetActive(false);
            LessonComplete.SetActive(false);
            //enable battelVictory
            BattleVictory.SetActive(true);
            battleCompleted = false;
        }

        if (lessonCompleted)
        {
            GameLoadScreen.SetActive(false);
            ChooseMode.SetActive(false);
            Learning.SetActive(false);
            Battle.SetActive(false);
            BattleVictory.SetActive(false);
            //enable battelVictory
            LessonComplete.SetActive(true);
            lessonCompleted = false;
        }
    }

    public void ShowLessonCompleted()
    {
        
    }

    public void ButtonLeftPress()
    {
        if (ChooseMode.activeSelf)
        {
            //button left = choose learning
            ChooseMode.SetActive(false);
            Learning.SetActive(true);
            //button right = choose battle
        }
        
        else if (Learning.activeSelf)
        {
            //button left when on Lesson Roadmap = back to chooseMode Canvas
            Learning.SetActive(false);
            ChooseMode.SetActive(true);
            //button right = begin learning screen
        }
        
        else if (Battle.activeSelf)
        {
            //button left = back to ChooseMode if currentindex is 0
            if (currentIndex == 0)
            {
                Battle.SetActive(false);
                ChooseMode.SetActive(true);
            }
            else
            {
                tutorialPages[currentIndex].SetActive(false);
                tutorialPages[currentIndex - 1].SetActive(true);
                currentIndex--;
            }
            // else 
        }
    }

    public void ButtonRightPress()
    {
        if (ChooseMode.activeSelf)
        {
            //button right = choose battle
            ChooseMode.SetActive(false);
            Battle.SetActive(true);
        }
        
        else if (Learning.activeSelf)
        {
            //button right = begin learning screen
            Learning.SetActive(false);
            //TODO: begin learning
        }
        
        else if (Battle.activeSelf)
        {
            //
            if (currentIndex == 2)
            {
                Battle.SetActive(false);
                //TODO: begin game
            }
            else
            {
                //button right = next 
                currentIndex++;
                tutorialPages[currentIndex - 1].SetActive(false);
                tutorialPages[currentIndex].SetActive(true);
            }
            
        }
    }

}
