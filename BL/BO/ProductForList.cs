using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductForList
    {
        /// <summary>
        /// The ID of the product
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The category of the product
        /// </summary>
        public BO.Category category { get; set; }
        public BO.Gender gender { get; set; }
        /// <summary>
        /// The Gender tip of the product
        /// </summary>
        /// <returns></returns>
        public BO.Clothing clothing { get; set; }
        /// <summary>
        /// Cloth tip 
        /// </summary>
        /// <returns></returns>
        public BO.Shoes shoes { get; set; }
        /// <summary>
        /// Shoe tip
        /// </summary>
        /// <returns></returns>
        public BO.Color color { get; set; }
        /// <summary>
        /// The color of the product
        /// </summary>
        /// <returns></returns>
        public BO.SizeClothing sizeClothing { get; set; }
        /// <summary>
        /// The size of the Clothing
        /// </summary>
        public BO.SizeShoes sizeShoes { get; set; }
        /// <summary>
        /// The size of the shoes
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// The Description of the product
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (category is (BO.Category)0)
            {
                return $@"
            ID: {ID}
            Name: {Name}
            Price: {Price}
            Category: {category}
            Description: {Description}
            Gender: {gender}
            Color: {color}
            Clothing: {clothing}
            Size of clothing: {sizeClothing}
            ";
            }
            else
            {
                return $@"
            ID: {ID}
            Name: {Name}
            Price: {Price}
            Category: {category}
            Description: {Description}
            Gender: {gender}
            Color: {color}
            Shoes: {shoes}
            Size of shoes: {(int)sizeShoes}
            ";
            }
        }
    }
}
