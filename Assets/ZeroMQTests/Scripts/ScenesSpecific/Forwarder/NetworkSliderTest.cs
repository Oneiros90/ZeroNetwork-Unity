using System.Globalization;
using ZeroNetwork;
using ZeroNetwork.Unity;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkSliderTest : MonoBehaviour
{
    [Header("Network components")]
    public PublisherComponent publisher;
    public SubscriberComponent subscriber;

    [Header("GUI")]
    public TMP_InputField hisCode;
    public Button joinButton;
    public Button stopJoinButton;

    public Slider slider;

    public TMP_InputField mycode;
    public Button hostButton;
    public Button stopHostButton;


    private void Start()
    {
        mycode.text = RandomCode(5);

        joinButton.onClick.AddListener(() =>
        {
            if (hisCode.text.Length > 0)
            {
                subscriber.Topic = hisCode.text;
                subscriber.enabled = true;
                SetJoinState(true);
            }
        });

        stopJoinButton.onClick.AddListener(() =>
        {
            subscriber.enabled = false;
            SetJoinState(false);
        });


        subscriber.OnNewMessage += (PubSubMessage msg) =>
        {
            slider.value = float.Parse(msg.Data, CultureInfo.InvariantCulture);
        };

        slider.onValueChanged.AddListener((float f) =>
        {
            if (publisher.enabled)
                publisher.Publish(f.ToString(CultureInfo.InvariantCulture));
        });


        hostButton.onClick.AddListener(() =>
        {
            publisher.Topic = mycode.text;
            publisher.enabled = true;
            SetHostState(true);
        });

        stopHostButton.onClick.AddListener(() =>
        {
            publisher.enabled = false;
            SetHostState(false);
        });

    }

    private string RandomCode(int n)
    {
        char[] chars = new char[n];
        for (int i = 0; i < n; i++)
            chars[i] = (char) ('0' + Random.Range(0, 10));
        return chars.ArrayToString();
    }

    private void SetJoinState(bool joined)
    {
        hisCode.gameObject.SetActive(true);
        joinButton.gameObject.SetActive(!joined);
        stopJoinButton.gameObject.SetActive(joined);

        slider.interactable = false;

        mycode.gameObject.SetActive(!joined);
        hostButton.gameObject.SetActive(!joined);
        stopHostButton.gameObject.SetActive(!joined);
    }

    private void SetHostState(bool hosted)
    {
        hisCode.gameObject.SetActive(!hosted);
        joinButton.gameObject.SetActive(!hosted);
        stopJoinButton.gameObject.SetActive(!hosted);

        slider.interactable = hosted;

        mycode.gameObject.SetActive(true);
        hostButton.gameObject.SetActive(!hosted);
        stopHostButton.gameObject.SetActive(hosted);
    }
}
