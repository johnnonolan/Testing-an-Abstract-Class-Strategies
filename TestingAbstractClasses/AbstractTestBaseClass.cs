using Moq;
using Xunit;

namespace TestingAbstractClasses
{
    public abstract class AbstractTestBaseClass
    {
        protected MyAbstractClass AbstractedVariable;
        [Fact]
        public void Should_return_correct_message()
        {
            var result= AbstractedVariable.MyMethodOnAbstractClass("input");
            Assert.Equal("I like input in an abstract sense.", result);
        }
    }

    public class TestingAConcreteClass :AbstractTestBaseClass
    {
        MyConcreteClass ConcreteclassUnderTest;
        public TestingAConcreteClass()
        {
            AbstractedVariable = new MyConcreteClass();
            ConcreteclassUnderTest = new MyConcreteClass();
        }

        [Fact]
        public void Should_say_Hello()
        {
            //this is less than ideal as we need to cast to concrete class each time
            var result =  ((MyConcreteClass) AbstractedVariable).SayHello();
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void StillShould_say_Hello()
        {
            //this is better although in constructor we still have 
            //to construct a variable for the abstract tests.
            var result = ConcreteclassUnderTest.SayHello();
            Assert.Equal("Hello", result);

        }

    }

    public class TestingAnAbstractClassWithAMock
    {
        [Fact] 
        public void Should_return_correct_message()
        {
            
            var mock = new Mock<MyAbstractClass>();
            var result = mock.Object.MyMethodOnAbstractClass("fish");
            Assert.Equal("I like fish in an abstract sense.", result);            
        }
    }

    public class TestingAnAbstractClassWithAFake
    {

        private class FakeConcreteInstance : MyAbstractClass
        {
            
        }


        [Fact]
        public void Should_return_correct_message()
        {
            var fake = new FakeConcreteInstance();
            var result = fake.MyMethodOnAbstractClass("fish");
            Assert.Equal("I like fish in an abstract sense.", result);
        }
    }


    public class MyConcreteClass : MyAbstractClass
    {
        public string SayHello()
        {
            return "Hello";
        }
    }

    public class MyAbstractClass
    {
        public string MyMethodOnAbstractClass(string input)
        {
            return "I like " + input + " in an abstract sense.";
        }
    }
}