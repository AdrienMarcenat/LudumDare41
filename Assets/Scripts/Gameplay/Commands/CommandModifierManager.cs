using UnityEngine;
using System.Collections;

public struct CommandModifier
{
	public float sizeModifier;
	public float numberModifier;
	public float speedModifier;

	public CommandModifier (float sizeModifier, float numberModifier, float speedModifier)
	{
		this.sizeModifier = sizeModifier;
		this.numberModifier = numberModifier;
		this.speedModifier = speedModifier;
	}

	public static CommandModifier operator + (CommandModifier l, CommandModifier r)
	{
		l.sizeModifier *= r.sizeModifier == 0 ? 1 : r.sizeModifier;
		if (l.sizeModifier > 8)
			l.sizeModifier = 8;
		l.numberModifier += r.numberModifier;
		if (l.numberModifier > 10)
			l.numberModifier = 10;
		l.speedModifier *= r.speedModifier == 0 ? 1 : r.speedModifier;
		if (l.speedModifier > 8)
			l.speedModifier = 8;

		return l;
	}
}

public class CommandModifierManager : Singleton<CommandModifierManager>
{
	[SerializeField] private string m_SizeModifiersFilename;
	[SerializeField] private string m_NumberModifiersFilename;
	[SerializeField] private string m_SpeedModifiersFilename;

	private static CommandModifierDictionnary m_SizeModifiers;
	private static CommandModifierDictionnary m_NumberModifiers;
	private static CommandModifierDictionnary m_SpeedModifiers;

	void Start ()
	{
		m_SizeModifiers = new CommandModifierDictionnary ();
		m_NumberModifiers = new CommandModifierDictionnary ();
		m_SpeedModifiers = new CommandModifierDictionnary ();

		m_SizeModifiers.FillModifier (m_SizeModifiersFilename);
		m_NumberModifiers.FillModifier (m_NumberModifiersFilename);
		m_SpeedModifiers.FillModifier (m_SpeedModifiersFilename);
	}

	public CommandModifier GetModifiers (string[] words, int endIndex)
	{
		CommandModifier result = new CommandModifier (1, 1, 1);
		for (int i = 0; i < endIndex; i++) {
			string word = words [i];
			result += new CommandModifier (m_SizeModifiers.GetModifier (word),
				m_NumberModifiers.GetModifier (word),
				m_SpeedModifiers.GetModifier (word)
			);
		}

		return result;
	}
}

