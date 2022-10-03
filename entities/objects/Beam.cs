using Godot;
using System;

public class Beam : Projectile
{
	public override void _Ready()
	{
		
	}

	private void OnProjectileAreaEntered(object area)
	{
		var player = area as Player;
		if (player != null)
		{
			player.Hurt(1);
			QueueFree();
		}
	}
}
