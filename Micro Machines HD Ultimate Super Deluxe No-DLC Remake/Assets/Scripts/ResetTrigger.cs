using Sjouke.Controls.Car;
using UnityEngine;

public class ResetTrigger : MonoBehaviour 
{
    public Transform ResetPos;

    public void OnCollisionEnter(Collision col)
    {
        Debug.Log("BOO!");
        var player = col.transform.GetComponent<PlayerCarController>();
        if (player == null) return;
        player.transform.position = ResetPos.position;
    }
}