using CashierApp.Core.Interfaces;
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

        public ProductManager(IProductService productService)
        {
            _productService = productService;
        }

        //CREATE NEW PRODUCT
        public void CreateNewProduct()
        {
            bool runCreateNewproduct = true;

            while (runCreateNewproduct)
            {
                Console.Clear();

                Console.WriteLine("ADD NEW PRODUCT");
                Console.WriteLine("Please enter the product's details for the new product");
                Console.WriteLine("........................................");

                // New CATEGORY.....................................
                string category;
                while (true)
                {
                    Console.Write("Category: ");
                    category = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(category))
                    {
                        Console.WriteLine("Category cannot be empty. Please enter a valid category.");
                        continue;
                    }
                    break;
                }

                //New PRODUCT ID.....................................
                int newProductId;
                while (true)
                {
                    Console.Write("ProductID: ");
                    string id = Console.ReadLine() ?? string.Empty;
                    if (id == string.Empty)
                    {
                        Console.WriteLine("Input cannot be empty.");
                        continue;
                    }
                    if (!int.TryParse(id, out newProductId))
                    {
                        Console.WriteLine("Invalid ProductID. Please enter a valid number.");
                        continue;
                    }
                    if (_productService.GetProductById(newProductId) != null)
                    {
                        Console.WriteLine($"A product with ID {newProductId} already exists. Please try a different ID.");
                    }
                    else
                    {
                        break;
                    }
                }

                //New PRODUCT NAME.............................................
                string newProductName;
                while (true)
                {
                    Console.Write("Product Name: ");

                    newProductName = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(newProductName))
                    {
                        Console.WriteLine("Product name cannot be empty. Please enter a valid name.");
                        continue;
                    }
                    if (_productService.GetProductByName(newProductName) != null)
                    {
                        Console.WriteLine($"'{newProductName}' already exists. Please try a different name.");
                        continue;
                    }
                    break;
                }

                // New Price ..................................................
                decimal newPrice;
                while (true)
                {
                    Console.Write("Price: ");
                    string priceInput = Console.ReadLine() ?? string.Empty;
                    if (!decimal.TryParse(priceInput, out newPrice) || newPrice <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number");
                        continue;
                    }
                    break;
                }

                // PRICE TYPE
                string priceType;
                while (true)
                {
                    Console.Write("PriceType (e.g., 'kg', 'piece'): ");
                    priceType = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(priceType) || !(priceType.Equals("kg", StringComparison.OrdinalIgnoreCase) || priceType.Equals("piece", StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine("Invalid PriceType. Please enter 'kg' or 'piece'.");
                        continue;
                    }
                    break;
                }


                try
                {
                    _productService.AddProduct(category, newProductId, newProductName, newPrice, priceType);
                    Console.WriteLine("Product successfully added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding the product: {ex.Message}");
                }



                Console.WriteLine("Press any key to return to the Admin menu.");
                Console.ReadKey();
                break;
            }
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
                    var product = _productService.GetProductById(productId);
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
                            }
                        }
                        break;

                    case "3": // PRODUCTID
                        while (true)
                        {
                            Console.Write("Enter the new product ID: ");
                            if (int.TryParse(Console.ReadLine(), out int newId))
                            {
                                if (_productService.GetProductById(newId) == null)
                                {
                                    var product = _productService.GetProductById(productId);
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
                                var product = _productService.GetProductById(productId);
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
                                var product = _productService.GetProductById(productId);
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
                var product = _productService.GetProductById(productId);
                if (product == null)
                {
                    Console.WriteLine($"No product found with ID {productId}. Please check and try again.");
                    continue;
                }

                // REmove product
                _productService.RemoveProduct(productId);

                var removedProduct = _productService.GetProductById(productId);
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
            Console.Clear();
            var products = _productService.GetAllProducts();
            if (products == null || !products.Any())
            {
                Console.WriteLine("No products available.");
                Console.ReadKey();
                return;
            }

            int pageSize = 5;
            int totalProducts = products.Count;
            int currentPage = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Page {currentPage + 1}/{(int)Math.Ceiling(totalProducts / (double)pageSize)}");
                Console.WriteLine(new string('-', 30));

                var pageProducts = products.Skip(currentPage * pageSize).Take(pageSize).ToList();

                foreach (var product in pageProducts)
                {
                    Console.WriteLine($"ID: {product.ProductID}");
                    Console.WriteLine($"Name: {product.ProductName}");
                    Console.WriteLine($"Category: {product.Category}");
                    Console.WriteLine($"Price: {product.Price} {product.PriceType}");

                    if (product.Campaigns != null && product.Campaigns.Any())
                    {
                        Console.WriteLine("Additional Campaigns:");
                        foreach (var campaign in product.Campaigns)
                        {
                            Console.WriteLine($" - Description: {campaign.Description}");
                            Console.WriteLine($"   Price: {campaign.CampaignPrice}");
                            Console.WriteLine($"   Start Date: {campaign.StartDate:yyyy-MM-dd}");
                            Console.WriteLine($"   End Date: {campaign.EndDate:yyyy-MM-dd}");
                        }
                    }
                    Console.WriteLine(new string('-', 30));
                }

                Console.WriteLine("[N] Next page | [P] Previous page | [Q] Quit");
                var command = Console.ReadKey(true).Key;

                if (command == ConsoleKey.N && (currentPage + 1) * pageSize < totalProducts)
                {
                    currentPage++;
                }
                else if (command == ConsoleKey.P && currentPage > 0)
                {
                    currentPage--;
                }
                else if (command == ConsoleKey.Q)
                {
                    break;
                }
            }
        }
    }
}
