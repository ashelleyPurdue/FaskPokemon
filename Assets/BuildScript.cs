#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class BuildScript
{
	public const string DATA_FOLDER_NAME = "moddable_data";

	[PostProcessBuildAttribute(0)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
	{
		//copy the moddable data files to the build path

		try
		{
			string targetDir = Path.GetDirectoryName(pathToBuiltProject) + "/" + DATA_FOLDER_NAME;

			Directory.CreateDirectory(targetDir);
			CloneDirectory(DATA_FOLDER_NAME + "/", targetDir);
		}
		catch(DirectoryNotFoundException e)
		{
			Debug.Log("couldn't find directory " + e.Message);
		}
	}

	//Taken from StackOverflow (http://stackoverflow.com/questions/36484009/how-to-copy-and-paste-a-whole-directory-to-a-new-path-recursively)
	//Because apparently, Microsoft doesn't include this function in their libraries.  Stupid.
	private static void CloneDirectory(string root, string dest)
	{
		foreach (var directory in Directory.GetDirectories(root))
		{
			string dirName = Path.GetFileName(directory);
			if (!Directory.Exists(Path.Combine(dest, dirName)))
			{
				Directory.CreateDirectory(Path.Combine(dest, dirName));
			}
			CloneDirectory(directory, Path.Combine(dest, dirName));
		}

		foreach (var file in Directory.GetFiles(root))
		{
			File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
		}
	}
}

#endif
