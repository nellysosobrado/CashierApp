using CashierApp.Core.Interfaces.ErrorManagement;
using CashierApp.Presentation.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierApp.Application.Services.StoreProduct
{
    /// <summary>
    /// The ProductCatalog class handles displaying product categories and products to the user
    /// Users can navigate categories and products, with error handling for invalid input
    /// </summary>
    public class ProductCatalog
    {
        private readonly ProductDisplay _productDisplay;
        private readonly ProductService _productService;
        private readonly IErrorManager _errorManager;

        public ProductCatalog(ProductService productService, ProductDisplay productDisplay, IErrorManager errorManager = null)
        {
            _productService = productService;
            _productDisplay = productDisplay;
            _errorManager = errorManager;
        }
        /// <summary>
        /// Displays the product catalog and allows the user to browse categories and products
        /// </summary>
        public void ShowProductCatalog()
        {
            while (true)
            {
                Console.Clear();
                var categories = _productService.FetchProductCategory();
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
                            case "n"://next page if there is
                                if ((currentPage + 1) * pageSize < products.Count())
                                {
                                    currentPage++;
                                }
                                else
                                {
                                    _errorManager.DisplayError("You are on the last page. Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;
                            case "p"://Previous page
                                if (currentPage > 0)
                                {
                                    currentPage--;
                                }
                                else
                                {
                                    _errorManager.DisplayError("You are on the first page. Press any key to continue...");
                                    Console.ReadKey();
                                }
                                break;

                            case "r"://return to category selection
                                browsing = false;
                                break;

                            case "c"://exit the catalog, to the cart
                                return;

                            default:
                                _errorManager.DisplayError($"Invalid command Press any key to try again...");
                                Console.ReadKey();
                                break;
                        }
                    }
                }
                else if (input == string.Empty)
                {
                    _errorManager.DisplayError($" Invalid. Input cannot be empty. Press any key to try again...");
                    Console.ReadKey();
                }
                else
                {
                    _errorManager.DisplayError($"Category '{input}' does not exist. Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }
    }
}
