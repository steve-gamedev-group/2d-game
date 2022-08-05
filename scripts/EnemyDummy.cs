namespace Game;

public class EnemyDummy : RigidBody2D
{
	[Export] public float Health = 100f;

	[Export] public bool InventoryIntegration = false;
	[Export]
	// Resource type ((int)ResourceTypes.x), amount to drop (int) 
	public Dictionary<int, int> ResourceDrops = new Dictionary<int, int>()
	{
		{ (int)InventoryManager.ResourceTypes.Fuel, 1 },
		{ (int)InventoryManager.ResourceTypes.Metal, 1 }
	};

	public void TakeDamage(float amount)
	{
		Health -= amount;

		if (Health <= 0f)
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
		Health += amount;

		// Play effect here
	}

	private void Die(float lastHitAmount)
	{
		// Screenshake on death
		GetNode("/root/Node2D/Player/Camera2D").Call("AddTrauma", 0.05f * lastHitAmount);

		// Drop resources
		if (InventoryIntegration)
		{
			GetNode("/root/InventoryManager").Call("SpawnResourceDrops", GlobalPosition, GlobalRotation, ResourceDrops);
		}

		QueueFree();
	}
}
