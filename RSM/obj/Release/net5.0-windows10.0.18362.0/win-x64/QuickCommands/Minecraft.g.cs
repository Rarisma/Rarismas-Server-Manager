﻿#pragma checksum "..\..\..\..\..\QuickCommands\Minecraft.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6B4225F6D5B40F8D0FD7C5752188269E1DEB6861"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RSM.QuickCommands;
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


namespace RSM.QuickCommands {
    
    
    /// <summary>
    /// Minecraft
    /// </summary>
    public partial class Minecraft : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SeedButton;
        
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
            System.Uri resourceLocater = new System.Uri("/RSM;component/quickcommands/minecraft.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
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
            
            #line 23 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.StopServer);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 24 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenServer);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.Save);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 27 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.WeatherClear);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 28 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.TimeSetDay);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 29 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.TimeSetNight);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SeedButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            this.SeedButton.Click += new System.Windows.RoutedEventHandler(this.Seed);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 32 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Peaceful);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 33 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Easy);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 34 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Normal);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 35 "..\..\..\..\..\QuickCommands\Minecraft.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Hard);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

