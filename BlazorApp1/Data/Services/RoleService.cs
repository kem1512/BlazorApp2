using BlazorApp1.Data.Models;
using BlazorApp1.Pages.Management;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorApp1.Data.Services
{
    public class RoleService
    {
        RoleManager<Role> _context;

        public RoleService(RoleManager<Role> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateRoleClaims(Role role)
        {
            var result = await _context.GetClaimsAsync(role);
        }

        public async Task<Result<Pagination<Role>>> GetPaginated(int page = 1, int pageSize = 5, string? searchTerm = null)
        {
            var result = new Result<Pagination<Role>>();

            if (_context != null && _context.Roles != null)
            {
                try
                {
                    var query = _context.Roles.Include(c => c.RoleClaims).AsQueryable();

                    // Áp dụng điều kiện lọc nếu có
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query = query.Where(r => r.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                    }

                    // Tính chỉ số bắt đầu của trang
                    int startIndex = (page - 1) * pageSize;

                    // Lấy tổng số mục (trước khi áp dụng phân trang)
                    int totalItems = await query.CountAsync();

                    // Lấy dữ liệu theo trang và kích thước trang
                    var roles = await query.Skip(startIndex).Take(pageSize).Include(c => c.RoleClaims).ToListAsync();

                    // Tạo đối tượng PaginationModel để chứa thông tin phân trang
                    var paginationModel = new Pagination<Role>(roles, totalItems, page, pageSize);

                    result.Success = true;
                    result.Data = paginationModel;
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Message = e.Message;
                }
            }

            return result;
        }


        public async Task<Result<Role>> Create(Role role)
        {
            var result = new Result<Role>();

            if (_context != null)
            {
                try
                {
                    await _context.CreateAsync(role);

                    result.Success = true;
                    result.Message = "OK";
                    result.Data = role;
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Message = e.Message;
                }

            }

            return result;
        }

        public async Task<Result<Role>> Update(Role role)
        {
            var result = new Result<Role>();

            if (_context != null)
            {
                try
                {
                    await _context.UpdateAsync(role);

                    result.Success = true;
                    result.Message = "OK";
                    result.Data = role;
                }
                catch (DbUpdateException e)
                {
                    result.Success = false;
                    result.Message = e.Message;
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Message = e.Message;
                }

            }

            return result;
        }

        public async Task<Result<Role>> Remove(Role role)
        {
            var result = new Result<Role>();

            if (_context != null)
            {
                try
                {
                    await _context.DeleteAsync(role);

                    result.Success = true;
                    result.Message = "OK";
                    result.Data = role;
                }
                catch (Exception e)
                {
                    result.Success = false;
                    result.Message = e.Message;
                }

            }

            return result;
        }
    }
}
