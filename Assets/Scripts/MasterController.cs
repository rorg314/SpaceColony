using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 
public enum GameSpeed {x1, x2, x4, x8 }

public class MasterController : MonoBehaviour {

    public static MasterController instance;

    // Current game speed
    public GameSpeed gameSpeed { get; protected set; }
    // Current FPT
    public int framesPerTick { get; protected set; }
    // Frame counter for updates
    public int frameCounter { get; protected set; }

    // Target FPS
    public int framerate = 60;


    // On Tick action
    public event Action onTick;

    public int FramesPerTick(GameSpeed gameSpeed) {
        switch (gameSpeed) {
            case GameSpeed.x1 :
                return 8;
            case GameSpeed.x2 :
                return 4;
            case GameSpeed.x4 :
                return 2;
            case GameSpeed.x8 :
                return 1;
            default:
                break;
        }
        return 1;
    }

    public void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one master controller!");
        }
        
        gameSpeed = GameSpeed.x1;
        framesPerTick = FramesPerTick(gameSpeed);
        frameCounter = 0;
        //onTick += debugTick;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Called every frame 
    public void Update() {

        frameCounter++;

        if(frameCounter >= framesPerTick) {
            Tick();
            frameCounter = 0;
        }

    }

    // Actual ingame logic tick invocation
    public void Tick() {

        onTick?.Invoke();

    }

    public void CycleSpeed() {

        switch (gameSpeed) {
            case GameSpeed.x1:
                gameSpeed = GameSpeed.x2;
                break;
            case GameSpeed.x2:
                gameSpeed = GameSpeed.x4;
                break;
            case GameSpeed.x4:
                gameSpeed = GameSpeed.x8;
                break;
            case GameSpeed.x8:
                gameSpeed = GameSpeed.x1;
                break;
        }
        
        framesPerTick = FramesPerTick(gameSpeed);
    }


    //public void debugTick() {

    //    Debug.Log("Tick -- Game Speed = " + gameSpeed);

    //}
}
