using UnityEngine;
using System.Collections;
using Assets.Code.Interfaces;
using Assets.Code.Scripts;
using System.Text.RegularExpressions;

public class StateDisplay : MonoBehaviour
{
    private string movementStateText;
    private string fightingStateText;
    public PlayerController currentPlayer;

    void Update()
    {
        movementStateText = currentPlayer.getCurrentMovementState();
        fightingStateText = currentPlayer.getCurrentFightingState();
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        movementStateText = movementStateText.Substring(movementStateText.LastIndexOf(@".") + 1);
        fightingStateText = fightingStateText.Substring(fightingStateText.LastIndexOf(@".") + 1);

        GUIStyle style = new GUIStyle();

        Rect topRect = new Rect(0, 0, w, h * 2 / 100);
        Rect bottomRect = new Rect(0, 25, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperRight;
        style.fontSize = h * 2 / 75;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

        GUI.Label(topRect, movementStateText, style);
        GUI.Label(bottomRect, fightingStateText, style);
    }
}