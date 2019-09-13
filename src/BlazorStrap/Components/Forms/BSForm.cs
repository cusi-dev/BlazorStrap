using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Forms;
using System.Timers;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSForm : EditForm
    {

        protected string classname =>
        new CssBuilder()
            .AddClass("form-inline", IsInline)
            .AddClass(Class)
        .Build();
        [Parameter] public bool UserValidation { get; set; }
        [Parameter] public bool ValidateOnInit { get; set; }
        [Parameter] public bool IsInline { get; set; }
        [Parameter] public string Class { get; set; }
        
        private RenderFragment Form { get; set; }
        private EditContext MyEditContext { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if(Model == null && EditContext == null)
            {
                Form = Formbuilder =>
                {
                    Formbuilder.OpenComponent<EditForm>(0);
                    Formbuilder.AddMultipleAttributes(1, AdditionalAttributes);
                    Formbuilder.AddAttribute(2, "class", classname);
                    Formbuilder.AddAttribute(3, "ChildContent", ChildContent);
                    Formbuilder.CloseComponent();
                };

                builder.OpenComponent<CascadingValue<BSForm>>(3);
                builder.AddAttribute(4, "IsFixed", true);
                builder.AddAttribute(5, "Value", this);
                builder.AddAttribute(6, "ChildContent", Form);
                builder.CloseComponent();
                return;
            }
            Form = Formbuilder =>
            {
                int sequence = -1;

                Formbuilder.OpenComponent<EditForm>(sequence++);
                Formbuilder.AddMultipleAttributes(sequence++, AdditionalAttributes);
                Formbuilder.AddAttribute(sequence++, "class", classname);

                if (EditContext == null)
                {
                    Formbuilder.AddAttribute(sequence++, "Model", Model);
                }
                else
                {
                    Formbuilder.AddAttribute(sequence++, "EditContext", EditContext);
                }

                Formbuilder.AddAttribute(sequence++, "OnSubmit", OnSubmit);
                Formbuilder.AddAttribute(sequence++, "OnValidSubmit", OnValidSubmit);
                Formbuilder.AddAttribute(sequence++, "OnInvalidSubmit", OnInvalidSubmit);
                Formbuilder.AddAttribute(sequence++, "ChildContent", ChildContent);
                Formbuilder.CloseComponent();
            };

            builder.OpenComponent<CascadingValue<BSForm>>(3);
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "Value", this);
            builder.AddAttribute(6, "ChildContent", Form);
            builder.CloseComponent();

        }

        public void FormIsReady(EditContext e)
        {
            MyEditContext = e;
            if (ValidateOnInit)
            {
                ForceValidate();
            }
        }
        public void ForceValidate()
        {
            InvokeAsync(() => MyEditContext?.Validate());
            StateHasChanged();
        }
    }
}
