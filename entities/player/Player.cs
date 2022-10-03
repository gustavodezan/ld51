using Godot;
using System;

public class Player : Area2D
{
	[Export]
	public PackedScene ProjectileScene;
	[Export]
	public int Speed = 100;
	
	[Signal]
	public delegate void Hit();

	public bool canShoot = true;

	public Vector2 ScreenSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionPressed("shoot") && canShoot)
		{
			var projectile = (Projectile)ProjectileScene.Instance();
			var mouseDirection = GetGlobalMousePosition() - Position;
			projectile.Shoot(Position, mouseDirection);
			GetParent().AddChild(projectile);
			canShoot = false;
			GetNode<Timer>("ShootTimer").Start();
		}
		
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
		{
			velocity.x += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.x -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.y -= 1;
		}

		var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		animatedSprite.Play();
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}
		// else
		// {
		// 	animatedSprite.Stop();
		// }

		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
			y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
		);

		if (velocity.x != 0)
		{
			animatedSprite.Animation = "run";
			animatedSprite.FlipV = false;
			animatedSprite.FlipH = velocity.x < 0;
		}
		else if (velocity.y != 0)
		{
			animatedSprite.Animation = "run";
		}
		else
		{
			animatedSprite.Animation = "idle";
		}
	}
	
	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	
	private void _on_ShootCD_timeout()
	{
		canShoot = true;
	}
}


