﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PolynomialDivision.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PolynomialDivision.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Both {0} and {1} were zero but {2} was false..
        /// </summary>
        public static string FullZeroAllowedWithInvalidRange {
            get {
                return ResourceManager.GetString("FullZeroAllowedWithInvalidRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to \r\ndividend: {0}\r\ndivisor: {1}\r\ndvndP: {2}\r\ndvsr: {3}\r\n{4} / {5} = {6}.
        /// </summary>
        public static string InfinityOrNaN {
            get {
                return ResourceManager.GetString("InfinityOrNaN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid dividend..
        /// </summary>
        public static string InvalidDividend {
            get {
                return ResourceManager.GetString("InvalidDividend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid divisor..
        /// </summary>
        public static string InvalidDivisor {
            get {
                return ResourceManager.GetString("InvalidDivisor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value cannot be negative..
        /// </summary>
        public static string ValueCannotBeNegative {
            get {
                return ResourceManager.GetString("ValueCannotBeNegative", resourceCulture);
            }
        }
    }
}