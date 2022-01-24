using System;
using Godot;

public class Player : RigidBody2D
{
	[Export] public float acceleration = 0.5f;
	[Export] public float maxVelocity = 500f;

	public Camera2D camera;

	private Vector2 direction = Vector2.Zero;
	private string state = "stopped";

	public override void _Ready()
	{
		camera = GetNode<Camera2D>("../Camera2D");
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustReleased("restart"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		// Up and down
		if (Input.IsActionPressed("move_up") || Input.IsActionPressed("move_down"))
		{
			// Up
			if (Input.IsActionPressed("move_up"))
			{
				LinearVelocity += direction * acceleration;
				state = "moving forward";
			}

			// Down
			if (Input.IsActionPressed("move_down"))
			{
				LinearVelocity += -direction * acceleration;
				state = "moving backwards";
			}
		}
		else
		{
			// Slow down
			LinearVelocity += new Vector2(0f, -LinearVelocity.y * delta);

			// Stop
			if (LinearVelocity.Abs().y <= 5f)
			{
				LinearVelocity = new Vector2(LinearVelocity.x, 0f);
			}
		}

		// Right and left
		if (Input.IsActionPressed("move_right") || Input.IsActionPressed("move_left"))
		{
			// Right
			if (Input.IsActionPressed("move_right"))
			{
				LinearVelocity += direction.Rotated(90 / (Mathf.Pi * 2)) * acceleration;
				state = "moving right";
			}

			// Left
			if (Input.IsActionPressed("move_left"))
			{
				LinearVelocity += -direction.Rotated(90 / (Mathf.Pi * 2)) * acceleration;
				state = "moving left";
			}
		}
		else
		{
			// Slow down
			LinearVelocity += new Vector2(0f, -LinearVelocity.x * delta);

			// Stop
			if (LinearVelocity.Abs().x <= 5f)
			{
				LinearVelocity = new Vector2(0f, LinearVelocity.y);
			}
		}

		Rotation = (GetGlobalMousePosition() - Position).Angle();
		direction = GetGlobalMousePosition() - Position;
		LinearVelocity = LinearVelocity.Clamped(maxVelocity);

		GetNode<Label>("/root/Node2D/UI/Debug info").Text = $"Position: {Position}\nVelocity: {LinearVelocity}\nState: {state}";
	}
}
