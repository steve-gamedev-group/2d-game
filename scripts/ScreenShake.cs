namespace Game;

public class ScreenShake : Camera2D
{
	#region Variables
	[Export] public string TargetNodePath; // The node this camera will follow
	private Node2D target;

	public float Decay = 0.8f; // How quickly the shaking stops [0, 1]
	public Vector2 MaxOffset = new Vector2(100, 75); // Maximum horizontal/vertical shake in pixels
	public float MaxRoll = 0.1f; // Maximum rotation in radians (use sparingly)

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

		target = GetNode<Node2D>(TargetNodePath);
	}

	public override void _Process(float delta)
	{
		if (target != null)
		{
			GlobalPosition = GlobalPosition.LinearInterpolate(target.GlobalPosition, 0.5f);
		}

		if (!Mathf.IsEqualApprox(trauma, 0f))
		{
			trauma = Mathf.Max(trauma - Decay * delta, 0);
			Shake();
		}
	}

	public void AddTrauma(float traumaToAdd) => trauma =+ traumaToAdd;

	private void Shake()
	{
		float amount = Mathf.Pow(trauma, traumaPower);

		Rotation = MaxRoll * amount * (float)GD.RandRange(-1, 1);
		Offset = new Vector2(MaxOffset.x * amount * (float)GD.RandRange(-1, 1), Offset.y);
		Offset = new Vector2(Offset.x, MaxOffset.y * amount * (float)GD.RandRange(-1, 1));
	}
}
