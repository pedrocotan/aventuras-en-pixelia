using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseInit : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Firebase está listo para usar.");
            }
            else
            {
                Debug.LogError($"No se pudo resolver todas las dependencias de Firebase: {dependencyStatus}");
            }
        });
    }
}