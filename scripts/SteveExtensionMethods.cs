using System;
using Godot;

namespace SteveExtensionMethods
{
	public static class SteveMathExtensionMethods
	{
		public static float DegToRad(this float input) => input * ((float)Math.PI / 180f);
		public static float RadToDeg(this float input) => input * (180f / (float)Math.PI);
	}
}
