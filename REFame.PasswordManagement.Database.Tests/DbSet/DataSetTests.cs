﻿using System.Data.Common;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Database.Tests.DbSet
{
    [TestFixture]
    public class DataSetTests : DatabaseSetup
    {
        [Test]
        public void DataSetTestAdding()
        {
            var testDataSet = new DataSet<PASSWORDDATA>(options);

            int countBeforeAdd = testDataSet.Entities.Count();

            testDataSet.Add(new PASSWORDDATA
            {
                PWID = "ID1",
                PWDESCRIPTION = "TEST1",
                PWCOMMENT = "TEST1",
                PWDATA = "TESTPW1"
            });
            testDataSet.SaveChanges();

            int countAfterAdd = testDataSet.Entities.Count();

            Assert.That(countAfterAdd, Is.GreaterThan(countBeforeAdd));
        }
    }
}