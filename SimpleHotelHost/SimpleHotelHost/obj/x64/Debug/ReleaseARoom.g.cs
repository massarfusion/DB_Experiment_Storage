﻿#pragma checksum "C:\Users\Yito Wang\source\repos\SimpleHotelHost\SimpleHotelHost\ReleaseARoom.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "07A5A5AA73A88F1AE35E91DC2EEE9C9C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleHotelHost
{
    partial class ReleaseARoom : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class ReleaseARoom_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IReleaseARoom_Bindings
        {
            private global::SimpleHotelHost.ReleaseARoom dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListBox obj12;
            private global::Windows.UI.Xaml.Controls.ListBox obj13;
            private global::Windows.UI.Xaml.Controls.ListBox obj14;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj12ItemsSourceDisabled = false;
            private static bool isobj13ItemsSourceDisabled = false;
            private static bool isobj14ItemsSourceDisabled = false;

            public ReleaseARoom_obj1_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 42 && columnNumber == 101)
                {
                    isobj12ItemsSourceDisabled = true;
                }
                else if (lineNumber == 43 && columnNumber == 93)
                {
                    isobj13ItemsSourceDisabled = true;
                }
                else if (lineNumber == 44 && columnNumber == 101)
                {
                    isobj14ItemsSourceDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 12: // ReleaseARoom.xaml line 42
                        this.obj12 = (global::Windows.UI.Xaml.Controls.ListBox)target;
                        break;
                    case 13: // ReleaseARoom.xaml line 43
                        this.obj13 = (global::Windows.UI.Xaml.Controls.ListBox)target;
                        break;
                    case 14: // ReleaseARoom.xaml line 44
                        this.obj14 = (global::Windows.UI.Xaml.Controls.ListBox)target;
                        break;
                    default:
                        break;
                }
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IReleaseARoom_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::SimpleHotelHost.ReleaseARoom)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::SimpleHotelHost.ReleaseARoom obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_provSel(obj.provSel, phase);
                        this.Update_citySel(obj.citySel, phase);
                        this.Update_distSel(obj.distSel, phase);
                    }
                }
            }
            private void Update_provSel(global::System.Collections.ObjectModel.ObservableCollection<global::System.String> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // ReleaseARoom.xaml line 42
                    if (!isobj12ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj12, obj, null);
                    }
                }
            }
            private void Update_citySel(global::System.Collections.ObjectModel.ObservableCollection<global::System.String> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // ReleaseARoom.xaml line 43
                    if (!isobj13ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj13, obj, null);
                    }
                }
            }
            private void Update_distSel(global::System.Collections.ObjectModel.ObservableCollection<global::System.String> obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // ReleaseARoom.xaml line 44
                    if (!isobj14ItemsSourceDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj14, obj, null);
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // ReleaseARoom.xaml line 94
                {
                    this.submitAttempt = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.submitAttempt).Click += this.submitAttempt_Click;
                }
                break;
            case 3: // ReleaseARoom.xaml line 88
                {
                    this.rIntro = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // ReleaseARoom.xaml line 82
                {
                    this.rName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // ReleaseARoom.xaml line 68
                {
                    this.prmpt2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // ReleaseARoom.xaml line 70
                {
                    this.priceNum = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.priceNum).LostFocus += this.priceNum_LostFocus;
                }
                break;
            case 7: // ReleaseARoom.xaml line 75
                {
                    this.unitPriceWarn2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // ReleaseARoom.xaml line 54
                {
                    this.prmpt1 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9: // ReleaseARoom.xaml line 56
                {
                    this.gstNumSum = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.gstNumSum).LostFocus += this.gstNumSum_LostFocus;
                }
                break;
            case 10: // ReleaseARoom.xaml line 61
                {
                    this.gstNumWarn = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 11: // ReleaseARoom.xaml line 48
                {
                    this.dtl = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // ReleaseARoom.xaml line 42
                {
                    this.province = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ListBox)this.province).SelectionChanged += this.province_SelectionChanged;
                }
                break;
            case 13: // ReleaseARoom.xaml line 43
                {
                    this.city = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ListBox)this.city).SelectionChanged += this.city_SelectionChanged;
                }
                break;
            case 14: // ReleaseARoom.xaml line 44
                {
                    this.district = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ListBox)this.district).SelectionChanged += this.district_SelectionChanged;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // ReleaseARoom.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    ReleaseARoom_obj1_Bindings bindings = new ReleaseARoom_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

