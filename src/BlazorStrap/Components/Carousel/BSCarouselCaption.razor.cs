﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSCarouselCaptionBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("carousel-caption d-none d-md-block")
        .AddClass(Class)
        .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public string CaptionText { get; set; }
        [Parameter] public string HeaderText { get; set; }
    }
}
