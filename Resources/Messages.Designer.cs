﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
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
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Please enter your address.
        /// </summary>
        public static string AddressError {
            get {
                return ResourceManager.GetString("AddressError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter your city.
        /// </summary>
        public static string CityError {
            get {
                return ResourceManager.GetString("CityError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter your Country.
        /// </summary>
        public static string CountryError {
            get {
                return ResourceManager.GetString("CountryError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The email address is not entered in a correct format.
        /// </summary>
        public static string EmailAddressError {
            get {
                return ResourceManager.GetString("EmailAddressError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have been locked out,Please Wait for 5 minutes.
        /// </summary>
        public static string LockOutError {
            get {
                return ResourceManager.GetString("LockOutError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter your phone number.
        /// </summary>
        public static string PhoneNumberError {
            get {
                return ResourceManager.GetString("PhoneNumberError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This Is Required,.
        /// </summary>
        public static string RequiredValidationErrorMessage {
            get {
                return ResourceManager.GetString("RequiredValidationErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter your State.
        /// </summary>
        public static string StateError {
            get {
                return ResourceManager.GetString("StateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid username or password!.
        /// </summary>
        public static string WrongPasswordError {
            get {
                return ResourceManager.GetString("WrongPasswordError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please enter your zip code.
        /// </summary>
        public static string ZipCodeError {
            get {
                return ResourceManager.GetString("ZipCodeError", resourceCulture);
            }
        }
    }
}
