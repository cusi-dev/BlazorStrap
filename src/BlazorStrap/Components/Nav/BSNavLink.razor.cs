﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorStrap
{
    public abstract class BSNavLinkBase : ComponentBase, IDisposable
    {
        [Inject] private NavigationManager UriHelper { get; set; }
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] BSNavItem Parent { get; set; }
        [CascadingParameter] BSCollapseItem CollapseItem { get; set; }
        [CascadingParameter] BSListItem ListItem { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("nav-item nav-link", !RemoveDefaultClass)
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(Class)
        .Build();

        protected string disabled => IsDisabled ? "disabled" : null;

        [Parameter] public bool RemoveDefaultClass { get; set; }
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string Href { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            UriHelper.LocationChanged += OnLocationChanged;
            OnLocationChanged(this, new LocationChangedEventArgs(UriHelper.Uri, true));
            OnLocationChanged(this, new LocationChangedEventArgs(UriHelper.Uri, true));
        }
        public void Dispose()
        {
            UriHelper.LocationChanged -= OnLocationChanged;
        }

        public void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            var active = e.Location.MatchActiveRoute(UriHelper.BaseUri + Href);
          
                if (Parent != null)
                {
                    Parent.Active = active;
                }
                if(CollapseItem != null)
                {
                    CollapseItem.Active = active;
                }
                if(ListItem != null)
                {
                    ListItem.Active = active;
                }
                IsActive = active;
            StateHasChanged();
        }
    }
}
