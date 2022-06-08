using Xunit;

namespace LinkedList.Tests;

public class DoublyLinkedListTests
{
    private readonly IDoublyLinkedList _sut;

    public DoublyLinkedListTests()
    {
        _sut = new DoublyLinkedList();
    }

    public static IEnumerable<object[]> LengthTestData()
    {
        yield return new object[] {0, Array.Empty<char>()};
        yield return new object[] {3, new[] {'a', 'b', 'c'}};
        yield return new object[] {12, "Hello world!".ToArray()};
    }

    [Theory]
    [MemberData(nameof(LengthTestData))]
    public void LengthMatchesElementsAmount(int expected, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var length = _sut.Length();
        Assert.Equal(expected, length);
    }

    [Theory]
    [InlineData('a', 0, 'a', 'b', 'c')]
    [InlineData('b', 1, 'a', 'b', 'c')]
    [InlineData('o', 4, 'H', 'e', 'l', 'l', 'o')]
    public void ReturnsElementAtIndex
        (char expected, int index, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var gotElement = _sut.Get(index);
        Assert.Equal(expected, gotElement);
    }

    [Theory]
    [InlineData(-1, 'a', 'b', 'c')]
    [InlineData(9, 'a', 'b', 'c')]
    public void ThrowsOnGettingInvalidIndex(int index, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var getElement = () => { _sut.Get(index); };
        Assert.Throws<IndexOutOfRangeException>(getElement);
    }

    [Theory]
    [InlineData('x', 0, 'a', 'b', 'c')]
    [InlineData('x', 1, 'a', 'b', 'c')]
    [InlineData('x', 4, 'H', 'e', 'l', 'l', 'o')]
    public void InsertsElementAtIndex
        (char inserted, int index, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var lengthBefore = _sut.Length();
        _sut.Insert(inserted, index);
        var gotElement = _sut.Get(index);
        Assert.Equal(inserted, gotElement);
        var lengthAfter = _sut.Length();
        Assert.Equal(lengthBefore + 1, lengthAfter);
    }

    [Theory]
    [InlineData(-1, 'a', 'b', 'c')]
    [InlineData(9, 'a', 'b', 'c')]
    public void ThrowsOnInsertingAtInvalidIndex(int index, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var insertElement = () => { _sut.Insert('x', index); };
        Assert.Throws<IndexOutOfRangeException>(insertElement);
    }

    [Theory]
    [InlineData('a', 0, 'a', 'b', 'c')]
    [InlineData('b', 1, 'a', 'b', 'c')]
    [InlineData('o', 4, 'H', 'e', 'l', 'l', 'o')]
    public void DeletesElementAtIndex
        (char expected, int index, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var lengthBefore = _sut.Length();
        var deleted = _sut.Delete(index);
        Assert.Equal(expected, deleted);
        var lengthAfter = _sut.Length();
        Assert.Equal(lengthBefore - 1, lengthAfter);
    }

    [Theory]
    [InlineData(-1, 'a', 'b', 'c')]
    [InlineData(9, 'a', 'b', 'c')]
    public void ThrowsOnDeletingInvalidIndex(int index, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var deleteElement = () => { _sut.Delete(index); };
        Assert.Throws<IndexOutOfRangeException>(deleteElement);
    }

    [Theory]
    [InlineData(2, 'a', 'a', 'b', 'c')]
    [InlineData(2, 'b', 'a', 'b', 'c')]
    [InlineData(3, 'l', 'H', 'e', 'l', 'l', 'o')]
    public void DeletesAllElements
        (int expectedLength, char toDelete, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        _sut.DeleteAll(toDelete);
        var lengthAfter = _sut.Length();
        Assert.Equal(expectedLength, lengthAfter);
    }

    [Theory]
    [InlineData]
    [InlineData('a', 'a', 'b', 'c')]
    [InlineData('b', 'a', 'b', 'c')]
    [InlineData('l', 'H', 'e', 'l', 'l', 'o')]
    public void Clones(params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }
        
        var clone = _sut.Clone();
        var selfLengthBefore = _sut.Length();
        var cloneLengthBefore = clone.Length();
        clone.Append('x');
        var selfLengthAfter = _sut.Length();
        var cloneLengthAfter = clone.Length();
        Assert.Equal(selfLengthBefore, selfLengthAfter);
        Assert.Equal(cloneLengthBefore + 1, cloneLengthAfter);
    }

    [Theory]
    [InlineData('a', 'b', 'c')]
    [InlineData('H', 'e', 'l', 'l', 'o')]
    public void Reverses(params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }
        
        var snapshot = _sut.Clone();
        var length = _sut.Length();
        _sut.Reverse();
        for (int i = 0; i < length; i++)
        {
            var element = _sut.Get(i);
            var symmetric = snapshot.Get(length - 1 - i);
            Assert.Equal(element, symmetric);
        }
    }

    [Theory]
    [InlineData(0, 'a', 'a', 'b', 'c')]
    [InlineData(1, 'b', 'a', 'b', 'c')]
    [InlineData(2, 'l', 'H', 'e', 'l', 'l', 'o')]
    [InlineData(0, 'l', 'l', 'l', 'l', 'l', 'l')]
    [InlineData(-1, 'x', 'H', 'e', 'l', 'l', 'o')]
    [InlineData(-1, 'x')]
    public void FindsFirst(int expected, char toFind, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var foundElement = _sut.FindFirst(toFind);
        Assert.Equal(expected, foundElement);
    }
    
    [Theory]
    [InlineData(0, 'a', 'a', 'b', 'c')]
    [InlineData(1, 'b', 'a', 'b', 'c')]
    [InlineData(3, 'l', 'H', 'e', 'l', 'l', 'o')]
    [InlineData(4, 'l', 'l', 'l', 'l', 'l', 'l')]
    [InlineData(-1, 'x', 'H', 'e', 'l', 'l', 'o')]
    [InlineData(-1, 'x')]
    public void FindsLast(int expected, char toFind, params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        var foundElement = _sut.FindLast(toFind);
        Assert.Equal(expected, foundElement);
    }
    
    [Theory]
    [InlineData('a', 'b', 'c')]
    [InlineData('H', 'e', 'l', 'l', 'o')]
    public void Clears(params char[] elements)
    {
        foreach (var element in elements)
        {
            _sut.Append(element);
        }

        _sut.Clear();
        var length = _sut.Length();
        Assert.Equal(0, length);
    }
    
    [Theory]
    [InlineData(new[] {'a', 'b', 'c'}, new[] {'a', 'b', 'c'})]
    [InlineData(new[] {'H', 'e', 'l', 'l', 'o'}, new[] {'w', 'o', 'r', 'l', 'd'})]
    public void ExtendsWithAnotherList(char[] list1Elements, char[] list2Elements)
    {
        foreach (var element in list1Elements)
            _sut.Append(element);

        var list2 = new DoublyLinkedList();
        foreach (var element in list2Elements)
            list2.Append(element);
            
        var list1Length = _sut.Length();
        var list2Length = list2.Length();
        _sut.Extend(list2);
        var list1LengthAfterExtend = _sut.Length();
        Assert.Equal(list1Length + list2Length, list1LengthAfterExtend);
        list2.Append('x');
        var list1LengthAfterAppend = _sut.Length();
        Assert.Equal(list1LengthAfterExtend, list1LengthAfterAppend);
    }
}