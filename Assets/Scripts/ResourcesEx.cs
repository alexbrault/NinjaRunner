using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public static class ResourcesEx {
	public static TOutput[] LoadAll<TOutput>(string path) where TOutput : class{
		var resources = Resources.LoadAll(path, typeof(TOutput));
		return Array.ConvertAll(resources, resource => resource as TOutput);
	}
}
