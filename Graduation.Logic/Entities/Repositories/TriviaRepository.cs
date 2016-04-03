using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduation.Logic.Entities.Repositories
{
    public class TriviaRepository
    {
        public interface IRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            T Get(int id);
            void Create(T item);
            void Update(T item);
            void Delete(int id);
        }

        public class BookRepository : IRepository<Book>
        {
            private OrderContext db;

            public BookRepository(OrderContext context)
            {
                this.db = context;
            }

            public IEnumerable<Book> GetAll()
            {
                return db.Books;
            }

            public Book Get(int id)
            {
                return db.Books.Find(id);
            }

            public void Create(Book book)
            {
                db.Books.Add(book);
            }

            public void Update(Book book)
            {
                db.Entry(book).State = EntityState.Modified;
            }

            public void Delete(int id)
            {
                Book book = db.Books.Find(id);
                if (book != null)
                    db.Books.Remove(book);
            }
        }

        public class OrderRepository : IRepository<Order>
        {
            private OrderContext db;

            public OrderRepository(OrderContext context)
            {
                this.db = context;
            }

            public IEnumerable<Order> GetAll()
            {
                return db.Orders.Include(o => o.Book);
            }

            public Order Get(int id)
            {
                return db.Orders.Find(id);
            }

            public void Create(Order order)
            {
                db.Orders.Add(order);
            }

            public void Update(Order order)
            {
                db.Entry(order).State = EntityState.Modified;
            }

            public void Delete(int id)
            {
                Order order = db.Orders.Find(id);
                if (order != null)
                    db.Orders.Remove(order);
            }
        }
    }
}
