using Godot;
using System;

public class Enemy : Area2D
{
	public int MaxHealth = 2;
	public int Health;
	public override void _Ready()
	{
				
	}
	
	public void Start(Vector2 pos)
	{
		Position = pos;
		Health = MaxHealth;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	
	public void Die()
	{
		QueueFree();
	}
	
	public bool Hurt(int damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			Die();
			return true;
		}
		return false;
	}
}
