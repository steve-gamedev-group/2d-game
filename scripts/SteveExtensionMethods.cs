using System;
using Godot;

namespace SteveExtensionMethods
{
	public static class SteveMathExtensionMethods
	{
		public static float DegToRad(this float input)
		{
			return input * ((float)Math.PI / 180f);
		}

		public static float RadToDeg(this float input)
		{
			return input * (180f / (float)Math.PI);
		}
	}
}
