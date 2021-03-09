using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;


//comment Austin Wittenburg 

namespace CS5800Proj.Testing
{
    public class UnitTest1
    {

        [Theory]
        [InlineData("Noah", "password2")]
        public void LogOnBadCredentials(string value1, string value2)
        {
            var pageModel = new CS5800Proj.Pages.LogInModel();
            var httpContext = new DefaultHttpContext();
            
            //act                
            var result = pageModel.OnPost(value1,value2);

            Assert.IsType<PageResult>(result);
        }

        [Theory]
        [InlineData("Noah","password1")]
        public void LogOnGoodCredentials(string value1, string value2)
        {
            var pageModel = new CS5800Proj.Pages.LogInModel();
            var httpContext = new DefaultHttpContext();

            //act                
            var result = pageModel.OnPost(value1, value2);

            Assert.IsType<RedirectToPageResult>(result);
        }
    }
}
