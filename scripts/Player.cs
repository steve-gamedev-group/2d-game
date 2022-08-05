global using System;
global using System.Collections.Generic;
global using Godot;

namespace Game;

public class Player : RigidBody2D
{
	[Export] public float Acceleration = 0.5f;
	[Export] public float Deceleration = 2f;
	[Export] public float MaxVelocity = 250f;
	[Export] public float ZeroVelocityRange = 10f;

	public Camera2D Camera;

	private Vector2 thrust = new Vector2(0, -250);
	private string playerState = "stopped";

	public override void _Ready()
	{
		Camera = GetNode<Camera2D>("../Camera2D");

		Engine.TargetFps = 120;
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustReleased("restart"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		Rotation = (GetGlobalMousePosition() - Position).Angle() + (90 * Mathf.Pi / 180);

		if (Input.IsActionPressed("move_forward"))
		{
			AppliedForce = thrust.Rotated(Rotation);
			playerState = "moving forward";
		}
		else
		{
			AppliedForce = new Vector2();
			playerState = "slowing down";

			if (LinearVelocity.Abs().x <= ZeroVelocityRange)
			{
				LinearVelocity = new Vector2(0, LinearVelocity.y);
			}
			if (LinearVelocity.Abs().y <= ZeroVelocityRange)
			{
				LinearVelocity = new Vector2(LinearVelocity.x, 0);
			}
		}

		// var rotationDir = 0;
		// if (Input.IsActionPressed("move_right"))
		// 	rotationDir += 1;
		// if (Input.IsActionPressed("move_left"))
		// 	rotationDir -= 1;
		// AppliedTorque = rotationDir * torque;

		LinearVelocity = LinearVelocity.Clamped(MaxVelocity);
	}

	public override void _PhysicsProcess(float delta)
	{
		string[] resourceTypeNames = Enum.GetNames(typeof(InventoryManager.ResourceTypes));
		string storedResourcesString = "";

		for (int i = 0; i < resourceTypeNames.Length; i++)
		{
			Dictionary<int, int> storedResources = InventoryManager.instance.StoredResources;
			storedResourcesString += $"\n{resourceTypeNames[i]}: {storedResources[i]}";
		}

		GetNode<Label>("/root/Node2D/UI/Debug info").Text =
		$"Delta time: {delta}"
		+ $"\n\nPosition: {Position}\nVelocity: {LinearVelocity}\nState: {playerState}"
		+ $"\n\nInventory"
		+ storedResourcesString;
	}
}
