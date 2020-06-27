using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Helper {
	public static Color HexToColor(string hex) {
		hex = hex.Replace("0x", "");
		hex = hex.Replace("#", "");
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		byte a = 255;//if lenght of hex is 6
					 //Only use alpha if the string has enough characters
		if (hex.Length == 8) {
			a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
		}
		return new Color32(r, g, b, a);
	}

	public static string GenerateID() {
		StringBuilder builder = new StringBuilder();
		Enumerable
		   .Range(65, 26)
			.Select(e => ((char)e).ToString())
			.Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
			.Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
			.OrderBy(e => Guid.NewGuid())
			.Take(11)
			.ToList().ForEach(e => builder.Append(e));
		return builder.ToString();
	}
}
