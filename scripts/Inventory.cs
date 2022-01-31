using Godot;

public class Inventory : Node
{
	[Export] string fuelSpritePath = "res://";
	[Export] string metalSpritePath = "res://";

	public int fuel = 0;
	public int metal = 0;

	public void SpawnResourceDrops(int amountOfFuel, int amountOfMetal)
	{
		for (int i = 0; i < amountOfFuel; i++)
		{
			Sprite spawnedResource = GD.Load<PackedScene>(fuelSpritePath).Instance<Sprite>();
			GetNode("/root/Node2D").AddChild(spawnedResource);

			spawnedResource.Call("Initialize", DroppedResource.ResourceTypes.Fuel);
		}

		for (int i = 0; i < amountOfMetal; i++)
		{
			Sprite spawnedResource = GD.Load<PackedScene>(metalSpritePath).Instance<Sprite>();
			GetNode("/root/Node2D").AddChild(spawnedResource);

			spawnedResource.Call("Initialize", DroppedResource.ResourceTypes.Metal);
		}
	}
}
