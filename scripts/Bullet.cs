using Godot;

public class Bullet : RigidBody2D
{
	private float damage;

	public override void _Ready()
	{
		Connect("body_entered", this, "_BodyEntered");
	}

	public void Initialize(float newDamage, Vector2 newPosition, float newRotation, float newSpeed)
	{
		damage = newDamage;
		Position = newPosition;
		Rotation = newRotation;
		LinearVelocity = new Vector2(0, -newSpeed).Rotated(Rotation);
	}

	private void _BodyEntered(Node body)
	{
		if (body.IsInGroup("Damageable"))
		{
			body.Call("TakeDamage", damage);
			QueueFree();
		}
	}
}
