using System;
using Godot;

public class EnemyDummy : RigidBody2D
{
	[Export] public bool inventoryIntegration = false;
	[Export] public int fuelDropAmount = 1;
	[Export] public int metalDropAmount = 1;

	public float health = 100f;

	public void TakeDamage(float amount)
	{
		health -= amount;
		GD.Print(health);

		if (health <= 0f)
		{
			Die(amount);
		}
		else
		{
			// Screenshake on hit
			GetNode("/root/Node2D/Player/Camera2D").Call("AddTrauma", 0.025f * amount);
		}
	}

	public void Heal(float amount)
	{
		health += amount;

		// Play effect here
	}

	private void Die(float lastHitAmount)
	{
		// Screenshake on death
		GetNode("/root/Node2D/Player/Camera2D").Call("AddTrauma", 0.05f * lastHitAmount);

		// Drop resources
		if (inventoryIntegration)
		{
			GetNode("/root/Inventory").Call("SpawnResourceDrops", fuelDropAmount, metalDropAmount);
		}

		QueueFree();
	}
}
