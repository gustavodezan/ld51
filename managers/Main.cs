using Godot;
using System;

public class Main : Node
{
	[Export]
	public PackedScene ProjectileScene;
	[Export]
	public PackedScene LongLegsScene;
	
	[Export]
	public PackedScene ShellScene;
	
	[Export]
	public PackedScene EnemyScene;
	
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
	}

	public void NewGame()
	{
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition");
		player.Start(startPosition.Position);
		GetNode<Timer>("SpawnEnemy").Start();
	}
	private void OnSpawnEnemyTimeout()
	{
		GD.Randomize();

		
		for (int i = 0; i < GD.Randi() % 9; i++)
		{
			var mob = (Shell)ShellScene.Instance();
			AddChild(mob);
					// Choose a random location on Path2D.
			var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawn");
			mobSpawnLocation.Offset = GD.Randi();

			// Set the mob's direction perpendicular to the path direction.
			float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

			// Set the mob's position to a random location.
			mob.Position = mobSpawnLocation.Position;
			mob.Speed = 200;
			// Add some randomness to the direction.
//			direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
//			mob.Rotation = direction;
		}
		GD.Randomize();
		for (int i = 0; i < GD.Randi() % 5; i++)
		{
			var mob = (LongLegs)LongLegsScene.Instance();
			AddChild(mob);
					// Choose a random location on Path2D.
			var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawn");
			mobSpawnLocation.Offset = GD.Randi();

			// Set the mob's direction perpendicular to the path direction.
			float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

			// Set the mob's position to a random location.
			mob.Position = mobSpawnLocation.Position;
		}
		
		GetNode<Timer>("SpawnEnemy").Start();	
	}
	
	private void _on_Player_Dead()
	{
		GetNode<Timer>("SpawnEnemy").Stop();
		GetNode<HUD>("HUD").ShowGameOver();
		//GetTree().CallGroup("enemies", "queue_free");
	}
}

