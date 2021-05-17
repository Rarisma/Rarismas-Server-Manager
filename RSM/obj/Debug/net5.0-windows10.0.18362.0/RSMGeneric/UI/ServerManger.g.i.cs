﻿#pragma checksum "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BCD07B22354E9604EC58C44BB85EA0FD13397E51"
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
using RSM.RSMGeneric.UI;
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


namespace RSM.RSMGeneric.UI {
    
    
    /// <summary>
    /// ServerManger
    /// </summary>
    public partial class ServerManger : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ServerName;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ServerVersion;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Config;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModButton;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PluginButton;
        
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
            System.Uri resourceLocater = new System.Uri("/RSM;component/rsmgeneric/ui/servermanger.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
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
            this.ServerName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.ServerVersion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            
            #line 21 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Launcher);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Config = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
            this.Config.Click += new System.Windows.RoutedEventHandler(this.ConfigServer);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 37 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteServer);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ModButton = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
            this.ModButton.Click += new System.Windows.RoutedEventHandler(this.Mods);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PluginButton = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
            this.PluginButton.Click += new System.Windows.RoutedEventHandler(this.OpenPluginFolder);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 43 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GoBack);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 45 "..\..\..\..\..\RSMGeneric\UI\ServerManger.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ConnectionHelp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

