using System;
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

        protected Districts[] districtsList;
        protected SubDistricts[] subDistrictsList;

        //protected List<Districts> districtsList = new List<Districts>();
        //protected List<SubDistricts> subDistrictsList = new List<SubDistricts>();
        protected Districts dstM = new Districts();
        protected SubDistricts subdstD = new SubDistricts();

        protected Boolean showAddMaster = false;
        protected Boolean showAddDetail = false;

        protected int showDetailStatus = 0;
        protected int sortStatus = 0;
        protected int districtsIDs = 0;
        protected string Imagename = "Images/toggle.png";
        protected string ImageSortname = "Images/sortAsc.png";

        protected string Messages = "";
        protected string title { get; set; }

        protected override async Task OnInitAsync()
        {
            //districtsList = await Http.GetJsonAsync<List<Districts>>("api/Districts/Index");
            districtsList = await Http.GetJsonAsync<Districts[]>("/api/Districts/Index");
            dstM = new Districts();
            subdstD = new SubDistricts();
            Messages = "";
        }

        public void OnGet()
        {
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
                Messages = "Add Districts";
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

        protected async Task FetchDistricts()
        {
            Messages = "Fetch Districts Data";
            districtsList = await Http.GetJsonAsync<Districts[]>("api/Districts/Index");
        }

        //to Add New Districts

        protected async void AddNewDistricts()
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
            Messages = "Districts Save to Database !";
            showAddMaster = false;
            dstM = new Districts();
            UriHelper.NavigateTo("/districts/fetch");

            //districtsList = await Http.GetJsonAsync<Districts[]>("/api/Districts/");
        }

        //Edit Districts
        protected async Task EditDistricts()
        {
            showAddMaster = true;
            if (dstM.DistrictId != 0)
            {
                await Http.SendJsonAsync(HttpMethod.Put, "api/Districts/Edit", dstM);
            }
            else
            {
                await Http.SendJsonAsync(HttpMethod.Post, "/api/Districts/Create", dstM);
            }
            UriHelper.NavigateTo("/districts/fetch");
            // dstM = await Http.GetJsonAsync<Districts>("/api/Districts/" + Convert.ToInt32(DistrictsIDs));
        }

        protected async Task EditDistricts(int DistrictsIDs)
        {
            //showAddMaster = true;
            //districtsList = await Http.GetJsonAsync<Districts>("/api/Districts/" + Convert.ToInt32(DistrictsIDs));
            //districtsList = await Http.GetJsonAsync<Districts[]>("api/Districts/" + Convert.ToInt32(DistrictsIDs));

            showAddMaster = true;
            if (DistrictsIDs != 0)
            {
                await Http.SendJsonAsync(HttpMethod.Put, "api/Districts/Edit", dstM);
                Messages = "Districts Updated to Database !";
                showAddMaster = false;
                dstM = new Districts();
                UriHelper.NavigateTo("/districts/fetch");
            }
        }

        //Delete Districts
        protected async Task DeleteDistricts(int DistrictsIDs)
        {
            await Http.DeleteAsync("/api/Districts/Delete/" + Convert.ToInt32(DistrictsIDs));
            Messages = "Districts Deleted from Database !";
            UriHelper.NavigateTo("/districts/fetch");

            //districtsList = await Http.GetJsonAsync<Districts[]>("/api/Districts/");           
        }

        protected void Cancel()
        {
            title = "Districts Data";
            UriHelper.NavigateTo("/districts/fetch");
        }

        //Sorting
        protected async Task DistrictSorting(string SortColumn)
        {
            districtsList = await Http.GetJsonAsync<Districts[]>("/api/Districts/");
            //districtsList = await Http.GetJsonAsync<List<Districts>>("api/Districts/Index");
            Messages = "";

            if (sortStatus == 1)
            {
                ImageSortname = "Images/sortDec.png";
                sortStatus = 0;

                switch (SortColumn)
                {
                    case "DistrictId":
                        districtsList = districtsList.OrderBy(x => x.DistrictId).ToArray();
                        break;
                    case "DistrictName":
                        districtsList = districtsList.OrderBy(x => x.DistrictName).ToArray();
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
                        districtsList = districtsList.OrderByDescending(x => x.DistrictId).ToArray();
                        break;
                    case "DistrictName":
                        districtsList = districtsList.OrderByDescending(x => x.DistrictName).ToArray();
                        break;

                }
            }
        }

        // For Filtering by DistrictId
        protected async Task OnDistrictIdChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            await studentFilteringList(values, "DistrictId");
        }

        // For Filtering by DistrictName
        protected async Task OnDistrictNameChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            await studentFilteringList(values, "DistrictName");
        }

        // For Filtering by SubDistrictsId
        protected async Task OnSubDistrictsIdChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            await studentFilteringList(values, "SubDistrictsId");
        }

        // For Filtering by StateId
        protected async Task OnSubDistrictNameChanged(UIChangeEventArgs args)
        {
            string values = args.Value.ToString();
            await studentFilteringList(values, "SubDistrictName");
        }

       //Filtering
        protected async Task studentFilteringList(String Value, string columnName)
        {
            districtsList = await Http.GetJsonAsync<Districts[]>("/api/Districts/");
            Messages = "";
            if (Value.Trim().Length > 0)
            {
                switch (columnName)
                {
                    case "DistrictName":
                        districtsList = districtsList.Where(x => x.DistrictName.Contains(Value)).ToArray();
                        break;
                }
            }
            else
            {
                districtsList = await Http.GetJsonAsync<Districts[]>("/api/Districts/");
            }
        }


        //--------------- Detail Grid CRUD
        protected async Task getSubDistricts(int dstID)
        {
            showAddMaster = false;
            showAddDetail = false;
            Messages = "";
            if (districtsIDs != dstID)
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
            districtsIDs = dstID;
            subDistrictsList = await Http.GetJsonAsync<SubDistricts[]>("/api/SubDistricts/" + Convert.ToInt32(dstID));

        }
        //to Add New SubDistricts


        protected void AddNewSubDistricts(int districtsID)
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
                //await Http.SendJsonAsync(HttpMethod.Post, "/api/SubDistricts/", subdstD);
                await Http.SendJsonAsync(HttpMethod.Post, "/api/SubDistricts/Create", subdstD);
            }
            else
            {
                //await Http.SendJsonAsync(HttpMethod.Put, "/api/SubDistricts/" + subdstD.SubDistrictsId, subdstD);
                await Http.SendJsonAsync(HttpMethod.Put, "api/SubDistricts/Edit", dstM);
            }
            Messages = "SubDistricts Saved to Databse !";
            showAddDetail = false;
            showAddMaster = false;
            subdstD = new SubDistricts();
            //UriHelper.NavigateTo("/SubDistricts/fetch");
            subDistrictsList = await Http.GetJsonAsync<SubDistricts[]>("/api/SubDistricts/" + Convert.ToInt32(subdstD.DistrictId));
        }

        //Edit Sub Districts
        protected async Task EditSubDistricts(int SubDistrictsIDs)
        {
            subdstD = await Http.GetJsonAsync<SubDistricts>("/api/SubDistricts/" + Convert.ToInt32(SubDistrictsIDs));
            showAddDetail = true;
            showAddMaster = false;
        }

        //Delete Districts
        protected async Task DeleteSubDistricts(int SubDistrictsIDs)
        {
            var districtsValue = subdstD.DistrictId;
            await Http.DeleteAsync("/api/SubDistricts/" + Convert.ToInt32(SubDistrictsIDs));
            subDistrictsList = await Http.GetJsonAsync<SubDistricts[]>("/api/SubDistricts/" + Convert.ToInt32(districtsValue));
            Imagename = "Images/toggle.png";
            showDetailStatus = 0;
            Messages = "SubDistricts Deleted from Database !";
        }

        protected void closeMessage()
        {
            Messages = "";
        }
    }
}