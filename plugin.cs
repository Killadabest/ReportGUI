using BepInEx; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using System; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using System.Collections; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using System.Collections.Generic; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using UnityEngine; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using UnityEngine.Networking; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using UnityEngine.UI; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using UnityEngine.EventSystems; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using GorillaLocomotion; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using Photon.Pun; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
using Photon.Realtime; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

namespace ReportGUI.Killa // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
{ // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    [BepInPlugin("Killa.MODS.ReportGUI", "ReportGUI", "1.0.0")] // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    public class ReportGUIPlugin : BaseUnityPlugin // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private const string API_URL = "https://paste-api-sync.lovable.app/api/public/ad0fa8c209"; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private const string STEAM_LOGO_URL = "https://raw.githubusercontent.com/Killadabest/ImGuiAssets/main/steamlogo.png"; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private const string GREEN_LOGO_URL = "https://raw.githubusercontent.com/Killadabest/ImGuiAssets/main/smileyfacegreen.png"; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private const string ORANGE_LOGO_URL = "https://raw.githubusercontent.com/Killadabest/ImGuiAssets/main/orangeyellowwarning.png"; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private const string RED_LOGO_URL = "https://raw.githubusercontent.com/Killadabest/ImGuiAssets/main/redalertlogo.png"; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private const string CLICK_AUDIO_URL = "https://raw.githubusercontent.com/Killadabest/ImGuiAssets/main/ui_click.txt.mp3"; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private const string NOTIF_AUDIO_URL = "https://raw.githubusercontent.com/Killadabest/ImGuiAssets/main/ui_notifaction.txt.mp3"; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private string apiStatus = "Loading..."; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private GameObject canvasObj; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private GameObject loadingScreenObj; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private GameObject mainPanelObj; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private Text titleText; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private Text fpsText; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private Text modalTitleText; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private Image statusIconImg; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private Transform playerListContent; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private GameObject scrollViewObj; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private GameObject maintenancePanel; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private Text maintenanceText; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private GameObject reasonModal; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private NetPlayer selectedTargetPlayer; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private RectTransform panelRect; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private Outline panelOutline; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private static AudioSource audioSource; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private static Sprite cachedSteamSprite = null; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private static Sprite cachedGreenSprite = null; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private static Sprite cachedOrangeSprite = null; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private static Sprite cachedRedSprite = null; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private static AudioClip cachedClickClip = null; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        private static AudioClip cachedNotifClip = null; // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void Start() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            CreateUI(); // Killa builds the entire user interface and loading screen hierarchy :(
            StartCoroutine(InitializeModRoutine()); // Killa starts initialization routine to fetch assets, API status, and then reveal menu :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private IEnumerator InitializeModRoutine() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            // Killa keeps full black loading screen active while fetching everything :(
            yield return StartCoroutine(DownloadSprites()); // Killa downloads all required logos and icons from GitHub repositories :(
            yield return StartCoroutine(DownloadAudio()); // Killa downloads UI click and notification audio clips :(
            yield return StartCoroutine(PollAPIStatusOnce()); // Killa fetches initial API status from the server endpoint :(

            // Killa finishes loading assets and hides black screen to reveal menu :(
            if (loadingScreenObj != null) loadingScreenObj.SetActive(false); // Killa deactivates black loading screen object :(
            if (mainPanelObj != null) mainPanelObj.SetActive(true); // Killa activates main GUI panel object :(

            // Killa starts background loops for continuous operations :(
            StartCoroutine(PollAPIStatusLoop()); // Killa starts continuous API polling loop :(
            StartCoroutine(UpdatePlayerListLoop()); // Killa starts continuous player room list refresh loop :(
            StartCoroutine(UpdateFpsLoop()); // Killa starts FPS counter update loop every 0.7 seconds :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private IEnumerator DownloadSprites() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            if (cachedSteamSprite == null) // Killa checks if steam logo sprite is already cached :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(STEAM_LOGO_URL)) // Killa creates web request for steam logo :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    www.certificateHandler = new BypassCertificate(); // Killa attaches certificate bypass handler :(
                    yield return www.SendWebRequest(); // Killa waits for web request to complete :(
                    if (www.result == UnityWebRequest.Result.Success) // Killa checks if download was successful :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        Texture2D tex = DownloadHandlerTexture.GetContent(www); // Killa extracts texture2d from download handler :(
                        cachedSteamSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)); // Killa creates sprite from texture :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            if (cachedGreenSprite == null) // Killa checks if green status smiley sprite is cached :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(GREEN_LOGO_URL)) // Killa creates web request for green status logo :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    www.certificateHandler = new BypassCertificate(); // Killa attaches certificate bypass handler :(
                    yield return www.SendWebRequest(); // Killa waits for request completion :(
                    if (www.result == UnityWebRequest.Result.Success) // Killa checks for success :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        Texture2D tex = DownloadHandlerTexture.GetContent(www); // Killa gets content texture :(
                        cachedGreenSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)); // Killa creates sprite :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            if (cachedOrangeSprite == null) // Killa checks if orange warning sprite is cached :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(ORANGE_LOGO_URL)) // Killa creates web request for orange warning logo :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    www.certificateHandler = new BypassCertificate(); // Killa attaches certificate bypass handler :(
                    yield return www.SendWebRequest(); // Killa waits for request completion :(
                    if (www.result == UnityWebRequest.Result.Success) // Killa checks for success :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        Texture2D tex = DownloadHandlerTexture.GetContent(www); // Killa gets content texture :(
                        cachedOrangeSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)); // Killa creates sprite :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            if (cachedRedSprite == null) // Killa checks if red alert sprite is cached :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(RED_LOGO_URL)) // Killa creates web request for red alert logo :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    www.certificateHandler = new BypassCertificate(); // Killa attaches certificate bypass handler :(
                    yield return www.SendWebRequest(); // Killa waits for request completion :(
                    if (www.result == UnityWebRequest.Result.Success) // Killa checks for success :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        Texture2D tex = DownloadHandlerTexture.GetContent(www); // Killa gets content texture :(
                        cachedRedSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f)); // Killa creates sprite :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            UpdateStatusIcon(); // Killa updates status icon image display :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private IEnumerator DownloadAudio() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            if (cachedClickClip == null) // Killa checks if click audio clip is cached :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(CLICK_AUDIO_URL, AudioType.MPEG)) // Killa requests click audio :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    www.certificateHandler = new BypassCertificate(); // Killa bypasses certificate check :(
                    yield return www.SendWebRequest(); // Killa waits for request completion :(
                    if (www.result == UnityWebRequest.Result.Success) // Killa checks for success :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        cachedClickClip = DownloadHandlerAudioClip.GetContent(www); // Killa extracts audio clip :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            if (cachedNotifClip == null) // Killa checks if notification audio clip is cached :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(NOTIF_AUDIO_URL, AudioType.MPEG)) // Killa requests notification audio :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    www.certificateHandler = new BypassCertificate(); // Killa bypasses certificate check :(
                    yield return www.SendWebRequest(); // Killa waits for request completion :(
                    if (www.result == UnityWebRequest.Result.Success) // Killa checks for success :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        cachedNotifClip = DownloadHandlerAudioClip.GetContent(www); // Killa extracts audio clip :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void PlayClickSound() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            if (audioSource != null && cachedClickClip != null) // Killa checks audio source and clip :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                audioSource.PlayOneShot(cachedClickClip); // Killa plays click sound effect once :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void PlayNotifSound() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            if (audioSource != null && cachedNotifClip != null) // Killa checks audio source and clip :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                audioSource.PlayOneShot(cachedNotifClip); // Killa plays notification sound effect once :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private IEnumerator PollAPIStatusOnce() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            using (UnityWebRequest www = UnityWebRequest.Get(API_URL)) // Killa sends GET request to API URL :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                www.certificateHandler = new BypassCertificate(); // Killa bypasses SSL certificate check :(
                www.timeout = 5; // Killa sets request timeout limit :(
                yield return www.SendWebRequest(); // Killa waits for response :(
                if (www.result == UnityWebRequest.Result.Success) // Killa checks if request succeeded :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    apiStatus = www.downloadHandler.text.Trim(); // Killa reads API status text :(
                    UpdateUIState(apiStatus); // Killa updates UI appearance based on status :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                else // Killa handles request failure :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    apiStatus = "🔴 Offline"; // Killa defaults to offline red state :(
                    UpdateUIState(apiStatus); // Killa updates UI appearance :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private IEnumerator PollAPIStatusLoop() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            while (true) // Killa runs infinite loop for real-time syncing :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                using (UnityWebRequest www = UnityWebRequest.Get(API_URL)) // Killa requests API status :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    www.certificateHandler = new BypassCertificate(); // Killa bypasses certificate check :(
                    www.timeout = 5; // Killa sets timeout :(
                    yield return www.SendWebRequest(); // Killa waits for request :(
                    if (www.result == UnityWebRequest.Result.Success) // Killa checks success :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        apiStatus = www.downloadHandler.text.Trim(); // Killa parses status string :(
                        UpdateUIState(apiStatus); // Killa updates UI state :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    else // Killa handles offline condition :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        apiStatus = "🔴 Offline"; // Killa sets red offline status :(
                        UpdateUIState(apiStatus); // Killa updates UI state :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                yield return new WaitForSeconds(3f); // Killa waits 3 seconds before next poll :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void CreateUI() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            canvasObj = new GameObject("ReportGUICanvas"); // Killa creates root canvas game object :(
            Canvas canvas = canvasObj.AddComponent<Canvas>(); // Killa adds Canvas component :(
            canvas.renderMode = RenderMode.ScreenSpaceOverlay; // Killa sets render mode to overlay :(
            canvas.sortingOrder = 999; // Killa sets high sorting order so it renders on top :(

            CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>(); // Killa adds CanvasScaler component :(
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; // Killa scales with screen size :(
            scaler.referenceResolution = new Vector2(1920, 1080); // Killa sets reference resolution :(

            canvasObj.AddComponent<GraphicRaycaster>(); // Killa adds graphic raycaster for UI interaction :(
            audioSource = canvasObj.AddComponent<AudioSource>(); // Killa adds AudioSource component for UI sound effects :(
            DontDestroyOnLoad(canvasObj); // Killa prevents canvas from being destroyed on scene loads :(

            // Killa creates full-black loading screen panel shown upon startup :(
            loadingScreenObj = new GameObject("LoadingScreenPanel"); // Killa creates loading screen game object :(
            loadingScreenObj.transform.SetParent(canvasObj.transform, false); // Killa parents to canvas :(
            RectTransform loadRect = loadingScreenObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            loadRect.anchorMin = Vector2.zero; // Killa stretches min anchor across screen :(
            loadRect.anchorMax = Vector2.one; // Killa stretches max anchor across screen :(
            loadRect.sizeDelta = Vector2.zero; // Killa clears size delta :(
            Image loadImg = loadingScreenObj.AddComponent<Image>(); // Killa adds image component :(
            loadImg.color = Color.black; // Killa sets color to solid black :(
            loadImg.raycastTarget = true; // Killa blocks raycasts during loading :(

            GameObject loadTextObj = new GameObject("LoadingText"); // Killa creates loading text object :(
            loadTextObj.transform.SetParent(loadingScreenObj.transform, false); // Killa parents to loading screen :(
            RectTransform loadTextRect = loadTextObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            loadTextRect.anchorMin = new Vector2(0.3f, 0.4f); // Killa anchors text in center :(
            loadTextRect.anchorMax = new Vector2(0.7f, 0.6f); // Killa anchors text in center :(
            loadTextRect.sizeDelta = Vector2.zero; // Killa clears size delta :(
            Text loadTxt = loadTextObj.AddComponent<Text>(); // Killa adds text component :(
            loadTxt.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            loadTxt.fontSize = 24; // Killa sets font size :(
            loadTxt.alignment = TextAnchor.MiddleCenter; // Killa centers text alignment :(
            loadTxt.color = Color.white; // Killa sets color to white :(
            loadTxt.text = "Loading...\nFetching assets & API status"; // Killa sets loading message :(

            // Killa creates main GUI panel container (hidden until loading completes) :(
            mainPanelObj = new GameObject("BackgroundPanel"); // Killa creates main panel object :(
            mainPanelObj.transform.SetParent(canvasObj.transform, false); // Killa parents to canvas :(
            panelRect = mainPanelObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            panelRect.anchorMin = new Vector2(0.5f, 0.5f); // Killa anchors to center :(
            panelRect.anchorMax = new Vector2(0.5f, 0.5f); // Killa anchors to center :(
            panelRect.sizeDelta = new Vector2(380, 420); // Killa sets panel dimensions :(
            panelRect.anchoredPosition = Vector2.zero; // Killa centers panel position :(

            Image panelImg = mainPanelObj.AddComponent<Image>(); // Killa adds panel background image :(
            panelImg.color = Color.black; // Killa sets background color to black :(
            panelImg.sprite = CreateRoundedSprite(256, 256, 16); // Killa generates a high-res smoothed rounded sprite :(
            panelImg.type = Image.Type.Sliced; // Killa sets image type to sliced :(
            panelImg.raycastTarget = true; // Killa enables raycast target :(

            panelOutline = mainPanelObj.AddComponent<Outline>(); // Killa adds outline component for menu stroke :(
            panelOutline.effectColor = Color.green; // Killa sets default outline color to green :(
            panelOutline.effectDistance = new Vector2(2, -2); // Killa sets outline thickness :(

            ReportUIDrag dragger = mainPanelObj.AddComponent<ReportUIDrag>(); // Killa adds drag handler component :(
            dragger.rectTransform = panelRect; // Killa assigns rect transform to dragger :(

            GameObject titleObj = new GameObject("TitleText"); // Killa creates title text object :(
            titleObj.transform.SetParent(mainPanelObj.transform, false); // Killa parents to main panel :(
            RectTransform titleRect = titleObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            titleRect.anchorMin = new Vector2(0, 1); // Killa anchors to top :(
            titleRect.anchorMax = new Vector2(1, 1); // Killa anchors to top :(
            titleRect.anchoredPosition = new Vector2(0, -22); // Killa positions title :(
            titleRect.sizeDelta = new Vector2(0, 30); // Killa sets size delta :(
            titleText = titleObj.AddComponent<Text>(); // Killa adds text component :(
            titleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            titleText.fontSize = 15; // Killa sets font size :(
            titleText.alignment = TextAnchor.MiddleCenter; // Killa centers alignment :(
            titleText.color = Color.white; // Killa sets color to white :(
            titleText.text = "ReportGUI"; // Killa sets title text :(
            titleText.raycastTarget = false; // Killa disables raycast target :(

            GameObject statusIconObj = new GameObject("StatusIcon"); // Killa creates status icon game object :(
            statusIconObj.transform.SetParent(titleObj.transform, false); // Killa parents to title object :(
            RectTransform iconRect = statusIconObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            iconRect.anchorMin = new Vector2(1f, 0.5f); // Killa anchors to right side :(
            iconRect.anchorMax = new Vector2(1f, 0.5f); // Killa anchors to right side :(
            iconRect.anchoredPosition = new Vector2(-25f, 0f); // Killa positions icon :(
            iconRect.sizeDelta = new Vector2(22, 22); // Killa sets size delta :(
            statusIconImg = statusIconObj.AddComponent<Image>(); // Killa adds image component :(
            statusIconImg.preserveAspect = true; // Killa preserves aspect ratio :(
            statusIconImg.raycastTarget = false; // Killa disables raycast target :(

            // Killa creates FPS counter text in the top-left corner of the GUI panel :(
            GameObject fpsObj = new GameObject("FPSText"); // Killa creates FPS text object :(
            fpsObj.transform.SetParent(mainPanelObj.transform, false); // Killa parents to main panel :(
            RectTransform fpsRect = fpsObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            fpsRect.anchorMin = new Vector2(0, 1); // Killa anchors to top-left :(
            fpsRect.anchorMax = new Vector2(0, 1); // Killa anchors to top-left :(
            fpsRect.anchoredPosition = new Vector2(55, -22); // Killa positions FPS counter :(
            fpsRect.sizeDelta = new Vector2(100, 30); // Killa sets size delta :(
            fpsText = fpsObj.AddComponent<Text>(); // Killa adds text component :(
            fpsText.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            fpsText.fontSize = 12; // Killa sets font size :(
            fpsText.alignment = TextAnchor.MiddleLeft; // Killa aligns text to the left :(
            fpsText.supportRichText = true; // Killa enables rich text so FPS number can be colored independently :(
            fpsText.color = Color.white; // Killa sets default color :(
            fpsText.text = "FPS: <color=#00FF00>60</color>"; // Killa sets initial FPS text :(
            fpsText.raycastTarget = false; // Killa disables raycast target :(

            maintenancePanel = new GameObject("MaintenancePanel"); // Killa creates maintenance panel object :(
            maintenancePanel.transform.SetParent(mainPanelObj.transform, false); // Killa parents to main panel :(
            RectTransform maintRect = maintenancePanel.AddComponent<RectTransform>(); // Killa adds rect transform :(
            maintRect.anchorMin = new Vector2(0.05f, 0.05f); // Killa sets min anchor :(
            maintRect.anchorMax = new Vector2(0.95f, 0.82f); // Killa sets max anchor :(
            maintRect.sizeDelta = Vector2.zero; // Killa clears size delta :(

            GameObject maintTextObj = new GameObject("MaintText"); // Killa creates maintenance text object :(
            maintTextObj.transform.SetParent(maintenancePanel.transform, false); // Killa parents to maintenance panel :(
            RectTransform maintTextRect = maintTextObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            maintTextRect.anchorMin = Vector2.zero; // Killa sets min anchor :(
            maintTextRect.anchorMax = Vector2.one; // Killa sets max anchor :(
            maintenanceText = maintTextObj.AddComponent<Text>(); // Killa adds text component :(
            maintenanceText.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            maintenanceText.fontSize = 13; // Killa sets font size :(
            maintenanceText.alignment = TextAnchor.MiddleCenter; // Killa centers alignment :(
            maintenanceText.color = new Color(1f, 0.3f, 0.3f); // Killa sets color to light red :(
            maintenanceText.horizontalOverflow = HorizontalWrapMode.Wrap; // Killa enables text wrapping :(
            maintenanceText.verticalOverflow = VerticalWrapMode.Truncate; // Killa truncates vertical overflow :(
            maintenanceText.raycastTarget = false; // Killa disables raycast target :(
            maintenanceText.text = "⚠️ [CRITICAL ALERT] ReportGUI API is currently offline and disconnected.\n\nReal-time sync has been halted for security. Please contact @.playfab. on Discord for status updates and fix ETAs!"; // Killa sets maintenance message :(

            maintenancePanel.SetActive(false); // Killa deactivates maintenance panel by default :(

            scrollViewObj = new GameObject("PlayerScrollView"); // Killa creates scroll view object for player list :(
            scrollViewObj.transform.SetParent(mainPanelObj.transform, false); // Killa parents to main panel :(
            RectTransform scrollRect = scrollViewObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            scrollRect.anchorMin = new Vector2(0.04f, 0.04f); // Killa sets min anchor :(
            scrollRect.anchorMax = new Vector2(0.96f, 0.82f); // Killa sets max anchor :(
            scrollRect.sizeDelta = Vector2.zero; // Killa clears size delta :(

            GameObject viewport = new GameObject("Viewport"); // Killa creates viewport object :(
            viewport.transform.SetParent(scrollViewObj.transform, false); // Killa parents to scroll view :(
            RectTransform vpRect = viewport.AddComponent<RectTransform>(); // Killa adds rect transform :(
            vpRect.anchorMin = Vector2.zero; // Killa sets min anchor :(
            vpRect.anchorMax = Vector2.one; // Killa sets max anchor :(
            vpRect.sizeDelta = Vector2.zero; // Killa clears size delta :(
            viewport.AddComponent<RectMask2D>(); // Killa adds rect mask 2d for clipping :(
            Image vpImg = viewport.AddComponent<Image>(); // Killa adds image component :(
            vpImg.color = Color.clear; // Killa sets color to clear :(
            vpImg.raycastTarget = false; // Killa disables raycast target :(

            GameObject content = new GameObject("Content"); // Killa creates content container object :(
            content.transform.SetParent(viewport.transform, false); // Killa parents to viewport :(
            RectTransform contentRect = content.AddComponent<RectTransform>(); // Killa adds rect transform :(
            contentRect.anchorMin = new Vector2(0, 1); // Killa anchors top :(
            contentRect.anchorMax = new Vector2(1, 1); // Killa anchors top :(
            contentRect.pivot = new Vector2(0.5f, 1f); // Killa sets pivot :(
            contentRect.sizeDelta = Vector2.zero; // Killa clears size delta :(

            VerticalLayoutGroup vlg = content.AddComponent<VerticalLayoutGroup>(); // Killa adds vertical layout group :(
            vlg.spacing = 6; // Killa sets spacing between rows :(
            vlg.childControlHeight = true; // Killa controls child heights :(
            vlg.childControlWidth = true; // Killa controls child widths :(
            vlg.childForceExpandHeight = false; // Killa prevents forced height expansion :(
            vlg.childForceExpandWidth = true; // Killa forces width expansion :(

            ContentSizeFitter csf = content.AddComponent<ContentSizeFitter>(); // Killa adds content size fitter :(
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize; // Killa fits vertical size to preferred size :(

            playerListContent = content.transform; // Killa assigns player list content transform reference :(

            CreateReasonModal(mainPanelObj.transform); // Killa creates reason selection modal window :(
            mainPanelObj.SetActive(false); // Killa hides main panel initially until loading screen completes :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        // Killa generates a high-res smoothed anti-aliased rounded sprite to eliminate pixelation :(
        private Sprite CreateRoundedSprite(int width, int height, int radius) // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false); // Killa creates high-res RGBA texture :(
            tex.filterMode = FilterMode.Bilinear; // Killa enables bilinear filtering for ultra-smooth edges :(
            Color[] colors = new Color[width * height]; // Killa creates color array buffer :(

            for (int y = 0; y < height; y++) // Killa loops through height pixels :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                for (int x = 0; x < width; x++) // Killa loops through width pixels :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    float dist = 0f; // Killa initializes corner distance variable :(
                    if (x < radius && y < radius) // Killa checks bottom-left corner :(
                        dist = new Vector2(radius - x, radius - y).magnitude; // Killa calculates distance from bottom-left corner center :(
                    else if (x >= width - radius && y < radius) // Killa checks bottom-right corner :(
                        dist = new Vector2(x - (width - radius - 1), radius - y).magnitude; // Killa calculates distance from bottom-right corner center :(
                    else if (x < radius && y >= height - radius) // Killa checks top-left corner :(
                        dist = new Vector2(radius - x, y - (height - radius - 1)).magnitude; // Killa calculates distance from top-left corner center :(
                    else if (x >= width - radius && y >= height - radius) // Killa checks top-right corner :(
                        dist = new Vector2(x - (width - radius - 1), y - (height - radius - 1)).magnitude; // Killa calculates distance from top-right corner center :(
                    else // Killa handles inner body pixels :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        colors[y * width + x] = Color.white; // Killa sets inner pixel to full white :(
                        continue; // Killa skips to next pixel :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

                    if (dist > radius) // Killa checks if pixel is outside rounded corner radius :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        colors[y * width + x] = Color.clear; // Killa sets outside pixel to transparent :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    else // Killa handles anti-aliased smoothing along corner edge :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        float alpha = Mathf.Clamp01((radius - dist) + 0.5f); // Killa calculates smooth alpha transition :(
                        colors[y * width + x] = new Color(1f, 1f, 1f, alpha); // Killa applies anti-aliased alpha value :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            tex.SetPixels(colors); // Killa applies color array to texture :(
            tex.Apply(); // Killa uploads texture changes to GPU :(
            return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.FullRect, new Vector4(radius, radius, radius, radius)); // Killa returns smoothed sprite :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void CreateReasonModal(Transform parent) // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            reasonModal = new GameObject("ReasonModal"); // Killa creates reason modal game object :(
            reasonModal.transform.SetParent(parent, false); // Killa parents to parent transform :(
            RectTransform modalRect = reasonModal.AddComponent<RectTransform>(); // Killa adds rect transform :(
            modalRect.anchorMin = new Vector2(0.08f, 0.12f); // Killa sets min anchor :(
            modalRect.anchorMax = new Vector2(0.92f, 0.88f); // Killa sets max anchor :(
            modalRect.sizeDelta = Vector2.zero; // Killa clears size delta :(

            Image modalImg = reasonModal.AddComponent<Image>(); // Killa adds image component :(
            modalImg.color = new Color(0.14f, 0.14f, 0.18f, 0.98f); // Killa sets modal background color :(
            modalImg.sprite = CreateRoundedSprite(256, 256, 12); // Killa creates smoothed rounded sprite for modal :(
            modalImg.type = Image.Type.Sliced; // Killa sets image type to sliced :(

            Outline modalOutline = reasonModal.AddComponent<Outline>(); // Killa adds outline component :(
            modalOutline.effectColor = new Color(1f, 0.3f, 0.3f, 0.8f); // Killa sets outline color :(
            modalOutline.effectDistance = new Vector2(2, -2); // Killa sets outline distance :(

            reasonModal.SetActive(false); // Killa hides modal by default :(

            GameObject titleObj = new GameObject("ModalTitle"); // Killa creates modal title object :(
            titleObj.transform.SetParent(reasonModal.transform, false); // Killa parents to modal :(
            RectTransform titleRect = titleObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            titleRect.anchorMin = new Vector2(0, 0.75f); // Killa sets min anchor :(
            titleRect.anchorMax = new Vector2(1, 1); // Killa sets max anchor :(
            modalTitleText = titleObj.AddComponent<Text>(); // Killa adds text component and stores reference :(
            modalTitleText.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            modalTitleText.fontSize = 15; // Killa sets font size :(
            modalTitleText.alignment = TextAnchor.MiddleCenter; // Killa centers alignment :(
            modalTitleText.color = Color.white; // Killa sets color to white :(
            modalTitleText.text = "Select Report Reason"; // Killa sets default title text :(
            modalTitleText.raycastTarget = false; // Killa disables raycast target :(

            // Killa assigns notification sound effect to reason buttons (Toxicity, Hate Speech, Cheating) :(
            CreateButton(reasonModal.transform, "Toxicity", new Vector2(0, 35), () => { PlayNotifSound(); SubmitReport("Toxicity"); }, new Color(0.25f, 0.25f, 0.35f)); // Killa creates Toxicity report button :(
            CreateButton(reasonModal.transform, "Hate Speech", new Vector2(0, -10), () => { PlayNotifSound(); SubmitReport("Hate Speech"); }, new Color(0.25f, 0.25f, 0.35f)); // Killa creates Hate Speech report button :(
            CreateButton(reasonModal.transform, "Cheating", new Vector2(0, -55), () => { PlayNotifSound(); SubmitReport("Cheating"); }, new Color(0.25f, 0.25f, 0.35f)); // Killa creates Cheating report button :(
            // Killa assigns click sound effect to Cancel button :(
            CreateButton(reasonModal.transform, "Cancel", new Vector2(0, -100), () => { PlayClickSound(); CloseReasonModal(); }, new Color(0.7f, 0.25f, 0.25f)); // Killa creates Cancel button :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void CloseReasonModal() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            reasonModal.SetActive(false); // Killa deactivates reason modal :(
            if (scrollViewObj != null) scrollViewObj.SetActive(true); // Killa restores player list scroll view :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void CreateButton(Transform parent, string label, Vector2 anchoredPos, UnityEngine.Events.UnityAction action, Color baseColor) // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            GameObject btnObj = new GameObject(label + "Btn"); // Killa creates button game object :(
            btnObj.transform.SetParent(parent, false); // Killa parents to parent transform :(
            RectTransform rect = btnObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            rect.anchorMin = new Vector2(0.5f, 0.5f); // Killa centers anchor min :(
            rect.anchorMax = new Vector2(0.5f, 0.5f); // Killa centers anchor max :(
            rect.anchoredPosition = anchoredPos; // Killa sets anchored position :(
            rect.sizeDelta = new Vector2(230, 36); // Killa sets button size :(

            Image img = btnObj.AddComponent<Image>(); // Killa adds image component :(
            img.color = baseColor; // Killa sets base image color :(
            img.sprite = CreateRoundedSprite(256, 256, 10); // Killa creates smoothed rounded sprite for button :(
            img.type = Image.Type.Sliced; // Killa sets image type to sliced :(
            img.raycastTarget = true; // Killa enables raycast target :(

            Outline btnOutline = btnObj.AddComponent<Outline>(); // Killa adds outline component :(
            btnOutline.effectColor = new Color(1f, 1f, 1f, 0.3f); // Killa sets outline color :(
            btnOutline.effectDistance = new Vector2(1, -1); // Killa sets outline distance :(

            Button btn = btnObj.AddComponent<Button>(); // Killa adds Button component :(
            btn.targetGraphic = img; // Killa assigns target graphic :(

            ColorBlock cb = btn.colors; // Killa gets color block :(
            cb.normalColor = baseColor; // Killa sets normal color :(
            cb.highlightedColor = new Color(baseColor.r * 0.4f, baseColor.g * 0.4f, baseColor.b * 0.4f, baseColor.a); // Killa sets highlighted color :(
            cb.pressedColor = new Color(baseColor.r * 0.3f, baseColor.g * 0.3f, baseColor.b * 0.3f, baseColor.a); // Killa sets pressed color :(
            cb.selectedColor = baseColor; // Killa sets selected color :(
            btn.colors = cb; // Killa applies color block :(

            btn.onClick.AddListener(action); // Killa adds click listener action :(

            GameObject textObj = new GameObject("Text"); // Killa creates button text object :(
            textObj.transform.SetParent(btnObj.transform, false); // Killa parents to button :(
            RectTransform textRect = textObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            textRect.anchorMin = Vector2.zero; // Killa sets min anchor :(
            textRect.anchorMax = Vector2.one; // Killa sets max anchor :(
            Text txt = textObj.AddComponent<Text>(); // Killa adds text component :(
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            txt.fontSize = 13; // Killa sets font size :(
            txt.alignment = TextAnchor.MiddleCenter; // Killa centers text alignment :(
            txt.color = Color.white; // Killa sets text color to white :(
            txt.text = label; // Killa sets button label text :(
            txt.raycastTarget = false; // Killa disables raycast target :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        // Killa defines UpdateUIState to handle status string parsing, title updates, and outline/maintenance panel states :(
        private void UpdateUIState(string status) // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            apiStatus = status; // Killa stores current api status string :(
            if (titleText != null) // Killa checks if title text exists :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                titleText.text = "ReportGUI"; // Killa sets title text :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            UpdateStatusIcon(); // Killa updates status icon image and sprite :(

            if (panelOutline != null) // Killa checks if main panel outline exists :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                if (status.Contains("🟢")) // Killa checks if status is green :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    panelOutline.effectColor = Color.green; // Killa sets menu stroke outline to green :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                else if (status.Contains("🟠")) // Killa checks if status is orange/warning :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    panelOutline.effectColor = new Color(1f, 0.5f, 0f); // Killa sets menu stroke outline to orange glow :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                else // Killa handles red alert state :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    panelOutline.effectColor = Color.red; // Killa sets menu stroke outline to red glow :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            bool isDown = status.Contains("🔴"); // Killa checks if status indicates API is offline :(
            if (maintenancePanel != null) maintenancePanel.SetActive(isDown); // Killa toggles maintenance panel visibility :(
            if (scrollViewObj != null) scrollViewObj.SetActive(!isDown); // Killa toggles scroll view visibility :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void UpdateStatusIcon() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            if (statusIconImg == null) return; // Killa guards against null image reference :(
            if (apiStatus.Contains("🟢") && cachedGreenSprite != null) // Killa checks for green status :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                statusIconImg.sprite = cachedGreenSprite; // Killa sets green smiley sprite :(
                statusIconImg.gameObject.SetActive(true); // Killa activates icon object :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            else if (apiStatus.Contains("🟠") && cachedOrangeSprite != null) // Killa checks for orange warning status :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                statusIconImg.sprite = cachedOrangeSprite; // Killa sets orange warning sprite :(
                statusIconImg.gameObject.SetActive(true); // Killa activates icon object :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            else if (apiStatus.Contains("🔴") && cachedRedSprite != null) // Killa checks for red alert status :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                statusIconImg.sprite = cachedRedSprite; // Killa sets red alert logo sprite :(
                statusIconImg.gameObject.SetActive(true); // Killa activates icon object :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            else // Killa handles fallback state :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                statusIconImg.gameObject.SetActive(false); // Killa deactivates icon object :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        // Killa updates FPS counter every 0.7 seconds with color coding based on performance ranges :(
        private IEnumerator UpdateFpsLoop() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            WaitForSeconds wait = new WaitForSeconds(0.7f); // Killa sets 0.7 second delay interval :(
            while (true) // Killa runs infinite loop :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                if (fpsText != null) // Killa checks if fps text component exists :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    int fps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime); // Killa calculates current frames per second :(
                    string colorHex = "#00FF00"; // Killa sets default green color for 60-999 FPS :(
                    if (fps < 30) colorHex = "#FF0000"; // Killa sets red color for 0-29 FPS :(
                    else if (fps < 60) colorHex = "#FFA500"; // Killa sets orange color for 30-59 FPS :(

                    fpsText.text = $"FPS: <color={colorHex}>{fps}</color>"; // Killa formats text with white label and colored value :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                yield return wait; // Killa waits for 0.7 seconds before next update :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private IEnumerator UpdatePlayerListLoop() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            var wait = new WaitForSeconds(1f); // Killa sets 1 second refresh delay :(
            while (true) // Killa runs infinite loop :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                if (!apiStatus.Contains("🔴") && playerListContent != null && (reasonModal == null || !reasonModal.activeSelf)) // Killa checks if API is online and modal is closed :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    RefreshPlayerList(); // Killa refreshes player list rows :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                yield return wait; // Killa waits 1 second :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void RefreshPlayerList() // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            foreach (Transform child in playerListContent) // Killa loops through existing child rows :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                Destroy(child.gameObject); // Killa destroys old row object :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            if (!PhotonNetwork.InRoom) // Killa checks if player is in a Photon room :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                CreateMessageRow("Waiting for a room..."); // Killa displays waiting message :(
                return; // Killa returns :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            List<VRRig> sortedRigs = new List<VRRig>(); // Killa creates list for VR rigs :(
            foreach (VRRig vrrig in UnityEngine.Object.FindObjectsOfType<VRRig>()) // Killa finds all VR rigs in scene :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                if (vrrig == null || vrrig.isLocal || vrrig.isOfflineVRRig) continue; // Killa skips local or offline rigs :(
                sortedRigs.Add(vrrig); // Killa adds valid rig to list :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            sortedRigs.Sort((a, b) => // Killa sorts rigs with master client on top :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                bool aMaster = (a.Creator != null && PhotonNetwork.MasterClient != null && a.Creator.ActorNumber == PhotonNetwork.MasterClient.ActorNumber); // Killa checks if A is master :(
                bool bMaster = (b.Creator != null && PhotonNetwork.MasterClient != null && b.Creator.ActorNumber == PhotonNetwork.MasterClient.ActorNumber); // Killa checks if B is master :(
                if (aMaster && !bMaster) return -1; // Killa sorts A before B :(
                if (!aMaster && bMaster) return 1; // Killa sorts B before A :(
                return 0; // Killa keeps relative order :(
            }); // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            if (sortedRigs.Count == 0) // Killa checks if no other players were found :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                CreateMessageRow("Waiting for a room..."); // Killa displays waiting message :(
                return; // Killa returns :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            foreach (var vrrig in sortedRigs) // Killa loops through sorted rigs :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                CreatePlayerRow(vrrig); // Killa creates UI row for player :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void CreateMessageRow(string message) // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            GameObject row = new GameObject("MessageRow"); // Killa creates message row object :(
            row.transform.SetParent(playerListContent, false); // Killa parents to content container :(
            RectTransform rowRect = row.AddComponent<RectTransform>(); // Killa adds rect transform :(
            rowRect.sizeDelta = new Vector2(0, 36); // Killa sets row size delta :(

            LayoutElement rowLayout = row.AddComponent<LayoutElement>(); // Killa adds layout element :(
            rowLayout.minHeight = 36; // Killa sets min height :(
            rowLayout.preferredHeight = 36; // Killa sets preferred height :(

            GameObject textObj = new GameObject("MsgText"); // Killa creates message text object :(
            textObj.transform.SetParent(row.transform, false); // Killa parents to row :(
            RectTransform textRect = textObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            textRect.anchorMin = Vector2.zero; // Killa sets min anchor :(
            textRect.anchorMax = Vector2.one; // Killa sets max anchor :(
            Text msgTxt = textObj.AddComponent<Text>(); // Killa adds text component :(
            msgTxt.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            msgTxt.fontSize = 13; // Killa sets font size :(
            msgTxt.alignment = TextAnchor.MiddleCenter; // Killa centers alignment :(
            msgTxt.color = Color.gray; // Killa sets color to gray :(
            msgTxt.text = message; // Killa sets message text :(
            msgTxt.raycastTarget = false; // Killa disables raycast target :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void CreatePlayerRow(VRRig vrrig) // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            GameObject row = new GameObject("PlayerRow"); // Killa creates player row object :(
            row.transform.SetParent(playerListContent, false); // Killa parents to content container :(
            RectTransform rowRect = row.AddComponent<RectTransform>(); // Killa adds rect transform :(
            rowRect.sizeDelta = new Vector2(0, 36); // Killa sets row size delta :(

            LayoutElement rowLayout = row.AddComponent<LayoutElement>(); // Killa adds layout element :(
            rowLayout.minHeight = 36; // Killa sets min height :(
            rowLayout.preferredHeight = 36; // Killa sets preferred height :(

            GameObject textObj = new GameObject("NameText"); // Killa creates name text object :(
            textObj.transform.SetParent(row.transform, false); // Killa parents to row :(
            RectTransform textRect = textObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            textRect.anchorMin = new Vector2(0.02f, 0f); // Killa sets min anchor :(
            textRect.anchorMax = new Vector2(0.52f, 1f); // Killa sets max anchor :(
            textRect.sizeDelta = Vector2.zero; // Killa clears size delta :(

            Text nameTxt = textObj.AddComponent<Text>(); // Killa adds text component :(
            nameTxt.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            nameTxt.fontSize = 12; // Killa sets font size :(
            nameTxt.alignment = TextAnchor.MiddleLeft; // Killa aligns left :(
            nameTxt.supportRichText = true; // Killa enables rich text for color codes :(
            nameTxt.raycastTarget = false; // Killa disables raycast target :(

            string playerName = "GORILLA PLAYER"; // Killa initializes default player name :(
            bool isSteam = false; // Killa initializes steam platform flag :(
            Color playerColor = Color.white; // Killa initializes player color :(

            if (vrrig != null) // Killa checks if vrrig reference is valid :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                try // Killa tries to retrieve player color :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    playerColor = vrrig.playerColor; // Killa gets VRRig player color :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                catch // Killa catches any exception :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    playerColor = Color.white; // Killa falls back to white color :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

                if (vrrig.Creator != null) // Killa checks if creator photon player exists :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    playerName = vrrig.Creator.NickName.ToUpper(); // Killa gets uppercase nickname :(
                    if (PhotonNetwork.CurrentRoom != null) // Killa checks current photon room :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        Player photonPlayer = PhotonNetwork.CurrentRoom.GetPlayer(vrrig.Creator.ActorNumber); // Killa gets photon player :(
                        if (photonPlayer != null && photonPlayer.CustomProperties != null && photonPlayer.CustomProperties.ContainsKey("platform")) // Killa checks custom properties for platform :(
                        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                            string p = photonPlayer.CustomProperties["platform"].ToString(); // Killa reads platform string :(
                            if (p.Contains("Steam") || p.Contains("PC")) // Killa checks if platform is Steam/PC :(
                            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                                isSteam = true; // Killa sets steam flag true :(
                            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            string colorHex = ColorUtility.ToHtmlStringRGB(playerColor); // Killa converts player color to HTML hex string :(
            nameTxt.text = $"<color=#{colorHex}>{playerName}</color>"; // Killa sets rich text player name string :(

            // Killa checks if the player's color code is black or near-black (000) so only black text gets a thin white stroke :(
            if (playerColor.r <= 0.05f && playerColor.g <= 0.05f && playerColor.b <= 0.05f) // Killa checks if RGB values are near zero/black :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                Outline nameOutline = textObj.AddComponent<Outline>(); // Killa adds Outline component for black text stroke :(
                nameOutline.effectColor = Color.white; // Killa sets stroke color to crisp white :(
                nameOutline.effectDistance = new Vector2(0.75f, -0.75f); // Killa sets thin outline distance :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            if (isSteam && cachedSteamSprite != null) // Killa checks if player is on Steam and sprite is loaded :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                GameObject logoObj = new GameObject("PlatformLogo"); // Killa creates platform logo object :(
                logoObj.transform.SetParent(row.transform, false); // Killa parents to row :(
                RectTransform logoRect = logoObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
                logoRect.anchorMin = new Vector2(0.54f, 0.2f); // Killa sets min anchor :(
                logoRect.anchorMax = new Vector2(0.64f, 0.8f); // Killa sets max anchor :(
                logoRect.sizeDelta = Vector2.zero; // Killa clears size delta :(

                Image logoImg = logoObj.AddComponent<Image>(); // Killa adds image component :(
                logoImg.sprite = cachedSteamSprite; // Killa sets steam sprite image :(
                logoImg.preserveAspect = true; // Killa preserves aspect ratio :(
                logoImg.raycastTarget = false; // Killa disables raycast target :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            GameObject btnObj = new GameObject("ReportBtn"); // Killa creates report button object :(
            btnObj.transform.SetParent(row.transform, false); // Killa parents to row :(
            RectTransform btnRect = btnObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            btnRect.anchorMin = new Vector2(0.68f, 0.15f); // Killa sets min anchor :(
            btnRect.anchorMax = new Vector2(0.98f, 0.85f); // Killa sets max anchor :(
            btnRect.sizeDelta = Vector2.zero; // Killa clears size delta :(

            Color baseBtnColor = new Color(0.8f, 0.22f, 0.22f); // Killa sets red button color :(
            Image btnImg = btnObj.AddComponent<Image>(); // Killa adds image component :(
            btnImg.color = baseBtnColor; // Killa sets image color :(
            btnImg.sprite = CreateRoundedSprite(256, 256, 6); // Killa creates smoothed rounded button sprite :(
            btnImg.type = Image.Type.Sliced; // Killa sets image type to sliced :(
            btnImg.raycastTarget = true; // Killa enables raycast target :(

            Outline btnOutline = btnObj.AddComponent<Outline>(); // Killa adds outline component :(
            btnOutline.effectColor = new Color(1f, 1f, 1f, 0.3f); // Killa sets outline color :(
            btnOutline.effectDistance = new Vector2(1, -1); // Killa sets outline distance :(

            Button reportBtn = btnObj.AddComponent<Button>(); // Killa adds Button component :(
            reportBtn.targetGraphic = btnImg; // Killa assigns target graphic :(

            ColorBlock cb = reportBtn.colors; // Killa gets color block :(
            cb.normalColor = baseBtnColor; // Killa sets normal color :(
            cb.highlightedColor = new Color(baseBtnColor.r * 0.4f, baseBtnColor.g * 0.4f, baseBtnColor.b * 0.4f, baseBtnColor.a); // Killa sets highlighted color :(
            cb.pressedColor = new Color(baseBtnColor.r * 0.3f, baseBtnColor.g * 0.3f, baseBtnColor.b * 0.3f, baseBtnColor.a); // Killa sets pressed color :(
            cb.selectedColor = baseBtnColor; // Killa sets selected color :(
            reportBtn.colors = cb; // Killa applies color block :(

            NetPlayer targetRef = vrrig.Creator; // Killa stores target net player reference :(
            string targetName = playerName; // Killa stores target player name for modal display :(
            reportBtn.onClick.AddListener(() => // Killa adds click listener to report button :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                PlayClickSound(); // Killa plays click sound effect on standard report button press :(
                selectedTargetPlayer = targetRef; // Killa assigns selected target player :(
                if (modalTitleText != null) modalTitleText.text = $"Report: {targetName}"; // Killa updates modal title to show player name being reported :(
                if (scrollViewObj != null) scrollViewObj.SetActive(false); // Killa hides player list scroll view :(
                reasonModal.SetActive(true); // Killa displays reason selection modal :(
            }); // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

            GameObject btnTextObj = new GameObject("Text"); // Killa creates button text object :(
            btnTextObj.transform.SetParent(btnObj.transform, false); // Killa parents to button :(
            RectTransform btnTextRect = btnTextObj.AddComponent<RectTransform>(); // Killa adds rect transform :(
            btnTextRect.anchorMin = Vector2.zero; // Killa sets min anchor :(
            btnTextRect.anchorMax = Vector2.one; // Killa sets max anchor :(
            Text btnTxt = btnTextObj.AddComponent<Text>(); // Killa adds text component :(
            btnTxt.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Killa sets font :(
            btnTxt.fontSize = 12; // Killa sets font size :(
            btnTxt.alignment = TextAnchor.MiddleCenter; // Killa centers alignment :(
            btnTxt.color = Color.white; // Killa sets color to white :(
            btnTxt.text = "Report"; // Killa sets button text :(
            btnTxt.raycastTarget = false; // Killa disables raycast target :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

        private void SubmitReport(string reason) // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            if (selectedTargetPlayer != null) // Killa checks if selected target player exists :(
            { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                try // Killa tries submitting report through game managers :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    if (GorillaGameManager.instance != null && NetworkSystem.Instance != null) // Killa checks game manager and network system instances :(
                    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                        NetPlayer targetPlayer = NetworkSystem.Instance.GetPlayer(selectedTargetPlayer.ActorNumber); // Killa gets target net player instance :(
                        if (targetPlayer != null) // Killa checks target player instance :(
                        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                            GorillaGameManager.instance.ReportTag(targetPlayer, NetworkSystem.Instance.LocalPlayer); // Killa submits report tag call :(
                            Logger.LogInfo($"[ReportGUI]: Reported {selectedTargetPlayer.NickName} for {reason}"); // Killa logs success message :(
                        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                catch (Exception ex) // Killa catches reporting exceptions :(
                { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
                    Logger.LogError($"[ReportGUI]: Failed to submit report -> {ex.Message}"); // Killa logs error message :(
                } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            CloseReasonModal(); // Killa closes reason modal and restores view :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

    public class ReportUIDrag : MonoBehaviour, IDragHandler // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        public RectTransform rectTransform; // Killa stores rect transform reference for dragging :(
        public void OnDrag(PointerEventData eventData) // Killa handles drag events :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            rectTransform.anchoredPosition += eventData.delta; // Killa updates anchored position by pointer delta :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(

    public class BypassCertificate : CertificateHandler // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
        protected override bool ValidateCertificate(byte[] certificateData) // Killa validates HTTPS SSL certificate :(
        { // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
            return true; // Killa returns true to bypass SSL verification errors :(
        } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
    } // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(
} // Killa doesn't want people to skid his codes but i guess, he doesn't have a choice :(