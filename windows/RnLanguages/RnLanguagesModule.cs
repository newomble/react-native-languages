using ReactNative.Bridge;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

/**
 * This is stubbed so I can move on while using this machine.
 * Event binding is implemented
 */
namespace Rn.Languages.RnLanguages
{
    /// <summary>
    /// A module that allows JS to share data.
    /// </summary>
    class RnLanguagesModule : NativeModuleBase
    {
        /// <summary>
        /// Instantiates the <see cref="RnLanguagesModule"/>.
        /// </summary>
        internal RnLanguagesModule()
        {

        }

        /// <summary>
        /// The name of the native module.
        /// </summary>
        public override string Name
        {
            get
            {
                return "RnLanguages";
            }
        }

        //copied from rn-i18n module
        private IList GetLocaleList()
        {
            var returnList = new List<string>();
            var langList = Windows.System.UserProfile.GlobalizationPreferences.Languages;
            var langListEnum = langList.GetEnumerator();
            while (langListEnum.MoveNext())
            {
                var curr = langListEnum.Current;
                returnList.Add(curr);
            }

            return returnList.ToArray();
        }

        public override IReadOnlyDictionary<string, object> Constants
        {
            get
            {
                var locals = this.GetLocaleList();
                return new Dictionary<string, object>
                {
                    {"languages",  locals},
                    {"language", locals[0]}
                };
            }
        }
    }
}
