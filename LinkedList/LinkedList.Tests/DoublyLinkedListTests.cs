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
        yield return new object[]{0, Array.Empty<char>() };
        yield return new object[]{3, new[] {'a', 'b', 'c'} };
        yield return new object[]{12, "Hello world!".ToArray() };
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
    
}