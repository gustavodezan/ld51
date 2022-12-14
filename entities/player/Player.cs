using Godot;
using System;

public class Player : Area2D
{
	[Export]
	public PackedScene SeedScene;
	[Export]
	public int Speed = 150;
	public int MaxHealth = 10;
	[Export]
	public int Health;
	public int Damage = 1;
	
	[Signal]
	public delegate void Dead();

	public bool canShoot = true;

	public Vector2 ScreenSize;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionPressed("shoot") && canShoot)
		{
			var projectile = (Seed)SeedScene.Instance();
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
	
	public bool Hurt(int damage)
	{
		Health -= damage;
		GD.Print(Health);
		if (Health <= 0)
		{
			Hide();
			EmitSignal(nameof(Dead));
		}
		
		return true;
	}
	
	public Vector2 GetPlayerPosition()
	{
		return Position;
	}
	
	public void Start(Vector2 pos)
	{
		Position = pos;
		Health = MaxHealth;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	
	private void _on_ShootTimer_timeout()
	{
		canShoot = true;
	}

}
