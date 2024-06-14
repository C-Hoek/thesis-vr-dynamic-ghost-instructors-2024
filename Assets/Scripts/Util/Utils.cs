using System.Collections.Generic;
using UnityEngine;

namespace Util {
    public class Utils {
        /// <summary>
        /// This method converts all values stored in the vector to their absolute values.
        /// </summary>
        /// <param name="vector"> The input vector. </param>
        /// <returns> Returns a vector with all values equal to the absolute values of the input vector. </returns>
        public static Vector3 Vector3Abs(Vector3 vector) {
            return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
        }
        
        /// <summary>
        /// This method takes a generic list and returns a string of the form [el1, el2, ...].
        /// </summary>
        /// <param name="list"> A list of items. </param>
        /// <typeparam name="T"> The type of parameter in the list. </typeparam>
        /// <returns> A string of the </returns>
        public static string LogList<T>(List<T> list)
        {
	        var stringRep = "[";
	        for (var i = 0; i < list.Count; i++)
	        {
		        stringRep += list[i];
		        if (i < list.Count - 1)
			        stringRep += ",";
	        }
	        return stringRep + "]";
        }
    }
}
