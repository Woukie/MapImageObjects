using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System;

namespace MapImageObjects.Core;

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
        LoadImage();
    }

    public async void LoadImage()
    {
        Texture2D texture = null;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        try
        {
            texture = await GetRemoteTexture(uri);
        }
        catch
        {
            Console.WriteLine("Failed to load image!");
        }

        if (texture == null)
        {
            texture = Texture2D.whiteTexture;
        }

        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height);
        UpdateCollision();
    }

    public void UpdateCollision()
    {
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        Destroy(gameObject.GetComponent<SFPolygon>());

        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        SFPolygon polygon = gameObject.AddComponent<SFPolygon>();

        for (int i = 0; i < collider.pathCount; i++)
        { // Will always be higher
            polygon.SetPath(i, collider.GetPath(i));
        }
    }

    // Yoink
    // https://stackoverflow.com/a/53770838
    // TODO: Timeout
    private static async Task<Texture2D> GetRemoteTexture(string url, int timeoutSeconds = 30)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var asyncOp = www.SendWebRequest();

            Task request = WaitForWebRequestCompletion(www, cancellationTokenSource.Token);
            Task timeout = Task.Delay(TimeSpan.FromSeconds(timeoutSeconds));
            Task completedTask = await Task.WhenAny(request, timeout);

            if (completedTask == request)
            {
                cancellationTokenSource.Cancel();
                return www.isNetworkError || www.isHttpError ? null : DownloadHandlerTexture.GetContent(www);
            }
            else
            {
                throw new TimeoutException("The operation timed out.");
            }
        }
    }

    private static async Task WaitForWebRequestCompletion(UnityWebRequest www, CancellationToken cancellationToken)
    {
        while (!www.isDone)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Delay(1000 / 30);
        }
    }
}
