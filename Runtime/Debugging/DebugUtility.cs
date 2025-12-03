using UnityEngine;

namespace Fsi.General.Debugging
{
    public static class DebugUtility
    {
        public static void DrawBox(Vector3 center, Vector3 size, Quaternion rotation, Color color)
                {
        	        Vector3 forward = rotation * Vector3.forward;
        	        Vector3 right   = rotation * Vector3.right;
        	        Vector3 up      = rotation * Vector3.up;
        
        	        Vector3 c0 = center
        	                     + forward * size.z
        	                     - right * size.x
        	                     + up * size.y;
        	        Vector3 c1 = center
        	                     + forward * size.z
        	                     + right * size.x
        	                     + up * size.y;
        	        Vector3 c2 = center
        	                     + forward * size.z
        	                     - right * size.x
        	                     - up * size.y;
        	        Vector3 c3 = center
        	                     + forward * size.z
        	                     + right * size.x
        	                     - up * size.y;
        	        
        	        Vector3 c4 = center
        	                     - forward * size.z
        	                     - right * size.x
        	                     + up * size.y;
        	        Vector3 c5 = center
        	                     - forward * size.z
        	                     + right * size.x
        	                     + up * size.y;
        	        Vector3 c6 = center
        	                     - forward * size.z
        	                     - right * size.x
        	                     - up * size.y;
        	        Vector3 c7 = center
        	                     - forward * size.z
        	                     + right * size.x
        	                     - up * size.y;
        
        	        Debug.DrawLine(c0, c1, color);
        	        Debug.DrawLine(c1, c3, color);
        	        Debug.DrawLine(c3, c2, color);
        	        Debug.DrawLine(c2, c0, color);
        	        
        	        Debug.DrawLine(c4, c5, color);
        	        Debug.DrawLine(c5, c7, color);
        	        Debug.DrawLine(c7, c6, color);
        	        Debug.DrawLine(c6, c4, color);
        	        
        	        Debug.DrawLine(c0, c4, color);
        	        Debug.DrawLine(c1, c5, color);
        	        Debug.DrawLine(c3, c7, color);
        	        Debug.DrawLine(c2, c6, color);
                }
    }
}