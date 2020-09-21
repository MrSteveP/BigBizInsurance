// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActiveDirectoryHelper.cs" company="Usama Nada">
//   No Copyright .. Copy, Share, and Evolve.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CrossCutting.Core.Utils
{
    #region usings

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.DirectoryServices.AccountManagement;
    using System.Linq;

    #endregion

    /// <summary>
    ///     The ad helper.
    /// </summary>
    public class AdHelper
    {
        /// <summary>
        ///     The ad admin password.
        /// </summary>
        private readonly string AdAdminPassword;

        /// <summary>
        ///     The ad admin user.
        /// </summary>
        private readonly string AdAdminUser;

        /// <summary>
        ///     The ad default ou.
        /// </summary>
        private readonly string AdDefaultOU;

        /// <summary>
        ///     The ad domain.
        /// </summary>
        private readonly string AdDomain;

        // private static AdHelper mInstance;

        // private AdHelper(string domain, string ou, string user, string password)
        // {
        // mInstance.AdDomain = domain;
        // mInstance.AdDefaultOU = ou;
        // mInstance.AdAdminUser = user;
        // mInstance.AdAdminPassword = password;
        // }

        // public static AdHelper Instance => mInstance;

        // public static AdHelper Create(string domain, string ou, string user, string password)
        // {
        // return new AdHelper(domain, ou,user,password);
        // }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdHelper"/> class.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <param name="ou">
        /// The ou.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public AdHelper(string domain, string ou, string user, string password)
        {
            this.AdDomain = domain;
            this.AdDefaultOU = ou;
            this.AdAdminUser = user;
            this.AdAdminPassword = password;
        }

        /// <summary>
        /// Adds the user for a given group
        /// </summary>
        /// <param name="sUserName">
        /// The user you want to add to a group
        /// </param>
        /// <param name="sGroupName">
        /// The group you want the user to be added in
        /// </param>
        /// <returns>
        /// Returns true if successful
        /// </returns>
        public bool AddUserToGroup(string sUserName, string sGroupName)
        {
            try
            {
                var oUserPrincipal = this.GetUser(sUserName);
                var oGroupPrincipal = this.GetGroup(sGroupName);
                if (oUserPrincipal != null && oGroupPrincipal != null)
                {
                    if (!this.IsUserGroupMember(sUserName, sGroupName))
                    {
                        oGroupPrincipal.Members.Add(oUserPrincipal);
                        oGroupPrincipal.Save();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new group in Active Directory
        /// </summary>
        /// <param name="sGroupName">
        /// The name of the new group
        /// </param>
        /// <param name="sDescription">
        /// The description of the new group
        /// </param>
        /// <param name="bSecurityGroup">
        /// True is you want this group to be a security group, false if you want this as a
        ///     distribution group
        /// </param>
        /// <returns>
        /// Retruns the GroupPrincipal object
        /// </returns>
        public GroupPrincipal CreateNewGroup(string sGroupName, string sDescription, bool bSecurityGroup = true)
        {
            var oPrincipalContext = this.GetPrincipalContext(this.AdDefaultOU); // TODO : REMOVE OU IF UNEED TO USE ROOT
           
            var oGroupPrincipal = new GroupPrincipal(oPrincipalContext, sGroupName);
            oGroupPrincipal.Description = sDescription;
            oGroupPrincipal.GroupScope = GroupScope.Local;
            oGroupPrincipal.IsSecurityGroup = bSecurityGroup;
            oGroupPrincipal.Save();

            return oGroupPrincipal;
        }

        /// <summary>
        /// Creates a new user on Active Directory
        /// </summary>
        /// <param name="sFullName">
        /// The given name of the new user
        /// </param>
        /// <param name="sUserName">
        /// The username of the new user
        /// </param>
        /// <param name="sPassword">
        /// The password of the new user
        /// </param>
        /// <param name="sEmail">
        /// The s Email.
        /// </param>
        /// <param name="sPhone">
        /// The s Phone.
        /// </param>
        /// <param name="isActive">
        /// The is Active.
        /// </param>
        /// <returns>
        /// returns the UserPrincipal object
        /// </returns>
        public UserPrincipal CreateNewUser(
            string sFullName,
            string sUserName,
            string sPassword,
            string sEmail,
            string sPhone,
            bool isActive = true)
        {
            if (!this.IsUserExisiting(sUserName))
            {
                var oPrincipalContext =
                    this.GetPrincipalContext(this.AdDefaultOU); // TODO : REMOVE OU IF UNEED TO USE ROOT

                var oUserPrincipal = new UserPrincipal(oPrincipalContext, sUserName, sPassword, isActive)
                                         {
                                             UserPrincipalName = sUserName,
                                             DisplayName = sFullName,
                                             EmailAddress = sEmail,
                                             VoiceTelephoneNumber = sPhone
                                         };

                // User Log on Name
                oUserPrincipal.Save();

                return oUserPrincipal;
            }

            throw new Exception("UserExist");
        }

        /// <summary>
        /// Deletes a user in Active Directory
        /// </summary>
        /// <param name="sUserName">
        /// The username you want to delete
        /// </param>
        /// <returns>
        /// Returns true if successfully deleted
        /// </returns>
        public bool DeleteUser(string sUserName)
        {
            try
            {
                var oUserPrincipal = this.GetUser(sUserName);

                oUserPrincipal.Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Force disbaling of a user account
        /// </summary>
        /// <param name="sUserName">
        /// The username to disable
        /// </param>
        public void DisableUserAccount(string sUserName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            oUserPrincipal.Enabled = false;
            oUserPrincipal.Save();
        }

        /// <summary>
        /// Enables a disabled user account
        /// </summary>
        /// <param name="sUserName">
        /// The username to enable
        /// </param>
        public void EnableUserAccount(string sUserName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            oUserPrincipal.Enabled = true;
            oUserPrincipal.Save();
        }

        /// <summary>
        /// Force expire password of a user
        /// </summary>
        /// <param name="sUserName">
        /// The username to expire the password
        /// </param>
        public void ExpireUserPassword(string sUserName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            oUserPrincipal.ExpirePasswordNow();
            oUserPrincipal.Save();
        }

        /// <summary>
        /// Gets a certain group on Active Directory
        /// </summary>
        /// <param name="sGroupName">
        /// The group to get
        /// </param>
        /// <returns>
        /// Returns the GroupPrincipal Object
        /// </returns>
        public GroupPrincipal GetGroup(string sGroupName)
        {
            var oPrincipalContext = this.GetPrincipalContext();

            var oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            return oGroupPrincipal;
        }

        /// <summary>
        /// The get group members.
        /// </summary>
        /// <param name="sGroupName">
        /// The s group name.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<string> GetGroupMembers(string sGroupName)
        {
            var oPrincipalContext = this.GetPrincipalContext();

            var oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);

            var items = new List<string>();

            if (oGroupPrincipal == null) return null;

            items.AddRange(oGroupPrincipal.GetMembers().Select(member => member.SamAccountName));

            return items;
        }

        /// <summary>
        ///     Gets the base principal context
        /// </summary>
        /// <returns>Returns the PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext()
        {
            if (string.IsNullOrEmpty(this.AdAdminUser) || string.IsNullOrEmpty(this.AdAdminPassword))
            {
                return new PrincipalContext(ContextType.Domain, this.AdDomain, null, ContextOptions.Negotiate);
            }

            return new PrincipalContext(
                ContextType.Domain,
                this.AdDomain,
                null,
                ContextOptions.Negotiate,
                this.AdAdminUser,
                this.AdAdminPassword);
        }

        /// <summary>
        /// Gets the principal context on specified OU
        /// </summary>
        /// <param name="sOU">
        /// The OU you want your Principal Context to run on
        /// </param>
        /// <returns>
        /// Retruns the PrincipalContext object
        /// </returns>
        public PrincipalContext GetPrincipalContext(string sOU)
        {
            var oPrincipalContext = new PrincipalContext(
                ContextType.Domain,
                this.AdDomain,
                sOU,
                ContextOptions.Negotiate,
                this.AdAdminUser,
                this.AdAdminPassword);
            return oPrincipalContext;
        }

        /// <summary>
        /// Gets a certain user on Active Directory
        /// </summary>
        /// <param name="sUserName">
        /// The username to get
        /// </param>
        /// <returns>
        /// Returns the UserPrincipal Object
        /// </returns>
        public UserPrincipal GetUser(string sUserName)
        {
            var oPrincipalContext = this.GetPrincipalContext();

            var oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);
            return oUserPrincipal;
        }

        public List<UserPrincipal> GetAllUsers()
        {
            PrincipalContext AD = this.GetPrincipalContext(this.AdDefaultOU);
            UserPrincipal u = new UserPrincipal(AD);
            PrincipalSearcher search = new PrincipalSearcher(u);

            List<UserPrincipal> users = new List<UserPrincipal>();
            foreach (var principal in search.FindAll())
            {
                var result = (UserPrincipal) principal;
                if (result.Enabled != null && result.Enabled.Value)
                    users.Add(result);
            }

            return users;
        }
        public List<string> GetAllUserNames()
        {
            PrincipalContext AD = this.GetPrincipalContext(this.AdDefaultOU);
            UserPrincipal u = new UserPrincipal(AD);
            PrincipalSearcher search = new PrincipalSearcher(u);

            List<string> users = new List<string>();
            foreach (var principal in search.FindAll())
            {
                var result = (UserPrincipal)principal;
                if (result.Enabled != null && result.Enabled.Value)
                    users.Add(result.DisplayName);
            }

            return users;
        }

        /// <summary>
        /// Gets a list of the users authorization groups
        /// </summary>
        /// <param name="sUserName">
        /// The user you want to get authorization groups
        /// </param>
        /// <returns>
        /// Returns an arraylist of group authorization memberships
        /// </returns>
        public ArrayList GetUserAuthorizationGroups(string sUserName)
        {
            var myItems = new ArrayList();
            var oUserPrincipal = this.GetUser(sUserName);

            var oPrincipalSearchResult = oUserPrincipal.GetAuthorizationGroups();

            foreach (var oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }

            return myItems;
        }

        /// <summary>
        /// Gets a list of the users group memberships
        /// </summary>
        /// <param name="sUserName">
        /// The user you want to get the group memberships
        /// </param>
        /// <returns>
        /// Returns an arraylist of group memberships
        /// </returns>
        public ArrayList GetUserGroups(string sUserName)
        {
            var myItems = new ArrayList();
            var oUserPrincipal = this.GetUser(sUserName);

            var oPrincipalSearchResult = oUserPrincipal.GetGroups();

            foreach (var oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }

            return myItems;
        }

        /// <summary>
        /// The is account enabled.
        /// </summary>
        /// <param name="sUserName">
        /// The s user name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAccountEnabled(string sUserName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            return oUserPrincipal.Enabled.Value;
        }

        /// <summary>
        /// Checks if user accoung is locked
        /// </summary>
        /// <param name="sUserName">
        /// The username to check
        /// </param>
        /// <returns>
        /// Retruns true of Account is locked
        /// </returns>
        public bool IsAccountLocked(string sUserName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            return oUserPrincipal.IsAccountLockedOut();
        }

        /// <summary>
        /// The is group exisiting.
        /// </summary>
        /// <param name="sGroupName">
        /// The s group name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsGroupExisiting(string sGroupName)
        {
            return this.GetGroup(sGroupName) != null;
        }

        /// <summary>
        /// The is user email exisiting.
        /// </summary>
        /// <param name="sEmail">
        /// The s email.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsUserEmailExisiting(string sEmail)
        {
            var oPrincipalContext = this.GetPrincipalContext();
            var oUserPrincipal = new UserPrincipal(oPrincipalContext) { EmailAddress = sEmail };

            // create a principal searcher for running a search operation
            var pS = new PrincipalSearcher(oUserPrincipal);

            return pS.FindOne() != null;
        }

        /// <summary>
        /// Checks if user exsists on AD
        /// </summary>
        /// <param name="sUserName">
        /// The username to check
        /// </param>
        /// <returns>
        /// Returns true if username Exists
        /// </returns>
        public bool IsUserExisiting(string sUserName)
        {
            return this.GetUser(sUserName) != null;
        }

        /// <summary>
        /// Checks if the User Account is Expired
        /// </summary>
        /// <param name="sUserName">
        /// The username to check
        /// </param>
        /// <returns>
        /// Returns true if Expired
        /// </returns>
        public bool IsUserExpired(string sUserName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            if (oUserPrincipal.AccountExpirationDate != null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if user is a member of a given group
        /// </summary>
        /// <param name="sUserName">
        /// The user you want to validate
        /// </param>
        /// <param name="sGroupName">
        /// The group you want to check the membership of the user
        /// </param>
        /// <returns>
        /// Returns true if user is a group member
        /// </returns>
        public bool IsUserGroupMember(string sUserName, string sGroupName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            var oGroupPrincipal = this.GetGroup(sGroupName);

            if (oUserPrincipal != null && oGroupPrincipal != null)
            {
                return oGroupPrincipal.Members.Contains(oUserPrincipal);
            }

            return false;
        }

        /// <summary>
        /// The is user phone exisiting.
        /// </summary>
        /// <param name="sPhone">
        /// The s phone.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsUserPhoneExisiting(string sPhone)
        {
            var oPrincipalContext = this.GetPrincipalContext();
            var oUserPrincipal = new UserPrincipal(oPrincipalContext) { VoiceTelephoneNumber = sPhone };

            // create a principal searcher for running a search operation
            var pS = new PrincipalSearcher(oUserPrincipal);

            return pS.FindOne() != null;
        }

        /// <summary>
        /// Removes user from a given group
        /// </summary>
        /// <param name="sUserName">
        /// The user you want to remove from a group
        /// </param>
        /// <param name="sGroupName">
        /// The group you want the user to be removed from
        /// </param>
        /// <returns>
        /// Returns true if successful
        /// </returns>
        public bool RemoveUserFromGroup(string sUserName, string sGroupName)
        {
            try
            {
                var oUserPrincipal = this.GetUser(sUserName);
                var oGroupPrincipal = this.GetGroup(sGroupName);
                if (oUserPrincipal != null && oGroupPrincipal != null)
                {
                    if (this.IsUserGroupMember(sUserName, sGroupName))
                    {
                        oGroupPrincipal.Members.Remove(oUserPrincipal);
                        oGroupPrincipal.Save();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The search users.
        /// </summary>
        /// <param name="sFullName">
        /// The s full name.
        /// </param>
        /// <param name="sUserName">
        /// The s user name.
        /// </param>
        /// <param name="sEmail">
        /// The s email.
        /// </param>
        /// <param name="sPhone">
        /// The s phone.
        /// </param>
        /// <param name="isActive">
        /// The is active.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<UserPrincipal> SearchUsers(
            string sFullName = null,
            string sUserName = null,
            string sEmail = null,
            string sPhone = null,
            bool isActive = true)
        {
            var oPrincipalContext = this.GetPrincipalContext();

            var oUserPrincipal = new UserPrincipal(oPrincipalContext);

            oUserPrincipal.DisplayName = sFullName;
            oUserPrincipal.SamAccountName = sUserName;

            // oUserPrincipal.EmailAddress = sEmail;
            // oUserPrincipal.VoiceTelephoneNumber = sPhone;
            // oUserPrincipal.Enabled = isActive;

            // create your principal searcher passing in the QBE principal    
            var ps = new PrincipalSearcher { QueryFilter = oUserPrincipal };
            var results = ps.FindAll();

            return results.OfType<UserPrincipal>().ToList();

            // Create a PrincipalSearcher object to perform the search.

            // Tell the PrincipalSearcher what to search for.

            // Run the query. The query locates users 
            // that match the supplied user principal object. 
        }

        /// <summary>
        /// Sets the user password
        /// </summary>
        /// <param name="sUserName">
        /// The username to set
        /// </param>
        /// <param name="sNewPassword">
        /// The new password to use
        /// </param>
        /// <param name="sMessage">
        /// Any output messages
        /// </param>
        public void SetUserPassword(string sUserName, string sNewPassword, out string sMessage)
        {
            try
            {
                var oUserPrincipal = this.GetUser(sUserName);
                oUserPrincipal.SetPassword(sNewPassword);
                sMessage = string.Empty;
            }
            catch (Exception ex)
            {
                sMessage = ex.Message;
            }
        }

        /// <summary>
        /// Unlocks a locked user account
        /// </summary>
        /// <param name="sUserName">
        /// The username to unlock
        /// </param>
        public void UnlockUserAccount(string sUserName)
        {
            var oUserPrincipal = this.GetUser(sUserName);
            oUserPrincipal.UnlockAccount();
            oUserPrincipal.Save();
        }

        /// <summary>
        /// The update fields.
        /// </summary>
        /// <param name="Property">
        /// The property.
        /// </param>
        /// <param name="sUserName">
        /// The s user name.
        /// </param>
        /// <param name="Value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool UpdateFields(string Property, string sUserName, string Value)
        {
            var result = false;
            var domain = new PrincipalContext(
                ContextType.Domain,
                "127.0.0.1",
                "OU=Users,DC=dev,DC=loc",
                ContextOptions.SimpleBind);

            // Search for the user in the domain
            var searcher = new PrincipalSearcher();
            var findUser = new UserPrincipal(domain) { SamAccountName = sUserName };
            searcher.QueryFilter = findUser;
            var foundUser = (UserPrincipal)searcher.FindOne();
            if (foundUser != null)
            {
                using (var baseEntry = foundUser.GetUnderlyingObject() as DirectoryEntry)
                {
                    using (var entry = new DirectoryEntry(baseEntry.Path, baseEntry.Username, "P@ssw0rd"))
                    {
                        entry.Properties[Property].Value = Value;
                        entry.CommitChanges();
                        entry.Close();
                        result = true;
                    }
                }
            }
            else
            {
                return false;
            }

            return result;
        }

        /// <summary>
        /// The update group.
        /// </summary>
        /// <param name="sGroupName">
        /// The s group name.
        /// </param>
        /// <param name="sDescription">
        /// The s description.
        /// </param>
        /// <returns>
        /// The <see cref="GroupPrincipal"/>.
        /// </returns>
        public GroupPrincipal UpdateGroup(string sGroupName, string sDescription)
        {
            var oPrincipalContext = this.GetPrincipalContext(this.AdDefaultOU); // TODO : REMOVE OU IF UNEED TO USE ROOT

            var oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            oGroupPrincipal.SamAccountName = sGroupName;
            oGroupPrincipal.Description = sDescription;
            oGroupPrincipal.Save();

            var dirEntry = (DirectoryEntry)oGroupPrincipal.GetUnderlyingObject();
            dirEntry.Properties["sAmAccountName"].Value = sGroupName;
            dirEntry.Properties["description"].Value = sDescription;

            // save group
            dirEntry.CommitChanges();

            return oGroupPrincipal;
        }

        /// <summary>
        /// The update user.
        /// </summary>
        /// <param name="sFullName">
        /// The s full name.
        /// </param>
        /// <param name="sUserName">
        /// The s user name.
        /// </param>
        /// <param name="sEmail">
        /// The s email.
        /// </param>
        /// <param name="sPhone">
        /// The s phone.
        /// </param>
        /// <returns>
        /// The <see cref="UserPrincipal"/>.
        /// </returns>
        public UserPrincipal UpdateUser(string sFullName, string sUserName, string sEmail, string sPhone)
        {
            var oPrincipalContext = this.GetPrincipalContext(this.AdDefaultOU); // TODO : REMOVE OU IF UNEED TO USE ROOT

            var oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);

            if (oUserPrincipal != null)
            {
                // if found, update e-mail address 
                oUserPrincipal.DisplayName = sFullName;
                oUserPrincipal.EmailAddress = sEmail;
                oUserPrincipal.VoiceTelephoneNumber = sPhone;

                // call .Save() to persist your changes! 
                oUserPrincipal.Save();

                var entryToUpdate = (DirectoryEntry)oUserPrincipal.GetUnderlyingObject();
                entryToUpdate.Properties["displayName"].Value = sFullName;
                entryToUpdate.Properties["mail"].Value = sEmail;
                entryToUpdate.Properties["telephoneNumber"].Value = sPhone;

                entryToUpdate.CommitChanges();
            }

            return oUserPrincipal;
        }

        /// <summary>
        /// Validates the username and password of a given user
        /// </summary>
        /// <param name="sUserName">
        /// The username to validate
        /// </param>
        /// <param name="sPassword">
        /// The password of the username to validate
        /// </param>
        /// <returns>
        /// Returns True of user is valid
        /// </returns>
        public bool ValidateCredentials(string sUserName, string sPassword)
        {
            var oPrincipalContext = this.GetPrincipalContext();
            return oPrincipalContext.ValidateCredentials(sUserName, sPassword);
        }

       
        // TODO : SearchUsers && UpdateUser && GetUserByProperty
    }
}