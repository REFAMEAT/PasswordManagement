using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace REFame.PasswordManagement.Localization
{
    /// <summary>
    /// Provide information about supported languages or register supported languages
    /// </summary>
    public class AppLocalization
    {
        private static AppLocalization current;
     
        public static AppLocalization Current => current ??= new AppLocalization();

        public List<CultureInfo> GetPossibleLanguages()
        {
            var resourceManager = new ResourceManager(typeof(Loc));
            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var possibleCultures = new List<CultureInfo>();

            foreach (CultureInfo cultureInfo in allCultures)
            {
                try
                {
                    if (cultureInfo.Equals(CultureInfo.InvariantCulture))
                    {
                        continue;
                    }

                    ResourceSet resourceSet = resourceManager.GetResourceSet(cultureInfo, true, false);

                    if (resourceSet != null)
                    {
                        possibleCultures.Add(cultureInfo);
                    }
                }
                catch (CultureNotFoundException )
                {
                    // skip
                }
            }

            return possibleCultures;
        }
    }
}