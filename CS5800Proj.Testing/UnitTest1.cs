using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;



namespace CS5800Proj.Testing
{
    // Noah Douglas Unit Tests
    public class LoginTests
    {
        [Theory]
        [InlineData("Noah", "password2")]
        public void BadCredentials(string user, string passWord)
        {

            var pageModel = new CS5800Proj.Pages.LogInModel();
            var httpContext = new DefaultHttpContext();
            
            //act                
            var result = pageModel.OnPost(user,passWord);

            Assert.IsType<PageResult>(result);
        }

        [Theory]
        [InlineData("Noah","password1")]
        public void GoodCredentials(string user, string passWord)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.LogInModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(user, passWord);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
        }
    }

    public class AccountCreationTests
    {
        [Theory]
        [InlineData("testNewUser", "password1", "password2","Donor")]
        public void PassWordsMismatch(string user, string passWord1, string passWord2, string userType)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AccountCreationModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(user, passWord1, passWord2, userType);

            //assert
            Assert.IsType<PageResult>(result);
        }
        [Theory]
        [InlineData("testNewUser", "password1", "password1", "Select...")]
        public void DidNotSelectAccountType(string user, string passWord1, string passWord2, string userType)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AccountCreationModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(user, passWord1, passWord2, userType);

            //assert
            Assert.IsType<PageResult>(result);
        }
        [Theory]
        [InlineData("", "password1", "password1", "Donor")]
        [InlineData("test", "", "password1", "Donor")]
        [InlineData("test", "password1", "", "Donor")]
        public void AnyFieldsLeftBlank(string user, string passWord1, string passWord2, string userType)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AccountCreationModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(user, passWord1, passWord2, userType);

            //assert
            Assert.IsType<PageResult>(result);
        }
    }
}
