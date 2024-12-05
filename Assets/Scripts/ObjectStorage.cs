using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStorage : MonoBehaviour
{
    [Header("EasyBookColorAnomaly")]
    public GameObject[] bookCollection = new GameObject[2];

    [Header("EasyBusHandleAnomaly")]
    public GameObject[] busHandles = new GameObject[4];

    [Header("EasyCanvasChangeAnomaly")]
    public GameObject normalCanvas;
    public GameObject anomalCanvas;

    [Header("EasyCanvasDisappearAnomaly")]
    public GameObject giraffeCanvas;

    [Header("EasyCanvasFlipAnomaly")]
    public GameObject normalCanvasFlip;

    [Header("EasyCubeAnomaly")]
    public GameObject normalCube;
    public GameObject anomalyCube;

    [Header("EasyDiceAnomaly")]
    public GameObject normalDice;
    public GameObject anomalyDice;

    [Header("EasyDigitalClockAnomaly")]
    public GameObject digitalClockText;

    [Header("EasyDrawerMissingContentAnomaly")]
    public GameObject drawerMissingContent;

    [Header("EasyDresserBackOpenAnomaly")]
    public GameObject dresser;
    public GameObject backOpenedDresser;

    [Header("EasyHangerDisappearAnomaly")]
    public GameObject[] hangers = new GameObject[4];

    [Header("EasyLaptopAnomaly")]
    public GameObject laptopObject;

    [Header("EasyLightAnomaly")]
    public GameObject anomalyLight;

    [Header("EasyPianoAnomaly")]
    public GameObject pianoObject;

    [Header("EasyPlayerAnomaly")]
    public GameObject playerAwake;
    public GameObject playerSleeping;

    [Header("EasySofaDisappearAnomaly")]
    public GameObject sofa;

    [Header("EasySpintopAnomaly")]
    public GameObject spintopObject;

    [Header("EasyTeddyBearAnomaly")]
    public GameObject normalTeddyBear;
    public GameObject anomalyTeddyBear;

    [Header("HardChessAnomaly")]
    public GameObject chessHitbox;
    public GameObject chessWalls;

    [Header("HardFruitDropAnomaly")]
    public GameObject bed;

    [Header("HardTimeBombAnomaly")]
    public GameObject digitalClock;
    public GameObject timeBomb;


}
