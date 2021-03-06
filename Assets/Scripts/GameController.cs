using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] List<GameObject> itemsList = new List<GameObject>();
    [SerializeField] XROrigin playerXR;
    [SerializeField] private GameObject box;
    private Vector3 firstPosition;
    Vector3 position;
    private Stopwatch _stopwatch;
    private Stopwatch _instanceStopWatch;
    private Stopwatch _lightsStopWatch;
    public int lightIncrementTime = 50;

    public float intensityIncrement = 0.1f;
    private float intensityVal = 1;

    private int points;
    //Countdown
    //float currentTime = 0f;
    // private int currentLevel = 0;

    //[SerializeField] float startingTime = 10f;
    [SerializeField] float timeBetweenInstance = 5f;
    [SerializeField] int levelDuration = 2;
    [SerializeField] GameObject spawnerFolder;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject spawnButton;
    [SerializeField] private GameObject spawnBoxButton;
    [SerializeField] private GameObject boxPosition;
    [SerializeField] private Light[] _lights;

    [SerializeField] private AudioSource _audioSourceCorrect;
    [SerializeField] private AudioSource _audioSourceSpawn;

    [SerializeField] private AudioClip[] clips;

    private float[] intensityValues;


    bool activate = false;


    // Start is called before the first frame update
    void Start()
    {
        intensityValues = new float[_lights.Length];
        for (int i = 0; i < _lights.Length; i++)
        {
            intensityValues[i] = _lights[i].intensity;
        }

        points = 0;
        _stopwatch = new Stopwatch();
        _instanceStopWatch = new Stopwatch();
        _lightsStopWatch = new Stopwatch();
        firstPosition = playerXR.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_lightsStopWatch);
        if (intensityVal < 1)
        {
            if (_lightsStopWatch.ElapsedMilliseconds > lightIncrementTime)
            {
                _lightsStopWatch.Restart();
                turnOnLights();
            }
        }

        if (activate)
        {
            //currentTime -= 1 * Time.deltaTime;
            Debug.Log("Current Time: " + _stopwatch.Elapsed);
            Debug.Log("Current instance Time: " + _instanceStopWatch.Elapsed);

            if (_stopwatch.Elapsed.TotalSeconds > levelDuration)
            {
                Debug.Log("Level Ended");
                activate = false;
                // currentLevel++;
                gameFinish();
            }
            else
            {
                if (_instanceStopWatch.Elapsed.Seconds > timeBetweenInstance)
                {
                    _instanceStopWatch.Restart();
                    spawnItem();
                }
            }
        }
    }

    public void spawnItem()
    {
        _audioSourceSpawn.Play();
        Debug.Log("Spawned Item");
        int randomId = Random.Range(0, itemsList.Count);
        GameObject obj = Instantiate(itemsList[randomId], gameObject.transform.position,
            gameObject.transform.rotation);
        obj.transform.SetParent(spawnerFolder.transform);
    }

    public void spawnBox()
    {
        Debug.Log("Spawned box");
        Instantiate(box, boxPosition.transform.position,
            gameObject.transform.rotation);
    }

    #region game flow

    public void startPlaying()
    {
        points = 0;
        _stopwatch.Restart();
        _instanceStopWatch.Restart();
        activate = true;
        hideButton(playButton);
        unHideButton(spawnButton);
        unHideButton(spawnBoxButton);
    }

    private void gameFinish()
    {
        _stopwatch.Stop();
        _instanceStopWatch.Stop();
        activate = false;
        unHideButton(playButton);
        hideButton(spawnButton);
        hideButton(spawnBoxButton);
    }

    public void goToMenu()
    {
        if (_stopwatch.IsRunning)
            _stopwatch.Stop();
        if (_instanceStopWatch.IsRunning)
            _instanceStopWatch.Stop();

        activate = false;
        hideButton(playButton);
        hideButton(spawnButton);
        hideButton(spawnBoxButton);
        blackFade();
    }

    #endregion


    public void turnOnLights()
    {
        intensityVal += intensityIncrement;
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].intensity = intensityVal * intensityValues[i];
        }
    }

    public void blackFade()
    {
        intensityVal = 0;
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].intensity = intensityVal * intensityValues[i];
        }

        _lightsStopWatch.Restart();
    }


    public string getTime()
    {
        TimeSpan ts = _stopwatch.Elapsed;
        return String.Format(String.Format("{0:00}:{1:00}.{2:00}",
            ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10));
    }


    private void hideButton(GameObject button)
    {
        MeshRenderer[] bu = button.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer rend in bu)
        {
            rend.enabled = false;
        }

        button.SetActive(false);
    }

    private void unHideButton(GameObject button)
    {
        button.SetActive(true);
        MeshRenderer[] mrC = button.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer rend in mrC)
        {
            rend.enabled = true;
        }
    }

    public int getPoints()
    {
        return points;
    }

    public void addPoint()
    {
        _audioSourceCorrect.clip = clips[0];
        _audioSourceCorrect.Play();
        points++;
    }

    public void takePoint()
    {
        _audioSourceCorrect.clip = clips[1];
        _audioSourceCorrect.Play();
        points--;
    }
}