// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using UnityEngine;

public  class MenuController : MonoBehaviour
{
    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
