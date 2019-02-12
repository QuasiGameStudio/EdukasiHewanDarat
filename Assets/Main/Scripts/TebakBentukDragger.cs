using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TebakBentukDragger : MonoBehaviour {

	public const string DRAGGABLE_TAG = "TebakBentukDragger";

    private bool dragging = false;

    private Vector2 originalPosition;
    private Transform objectToDrag;
    private Image objectToDragImage;
		
	private Vector2 sourceSize;
	private Vector2 targetSize;

	[SerializeField]
	private GameObject sourceTile;
	[SerializeField]
	private GameObject targetTile;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    [SerializeField]
	private AudioClip[] gameClips;

    #region Monobehaviour API

	void Start(){
		sourceSize = sourceTile.GetComponent<RectTransform>().sizeDelta;
		targetSize = targetTile.GetComponent<RectTransform>().sizeDelta;
	}

    void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if (objectToDrag != null)
            {
                dragging = true;

                objectToDrag.SetAsLastSibling();

                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Image>();
                objectToDragImage.raycastTarget = false;

                //
                GetComponent<AudioSource>().PlayOneShot(gameClips[0]);
            }
        }

        if (dragging && !objectToDrag.GetComponent<Tile_TebakBentuk>().GetIsTarget())
        {
            objectToDrag.position = Input.mousePosition;
			objectToDrag.GetChild(0).GetComponent<RectTransform>().sizeDelta = targetSize;
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            Transform tempObjectToDrag = null;

            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null && objectToDrag.GetComponent<Tile_TebakBentuk>().GetNumber() == objectToReplace.GetComponent<Tile_TebakBentuk>().GetNumber())
                {
                    //objectToDrag.position = objectToReplace.position;
                    //objectToReplace.position = originalPosition;                                       
					GM_TebakBentuk.Instance.Match();
					objectToDrag.position = originalPosition;
					objectToDrag.GetChild(0).GetComponent<RectTransform>().sizeDelta = sourceSize;

                    GetComponent<AudioSource>().PlayOneShot(gameClips[2]);
                }
                else
                {
                    GM_TebakBentuk.Instance.UnMatch();
					objectToDrag.position = originalPosition;
					objectToDrag.GetChild(0).GetComponent<RectTransform>().sizeDelta = sourceSize;

                    GetComponent<AudioSource>().PlayOneShot(gameClips[1]);
                }

                tempObjectToDrag = objectToDrag;

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;

                
            }

            dragging = false;
            
        }
	}

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;        
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        var clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }

    #endregion
}
