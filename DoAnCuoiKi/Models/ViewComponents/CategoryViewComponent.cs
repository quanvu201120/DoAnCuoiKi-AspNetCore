using DoAnCuoiKi.Data;
using Microsoft.AspNetCore.Mvc;

namespace DoAnCuoiKi.Models.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly MyDbContext _context;
        public CategoryViewComponent(MyDbContext dbContext)
        {
            _context = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var listCate = _context.categories.ToList();
            return View("CateViewComponent", listCate);
        }
    }
}
