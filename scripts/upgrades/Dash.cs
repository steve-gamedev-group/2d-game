using System;
using Godot;

public class Dash : Node2D
{
	[Export] public float speed = 25f;
	[Export] public float range = 50f;

	private Node localPlayer;
	private RigidBody2D localPlayerRb2D;

	public override void _Ready()
	{
		localPlayerRb2D = GetNode<RigidBody2D>("/root/Node2D/Player/RigidBody2D");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (Input.IsActionJustPressed("dash"))
		{
			localPlayerRb2D.GlobalPosition = localPlayerRb2D.GlobalPosition.Normalized().Slerp(new Vector2(0f, range).Rotated(Rotation), 0.5f);
		}
	}
}
