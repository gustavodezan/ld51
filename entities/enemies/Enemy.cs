using Godot;
using System;

public class Enemy : Area2D
{
	public AnimatedSprite animSprite;
	public Vector2 ScreenSize;

	public void SetScreenSize()
	{
		ScreenSize = GetViewportRect().Size;
	}

	public void SetSprite()
	{
		animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		animSprite.Playing = true;
	}

	public void UpdatePosition(Vector2 velocity, int Speed, float delta)
	{
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
}
