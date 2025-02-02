﻿using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSFormFeedback<T> : ValidationMessage<T> 
    {
        private bool Clean = true;
        private FieldIdentifier _fieldIdentifier;
        ///  [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("valid-tooltip", IsTooltip && !HasValidationErrors())
            .AddClass("valid-feedback", !IsTooltip && !HasValidationErrors())
            .AddClass("invalid-tooltip", IsTooltip && HasValidationErrors())
            .AddClass("invalid-feedback", !IsTooltip && HasValidationErrors())

            .AddClass("valid-tooltip", (Parent?.UserValidation ?? false) && IsValid && IsTooltip)
            .AddClass("valid-feedback", (Parent?.UserValidation ?? false) && IsValid && !IsTooltip)
            .AddClass("invalid-tooltip", (Parent?.UserValidation ?? false) && IsInvalid && IsTooltip)
            .AddClass("invalid-feedback", (Parent?.UserValidation ?? false) && IsInvalid && !IsTooltip)
            .AddClass(Class)
        .Build();

        protected bool HasValidationErrors()
        {
            if(Clean || MyEditContext == null)
            {
                Clean = false;
                return false;
            }
            return MyEditContext.GetValidationMessages(_fieldIdentifier).Any();
        }

        [CascadingParameter] BSForm Parent { get; set; }
        [CascadingParameter] EditContext MyEditContext { get; set; }
        [Parameter] public bool IsValid { get; set; }
        [Parameter] public bool IsInvalid { get; set; }
        [Parameter] public bool IsTooltip { get; set; }
        [Parameter] public string Class { get; set; }

        /// <summary>
        /// ValidMessage is the string that gets returned if validation is valid.
        /// </summary>
        [Parameter] public string ValidMessage { get; set; }
        /// <summary>
        /// InvalidMessage is the string that gets returned if validation is invalid.
        /// </summary>
        [Parameter] public string InvalidMessage { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            if (For != null)
            {
                _fieldIdentifier = FieldIdentifier.Create(For);
            }
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ;
            if ((IsValid == true || !HasValidationErrors()) && IsInvalid == false)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", classname);
                builder.AddContent(6, ValidMessage);
                builder.CloseElement();
            }
            else if(IsInvalid)
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", classname);
                builder.AddContent(6, InvalidMessage);
                builder.CloseElement();
            }
            else {
                    builder.OpenElement(0, "div");
                    builder.AddAttribute(1, "class", classname);
                    builder.OpenComponent<ValidationMessage<T>>(2);
                    builder.AddMultipleAttributes(3, AdditionalAttributes);
                    builder.AddAttribute(4, "For", For);
                    builder.CloseComponent();
                    builder.AddContent(5, ChildContent);
                    builder.CloseElement();
            }
        }

       
    }
}
