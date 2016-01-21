using UnityEngine;
using System.Collections.Generic;

public static class Resource
{
	/********************************************************
	 * List of objects being used in the game
	 * *****************************************************/
	public static Dictionary<string,Tile> tileTemplate = new Dictionary<string, Tile> ();
	public static Dictionary<string,Reaction> reactionTemplate = new Dictionary<string, Reaction> ();
	public static Dictionary<string,Disaster> disasterTemplate = new Dictionary<string, Disaster> ();

	public static Mesh baseMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));
	public static Material baseMaterial = (Material)Resources.Load (("Tiles/Materials/Default"), typeof(Material));

	/**********************************************************
	 * SOUND OBJECTS
	 * ******************************************************/
	public static AudioClip ambience = (AudioClip)Resources.Load ("Sound/Music/ambience", typeof(AudioClip));
	public static AudioClip mainTheme = (AudioClip)Resources.Load ("Sound/Music/Main_Theme/Main", typeof(AudioClip));
	public static AudioClip loseTheme = (AudioClip)Resources.Load ("Sound/Music/GameOver_Theme/GameOverRev2", typeof(AudioClip));
	public static AudioClip winTheme = (AudioClip)Resources.Load ("Sound/Music/Winning_Theme/WinMusic", typeof(AudioClip));
	
	public static AudioClip Click = (AudioClip)Resources.Load ("Sound/FX/gameplay/Click", typeof(AudioClip));
	public static AudioClip startButton = (AudioClip)Resources.Load ("Sound/FX/gameplay/StartButton", typeof(AudioClip));
	public static AudioClip wannaQuit = (AudioClip)Resources.Load ("Sound/FX/gameplay/WannaQuit", typeof(AudioClip));
	
	public static AudioClip[] elementSound = new AudioClip[]
	{
		(AudioClip)Resources.Load ("Sound/FX/Elements/earth", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/Elements/air", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/Elements/water", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/Elements/fire", typeof(AudioClip)),
	};
	
	/****************************************************************************
	 * GUI OBJECTS
	 * **************************************************************************/
	public static Texture2D Air_Btn = (Texture2D)Resources.Load (("Gui/Air_Btn"), typeof(Texture2D));
	public static Texture2D Earth_Btn = (Texture2D)Resources.Load (("Gui/Earth_Btn"), typeof(Texture2D));
	public static Texture2D Fire_Btn = (Texture2D)Resources.Load (("Gui/Fire_Btn"), typeof(Texture2D));
	public static Texture2D Water_Btn = (Texture2D)Resources.Load (("Gui/Water_Btn"), typeof(Texture2D));
	public static Texture2D Menu_Btn = (Texture2D)Resources.Load (("Gui/Menu_Btn"), typeof(Texture2D));
	
	public static Texture2D Air_Cursor = (Texture2D)Resources.Load (("Gui/Air_Cursor"), typeof(Texture2D));
	public static Texture2D Earth_Cursor = (Texture2D)Resources.Load (("Gui/Earth_Cursor"), typeof(Texture2D));
	public static Texture2D Fire_Cursor = (Texture2D)Resources.Load (("Gui/Fire_Cursor"), typeof(Texture2D));
	public static Texture2D Water_Cursor = (Texture2D)Resources.Load (("Gui/Water_Cursor"), typeof(Texture2D));
	public static Texture2D Menu_Cursor = (Texture2D)Resources.Load (("Gui/Blk_Cursor"), typeof(Texture2D));
	
	public static Texture2D NG_Btn = (Texture2D)Resources.Load (("Gui/NG_Btn"), typeof(Texture2D));
	public static Texture2D ContinueGame_Btn = (Texture2D)Resources.Load (("Gui/ContinueGame_Btn"), typeof(Texture2D));
	public static Texture2D QuitGame_Btn = (Texture2D)Resources.Load (("Gui/QuitGame_Btn"), typeof(Texture2D));
	public static Texture2D RestartGame_Btn = (Texture2D)Resources.Load (("Gui/RestartGame_Btn"), typeof(Texture2D));
	public static Texture2D Yes_Btn = (Texture2D)Resources.Load (("Gui/Yes_Btn"), typeof(Texture2D));
	public static Texture2D No_Btn = (Texture2D)Resources.Load (("Gui/No_Btn"), typeof(Texture2D));
	
	public static Texture2D HardDifficulty_Btn = (Texture2D)Resources.Load (("Gui/HardDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D MediumDifficulty_Btn = (Texture2D)Resources.Load (("Gui/MediumDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D LowDifficulty_Btn = (Texture2D)Resources.Load (("Gui/LowDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D Back_Btn = (Texture2D)Resources.Load (("Gui/Back_Btn"), typeof(Texture2D));
	public static Texture2D CenterConsole = (Texture2D)Resources.Load (("Gui/CenterConsole"), typeof(Texture2D));
	
	public static Texture2D Title = (Texture2D)Resources.Load (("Gui/Title"), typeof(Texture2D));
	public static Texture2D TitleBackground = (Texture2D)Resources.Load (("Gui/TitleBackground"), typeof(Texture2D));
	public static Texture2D Lose = (Texture2D)Resources.Load (("Gui/GameOver2"), typeof(Texture2D));
	public static Texture2D Win = (Texture2D)Resources.Load (("Gui/GameWin"), typeof(Texture2D));
	public static Texture2D Exit = (Texture2D)Resources.Load (("Gui/AreYouSure"), typeof(Texture2D));
}
