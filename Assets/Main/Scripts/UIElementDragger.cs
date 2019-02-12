using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Copyright (C) Xenfinity LLC - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bilal Itani <bilalitani1@gmail.com>, June 2017
 */

public class UIElementDragger : MonoBehaviour {

    public const string DRAGGABLE_TAG = "UIDraggable";

    private bool dragging = false;

    private Vector2 originalPosition;
    private Transform objectToDrag; 
    private Image objectToDragImage;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    [SerializeField]
	private AudioClip[] gameClips;

    #region Monobehaviour API

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

        if (dragging && objectToDrag.GetComponent<Tile>().GetCanDrag())
        {
            objectToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            bool changeCanDrag = false;
            Transform tempObjectToDrag = null;

            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null && objectToDrag.GetComponent<Tile>().GetNumber() == objectToReplace.GetComponent<Tile>().GetNumber())
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToReplace.position = originalPosition;

                    objectToReplace.gameObject.SetActive(false); 

                    //
                    changeCanDrag = true;

                    GetComponent<AudioSource>().PlayOneShot(gameClips[2]);
                }
                else
                {
                    objectToDrag.position = originalPosition;
                    GetComponent<AudioSource>().PlayOneShot(gameClips[1]);
                }

                tempObjectToDrag = objectToDrag;

                objectToDragImage.raycastTarget = true;
                objectToDrag = null;

                
            }

            dragging = false;

            //disable raycast
            if(changeCanDrag){
                tempObjectToDrag.GetComponent<Tile>().SetCanDrag(false);
                GM_Puzzle.Instance.AddTileCompleted();
                changeCanDrag = false;
            }
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
