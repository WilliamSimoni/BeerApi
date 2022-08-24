using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeerApi.Test.Helpers
{
    public static class ValidationTestHelper
    {
        /// <summary>
        /// Returns true if the object is valid according to its data annotation
        /// </summary>
        /// <returns></returns>
        public static bool Validate<T>(T obj)
        {
            var context = new ValidationContext(obj, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(T), typeof(T)), typeof(T));

            return Validator.TryValidateObject(obj, context, results, true);
        }
    }
}
