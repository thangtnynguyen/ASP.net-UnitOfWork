namespace AFF_BE.Core.Constants.Identity
{
    public class PermissionConstant
    {
        // Master Permissions
        public const string Admin = "A";
        public const string Master = "M";
        public const string Employee = "E";


        // Branch Permissions
        public const string ManageBranch = "MB";
        public const string ManageBranchView = "MB.V";
        public const string ManageBranchCreate = "MB.C";
        public const string ManageBranchEdit = "MB.E";
        public const string ManageBranchDelete = "MB.D";

        // WarrantyPolicy Permissions
        public const string ManageWarrantyPolicy = "MWP";
        public const string ManageWarrantyPolicyView = "MWP.V";
        public const string ManageWarrantyPolicyCreate = "MWP.C";
        public const string ManageWarrantyPolicyEdit = "MWP.E";
        public const string ManageWarrantyPolicyDelete = "MWP.D";

        // ProductCategory Permissions
        public const string ManageProductCategory = "MPC";
        public const string ManageProductCategoryView = "MPC.V";
        public const string ManageProductCategoryCreate = "MPC.C";
        public const string ManageProductCategoryEdit = "MPC.E";
        public const string ManageProductCategoryDelete = "MPC.D";

        // Product Permissions
        public const string ManageProduct = "MP";
        public const string ManageProductView = "MP.V";
        public const string ManageProductCreate = "MP.C";
        public const string ManageProductEdit = "MP.E";
        public const string ManageProductDelete = "MP.D";

        // Brand Permissions
        public const string ManageBrand = "MBR";
        public const string ManageBrandView = "MBR.V";
        public const string ManageBrandCreate = "MBR.C";
        public const string ManageBrandEdit = "MBR.E";
        public const string ManageBrandDelete = "MBR.D";

        // Inventory Permissions
        public const string ManageInventory = "MI";
        public const string ManageInventoryView = "MI.V";
        public const string ManageInventoryCreate = "MI.C";
        public const string ManageInventoryEdit = "MI.E";
        public const string ManageInventoryDelete = "MI.D";
        public const string ManageInventoryStatisticsBranch = "MI.SB";
        public const string ManageInventoryStatisticsTotal = "MI.ST";

        // InventoryStockIn Permissions
        public const string ManageInventoryStockIn = "MISI";
        public const string ManageInventoryStockInView = "MISI.V";
        public const string ManageInventoryStockInCreate = "MISI.C";
        public const string ManageInventoryStockInEdit = "MISI.E";
        public const string ManageInventoryStockInDelete = "MISI.D";

        // WarrantyClaim Permissions
        public const string ManageWarrantyClaim = "MWC";
        public const string ManageWarrantyClaimView = "MWC.V";
        public const string ManageWarrantyClaimCreate = "MWC.C";
        public const string ManageWarrantyClaimEdit = "MWC.E";
        public const string ManageWarrantyClaimDelete = "MWC.D";

        // BillOfLading Permissions
        public const string ManageBillOfLading = "MBL";
        public const string ManageBillOfLadingView = "MBL.V";
        public const string ManageBillOfLadingCreate = "MBL.C";
        public const string ManageBillOfLadingEdit = "MBL.E";
        public const string ManageBillOfLadingDelete = "MBL.D";

        // User Permissions
        public const string ManageUser = "MU";
        public const string ManageUserView = "MU.V";
        public const string ManageUserCreate = "MU.C";
        public const string ManageUserEdit = "MU.E";
        public const string ManageUserDelete = "MU.D";

        // Customer Permissions
        public const string CustomerUser = "MC";
        public const string ManageCustomerView = "MC.V";
        public const string ManageCustomerCreate = "MC.C";
        public const string ManageCustomerEdit = "MC.E";
        public const string ManageCustomerDelete = "MC.D";

        // Role Permissions
        public const string ManageRole = "MR";
        public const string ManageRoleView = "MR.V";
        public const string ManageRoleCreate = "MR.C";
        public const string ManageRoleEdit = "MR.E";
        public const string ManageRoleDelete = "MR.D";

        // Permission Permissions
        public const string ManagePermission = "MPM";
        public const string ManagePermissionView = "MPM.V";
        public const string ManagePermissionCreate = "MPM.C";
        public const string ManagePermissionEdit = "MPM.E";
        public const string ManagePermissionDelete = "MPM.D";

        // ActivateWarranty Permissions
        public const string ManageActivateWarranty = "MAW";
        public const string ManageActivateWarrantyActivate = "MAW.A";
    }
}





//namespace Shares.Static
//{
//    public class Permission
//    {

//        //BHDT

//        ////Master Permissions
//        //public const string Admin = "Admin";
//        //public const string Master = "Master";

//        //// Branch Permissions
//        //public const string ManageBranch = "ManageBranch";
//        //public const string ManageBranchView = "ManageBranch.View";
//        //public const string ManageBranchCreate = "ManageBranch.Create";
//        //public const string ManageBranchEdit = "ManageBranch.Edit";
//        //public const string ManageBranchDelete = "ManageBranch.Delete";

//        //// WarrantyPolicy Permissions
//        //public const string ManageWarrantyPolicy = "ManageWarrantyPolicy";
//        //public const string ManageWarrantyPolicyView = "ManageWarrantyPolicy.View";
//        //public const string ManageWarrantyPolicyCreate = "ManageWarrantyPolicy.Create";
//        //public const string ManageWarrantyPolicyEdit = "ManageWarrantyPolicy.Edit";
//        //public const string ManageWarrantyPolicyDelete = "ManageWarrantyPolicy.Delete";

//        //// ProductCategory Permissions
//        //public const string ManageProductCategory = "ManageProductCategory";
//        //public const string ManageProductCategoryView = "ManageProductCategory.View";
//        //public const string ManageProductCategoryCreate = "ManageProductCategory.Create";
//        //public const string ManageProductCategoryEdit = "ManageProductCategory.Edit";
//        //public const string ManageProductCategoryDelete = "ManageProductCategory.Delete";

//        //// Product Permissions
//        //public const string ManageProduct = "ManageProduct";
//        //public const string ManageProductView = "ManageProduct.View";
//        //public const string ManageProductCreate = "ManageProduct.Create";
//        //public const string ManageProductEdit = "ManageProduct.Edit";
//        //public const string ManageProductDelete = "ManageProduct.Delete";

//        //// Brand Permissions
//        //public const string ManageBrand = "ManageBrand";
//        //public const string ManageBrandView = "ManageBrand.View";
//        //public const string ManageBrandCreate = "ManageBrand.Create";
//        //public const string ManageBrandEdit = "ManageBrand.Edit";
//        //public const string ManageBrandDelete = "ManageBrand.Delete";

//        //// Inventory Permissions
//        //public const string ManageInventory = "ManageInventory";
//        //public const string ManageInventoryView = "ManageInventory.View";
//        //public const string ManageInventoryCreate = "ManageInventory.Create";
//        //public const string ManageInventoryEdit = "ManageInventory.Edit";
//        //public const string ManageInventoryDelete = "ManageInventory.Delete";
//        //        //Statistics Inventory Permission
//        //public const string ManageInventoryStatisticsBranch = "ManageInventory.StatisticsBranch";
//        //public const string ManageInventoryStatisticsTotal = "ManageInventory.StatisticsTotal";


//        //// InventoryStockIn Permissions
//        //public const string ManageInventoryStockIn = "ManageInventoryStockIn";
//        //public const string ManageInventoryStockInView = "ManageInventoryStockIn.View";
//        //public const string ManageInventoryStockInCreate = "ManageInventoryStockIn.Create";
//        //public const string ManageInventoryStockInEdit = "ManageInventoryStockIn.Edit";
//        //public const string ManageInventoryStockInDelete = "ManageInventoryStockIn.Delete";

//        //// WarrantyClaim Permissions
//        //public const string ManageWarrantyClaim = "ManageWarrantyClaim";
//        //public const string ManageWarrantyClaimView = "ManageWarrantyClaim.View";
//        //public const string ManageWarrantyClaimCreate = "ManageWarrantyClaim.Create";
//        //public const string ManageWarrantyClaimEdit = "ManageWarrantyClaim.Edit";
//        //public const string ManageWarrantyClaimDelete = "ManageWarrantyClaim.Delete";

//        //// BillOfLading Permissions
//        //public const string ManageBillOfLading = "ManageBillOfLading";
//        //public const string ManageBillOfLadingView = "ManageBillOfLading.View";
//        //public const string ManageBillOfLadingCreate = "ManageBillOfLading.Create";
//        //public const string ManageBillOfLadingEdit = "ManageBillOfLading.Edit";
//        //public const string ManageBillOfLadingDelete = "ManageBillOfLading.Delete";

//        //// User Permissions
//        //public const string ManageUser = "ManageUser";
//        //public const string ManageUserView = "ManageUser.View";
//        //public const string ManageUserCreate = "ManageUser.Create";
//        //public const string ManageUserEdit = "ManageUser.Edit";
//        //public const string ManageUserDelete = "ManageUser.Delete";

//        //// Role Permissions
//        //public const string ManageRole = "ManageRole";
//        //public const string ManageRoleView = "ManageRole.View";
//        //public const string ManageRoleCreate = "ManageRole.Create";
//        //public const string ManageRoleEdit = "ManageRole.Edit";
//        //public const string ManageRoleDelete = "ManageRole.Delete";

//        //// Permission Permissions
//        //public const string ManagePermission = "ManagePermission";
//        //public const string ManagePermissionView = "ManagePermission.View";
//        //public const string ManagePermissionCreate = "ManagePermission.Create";
//        //public const string ManagePermissionEdit = "ManagePermission.Edit";
//        //public const string ManagePermissionDelete = "ManagePermission.Delete";


//        //// ActivateWarranty Permissions
//        //public const string ManageActivateWarranty = "ManageActivateWarranty";
//        //public const string ManageActivateWarrantyActivate = "ManageActivateWarranty.Activate";










//    }





//}
