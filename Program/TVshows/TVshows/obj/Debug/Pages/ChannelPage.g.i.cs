﻿#pragma checksum "..\..\..\Pages\ChannelPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C51B1E2C06A1573909CB21107A0D89DF384F72D21BEB45732F22C22E54C572A5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using TVshows.Pages;


namespace TVshows.Pages {
    
    
    /// <summary>
    /// ChannelPage
    /// </summary>
    public partial class ChannelPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Pages\ChannelPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ShowScheduleButton;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\ChannelPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ShowCategoriesButton;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Pages\ChannelPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ChannelsStackPanel;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\Pages\ChannelPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridSplitter DialogGridSplitter;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\Pages\ChannelPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer DialogScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Pages\ChannelPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame DialogFrame;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TVshows;component/pages/channelpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\ChannelPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ShowScheduleButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\Pages\ChannelPage.xaml"
            this.ShowScheduleButton.Click += new System.Windows.RoutedEventHandler(this.ShowScheduleButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ShowCategoriesButton = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Pages\ChannelPage.xaml"
            this.ShowCategoriesButton.Click += new System.Windows.RoutedEventHandler(this.ShowScheduleButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ChannelsStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.DialogGridSplitter = ((System.Windows.Controls.GridSplitter)(target));
            return;
            case 5:
            this.DialogScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 6:
            this.DialogFrame = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

