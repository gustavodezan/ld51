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
		var shell = area as Shell;
		if (shell != null)
		{
			// Player damage
			var playerDamage = GetNode<Player>("../Player").Damage;
			shell.Hurt(playerDamage);
			QueueFree();
		}
	}
}
