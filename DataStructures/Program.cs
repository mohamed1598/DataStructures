using DataStructures;
using System.Collections.Generic;

int[] inputArray = new int[] { 1, 4, 3, 2, 4, 5, 7, 9, 8 };
DynamicArray<int> ints = new DynamicArray<int>(inputArray);
testInserting();
displayList("intial");

ints.Reverse();
displayList("after reversing");

ints.Sort();
displayList("after sorting");

ints.Reverse();
displayList("after reversing");


void testInserting()
{
    ints[ints.Length] = 40;
    ints[ints.Length + 3] = 10;
    ints[50] = 20;
    ints[60] = 30;
}

void displayList(string message)
{
    Console.WriteLine(message);

    foreach (var item in ints)
    {
        Console.WriteLine(item);
    }

    Console.WriteLine("another check for index");
    for(int i = 0;i< ints.Length; i++)
    {
        Console.WriteLine(ints[i]);

    }

}

