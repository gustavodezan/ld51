using Godot;
using System;

public class HUD : CanvasLayer
{
	[Signal]
	public delegate void StartGame();
	
	public void ShowMessage(string text)
	{
		GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game Over");
		
		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, "timeout");
		
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		GetNode<Button>("StartButton").Show();
	}
	
	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void _on_StartButton_pressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal("StartGame");
	}
}
