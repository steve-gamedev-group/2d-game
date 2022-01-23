using System;
using Godot;

public class Player : RigidBody2D
{
	public Camera2D camera;

	private string state = "Stopped";
	public float maxVelocity = 50f;

	public override void _Ready()
	{
		camera = GetNode<Camera2D>("../Camera2D");
	}

	public override void _PhysicsProcess(float delta)
	{
		// Up and down
		if (Input.IsActionPressed("move_up") || Input.IsActionPressed("move_down"))
		{
			// Up
			if (Input.IsActionPressed("move_up"))
			{
				state = "Moving forward";
				LinearVelocity += -Transform.y * 1f;
			}

			// Down
			if (Input.IsActionPressed("move_down"))
			{
				state = "Moving backwards";
				LinearVelocity += Transform.y * 1f;
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
				state = "Moving right";
				LinearVelocity += Transform.x * 1f;
			}

			// Left
			if (Input.IsActionPressed("move_left"))
			{
				state = "Moving left";
				LinearVelocity += -Transform.x * 1f;
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

		Rotation = (GetGlobalMousePosition() - Position).Angle() + Mathf.Pi / 2;

		GetNode<Label>("/root/Node2D/UI/Debug info").Text = $"Position: {Position}\nVelocity: {LinearVelocity}\nState: {state}";
	}
}
