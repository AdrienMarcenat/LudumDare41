using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour {

    [SerializeField] private SpriteRenderer m_letterSprite;
	private char m_character;
	public char character
	{
		get
		{
			return m_character;
        }
        set
        {
            m_character = value;
            //UpdateChar();
        }
    }

    private void Start()
    {
        int charIndex = LetterInventory.ms_AllLetters.IndexOf (character);
        m_letterSprite.sprite = RessourceManager.instance.LoadSprite ("Game/Item/chars_spaced", charIndex);
    }
}