namespace Game;

public class EnemyDummy : RigidBody2D
{
	[Export] public float health = 100f;

	[Export] public bool inventoryIntegration = false;
	[Export]
	// Resource type ((int)ResourceTypes.x), amount to drop (int) 
	public Dictionary<int, int> resourceDrops = new Dictionary<int, int>()
	{
		{ (int)InventoryManager.ResourceTypes.Fuel, 1 },
		{ (int)InventoryManager.ResourceTypes.Metal, 1 }
	};

	public void TakeDamage(float amount)
	{
		health -= amount;

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
			GetNode("/root/InventoryManager").Call("SpawnResourceDrops", GlobalPosition, GlobalRotation, resourceDrops);
		}

		QueueFree();
	}
}
