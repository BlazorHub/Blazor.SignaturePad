// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI subcomponent for rendering a footer in the <see cref="SignaturePad"/> component.
    /// </summary>
    public partial class SignaturePadFooter
    {
        /****************************************************
        *
        *  PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Content to render.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }
        
        /// <summary>
        /// Directive placed directly below signature pad.
        /// </summary>
        [Parameter] public string FooterDirective { get; set; }

        /// <summary>
        /// Whether to inherit a parent's colors (dark, light, or normal modes).
        /// </summary>
        [Parameter] public override bool InheritParentColors { get; set; } = true;

        /// <summary>
        /// The foreground color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string Color
        {
            get => ContrastMode == ContrastModes.Light ? "black" : base.Color ?? "black";
            set => base.Color = value;
        }

        /// <summary>
        /// Whether to inherit a parent's background colors (dark, light, or normal modes).
        /// </summary>
        [Parameter] public override bool InheritParentBackgroundColors { get; set; } = true;

        

        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal SignaturePadPen SignaturePadPen { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal SignaturePadClear SignaturePadClear { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal SignaturePadUndo SignaturePadUndo { get; set; }

        /// <summary>
        /// Child reference. (Assigned by child.)
        /// </summary>
        internal SignaturePadSave SignaturePadSave { get; set; }

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.SignaturePadFooter = this;
        }

        internal void SetOptions(SignaturePad.Options options)
        {
            options.SignaturePadFooter = new Options 
            {
                
            };

            base.SetOptions(options.SignaturePadFooter);
            this.SignaturePadPen?.SetOptions(options);
            this.SignaturePadClear?.SetOptions(options);
            this.SignaturePadUndo?.SetOptions(options);
            this.SignaturePadSave?.SetOptions(options);
        }

        internal async Task<bool> CheckState(SignaturePad.Options options)
        {
            bool baseStateChanged = await base.CheckState(options.SignaturePadFooter);
            bool penStateChanged = await this.SignaturePadPen?.CheckState(options);
            bool clearStateChanged = await this.SignaturePadClear?.CheckState(options);
            bool undoStateChanged = await this.SignaturePadUndo?.CheckState(options);
            bool saveStateChanged = await this.SignaturePadSave?.CheckState(options);

            return baseStateChanged 
                || penStateChanged
                || clearStateChanged
                || undoStateChanged
                || saveStateChanged;
        }
    }
}