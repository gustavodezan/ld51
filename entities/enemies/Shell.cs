using Godot;
using System;

public class Shell : Enemy
{		
	[Export]
	public int Speed = 100;

	public override void _Ready()
	{
		SetScreenSize();
		SetSprite();
	}
	
	public override void _Process(float delta)
	{		
		var velocity = Vector2.Zero;
		var playerPosition = new Vector2(x: 240, y: 210);
		
		if (Math.Abs(Position.x - playerPosition.x) <= 32 && Math.Abs(Position.y - playerPosition.y) <= 32)
		{
			animSprite.Animation = "shell_attack";
		}
		else
		{
			animSprite.Animation = "shell_run";
		}
		
		if (Position.x < playerPosition.x)
		{
			velocity.x += 1;
		}
		if (Position.x > playerPosition.x)
		{
			velocity.x -= 1;
		}
		if (Position.y < playerPosition.y)
		{
			velocity.y += 1;
		}
		if (Position.y > playerPosition.y)
		{
			velocity.y -= 1;
		}
		
		UpdatePosition(velocity, Speed, delta);
	}
}
