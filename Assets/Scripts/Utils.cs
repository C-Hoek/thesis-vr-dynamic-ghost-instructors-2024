using UnityEngine;

namespace Misc {
    public class Utils {
        /// <summary>
        /// This method converts all values stored in the vector to their avsolute values.
        /// </summary>
        /// <param name="vector"> The input vector. </param>
        /// <returns> Returns a vector with all values equal to the absolute values of the input vector. </returns>
        public static Vector3 Vector3Abs(Vector3 vector) {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
    }
}