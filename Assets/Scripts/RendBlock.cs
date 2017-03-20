using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RendBlock : MonoBehaviour {

    #region переменные
    public GameObject[] Circle;
    private int Lifes;
    public Text lifes;
    private int Score;
    public Text score;
    public GameObject Panel;
    public GameObject Back;
    #endregion
    #region функции
    //создаем сетку из объектов
    private void CreateGred()
    {
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -5; j <=8; j++)
                Instantiate(Circle[Random.Range(0, Circle.Length)], new Vector3(i, j, -6), Quaternion.identity);
        }
    }
    //функция получает список всех шариков
    List<GameObject> AllCircles()
    {
        List<GameObject> AllCircles = new List<GameObject>();
        string[] tags = { "Red", "orange", "Yellow", "Green", "Blue", "DarkBlue", "Violet" };
        foreach (string t in tags)
        {
            GameObject[] M = GameObject.FindGameObjectsWithTag(t);
            foreach (GameObject g in M)
            {
                AllCircles.Add(g);
            }
        }
        return AllCircles;
    }
    //функция получает список координат всех шариков
    List<Vector3> AllCoord(List<GameObject>O)
    {
        List<Vector3> AllCoord = new List<Vector3>();
        foreach (GameObject circle in O)
        {
            AllCoord.Add(circle.transform.position);
        }
        return AllCoord;
    }
    //функция для псевдофизики шариков
    void GoDown(List<GameObject> A,List<Vector3>B) {
        Vector3 P=new Vector3();
        bool Void=false;
        for (int i = 0; i < A.Count; i++)
        {
            //Проверяем не в нижней ли строке шарик. Если в нижней берем следующий
            if (A[i].transform.position.y == -5) continue;
            //Для выбранного шарика создаем проверяемые координаты вниз по y
            else
            {
                P = A[i].transform.position;
                P.y = A[i].transform.position.y - 1;
            }
            for (int j = 0; j < B.Count; j++)
            {
                if (B[j] == P) { Void = false; break; }
                else Void = true;
            }
            if(Void==true)
            A[i].transform.position = P;
        }
}
    //функция запускается при проигрыше
    void PlayerLose()
    {
        if (PlayerPrefs.GetInt("Score") < Score)
        {
            PlayerPrefs.SetInt("Score", Score);
            SceneManager.LoadScene("Records");
        }
        else
        Panel.SetActive(true);
    }
    //функция для редактирования кол-ва жизней
   public void EditLifes(int a)
    {
        Lifes += a;
    }
    // Управление счетом
    public void AddScore(int x)
    {
        Score += x;
    }

    #endregion
    // Use this for initialization
    void Start () {
        //создаем сетку объектов
        CreateGred();
        //Назначаем кол-во жизней и выстовляем кол-во очков в ноль
        Lifes = 3;
        Score = 0;
        //выводим кол-во жизней на экран
        lifes.text = Lifes.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //проверяем есть ли пустые места в сетке и если есть устраняем их
        GoDown(AllCircles(), AllCoord(AllCircles()));
        lifes.text = Lifes.ToString();
        score.text = Score.ToString();
        //условие проигрыша
        if (Lifes == 0) PlayerLose();
    }
}
