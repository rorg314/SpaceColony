using System;
using UnityEngine;

public enum GameSpeed { x1, x2, x4, x8 }

public class MasterController : MonoBehaviour {
    public static MasterController instance;

    // Current game speed
    public GameSpeed gameSpeed { get; protected set; }

    // Epoch time in seconds since Awake()
    public float time { get; protected set; }

    // Base TPS
    public int baseTPS = 8;

    // Epoch time that last tick occured
    public float lastTick;

    // Current seconds per tick
    public float SPT;

    // On Tick action
    public event Action onTick;

    // On speed changed action
    public event Action onSpeedChanged;

    public bool debugTick = true;

    public int getGameSpeedInt() {
        switch (gameSpeed) {
            case GameSpeed.x1:
                return 1;

            case GameSpeed.x2:
                return 2;

            case GameSpeed.x4:
                return 4;

            case GameSpeed.x8:
                return 8;

            default:
                break;
        }
        return 8;
    }

    // Ticks per second
    public int getTPS() {
        switch (gameSpeed) {
            case GameSpeed.x1:
                return baseTPS;

            case GameSpeed.x2:
                return baseTPS * getGameSpeedInt();

            case GameSpeed.x4:
                return baseTPS * getGameSpeedInt();

            case GameSpeed.x8:
                return baseTPS * getGameSpeedInt();

            default:
                break;
        }
        return 8;
    }

    // Seconds per tick
    public float getSPT() {
        switch (gameSpeed) {
            case GameSpeed.x1:
                return (float)1 / baseTPS;

            case GameSpeed.x2:
                return (float)1 / (baseTPS * getGameSpeedInt());

            case GameSpeed.x4:
                return (float)1 / (baseTPS * getGameSpeedInt());

            case GameSpeed.x8:
                return (float)1 / (baseTPS * getGameSpeedInt());

            default:
                break;
        }
        return 8;
    }

    // Convert a realtime second interval to number of ticks
    public int GetTicksInRealtimeInterval(float seconds) {
        float rawTicks = seconds / SPT;
        // Round rawTicks to integer number of ticks (might want to adjust this)
        return Mathf.RoundToInt(rawTicks);
    }

    public void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Trying to create more than one master controller!");
        }

        gameSpeed = GameSpeed.x1;
        SPT = getSPT();
        time = 0f;
        lastTick = 0f;

        debugTick = false;

        // Initial tick

        //framesPerTick = FramesPerTick(gameSpeed);
        //frameCounter = 0;
        //onTick += debugTick;

        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
    }

    // Called every frame
    public void Update() {
        // Increase epoch time in seconds
        time += Time.deltaTime;

        if (debugTick == false) {
            if (time - lastTick >= SPT) {
                Tick();
            }
        }
    }

    // Actual ingame logic tick invocation
    public void Tick() {
        lastTick = time;
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
        Debug.Log(gameSpeed);
        SPT = getSPT();

        onSpeedChanged?.Invoke();
    }

    public void DebugTick() {
        //Debug.Log("Tick -- Game Speed = " + gameSpeed);
        Tick();
    }
}