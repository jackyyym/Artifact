/*
 * CREDIT: Michael Garforth
 * http://wiki.unity3d.com/index.php?title=IsVisibleFrom#UnityScript_-_RendererHelpers.js
 * 
 */

using UnityEngine;

public static class RendererExtensions
{
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

