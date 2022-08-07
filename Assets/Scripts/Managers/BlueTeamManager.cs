using System.Collections.Generic;
using UnityEngine;

public class BlueTeamManager : MonoBehaviour
{
    [SerializeField] private List<Player> teamList;

    private void Start()
    {
        foreach (Player player in teamList)
        {
            if (player.PlayerData.ID == 1)
            {
                player.GetComponent<AgentInput>().isActivePlayer = true;
            }
            else
            {
                player.GetComponent<AgentInput>().isActivePlayer = false;
            }
        }
    }
}
