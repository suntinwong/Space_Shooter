﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _2Dshootertutorial {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Defualt : global::System.Configuration.ApplicationSettingsBase {
        
        private static Defualt defaultInstance = ((Defualt)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Defualt())));
        
        public static Defualt Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UsingKinect {
            get {
                return ((bool)(this["UsingKinect"]));
            }
            set {
                this["UsingKinect"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("725")]
        public int _H {
            get {
                return ((int)(this["_H"]));
            }
            set {
                this["_H"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("725")]
        public int _W {
            get {
                return ((int)(this["_W"]));
            }
            set {
                this["_W"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int AsteroidMax {
            get {
                return ((int)(this["AsteroidMax"]));
            }
            set {
                this["AsteroidMax"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5")]
        public int EnemyMax {
            get {
                return ((int)(this["EnemyMax"]));
            }
            set {
                this["EnemyMax"] = value;
            }
        }
    }
}
