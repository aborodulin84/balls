using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    List<GameObject> Collor = new List<GameObject>();
    List<GameObject> Applicants = new List<GameObject>();
    List<Vector3> Elem = new List<Vector3>();
    //функция поиска соседей
    void GetNeighbor(GameObject O)
    {
        Vector3 P = O.transform.position;
        Vector3[] N = new Vector3[4];
        N[0] = P; N[0].x++; Elem.Add(N[0]);
        N[1] = P; N[1].x--; Elem.Add(N[1]);
        N[2] = P; N[2].y++; Elem.Add(N[2]);
        N[3] = P; N[3].y--; Elem.Add(N[3]);
        O.tag = "sellected";
        Collor.Remove(O);
    }
    #region Поиск всех соседей
    void Add (List<Vector3> S) //Ищет соседей выбранного цвета.
    {
        for (int i = 0; i < Collor.Count; i++)
        {
            Vector3 CoordPoint = Collor[i].transform.position;
            for (int j = 0; j < S.Count; j++)
                if (S[j] == CoordPoint)
                    Applicants.Add(Collor[i]);
        }
        S.Clear();
    }
    void Prog(List<GameObject> C)//Ищет координаты соседей соседей
    {
        foreach (GameObject O in Applicants)
        {
            GetNeighbor(O);
        }
        Applicants.Clear();
    }
    #endregion

   

    private void OnMouseDown()
    {
        GameObject MainCamera = GameObject.Find("Main Camera");
        //за каждый ход снимаем жизнь
        MainCamera.GetComponent<RendBlock>().EditLifes(-1);
        //очищаем список объектов предыдущего цвета
        Collor.Clear();
        //кладем в этот список выбранный объект
        Applicants.Add(this.gameObject);
        //запоминаем цвет
        string collor=tag;
        Vector3 Point = transform.position;//берем координаты выбранной точки
        //ищем все объекты выбранного цвета, перегоняем их в список
        GameObject[] AllSellectedCollor = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject elem in AllSellectedCollor) Collor.Add(elem);
        //пока в списке есть эл-ты(соседи) проверяем соседей данного цвета для них и помечаем их тегом
        while (Applicants.Count != 0)
        {
            for (int i = 0; i < Applicants.Count; i++)
            {
                //ищем ближайших соседей, исключаем наш объект из поиска
                Prog(Applicants);
                //находим из соседей только выбранного цвета, очищаем список координат соседей
                Add(Elem);
            }
        }
        //ищем все помеченные объекты
        GameObject[] M = GameObject.FindGameObjectsWithTag("sellected");
        //если находим больше 2, вознаграждаем игрока очками и жизнями
        if (M.Length>2)
        { 
            MainCamera.GetComponent<RendBlock>().EditLifes(M.Length-1); }
        MainCamera.GetComponent<RendBlock>().AddScore(M.Length);
        foreach (GameObject elem in M)
        {
            //всем найденным объектам возвращаем первоначальный тег и новые координаты вверху нашей сетки.
            elem.tag = collor;
           elem.transform.position= new Vector3(elem.transform.position.x, Random.Range(9,20), -6);
        }

    }

    private void OnMouseUp()
    {
        
    }
}