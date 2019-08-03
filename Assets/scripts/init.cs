using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class init : MonoBehaviour
{
    string counter;
    string counter1;

    GUIStyle style = new GUIStyle();

    private void Start()
    {
        style.normal.textColor = Color.white;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;

        // Получаем отсортированную таблицу лидеров
        DataTable scoreboard = DBmanager.GetTable("SELECT * FROM Scores ORDER BY score DESC;");
        // Получаем id лучшего игрока
        int idBestPlayer = int.Parse(scoreboard.Rows[0][1].ToString());
        // Получаем ник лучшего игрока
        string nickname = DBmanager.ExecuteQueryWithAnswer($"SELECT nickname FROM Player WHERE id_player = {idBestPlayer};");
        Debug.Log($"Лучший игрок {nickname} набрал {scoreboard.Rows[0][2].ToString()} очков.");

        counter = $"{scoreboard.Rows[0][2].ToString()}";
        counter1 = DBmanager.ExecuteQueryWithAnswer($"SELECT healh FROM Player WHERE id_player = 1");
    }


    void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 100, 34), counter, style);
        GUI.Label(new Rect(200, 100, 100, 34),"healh:"+ counter1, style);
    }

}