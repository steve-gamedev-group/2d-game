using System.Collections.Generic;
using Godot;

/// <summary>
/// Requires inventory integration.
/// </summary>
/// <seealso cref="InventoryManager.ResourceTypes"/>
public class DroppedResource : Sprite
{
	#region Variables
	/// <summary>
	/// The range of randomness allowed to be added to resource drops' positions.
	/// </summary>
	[Export] public double randomisedPositionRange = 10;
	/// <summary>
	/// The range of randomness allowed to be added to resource drops' rotations, in radians.
	/// </summary>
	[Export] public double randomisedRotationRangeDegrees = 10;

	public InventoryManager.ResourceTypes resourceType;

	private readonly Dictionary<int, string> resourceTypeToSpritePath = new Dictionary<int, string>()
	{
		{ (int)InventoryManager.ResourceTypes.Fuel, "res://resources/sprites/fuel.png" },
		{ (int)InventoryManager.ResourceTypes.Metal, "res://resources/sprites/metal.png" }
	};
	#endregion

	public override void _Ready()
	{
		GetNode("./Area2D").Connect("body_entered", this, "_BodyEntered");
	}

	public void _BodyEntered(Node body)
	{
		if (body.IsInGroup("Player"))
		{
			PickUp();
		}
	}

	public void Initialize(Vector2 newGlobalPosition, float newGlobalRotation, int newResourceType)
	{
		GlobalPosition = newGlobalPosition + new Vector2(
			(float)GD.RandRange(-randomisedPositionRange, randomisedPositionRange),
			(float)GD.RandRange(-randomisedPositionRange, randomisedPositionRange));
		GlobalRotation = newGlobalRotation +
			(float)GD.RandRange(-randomisedRotationRangeDegrees, randomisedRotationRangeDegrees)
			* (Mathf.Pi / 180);

		resourceType = (InventoryManager.ResourceTypes)newResourceType;
		Texture = GD.Load<Texture>(resourceTypeToSpritePath[newResourceType]);
	}

	public void PickUp()
	{
		GetNode("/root/InventoryManager").Call("IncreaseStoredResource", resourceType, 1);
		QueueFree();
	}
}
