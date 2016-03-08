using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicScroll : ScrollBase {

    private void Start()
    {
        base.Start();

        BoxCollider collider = GetComponent<BoxCollider>();
        RectTransform parentTransform = (RectTransform) transform.parent;
        collider.size = new Vector3(parentTransform.rect.width, parentTransform.rect.height, 0.5f);

        // Hacky fix for ContentSizeFitter
        Invoke("ContentBoundUpdate", 0.1f);
    }

    public void DelayedUpdate(float delay)
    {
        Invoke("ContentBoundUpdate", delay);
    }

    // Hacky fix for ContentSizeFitter
    void ContentBoundUpdate()
    {
        var heightMid = ContentTransform.GetComponent<RectTransform>().rect.height/2f;
        ContentTopBound.localPosition = new Vector3(0f, heightMid, 0f);
        ContentBottomBound.localPosition = new Vector3(0f, -heightMid, 0f);
        ContentTransform.localPosition = new Vector3(0f, -heightMid, 0f);
        ContentTransform.gameObject.GetComponent<LayoutElement>().minWidth = ((RectTransform) transform.parent).rect.width;
        ContentTransform.gameObject.GetComponent<LayoutElement>().preferredWidth = ((RectTransform)transform.parent).rect.width;
    }
}
