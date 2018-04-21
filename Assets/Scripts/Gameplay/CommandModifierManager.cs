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
	[SerializeField] private CommandModifierDictionnary m_SizeModifiers;
	[SerializeField] private CommandModifierDictionnary m_NumberModifiers;
	[SerializeField] private CommandModifierDictionnary m_SpeedModifiers;

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

