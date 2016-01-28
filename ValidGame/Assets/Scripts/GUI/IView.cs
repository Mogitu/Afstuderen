﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Represents a "root view" object directly attached to a canvas with a presenter component.
/// </summary>
public interface IView {
    void SetPresenter(Presenter presenter);
    bool IsActive { get; }  
}