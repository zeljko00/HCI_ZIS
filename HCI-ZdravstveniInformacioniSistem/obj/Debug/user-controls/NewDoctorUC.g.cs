#pragma checksum "..\..\..\user-controls\NewDoctorUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "01581BC2CA254EA5C6D9879DB7CB431EB94F7CB06BEDBD0BBEC8C00D7FC33F59"
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
    /// NewDoctorUC
    /// </summary>
    public partial class NewDoctorUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Desc;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveBtn;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Tel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePicker;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label HideLbl;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SpecCombo;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\user-controls\NewDoctorUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox HcfCombo;
        
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
            System.Uri resourceLocater = new System.Uri("/HCI-ZdravstveniInformacioniSistem;component/user-controls/newdoctoruc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\user-controls\NewDoctorUC.xaml"
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
            this.Name = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.Name.LostFocus += new System.Windows.RoutedEventHandler(this.Name_LostFocus);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.Name.GotFocus += new System.Windows.RoutedEventHandler(this.Name_GotFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Desc = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.Desc.LostFocus += new System.Windows.RoutedEventHandler(this.Desc_LostFocus);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.Desc.GotFocus += new System.Windows.RoutedEventHandler(this.Desc_GotFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SaveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.SaveBtn.Click += new System.Windows.RoutedEventHandler(this.SaveBtn_OnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Tel = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.Tel.LostFocus += new System.Windows.RoutedEventHandler(this.Tel_LostFocus);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.Tel.GotFocus += new System.Windows.RoutedEventHandler(this.Tel_GotFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DatePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 23 "..\..\..\user-controls\NewDoctorUC.xaml"
            this.DatePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.DatePicker_OnSelect);
            
            #line default
            #line hidden
            return;
            case 6:
            this.HideLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.SpecCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.HcfCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

