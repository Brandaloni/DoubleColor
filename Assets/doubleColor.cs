﻿using KMHelper;
using Random = UnityEngine.Random;
using UnityEngine;
using System.Collections;

public class doubleColor : MonoBehaviour {

    private static int _moduleIdCounter = 1;
    public KMAudio newAudio;
    public KMBombModule module;
    public KMBombInfo info;
    private int _moduleId = 0;
    private bool _isSolved = false, _lightsOn = false;

    public KMSelectable submit;

    public MeshRenderer light1, light2, light3, screen, stage1, stage2;

    private bool danger = false;

    private int stageNumber = 1;

    private int screenColor; //0=green, 1=blue, 2=red, 3=pink, 4=yellow

    private int correctDidget;

    public Material screenOff, screenOn, screenRed, screenBlue, screenPink, screenGreen, screenYellow;

    void Start()
    {
        _moduleId = _moduleIdCounter++;
        module.OnActivate += Activate;
    }

    void Activate()
    {
        Init();
        _lightsOn = true;
    }

    private void Awake()
    {
        submit.OnInteract += delegate
        {
            handleSubmit();
            return false;
        };
    }

    void handleSubmit()
    {
        newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, submit.transform);
        if (!_lightsOn || _isSolved) return;

        Debug.LogFormat("[Double Color #{0}] Submit button pressed", _moduleId);
        string time = info.GetFormattedTime();
        if (danger)
        {
            module.HandleStrike();
            Debug.LogFormat("[Double Color #{0}] Strike! Submit button was pressed when all three lights were on!", _moduleId);

        }
        else if (time.Contains(correctDidget.ToString()))
        {
            if (stageNumber == 2) { 
                module.HandlePass();
                Debug.LogFormat("[Double Color #{0}] Module passed!", _moduleId);
                stage2.material = screenOn;
                newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, submit.transform);
                _isSolved = true;
            } else
            {
                Debug.LogFormat("[Double Color #{0}] Stage 1 complete!", _moduleId);
                newAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, submit.transform);
                StartCoroutine(passStageOne());
            }
        } else
        {
            module.HandleStrike();
            Debug.LogFormat("[Double Color #{0}] Strike! Submit button at wrong time! There wasn't a {1} in the bomb timer!", _moduleId, correctDidget);
        }

    }

    void Init()
    {
        MainScreenSetup();
        StartCoroutine(Lights());
        getCorrectDidget();
    }

    void getCorrectDidget()
    {
        if (stageNumber == 1)
        {
            switch (info.GetBatteryCount())
            {
                case 0:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 1;
                            break;
                        case 1:
                            correctDidget = 0;
                            break;
                        case 2:
                            correctDidget = 9;
                            break;
                        case 3:
                            correctDidget = 8;
                            break;
                        case 4:
                            correctDidget = 7;
                            break;
                    }
                    break;
                case 1:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 2;
                            break;
                        case 1:
                            correctDidget = 7;
                            break;
                        case 2:
                            correctDidget = 6;
                            break;
                        case 3:
                            correctDidget = 5;
                            break;
                        case 4:
                            correctDidget = 6;
                            break;
                    }
                    break;
                case 2:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 3;
                            break;
                        case 1:
                            correctDidget = 8;
                            break;
                        case 2:
                            correctDidget = 1;
                            break;
                        case 3:
                            correctDidget = 4;
                            break;
                        case 4:
                            correctDidget = 5;
                            break;
                    }
                    break;
                case 3:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 4;
                            break;
                        case 1:
                            correctDidget = 9;
                            break;
                        case 2:
                            correctDidget = 2;
                            break;
                        case 3:
                            correctDidget = 3;
                            break;
                        case 4:
                            correctDidget = 4;
                            break;
                    }
                    break;
                case 4:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 5;
                            break;
                        case 1:
                            correctDidget = 0;
                            break;
                        case 2:
                            correctDidget = 1;
                            break;
                        case 3:
                            correctDidget = 2;
                            break;
                        case 4:
                            correctDidget = 3;
                            break;
                    }
                    break;
                default:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 6;
                            break;
                        case 1:
                            correctDidget = 7;
                            break;
                        case 2:
                            correctDidget = 8;
                            break;
                        case 3:
                            correctDidget = 9;
                            break;
                        case 4:
                            correctDidget = 0;
                            break;
                    }
                    break;

            }
        }
        else
        {
            switch (info.GetBatteryCount())
            {
                case 0:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 0;
                            break;
                        case 1:
                            correctDidget = 2;
                            break;
                        case 2:
                            correctDidget = 6;
                            break;
                        case 3:
                            correctDidget = 8;
                            break;
                        case 4:
                            correctDidget = 5;
                            break;
                    }
                    break;
                case 1:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 4;
                            break;
                        case 1:
                            correctDidget = 9;
                            break;
                        case 2:
                            correctDidget = 9;
                            break;
                        case 3:
                            correctDidget = 0;
                            break;
                        case 4:
                            correctDidget = 2;
                            break;
                    }
                    break;
                case 2:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 1;
                            break;
                        case 1:
                            correctDidget = 7;
                            break;
                        case 2:
                            correctDidget = 5;
                            break;
                        case 3:
                            correctDidget = 9;
                            break;
                        case 4:
                            correctDidget = 6;
                            break;
                    }
                    break;
                case 3:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 4;
                            break;
                        case 1:
                            correctDidget = 2;
                            break;
                        case 2:
                            correctDidget = 0;
                            break;
                        case 3:
                            correctDidget = 8;
                            break;
                        case 4:
                            correctDidget = 3;
                            break;
                    }
                    break;
                case 4:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 6;
                            break;
                        case 1:
                            correctDidget = 8;
                            break;
                        case 2:
                            correctDidget = 4;
                            break;
                        case 3:
                            correctDidget = 7;
                            break;
                        case 4:
                            correctDidget = 1;
                            break;
                    }
                    break;
                default:
                    switch (screenColor)
                    {
                        case 0:
                            correctDidget = 1;
                            break;
                        case 1:
                            correctDidget = 3;
                            break;
                        case 2:
                            correctDidget = 7;
                            break;
                        case 3:
                            correctDidget = 3;
                            break;
                        case 4:
                            correctDidget = 5;
                            break;
                    }
                    break;
            }
        }
        Debug.LogFormat("[Double Color #{0}] Correct didget for Stage {1}: {2}", _moduleId, stageNumber, correctDidget);
    }

    void MainScreenSetup()
    {
        screenColor = Random.Range(0, 5);
        switch (screenColor)
        {
            case 0:
                screen.material = screenGreen;
                Debug.LogFormat("[Double Color #{0}] Screen Color for Stage {1}: Green", _moduleId, stageNumber);
                break;
            case 1:
                screen.material = screenBlue;
                Debug.LogFormat("[Double Color #{0}] Screen Color for Stage {1}: Blue", _moduleId, stageNumber);
                break;
            case 2:
                screen.material = screenRed;
                Debug.LogFormat("[Double Color #{0}] Screen Color for Stage {1}: Red", _moduleId, stageNumber);
                break;
            case 3:
                screen.material = screenPink;
                Debug.LogFormat("[Double Color #{0}] Screen Color for Stage {1}: Pink", _moduleId, stageNumber);
                break;
            case 4:
                screen.material = screenYellow;
                Debug.LogFormat("[Double Color #{0}] Screen Color for Stage {1}: Yellow", _moduleId, stageNumber);
                break;

        }
    }

    private IEnumerator Lights()
    {
        while (!_isSolved)
        {
            danger = false;
            light1.material = screenOff;
            light2.material = screenOff;
            light3.material = screenOff;
            yield return new WaitForSeconds(2.4f);
            light1.material = screenRed;
            yield return new WaitForSeconds(0.5f);
            light2.material = screenRed;
            yield return new WaitForSeconds(0.5f);
            light3.material = screenRed;
            danger = true;
            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator passStageOne()
    {
        yield return new WaitForSeconds(2.0f);
        stage1.material = screenOn;
        stageNumber = 2;
        MainScreenSetup();
        getCorrectDidget();
    }
}
