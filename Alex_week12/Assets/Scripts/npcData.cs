using UnityEngine;

[CreateAssetMenu(menuName = "NPC Data", fileName = "NewNPCData")]
public class npcData : ScriptableObject
{
    public string characterName;
    public string characterCatchphrase;
    public Sprite characterSprite;

    public int characterIQ;
    public float moveSpeed;

    public bool isSmelly;
}
