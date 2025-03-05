using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace UserApiIntegration
{
    public class ApiClient : MonoBehaviour
    {
        private const string apiUrl = "http://localhost:8080";

        public static ApiClient Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void GetUser(long userId, Action<User> onUserReceived)
        {
            StartCoroutine(GetRequest<User>($"/api/users/{userId}", onUserReceived));
        }

        public void GetUserPosts(long userId, Action<List<Post>> onPostsReceived)
        {
            StartCoroutine(GetRequest<List<Post>>($"/api/users/{userId}/posts", onPostsReceived));
        }

        private IEnumerator GetRequest<T>(string endpoint, Action<T> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl + endpoint))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string responseText = webRequest.downloadHandler.text;

                    if (callback != null)
                    {
                        T result = typeof(T) == typeof(List<Post>)
                            ? (T)(object)new List<Post>(JsonHelper.FromJson<Post>(responseText))
                            : JsonUtility.FromJson<T>(responseText);
                        callback(result);
                    }
                }
                else
                {
                    UnityEngine.Debug.LogError("API Request Failed: " + webRequest.error);
                }
            }
        }
    }

    [System.Serializable]
    public class Post
    {
        public long id;
        public long userId;
        public string title;
        public string content;
    }

    [System.Serializable]
    public class User
    {
        public long id;
        public string name;
        public string email;
    }
}