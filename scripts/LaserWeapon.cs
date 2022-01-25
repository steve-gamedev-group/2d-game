using System;
using Godot;

public class LaserWeapon : Node2D
{
	[Export] public float damage = 10f;
	[Export] public float shootSpeed = 0.25f;
	[Export] public float bulletSpeed = 1f;
	[Export] public string bulletResourcePath = "res://prefabs/laser_bullet.tscn";

	private float lastShotTime = 0f;

	public override void _Ready()
	{
		lastShotTime = OS.GetTicksMsec() / 1000;
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionPressed("shoot") && OS.GetTicksMsec() / 1000 - lastShotTime >= shootSpeed)
		{
			Shoot();
			lastShotTime = OS.GetTicksMsec() / 1000;
		}

		// GD.Print($"lastShotTime: {lastShotTime} ----- OS.GetTicksMsec() / 1000 - lastShotTime: {OS.GetTicksMsec() / 1000 - lastShotTime}");
	}

	public void Shoot()
	{
		RigidBody2D bullet = GD.Load<PackedScene>(bulletResourcePath).Instance<RigidBody2D>();
		GetNode("../../").AddChild(bullet);

		bullet.Position = GlobalPosition;
		bullet.Rotation = GlobalRotation;
		bullet.LinearVelocity = new Vector2(0, -bulletSpeed).Rotated(bullet.Rotation);
	}
}
