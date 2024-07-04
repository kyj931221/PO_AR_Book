using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;

    // Dictionary 란?
    // <키 값, 실제 velue> 이루어진 배열(자료구조).
    // 사전에서 단어를 찾듯이 키 값이 필요한 이유는 쉽고 빠르게 찾을 수 있기 때문.

    public List<GameObject> list1 = new List<GameObject>();
    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();

    public List<AudioClip> list2 = new List<AudioClip>();
    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();
    void Start()
    {
        foreach (GameObject o in list1)
        {
            dict1.Add(o.name, o);
        }

        foreach (AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        manager.trackedImagesChanged += OnChanged;
    }

    void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage t in eventArgs.added)
        {
            UpdateImage(t);
            UpdateSound(t);
        }
        foreach (ARTrackedImage t in eventArgs.updated)
        {
            UpdateImage(t);
        }
    }

    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        GameObject o = dict1[(name)];
        o.transform.position = t.transform.position;
        o.transform.rotation = t.transform.rotation;
        o.SetActive(true);
    }

    void UpdateSound(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        AudioClip sound = dict2[name];
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
}
