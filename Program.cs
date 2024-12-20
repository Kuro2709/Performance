
using System;
using System.Collections.Generic;

namespace PerformanceDemo
{
    ///Cải tiến: Tách các chức năng thành các lớp và phương thức riêng biệt.
    class Program
    {
        static void Main()
        {
            ProductManager productManager = new();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. Thêm sản phẩm mới");
                Console.WriteLine("2. Liệt kê sản phẩm");
                Console.WriteLine("3. Tính tổng giá trị sản phẩm");
                Console.WriteLine("4. Thoát");

                Console.Write("Chọn một chức năng: ");
                string? choice = Console.ReadLine();

                if (choice == null)
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    continue;
                }

                switch (choice)
                {
                    case "1":
                        productManager.AddProduct();
                        break;
                    case "2":
                        productManager.ListProducts();
                        break;
                    case "3":
                        Console.WriteLine($"Tổng giá trị sản phẩm: {productManager.CalculateTotalValue()}");
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    public class ProductManager
    {
        private readonly List<Product> _productList = new();

        public void AddProduct()
        {
            Console.Write("Nhập tên sản phẩm: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Tên sản phẩm không hợp lệ!");
                return;
            }

            Console.Write("Nhập giá sản phẩm: ");
            if (!double.TryParse(Console.ReadLine(), out double price) || price < 0)
            //Vấn đề 1: Khả năng lỗi khi nhập dữ liệu
            //Sử dụng double.Parse mà không kiểm tra lỗi có thể gây ra lỗi khi người dùng nhập không hợp lệ.
            //Cải tiến: Sử dụng double.TryParse để xử lý lỗi khi nhập giá trị không phải số.
            {
                Console.WriteLine("Giá sản phẩm không hợp lệ!");
                return;
            }

            _productList.Add(new Product(name, price));
            Console.WriteLine($"Đã thêm sản phẩm: {name}"); ///Cải tiến: Sử dụng string interpolation thay vì cộng chuỗi.
        }

        public void ListProducts()
        {
            Console.WriteLine("Danh sách sản phẩm:");

            if (_productList.Count == 0)
            {
                Console.WriteLine("Không có sản phẩm nào.");
            }
            else
            {
                foreach (var product in _productList)
                {
                    Console.WriteLine($"Tên sản phẩm: {product.Name}, Giá: {product.Price}"); ///Cải tiến: Sử dụng string interpolation thay vì cộng chuỗi.
                }
            }
        }

        public double CalculateTotalValue()
        {
            double total = 0;
            foreach (var product in _productList)
            //Vấn đề 2: Thiếu encapsulation
            //productList là một biến public static, dễ bị truy cập và thay đổi ngoài ý muốn.
            //Cải tiến: Đóng gói productList bằng cách sử dụng thuộc tính private và chỉ truy cập thông qua các phương thức.
            {
                total += product.Price;
            }
            return total;
        }
    }
}

