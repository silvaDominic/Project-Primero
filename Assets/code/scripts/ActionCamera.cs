using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts {

    [RequireComponent(typeof(Camera))]
    public class ActionCamera : MonoBehaviour {

        private GameObject playersParent;
        private Transform[] players;
        private Camera actionCamera;

        public float minimumCameraDistance = -3f;
        public float defaultPlayerDistance = 3.33f;

        // Use this for initialization
        void Start() {
            actionCamera = GetComponent<Camera>();
            playersParent = GameObject.Find(Constants.PLAYERS_OBJECT);
            if (playersParent == null) {
                playersParent = new GameObject(Constants.PLAYERS_OBJECT);
            }
            // Retrieve all player transforms from scene
            players = playersParent.transform.GetComponentsInDirectChildren<Transform>();
        }

        // Update is called once per frame
        void Update() {
            // Find all left, right, top, and bottom edge players
            float LeftEdge = FindLeftEdgePlayer();
            float RightEdge = FindRightEdgePlayer();
            float BottomEdge = FindBottomEdgePlayer();
            float TopEdge = FindTopEdgePlayer();

            // Calculate the max horizontal and vertical distances between all players
            float maxHorizontalPlayerSeparation = Mathf.Sqrt(Mathf.Pow(LeftEdge - RightEdge, 2));
            float maxVerticalPlayerSeparation = Mathf.Sqrt(Mathf.Pow(BottomEdge - TopEdge, 2));

            // Calculate the distance to center of viewbox from max player separation
            float horizontalDistanceToCenter = maxHorizontalPlayerSeparation / 2;
            float verticalDistanceToCenter = maxVerticalPlayerSeparation/ 2;

            // Calculate the ratio between the minimum camera distance and an arbitrary default player distance
            float cameraZIndexFactor = (minimumCameraDistance / defaultPlayerDistance);
            // Calculate the camera's z index (distance from 'action') using the z index factor
            float zIndex = maxHorizontalPlayerSeparation * cameraZIndexFactor;

            // Update that camera's z index
            // If it falls below the minimumCameraDistance, set it equal to the minimum
            actionCamera.transform.position = new Vector3(LeftEdge + horizontalDistanceToCenter, BottomEdge + verticalDistanceToCenter, zIndex);
            if (actionCamera.transform.position.z > minimumCameraDistance) {
                actionCamera.transform.position = new Vector3(LeftEdge + horizontalDistanceToCenter, BottomEdge + verticalDistanceToCenter, minimumCameraDistance);
            }
        }

        /// <summary>
        /// Finds the left most player's x position
        /// </summary>
        /// <returns>A float representing the x position of the left most player</returns>
        private float FindLeftEdgePlayer() {
            // Use an arbitrary player's x position (first in array in this case) as a reference
            float leftMostPlayer = players[0].transform.position.x;
            // Iterate over all players, compare x position, and update leftMostPlayer accordingly
            foreach (Transform player in players) {
                if (player.position.x < leftMostPlayer) {
                    leftMostPlayer = player.position.x;
                }
            }
            return leftMostPlayer;
        }

        /// <summary>
        /// Finds the right most player's x position
        /// </summary>
        /// <returns>A float representing the x position of the right most player</returns>
        private float FindRightEdgePlayer() {
            // Use an arbitrary player's x position (first in array in this case) as a reference
            float rightMostPlayer = players[0].transform.position.x;
            // Iterate over all players, compare x positions, and update rightMostPlayer accordingly
            foreach (Transform player in players) {
                if (player.position.x > rightMostPlayer) {
                    rightMostPlayer = player.position.x;
                }
            }
            return rightMostPlayer;
        }

        /// <summary>
        /// Finds the bottom most player's y position
        /// </summary>
        /// <returns>A float representing the y position of the bottom most player</returns>
        private float FindBottomEdgePlayer() {
            // Use an arbitrary player's x position (first in array in this case) as a reference
            float bottomMostPlayer = players[0].transform.position.y;
            // Iterate over all players, compare y positions, and update bottomMostPlayer accordingly
            foreach (Transform player in players) {
                if (player.position.y < bottomMostPlayer) {
                    bottomMostPlayer = player.position.y;
                }
            }
            return bottomMostPlayer;
        }

        /// <summary>
        /// Finds the top most player's y position
        /// </summary>
        /// <returns>A float representing the y position of the top most player</returns>
        private float FindTopEdgePlayer() {
            // Use an arbitrary player's x position (first in array in this case) as a reference
            float topMostPlayer = players[0].transform.position.y;
            // Iterate over all players, compare y positions, and update topMostPlayer accordingly
            foreach (Transform player in players) {
                if (player.position.y > topMostPlayer) {
                    topMostPlayer = player.position.y;
                }
            }
            return topMostPlayer;
        }
    }
}