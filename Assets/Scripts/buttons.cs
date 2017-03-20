using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour {
    private static Color Bcolor= new Vector4(53, 227, 10, 255);
    private void OnMouseDown()
    {
        //изменим цвет при нажатии.
        GetComponent<Renderer>().material.color= new Vector4(100, 10, 255, 255);
    }

    private void OnMouseUp()
    {
        GetComponent<Renderer>().material.color = Bcolor;
    }

    private void OnMouseUpAsButton()
    {
        //действия при нажатии кнопок, сюда буду складывать все кнопки.
        switch(gameObject.name)
        {
            case "play":
                SceneManager.LoadScene("Game");
                break;
            case "records":
                SceneManager.LoadScene("Records");
                break;
            case "info":
                SceneManager.LoadScene("Info");
                break;
            case "Home":
                GameObject MainCamera = GameObject.Find("Main Camera");
               MainCamera.GetComponent<RendBlock>().Back.SetActive(true);
                break;
            case "OK":
                SceneManager.LoadScene("MainMenu");
                break;
            case "facebook":
                Application.OpenURL("http://facebook.com");
                break;
            case "Leave":
                SceneManager.LoadScene("MainMenu");
                break;
            case "Stay":
                GameObject Camera = GameObject.Find("Main Camera");
                Camera.GetComponent<RendBlock>().Back.SetActive(false);
                break;
            case "exit":
                Application.Quit();
                break;
        }
    }
}
