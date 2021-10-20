using System;

namespace Auxilia.Graphs
{
    public class Node : IEquatable<Node>, IComparable<Node>
    {
        public Node(int id)
        {
            Id = id;
        }

        public int Id { get; }
        
        public int CompareTo(Node other)
        {
            return other == null
                ? 1
                : Id.CompareTo(other.Id);
        }

        public bool Equals(Node other)
        {
            return Id.Equals(other?.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
