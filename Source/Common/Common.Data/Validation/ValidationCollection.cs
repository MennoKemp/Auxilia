using Auxilia.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Auxilia.Data
{
	public class ValidationCollection : IEnumerable<ValidationMessage>
    {
        private readonly List<ValidationMessage> _messages = new List<ValidationMessage>();

        public ValidationCollection()
        {

        }
        public ValidationCollection(IEnumerable<ValidationMessage> messages = null)
        {
            _messages = messages.ThrowIfNull(nameof(messages)).ToList();
        }

        public int Count
        {
            get => _messages.Count;
        }
        public bool IsReadOnly { get; } = false;

        public IReadOnlyCollection<ValidationMessage> Messages
        {
            get => _messages.AsReadOnly();
        }
        public bool IsValid
        {
            get => Messages.All(m => m.IsValid);
        }

        public IEnumerator<ValidationMessage> GetEnumerator()
        {
            return _messages.GetEnumerator();
        }

        public override string ToString()
        {
            return Messages.SelectStrings().Combine(Environment.NewLine);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _messages.GetEnumerator();
        }

        public void Add(ValidationMessage item)
        {
            item.ThrowIfNull(nameof(item));
            _messages.Add(item);
        }
        public void Add(string message, ValidationLevel level = ValidationLevel.Error)
        {
            _messages.Add(new ValidationMessage(message, level));
        }

        public void Clear()
        {
            _messages.Clear();
        }

        public bool Contains(ValidationMessage item)
        {
            return _messages.Contains(item);
        }

        public void CopyTo(ValidationMessage[] array, int arrayIndex)
        {
            _messages.CopyTo(array, arrayIndex);
        }

        public bool Remove(ValidationMessage item)
        {
            return _messages.Remove(item);
        }
    }
}
