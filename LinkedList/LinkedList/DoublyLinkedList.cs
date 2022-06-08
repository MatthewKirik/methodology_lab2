namespace LinkedList;

public class DoublyLinkedList : IDoublyLinkedList
{
    private class Node
    {
        public char Value { get; set; }
        public Node? Prev { get; set; }
        public Node? Next { get; set; }
    }

    private Node? _head;
    private Node? _tail;

    public int Length()
    {
        var length = 0;
        var current = _head;
        while (current != null)
        {
            length++;
            current = current.Next;
        }

        return length;
    }

    public void Append(char element)
    {
        var node = new Node
        {
            Value = element
        };
        if (_head == null || _tail == null)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            _tail.Next = node;
            node.Prev = _tail;
            _tail = node;
        }
    }

    public void Insert(char element, int index)
    {
        if (index < 0)
            throw new IndexOutOfRangeException("Index was negative");
        var node = new Node {Value = element};
        int currentIx = 0;
        var current = _head;
        while (current != null)
        {
            if (currentIx == index)
            {
                var prev = current.Prev;
                current.Prev = node;
                node.Next = current;
                node.Prev = prev;
                if (prev != null) prev.Next = node;
                if (current == _head) _head = node;
                return;
            }

            current = current.Next;
            currentIx++;
        }

        if (currentIx != index) throw new IndexOutOfRangeException("Element with such index does not exist");
        _tail.Next = node;
        node.Prev = _tail;
        _tail = node;
    }

    public char Delete(int index)
    {
        if (index < 0)
            throw new IndexOutOfRangeException("Index was negative");
        int currentIx = 0;
        var current = _head;
        while (current != null)
        {
            if (currentIx == index)
            {
                if (current.Prev != null)
                    current.Prev.Next = current.Next;
                if (current.Next != null)
                    current.Next.Prev = current.Prev;
                if (current == _head)
                    _head = current.Next;
                if (current == _tail)
                    _tail = current.Prev;
                return current.Value;
            }
            current = current.Next;
            currentIx++;
        }
        throw new IndexOutOfRangeException("Element with such index does not exist");
    }

    public void DeleteAll(char element)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Value == element)
            {
                if (current.Prev != null)
                    current.Prev.Next = current.Next;
                if (current.Next != null)
                    current.Next.Prev = current.Prev;
                if (current == _head)
                    _head = current.Next;
                if (current == _tail)
                    _tail = current.Prev;
            }
            current = current.Next;
        }
    }

    public char Get(int index)
    {
        if (index < 0)
            throw new IndexOutOfRangeException("Index was negative");
        int currentIx = 0;
        var current = _head;
        while (current != null)
        {
            if (currentIx == index) return current.Value;
            current = current.Next;
            currentIx++;
        }
        throw new IndexOutOfRangeException("Element with such index does not exist");
    }

    public IDoublyLinkedList Clone()
    {
        var clone = new DoublyLinkedList();
        var current = _head;
        while (current != null)
        {
            clone.Append(current.Value);
            current = current.Next;
        }
        return clone;
    }

    public void Reverse()
    {
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            current.Next = current.Prev;
            current.Prev = next;
            current = next;
        }
        (_head, _tail) = (_tail, _head);
    }

    public int FindFirst(char element)
    {
        int currentIx = 0;
        var current = _head;
        while (current != null)
        {
            if (current.Value == element)
                return currentIx;
            current = current.Next;
            currentIx++;
        }
        return -1;
    }

    public int FindLast(char element)
    {
        int currentIx = Length() - 1;
        var current = _tail;
        while (current != null)
        {
            if (current.Value == element)
                return currentIx;
            current = current.Prev;
            currentIx--;
        }
        return -1;
    }

    public void Clear()
    {
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            current.Next = null;
            current.Prev = null;
            current = next;
        }
        _head = null;
        _tail = null;
    }

    public void Extend(IDoublyLinkedList elements)
    {
        throw new NotImplementedException();
    }
}