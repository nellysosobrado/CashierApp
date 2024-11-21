## Cashier App Project

UML

![CashierApp_UML](https://github.com/user-attachments/assets/913e7803-4670-46e0-b4d8-725efafe02a2)


## Description
The project is a console based grocery store cashier register system, that I have developed as part of my final examination assignment for the course 'OOP C#' at KYH liljeholmen, Stochkolm.


The program uses Autofac for dependency injection, interfaces for flexibility, factory design pattern, classes and methods. It follows S.O.L.I.D principles and clean code to ensure simplicity and maintainability.

## Program Functionality


<ins>**Admin setting**</ins>

The admin settings allow users to manage products and campaigns with various options to edit product attributes and campaigns.
- ![image](https://github.com/user-attachments/assets/056aad35-0c8d-4dee-b3bd-0692bd74b7ee)
- ![image](https://github.com/user-attachments/assets/0b88cd93-54c0-4c32-a41c-9f71ff17276d)
One of the admin options: 'view products'
- ![image](https://github.com/user-attachments/assets/2336ee45-7f3f-40ea-8f86-5e8aac725dbc)

<ins>**Product management**</ins>

Users can add, remove, and edit a product’s attributes:

- Product ID
- Name
- Price
- PriceType

<ins>**Campaign management**</in>

For products with special deals, users can add, remove, and manage campaigns

- A product can have multiple campaigns.
- Each campaign includes a Start Date and End Date.
- Products with campaigns are only active during the specified campaign dates.

<ins>**Product overview**</ins>

Users can view and browse all available products, which are read from a JSON file where the product details are stored.

- Products in json file are sorted by product ID. In order to maintain the storted system in json file, it also sorts whenever a new products adds

<ins>**Cart**</ins>

- Add items by product name and quantity
- Fast command to add items by productid and quanitity (300 1)
- ![image](https://github.com/user-attachments/assets/0b05f55b-eda4-4fe0-b2b7-3adb11deb947)

<ins>**Payment & receipt**</ins>
- Enter 'PAY' to confirm the purchase
- purschased products, will genereate a txt receipt including the product details (price, name, campaign if it has one, total sum).
- Each time the application starts, it reads all existing receipt.txt files to ensure correct receipt numbering
- Receipts are saved by date. If multiple purchases occur on the same day, they are stored in the same file.
- A new receipt.txt file is created for purchases made on a new day.
- ![image](https://github.com/user-attachments/assets/2255d226-c5ca-47ad-9135-809ec5238efc)
