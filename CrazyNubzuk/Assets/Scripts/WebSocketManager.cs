using UnityEngine;
using NativeWebSocket;
using System;
using System.Collections.Generic;

public class WebSocketManager : MonoBehaviour
{
    public static WebSocketManager Instance;

    private WebSocket websocket;

    // type -> handler(json payload)
    private Dictionary<string, Action<string>> handlers = new();

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            string url = "ws://localhost:3000"; // ⚠️ 너 서버 주소로 바꿔야 함
            websocket = new WebSocket(url);

            websocket.OnOpen += () =>
            {
                Debug.Log("WebSocket 연결됨");
            };

            websocket.OnError += (e) =>
            {
                Debug.LogError("WebSocket 에러: " + e);
            };

            websocket.OnClose += (e) =>
            {
                Debug.Log("WebSocket 연결 종료됨");
            };

            websocket.OnMessage += (bytes) =>
            {
                string msg = System.Text.Encoding.UTF8.GetString(bytes);
                Debug.Log("수신 메시지: " + msg);
                HandleMessage(msg);
            };

            await websocket.Connect();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket?.DispatchMessageQueue(); // WebGL에서는 필수
#endif
    }

    public async void Send(object obj)
    {
        string json = JsonUtility.ToJson(obj);
        Debug.Log("전송 메시지: " + json);
        await websocket.SendText(json);
    }

    private void HandleMessage(string json)
    {
        MessageWrapper wrapper = JsonUtility.FromJson<MessageWrapper>(json);

        if (handlers.TryGetValue(wrapper.type, out var callback))
        {
            string payloadJson = JsonUtility.ToJson(wrapper.payload);
            callback?.Invoke(payloadJson);
        }
        else
        {
            Debug.LogWarning($"핸들러 없음: {wrapper.type}");
        }
    }

    public void RegisterHandler(string type, Action<string> callback)
    {
        handlers[type] = callback;
    }

    public void UnregisterHandler(string type)
    {
        if (handlers.ContainsKey(type))
            handlers.Remove(type);
    }

    [Serializable]
    private class MessageWrapper
    {
        public string type;
        public PayloadWrapper payload;

        [Serializable]
        public class PayloadWrapper { }
    }
}
