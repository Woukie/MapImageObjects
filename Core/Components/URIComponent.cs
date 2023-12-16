using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System;
using UnboundLib;

namespace MapImageObjects.Core.Components
{
    public class URIComponent : MonoBehaviour
    {
        private string uri; // epic pic: https://raw.githubusercontent.com/Woukie/Image/main/cover%20transparent.png

        public string GetURI()
        {
            return uri;
        }

        public void SetURI(string uri)
        {
            this.uri = uri;

            // Stop the previous loading operation and start a new one
            StopAllCoroutines();
            StartCoroutine(LoadImageCoroutine());
        }

        private IEnumerator LoadImageCoroutine()
        {
            Sprite sprite = null;
            if (Plugin.ImageCache.TryGetValue(uri, out sprite))
            {
                UpdateWithSprite(sprite);
                yield break;
            }

            Texture2D texture = null;

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(GetURI()))
            {
                UnityWebRequestAsyncOperation asyncOp = www.SendWebRequest();

                while (!asyncOp.isDone)
                {
                    yield return null;
                }

                if (www.isNetworkError || www.isHttpError)
                {
                    yield break;
                }

                texture = DownloadHandlerTexture.GetContent(www);
                texture.filterMode = FilterMode.Point;
            }

            if (texture != null)
            {
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height);
                Plugin.ImageCache[uri] = sprite;

                UpdateWithSprite(sprite);
            }
        }

        private void UpdateWithSprite(Sprite sprite)
        {
            try
            {
                SpriteRenderer spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;

                if (!(gameObject.GetComponent<PolygonCollider2D>() && gameObject.GetComponent<SFPolygon>())) return; // Return when the object has no collision

                Destroy(gameObject.GetComponent<PolygonCollider2D>());
                Destroy(gameObject.GetComponent<SFPolygon>());

                PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
                SFPolygon polygon = gameObject.AddComponent<SFPolygon>();

                polygon.pathCount = collider.pathCount;
                for (int i = 0; i < collider.pathCount; i++)
                {
                    polygon.SetPath(i, collider.GetPath(i));
                }
            }
            catch (Exception e)
            {
                // Ignore
            }
        }
    }
}