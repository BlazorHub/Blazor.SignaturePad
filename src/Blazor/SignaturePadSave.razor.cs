// Copyright (c) 2020 Allan Mobley. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Mobsites.Blazor
{
    /// <summary>
    /// UI child component of the <see cref="SignaturePadFooter"/> component, 
    /// housing the functionality for saving to file the signature
    /// of the <see cref="SignaturePad"/> component.
    /// </summary>
    public partial class SignaturePadSave
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
        /// Image type to save as. Defaults to png.
        /// </summary>
        [Parameter] public SignaturePad.SupportedSaveAsTypes SaveAsType { get; set; }

        /// <summary>
        /// The foreground color for this component. Accepts any valid css color usage.
        /// </summary>
        [Parameter] public override string Color
        {
            get => base.Color ?? "black";
            set => base.Color = value;
        }

        /// <summary>
        /// Whether to inherit a parent's background colors (dark, light, or normal modes).
        /// </summary>
        [Parameter] public override bool InheritParentBackgroundColors { get; set; } = true;

        /// <summary>
        /// URL or URL fragment for image source.
        /// </summary>
        [Parameter] public string Image { get; set; }

        private int imageWidth = 36;
        
        /// <summary>
        /// Image width in pixels. Defaults to 36px.
        /// </summary>
        [Parameter] public int ImageWidth 
        { 
            get => imageWidth; 
            set 
            { 
                if (value > 0)
                {
                    imageWidth = value;
                } 
            } 
        }

        private int imageHeight = 36;
        
        /// <summary>
        /// Image height in pixels. Defaults to 36px.
        /// </summary>
        [Parameter] public int ImageHeight 
        { 
            get => imageHeight; 
            set 
            { 
                if (value > 0)
                {
                    imageHeight = value;
                } 
            } 
        }

        /// <summary>
        /// Save signature to file as one of the supported image types.
        /// </summary>
        public Task Save(SignaturePad.SupportedSaveAsTypes saveAsType) => this.jsRuntime.InvokeVoidAsync(
            "Mobsites.Blazor.SignaturePad.save", 
            saveAsType.ToString())
            .AsTask();

        

        /****************************************************
        *
        *  NON-PUBLIC INTERFACE
        *
        ****************************************************/

        protected override void OnParametersSet()
        {
            // This will check for valid parent.
            base.OnParametersSet();
            base.Parent.SignaturePadSave = this;
        }

        internal void SetOptions(SignaturePad.Options options)
        {
            options.SignaturePadSave = new Options 
            {
                
            };

            base.SetOptions(options.SignaturePadSave);
        }

        internal async Task<bool> CheckState(SignaturePad.Options options)
        {
            return await base.CheckState(options.SignaturePadSave);
        }
    }
}