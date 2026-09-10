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

Public Class ServiceOrderRepository
    Inherits GenericRepository(Of ServiceOrder)
    Implements IServiceOrderRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    Public Function GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As InvoiceDetail Implements IServiceOrderRepository.GetInvoiceDetailByServiceOrderDetailId
        Return (From ind In _context.InvoiceDetail.AsNoTracking() Where ind.ServiceOrderDetailId = serviceOrderDetailId Select ind).FirstOrDefault()
    End Function
    ''' <summary>
    ''' Obtener una orden de servicio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetServiceOrder(code As String) As ServiceOrder Implements IServiceOrderRepository.GetServiceOrder
        Dim res = (From so In _context.ServiceOrder Where so.Code = code Select so).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From so In _context.ServiceOrder.AsNoTracking() Where so.Code = code Select so).FirstOrDefault()
            Return res
        Else
            Return New ServiceOrder
        End If
    End Function

    ''' <summary>
    ''' Obtener una orden de servicio por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderById(id As Integer) As ServiceOrder Implements IServiceOrderRepository.GetServiceOrderById
        Dim res = (From so In _context.ServiceOrder Where so.Id = id Select so).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From so In _context.ServiceOrder.AsNoTracking() Where so.Id = id Select so).FirstOrDefault()
            Return res
        Else
            Return New ServiceOrder
        End If
    End Function

    ''' <summary>
    ''' Obtiene el detalle de una orden de servicio por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailById(Id As Integer) As ServiceOrderDetail Implements IServiceOrderRepository.GetServiceOrderDetailById
        Dim res = (From so In _context.ServiceOrderDetail.Include("ServiceOrderDetail1") Where so.Id = Id Select so).FirstOrDefault()
        If res IsNot Nothing Then

            For Each sodd In res.ServiceOrderDetail1
                Dim item = IIf(sodd.RecordType = 1, (From i In _context.IPSService Where sodd.IPSServiceId = i.Id Select New With { _
                                                .Code = i.Code, _
                                                .Description = i.Name
                                            }).FirstOrDefault(), _
                                         (From i In _context.InventoryProduct Where sodd.ProductId = i.Id Select New With { _
                                                .Code = i.Code, _
                                                .Description = i.Name
                                            }).FirstOrDefault())
                sodd.ItemCode = item.Code
                sodd.ItemDescription = item.Description
            Next

            Return res
        Else
            Return New ServiceOrderDetail
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la orden de servicios
    ''' </summary>
    ''' <param name="ServiceOrderId"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailByServiceOrderId(ServiceOrderId As Integer) As List(Of ServiceOrderDetail) Implements IServiceOrderRepository.GetServiceOrderDetailByServiceOrderId
        Dim res = (From sod In _context.ServiceOrderDetail.Include("ServiceOrderDetailSurgical") Where sod.ServiceOrderId = ServiceOrderId And sod.IsDelete = False Select sod).ToList()
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
                If item.RateManualId IsNot Nothing Then
                    item.LiquidateAllMIVIE = (From x In _context.RateManual Where x.Id = item.RateManualId Select x.LiquidateAllMIVIE)?.FirstOrDefault
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

    ''' <summary>
    ''' Obtiene una orden de servicio y sus detalles por Id
    ''' </summary>
    ''' <param name="id">Id de la orden de servicio</param>
    ''' <returns>Orden de servicio</returns>
    Public Function GetServiceOrderByIdWithDetails(id As Integer) As ServiceOrder Implements IServiceOrderRepository.GetServiceOrderByIdWithDetails
        Dim res = (From so In _context.ServiceOrder.Include("ServiceOrderDetail") Where so.Id = id Select so).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New ServiceOrder
        End If
    End Function

    ''' <summary>
    ''' obtiene una orden de servicio por el codigo de la entidad que lo creo
    ''' </summary>
    ''' <param name="entityCode"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderByEntityCode(entityCode As String) As ServiceOrder Implements IServiceOrderRepository.GetServiceOrderByEntityCode
        Return (From so In _context.ServiceOrder.Include("ServiceOrderDetail") Where so.EntityCode = entityCode Select so).FirstOrDefault()
    End Function

    Public Function GetServiceOrderByAdmissionCode(admissionCode As String) As ServiceOrder Implements IServiceOrderRepository.GetServiceOrderByAdmissionCode
        Return (From so In _context.ServiceOrder Where so.AdmissionNumber.Equals(admissionCode) Select so).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Metodo para crear una orden de servicio a través de un SP
    ''' </summary>
    ''' <param name="ServiceOrderXml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateServiceOrderSP(ServiceOrderXml As String, UserCode As String) As Core.Objects.ObjectResult(Of SP_GenerateServiceOrder_Result) Implements IServiceOrderRepository.GenerateServiceOrderSP
        Return _context.SP_GenerateServiceOrder(ServiceOrderXml, UserCode)
    End Function

    Public Function GetServiceOrderGeneratedByDispensing(code As String) As Integer Implements IServiceOrderRepository.GetServiceOrderGeneratedByDispensing
        Dim entityName As String = "PharmaceuticalDispensing"
        Return (From so In _context.ServiceOrder.AsNoTracking().Include("ServiceOrderDetail") Where so.EntityName = entityName AndAlso so.EntityCode = code Select so).ToList().Count
    End Function

    Public Function GetServiceOrderByEntityId(EntityId As Integer) As ServiceOrder Implements IServiceOrderRepository.GetServiceOrderByEntityId
        Return (From e In _context.ServiceOrder.AsNoTracking() Where e.EntityId = EntityId Select e).FirstOrDefault()
    End Function

    Public Function SP_GenerateDocuments(xmlData As String, codeUser As String) As SP_GenerateDocuments_Result Implements IServiceOrderRepository.SP_GenerateDocuments
        Return _context.SP_GenerateDocuments(xmlData, codeUser).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Método para listar medicamentos procesados por la central de mezclas para generación de la orden de servicios.
    ''' </summary>
    ''' <param name="admissionNumber">Número de admisión</param>
    ''' <param name="isChild">Obtiene los registros hijos</param>
    ''' <returns>Listado de medicamentos</returns>
    Public Function GetProcessedMedicationItemsForBillingList(admissionNumber As String, isChild As Boolean) As List(Of SP_GetProcessedMedicationItemsForBilling_Result) Implements IServiceOrderRepository.GetProcessedMedicationItemsForBillingList
        Return _context.SP_GetProcessedMedicationItemsForBilling(admissionNumber, isChild).ToList()
    End Function
End Class


