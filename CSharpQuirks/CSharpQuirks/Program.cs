/******************************************************************************************
 * This is a file that tests various the get a better understanding of how C# works. 
 * It also serves as a reference for working code snippets of various functionality of C# 
 * (ie async and lambda functions)
******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpQuirks {
    // compile with: /unsafe
    // -> eight-click project -> Properties -> Build -> allow unsafe code

    struct MyStruct {
        public int my_field;
    }

    class MyTemplate<T> {
        T yo;
        public MyTemplate(T yo1) {
            yo = yo1;
        }
    }

    class MyClass : IDisposable {
        public int my_field;
        bool disposed;

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
            Console.WriteLine("MyClass Dispose");
        }

        // ctor
        public MyClass() {
            my_field = 2;
            Console.WriteLine("MyClass ctor");
        }
        // copy ctor
        public MyClass(MyClass my_class) {
            Console.WriteLine("MyClass copy ctor");
        }
        // dtor
        ~MyClass() {
            Console.WriteLine("MyClass dtor");
        }
    }

    class Program {
        /* New Features
        */

        /*
        */

        /* Done
         * string functions
         * return in using
         * return in try catch finally
         * C# for loop
         * lambda functions
         * pass class, struct, string, List, array by reference?
         * return class, struct, string, List, array by reference?
         * "copy ctor" class, struct, string, List, array by reference?
         * "assign" class, struct, string, List, array by reference?
         * data structures
         * are lists dynamic arrays
         * what are linkedlists in C#? LinkedList
         * binary search
         * templates
         * comparison function
         * C# async
         * enum
        */

        static void printArray(string[] arr) {
            if (arr != null) {
                Console.WriteLine(string.Join("-", arr));
            }
        }
        static void stringManipulation() {
            string yo = "This is a random sentence.";
            string[] arr = new string[4] { "T", "h", "i", "s" };

            printArray(arr.Reverse().ToArray());                       // Output: s-i-h-T
            Console.WriteLine(yo.IndexOf("is"));                       // Output: 2
            printArray(yo.Split(new string[1] { "is" }, StringSplitOptions.None));
            // Output: Th- - a random sentence.
            Console.WriteLine(string.Join("\t", yo.Split(new string[1] { "is" }, StringSplitOptions.None)));
            // Output: Th               a random sentence.
            Console.WriteLine(yo.StartsWith("This"));                  // Output: true
            Console.WriteLine(yo.EndsWith("sentence."));               // Output: true
            Console.WriteLine(yo.Contains("random"));                  // Output: true
            Console.WriteLine(yo.LastIndexOf("is"));                   // Output: 5
            Console.WriteLine(yo.Replace("i", "a"));                   // Output: Thas as a random sentence.
            Console.WriteLine(yo.TrimStart(new char[2] { 'T', 'h' })); // Output: is is a random sentence.
            Console.WriteLine(yo.TrimEnd(new char[2] { 'e', '.' }));   // Output: This is a random sentenc

            Console.WriteLine(yo);                                     // Output: This is a random sentence.
            Console.WriteLine("");
        }

        static int start = 0;
        static int forStart() {
            start++;
            return start;
        }
        static int end = 10;
        static int forEnd() {
            end--;
            return end;
        }
        static void forLoopTest() {
            int i = 0;
            start = 0;
            end = 10;
            for (i = forStart(); i < forEnd(); i++ ) {
                Console.WriteLine("New Iteration: i = " + i.ToString() + ", start = " + start + ", end = " + end);
            }
            Console.WriteLine("End of test: i = " + i.ToString() + ", start = " + start + ", end = " + end);
            Console.WriteLine("");

            /* Output
            New Iteration: i = 1, start = 1, end = 9
            New Iteration: i = 2, start = 1, end = 8
            New Iteration: i = 3, start = 1, end = 7
            New Iteration: i = 4, start = 1, end = 6
            End of test: i = 5, start = 1, end = 5
            */
        }

        static int returnInTryCatchFinallyHelper() {
            try {
                Console.WriteLine("Try");
                return 1;
            } catch {
                Console.WriteLine("Catch");
            } finally {
                Console.WriteLine("Finally");
            }
            Console.WriteLine("after catch block");
            return 0;
        }
        static int returnInUsingHelper() {
            using (MyClass mc = new MyClass()) {
                return 1;
            }
            Console.WriteLine("after using");
            return 0;
        }
        static void returnInTryCatchFinally() {
            Console.WriteLine(returnInTryCatchFinallyHelper().ToString());
            Console.WriteLine("");
            /* Output
            Try
            Finally
            1
            */
        }
        static void returnInUsing() {
            Console.WriteLine(returnInUsingHelper().ToString());
            Console.WriteLine("");
            /* Output
            MyClass ctor
            MyClass Dispose
            1
            */
        }

        static void lambdaFunctions() {
            // source: www.dotnetperls.com/lambda

            /*
            Lambda details (ie x => x # 2 != 0)
            x          The argument name.
            =>         Separates argument list from result expression.
            x % 2 !=0  Returns true if x is not even.
             */
            List<int> elements = new List<int>() { 10, 20, 31, 40 };
            // ... Find index of first odd element (ie 31).
            int oddIndex = elements.FindIndex(x => x % 2 != 0);
            Console.WriteLine(oddIndex);

            //
            // Use implicitly typed lambda expression.
            // ... Assign it to a Func instance.
            //
            Func<int, int> func1 = x => x + 1;
            //
            // Use lambda expression with statement body.
            //
            Func<int, int> func2 = x => { return x + 1; };
            //
            // Use formal parameters with expression body.
            //
            Func<int, int> func3 = (int x) => x + 1;
            //
            // Use parameters with a statement body.
            //
            Func<int, int> func4 = (int x) => { return x + 1; };
            //
            // Use multiple parameters.
            //
            Func<int, int, int> func5 = (x, y) => x * y;
            //
            // Use no parameters in a lambda expression.
            //
            Action func6 = () => Console.WriteLine();
            //
            // Use delegate method expression.
            //
            Func<int, int> func7 = delegate(int x) { return x + 1; };
            //
            // Use delegate expression with no parameter list.
            //
            Func<int> func8 = delegate { return 1 + 1; };
            //
            // Invoke each of the lambda expressions and delegates we created.
            // ... The methods above are executed.
            //
            Console.WriteLine(func1.Invoke(3)); // Output: 4
        }

        static void modifyString(string str) {
           str = "world";
        }
        static void modifyClass(MyClass my_class) {
            my_class.my_field = 10;
        }
        static void modifyStruct(MyStruct my_struct) {
            my_struct.my_field = 10;
        }
        static void modifyStringList(List<string> str_list) {
            str_list[0] = "first";
            str_list.Add("added");
        }
        static void modifyStringArray(string[] str_arr) {
            str_arr[0] = "first";
        }
        static void passByReference() {
            string str = "hello";
            MyClass my_class = new MyClass();
            MyStruct my_struct = new MyStruct();
            my_struct.my_field = 2;
            List<string> str_list = new List<string>(){
                "hello", "world", "hi"
            };
            string[] str_arr = new string[3] { "hello", "world", "hi" };

            modifyString(str);
            Console.WriteLine("Modified String: " + str);

            modifyStruct(my_struct);
            Console.WriteLine("Modified Struct Field: " + my_struct.my_field);

            modifyClass(my_class);
            Console.WriteLine("Modified Class Field: " + my_class.my_field);

            modifyStringList(str_list);
            Console.WriteLine("Modified String List: " + string.Join("-", str_list));
            
            modifyStringArray(str_arr);
            Console.WriteLine("Modified String Array: ");
            printArray(str_arr);

            /* Output
            MyClass ctor
            Modified String: hello
            Modified Class Field: 10
            Modified String: 10
            Modified String List: first-world-hi-added
            Modified String Array:
            first-world-hi
            */
        }

        static string returnSelf(string str) {
           return str;
        }
        static MyClass returnSelf(MyClass my_class) {
            return my_class;
        }
        static MyStruct returnSelf(MyStruct my_struct) {
            return my_struct;
        }
        static List<string> returnSelf(List<string> str_list) {
            return str_list;
        }
        static string[] returnSelf(string[] str_arr) {
            return str_arr;
        }
        // same output as passByReference
        static void returnByReference() {
            string str = "hello";
            MyClass my_class = new MyClass();
            MyStruct my_struct = new MyStruct();
            my_struct.my_field = 2;
            List<string> str_list = new List<string>(){
                "hello", "world", "hi"
            };
            string[] str_arr = new string[3] { "hello", "world", "hi" };

            modifyString(returnSelf(str));
            Console.WriteLine("Modified String: " + str);

            modifyStruct(returnSelf(my_struct));
            Console.WriteLine("Modified Struct Field: " + my_struct.my_field);

            modifyClass(returnSelf(my_class));
            Console.WriteLine("Modified Class Field: " + my_class.my_field);

            modifyStringList(returnSelf(str_list));
            Console.WriteLine("Modified String List: " + string.Join("-", str_list));

            modifyStringArray(returnSelf(str_arr));
            Console.WriteLine("Modified String Array: ");
            printArray(str_arr);

            /* Output
            MyClass ctor
            Modified String: hello
            Modified Class Field: 10
            Modified String: 10
            Modified String List: first-world-hi-added
            Modified String Array:
            first-world-hi
            */
        }


        static void modifyCopyCtorCopies() {
            MyClass my_class = new MyClass();
            List<string> str_list = new List<string>(){
                "hello", "world", "hi"
            };
            
            MyClass my_class1 = new MyClass(my_class);
            List<string> str_list1 = new List<string>(str_list);

            // modify copy and display original
            modifyClass(my_class1);
            Console.WriteLine("Modified Class Field: " + my_class.my_field);

            modifyStringList(str_list1);
            Console.WriteLine("Modified String List: " + string.Join("-", str_list));

            /* Output
            MyClass ctor
            Modified String: hello
            Modified Struct Field: 2
            Modified Class Field: 10
            Modified String List: first-world-hi-added
            Modified String Array:
            first-world-hi

            */
        }
        // same output as modifyCopyCtorCopies
        static void modifyAssignmentOpCopies() {
            string str = "hello";
            MyClass my_class = new MyClass();
            MyStruct my_struct = new MyStruct();
            my_struct.my_field = 2;
            List<string> str_list = new List<string>(){
                "hello", "world", "hi"
            };
            string[] str_arr = new string[3] { "hello", "world", "hi" };

            string str1 = str;
            str1 = str;
            MyClass my_class1 = my_class;
            my_class1 = my_class;
            MyStruct my_struct1 = my_struct;
            my_struct1 = my_struct;
            List<string> str_list1 = str_list;
            str_list1 = str_list;
            string[] str_arr1 = str_arr;
            str_arr1 = str_arr;

            // modify copy and display original
            modifyString(str1);
            Console.WriteLine("Modified String: " + str);

            modifyStruct(my_struct);
            Console.WriteLine("Modified Struct Field: " + my_struct.my_field);

            modifyClass(my_class);
            Console.WriteLine("Modified Class Field: " + my_class.my_field);

            modifyStringList(str_list1);
            Console.WriteLine("Modified String List: " + string.Join("-", str_list));

            modifyStringArray(str_arr1);
            Console.WriteLine("Modified String Array: ");
            printArray(str_arr);

            /* Output
            MyClass ctor
            Modified String: hello
            Modified Struct Field: 2
            Modified Class Field: 10
            Modified String List: first-world-hi-added
            Modified String Array:
            first-world-hi

            */
        }

        static void linkedList() {
            LinkedList<int> yo = new LinkedList<int>();
            yo.AddFirst(1);
            yo.AddLast(2);
            for (LinkedListNode<int> node = yo.First; node != yo.Last.Next; node = node.Next) {
                yo.AddBefore(node, 0);
            }

            // print linked list
            foreach (int item in yo) {
                Console.WriteLine(item);
            }

            /* Output:
            0
            1
            0
            2
            */
        }

        static T templateElementAt<T>(T[] arr, int index) {
            return arr[index];
        }
        static void templateExample() {
            int[] yo = new int[3]{1, 2, 3};
            Console.WriteLine(templateElementAt(yo, 1)); // Output: 2
        }

        static int compareMyClass(MyClass mc1, MyClass mc2) {
            if (mc1.my_field == mc2.my_field) {
                return 0;
            } else if (mc1.my_field < mc2.my_field) { // correct order -> -1
                return -1;
            } else {
                return 1;
            }
        }
        static void compareExample() {
            MyClass mc1 = new MyClass();
            mc1.my_field = 1;
            MyClass mc2 = new MyClass();
            mc2.my_field = 2;
            MyClass mc3 = new MyClass();
            mc3.my_field = 3;

            MyClass[] mc_arr = new MyClass[3]{ mc1, mc2, mc3 };
            Array.Sort(mc_arr, compareMyClass);
            for (int i = 0; i < 3; i++) {
                Console.WriteLine(mc_arr[i].my_field);
            }
            /* Output:
            1
            2
            3
            */
        }

        // must be outside function
        enum Importance {
            None,
            Trivial,
            Regular,
            Important,
            Critical
        };
        static void enumExample() {
            Importance value = Importance.Critical;

            // ... Test against known Importance values.
            if (value == Importance.Critical) {
                Console.WriteLine(value.ToString());
            }

            /* Output:
            Critical
            */
        }

        // Async Examples
        static string returnHello() {
            return "hello";
        }
        static void processOne() {
            Console.WriteLine("processOne Point 1");
            Thread.Sleep(500);
            Console.WriteLine("processOne Point 2");
            Thread.Sleep(500);
            Console.WriteLine("processOne Point 3");
        }
        static void processTwo() {
            Console.WriteLine("processTwo Point 1");
            Thread.Sleep(500);;
            Console.WriteLine("processTwo Point 2");
            Thread.Sleep(500);
            Console.WriteLine("processTwo Point 3");
        }
        static async void processOneAsync() {
            await Task.Run(() => processOne());
        }
        static async void processTwoAsync() {
            await Task.Run(() => processTwo());
        }

        public delegate void VoidFunction();
        static async void runAsync(VoidFunction func) {
            await Task.Run(() => func());
        }
        static async void asyncExample() {
            // An async method will be run synchronously if it does not contain the await keyword.

            Console.WriteLine("Async Method 1");
            processOneAsync();
            processTwoAsync();
            Thread.Sleep(2000);
            Console.WriteLine("");

            Console.WriteLine("Async Method 2");
            runAsync(processOne);
            runAsync(processTwo);
            Thread.Sleep(2000);
            Console.WriteLine("");

            /* Output:
            Async Method 1
            processOne Point 1
            processTwo Point 1
            processOne Point 2
            processTwo Point 2
            processOne Point 3
            processTwo Point 3

            Async Method 2
            processOne Point 1
            processTwo Point 1
            processOne Point 2
            processTwo Point 2
            processOne Point 3
            processTwo Point 3
            */
        }

        static void Main(string[] args) {
            // Uncomment to test

            //stringManipulation();
            //forLoopTest();
            //returnInTryCatchFinally();
            //returnInUsing();
            //passByReference();
            //returnByReference();
            //modifyCopyCtorCopies();
            //modifyAssignmentOpCopies();

            /* Learned
            Test One - pass to function which modifies it by parameter
             * passed by value
               * string
              * passed by "reference" (changes visible outside function)
                * class, array, List<>, struct
            
            Test Two - pass to function which simply returns it, then
                       pass that to function which modifies it by parameter
             * same results as above
            
            Test Three - create copy with copy ctor, modify it via function, then display original
             * copy value - (changes not observed in both variables)
                * List<>, class
             * MyClass mc = yo; does NOT call copy ctor
             * MyClass mc = new MyClass(yo); calls copy ctor
            
            Test Four - create copy with assignment operator, modify it via function, then display original
             * copied "reference" (changes observed in both variables)
                * class, array, List<>
             * copy value - (changes not observed in both variables)
                * struct
            
            Test Five - have function that returns a local instance of a class and copy ctor is never called
            */

            //linkedList();
            // Dictionary is hashtable
            // Stack is stack
            // List is dynamic array
            // templateExample();
            //compareExample();
            //enumExample();
            //asyncExample();

            // Binary Search Example
            // int[] arr = new int[5] { 1, 2, 3, 4, 5 };
            // Console.WriteLine(Array.BinarySearch(arr, 3).ToString());
            // Output: 2 (index)



            Console.ReadLine();
        }
    }
    /*
    static void modifyString1(string str) {
            unsafe {
                string* str_address_1 = &str;
                char* str_entry_address_1 = &str[0];

                str = "world";

                string* str_address_2 = &str;
                char* str_entry_address_1 = &str[0];
            }
        }
        static void modifyClass(MyClass my_class) {
            unsafe {
                MyClass* str_address_1 = &my_class;
                int* str_entry_address_1 = &my_class.my_field;

                my_class.my_field = 10;

                MyClass* str_address_2 = &my_class;
                int* str_entry_address_2 = &my_class.my_field;
            }
        }
        static void modifyStruct(MyStruct my_struct) {
            unsafe {
                MyStruct* str_address_1 = &my_struct;
                int* str_entry_address_1 = &my_struct.my_field;

                my_struct.my_field = 10;

                MyStruct* str_address_2 = &my_struct;
                int* str_entry_address_2 = &my_struct.my_field;
            }
        }
     */
}
