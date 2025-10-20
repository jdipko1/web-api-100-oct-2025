namespace SoftwareCenter.Tests;

[Trait("Category","Demo")]
public class UnitTest1
{
    [Fact]
    public void CanDotNetAddTenAndTwenty()
    {
        //given
        int a = 10; int b = 20; int answer;
        //when
        answer = a + b;
        //then
        Assert.Equal(30, answer);
    }
    
    [Theory]
    [InlineData(10,20,30)]
    [InlineData(2, 2, 4)]
    [InlineData(3, 3, 6)]
    public void CanAddAnyIntegerTogether(int a, int b, int expected)
    {
        var answer = a + b;
        Assert.Equal(expected, answer);
    }

    [Fact]
    public void Test1()
    {
        //ctrl r  a run all tests
        //ctrl r ctrl a debug all tests

        //given
        //when
        //then
    }
}
