using CashierApp.Core.Interfaces.Admin;
using CashierApp.Core.Interfaces.StoreProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Admin
{
    public class ProductManager : IProductManager
    {
        private readonly IProductService _productService;
        private readonly ICreateProductHandler _createProductHandler;

        public ProductManager(IProductService productService, ICreateProductHandler createProductHandler)
        {
            _productService = productService;
            _createProductHandler = createProductHandler;
        }
        public void CreateNewProduct()
        {
            _createProductHandler.AddProduct();
        }

        //EDIT PRODUCT--------------------------------
        public void EditProductDetails()
        {
            Console.Clear();
            Console.WriteLine("UPDATE PRODUCT DETAILS");


            int productId;
            while (true)
            {
                
                Console.Write("Enter the product's ID you wish to edit: ");
                if (int.TryParse(Console.ReadLine(), out productId))
                {
                    var product = _productService.GetProductId(productId);
                    if (product != null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"No product found with ID {productId}. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid number.");
                }
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\n1. Change product name" +
                                  $"\n2. Change product price" +
                                  $"\n3. Change product ID" +
                                  $"\n4. Change product category" +
                                  $"\n5. Change product price type");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1": //PRODUCTNAME
                        while (true)
                        {
                            Console.Write("Enter the new product name: ");
                            string newName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newName))
                            {
                                _productService.UpdateProductName(productId, newName);
                                Console.WriteLine($"Product name updated to {newName}.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Product name cannot be empty. Please try again.");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case "2": //PRODUCTPRICE
                        while (true)
                        {
                            Console.Write("Enter the new product price: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
                            {
                                _productService.UpdateProductPrice(productId, newPrice);
                                Console.WriteLine($"Product price updated to {newPrice:C}.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid price. Please enter a positive number.");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case "3": // PRODUCTID
                        while (true)
                        {
                            Console.Write("Enter the new product ID: ");
                            if (int.TryParse(Console.ReadLine(), out int newId))
                            {
                                if (_productService.GetProductId(newId) == null)
                                {
                                    var product = _productService.GetProductId(productId);
                                    product.ProductID = newId;
                                    _productService.UpdateProduct(product);
                                    Console.WriteLine($"Product ID updated to {newId}.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"A product with ID {newId} already exists. Please choose a different ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID. Please enter a valid number.");
                            }
                        }
                        break;

                    case "4": //PRODUCTCATEGORY
                        while (true)
                        {
                            Console.Write("Enter the new product category: ");
                            string newCategory = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newCategory))
                            {
                                var product = _productService.GetProductId(productId);
                                product.Category = newCategory;
                                _productService.UpdateProduct(product);
                                Console.WriteLine($"Product category updated to {newCategory}.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Category cannot be empty. Please try again.");
                            }
                        }
                        break;

                    case "5": // PRICETYPE
                        while (true)
                        {
                            Console.Write("Enter the new price type (e.g., 'kg', 'piece'): ");
                            string newPriceType = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newPriceType) &&
                                (newPriceType.Equals("kg", StringComparison.OrdinalIgnoreCase) || newPriceType.Equals("piece", StringComparison.OrdinalIgnoreCase)))
                            {
                                var product = _productService.GetProductId(productId);
                                product.PriceType = newPriceType;
                                _productService.UpdateProduct(product);
                                Console.WriteLine($"Product price type updated to {newPriceType}.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid price type. Please enter 'kg' or 'piece'.");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();
                        continue;
                }
                break;
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        //REMOVE PRODUYCT
        public void RemoveProduct()
        {
            Console.Clear();
            Console.WriteLine("REMOVE PRODUCT");

            while (true)
            {
                Console.Write("Enter the Product ID to remove: ");
                string producttoremove = Console.ReadLine() ?? string.Empty;
                if (producttoremove == string.Empty)
                {
                    Console.WriteLine("Invalid. Input cannot be empty");
                    continue;
                }
                if (!int.TryParse(producttoremove, out int productId))
                {
                    Console.WriteLine("Invalid ID. Please enter a valid number.");
                    continue;
                }

                //Controll if product exists
                var product = _productService.GetProductId(productId);
                if (product == null)
                {
                    Console.WriteLine($"No product found with ID {productId}. Please check and try again.");
                    continue;
                }

                // REmove product
                _productService.RemoveProduct(productId);

                var removedProduct = _productService.GetProductId(productId);
                if (removedProduct != null)
                {
                    Console.WriteLine($"\nERROR: Product ID {productId} still exists after removal. Please try again.");
                    continue;
                }


                Console.WriteLine($"Product ID {productId} has been successfully removed.");
                break;
            }

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        //DISPLAY ALL

        public void DisplayProductsAndCampaigns()
        {
            var products = _productService.GetAllProducts();
            if (products == null || !products.Any())
            {
                Console.WriteLine("No products available.");
                Console.ReadKey();
                return;
            }

            int pageSize = 5, currentPage = 0, totalPages = (int)Math.Ceiling(products.Count / (double)pageSize);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Page {currentPage + 1}/{totalPages}");
                Console.WriteLine(new string('-', 50));

                products.Skip(currentPage * pageSize).Take(pageSize).ToList().ForEach(product =>
                {
                    Console.WriteLine($"ID: {product.ProductID} | Name: {product.ProductName} | Price: {product.Price} {product.PriceType}");
                    product.Campaigns?.ForEach(c => Console.WriteLine($"   Campaign: {c.Description}, {c.CampaignPrice:C} ({c.StartDate:yyyy-MM-dd} to {c.EndDate:yyyy-MM-dd})"));
                    Console.WriteLine(new string('-', 50));
                });

                Console.WriteLine("[N] Next page | [P] Previous page | [Q] Quit");
                var command = Console.ReadKey(true).Key;
                if (command == ConsoleKey.N && currentPage < totalPages - 1) currentPage++;
                else if (command == ConsoleKey.P && currentPage > 0) currentPage--;
                else if (command == ConsoleKey.Q) break;
            }
        }

    }
}
