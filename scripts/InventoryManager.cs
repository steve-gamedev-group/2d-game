using System.Collections.Generic;
using Godot;

/// <summary>
/// Singleton (autoload in Godot), data persists with scene changes.
/// </summary>
public class InventoryManager : Node
{
	#region Variables
	public static InventoryManager instance;

	[Export] public string droppedResourcePrefabPath = "res://";

	public enum ResourceTypes
	{
		Fuel = 0,
		Metal,
	}

	// Resource type ((int)ResourceTypes.x), amount stored in inventory (int) 
	public Dictionary<int, int> storedResources = new Dictionary<int, int>()
	{
		{ (int)ResourceTypes.Fuel, 0 },
		{ (int)ResourceTypes.Metal, 0 }
	};
	#endregion

	public override void _Ready()
	{
		// C# singleton instance initializer for strong typing support
		if (instance != null)
		{
			GD.Print("InventoryManager instance was already set, overriding.");
		}
		instance = this;
	}

	public void SpawnResourceDrops(Vector2 globalPosition, float globalRotation, Dictionary<int, int> resourceDrops)
	{
		//* Resource drops dictionary example (use as a base): ------------------
		// // Resource type ((int)ResourceTypes.x), amount to drop (int) 
		// public Dictionary<int, int> resourceDrops = new Dictionary<int, int>()
		// {
		// 	{ (int)DroppedResource.ResourceTypes.Fuel, 1 },
		// 	{ (int)DroppedResource.ResourceTypes.Metal, 1 }
		// };
		//* Don't delete --------------------------------------------------------

		for (int currentKey = 0; currentKey < resourceDrops.Keys.Count; currentKey++)
		{
			for (int i = 0; i < resourceDrops[currentKey]; i++)
			{
				Sprite spawnedResource = GD.Load<PackedScene>(droppedResourcePrefabPath).Instance<Sprite>();
				GetNode("/root/Node2D").CallDeferred("add_child", spawnedResource);

				spawnedResource.Call("Initialize", globalPosition, globalRotation, currentKey);
			}
		}
	}

	public void IncreaseStoredResource(ResourceTypes resourceType, int amount)
	{
		storedResources[(int)resourceType] += amount;

		// Play effect here
	}
}
