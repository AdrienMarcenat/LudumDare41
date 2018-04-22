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
		l.numberModifier += r.numberModifier;
		l.speedModifier *= r.speedModifier == 0 ? 1 : r.speedModifier;

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

	public CommandModifier GetModifiers (string[] words)
	{
		CommandModifier result = new CommandModifier (1, 1, 1);
		foreach (string word in words)
		{
			result += new CommandModifier (m_SizeModifiers.GetModifier (word),
				m_NumberModifiers.GetModifier (word),
				m_SpeedModifiers.GetModifier (word)
			);
		}

		return result;
	}
}

