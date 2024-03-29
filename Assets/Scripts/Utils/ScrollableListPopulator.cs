﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using UnityEngine;

namespace HoloLens4Labs.Scripts.Utils
{
    /// <summary>
    /// Simple demonstration of how to instantiate a <see cref="Microsoft.MixedReality.Toolkit.UI.ScrollingObjectCollection"/> as well as use lazy loading to mitigate the perf cost of a large list of items.
    /// </summary>
    [AddComponentMenu("Scripts/MRTK/Examples/ScrollableListPopulator")]
    public class ScrollableListPopulator : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The ScrollingObjectCollection to populate, if left empty. the populator will create on your behalf.")]
        private ScrollingObjectCollection scrollView;

        /// <summary>
        /// The ScrollingObjectCollection to populate, if left empty. the populator will create on your behalf.
        /// </summary>
        public ScrollingObjectCollection ScrollView
        {
            get { return scrollView; }
            set { scrollView = value; }
        }

        [SerializeField]
        [Tooltip("Prefab to duplicate in ScrollCollection")]
        private GameObject itemPrefab;


        [SerializeField]
        [Tooltip("Actions to perform on the instantiated prefab")]
        public GameObjectWithDataActionInterface<object> onInstantiateActions;

        /// <summary>
        /// Object to duplicate in <see cref="ScrollView"/>. 
        /// </summary>
        public GameObject DynamicItem
        {
            get { return itemPrefab; }
            set { itemPrefab = value; }
        }

        [SerializeField]
        [Tooltip("Number of items to generate")]
        private int numItems;

        /// <summary>
        /// Number of items to generate
        /// </summary>
        public int NumItems
        {
            get { return numItems; }
            set { numItems = value; }
        }

        [SerializeField]
        [Tooltip("Demonstrate lazy loading")]
        private bool lazyLoad;

        /// <summary>
        /// Demonstrate lazy loading 
        /// </summary>
        public bool LazyLoad
        {
            get { return lazyLoad; }
            set { lazyLoad = value; }
        }

        [SerializeField]
        [Tooltip("Number of items to load each frame during lazy load")]
        private int itemsPerFrame = 3;

        /// <summary>
        /// Number of items to load each frame during lazy load 
        /// </summary>
        public int ItemsPerFrame
        {
            get { return itemsPerFrame; }
            set { itemsPerFrame = value; }
        }

        [SerializeField]
        [Tooltip("Indeterminate loader to hide / show for LazyLoad")]
        private GameObject loader;

        [SerializeField]
        private float cellWidth = 0.032f;

        [SerializeField]
        private float cellHeight = 0.032f;

        [SerializeField]
        private float cellDepth = 0.032f;

        [SerializeField]
        private int cellsPerTier = 3;

        [SerializeField]
        private int tiersPerPage = 5;

        [SerializeField]
        private Transform scrollPositionRef = null;

        private GridObjectCollection gridObjectCollection;

        /// <summary>
        /// Indeterminate loader to hide / show for <see cref="LazyLoad"/> 
        /// </summary>
        public GameObject Loader
        {
            get { return loader; }
            set { loader = value; }
        }

        private void OnEnable()
        {
            // Make sure we find a collection
            if (scrollView == null)
            {
                scrollView = GetComponentInChildren<ScrollingObjectCollection>();
            }
        }





        public void MakeScrollingList()
        {
            if (scrollView == null)
            {
                GameObject newScroll = new GameObject("Scrolling Object Collection");
                newScroll.transform.parent = scrollPositionRef ? scrollPositionRef : transform;
                newScroll.transform.localPosition = Vector3.zero;
                newScroll.transform.localRotation = Quaternion.identity;
                newScroll.SetActive(false);
                scrollView = newScroll.AddComponent<ScrollingObjectCollection>();

                // Prevent the scrolling collection from running until we're done dynamically populating it.
                scrollView.CellWidth = cellWidth;
                scrollView.CellHeight = cellHeight;
                scrollView.CellDepth = cellDepth;
                scrollView.CellsPerTier = cellsPerTier;
                scrollView.TiersPerPage = tiersPerPage;
            }

            gridObjectCollection = scrollView.GetComponentInChildren<GridObjectCollection>();

            if (gridObjectCollection != null)
            {
                Destroy(gridObjectCollection.gameObject);
            }


            GameObject collectionGameObject = new GameObject("Grid Object Collection");
            collectionGameObject.transform.position = scrollView.transform.position;
            collectionGameObject.transform.rotation = scrollView.transform.rotation;
            collectionGameObject.transform.localScale = scrollView.transform.localScale;

            gridObjectCollection = collectionGameObject.AddComponent<GridObjectCollection>();
            gridObjectCollection.CellWidth = cellWidth;
            gridObjectCollection.CellHeight = cellHeight;
            gridObjectCollection.SurfaceType = ObjectOrientationSurfaceType.Plane;
            gridObjectCollection.Layout = LayoutOrder.ColumnThenRow;
            gridObjectCollection.Columns = cellsPerTier;
            gridObjectCollection.Anchor = LayoutAnchor.UpperLeft;

            scrollView.AddContent(collectionGameObject);

            scrollView.gameObject.SetActive(true);

            /*
                                   if (!lazyLoad)
                                   {
                                       for (int i = 0; i < numItems; i++)
                                       {
                                           AddItemTest(itemPrefab);
                                       }
                                       scrollView.gameObject.SetActive(true);
                                       gridObjectCollection.UpdateCollection();
                                   }
                                   else
                                   {
                                       if (loader != null)
                                       {
                                           loader.SetActive(true);
                                       }

                                       StartCoroutine(UpdateListOverTime(loader, itemsPerFrame));
                                   }
            */
        }

        private IEnumerator UpdateListOverTime(GameObject loaderViz, int instancesPerFrame)
        {
            for (int currItemCount = 0; currItemCount < numItems; currItemCount++)
            {
                for (int i = 0; i < instancesPerFrame; i++)
                {
                    AddItemTest(itemPrefab);
                }
                yield return null;
            }

            // Now that the list is populated, hide the loader and show the list
            loaderViz.SetActive(false);
            scrollView.gameObject.SetActive(true);

            // Finally, manually call UpdateCollection to set up the collection
            gridObjectCollection.UpdateCollection();
        }



        private void AddItemTest(GameObject item)
        {
            GameObject itemInstance = Instantiate(item, gridObjectCollection.transform);
            itemInstance.SetActive(true);
        }


        public void AddItem<T>(T data)
        {

            GameObject itemInstance = Instantiate(itemPrefab, gridObjectCollection.transform);
            itemInstance.SetActive(true);
            onInstantiateActions.Invoke(itemInstance, data);
            gridObjectCollection.UpdateCollection();

        }


    }
}