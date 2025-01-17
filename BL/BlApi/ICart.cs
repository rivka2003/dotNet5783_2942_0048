﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        /// <summary>
        /// Adding a product to the cart and returns the full cart
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public BO.Cart AddProductToCart(BO.Cart cart, int productID, int productAmount);
        /// <summary>
        /// Updetes the amount of the item in the cart and returns the full cart
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="ID"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public BO.Cart UpdateAmountProduct(BO.Cart cartt, int ID, int Amount);
        /// <summary>
        /// Making a new order by the details that it got
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="Name"></param>
        /// <param name="Email"></param>
        /// <param name="Address"></param>
        public BO.Order OrderMaking(BO.Cart cart);

    }
}
