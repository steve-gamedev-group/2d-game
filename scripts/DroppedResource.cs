namespace Game;

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
	[Export] public double RandomisedPositionRange = 10;
	/// <summary>
	/// The range of randomness allowed to be added to resource drops' rotations, in radians.
	/// </summary>
	[Export] public double RandomisedRotationRangeDegrees = 10;

	public InventoryManager.ResourceTypes ResourceType;

	// TODO: Make resourceTypeToSpritePath dynamic
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
			(float)GD.RandRange(-RandomisedPositionRange, RandomisedPositionRange),
			(float)GD.RandRange(-RandomisedPositionRange, RandomisedPositionRange));
		GlobalRotation = newGlobalRotation +
			(float)GD.RandRange(-RandomisedRotationRangeDegrees, RandomisedRotationRangeDegrees)
			* (Mathf.Pi / 180);

		ResourceType = (InventoryManager.ResourceTypes)newResourceType;
		Texture = GD.Load<Texture>(resourceTypeToSpritePath[newResourceType]);
	}

	public void PickUp()
	{
		GetNode("/root/InventoryManager").Call("IncreaseStoredResource", ResourceType, 1);
		QueueFree();
	}
}
