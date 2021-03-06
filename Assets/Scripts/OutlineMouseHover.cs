﻿/**
 * @file OutlineMouseHover.cs
 * @author Fabian Huber (fabian.hbr@eduge.ch) and Gawen Ackermann (gawen.ackrmnn@eduge.ch)
 * @brief Contains the OutlineMouseHover class.
 * @version 1.0
 * @date 08.10.2020
 *
 * @copyright CFPT (c) 2020
 *
 */

using UnityEngine;

/// <summary>
/// Highlights a gameobject with an OutlineController when the mouse is over it.
/// </summary>
[RequireComponent(typeof(Camera))]
public class OutlineMouseHover : MonoBehaviour
{
    public float highlightRange;

    private Camera cam;
    private OutlineController previousOutlineController;
    private OutlineController currentOutlineController;
    public LayerMask wallLayer;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Ray of the mouse from the camera
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Launch a raycast with this ray
        if (Physics.Raycast(ray, out RaycastHit hit, highlightRange, ~wallLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
            // Check if we hit something
            if (hit.transform == null)
                return;

            // Get the outline controller of the object that we hit
            currentOutlineController = hit.collider.GetComponent<OutlineController>();

            // Check if the object has an outline controller
            if (currentOutlineController == null)
                return;

            if (previousOutlineController != currentOutlineController)
            {
                HideOutline();
                ShowOutline();
            }
            previousOutlineController = currentOutlineController;
        }
        else
        {
            HideOutline();
        }
    }

    /// <summary>
    /// Shows the outline of the current outline controller if it's not null.
    /// </summary>
    private void ShowOutline()
    {
        if (currentOutlineController != null)
        {
            currentOutlineController.ShowOutline();
        }
    }

    /// <summary>
    /// Hide the outline of the current outline controller if it's null.
    /// </summary>
    private void HideOutline()
    {
        if (previousOutlineController != null)
        {
            previousOutlineController.HideOutline();
            previousOutlineController = null;
        }
    }
}