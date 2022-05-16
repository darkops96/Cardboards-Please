using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private float wheight;
    [SerializeField] private string boxSize;
    [SerializeField] private bool isEspecial;
    private bool scanned = false;

    public void Start()
    {
        scanned = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setScanned()
    {
        scanned = true;
    }

    public bool getScanned()
    {
        return scanned;
    }
}