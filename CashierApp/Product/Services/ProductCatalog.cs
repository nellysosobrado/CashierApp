using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Product.Services
{
    public class ProductCatalog
    {
        private readonly ProductDisplay _productDisplay;
        private readonly ProductService _productService;

        public ProductCatalog(ProductService productService, ProductDisplay productDisplay)
        {
            _productService = productService;
            _productDisplay = productDisplay;
        }

        public void ShowProductCatalog()
        {
            while (true)
            {
                Console.Clear();
                var categories = _productService.GetDistinctCategories();
                _productDisplay.ShowCategories(categories);

                string input = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

                if (input == "c")
                {
                    return;
                }

                var products = _productService.GetProductsByCategory(input);

                if (products.Any())
                {
                    int pageSize = 5;
                    int currentPage = 0;
                    bool browsing = true;

                    while (browsing)
                    {
                        _productDisplay.ShowProductsByCategory(products, input, currentPage, pageSize);

                        string command = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;

                        switch (command)
                        {
                            case "n":
                                if ((currentPage + 1) * pageSize < products.Count())
                                {
                                    currentPage++;
                                }
                                else
                                {
                                    Console.WriteLine("You are on the last page. Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;

                            case "p":
                                if (currentPage > 0)
                                {
                                    currentPage--;
                                }
                                else
                                {
                                    Console.WriteLine("You are on the first page. Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;

                            case "r":
                                browsing = false;
                                break;

                            case "c":
                                return;

                            default:
                                Console.WriteLine("Invalid command. Press any key to try again...");
                                Console.ReadKey();
                                break;
                        }
                    }
                }
                else
                {
                    _productDisplay.ShowNoProductsMessage(input);
                    Console.ReadKey();
                }
            }
        }
    }
}
