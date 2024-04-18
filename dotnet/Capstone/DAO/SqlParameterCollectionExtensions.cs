using System;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    // this is my helper method for handling nullable values for the item attributes
    public static class SqlParameterCollectionExtensions
    {
        public static SqlParameter AddWithNullableValue(this SqlParameterCollection collection, string parameterName, Item item)
        {
            SqlParameter output = new SqlParameter();

            if (item.ProductUrl == null)
            {
                output = collection.AddWithValue(parameterName, DBNull.Value);
            }
            else
            {
                output = collection.AddWithValue(parameterName, item.ProductUrl);
            }

            if (item.SkuItemNumber == null)
            {
                output = collection.AddWithValue(parameterName, DBNull.Value);
            }
            else
            {
                output = collection.AddWithValue(parameterName, item.SkuItemNumber);
            }

            if (item.Price == null)
            {
                output = collection.AddWithValue(parameterName, DBNull.Value);
            }
            else
            {
                output = collection.AddWithValue(parameterName, item.Price);
            }
            return output;
        }
    }
}
