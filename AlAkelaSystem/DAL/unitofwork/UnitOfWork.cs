using DAL.Data;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.unitofwork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AlAkelaDBcontext alAkelaDBcontext;
        public UnitOfWork(AlAkelaDBcontext alAkelaDBcontext)
        {
            this.alAkelaDBcontext = alAkelaDBcontext;
        }
        IGenericRepository<Product> _product;
        IGenericRepository<Category> _category;
        IGenericRepository<Orders> _order;
        IGenericRepository<OrderItem> _orderItem;
        IGenericRepository<Customer> _customer;
        IGenericRepository<Discount> _discount;
        IGenericRepository<Coupon> _coupon;
        IGenericRepository<Extras> _extras;

        #region Product
        public IGenericRepository<Product> ProductRepo
        {
            get
            {
                if (_product == null)
                {
                    _product = new Repository<Product>(alAkelaDBcontext);
                }
                return _product;
            }
        }
        #endregion

        #region CategoryRepo
        public IGenericRepository<Category> CategoryRepo
        {
            get
            {
                if (_category == null)
                {
                    _category = new Repository<Category>(alAkelaDBcontext);
                }
                return _category;
            }
        }
        #endregion

        #region OrderRepo
        public IGenericRepository<Orders> OrderRepo
        {
            get
            {
                if (_order == null)
                {
                    _order = new Repository<Orders>(alAkelaDBcontext);
                }
                return _order;
            }
        }
        #endregion

        #region OrderItemRepo
        public IGenericRepository<OrderItem> OrderItemRepo
        {
            get
            {
                if (_orderItem == null)
                {
                    _orderItem = new Repository<OrderItem>(alAkelaDBcontext);
                }
                return _orderItem;
            }
        }
        #endregion

        #region CustomerRepo
        public IGenericRepository<Customer> CustomerRepo
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new Repository<Customer>(alAkelaDBcontext);
                }
                return _customer;
            }
        }
        #endregion

        #region DiscountRepo
        public IGenericRepository<Discount> DiscountRepo
        {
            get
            {
                if (_discount == null)
                {
                    _discount = new Repository<Discount>(alAkelaDBcontext);
                }
                return _discount;
            }
        }
        #endregion

        #region CouponRepo
        public IGenericRepository<Coupon> CouponRepo
        {
            get
            {
                if (_coupon == null)
                {
                    _coupon = new Repository<Coupon>(alAkelaDBcontext);
                }
                return _coupon;
            }
        }
        #endregion

        #region ExtrasRepo
        public IGenericRepository<Extras> ExtrasRepo
        {
            get
            {
                if (_extras == null)
                {
                    _extras = new Repository<Extras>(alAkelaDBcontext);
                }
                return _extras;
            }
        }


        #endregion      
        public async Task<int> SaveChangesAsync()
        {
            return await alAkelaDBcontext.SaveChangesAsync();
        }


    }
}

