using System.Linq;
using UnityEngine;

namespace Data {

    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject {
        static T _instance;
        public static T Instance {
            get {
                if (!_instance) _instance = Resources.LoadAll<T>("").First();
                if (!_instance) Debug.LogError(typeof(T) + " singleton hasn't been created yet.");
                return _instance;
            }
        }
    }
}
