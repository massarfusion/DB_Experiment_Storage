﻿#pragma checksum "C:\Users\Yito Wang\source\repos\SimpleHotelHost\SimpleHotelHost\loginIn.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "16ED226AA2334D27B60E664F20D5AC62"
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
    partial class loginIn : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // loginIn.xaml line 24
                {
                    this.loginMainBackGround = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // loginIn.xaml line 52
                {
                    this.loginAttempt = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.loginAttempt).Click += this.loginAttempt_Click;
                }
                break;
            case 4: // loginIn.xaml line 45
                {
                    this.fastLogin = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.fastLogin).Click += this.fastLogin_Click;
                }
                break;
            case 5: // loginIn.xaml line 43
                {
                    this.hostId = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6: // loginIn.xaml line 35
                {
                    this.pwdInput = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // loginIn.xaml line 32
                {
                    this.nicknameInput = (global::Windows.UI.Xaml.Controls.TextBox)(target);
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
            return returnValue;
        }
    }
}
