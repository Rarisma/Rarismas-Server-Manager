﻿#pragma checksum "..\..\..\..\Creator\RAMAllocator.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "075C348044F67323505244C9DB2A827297CC5702"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ModernWpf;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using ModernWpf.DesignTime;
using ModernWpf.Markup;
using ModernWpf.Media.Animation;
using RSM.Creator;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace RSM.Creator {
    
    
    /// <summary>
    /// RAMAllocator
    /// </summary>
    public partial class RAMAllocator : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\Creator\RAMAllocator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider RamSlider;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Creator\RAMAllocator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MaxRam;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Creator\RAMAllocator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AllocatedRAMDisplay;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RSM;component/creator/ramallocator.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Creator\RAMAllocator.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.RamSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 34 "..\..\..\..\Creator\RAMAllocator.xaml"
            this.RamSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SliderChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MaxRam = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.AllocatedRAMDisplay = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            
            #line 51 "..\..\..\..\Creator\RAMAllocator.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Continue);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 52 "..\..\..\..\Creator\RAMAllocator.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GoBack);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

