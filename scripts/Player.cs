using System;
using Godot;

public class Player : RigidBody2D
{
	[Export] public float acceleration = 0.5f;
	[Export] public float deceleration = 2f;
	[Export] public float maxVelocity = 500f;
	[Export] public float zeroVelocityRange = 50f;

	private Vector2 _thrust = new Vector2(0, -250);
	private float _torque = 2500;

	public Camera2D camera;

	private Vector2 direction = Vector2.Zero;
	private string playerState = "stopped";

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

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		if (Input.IsActionPressed("move_forward"))
		{
			AppliedForce = _thrust.Rotated(Rotation);
			playerState = "moving forward";
		}
		else
		{
			AppliedForce = new Vector2();
			playerState = "slowing down";
		}

		var rotationDir = 0;
		if (Input.IsActionPressed("move_right"))
			rotationDir += 1;
		if (Input.IsActionPressed("move_left"))
			rotationDir -= 1;
		AppliedTorque = rotationDir * _torque;

		// TODO: get mouse rotation working without borking the physics
		// AngularVelocity = (GetGlobalMousePosition() - Position).Angle() + (90 * Mathf.Pi / 180);
		// Transform2D xform = state.Transform.Rotated((GetGlobalMousePosition() - Position).Angle());
		// state.Transform = xform;

		// TODO: max velocity
	}

	public override void _PhysicsProcess(float delta)
	{
		GetNode<Label>("/root/Node2D/UI/Debug info").Text = $"Delta time: {delta}\n\nPosition: {Position}\nVelocity: {LinearVelocity}\nState: {playerState}";
	}
}
