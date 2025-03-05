using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserApiIntegration
{
    public class DisplayPosts : MonoBehaviour
    {
        public TextMeshProUGUI postsText;

        private void Start()
        {
            ApiClient.Instance.GetUserPosts(1, DisplayUserPosts);
        }

        private void DisplayUserPosts(List<Post> posts)
        {
            string displayText = "<b>User Posts:</b>\n\n";

            foreach (var post in posts)
            {
                displayText += $"<b>{post.title}</b>\n{post.content}\n\n";
            }

            postsText.text = displayText;
        }
    }
}

