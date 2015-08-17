using UnityEngine;
using System.Collections;

public static class GameEventManager {

	public delegate void GameEvent();
	public delegate void PlayerEvent();
	
	public static event GameEvent GameStart, GameOver, Respawn, EndGame;
	public static event PlayerEvent PlayerRunning, PlayerWalking, PlayerStopping; 
	public enum GameState
	{
		Live,
		Pause,
		MainMenu,
		EndGame
	};
	public enum PlayerState
	{
		Walking,
		Running
	};
	public static bool gameOver = false;
	
	public static void TriggerGameStart(string _trigger)
	{
		if(GameStart != null)
		{
			Debug.LogWarning("GAMESTART "  + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.MainMenu;
			GameStart();
		}
	}

	
	public static void TriggerRespawn(string _trigger)
	{
		if( Respawn != null && LevelManager.GAMESTATE != GameState.Live)
		{
			Debug.LogWarning("RESPAWN " + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.Live;
			Respawn();
		}
	}

	public static void TriggerEndGame()
	{
		if(EndGame != null && LevelManager.GAMESTATE != GameState.MainMenu)
		{
			Debug.LogWarning("ENDGAME");
			gameOver = false;
			LevelManager.GAMESTATE = GameState.EndGame;
			EndGame();
		}
	}
	public static void TriggerWalk()
	{
		if(EndGame != null && LevelManager.GAMESTATE != GameState.MainMenu)
		{
			Debug.LogWarning("ENDGAME");
			gameOver = false;
			LevelManager.GAMESTATE = GameState.EndGame;
			EndGame();
		}
	}
	public static void TriggerRun()
	{
		if(EndGame != null)
		{
			Debug.LogWarning("ENDGAME");
			gameOver = false;
			LevelManager.GAMESTATE = GameState.EndGame;
			EndGame();
		}
	}
}
