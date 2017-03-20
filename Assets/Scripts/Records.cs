using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Records : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //записываем в таблицу рекордов
        GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();
	}
	
}
