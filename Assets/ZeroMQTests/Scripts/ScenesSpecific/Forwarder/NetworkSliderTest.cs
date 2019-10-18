using System.Globalization;
using ZeroNetwork;
using ZeroNetwork.Unity;
using UnityEngine;
using UnityEngine.UI;

public class NetworkSliderTest : MonoBehaviour
{
    public PublisherComponent publisher;
    public SubscriberComponent subscriber;
    public Slider slider1, slider2;

    private void Start()
    {
        slider1.onValueChanged.AddListener((float f) =>
        {
            publisher.Publish(f.ToString(CultureInfo.InvariantCulture));
        });

        subscriber.OnNewMessage += (PubSubMessage msg) =>
        {
            slider2.value = float.Parse(msg.Data, CultureInfo.InvariantCulture);
        };

#if !UNITY_EDITOR
        var tmp = publisher.Topic;
        publisher.Topic = subscriber.Topic;
        subscriber.Topic = tmp;
#endif

        publisher.enabled = true;
        subscriber.enabled = true;

    }
}
