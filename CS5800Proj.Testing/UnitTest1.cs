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

    public class AddTests
    {
        [Theory]
        [InlineData("U.S", "Money", 10, "TODO")]
        public void validAdd(string location, string catagory, int amount,  string request)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AddModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(location, catagory, amount, request);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
        }
        [Theory]
        [InlineData("U.S", "Money", 10, "TODO")]
        [InlineData("U.S", "", -1, "TODO")]
        public void AnyFieldsLeftBlank(string location, string catagory, int amount, string request)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AddModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(location, catagory, amount, request);

            //assert
            Assert.IsType<PageResult>(result);
        }
    }

    public class DeleteTests
    {
        [Theory]
        [InlineData("U.S", "Money", 10, "TODO")]
        public void validDelete(string location, string catagory, int amount, string request)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AddModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(location, catagory, amount, request);

            //arrange
            var pageModel2 = new CS5800Proj.Pages.DeleteModel();
            var httpContext2 = new DefaultHttpContext();

            //act                
            var result2 = pageModel2.OnPost(catagory+'('+amount+')'+'-'+location);

            //assert
            Assert.IsType<RedirectToPageResult>(result2);
        }
    }

    public class ModifyTests
    {
        [Theory]
        [InlineData("Money(10)-U.S","U.S", "Money", 10, "TODO")]
        public void validModify(string mod_id, string location, string catagory, int amount, string request)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AddModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(location, catagory, amount, request);

            //arrange
            var pageModel2 = new CS5800Proj.Pages.ModifyModel();
            var httpContext2 = new DefaultHttpContext();

            //act                
            var result2 = pageModel2.OnPost(mod_id, location, catagory, amount, request);

            //assert
            Assert.IsType<RedirectToPageResult>(result2);
        }

        [Theory]
        [InlineData("Money(-1)-U.S", "U.S", "Money", -1, "TODO")]
        public void negitiveModify(string mod_id, string location, string catagory, int amount, string request)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.AddModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(location, catagory, amount, request);

            //arrange
            var pageModel2 = new CS5800Proj.Pages.ModifyModel();
            var httpContext2 = new DefaultHttpContext();

            //act                
            var result2 = pageModel2.OnPost(mod_id, location, catagory, amount, request);

            //assert
            Assert.IsType<PageResult>(result2);
        }
    }

    // Austin Wittenburg Unit Tests
    public class EventCreationTests
	{
        [Theory]
        [InlineData("", "", "")]
        public void BadInputs3(string recipient, string time, string location)
        {

            var pageModel = new CS5800Proj.Pages.EventCreationModel();
            var httpContext = new DefaultHttpContext();
            var result = pageModel.OnPost(recipient, time, location);

            Assert.IsType<PageResult>(result);
        }

        [Theory]
        [InlineData("Good Name", "", "")]
        public void BadInputs2(string recipient, string time, string location)
        {

            var pageModel = new CS5800Proj.Pages.EventCreationModel();
            var httpContext = new DefaultHttpContext();
            var result = pageModel.OnPost(recipient, time, location);

            Assert.IsType<PageResult>(result);
        }

        [Theory]
        [InlineData("Good Name", "Good Date", "")]
        public void BadInputs1(string recipient, string time, string location)
        {

            var pageModel = new CS5800Proj.Pages.EventCreationModel();
            var httpContext = new DefaultHttpContext();              
            var result = pageModel.OnPost(recipient, time, location);

            Assert.IsType<PageResult>(result);
        }

        [Theory]
        [InlineData("Name", "3/15/2021", "Location")]
        public void GoodInputs(string recipient, string time, string location)
        {

            var pageModel = new CS5800Proj.Pages.EventCreationModel();
            var httpContext = new DefaultHttpContext();
            var result = pageModel.OnPost(recipient, time, location);

            Assert.IsType<RedirectToPageResult>(result);
        }
    }

    public class ResponseTests
	{
        [Theory]
        [InlineData("", "", 0)]
        public void BadInputs3(string recipient, string type, int amount)
        {

            var pageModel = new CS5800Proj.Pages.ResponseModel();
            var httpContext = new DefaultHttpContext();
            var result = pageModel.OnPost(recipient, type, amount);

            Assert.IsType<RedirectToPageResult>(result);
        }

        [Theory]
        [InlineData("", "", 100)]
        public void BadInputs2(string recipient, string type, int amount)
        {

            var pageModel = new CS5800Proj.Pages.ResponseModel();
            var httpContext = new DefaultHttpContext();
            var result = pageModel.OnPost(recipient, type, amount);

            Assert.IsType<RedirectToPageResult>(result);
        }

        [Theory]
        [InlineData("", "material", 100)]
        public void BadInputs1(string recipient, string type, int amount)
        {

            var pageModel = new CS5800Proj.Pages.ResponseModel();
            var httpContext = new DefaultHttpContext();
            var result = pageModel.OnPost(recipient, type, amount);

            Assert.IsType<RedirectToPageResult>(result);
        }

        [Theory]
        [InlineData("testName2", "money", 100)]
        public void GoodInputs(string recipient, string type, int amount)
        {

            var pageModel = new CS5800Proj.Pages.ResponseModel();
            var httpContext = new DefaultHttpContext();
            var result = pageModel.OnPost(recipient, type, amount);

            Assert.IsType<RedirectToPageResult>(result);
        }
    }

    public class PledgeTests
    {
        [Theory]
        [InlineData("U.S", "Money", 10)]
        public void validPledge(string location, string catagory, int amount)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.PledgeModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(location, catagory, amount);

            //assert
            Assert.IsType<RedirectToPageResult>(result);
        }
        [Theory]
        [InlineData("U.S", "Money", 10)]
        [InlineData("U.S", "", -1)]
        public void AnyFieldsLeftBlank(string location, string catagory, int amount)
        {
            //arrange
            var pageModel = new CS5800Proj.Pages.PledgeModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(location, catagory, amount);

            //assert
            Assert.IsType<PageResult>(result);
        }
    }
}
