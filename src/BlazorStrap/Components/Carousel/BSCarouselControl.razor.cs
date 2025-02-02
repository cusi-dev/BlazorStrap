﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSCarouselControlBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
         new CssBuilder()
             .AddClass("carousel-control-prev", CarouselDirection == CarouselDirection.Previous)
             .AddClass("carousel-control-next", CarouselDirection == CarouselDirection.Next)
             .AddClass(Class)
         .Build();

        protected string iconClassname =>
            new CssBuilder()
                .AddClass("carousel-control-prev-icon", CarouselDirection == CarouselDirection.Previous)
                .AddClass("carousel-control-next-icon", CarouselDirection == CarouselDirection.Next)
            .Build();

        protected string directionName => CarouselDirection == CarouselDirection.Previous ? "Previous" : "Next";

        [Parameter] public int ActiveIndex { get; set; }
        [Parameter] public int NumberOfItems { get; set; }
        [Parameter] public CarouselDirection CarouselDirection { get; set; } = CarouselDirection.Previous;
        [Parameter] public string Class { get; set; }
        [CascadingParameter] internal BSCarousel Parent { get; set; }

        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }

        protected async Task _onclick(MouseEventArgs e)
        {
            if (CarouselDirection == CarouselDirection.Previous)
            {

                if (ActiveIndex == 0) { ActiveIndex = NumberOfItems - 1; }
                else { ActiveIndex = ActiveIndex - 1; }
            }
            else
            {
                if (ActiveIndex == NumberOfItems - 1) { ActiveIndex = 0; }
                else { ActiveIndex = ActiveIndex + 1; }

            }
            await ActiveIndexChanged.InvokeAsync(ActiveIndex);
        }
    }
}
