using Game.Utils;

namespace Game;

public class Dash : Node
{
	[Export] public float Duration = 1f;
	[Export] public float SpeedMultiplier = 100f;
	[Export] public Curve SpeedCurve;

	public bool IsDashing { get; private set; } = false;

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

		if (IsDashing)
		{
			DashProcess(delta);
		}
	}

	public void StartDash()
	{
		IsDashing = true;
		currentDuration = 0f;
	}

	public void StopDash() => IsDashing = false;

	private void DashProcess(float delta)
	{
		if (currentDuration > Duration)
		{
			StopDash();
			return;
		}

		GD.Print(SpeedCurve.GetPointCount());

		localPlayerRb2D.AppliedForce += new Vector2(0f, SpeedCurve.Interpolate(currentDuration) * SpeedMultiplier).
			Rotated(localPlayerRb2D.Rotation + 180f.DegToRad());
		currentDuration += delta;
	}
}
