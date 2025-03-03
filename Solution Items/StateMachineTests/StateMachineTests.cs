using NUnit.Framework;
using System;
using System.Collections.Generic;
using StateMachine;

[TestFixture]
public class StateMachineTests
{
    private StateMachine<string, string> _stateMachine;

    [SetUp]
    public void SetUp()
    {
        _stateMachine = new StateMachine<string, string>();
    }

    [Test]
    public void WhenSetInitialState_ThenCurrentStateIsSet()
    {
        _stateMachine.SetInitialState("State1");
        Assert.That(_stateMachine.CurrentState, Is.EqualTo("State1"));
    }

    [Test]
    public void WhenSetInitialStateTwice_ThenThrowsInvalidOperationException()
    {
        _stateMachine.SetInitialState("State1");
        Assert.Throws<InvalidOperationException>(() => _stateMachine.SetInitialState("State2"));
    }

    [Test]
    public void WhenAddTransition_ThenTransitionIsAdded()
    {
        _stateMachine.SetInitialState("State1");
        _stateMachine.AddTransition("State1", "Trigger1", "State2");
        Assert.That(_stateMachine.CanFire("Trigger1"), Is.True);
    }

    [Test]
    public void WhenAddTransitionTwice_ThenThrowsInvalidOperationException()
    {
        _stateMachine.AddTransition("State1", "Trigger1", "State2");
        Assert.Throws<InvalidOperationException>(() => _stateMachine.AddTransition("State1", "Trigger1", "State3"));
    }

    [Test]
    public void WhenAddTransitionWithNewFromState_ThenDictionaryIsCreated()
    {
        _stateMachine.SetInitialState("State1");
        _stateMachine.AddTransition("State1", "Trigger1", "State2");
        _stateMachine.AddTransition("State2", "Trigger2", "State3");
        
        // Fire to move to State2
        _stateMachine.Fire("Trigger1");
        Assert.That(_stateMachine.CurrentState, Is.EqualTo("State2"));
        
        // Verify new dictionary was created for State2
        Assert.That(_stateMachine.CanFire("Trigger2"), Is.True);
    }

    [Test]
    public void WhenCanFireWithValidTrigger_ThenReturnsTrue()
    {
        _stateMachine.SetInitialState("State1");
        _stateMachine.AddTransition("State1", "Trigger1", "State2");
        Assert.That(_stateMachine.CanFire("Trigger1"), Is.True);
    }

    [Test]
    public void WhenCanFireWithInvalidTrigger_ThenReturnsFalse()
    {
        _stateMachine.SetInitialState("State1");
        Assert.That(_stateMachine.CanFire("Trigger1"), Is.False);
    }

    [Test]
    public void WhenCanFireWithoutInitialState_ThenThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _stateMachine.CanFire("Trigger1"));
    }

    [Test]
    public void WhenFireWithValidTrigger_ThenStateIsChanged()
    {
        _stateMachine.SetInitialState("State1");
        _stateMachine.AddTransition("State1", "Trigger1", "State2");
        var result = _stateMachine.Fire("Trigger1");
        Assert.That(_stateMachine.CurrentState, Is.EqualTo("State2"));
        Assert.That(result, Is.EqualTo("State2"));
    }

    [Test]
    public void WhenFireWithInvalidTrigger_ThenThrowsInvalidOperationException()
    {
        _stateMachine.SetInitialState("State1");
        var ex = Assert.Throws<InvalidOperationException>(() => _stateMachine.Fire("Trigger1"));

        Assert.That(ex.Message, Is.EqualTo("Cannot transition from state State1 with trigger Trigger1"));
    }

    [Test]
    public void WhenFireWithoutInitialState_ThenThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _stateMachine.Fire("Trigger1"));
    }
}