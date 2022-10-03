using Godot;
using System;

public class Shell : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	
	public Vector2 ScreenSize;
	
	[Export]
	public int Speed = 100;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		animSprite.Playing = true;
		string[] mobTypes = animSprite.Frames.GetAnimationNames();
		animSprite.Animation = mobTypes[0];//mobTypes[GD.Randi() % mobTypes.Length];
	}
	
		public override void _Process(float delta)
	{
		var velocity = Vector2.Zero;
		var playerPosition = new Vector2(x: 240, y: 210);//GetNode<Position2D>("res://entities/player/Player/PlayerPosition");
		
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
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
