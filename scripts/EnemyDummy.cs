using System;
using Godot;

public class EnemyDummy : RigidBody2D
{
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
		QueueFree();
	}
}
