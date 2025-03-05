using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace UserApiIntegration
{
    public class TestApiIntegration : MonoBehaviour
    {
        public TextMeshProUGUI postText;

        private string userName = "User";

        private void Start()
        {
            if (postText == null)
            {
                Debug.LogError("⚠️ PostText UI element is not assigned in Inspector!");
                return;
            }

            postText.text = "Loading...";

            ApiClient.Instance.GetUser(1, user =>
            {
                userName = user.name;
                Debug.Log($"✅ User: {user.name}, {user.email}");

                ApiClient.Instance.GetUserPosts(1, DisplayPosts);
            });
        }

        private void DisplayPosts(List<Post> posts)
        {
            if (postText == null)
            {
                Debug.LogError("⚠️ PostText is null. Check UI assignment.");
                return;
            }

            if (posts == null || posts.Count == 0)
            {
                postText.text = $"{userName} has no posts.";
                return;
            }

            string displayText = $"<b>{userName}'s Posts:</b>\n\n";

            foreach (var post in posts)
            {
                displayText += $"<b>{post.title}</b>\n{post.content}\n\n";
            }

            postText.text = displayText;
            Debug.Log("✅ UI Updated with Posts!");
        }
    }
}



