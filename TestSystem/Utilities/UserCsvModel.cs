using FileHelpers;

namespace TestSystem.Utilities
{
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    public class UserCsvModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Group { get; set; }
        public string Role { get; set; }
    }
}