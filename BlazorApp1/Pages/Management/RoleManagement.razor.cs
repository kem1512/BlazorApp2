using BlazorApp1.Data.Models;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp1.Pages.Management
{
    public partial class RoleManagement
    {
        Role? Role;

        List<Role>? Roles;

        bool _isEdit = false;

        int currentPage = 1;

        int pageSize = 5;

        int totalPages = 1;

        async Task Load()
        {
            Role = new Role();
            _isEdit = false;

            var result = await RoleService.GetPaginated(currentPage, pageSize);

            if (result.Success)
            {
                var pagination = result.Data;

                if (pagination != null)
                {
                    Roles = pagination.Items;
                    totalPages = pagination.TotalPages;
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        async Task OnPageChanged(int newPage)
        {
            currentPage = newPage;
            await Load();
        }

        async Task CreateOrUpdate(EditContext context)
        {
            if (context.Validate())
            {
                var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = $"Are You Sure You Want to {(_isEdit ? "Update" : "Create")}?", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

                if (string.IsNullOrEmpty(confirm.Value)) return;

                if (Role != null)
                {
                    var result = new Result<Role>();

                    if (_isEdit)
                    {
                        result = await RoleService.Update(Role);
                    }
                    else
                    {
                        result = await RoleService.Create(Role);
                    }

                    await Load();

                    await Swal.FireAsync("", result.Message, result.Success ? SweetAlertIcon.Success : SweetAlertIcon.Error);
                }
                else
                {
                    await Swal.FireAsync("", "Unknown Error", SweetAlertIcon.Error);
                }
            }
            else
            {
                await Swal.FireAsync("", string.Join("<br>", context.GetValidationMessages().Select(c => c)), SweetAlertIcon.Error);
            }
        }

        async Task Remove(Role role)
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = $"Are You Sure You Want to {(_isEdit ? "Update" : "Create")}?", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await RoleService.Remove(role);

            if (result.Success)
            {
                Roles?.Remove(role);
            }

            await Swal.FireAsync("", result.Message, result.Success ? SweetAlertIcon.Success : SweetAlertIcon.Error);
        }
    }
}
