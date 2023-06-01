namespace LinqJoin
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // Customers koleksiyonu
            List<Customer> customers = new List<Customer>
            {
                new Customer { ID = 1, Name = "John" },
                new Customer { ID = 2, Name = "Jane" },
                new Customer { ID = 3, Name = "David" }
            };

            // Orders koleksiyonu
            List<Order> orders = new List<Order>
            {
                new Order { OrderID = 101, CustomerID = 1, Product = "Product A" },
                new Order { OrderID = 102, CustomerID = 2, Product = "Product B" },
                new Order { OrderID = 103, CustomerID = 1, Product = "Product C" }
            };

            var result = customers.Join(
            orders,
            customer => customer.ID,
            order => order.CustomerID,
            (customer, order) => new { customer.Name, order.OrderID, order.Product }
            );

            // Sonuçları yazdırma
            Console.WriteLine("Join Result:");
            foreach (var item in result)
            {
                Console.WriteLine($"Customer: {item.Name}, Order ID: {item.OrderID}, Product: {item.Product}");
            }
            Console.WriteLine("--------------------------------");

            // Group Join işlemi
            var groupJoinResult = customers.GroupJoin(
                orders,
                customer => customer.ID,
                order => order.CustomerID,
                (customer, orderGroup) => new { customer.Name, Orders = orderGroup }
            );

            Console.WriteLine("\nGroup Join Result:");
            foreach (var item in groupJoinResult)
            {
                Console.WriteLine($"Customer: {item.Name}");
                foreach (var order in item.Orders)
                {
                    Console.WriteLine($"  Order ID: {order.OrderID}, Product: {order.Product}");
                }
            }

            Console.WriteLine("--------------------------------");
            // Cross Join işlemi
            var crossJoinResult = customers.SelectMany(
                customer => orders,
                (customer, order) => new { customer.Name, order.OrderID, order.Product }
            );

            Console.WriteLine("\nCross Join Result:");
            foreach (var item in crossJoinResult)
            {
                Console.WriteLine($"Customer: {item.Name}, Order ID: {item.OrderID}, Product: {item.Product}");
            }

            Console.WriteLine("--------------------------------");
            // Left Join işlemi
            var leftJoinResult = customers.GroupJoin(
                orders,
                customer => customer.ID,
                order => order.CustomerID,
                (customer, orderGroup) => new { customer, Orders = orderGroup.DefaultIfEmpty() }
            ).SelectMany(
                x => x.Orders,
                (customer, order) => new { customer.customer.Name, OrderID = order?.OrderID, Product = order?.Product }
            );

            Console.WriteLine("\nLeft Join Result:");
            foreach (var item in leftJoinResult)
            {
                Console.WriteLine($"Customer: {item.Name}, Order ID: {item.OrderID}, Product: {item.Product ?? "N/A"}");
            }

            Console.ReadLine();
        }
    }


        // Customer sınıfı
        class Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        // Order sınıfı
        class Order
        {
            public int OrderID { get; set; }
            public int CustomerID { get; set; }
            public string Product { get; set; }
        }
    
    
}