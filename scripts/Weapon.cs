namespace Game;

public class Weapon : Node2D
{
	[Export] public float Damage = 10f;
	[Export] public float ShootSpeed = 0.25f;
	[Export] public float BulletSpeed = 1f;
	[Export] public string BulletResourcePath = "res://";

	private float timeUntilNextShot = 0;

	public override void _Process(float delta)
	{
		if (Input.IsActionPressed("shoot") && timeUntilNextShot <= 0)
		{
			Shoot();
			timeUntilNextShot = ShootSpeed;
		}

		timeUntilNextShot -= delta;

		// GD.Print($"lastShotTime: {lastShotTime} ----- OS.GetTicksMsec() / 1000 - lastShotTime: {OS.GetTicksMsec() / 1000 - lastShotTime}");
	}

	public void Shoot()
	{
		RigidBody2D bullet = GD.Load<PackedScene>(BulletResourcePath).Instance<RigidBody2D>();
		GetNode("../../").AddChild(bullet);

		bullet.Call("Initialize", Damage, GlobalPosition, GlobalRotation, BulletSpeed);
	}
}
