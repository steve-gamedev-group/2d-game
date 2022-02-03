using System;
using Godot;
using SteveExtensionMethods;

public class Dash : Node
{
	[Export] public Curve speedCurve = new Curve();

	public bool isDashing { get; private set; } = false;

	private Node localPlayer;
	private RigidBody2D localPlayerRb2D;

	private int currentDashFrame = 0;

	public override void _Ready()
	{
		localPlayer = GetNode<Node>("/root/Node2D/Player");
		localPlayerRb2D = localPlayer.GetNode<RigidBody2D>("./RigidBody2D");

		// speedCurve.AddPoint(new Vector2(0f, 0f), 0f, 0.5f);
		// speedCurve.AddPoint(new Vector2(1f, 1000f), 0.5f, 0f);
		speedCurve.BakeResolution = Engine.IterationsPerSecond;
		speedCurve.Bake();
	}

	public override void _PhysicsProcess(float delta)
	{
		if (Input.IsActionJustPressed("dash"))
		{
			StartDash();
		}

		if (isDashing)
		{
			DashProcess();
		}
	}

	public void StartDash()
	{
		isDashing = true;
		currentDashFrame = 0;
	}

	public void StopDash()
	{
		isDashing = false;
	}

	private void DashProcess()
	{
		if (currentDashFrame >= speedCurve.BakeResolution)
		{
			StopDash();
			return;
		}

		GD.Print(speedCurve.GetPointCount());

		localPlayerRb2D.AppliedForce += new Vector2(0f, speedCurve.GetPointPosition(currentDashFrame).y * 1000f).
			Rotated(localPlayerRb2D.Rotation + 180f.DegToRad());
		currentDashFrame += 1;
	}
}
