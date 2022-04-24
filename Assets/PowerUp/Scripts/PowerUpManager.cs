using UnityEngine;
using System.Collections.Generic;

public class PowerUpManager : Singleton<PowerUpManager>
{
    private Queue<CustomizablePowerUp> _powerUps;
    private Queue<CustomizablePowerUp> _powerUpsLogs;
    private ushort powerUpLogLimit = 3;

    private void Awake()
    {
        _powerUps = new Queue<CustomizablePowerUp>();
        _powerUpsLogs = new Queue<CustomizablePowerUp>();
    }

    public void Add(CustomizablePowerUp powerUp)
    {
        _powerUps.Enqueue(powerUp);
        _powerUpsLogs.Enqueue(powerUp);
        while (_powerUpsLogs.Count > powerUpLogLimit && _powerUpsLogs.Dequeue()) ;
    }

    private string RGBToHex(Color color)
    {
        return string.Format("#{0}{1}{2}",
                     ((int)(color.r * 255)).ToString("X2"),
                     ((int)(color.g * 255)).ToString("X2"),
                     ((int)(color.b * 255)).ToString("X2"));
    }

    private void OnGUI()
    {
        foreach (CustomizablePowerUp pu in _powerUpsLogs)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("You picked up <color=" + RGBToHex(pu.lightColor) + ">" + pu.powerUpName + "</color>");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        GUI.Label(new Rect(Screen.width - 180, 0, 180, 20), "PowerUp count: <color=yellow>" + this._powerUps.Count + "</color>");
    }
}

