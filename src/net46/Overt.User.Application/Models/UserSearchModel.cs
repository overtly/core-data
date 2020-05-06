using Overt.Core.Data;
using Overt.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Overt.User.Application.Models
{
    public class UserSearchModel
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public bool IsMaster { get; set; }

        public List<int> UserIds { get; set; }

        public string SearchKey { get; set; }

        public Expression<Func<UserEntity, bool>> GetExpression()
        {
            Expression<Func<UserEntity, bool>> expression = oo => oo.UserId > 0;
            if (UserIds?.Count > 0)
                expression = expression.And(oo => UserIds.Contains(oo.UserId));

            if (!string.IsNullOrEmpty(SearchKey))
                expression = expression.And(oo => oo.UserName.Contains(SearchKey));

            return expression;
        }

        public OrderByField[] GetOrder()
        {
            var list = new List<OrderByField>()
           {
               OrderByField.Create(nameof(UserEntity.AddTime), FieldSortType.Asc)
           };
            return list.ToArray();
        }

    }
}
