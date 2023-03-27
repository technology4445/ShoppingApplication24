using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingApplication24.Models;
using ShoppingApplication24.Models.ViewModels;

namespace ShoppingApplication24.Controllers
{

    public class AdminController : Controller
    {
        private readonly ShoppingContext _context;
        public AdminController(ShoppingContext Context)
        {
            _context = Context;
        }
        [Admin]
        public IActionResult Dashboard()
        {
            AdminDashboardViewModel adminDashboardViewModel = new AdminDashboardViewModel();
            adminDashboardViewModel.Users = _context.Users.Include(x => x.Role).ToList();
            adminDashboardViewModel.AddressDetails = _context.AddressDetails.Include(x => x.User).Include(x => x.City).ToList();
            return View(adminDashboardViewModel);
        }
        [AdminSellerCustomer]
        public IActionResult Products()
        {
            List<Product> products = _context.Products.Include(x => x.Seller).Include(x => x.ProductStatus).Include(x => x.SubCategory).ToList();
            return View(products);
        }
        public IActionResult ViewProducts(int id)
        {


            Product viewProducts = _context.Products.Include(x => x.Seller).Include(x => x.ProductStatus).Where(x => x.Id == id).FirstOrDefault();
            return View(viewProducts);
        }
        [SellerAdmin]
        [HttpGet]
        public IActionResult CreateUpdateProducts(int id = 0)
        {
            ViewBag.Users = new SelectList(_context.Users.Where(x => x.Role.Name == "Seller").ToList(), "Id", "Name");
            ViewBag.ProductStatus = new SelectList(_context.ProductStatuses.ToList(), "Id", "Name");
            ViewBag.SubCategory = new SelectList(_context.SubCategories.ToList(), "Id", "Name");

            if (id == 0)
            {
                return View();
            }
            else
            {
                Product createUpdateProducts = _context.Products.Include(x => x.Seller).Where(x => x.Id == id).FirstOrDefault();
                return View(createUpdateProducts);
            }
        }
        [SellerAdmin]
        [HttpPost]
        public IActionResult CreateUpdateProducts(Product product)
        {

            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Products");

        }
        [SellerAdmin]
        public IActionResult DeleteProducts(int id)
        {
            List<Order> orders = _context.Orders.Where(x => x.ProductId == id).ToList();
            foreach (var delOrders in orders)
            {
                _context.Orders.Remove(delOrders);
            }
            _context.SaveChanges();

            List<Image> image = _context.Images.Where(x => x.ProductId == id).ToList();
            foreach (var delImages in image)
            {
                _context.Images.Remove(delImages);
            }
            _context.SaveChanges();

            Product delProducts = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            _context.Products.Remove(delProducts);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }
        [Admin]
        public IActionResult Users()
        {

            List<User> users = _context.Users.Include(x => x.Role).ToList();
            return View(users);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateUsers(int id = 0)
        {
            ViewBag.Roles = new SelectList(_context.Roles.ToList(), "Id", "Name");
            if (id == 0)
            {
                return View();
            }
            else
            {
                User user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                return View(user);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateUsers(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }
        [Admin]
        public IActionResult DeleteUsers(int id)
        {
            User deluser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            _context.Users.Remove(deluser);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }
        [Admin]
        public IActionResult AddressDetail(AddressDetail addressDetail)
        {
            List<AddressDetail> AddressDetails = _context.AddressDetails.Include(x => x.User).Include(x => x.City).ToList();
            return View(AddressDetails);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateAddress(int id = 0)
        {
            ViewBag.Users = new SelectList(_context.Users.ToList(), "Id", "Name");
            ViewBag.Cities = new SelectList(_context.Cities.ToList(), "Id", "Name");

            if (id == 0)
            {
                return View();
            }
            else
            {
                AddressDetail createUpdateAddress = _context.AddressDetails.Where(x => x.Id == id).FirstOrDefault();
                return View(createUpdateAddress);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateAddress(AddressDetail addressDetail)
        {
            _context.AddressDetails.Update(addressDetail);
            _context.SaveChanges();
            return RedirectToAction("AddressDetail");

        }
        [Admin]
        public IActionResult DeleteAddress(int id)
        {
            AddressDetail delAddress = _context.AddressDetails.Where(x => x.Id == id).FirstOrDefault();

            _context.AddressDetails.Remove(delAddress);
            _context.SaveChanges();
            return RedirectToAction("AddressDetail");
        }
        [Admin]
        public IActionResult Category()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateCategory(int id = 0)
        {
            //create category
            if (id == 0)
            {
                return View();
            }
            else
            {
                Category categoryUpdate = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
                return View(categoryUpdate);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Category");
        }
        [Admin]
        public IActionResult DeleteCategory(int id)
        {
            Category delCategory = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

            _context.Categories.Remove(delCategory);
            _context.SaveChanges();
            return RedirectToAction("Category");
        }
        [Admin]
        public IActionResult City()
        {
            List<City> cities = _context.Cities.ToList();
            return View(cities);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateCity(int id = 0)
        {
            // add city
            if (id == 0)
            {
                return View();
            }
            else
            {
                City updateCity = _context.Cities.Where(x => x.Id == id).FirstOrDefault();
                return View(updateCity);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateCity(City city)
        {
            _context.Cities.Update(city);
            _context.SaveChanges();
            return RedirectToAction("City");
        }
        [Admin]
        public IActionResult DeleteCity(int id)
        {
            City delCity = _context.Cities.Where(x => x.Id == id).FirstOrDefault();

            _context.Cities.Remove(delCity);
            _context.SaveChanges();
            return RedirectToAction("City");
        }
        [Admin]
        public IActionResult Image()
        {
            List<Image> images = _context.Images.Include(x => x.Product).ToList();
            return View(images);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateImage(int id = 0)
        {
            ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Title");
            if (id == 0)
            {
                return View();
            }
            else
            {
                Image image = _context.Images.Where(x => x.Id == id).FirstOrDefault();
                return View(image);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateImage(Image image)
        {
            _context.Images.Update(image);
            _context.SaveChanges();
            return RedirectToAction("Image");
        }
        [Admin]
        public IActionResult DeleteImage(int id)
        {
            Image delImage = _context.Images.Where(x => x.Id == id).FirstOrDefault();
            _context.Images.Remove(delImage);
            _context.SaveChanges();
            return RedirectToAction("Image");
        }
        [AdminSellerCustomer]
        public IActionResult Order()
        {
            List<Order> orders = _context.Orders.Include(x => x.Product).Include(x => x.Buyer).Include(x => x.OrderStatus).ToList();
            return View(orders);
        }
        [AdminSellerCustomer]
        public IActionResult ViewOrder(int id)
        {
            Order ViewOrders = _context.Orders.Include(x => x.Product).Include(x => x.Buyer).Include(x => x.OrderStatus).Where(x => x.Id == id).FirstOrDefault();
            return View(ViewOrders);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateOrder(int id = 0)
        {
            ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Title");
            ViewBag.Buyer = new SelectList(_context.Users.Where(x=>x.Role.Name=="Customer").ToList(), "Id", "Name");
            ViewBag.OrderStatus = new SelectList(_context.OrderStatuses.ToList(), "Id", "Name");
            if (id == 0)
            {
            return View();
            }
            else
            {
                Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
                return View(order);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction("Order");
        }
        [Admin]
        public IActionResult DeleteOrder(int id)
        {
            Order delOrder = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
            _context.Orders.Remove(delOrder);
            _context.SaveChanges();
            return RedirectToAction("Order");
        }
        [AdminSellerCustomer]
        public IActionResult OrderStatus()
        {
            List<OrderStatus> orderStatuses = _context.OrderStatuses.ToList();
            return View(orderStatuses);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateOrderStatus(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                OrderStatus orderStatus = _context.OrderStatuses.Where(x => x.Id == id).FirstOrDefault();
                return View(orderStatus);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateOrderStatus(OrderStatus orderStatus)
        {
            _context.Update(orderStatus);
            _context.SaveChanges();
            return RedirectToAction("OrderStatus");
        }
        [Admin]
        public IActionResult DeleteOrderStatus(int id)
        {
            OrderStatus delOrderStatus = _context.OrderStatuses.Where(x => x.Id == id).FirstOrDefault();

            _context.OrderStatuses.Remove(delOrderStatus);
            _context.SaveChanges();
            return RedirectToAction("OrderStatus");
        }
        [SellerAdmin]
        public IActionResult ProductStatus()
        {
            List<ProductStatus> productStatuses = _context.ProductStatuses.ToList();
            return View(productStatuses);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateProductStatus(int id = 0)
        {
            

            if (id == 0)
            {
                return View();
            }
            else
            {
                ProductStatus productStatus = _context.ProductStatuses.Where(x => x.Id == id).FirstOrDefault();
                return View(productStatus);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateProductStatus(ProductStatus productStatus)
        {
            _context.Update(productStatus);
            _context.SaveChanges();
            return RedirectToAction("ProductStatus");
        }
        [Admin]
        public IActionResult DeleteProductStatus(int id)
        {
            ProductStatus delProductStatus = _context.ProductStatuses.Where(x => x.Id == id).FirstOrDefault();
            _context.ProductStatuses.Remove(delProductStatus);
            _context.SaveChanges();
            return RedirectToAction("ProductStatus");
        }
        [Admin]
        public IActionResult Role()
        {
            List<Role> roles = _context.Roles.ToList();
            return View(roles);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateRole(int id = 0)
        {
            //create category
            if (id == 0)
            {
                return View();
            }
            else
            {
                Role createUpdateRole = _context.Roles.Where(x => x.Id == id).FirstOrDefault();
                return View(createUpdateRole);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return RedirectToAction("Role");
        }
        [Admin]
        public IActionResult DeleteRole(int id)
        {
            Role delRole = _context.Roles.Where(x => x.Id == id).FirstOrDefault();

            _context.Roles.Remove(delRole);
            _context.SaveChanges();
            return RedirectToAction("Role");
        }
        [Admin]
        public IActionResult SubCategory()
        {
            List<SubCategory> subCategories = _context.SubCategories.Include(x=>x.Category).ToList();
            return View(subCategories);
        }
        [Admin]
        [HttpGet]
        public IActionResult CreateUpdateSubCategory(int id = 0)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(),"Id","Name");
            //create category
            if (id == 0)
            {
                return View();
            }
            else
            {
                SubCategory subCategoryUpdate = _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();
                return View(subCategoryUpdate);
            }
        }
        [Admin]
        [HttpPost]
        public IActionResult CreateUpdateSubCategory(SubCategory subCategory)
        {
            _context.SubCategories.Update(subCategory);
            _context.SaveChanges();
            return RedirectToAction("SubCategory");
        }
        [Admin]
        public IActionResult DeleteSubCategory(int id)
        {
            SubCategory delSubCategory = _context.SubCategories.Where(x => x.Id == id).FirstOrDefault();

            _context.SubCategories.Remove(delSubCategory);
            _context.SaveChanges();
            return RedirectToAction("SubCategory");
        }
    }
}
