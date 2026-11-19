public static HashSet<int> FindIntersection(HashSet<int> set1, HashSet<int> set2)
{
    HashSet<int> result = new HashSet<int>();

    foreach (int item in set1)
    {
        if (set2.Contains(item))
        {
            result.Add(item);
        }
    }
    return result;
}

public static HashSet<int> FindUnion(HashSet<int> set1, HashSet<int> set2)
{
    HashSet<int> result = new HashSet<int>(set1);
    
    foreach (int item in set1)
    {
        result.Add(item);
    }
    foreach (int item in set2)
    {
        result.Add(item);
    }
    
    return result;
}