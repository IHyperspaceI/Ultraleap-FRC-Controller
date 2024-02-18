using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


        // Button events
        enable.onClick.AddListener(ToggleEnabled);

        // Manage enable/disable
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //enabled = true;
            //ToggleEnabled();
        }

        // Send the data
        if (enabled)
        {
            string message = "Position: " + manager.getPosition() + ", Rotation: " + manager.getRotation() + ", Pose: " + manager.getPose() + "\n";
            comms.SendMessageData(message);
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
}
