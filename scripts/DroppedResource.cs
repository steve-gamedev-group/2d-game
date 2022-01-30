using System;
using System.Collections.Generic;
using Godot;

public class DroppedResource : Sprite
{
	public enum ResourceTypes
	{
		Fuel = 0,
		Metal,
	}
	public ResourceTypes resourceType;

	private readonly Dictionary<int, string> resourceTypeToSpritePath = new Dictionary<int, string>()
	{
		{ (int)ResourceTypes.Fuel, "res://resources/fuel.png" },
		{ (int)ResourceTypes.Metal, "res://resources/metal.png" }
	};

	public void Initialize(ResourceTypes newResourceType)
	{
		resourceType = newResourceType;
		Texture = GD.Load<Texture>(resourceTypeToSpritePath[(int)resourceType]);
	}
}
