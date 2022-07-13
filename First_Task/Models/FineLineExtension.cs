using System.Drawing;

namespace First_Task.Models
{
    public static class FineLineExtension
    {
        public static bool IsValidColor(this string color)
        {
            return Color.FromName(color).IsKnownColor;
        }

        public static void Validate(this FileLine fileLine)
        {
            try
            {
                if (!fileLine.Color.IsValidColor())
                {
                    throw new InvalidOperationException("Color is not valid.");
                }

                if (string.IsNullOrWhiteSpace(fileLine.Label))
                {
                    throw new InvalidOperationException("Label can't be empty.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
