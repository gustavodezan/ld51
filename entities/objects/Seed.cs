using Godot;
using System;

public class Seed : Projectile
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Speed = 200;
	}

	private void OnProjectileAreaEntered(object area)
	{
		var enemy = area as Enemy;
		if (enemy != null)
		{
			// Player damage
			var playerDamage = GetNode<Player>("../Player").Damage;
			enemy.Hurt(playerDamage);
			QueueFree();
		}
	}
}
