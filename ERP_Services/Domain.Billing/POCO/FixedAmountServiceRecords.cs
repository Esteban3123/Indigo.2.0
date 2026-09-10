using System;
using System.Collections.Generic;

namespace Domain.Billing.POCO
{
    /// <summary>
    /// Filters used to query service records for a fixed amount invoice.
    /// </summary>
    public class FixedAmountServiceRecordQuery
    {
        public string FixedAmountInvoiceNumber { get; set; }
        public int CareGroupId { get; set; }
        public int InvoiceCategoryId { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PreviousRIPSInvoiceId { get; set; }
        public List<string> PatientCodes { get; set; } = new List<string>();
        public DateTime? SentAt { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 200;
    }

    /// <summary>
    /// Service record that supports a fixed amount invoice.
    /// </summary>
    public class FixedAmountServiceRecordDto
    {
        public string InvoiceNumber { get; set; }
        public decimal Value { get; set; }
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string PatientCodeName { get; set; }
        public string AdmissionNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string HealthAdministrator { get; set; }
        public string CosmoDBId { get; set; }
        public DateTime? SendDate { get; set; }
        public byte? StatusRIPS { get; set; }
    }

    /// <summary>
    /// Paged response returned by service record queries.
    /// </summary>
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    /// <summary>
    /// Service record candidate for RIPS JSON rebuild.
    /// </summary>
    public class FixedAmountRebuildCandidateDto
    {
        public string InvoiceNumber { get; set; }
        public string PatientCode { get; set; }
        public string CosmoDBId { get; set; }
        public DateTime? SendDate { get; set; }
        public byte? StatusRIPS { get; set; }
    }

    /// <summary>
    /// Rebuild progress for fixed amount service records.
    /// </summary>
    public class FixedAmountRebuildStatusDto
    {
        public int Total { get; set; }
        public int Rebuilt { get; set; }
        public int Pending { get; set; }
        public int Failed { get; set; }
        public DateTime SentAt { get; set; }
        public List<string> PendingInvoiceNumbers { get; set; } = new List<string>();
        public List<string> FailedInvoiceNumbers { get; set; } = new List<string>();
    }
}
