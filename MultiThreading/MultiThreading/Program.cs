using System;
using System.Threading;
using System.Threading.Tasks;

class CustomData
{
    public long CreationTime;
    public int Name;
    public int ThreadNum;
}

public class Example
{
    public static void Main()
    {
        // Create the task object by using an Action(Of Object) to pass in the loop
        // counter. This produces an unexpected result.
        Task[] taskArray = new Task[10];
        for (int i = 0; i < taskArray.Length; i++)
        {
            taskArray[i] = Task.Factory.StartNew((Object obj) =>
                {

                    var data =  obj as CustomData;

                    if (data != null)
                    {
                        data.ThreadNum = Thread.CurrentThread.ManagedThreadId;

                        Console.WriteLine("Task #{0} created at {1} on thread #{2}.",
                            data.Name, data.CreationTime, data.ThreadNum);
                    }
                },
                new CustomData { Name=i});
        }
        Task.WaitAll(taskArray);

        foreach (var task in taskArray)
        {
            var i = (CustomData)task.AsyncState;
            Console.WriteLine(i.Name);

        }
    }
}
// The example displays output like the following:
//       Task #10 created at 635116418427727841 on thread #4.
//       Task #10 created at 635116418427737842 on thread #4.
//       Task #10 created at 635116418427737842 on thread #4.
//       Task #10 created at 635116418427737842 on thread #4.
//       Task #10 created at 635116418427737842 on thread #4.
//       Task #10 created at 635116418427737842 on thread #4.
//       Task #10 created at 635116418427727841 on thread #3.
//       Task #10 created at 635116418427747843 on thread #3.
//       Task #10 created at 635116418427747843 on thread #3.
//       Task #10 created at 635116418427737842 on thread #4.