using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CFSIS.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace CFSIS.Client.Pages
{
    public class CourseDataModel : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string paramCourseID { get; set; } = "0";
        [Parameter]
        protected string action { get; set; }

        protected List<Course> courseList = new List<Course>();
        protected Course course = new Course();
        protected string title { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (action == "fetch")
            {
                await FetchCourse();
                this.StateHasChanged();
            }
            else if (action == "create")
            {
                title = "Add Course";
                course = new Course();
            }
            else if (paramCourseID != "0")
            {
                if (action == "edit")
                {
                    title = "Edit Course";
                }
                else if (action == "delete")
                {
                    title = "Delete Course";
                }

                course = await Http.GetJsonAsync<Course>("/api/Course/Details/" + Convert.ToInt32(paramCourseID));
            }
        }

        protected async Task FetchCourse()
        {
            title = "Course Data";
            courseList = await Http.GetJsonAsync<List<Course>>("api/Course/Index");
        }

        protected async Task CreateCourse()
        {
            if (course.CourseId != 0)
            {
                await Http.SendJsonAsync(HttpMethod.Put, "api/Course/Edit", course);
            }
            else
            {
                await Http.SendJsonAsync(HttpMethod.Post, "/api/Course/Create", course);
            }
            UriHelper.NavigateTo("/course/fetch");
        }

        protected async Task DeleteCourse()
        {
            await Http.DeleteAsync("api/Course/Delete/" + Convert.ToInt32(paramCourseID));
            UriHelper.NavigateTo("/course/fetch");
        }

        protected void Cancel()
        {
            title = "Course Data";
            UriHelper.NavigateTo("/course/fetch");
        }
    }
}
