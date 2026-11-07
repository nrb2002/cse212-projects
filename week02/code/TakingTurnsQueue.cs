
using System;

public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the queue with a name and number of turns.
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them.
    /// People with finite turns are re-enqueued if they have turns remaining.
    /// People with zero or negative turns are re-enqueued indefinitely.
    /// An exception is thrown if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        // Remove the person at the front of the queue.
        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // Zero or negative turns mean infinite turns.
            // Re-enqueue the person without changing their turn count.
            _people.Enqueue(person);
        }
        else
        {
            // Decrease the number of remaining finite turns.
            person.Turns--;

            // Re-enqueue the person only if turns remain.
            if (person.Turns > 0)
            {
                _people.Enqueue(person);
            }
        }

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
