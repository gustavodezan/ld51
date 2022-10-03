using Godot;
using System;

public class Shell : Enemy
{
	// 0 = free, 1 = attack
	public int State = 0;
	private bool PlayerHurt;
	public Vector2 ScreenSize;
	[Export]
	public int Speed = 150;
	
	[Export]
	public PackedScene PlayerScene;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}
	
	public override void _Process(float delta)
	{
		
		var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		animSprite.Playing = true;
		// string[] mobTypes = animSprite.Frames.GetAnimationNames();
		//mobTypes[0];//mobTypes[GD.Randi() % mobTypes.Length];
		
		var velocity = Vector2.Zero;
		var playerPosition = GetNode<Player>("../Player").Position;
		//var playerPosition = new Vector2(x: 240, y: 210);//GetNode<Position2D>("res://entities/player/Player/PlayerPosition");
		if (State != 1 && Math.Abs(Position.x - playerPosition.x) <= 18 && Math.Abs(Position.y - playerPosition.y) <= 18)
		{
			animSprite.Animation = "shell_attack";
			State = 1;
			PlayerHurt = false;
		}
		if (State == 1)
		{
			int numFrames = animSprite.Frame;
			if (numFrames > 4 && numFrames < 13 && !PlayerHurt)
			{
				var player = GetNode<Player>("../Player");
				// check if player is in the shell's attack range
				if (!PlayerHurt && (Math.Abs(Position.x - playerPosition.x) <= 18 && Math.Abs(Position.y - playerPosition.y) <= 18))
				{
					PlayerHurt = true;
					player.Hurt(1);
				}
			}
			if (numFrames >= 13)
			{
				State = 0;
			}
		}
		if (State == 0)
		{
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
		}
		
		// var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		// animatedSprite.Play();
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}
		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
			y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
		);
		
		if (velocity.x != 0)
		{
			animSprite.Animation = "shell_run";
			animSprite.FlipV = false;
			animSprite.FlipH = velocity.x < 0;
		}
		else if (velocity.y != 0)
		{
			animSprite.Animation = "shell_run";
		}
		else
		{
			animSprite.Animation = "shell_attack";
		}
		
	}

	private void _on_Shell_area_entered(object area)
	{
		GD.Print(area);
	}


}



