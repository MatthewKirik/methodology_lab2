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

}