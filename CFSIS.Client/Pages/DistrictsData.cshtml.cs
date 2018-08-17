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
    public class DistrictsDataModel : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string paramDistrictID { get; set; } = "0";
        [Parameter]
        protected string action { get; set; }

        Districts[] districts;
        SubDistricts[] subDistricts;

        protected List<Districts> districtsList = new List<Districts>();
        Districts dstM = new Districts();
        SubDistricts subdstD = new SubDistricts();

        Boolean showAddMaster = false;
        Boolean showAddDetail = false;

        int showDetailStatus = 0;
        int sortStatus = 0;
        int districtsIDs = 0;
        string Imagename = "Images/toggle.png";
        string ImageSortname = "Images/sortAsc.png";

        string Messages = "";

        protected override async Task OnInitAsync()
        {
            districtsList = await Http.GetJsonAsync<List<Districts>>("api/Districts/Index");
            //districts = await Http.GetJsonAsync<Districts[]>("/api/Districts/Index");
            subdstD = new SubDistricts();
            dstM = new Districts();
            Messages = "";
        }

        protected override async Task OnParametersSetAsync()
        {
            if (action == "fetch")
            {
                await FetchDistricts();
                this.StateHasChanged();
            }
            else if (action == "create")
            {
                Messages = "Add Course";
                dstM = new Districts();
            }
            else if (paramDistrictID != "0")
            {
                if (action == "edit")
                {
                    Messages = "Edit Districts";
                }
                else if (action == "delete")
                {
                    Messages = "Delete Districts";
                }

                dstM = await Http.GetJsonAsync<Districts>("/api/Districts/Details/" + Convert.ToInt32(paramDistrictID));
            }
        }

        public void OnGet()
        {
        }

        protected async Task FetchDistricts()
        {
            Messages = "Fetch Districts Data";
            districtsList = await Http.GetJsonAsync<List<Districts>>("api/Districts/Index");
        }

        //to Add New Districts

        void AddNewDistricts()
        {

            dstM = new Districts();
            showAddMaster = true;
            showAddDetail = false;
            Messages = "";
        }

        //Save New or update Districts

        protected async Task SaveDistricts()
        {
            if (dstM.DistrictId == 0)

            {
                await Http.SendJsonAsync(HttpMethod.Post, "/api/Districts/Create", dstM);
            }
            else
            {
                //await Http.SendJsonAsync(HttpMethod.Put, "/api/Districts/" + dstM.DistrictId, dstM);
                await Http.SendJsonAsync(HttpMethod.Put, "api/Districts/Edit", dstM);
            }
            UriHelper.NavigateTo("/Districts/fetch");

            dstM = new Districts();
            districts = await Http.GetJsonAsync<Districts[]>("/api/Districts/");

            Messages = "Districts Save to Database !";
            showAddMaster = false;

        }




        //Edit Districts


        protected async Task EditDistricts()
        {
            showAddMaster = true;

            if (Districts.DistrictsIDs != 0)
            {
                await Http.SendJsonAsync(HttpMethod.Put, "api/Districts/Edit", course);
            }
            else
            {
                await Http.SendJsonAsync(HttpMethod.Post, "/api/Districts/Create", course);
            }
            UriHelper.NavigateTo("/course/fetch");


# dstM = await Http.GetJsonAsync<Districts>("/api/Districts/" + Convert.ToInt32(DistrictsIDs));
        }

        //Delete Districts
        protected async Task DeleteDistricts(int DistrictsIDs)
        {
            await Http.DeleteAsync("/api/Districts/Delete/" + Convert.ToInt32(DistrictsIDs));
            UriHelper.NavigateTo("/course/fetch");

            districts = await Http.GetJsonAsync<Districts[]>("/api/Districts/");
            Messages = "Districts Deleted from Database !";
            
        }

        //Sorting

        protected async Task StudentSorting(string SortColumn)
        {
            districts = await Http.GetJsonAsync<Districts[]>("/api/Districts/");
            Messages = "";

            if (sortStatus == 1)
            {
                ImageSortname = "Images/sortDec.png";
                sortStatus = 0;

                switch (SortColumn)
                {
                    case "DistrictId":
                        districts = districts.OrderByDescending(x => x.DistrictId).ToArray();
                        break;
                    case "DistrictName":
                        districts = districts.OrderByDescending(x => x.DistrictName).ToArray();
                        break;
                }
            }
            else
            {
                ImageSortname = "Images/sortAsc.png";
                sortStatus = 1;

                switch (SortColumn)
                {
                    case "DistrictId":
                        districts = districts.OrderByDescending(x => x.DistrictId).ToArray();
                        break;
                    case "DistrictName":
                        districts = districts.OrderByDescending(x => x.DistrictName).ToArray();
                        break;

                }
            }
        }



        // For Filtering by DistrictId
        void OnDistrictIdChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            studentFilteringList(values, "DistrictId");
        }


        // For Filtering by DistrictName
        void OnDistrictNameChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            studentFilteringList(values, "DistrictName");
        }

        // For Filtering by SubDistrictsId
        void OnSubDistrictsIdChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            studentFilteringList(values, "SubDistrictsId");
        }

        // For Filtering by StateId
        void OnSubDistrictNameChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            studentFilteringList(values, "SubDistrictName");
        }





        //Filtering
        protected async Task studentFilteringList(String Value, string columnName)
        {
            districts = await Http.GetJsonAsync<Districts[]>("/api/Districts/");

            Messages = "";
            if (Value.Trim().Length > 0)
            {

                switch (columnName)
                {

                    case "DistrictName":
                        districts = districts.Where(x => x.DistrictName.Contains(Value)).ToArray();
                        break;


                }

            }
            else
            {
                districts = await Http.GetJsonAsync<Districts[]>("/api/Districts/");
            }
        }


        //--------------- Detail Grid CRUD



        protected async Task getSubDistricts(int ordID)
        {
            showAddMaster = false;
            showAddDetail = false;
            Messages = "";
            if (districtsIDs != ordID)
            {
                Imagename = "Images/expand.png";
                showDetailStatus = 1;

            }
            else
            {


                if (showDetailStatus == 0)
                {
                    Imagename = "Images/expand.png";
                    showDetailStatus = 1;
                }
                else
                {
                    Imagename = "Images/toggle.png";
                    showDetailStatus = 0;
                }

            }
            districtsIDs = ordID;
            subDistricts = await Http.GetJsonAsync<SubDistricts[]>("/api/SubDistricts/" + Convert.ToInt32(ordID));

        }
        //to Add New SubDistricts


        protected async Task AddNewSubDistricts(int districtsID)
        {
            subdstD = new SubDistricts();

            subdstD.DistrictId = districtsID;
            Messages = "";


            showAddDetail = true;
            showAddMaster = false;

        }

        //Save New or update Districts

        protected async Task SaveSubDistricts()
        {
            if (subdstD.SubDistrictsId == 0)

            {
                await Http.SendJsonAsync(HttpMethod.Post, "/api/SubDistricts/", subdstD);

            }
            else
            {
                await Http.SendJsonAsync(HttpMethod.Put, "/api/SubDistricts/" + subdstD.SubDistrictsId, subdstD);
            }

            subDistricts = await Http.GetJsonAsync<SubDistricts[]>("/api/SubDistricts/" + Convert.ToInt32(subdstD.DistrictId));
            subdstD = new SubDistricts();
            showAddDetail = false;
            showAddMaster = false;
            Messages = "SubDistricts Saved to Databse !";
        }




        //Edit Districts


        protected async Task EditSubDistricts(int SubDistrictsIDs)
        {

            subdstD = await Http.GetJsonAsync<SubDistricts>("/api/SubDistricts1/" + Convert.ToInt32(SubDistrictsIDs));
            showAddDetail = true;
            showAddMaster = false;
        }

        //Delete Districts
        protected async Task DeleteSubDistricts(int SubDistrictsIDs)
        {
            var districtsValue = subdstD.DistrictId;
            // ids = studentID.ToString();
            await Http.DeleteAsync("/api/SubDistricts/" + Convert.ToInt32(SubDistrictsIDs));

            // await Http.DeleteAsync("/api/StudentMasters/Delete/" + Convert.ToInt32(studentID));

            subDistricts = await Http.GetJsonAsync<SubDistricts[]>("/api/SubDistricts/" + Convert.ToInt32(districtsValue));
            Imagename = "Images/toggle.png";
            showDetailStatus = 0;
            Messages = "SubDistricts Deleted from Database !";
        }

        void closeMessage()
        {
            Messages = "";
        }
    }
}