using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    void Awake()
    {
        int firstCharacter = PlayerPrefs.GetInt("firstCharacter");
        int secondCharacter = PlayerPrefs.GetInt("secondCharacter");

        GameObject prefab1 = characterPrefabs[firstCharacter];
        GameObject clone1 = Instantiate(prefab1, spawnPoint1.position, Quaternion.identity);

        GameObject prefab2 = characterPrefabs[secondCharacter];
        GameObject clone2 = Instantiate(prefab2, spawnPoint2.position, Quaternion.identity);

        Ship player1 = clone1.GetComponent<Ship>();
        Ship player2 = clone2.GetComponent<Ship>();

        Test(player1, AssignedPlayer.Player1);
        Test(player2, AssignedPlayer.Player2);
    }

    void Test(Ship player, AssignedPlayer assignedPlayer)
    {
        player.assignedPlayer = assignedPlayer;
    }
}