using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAnimationSystem;

public static class BattleAnimationDex
{
	public const string DATA_FOLDER = "moddable_data/battle_animations";

	private static GenericDex<SimpleAnimation> dex = new GenericDex<SimpleAnimation>();

	static BattleAnimationDex()
	{
		//TODO:Load all battle animations

		//string[] animationFiles = Directory.GetFiles(DATA_FOLDER);

	}

	public static void AddEntry(DexID id, SimpleAnimation animation)
	{
		dex.AddEntry(id, animation);
	}

	public static SimpleAnimation GetEntry(DexID id)
	{
		return dex.GetEntry(id);
	}
}
