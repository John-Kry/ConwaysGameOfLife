using System.Linq;
using ConwaysGameOfLife;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests;

public class Tests
{
    public Rule rule = new();
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RulesImplemented()
    {
        bool[,] state = new bool[10, 10];
        state[5, 5] = true;
        state[6, 5] = true;
        state[7, 5] = true;

        Rule.GetNeighbors(state, new Position(6, 5)).Count((b =>b)).Should().Be(2);
        Rule.GetNeighbors(state, new Position(5, 5)).Count((b =>b)).Should().Be(1);
        Rule.GetNeighbors(state, new Position(7, 5)).Count((b =>b)).Should().Be(1);
        Rule.GetNeighbors(state, new Position(5, 6)).Count((b =>b)).Should().Be(2);
        Rule.GetNeighbors(state, new Position(5, 4)).Count((b =>b)).Should().Be(2);
        
        Rule.GetNeighbors(state, new Position(6, 6)).Count((b =>b)).Should().Be(3);

        rule.ShouldLive(state, new Position(6, 5)).Should().Be(true);
        rule.ShouldLive(state, new Position(6, 6)).Should().Be(true);
        rule.ShouldLive(state, new Position(6, 4)).Should().Be(true);

        Rule.GetPosition(state, new Position(7, 5)).Should().Be(true);
        
        Rule.GetPosition(state, new Position(-100, 7)).Should().Be(false);
        Assert.Pass();
    }
}