using System;
using Godot;

public class LaserWeapon : Node2D
{
	[Export] public float damage = 10f;
	[Export] public float shootSpeed = 0.25f;
	[Export] public float bulletSpeed = 1f;
	[Export] public string bulletResourcePath = "res://prefabs/laser_bullet.tscn";

	private float timeUntilNextShot = 0;

	public override void _Process(float delta)
	{
		if (Input.IsActionPressed("shoot") && timeUntilNextShot <= 0)
		{
			Shoot();
			timeUntilNextShot = shootSpeed;
		}

		timeUntilNextShot -= delta;

		// GD.Print($"lastShotTime: {lastShotTime} ----- OS.GetTicksMsec() / 1000 - lastShotTime: {OS.GetTicksMsec() / 1000 - lastShotTime}");
	}

	public void Shoot()
	{
		RigidBody2D bullet = GD.Load<PackedScene>(bulletResourcePath).Instance<RigidBody2D>();
		GetNode("../../").AddChild(bullet);

		bullet.Call("Initialize", damage, GlobalPosition, GlobalRotation, bulletSpeed);
	}
}
