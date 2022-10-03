using Godot;
using System;

public class Main : Node
{
	[Export]
	public PackedScene ProjectileScene;
	[Export]
	public PackedScene LongLegsScene;
	
	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
		
				
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


