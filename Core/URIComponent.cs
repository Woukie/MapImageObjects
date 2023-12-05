﻿using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Threading;
using System;

namespace MapImageObjects.Core;

public class URIComponent : MonoBehaviour
{
    private string uri; // epic pic: https://raw.githubusercontent.com/Woukie/Image/main/cover%20transparent.png
    private CancellationTokenSource tokenSource = new CancellationTokenSource(); // Used to cancel loading images

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
        Texture2D texture = null;

        // Cancel and reset the token
        tokenSource.Cancel();
        tokenSource.Dispose();
        tokenSource = new CancellationTokenSource();

        CancellationToken token = tokenSource.Token;

        Task task = Task.Run(() => {
            Console.WriteLine("Running image task...");

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
            }
        }, tokenSource.Token);

        try {
            await task;
            UpdateWithTexture(texture);

        } catch (OperationCanceledException) {
            // Ignore, cancellations expected
        }
    }

    // Updates sprite, collision and SFPolygon
    private void UpdateWithTexture(Texture2D texture)
    {
        if (texture == null) return;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height);

        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        Destroy(gameObject.GetComponent<SFPolygon>());

        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        SFPolygon polygon = gameObject.AddComponent<SFPolygon>();

        for (int i = 0; i < collider.pathCount; i++)
        { // collider path count will always be higher than polygon path count
            polygon.SetPath(i, collider.GetPath(i));
        }
    }
}