using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add Alice, Bob, and Charlie with the same priority (2).
    // Expected Result: They are removed in the same order they were added: Alice, Bob, Charlie. 
    // Defect(s) Found: Expected Alice but returned Bob.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        // Assert.Fail("Implement the test case and then remove this.");

        priorityQueue.Enqueue("Alice", 2);
        priorityQueue.Enqueue("Bob", 2);
        priorityQueue.Enqueue("Charlie", 2);

        Assert.AreEqual("Alice", priorityQueue.Dequeue());
        Assert.AreEqual("Bob", priorityQueue.Dequeue());
        Assert.AreEqual("Charlie", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add Alice (priority 1), Bob (priority 5),
    // and Charlie (priority 3).
    // Expected Result: Bob, Charlie, Alice.
    // The highest priority item must be removed first. 
    // Defect(s) Found: Expected Charlie but returned Bob. 
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        // Assert.Fail("Implement the test case and then remove this.");

        priorityQueue.Enqueue("Alice", 1);
        priorityQueue.Enqueue("Bob", 5);
        priorityQueue.Enqueue("Charlie", 3);

        Assert.AreEqual("Bob", priorityQueue.Dequeue());
        Assert.AreEqual("Charlie", priorityQueue.Dequeue());
        Assert.AreEqual("Alice", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Add Alice (3), Bob (5), Charlie (5), and David (2).
    // Expected Result: Bob, Charlie, Alice, David.
    // The two highest-priority items must be removed first,
    // with FIFO order preserved between Bob and Charlie.
    // Defect(s) Found: Expected Bob but returned Charlie. 
    public void TestPriorityQueue_HighestPriorityWithFIFO()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Alice", 3);
        priorityQueue.Enqueue("Bob", 5);
        priorityQueue.Enqueue("Charlie", 5);
        priorityQueue.Enqueue("David", 2);

        Assert.AreEqual("Bob", priorityQueue.Dequeue());
        Assert.AreEqual("Charlie", priorityQueue.Dequeue());
        Assert.AreEqual("Alice", priorityQueue.Dequeue());
        Assert.AreEqual("David", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Call Dequeue on an empty queue.
    // Expected Result: InvalidOperationException with the message
    // "The queue is empty."
    // Defect(s) Found: 
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue()
        );

        Assert.AreEqual("The queue is empty.", exception.Message);
    }

}