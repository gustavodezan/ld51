using Godot;
using System;

public class LongLegs : Enemy
{
	[Export]
	public PackedScene PlayerScene;
	
	[Export]
	public PackedScene BeamScene;
	
	[Export]
	public int Speed = 100;
	
	public bool canShoot = false;
	
	public int Distance = 128;
	
	public int State = 0; // 0 - livre 1 - ataque

	public override void _Ready()
	{
		SetScreenSize();
		SetSprite();
	}
	
	private void OnWaitTimeTimeout()
	{
		State = 0;
	}
	
	public override void _Process(float delta)
	{
		GetNode<CollisionShape2D>("CollisionShellAttack").Disabled = true;
		GetNode<CollisionShape2D>("CollisionShell").Disabled = true;
		GetNode<CollisionShape2D>("CollisionLongLegs").Disabled = false;
		var velocity = Vector2.Zero;
		var playerPosition = GetNode<Player>("../Player").Position;
		
		if (State == 0 && Math.Abs(Position.x - playerPosition.x) <= Distance + 32 && Math.Abs(Position.y - playerPosition.y) <= Distance + 32)
		{
			GetNode<Timer>("WaitTime").Start();
			animSprite.Animation = "long_legs";
			State = 1;
			canShoot = true;
		}
		if (State == 1)
		{
			// State Attack
			if (canShoot)
			{
				var projectile = (Beam)BeamScene.Instance();
				var direction = playerPosition - Position;
				projectile.Shoot(Position, direction);
				GetParent().AddChild(projectile);
				canShoot = false;
				GetNode<Timer>("ShotTimer").Start();
			}
		}
		else if (State == 0)
		{
			// State Free
			animSprite.Animation = "long_legs_run";
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
	

	private void OnShotTimerTimeout()
	{
		canShoot = true;
	}
	
}



