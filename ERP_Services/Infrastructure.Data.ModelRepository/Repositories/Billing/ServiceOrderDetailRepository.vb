'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
#End Region

Public Class ServiceOrderDetailRepository
    Inherits GenericRepository(Of ServiceOrderDetail)
    Implements IServiceOrderDetailRepository

#Region "Fields"
    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' lista los detalles de la orden que podran ser incluidos en otro
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function ListServicesOrderDetailByAdminssionNumber(admissionNumber As String) As List(Of ServiceOrderDetail) Implements IServiceOrderDetailRepository.ListServicesOrderDetailByAdminssionNumber
        Dim recordType As Integer = 1
        Dim res = (From sod In _context.ServiceOrderDetail
                   Join so In _context.ServiceOrder On sod.ServiceOrderId Equals so.Id
                   Where so.Status <> 3 And sod.RecordType = recordType And so.AdmissionNumber = admissionNumber And sod.IsDelete = False Select sod).ToList()
        For Each item In res
            item.IdTmp = item.Id
            Dim careGroup = (From cg In _context.CareGroup.AsNoTracking() Where cg.Id = item.CareGroupId Select cg).FirstOrDefault()
            item.CodeNameCareGroup = careGroup.Code + " - " + careGroup.Name
            If item.IPSServiceId IsNot Nothing Then
                Dim ips = (From ipss In _context.IPSService.AsNoTracking() Where ipss.Id = item.IPSServiceId Select ipss).FirstOrDefault()
                item.CodeNameIpsService = ips.Code + " - " + ips.Name
            End If
            If item.CUPSEntityId IsNot Nothing Then
                Dim cups = (From cupss In _context.CUPSEntity.AsNoTracking() Where cupss.Id = item.CUPSEntityId Select cupss).FirstOrDefault()
                item.CodeNameCups = cups.Code + " - " + cups.Description
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' Obtiene el detalle de una orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailById(Id As Integer, Optional tracking As Boolean = True) As ServiceOrderDetail Implements IServiceOrderDetailRepository.GetServiceOrderDetailById
        Dim query
        If tracking Then
            query = (From s In _context.ServiceOrderDetail.Include("ServiceOrderDetail1") Where s.Id = Id Select s).FirstOrDefault()
        Else
            query = (From s In _context.ServiceOrderDetail.AsNoTracking.Include("RateManual").AsNoTracking Where s.Id = Id Select s).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    Public Function GetServiceOrderDetailByIdWithAggregates(Id As Integer, Optional tracking As Boolean = True) As ServiceOrderDetail Implements IServiceOrderDetailRepository.GetServiceOrderDetailByIdWithAggregates
        Dim query
        If tracking Then
            query = (From s In _context.ServiceOrderDetail.Include("ServiceOrderDetail1").Include("ServiceOrderDetailSurgical").Include("ServiceOrderDetailSurgical.BillingConcept") _
                     .Include("FunctionalUnit") Where s.Id = Id Select s).FirstOrDefault()
        Else
            query = (From s In _context.ServiceOrderDetail.AsNoTracking.Include("RateManual").AsNoTracking.Include("ServiceOrderDetailSurgical").AsNoTracking.Include("ServiceOrderDetailSurgical.BillingConcept").AsNoTracking Where s.Id = Id Select s).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    Public Function GetServiceOrderDetailByIdIncludes(id As Integer, includes() As String) As ServiceOrderDetail Implements IServiceOrderDetailRepository.GetServiceOrderDetailByIdIncludes
        Dim query As IQueryable(Of ServiceOrderDetail) = _context.ServiceOrderDetail.Where(Function(sod) sod.Id = id).AsQueryable()
        If includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(include)
                                          query = query.Include(include)
                                      End Sub)
        End If
        Return query.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Gets the service order detail by identifier aggregates.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailByIdAggregates(Id As Integer, Optional tracking As Boolean = True) As ServiceOrderDetail Implements IServiceOrderDetailRepository.GetServiceOrderDetailByIdAggregates
        Dim query
        If tracking Then
            query = (From s In _context.ServiceOrderDetail.Include("ServiceOrderDetailDistribution") Where s.Id = Id Select s).FirstOrDefault()
        Else
            query = (From s In _context.ServiceOrderDetail.AsNoTracking.Include("ServiceOrderDetailDistribution").AsNoTracking Where s.Id = Id Select s).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lista las ordenes de servicio por ingreso y que no estén dentro del listado que se envia como parámetro
    ''' </summary>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="serviceOrderDetailIds">The service order detail ids.</param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, serviceOrderDetailIds As List(Of Integer)) As List(Of ServiceOrderDetail) Implements IServiceOrderDetailRepository.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId
        Dim query = (From sod In _context.ServiceOrderDetail
                     Join sodd In _context.ServiceOrderDetailDistribution On sodd.ServiceOrderDetailId Equals sod.Id
                     Join rcd In _context.RevenueControlDetail On sodd.RevenueControlDetailId Equals rcd.Id
                     Join rc In _context.RevenueControl On rcd.RevenueControlId Equals rc.Id
                     Where rc.AdmissionNumber.Equals(admissionNumber) AndAlso Not serviceOrderDetailIds.Contains(sod.Id) AndAlso sod.IsAnnulled = False AndAlso sod.IsDelete = False AndAlso sod.RecordType = 1
                     Select sod).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            For Each item In query
                Dim careGroup = (From cg In _context.CareGroup.AsNoTracking() Where cg.Id = item.CareGroupId Select cg).FirstOrDefault()
                item.CodeNameCareGroup = careGroup.Code + " - " + careGroup.Name
                If item.IPSServiceId IsNot Nothing Then
                    Dim ips = (From ipss In _context.IPSService.AsNoTracking() Where ipss.Id = item.IPSServiceId Select ipss).FirstOrDefault()
                    item.CodeNameIpsService = ips.Code + " - " + ips.Name
                    item.ItemCode = ips.Code
                    item.ItemDescription = ips.Name
                End If
            Next
        End If
        Return query
    End Function

    Public Sub RemoveRange(servorderDetailList As List(Of ServiceOrderDetail)) Implements IServiceOrderDetailRepository.RemoveRange
        _context.ServiceOrderDetail.RemoveRange(servorderDetailList)
    End Sub

    Public Function GetServiceOrderDetailsEqualServiceOrder(serviceOrderId As Integer, serviceOrderDetailId As Integer) As List(Of ServiceOrderDetail) Implements IServiceOrderDetailRepository.GetServiceOrderDetailsEqualServiceOrder

        Dim res = (From sod In _context.ServiceOrderDetail.Include("ServiceOrderDetailSurgical") Where sod.ServiceOrderId = serviceOrderId AndAlso sod.IsDelete = False Select sod).ToList()
        'AndAlso sod.Id <> serviceOrderDetailId
        If res.Count > 0 Then
            For Each item In res
                item.IdTmp = item.Id
                Dim careGroup = (From cg In _context.CareGroup.AsNoTracking() Where cg.Id = item.CareGroupId Select cg).FirstOrDefault()
                item.CodeNameCareGroup = careGroup.Code + " - " + careGroup.Name
                If item.IPSServiceId IsNot Nothing Then
                    Dim ips = (From ipss In _context.IPSService.AsNoTracking() Where ipss.Id = item.IPSServiceId Select ipss).FirstOrDefault()
                    item.CodeNameIpsService = ips.Code + " - " + ips.Name
                End If
                If item.CUPSEntityId IsNot Nothing Then
                    Dim cups = (From cupss In _context.CUPSEntity.AsNoTracking() Where cupss.Id = item.CUPSEntityId Select cupss).FirstOrDefault()
                    item.CodeNameCups = cups.Code + " - " + cups.Description
                End If
                Dim functionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = item.PerformsFunctionalUnitId Select fu).FirstOrDefault()
                item.CodeNameFunctionalUnit = functionalUnit.Code + " - " + functionalUnit.Name
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.CostCenterId Select cc).FirstOrDefault()
                item.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
                If item.ThirdPartyId IsNot Nothing Then
                    item.NitNameThirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = item.ThirdPartyId Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()
                End If
                If item.ProductId IsNot Nothing Then
                    item.CodeNameProduct = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                End If
                If item.HealthAdministratorId IsNot Nothing Then
                    item.CodeNameHealthAdministrator = (From ha In _context.HealthAdministrator.AsNoTracking() Where ha.Id = item.HealthAdministratorId Select String.Concat(ha.Code, " - ", ha.Name)).FirstOrDefault()
                End If
                For Each itemSurgical In item.ServiceOrderDetailSurgical
                    Dim ips = (From ipss In _context.IPSService.AsNoTracking() Where ipss.Id = itemSurgical.IPSServiceId Select ipss).FirstOrDefault()
                    itemSurgical.CodeNameIpsService = ips.Code + " - " + ips.Name
                    Select Case ips.ServiceClass
                        Case 1
                            itemSurgical.ClassServiceIps = "Ninguno"
                        Case 2
                            itemSurgical.ClassServiceIps = "Cirujano"
                        Case 3
                            itemSurgical.ClassServiceIps = "Anestesiólogo"
                        Case 4
                            itemSurgical.ClassServiceIps = "Ayudante"
                        Case 5
                            itemSurgical.ClassServiceIps = "Derecho Sala"
                        Case 6
                            itemSurgical.ClassServiceIps = "Materiales Sutura"
                        Case 7
                            itemSurgical.ClassServiceIps = "Instrumentación Quirúrgica"
                    End Select
                Next
            Next
        End If
        Return res
    End Function

    Public Function SP_GetServiceValue(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As SP_GetServiceValue_Result Implements IServiceOrderDetailRepository.SP_GetServiceValue
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetServiceValue(AdmissionNumber, CenterAttentionCode, CupsEntityId, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId, ContractDescriptionId).FirstOrDefault()
    End Function

    Public Function SP_ValidateServiceOrderDetail(serviceOrderDetailXml As String, userCode As String) As SP_ValidateServiceOrderDetail_Result Implements IServiceOrderDetailRepository.SP_ValidateServiceOrderDetail
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ValidateServiceOrderDetail(serviceOrderDetailXml, userCode).FirstOrDefault()
    End Function

#End Region

End Class
