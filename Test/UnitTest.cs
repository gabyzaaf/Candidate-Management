using System;
using System.Linq;
using Core.Adapter;
using Xunit;

namespace Test.user{

public class UnitTest{
delegate int DelegateType(int i);

        [Fact]
        public void ReturnFalseGivenValueOf1()
        {
           Assert.True(true);
        }

        [Fact]
        public void ShouldReturn(){
            DelegateType func = x => x+x;
            Assert.Equal(func(2),4);
        }

        [Fact]
        public void ShouldReturn2(){
            DelegateType func = x => x+x;
            int[] nombres = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            System.Console.WriteLine(nombres.Count(x => x%2==0));
            Assert.Equal(func(2),4);
        }

      
        private void ShouldTestRemind(string action)
        {
            try{
                MysqlDb db = new MysqlDb();
                db.typeAction(action,0,DateTime.Now,8,"ADD");
            }catch(Exception exc){
                Console.WriteLine("erreur is --> "+exc.Message);
                Assert.Equal(2,0);
            }
            
        }

           [Fact]
           public void ShouldTestaRelancerLKD(){
                ShouldTestRemind("aRelancerLKD");
           }

             [Fact]
            public void ShouldTestaRelancerMail(){
                ShouldTestRemind("aRelancerMail");
           }

            [Fact]
            public void ShouldTestPAERemind(){
                ShouldTestRemind("PAERemind");
           }
        [Fact]
           public void ShouldTestHcLangue(){
                ShouldTestRemind("HcLangue");
           }
             [Fact]
           public void ShouldTestHCGeo(){
                ShouldTestRemind("HCGeo");
           }



}

}