using Godot;
using System;

public class Projectile : Area2D
{
	[Export]
	public int Speed = 100;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public override void _Process(float delta)
	{
		var direction = Vector2.Right.Rotated(Rotation);
		Position += Speed * direction * delta;
	}

	// Shoot
	public void Shoot(Vector2 position, Vector2 direction)
	{
		Position = position;
		Rotation = direction.Angle();
	}



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
