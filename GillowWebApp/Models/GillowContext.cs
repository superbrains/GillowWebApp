using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GillowWebApp.Models;
using GillowWebApp2.Models;

namespace GillowWebApp.Models
{
    public class GillowContext: DbContext
    {
		public GillowContext()
		{
		}

		public GillowContext(DbContextOptions<GillowContext> options) : base(options)
		{

		}



		public DbSet<Invoice> Invoice { get; set; }
		public DbSet<Payment> Payment { get; set; }
		public DbSet<ProductImages> ProductImages { get; set; }
		public DbSet<ProductQuotation> ProductQuotations { get; set; }
		public DbSet<ProductQuotationRequest> ProductQuotationRequests { get; set; }
		public DbSet<Products> Products { get; set; }
		public DbSet<Profile> Profile { get; set; }
		public DbSet<Properties> Properties { get; set; }
		public DbSet<Property3D> Property3D { get; set; }
		public DbSet<PropertyFeatures> PropertyFeatures { get; set; }
		public DbSet<PropertyImages> PropertyImages { get; set; }
		public DbSet<Reviews> Reviews { get; set; }
		public DbSet<ServiceImages> ServiceImages { get; set; }
		public DbSet<ServiceQuotation> ServiceQuotation { get; set; }
		public DbSet<ServiceRequests> ServiceRequests { get; set; }
		public DbSet<VideoSchedules> VideoSchedules { get; set; }
		public DbSet<GillowWebApp.Models.BusinessArea> BusinessArea { get; set; }
		public DbSet<GillowWebApp.Models.CallLog> CallLog { get; set; }
		public DbSet<GillowWebApp.Models.Services> Services { get; set; }

		public DbSet<PropertySubscription> PropertySubscription { get; set; }

		public DbSet<Tokens> Tokens { get; set; }
		public DbSet<VersionInfo> VersionInfos { get; set; }
		public DbSet<GillowWebApp2.Models.InspectionPayment> InspectionPayment { get; set; }

		public DbSet<RefundRequest> RefundRequests { get; set; }

		public DbSet<Banks> Banks { get; set; }

		public DbSet<VirtualTourRequests> VirtualTourRequests { get; set; }
		public DbSet<Referals> Referals { get; set; }
		public DbSet<ReferalSubscription> ReferalSubscriptions { get; set; }
		public DbSet<ReferalPayment> ReferalPayments { get; set; }
		public DbSet<SpecialAgents> SpecialAgents { get; set; }
	}
}
