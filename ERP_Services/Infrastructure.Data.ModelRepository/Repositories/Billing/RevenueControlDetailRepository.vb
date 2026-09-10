'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Carlos Eresto Cordoba
' Created          : 2014-11-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Dynamic
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class RevenueControlDetailRepository
    Inherits GenericRepository(Of RevenueControlDetail)
    Implements IRevenueControlDetailRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene un detalle del folio por grupo de atencion y tercero
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="ThirdpartyId"></param>
    ''' <returns></returns>
    Public Function GetRevenueControlDetailByCareGroupIdThirdPartyId(RevenueControlId As Integer, CareGroupId As Integer, ThirdpartyId As Integer) As RevenueControlDetail Implements IRevenueControlDetailRepository.GetRevenueControlDetailByCareGroupIdThirdPartyId
        Dim Status As Integer = 1
        Return (From rcd In _context.RevenueControlDetail Where rcd.RevenueControlId = RevenueControlId And rcd.CareGroupId = CareGroupId And rcd.ThirdPartyId = ThirdpartyId And rcd.Status = Status Order By rcd.FolioOrder Ascending Select rcd).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el id del folio, si no existe devuelve 0
    ''' </summary>
    ''' <param name="RevenueControlId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="ThirdpartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIdByCareGroupIdThirdPartyId(RevenueControlId As Integer, CareGroupId As Integer, ThirdpartyId As Integer) As Integer Implements IRevenueControlDetailRepository.GetIdByCareGroupIdThirdPartyId
        Dim Status As Integer = 1
        Return (From rcd In _context.RevenueControlDetail.AsNoTracking() Where rcd.RevenueControlId = RevenueControlId And rcd.CareGroupId = CareGroupId And rcd.ThirdPartyId = ThirdpartyId And rcd.Status = Status Order By rcd.FolioOrder Ascending Select rcd.Id).FirstOrDefault()
    End Function

    ''' <summary>
    ''' obtiene el listado de los detalles
    ''' </summary>
    ''' <param name="RevenueControlId"></param>
    ''' <returns></returns>
    Public Function GetRevenueControlDetailByRevenueControlId(RevenueControlId As Integer) As List(Of RevenueControlDetail) Implements IRevenueControlDetailRepository.GetRevenueControlDetailByRevenueControlId
        Return (From rcd In _context.RevenueControlDetail.AsNoTracking() Where rcd.RevenueControlId = RevenueControlId Select rcd).ToList()
    End Function

    ''' <summary>
    ''' odtiene un detalle del folio por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetRevenueControlDetailById(Id As Integer) As RevenueControlDetail Implements IRevenueControlDetailRepository.GetRevenueControlDetailById
        Return (From rcd In _context.RevenueControlDetail.Include("RevenueControl").Include("ServiceOrderDetailDistribution") Where rcd.Id = Id Select rcd).FirstOrDefault()
    End Function

    Public Function GetRevenueControlDetailByIdNoAdded(Id As Integer) As RevenueControlDetail Implements IRevenueControlDetailRepository.GetRevenueControlDetailByIdNoAdded
        Return (From rcd In _context.RevenueControlDetail.AsNoTracking() Where rcd.Id = Id Select rcd).FirstOrDefault()
    End Function


    Public Function IncludeInOtherService(xmlData As String) As SP_IncludeInOtherService_Result Implements IRevenueControlDetailRepository.IncludeInOtherService
        Return _context.SP_IncludeInOtherService(xmlData).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un folio y sus agregados por su id
    ''' </summary>
    ''' <param name="Id">Id del folio</param>
    ''' <returns>Folio consultado</returns>
    Public Function GetRevenueControlDetailWithAggregatesById(Id As Integer) As RevenueControlDetail Implements IRevenueControlDetailRepository.GetRevenueControlDetailWithAggregatesById
        Dim res = (From rcd In _context.RevenueControlDetail.Include("RevenueControl").Include("ServiceOrderDetailDistribution").
                   Include("ServiceOrderDetailDistribution.ServiceOrderDetail").Include("ServiceOrderDetailDistribution.ServiceOrderDetail.ThirdParty").
                   Include("ServiceOrderDetailDistribution.ServiceOrderDetail.FunctionalUnit").Include("ServiceOrderDetailDistribution.ServiceOrderDetail.IPSService").
                   Include("ServiceOrderDetailDistribution.ServiceOrderDetail.CUPSEntity").Include("ServiceOrderDetailDistribution.ServiceOrderDetail.ServiceOrderDetailSurgical").
                   Include("ServiceOrderDetailDistribution.ServiceOrderDetail.RateManualDetail").Include("ServiceOrderDetailDistribution.ServiceOrderDetail.RateManualDetail.RateManual")
                   Where rcd.Id = Id Select rcd).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res(0)
        Else
            Return New RevenueControlDetail()
        End If
    End Function

    Public Function GetRevenueControlDetailByIdWithIncludes(id As Integer, Optional includes() As String = Nothing) As RevenueControlDetail Implements IRevenueControlDetailRepository.GetRevenueControlDetailByIdWithIncludes
        Dim query As IQueryable(Of RevenueControlDetail) = _context.RevenueControlDetail.Where(Function(rcd) rcd.Id = id).AsQueryable()
        If includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(include)
                                          query = query.Include(include)
                                      End Sub)
        End If
        Return query.FirstOrDefault()
    End Function

    Public Function UpdateRevenueControlDetailValues(id As Integer, Optional operativeUnitId As Integer? = Nothing) As SP_UpdateRevenueControlDetailValues_Result Implements IRevenueControlDetailRepository.UpdateRevenueControlDetailValues
        Return _context.SP_UpdateRevenueControlDetailValues(id, operativeUnitId).FirstOrDefault()
    End Function

    Public Function UpdateRevenueControlDetailValuesNew(id As Integer, Optional operativeUnitId As Integer? = Nothing) As SP_UpdateRevenueControlDetailValuesNew_Result Implements IRevenueControlDetailRepository.UpdateRevenueControlDetailValuesNew
        CType(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 0
        Return _context.SP_UpdateRevenueControlDetailValuesNew(id, operativeUnitId).FirstOrDefault()
    End Function

    ''' <summary>
    ''' retorna el maximo numero de folio
    ''' </summary>
    ''' <param name="RevenueControlId"></param>
    ''' <returns></returns>
    Public Function GetMaxFolioOrder(RevenueControlId As Integer) As Integer Implements IRevenueControlDetailRepository.GetMaxFolioOrder
        Dim res = (From rcd In _context.RevenueControlDetail.AsNoTracking() Where rcd.RevenueControlId = RevenueControlId Select rcd).ToList()
        If res.Count = 0 Then
            Return 0
        Else
            Return (From r In res Select r.FolioOrder).Max()
        End If
    End Function

    ''' <summary>
    ''' Lista los folios en los que se encuentra distribuida una estancias
    ''' </summary>
    ''' <param name="idStay">Id de la estancia</param>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Lista de folios</returns>
    Public Function ListStayFolios(idStay As Integer, admissionNumber As String) As List(Of Object) Implements IRevenueControlDetailRepository.ListStayFolios
        'Dim res = (From TSOD In Me._context.ServiceOrderDetail
        '                        Join TSODD In Me._context.ServiceOrderDetailDistribution On TSOD.Id Equals TSODD.ServiceOrderDetailId
        '                        Join TRVCD In Me._context.RevenueControlDetail On TRVCD.Id Equals TSODD.RevenueControlDetailId
        '                        Join TRVC In Me._context.RevenueControl On TRVC.Id Equals TRVCD.RevenueControlId
        '                        Where TSOD.HospitalStayId IsNot Nothing AndAlso TSOD.HospitalStayId.Value = idStay AndAlso TRVC.AdmissionNumber.Equals(admissionNumber)
        '                        Select New With {.IdFolio = TRVCD.Id, _
        '                                         .NumberFolio = TRVCD.FolioOrder, _
        '                                         .ValueFolio = TSODD.GrandTotalSalesPrice}).ToList()
        Dim res = _context.SP_ListStayFolios(idStay, admissionNumber).ToList()
        Dim listStays As New List(Of Object)()
        If res IsNot Nothing AndAlso res.Any() Then
            For Each r In res
                Dim obj As Object = New ExpandoObject()
                obj.IdFolio = r.IdFolio
                obj.NumberFolio = r.FolioOrder
                obj.ValueFolio = r.ValueFolio
                listStays.Add(obj)
            Next
        End If
        Return listStays
    End Function


    ''' <summary>
    ''' obtiene un folio por id del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As RevenueControlDetail Implements IRevenueControlDetailRepository.GetRevenueControlDetailByServiceOrderDetailId
        Return (From sodd In _context.ServiceOrderDetailDistribution.AsNoTracking()
                Join rcd In _context.RevenueControlDetail.AsNoTracking() On sodd.RevenueControlDetailId Equals rcd.Id
                Where sodd.ServiceOrderDetailId = serviceOrderDetailId Select rcd).FirstOrDefault()
    End Function

    Public Function GenerateJournalVoucherDetails(folioId As Integer, OperativeUnitId As Integer, reverse As Boolean, invoiceAnullateId As Integer?) As List(Of SP_GenerateJournalVoucherDetails_Result) Implements IRevenueControlDetailRepository.GenerateJournalVoucherDetails
        Return _context.SP_GenerateJournalVoucherDetails(folioId, OperativeUnitId, reverse, invoiceAnullateId).ToList()
    End Function

    Public Function SP_AnulateInvoice(OperativeUnitId As Integer, folioId As Integer, ReversalReasonId As Integer, ReversalDescription As String, UserCode As String, containerHis As String, patientCode As String, CompanyType As Byte) As SP_AnulateInvoice_Result Implements IRevenueControlDetailRepository.SP_AnulateInvoice
        Return _context.SP_AnulateInvoice(OperativeUnitId, folioId, ReversalReasonId, ReversalDescription, UserCode, containerHis, patientCode, CompanyType).FirstOrDefault()
    End Function

    ''' <summary>
    ''' obtiene el modo de impresión de contrato por el id del detalle del folio
    ''' </summary>
    ''' <param name="idRevenueControl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPrintgModeByIdRevenueControl(idRevenueControl As Integer) As Byte Implements IRevenueControlDetailRepository.GetPrintgModeByIdRevenueControl
        Dim printingMode As Byte = 2
        Dim info = (From c In _context.Contract.AsNoTracking()
                    Join cd In _context.ContractDetail.AsNoTracking() On cd.ContractId Equals c.Id
                    Join cg In _context.CareGroup On c.Id Equals cg.ContractId
                    Join rcd In _context.RevenueControlDetail On cg.Id Equals rcd.CareGroupId
                    Where rcd.Id = idRevenueControl AndAlso cd.ValidRecord = True
                    Select cd).FirstOrDefault()
        If info IsNot Nothing Then
            printingMode = info.PrintingMode
        End If
        Return printingMode
    End Function


End Class
