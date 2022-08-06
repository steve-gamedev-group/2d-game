namespace Game;

public class UpgradeManager : Node
{
	public static UpgradeManager instance;

	public override void _Ready()
	{
		// C# singleton instance initializer for strong typing support
		if (instance != null)
		{
			GD.Print("InventoryManager instance was already set, overriding.");
		}
		instance = this;
	}

	public void AddUpgrade()
	{
		
	}

	public void RemoveUpgrade()
	{

	}
}
