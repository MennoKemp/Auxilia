namespace Auxilia.Utilities
{
    internal class Setting
    {
        private string _value;

        public Setting(string name, string value)
        {
            Name = name ?? string.Empty;
            Value = value ?? string.Empty;
        }

        public string Name { get; }
        public string Value
        {
            get => _value;
            set => _value = value ?? string.Empty;
        }

        /// <summary>
        /// Returns object string.
        /// </summary>
        /// <returns>Object string.</returns>
        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}
