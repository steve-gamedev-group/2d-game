using System;
using Godot;
using SteveExtensionMethods;

public class Dash : Node
{
	[Export] public float duration = 1f;
	[Export] public float speedMultiplier = 100f;
	[Export] public Curve speedCurve;

	public bool isDashing { get; private set; } = false;

	private Node localPlayer;
	private RigidBody2D localPlayerRb2D;

	private float currentDuration = 0f;

	public override void _Ready()
	{
		localPlayer = GetNode<Node>("/root/Node2D/Player");
		localPlayerRb2D = localPlayer.GetNode<RigidBody2D>("./RigidBody2D");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (Input.IsActionJustPressed("dash"))
		{
			StartDash();
		}

		if (isDashing)
		{
			DashProcess(delta);
		}
	}

	public void StartDash()
	{
		isDashing = true;
		currentDuration = 0f;
	}

	public void StopDash()
	{
		isDashing = false;
	}

	private void DashProcess(float delta)
	{
		if (currentDuration > duration)
		{
			StopDash();
			return;
		}

		GD.Print(speedCurve.GetPointCount());

		localPlayerRb2D.AppliedForce += new Vector2(0f, speedCurve.Interpolate(currentDuration) * speedMultiplier).
			Rotated(localPlayerRb2D.Rotation + 180f.DegToRad());
		currentDuration += delta;
	}
}
