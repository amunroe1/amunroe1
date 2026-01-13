## Project Description
The aim of this final project is to demonstrate knowledge of lecture concepts including the Web application architecture, components and
server controls, and state management, and access control for security. Originally, the project was completely deployed into and hosted on a Arizona State University server, but I have made it all accessible from the local machine by including all of the necessary service source code in this project. This was a collaborative project and below, in the project directory, I have included credit to my two partners.

## Viewing Instructions
The best and easiest way to view this web application is to open the "Group43.sln" Visual Studio solution file and click "Run" or F5. This should run the two SOAP services. The service for the shopping cart unfortunately was not available to me, so the "view order" capabilities are not avaiable, but all of the features specific to my developed components are functional.

## Project Directory
| Provider Name | Page and Component Type | Component Description | Resource/Methods |
| --- | --- | --- | --- |
| Andrew Munroe | DLL: <br> Encyption.dll | Encryption function <br> Intput:string <br> Output:string | Caesar cipher implemented locally in C# |
| Andrew Munroe | SOAP Service: <br> WordCount.svc | Filter text <br> Intput:string <br> Output:string <br><br> Return dictionary of word frequencies <br> Input:string <br> Output:JSON string | Implemented locally in C# |
| Mark Adan | Default.aspx | Website home page linking to all other pages | .aspx design C# code behind |
| Andrew Munroe | Global.asax | Session start and Application Error event handlers | C# script in global.asax |
| Mark Adan | User control: <br> Captcha.ascx <br> Captcha.ascx.cs <br><br> ImageHandler.ashx | User control design and code behind. HTTP handler to generate image output. | Use C# Bitmap and MemoryStream library calls. Use C# IHttpHandler class |  |
| James Cajas | RESTful Service: <br> OrderService.svc | Shopping cart and order creation | WCF RESTful service w/ JSON requests. Client uses JavaScript fetch() to call CreateOrder.Uses XDocument for XML data storage. |
| Mark Adan | SOAP Service: <br> MyEbayService1.svc | Wrapper for eBay RESTful API search operations | Use C# Http and JSON library calls |
| Mark Adan | Cookies:\[ShoppingCart\] | Saves eBay search item in Shopping Cart | Use C# HttpCookie |
| James Cajas | Cookies | read in implementation | JavaScript cookie functions: setCookie(), getCookie(), clearCookie(), used to persist cart state |
| Mark Adan | DLL: <br> Shipping.dll | Library for shipping cost estimate | Use C# user class and static function |
| James Cajas | DLL: <br> group43.dll | POST and GET | GET: /ping, /orders, /orders/{id}\\ POST: /CreateOrder(JSON) |
| James Cajas | ShoppingCart.aspx | Order checkout page | aspx design and C# Code behind |
| Mark Adan | Registration.aspx | Security Authentication Form for new (Human) members | CAPTCHA Verification, Encryption.dll, XML data storage |
| Andrew Munroe | login.aspx | Security Authorization Form for existing <br> members- member functionality allows members to <br> view their past orders | Encryption.dll <br> XML data storage |
| James Cajas | Admin.aspx | Security Authorization Form for system <br> admins- admin functionality allows admins to <br> view all orders | Encryption.dll <br> XML data storage |
| James Cajas | ShoppingOrdersPage.aspx | Order Retrieval Page | OrderService.svc |

