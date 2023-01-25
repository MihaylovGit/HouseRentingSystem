namespace HouseRentingSystem.Data
{
    public static class DataConstants
    {
        public static class Category
        {
            public const int CategoryNameMaxLength = 50;
        }

        public static class House
        {
            public const int HouseTitleMinLength = 10;

            public const int HouseTitleMaxLength = 50;

            public const int HouseAddressMinLength = 30;

            public const int HouseAddressMaxLength = 150;

            public const int HouseDescriptionMinLength = 50;

            public const int HouseDescriptionMaxLength = 500;

            public const string HousePricePerMonthMinValue = "0";

            public const string HousePricePerMonthMaxValue = "2000";
        }

        public static class Agent
        {
            public const int AgentPhoneNumberMinLength = 7;

            public const int AgentPhoneNumberMaxLength = 15;
        }
    }
}
