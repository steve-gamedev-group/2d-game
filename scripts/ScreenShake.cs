using Godot;

public class ScreenShake : Camera2D
{
	#region Variables
	[Export] public string targetNodePath; // The node this camera will follow
	private Node2D target;

	public float decay = 0.8f; // How quickly the shaking stops [0, 1]
	public Vector2 maxOffset = new Vector2(100, 75); // Maximum horizontal/vertical shake in pixels
	public float maxRoll = 0.1f; // Maximum rotation in radians (use sparingly)

	private OpenSimplexNoise noise = new OpenSimplexNoise();
	private float noiseY = 0f;
	private float trauma = 0f; // Current shake strength
	private float traumaPower = 2f; // Trauma exponent [2, 3]
	#endregion

	public override void _Ready()
	{
		noise.Seed = (int)GD.Randi();
		noise.Period = 4;
		noise.Octaves = 2;

		target = GetNode<Node2D>(targetNodePath);
	}

	public override void _Process(float delta)
	{
		if (target != null)
		{
			GlobalPosition = GlobalPosition.LinearInterpolate(target.GlobalPosition, 0.5f);
		}

		if (!Mathf.IsEqualApprox(trauma, 0f))
		{
			trauma = Mathf.Max(trauma - decay * delta, 0);
			Shake();
		}
	}

	public void AddTrauma(float traumaToAdd) => trauma =+ traumaToAdd;

	private void Shake()
	{
		float amount = Mathf.Pow(trauma, traumaPower);

		Rotation = maxRoll * amount * (float)GD.RandRange(-1, 1);
		Offset = new Vector2(maxOffset.x * amount * (float)GD.RandRange(-1, 1), Offset.y);
		Offset = new Vector2(Offset.x, maxOffset.y * amount * (float)GD.RandRange(-1, 1));
	}
}
