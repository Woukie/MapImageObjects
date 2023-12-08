﻿using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Threading;
using System;
using UnboundLib;

namespace MapImageObjects.Core;

public class URIComponent : MonoBehaviour
{
    private string uri; // epic pic: https://raw.githubusercontent.com/Woukie/Image/main/cover%20transparent.png
    private CancellationTokenSource tokenSource = new CancellationTokenSource(); // Used to cancel loading images

    // Stop loading any images if the image is destoryed
    public void OnDestroy()
    {
        tokenSource.Cancel();
        tokenSource.Dispose();
    }

    public string GetURI()
    {
        return uri;
    }

    public void SetURI(string uri)
    {
        this.uri = uri;

        LoadImage();
    }

    public async void LoadImage()
    {
        // Cancel and reset the token
        tokenSource.Cancel();
        tokenSource.Dispose();
        tokenSource = new CancellationTokenSource();

        Sprite sprite = null;
        if (Plugin.ImageCache.TryGetValue(uri, out sprite))
        {
            UpdateWithSprite(sprite);
            return;
        }

        Texture2D texture = null;

        CancellationToken token = tokenSource.Token;

        Task task = Task.Run(() => {
            token.ThrowIfCancellationRequested();

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(GetURI())) {
                UnityWebRequestAsyncOperation asyncOp = www.SendWebRequest();

                while (!asyncOp.isDone) {
                    // TODO: Potentially implement timeout feature here
                    // Check for cancellations, otherwise do nothing while loading

                    if (token.IsCancellationRequested) {
                        // TODO: Set sprite to nothing, or leave (will only run if new image is loading (eg user changes url in the input box))
                        token.ThrowIfCancellationRequested();
                    }
                };

                texture = www.isNetworkError || www.isHttpError ? Texture2D.whiteTexture : DownloadHandlerTexture.GetContent(www);
                texture.filterMode = FilterMode.Point;
            }
        }, tokenSource.Token);

        try {
            await task;

            if (texture != null)
            {
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height);

                // Only cache working URIs
                if (texture != Texture2D.whiteTexture) Plugin.ImageCache.Add(uri, sprite);

                UpdateWithSprite(sprite);
            }

        } catch (OperationCanceledException) {
            // Ignore, cancellations expected
        }
    }

    // Updates sprite, collision and SFPolygon
    // I have no idea what keeps causing this to crash, so I surrounded with try catch as a temp solution
    private void UpdateWithSprite(Sprite sprite)
    {
        try
        {
            SpriteRenderer spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;

            if (!(gameObject.GetComponent<PolygonCollider2D>() && gameObject.GetComponent<SFPolygon>())) return; // Return when object has no collision

            Destroy(gameObject.GetComponent<PolygonCollider2D>());
            Destroy(gameObject.GetComponent<SFPolygon>());

            PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
            SFPolygon polygon = gameObject.AddComponent<SFPolygon>();

            polygon.pathCount = collider.pathCount;
            for (int i = 0; i < collider.pathCount; i++)
            {
                polygon.SetPath(i, collider.GetPath(i));
            }
        } catch (Exception e)
        {
            throw e;
        }
    }
}
