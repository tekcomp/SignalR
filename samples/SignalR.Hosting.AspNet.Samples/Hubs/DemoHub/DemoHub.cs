using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SignalR.Hubs;

namespace SignalR.Samples.Hubs.DemoHub
{
    [HubName("demo")]
    public class DemoHub : Hub
    {
        public Task<int> GetValue()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return 10;
            });
        }

        public void AddToGroups()
        {
            Groups.Add(Context.ConnectionId, "foo");
            Groups.Add(Context.ConnectionId, "bar");
            Clients.Caller.groupAdded();
        }

        public void DoSomethingAndCallError()
        {
            Clients.All.errorInCallback();
        }

        public Task DynamicTask()
        {
            return Clients.All.signal(Guid.NewGuid());
        }

        public Task PlainTask()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
            });
        }

        public Task GenericTaskTypedAsPlain()
        {
            return Task.Factory.StartNew(() =>
            {
                return 2 + 2;
            });
        }

        public Task TaskWithException()
        {
            return Task.Factory.StartNew(() =>
            {
                throw new Exception();
            });
        }

        public Task<int> GenericTaskWithException()
        {
            return Task<int>.Factory.StartNew(() =>
            {
                throw new Exception();
            });
        }

        public void SimpleArray(int[] nums)
        {
            foreach (var n in nums)
            {
            }
        }

        public string ReadStateValue()
        {
            return Clients.Caller.name;
        }

        public string SetStateValue(string value)
        {
            Clients.Caller.Company = value;

            return Clients.Caller.Company;
        }

        public void ComplexArray(Person[] people)
        {

        }

        public Person ComplexType(Person p)
        {
            Clients.Caller.person = p;
            return p;
        }

        public int PassingDynamicComplex(dynamic p)
        {
            return p.Age;
        }

        public void MultipleCalls()
        {
            for (int i = 0; i < 10; i++)
            {
                Clients.Caller.index = i + 1;
                Clients.Caller.invoke(i);
                Thread.Sleep(1000);
            }
        }

        public void Overload()
        {

        }

        public int Overload(int n)
        {
            return n;
        }

        public void UnsupportedOverload(string x)
        {

        }

        public void UnsupportedOverload(int x)
        {

        }

        public void TestGuid()
        {
            Clients.Caller.TestGuid(new Guid());
        }

        public void DynamicInvoke(string method)
        {
            IClientProxy proxy = Clients.Caller;
            proxy.Invoke(method);
        }

        public void MispelledClientMethod()
        {
            Clients.Caller.clientMethd();
        }

        public override IEnumerable<string> RejoiningGroups(IEnumerable<string> groups)
        {
            return groups;
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Address Address { get; set; }
        }

        public class Address
        {
            public string Street { get; set; }
            public string Zip { get; set; }
        }
    }
}