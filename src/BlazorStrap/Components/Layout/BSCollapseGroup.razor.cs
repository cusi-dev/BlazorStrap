﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;

namespace BlazorStrap 
{
    public abstract class BSCollapseGroupBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        private BSCollapseItem _selected;

        public BSCollapseItem Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (value == null)
                {
                    _selected?.Collapse.Hide();
                    _selected = null;
                }
                else
                {
                    _selected?.Collapse.Hide();
                    _selected = value;
                    _selected?.Collapse.Show();
                }
            }
        }
    }
}
