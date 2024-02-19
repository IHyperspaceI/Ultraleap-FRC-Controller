using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DebugDisplay : MonoBehaviour
{
    public HandManager manager;
    public BaseWebsocket comms;

    public TextMeshProUGUI outputLabel;
    public TextMeshProUGUI statusLabel;

    public Button enable;

    private bool enabled;

    private void Start()
    {
        enabled = false;
        ToggleEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        // Show data
        outputLabel.text =
            "Output: \nPosition: " + manager.getPosition() +
            "\nRotation: " + manager.getRotation() +
            "\nPose: " + manager.getPose();


        statusLabel.text = comms.GetStatus();


        // Send the data
        if (enabled)
        {
            Data handData = new Data();
            handData.xPos = manager.getPosition().x;
            handData.yPos = manager.getPosition().y;
            handData.zPos = manager.getPosition().z;

            handData.pitch = manager.getRotation().z;
            handData.roll = manager.getRotation().x;
            handData.yaw = manager.getRotation().y;

            handData.pose = manager.getPose();

            string json = JsonUtility.ToJson(handData);

            comms.SendMessageData(json + "\n");
        }
    }

    private void ToggleEnabled()
    {
        enabled = !enabled;

        if (enabled == true)
        {
            enable.GetComponent<Image>().color = new Color(210, 50, 50, 255);
            enable.GetComponentInChildren<TextMeshProUGUI>().text = "Enabled!";
        } else
        {
            enable.GetComponent<Image>().color = new Color(50, 210, 50, 255);
            enable.GetComponentInChildren<TextMeshProUGUI>().text = "Disabled";
        }
    }

    [Serializable]
    private class Data
    {
        public string pose;
        public double xPos; // Sideways
        public double yPos; // Height
        public double zPos; // Forward/back

        public double pitch;
        public double roll;
        public double yaw;
    }
}
