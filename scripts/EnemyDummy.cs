using System;
using Godot;

public class EnemyDummy : RigidBody2D
{
	public float health = 100f;

	public void TakeDamage(float amount)
	{
		health -= amount;
		GD.Print(health);

		// Play effect here
	}

	public void Heal(float amount)
	{
		health += amount;

		// Play effect here
	}
}
