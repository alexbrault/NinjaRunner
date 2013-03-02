using UnityEngine;
using System.Collections;

public class AnimationFrame
{
	public int X { get; private set; }

	public int Y { get; private set; }

	public int Width { get; private set; }

	public int Height { get; private set; }
	
	public AnimationFrame (int x, int y, int width, int height) {
		X = x;
		Y = y;
		Width = width;
		Height = height;
	}
	
	private Texture2D frame = null;
	public Texture GetTexture(Texture2D sheet) {
		if (frame == null) {
			frame = new Texture2D(Width, Height);
			frame.filterMode = FilterMode.Point;
			frame.SetPixels(sheet.GetPixels(X, sheet.height - Y - Height, Width, Height));
			frame.Apply();
		}
		
		return frame;
	}
}
