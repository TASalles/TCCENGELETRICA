using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Guidao : MonoBehaviour {

    SerialPort sp;
    //public float Valor;
    private string Arduino;

    public float Rotacao;

	// Use this for initialization
	void Start ()
    {
        Rotacao = 0;
        sp = new SerialPort("COM7", 9600);
        sp.Open();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, Rotacao, 0);
        Arduino = sp.ReadLine();
        Rotacao = float.Parse(Arduino);
		
	}
}
