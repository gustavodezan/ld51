using Godot;
using System;

public class Main : Node
{
	[Export]
	public PackedScene ProjectileScene;
	[Export]
	public PackedScene LongLegsScene;
	
	[Export]
	public PackedScene ShellScene;
	
	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
		
		for (int i = 0; i < 10; i++)
		{
			var shell = (Shell)ShellScene.Instance();
			shell.Position = new Vector2(x:i*32, y:32);
			AddChild(shell);
		}
				
		var longlegs = (LongLegs)LongLegsScene.Instance();
		AddChild(longlegs);
	}


	public void NewGame()
	{
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
		
		
	}
}


