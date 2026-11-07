
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Add Bob (2 turns), Tim (5 turns), and Sue (3 turns).
    // Expected Result: Each person gets their turns in FIFO order.
    // Bob is removed after 2 turns, Sue after 3 turns, and Tim after 5 turns.
    // Defect(s) Found: The original implementation did not correctly
    // handle people with zero or negative turns. This test checks finite turns.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 2);
        queue.AddPerson("Tim", 5);
        queue.AddPerson("Sue", 3);

        var expected = new[] {
            "Bob", "Tim", "Sue", "Bob", "Tim",
            "Sue", "Tim", "Sue", "Tim", "Tim"
        };

        foreach (var name in expected)
        {
            Assert.AreEqual(name, queue.GetNextPerson().Name);
        }

        Assert.AreEqual(0, queue.Length);
    }

    [TestMethod]
    // Scenario: Add Bob (2 turns), Tim (5 turns), and Sue (3 turns).
    // After five dequeues, add George (3 turns).
    // Expected Result: George joins at the back of the queue.
    // Existing people retain their FIFO order and remaining turns.
    // Defect(s) Found: Incorrect re-enqueue behavior can disrupt
    // the expected order when a new person is added midway.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 2);
        queue.AddPerson("Tim", 5);
        queue.AddPerson("Sue", 3);

        var expected = new[] {
            "Bob", "Tim", "Sue", "Bob", "Tim"
        };

        foreach (var name in expected)
        {
            Assert.AreEqual(name, queue.GetNextPerson().Name);
        }

        queue.AddPerson("George", 3);

        var expectedAfterAdding = new[] {
            "Sue", "Tim", "George", "Sue", "Tim",
            "George", "Tim", "George"
        };

        foreach (var name in expectedAfterAdding)
        {
            Assert.AreEqual(name, queue.GetNextPerson().Name);
        }

        Assert.AreEqual(0, queue.Length);
    }

    [TestMethod]
    // Scenario: Add Bob (2 turns), Tim (0 turns), and Sue (3 turns).
    // Expected Result: Tim remains in the queue indefinitely.
    // His Turns property remains 0 after each turn.
    // Defect(s) Found: The original implementation removed people
    // with zero turns instead of treating them as infinite-turn people.
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Bob", 2);
        queue.AddPerson("Tim", 0);
        queue.AddPerson("Sue", 3);

        var expected = new[] {
            "Bob", "Tim", "Sue", "Bob", "Tim",
            "Sue", "Tim", "Sue", "Tim", "Tim"
        };

        foreach (var name in expected)
        {
            Assert.AreEqual(name, queue.GetNextPerson().Name);
        }

        var person = queue.GetNextPerson();
        Assert.AreEqual("Tim", person.Name);
        Assert.AreEqual(0, person.Turns);
    }

    [TestMethod]
    // Scenario: Add Tim (-3 turns) and Sue (3 turns).
    // Expected Result: Tim remains in the queue indefinitely.
    // His Turns property remains -3 after each turn.
    // Defect(s) Found: The original implementation did not re-enqueue
    // people with negative turns.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var queue = new TakingTurnsQueue();
        queue.AddPerson("Tim", -3);
        queue.AddPerson("Sue", 3);

        var expected = new[] {
            "Tim", "Sue", "Tim", "Sue", "Tim",
            "Sue", "Tim", "Tim", "Tim", "Tim"
        };

        foreach (var name in expected)
        {
            Assert.AreEqual(name, queue.GetNextPerson().Name);
        }

        var person = queue.GetNextPerson();
        Assert.AreEqual("Tim", person.Name);
        Assert.AreEqual(-3, person.Turns);
    }

    [TestMethod]
    // Scenario: Call GetNextPerson on an empty queue.
    // Expected Result: An InvalidOperationException is thrown.
    // Defect(s) Found: The implementation must check for an empty
    // queue and throw the required exception.
    public void TestTakingTurnsQueue_Empty()
    {
        var queue = new TakingTurnsQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => queue.GetNextPerson()
        );

        Assert.AreEqual("No one in the queue.", exception.Message);
    }
}
