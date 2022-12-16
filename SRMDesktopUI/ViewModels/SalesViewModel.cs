using AutoMapper;
using Caliburn.Micro;
using SRMDataManager.Library.DataAccess;
using SRMDataManager.Library.Models;
using SRMDesktopUI.Library.Api;
using SRMDesktopUI.Library.Helpers;
using SRMDesktopUI.Library.Models;
using SRMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using SaleDetailModel = SRMDesktopUI.Library.Models.SaleDetailModel;
using SaleModel = SRMDesktopUI.Library.Models.SaleModel;

namespace SRMDesktopUI.ViewModels
{
    public class SalesViewModel: Screen
    {
        private IProductEndpoint _productEndpoint;
        IConfigHelper _configHelper;
        ISaleEndpoint _saleEndpoint;
        IMapper _mapper;

        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper,
            ISaleEndpoint saleEndpoint, IMapper mapper)
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
            _saleEndpoint = saleEndpoint;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        public async Task LoadProducts()
        {
            var ProductList = await _productEndpoint.GetAll();
            var products = _mapper.Map<List<ProductDisplayModel>>(ProductList);
            Products = new BindingList<ProductDisplayModel>(products); 
        }

        private ProductDisplayModel selectedProduct;

        public ProductDisplayModel SelectedProduct
        {
            get { return selectedProduct; }
            set 
            { 
                selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
            }
        }


        private BindingList<CartItemDisplayModel> _cart = new BindingList<CartItemDisplayModel>();

        public BindingList<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set 
            {
                _cart = value; 
                NotifyOfPropertyChange(() => Cart);
            }
        }

        private BindingList<ProductDisplayModel> _products;

        public BindingList<ProductDisplayModel> Products
        {
            get { return _products; }
            set 
            {
                _products = value; 
                NotifyOfPropertyChange(() => Products);
            }
        }

        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal  
        {
            get 
            {       
                return CalculateSubTotal().ToString("C"); 
            }
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;

            subTotal = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart);

            //foreach (var item in Cart)
            //{
            //    subTotal += (item.Product.RetailPrice * item.QuantityInCart);
            //}

            return subTotal;
        }

        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.getTaxRate()/100;

            taxAmount = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);

            //foreach (var item in Cart)
            //{
            //    taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
            //}

            return taxAmount;
        }

        public string Tax
        {
            get
            {
                return CalculateTax().ToString("C");
            }
        }
        public string Total
        {
            get
            {
                decimal total = CalculateSubTotal() + CalculateTax();
                return total.ToString("C");
            }
        }


        public bool CanAddToCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected
                // Make sure there is an item quantity
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;  
                }

                return output;
            }
        }

        public void AddToCart()
        {
            CartItemDisplayModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

            if (existingItem != null) 
            {
                existingItem.QuantityInCart += ItemQuantity;
            }
            else
            {
                CartItemDisplayModel item = new CartItemDisplayModel()
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity= 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;

                // Make sure something is selected

                return output;
            }

        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }


        public bool CanCheckOut
        { 
            get
            {
                bool output = false;

                // Make sure something is in the cart
                if (Cart.Count > 0)
                {
                    output = true;
                }

                return output;
            }

        }

        public async Task CheckOut()
        {
            SaleModel sale = new SaleModel();   

            foreach (var item in Cart) 
            {
                sale.SaleDetails.Add(new SaleDetailModel()
                {
                    ProductId = item.Product.Id,
                    Quantity = item.QuantityInCart
                });
            }

            await _saleEndpoint.PostSale(sale);
        }

    }
}
