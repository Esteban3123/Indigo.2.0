'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Threading.Tasks
Imports Domain.Billing.POCO
Imports Domain.Crystal

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class InvoiceRepository
    Inherits GenericRepository(Of Invoice)
    Implements IInvoiceRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork
    Private ReadOnly _patientRepository As IPatientRepository

    Public Sub New(ByVal context As IGlobalModelUnitOfWork, ByVal patientRepository As IPatientRepository)
        MyBase.New(context)
        _context = context
        _patientRepository = patientRepository
    End Sub

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    Public Function GetInvoiceById(Id As Integer, Optional tracking As Boolean = True) As Invoice Implements IInvoiceRepository.GetInvoiceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As Invoice
        If tracking Then
            query = (From i In _context.Invoice Where i.Id = Id Select i).FirstOrDefault()
        Else
            query = (From i In _context.Invoice.AsNoTracking() Where i.Id = Id Select i).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New Invoice()
        End If
    End Function

    Public Function GetInvoiceDetailByServiceOrderDetailId(sodId As Integer) As InvoiceDetail Implements IInvoiceRepository.GetInvoiceDetailByServiceOrderDetailId
        Return _context.InvoiceDetail.AsNoTracking().Where(Function(o) o.ServiceOrderDetailId = sodId).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene una factura por numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceByInvoiceNumber(invoiceNumber As String, Optional tracking As Boolean = True) As Invoice Implements IInvoiceRepository.GetInvoiceByInvoiceNumber
        Dim res As Invoice = Nothing
        If tracking Then
            res = (From so In _context.Invoice Where so.InvoiceNumber = invoiceNumber Select so).FirstOrDefault()
        Else
            res = (From so In _context.Invoice.AsNoTracking() Where so.InvoiceNumber = invoiceNumber Select so).FirstOrDefault()
        End If
        If res IsNot Nothing Then
            Return res
        End If
        Return New Invoice
    End Function

    ''' <summary>
    ''' Obtiene una factura por el id del folio
    ''' </summary>
    Public Function GetInvoiceByRevenueControlDetailId(RevenueControlDetailId As Integer) As Invoice Implements IInvoiceRepository.GetInvoiceByRevenueControlDetailId
        If RevenueControlDetailId = 0 Then
            Throw New ArgumentNullException("RevenueControlDetailId")
        End If
        Dim query = (From i In _context.Invoice.Include("InvoiceDetail").Include("InvoicePortfolioAdvance") Where i.RevenueControlDetailId = RevenueControlDetailId Select i).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New Invoice()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una factura por el id del folio
    ''' </summary>
    Public Function GetInvoiceAndRevenueControlByRevenueControlDetailIdNotTracking(RevenueControlDetailId As Integer) As Invoice Implements IInvoiceRepository.GetInvoiceAndRevenueControlByRevenueControlDetailIdNotTracking
        If RevenueControlDetailId = 0 Then
            Throw New ArgumentNullException("RevenueControlDetailId")
        End If
        Dim query = (From i In _context.Invoice.AsNoTracking().Include("RevenueControlDetail").AsNoTracking() Where i.RevenueControlDetailId = RevenueControlDetailId Select i).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New Invoice()
        End If
    End Function

    ''' <summary>
    ''' Lists the annullate invoice identifier.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer) Implements IInvoiceRepository.ListAnnullateInvoiceIdByAdmission
        Dim DOS As Integer = 2
        Return (From i In _context.Invoice Where i.AdmissionNumber.Trim() = admission.Trim() AndAlso i.Status = DOS Select i).Select(Function(o) o.Id).Distinct().ToList()
    End Function

    ''' <summary>
    ''' Obtiene información adicional de una factura
    ''' </summary>
    Public Function GetInvoiceMoreInformationByInvoiceId(invoiceId As Integer) As SP_GetInvoiceMoreInformationByInvoiceId_Result Implements IInvoiceRepository.GetInvoiceMoreInformationByInvoiceId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetInvoiceMoreInformationByInvoiceId(invoiceId).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la fecha final del período de la factura de capitación anterior.
    ''' </summary>
    Public Function GetPreviousCapitationPeriodEndDateByInvoiceId(invoiceId As Integer) As Nullable(Of Date) Implements IInvoiceRepository.GetPreviousCapitationPeriodEndDateByInvoiceId
        If invoiceId = 0 Then
            Return Nothing
        End If

        Dim previousInvoiceEntityCapitatedId = (From current In _context.InvoiceEntityCapitated.AsNoTracking()
                                                Where current.InvoiceId = invoiceId AndAlso
                                                      current.InvoicePeriod = 2 AndAlso
                                                      current.PreviousRIPSInvoice.HasValue
                                                Select current.PreviousRIPSInvoice).FirstOrDefault()

        If Not previousInvoiceEntityCapitatedId.HasValue Then
            Return Nothing
        End If

        Return (From previous In _context.InvoiceEntityCapitated.AsNoTracking()
                Where previous.Id = previousInvoiceEntityCapitatedId.Value
                Select previous.EndDate).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los pagos asociados a una factura
    ''' </summary>
    Public Function GetInvoiceDetailsByInvoiceId(invoiceId As Integer) As List(Of SP_GetInvoiceDetailsByInvoiceId_Result) Implements IInvoiceRepository.GetInvoiceDetailsByInvoiceId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetInvoiceDetailsByInvoiceId(invoiceId).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los pagos asociados a una factura
    ''' </summary>
    Public Function GetPaymentMethodsByInvoiceId(invoiceId As Integer) As List(Of SP_GetPaymentMethodsByInvoiceId_Result) Implements IInvoiceRepository.GetPaymentMethodsByInvoiceId
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetPaymentMethodsByInvoiceId(invoiceId).ToList()
    End Function

    ''' <summary>
    ''' Obtiene los pagos realizados a facturas de sector salud de tipo EAPB con/sin Contrato
    ''' </summary>
    ''' <param name="invoiceId"></param>
    ''' <returns></returns>
    Public Function GetPrepaidPaymentHealth(invoiceId As Integer) As List(Of SP_GetPrepaidPaymentHealth) Implements IInvoiceRepository.GetPrepaidPaymentHealth
        Return Me.ExecuteStoredProcedure(Of SP_GetPrepaidPaymentHealth)("[Billing].[SP_GetPrepaidPaymentHealth]", {("@InvoiceId", invoiceId)})?.ToList()
    End Function


    ''' <summary>
    ''' Lista registros de servicio paginados para facturas monto fijo.
    ''' </summary>
    Public Async Function ListFixedAmountServiceRecords(query As FixedAmountServiceRecordQuery) As Task(Of PagedResult(Of FixedAmountServiceRecordDto)) Implements IInvoiceRepository.ListFixedAmountServiceRecords
        Dim pageSize = Math.Max(1, If(query IsNot Nothing AndAlso query.PageSize > 0, query.PageSize, 200))
        Dim pageNumber = Math.Max(1, If(query IsNot Nothing AndAlso query.PageNumber > 0, query.PageNumber, 1))
        Dim source = BuildFixedAmountServiceRecordsQuery(query)
        Dim total = Await source.CountAsync()
        Dim rows = Await source.Include("HealthAdministrator").
            OrderBy(Function(i) i.InvoiceDate).
            ThenBy(Function(i) i.InvoiceNumber).
            Skip((pageNumber - 1) * pageSize).
            Take(pageSize).
            ToListAsync()

        Dim patientNamesByCode = GetFixedAmountPatientNames(rows.Select(Function(i) i.PatientCode).ToList())

        Return New PagedResult(Of FixedAmountServiceRecordDto) With {
            .Items = rows.Select(Function(i) MapFixedAmountServiceRecord(i, patientNamesByCode)).ToList(),
            .TotalCount = total,
            .PageNumber = pageNumber,
            .PageSize = pageSize
        }
    End Function

    ''' <summary>
    ''' Lista registros de servicio aptos para reconstruccion de JSON RIPS.
    ''' </summary>
    Public Async Function ListFixedAmountRebuildCandidates(query As FixedAmountServiceRecordQuery) As Task(Of List(Of FixedAmountRebuildCandidateDto)) Implements IInvoiceRepository.ListFixedAmountRebuildCandidates
        Dim rows = Await BuildFixedAmountServiceRecordsQuery(query).
            OrderBy(Function(i) i.InvoiceDate).
            ThenBy(Function(i) i.InvoiceNumber).
            ToListAsync()
        Dim latestStatusByInvoice = Await GetLatestRipsStatusByInvoice(rows.Select(Function(i) i.Id).ToList())
        Dim result = New List(Of FixedAmountRebuildCandidateDto)

        For Each invoice In rows
            Dim statusRow As FixedAmountRipsStatusRow = Nothing
            latestStatusByInvoice.TryGetValue(invoice.Id, statusRow)

            result.Add(New FixedAmountRebuildCandidateDto With {
                .InvoiceNumber = invoice.InvoiceNumber,
                .PatientCode = invoice.PatientCode,
                .CosmoDBId = If(statusRow Is Nothing, Nothing, statusRow.CosmoDBId),
                .SendDate = If(statusRow Is Nothing, Nothing, statusRow.SendDate),
                .StatusRIPS = If(statusRow Is Nothing, Nothing, statusRow.StatusRIPS)
            })
        Next

        Return result
    End Function

    ''' <summary>
    ''' Consulta el avance de reconstruccion usando sendDate como frontera.
    ''' </summary>
    Public Async Function GetFixedAmountRebuildStatus(query As FixedAmountServiceRecordQuery) As Task(Of FixedAmountRebuildStatusDto) Implements IInvoiceRepository.GetFixedAmountRebuildStatus
        Dim sentAt = If(query?.SentAt, DateTime.MinValue)
        Dim candidates = Await ListFixedAmountRebuildCandidates(query)
        Dim rebuilt = candidates.Where(Function(x) x.SendDate.HasValue AndAlso x.SendDate.Value >= sentAt).ToList()
        Dim pending = candidates.Where(Function(x) Not x.SendDate.HasValue OrElse x.SendDate.Value < sentAt).ToList()

        Return New FixedAmountRebuildStatusDto With {
            .Total = candidates.Count,
            .Rebuilt = rebuilt.Count,
            .Pending = pending.Count,
            .Failed = 0,
            .SentAt = sentAt,
            .PendingInvoiceNumbers = pending.Select(Function(x) x.InvoiceNumber).ToList()
        }
    End Function

    ''' <summary>
    ''' Obtiene la fecha actual del motor de base de datos.
    ''' </summary>
    Public Function GetDatabaseDate() As DateTime Implements IInvoiceRepository.GetDatabaseDate
        Return DirectCast(_context, System.Data.Entity.DbContext).Database.SqlQuery(Of DateTime)("SELECT GETDATE()").Single()
    End Function

    ''' <summary>
    ''' Construye la consulta base de registros de servicio validos.
    ''' </summary>
    Private Function BuildFixedAmountServiceRecordsQuery(query As FixedAmountServiceRecordQuery) As IQueryable(Of Invoice)
        If query Is Nothing Then Throw New ArgumentNullException(NameOf(query))
        If query.CareGroupId <= 0 Then Throw New ArgumentException("Grupo de atencion requerido")
        If query.InvoiceCategoryId <= 0 Then Throw New ArgumentException("Categoria de factura requerida")
        If Not query.InitialDate.HasValue OrElse Not query.EndDate.HasValue Then Throw New ArgumentException("Rango de fechas requerido")

        Dim initialDate = query.InitialDate.Value.Date
        Dim endDateExclusive = query.EndDate.Value.Date.AddDays(1)
        Dim result = _context.Invoice.AsNoTracking().
            Where(Function(i) i.DocumentType = 5 AndAlso
                              i.Status = 1 AndAlso
                              i.CareGroupId.HasValue AndAlso i.CareGroupId.Value = query.CareGroupId AndAlso
                              i.InvoiceCategoryId.HasValue AndAlso i.InvoiceCategoryId.Value = query.InvoiceCategoryId AndAlso
                              i.InvoiceDate >= initialDate AndAlso
                              i.InvoiceDate < endDateExclusive)

        If query.PatientCodes IsNot Nothing AndAlso query.PatientCodes.Any() Then
            Dim patientCodes = query.PatientCodes.Where(Function(x) Not String.IsNullOrWhiteSpace(x)).Select(Function(x) x.Trim()).Distinct().ToList()
            result = result.Where(Function(i) patientCodes.Contains(i.PatientCode))
        End If

        Return result
    End Function

    ''' <summary>
    ''' Convierte una factura de servicio en DTO para la grilla.
    ''' </summary>
    Private Function MapFixedAmountServiceRecord(invoice As Invoice, patientNamesByCode As Dictionary(Of String, String)) As FixedAmountServiceRecordDto
        Dim administrator = String.Empty
        If invoice.HealthAdministrator IsNot Nothing Then
            administrator = String.Concat(invoice.HealthAdministrator.Code, " - ", invoice.HealthAdministrator.Name)
        End If

        Dim patientCode = If(invoice.PatientCode, String.Empty).Trim()
        Dim patientName = String.Empty
        If patientNamesByCode IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(patientCode) Then
            patientNamesByCode.TryGetValue(patientCode, patientName)
        End If

        Dim patientCodeName = If(String.IsNullOrWhiteSpace(patientName), patientCode, String.Concat(patientCode, " - ", patientName.Trim()))

        Return New FixedAmountServiceRecordDto With {
            .InvoiceNumber = invoice.InvoiceNumber,
            .Value = invoice.ThirdPartySalesValue,
            .PatientCode = patientCode,
            .PatientName = patientName,
            .PatientCodeName = patientCodeName,
            .AdmissionNumber = invoice.AdmissionNumber,
            .InvoiceDate = invoice.InvoiceDate,
            .HealthAdministrator = administrator
        }
    End Function

    ''' <summary>
    ''' Obtiene nombres de pacientes para mostrar documento y nombre.
    ''' </summary>
    Private Function GetFixedAmountPatientNames(patientCodes As List(Of String)) As Dictionary(Of String, String)
        Dim keys = If(patientCodes, New List(Of String)()).
            Where(Function(x) Not String.IsNullOrWhiteSpace(x)).
            Select(Function(x) x.Trim()).
            Distinct().
            ToList()

        If Not keys.Any() OrElse _patientRepository Is Nothing Then
            Return New Dictionary(Of String, String)()
        End If

        Dim names = _patientRepository.GetByFilter(Function(p) keys.Contains(p.IPCODPACI), False).
            Where(Function(p) Not String.IsNullOrWhiteSpace(p.IPCODPACI) AndAlso Not String.IsNullOrWhiteSpace(p.IPNOMCOMP)).
            GroupBy(Function(p) p.IPCODPACI.Trim()).
            ToDictionary(Function(g) g.Key, Function(g) g.First().IPNOMCOMP.Trim())

        Return names
    End Function

    ''' <summary>
    ''' Obtiene el ultimo estado RIPS registrado por factura.
    ''' </summary>
    Private Async Function GetLatestRipsStatusByInvoice(invoiceIds As List(Of Integer)) As Task(Of Dictionary(Of Integer, FixedAmountRipsStatusRow))
        Dim keys = If(invoiceIds, New List(Of Integer)()).Distinct().ToList()
        If Not keys.Any() Then
            Return New Dictionary(Of Integer, FixedAmountRipsStatusRow)()
        End If

        Dim rows = Await (From ep In _context.ElectronicsProperties.AsNoTracking()
                          Join er In _context.ElectronicsRIPS.AsNoTracking() On ep.Id Equals er.ElectronicsPropertiesId
                          Where ep.EntityName = "Invoice" AndAlso keys.Contains(ep.EntityId)
                          Select New FixedAmountRipsStatusRow With {
                              .InvoiceId = ep.EntityId,
                              .CosmoDBId = er.CosmoDBId,
                              .SendDate = er.sendDate,
                              .StatusRIPS = ep.StatusRIPS
                          }).ToListAsync()

        Return rows.
            GroupBy(Function(x) x.InvoiceId).
            ToDictionary(Function(g) g.Key, Function(g) g.OrderByDescending(Function(x) x.SendDate).First())
    End Function

    ''' <summary>
    ''' Estado RIPS reducido usado por las consultas de reconstruccion.
    ''' </summary>
    Private Class FixedAmountRipsStatusRow
        Public Property InvoiceId As Integer
        Public Property CosmoDBId As String
        Public Property SendDate As Date?
        Public Property StatusRIPS As Byte?
    End Class

End Class
