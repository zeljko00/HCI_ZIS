﻿#pragma checksum "..\..\..\user-controls\InfoControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "03C2155F1784BC9A7474B1D3E1164ACAF7DF8CDCBF8931A83ABF126E299338CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HCI_ZdravstveniInformacioniSistem.user_controls;
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


namespace HCI_ZdravstveniInformacioniSistem.user_controls {
    
    
    /// <summary>
    /// InfoControl
    /// </summary>
    public partial class InfoControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\user-controls\InfoControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MedName;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\user-controls\InfoControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MedContent;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\user-controls\InfoControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderControl;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\user-controls\InfoControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MedDesc;
        
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
            System.Uri resourceLocater = new System.Uri("/HCI-ZdravstveniInformacioniSistem;component/user-controls/infocontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\user-controls\InfoControl.xaml"
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
            
            #line 8 "..\..\..\user-controls\InfoControl.xaml"
            ((HCI_ZdravstveniInformacioniSistem.user_controls.InfoControl)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.Hover);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\user-controls\InfoControl.xaml"
            ((HCI_ZdravstveniInformacioniSistem.user_controls.InfoControl)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.HoverEnd);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MedName = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.MedContent = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.BorderControl = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            this.MedDesc = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

