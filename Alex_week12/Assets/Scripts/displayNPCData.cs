using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class displayNPCData : MonoBehaviour
{
    public GameObject statsCard;
    public TMP_Text _npcname, _catchphrase, _iq, _speed, _smelly;
    public Image artwork;

    public void displayCharacterCard(npcData stats)
    {
        statsCard.SetActive(true);
        _npcname.text = "Alright? I'm " + stats.characterName;
        _catchphrase.text = stats.characterCatchphrase;
        _iq.text = "I actually have a very high IQ. It's " + stats.characterIQ;
        _speed.text = "I'm also bloody rapid mate. My top running speed is " + stats.moveSpeed + "mph";
        artwork.sprite = stats.characterSprite;

        if (stats.isSmelly)
        {
            _smelly.text = "You can smell " + stats.characterName + " from about 5 miles away.";
        }
        else
        {
            _smelly.text = stats.characterName + " smells like a fresh summer's day. What a lovely smelling fella!";
        }
    }
}
