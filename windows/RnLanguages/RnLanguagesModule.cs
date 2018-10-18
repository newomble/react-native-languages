using ReactNative;
using ReactNative.Bridge;
using ReactNative.Modules.Core;
using System;
using System.Collections.Generic;
using Windows.UI.Text.Core;

namespace Rn.Languages.RnLanguages
{
    /// <summary>
    /// A module that allows JS to share data.
    /// </summary>
    class RnLanguagesModule : NativeModuleBase
    {
        private ReactContext context;

        /// <summary>
        /// Instantiates the <see cref="RnLanguagesModule"/>.
        /// </summary>
        internal RnLanguagesModule(ReactContext _context)
        {
            this.context = _context;
            CoreTextServicesManager.GetForCurrentView().InputLanguageChanged += 
                OnLanguageChange;
        }

        /// <summary>
        /// The name of the native module.
        /// </summary>
        public override string Name
        {
            get
            {
                return "RNLanguages";
            }
        }

        private Dictionary<string, object> GetLanguageOutput()
        {
            var returnList = new List<string>();
            var langList = Windows.System.UserProfile.GlobalizationPreferences.Languages;
            var langListEnum = langList.GetEnumerator();
            while (langListEnum.MoveNext())
            {
                var curr = langListEnum.Current;
                returnList.Add(curr);
            }

            var locals = returnList.ToArray();
            return new Dictionary<string, object>
            {
                {"languages",  locals},
                {"language", locals[0]}
            };
        }

        public override IReadOnlyDictionary<string, object> Constants
        {
            get
            {
                return this.GetLanguageOutput();
            }
        }

        private void OnLanguageChange(CoreTextServicesManager sender, object args)
        {
            context.GetJavaScriptModule<RCTDeviceEventEmitter>()
                .emit("languagesDidChange", this.GetLanguageOutput());
        }
    }
}
