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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void Reverse()
    {
        throw new NotImplementedException();
    }

    public int FindFirst(char element)
    {
        throw new NotImplementedException();
    }

    public int FindLast(char element)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Extend(IDoublyLinkedList elements)
    {
        throw new NotImplementedException();
    }
}