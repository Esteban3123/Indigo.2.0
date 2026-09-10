'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base

Public Class ContractExternalClientsRepository
    Inherits GenericRepository(Of ContractExternalClients)
    Implements IContractExternalClientsRepository, Inject

    ''' <summary>
    ''' Contexto de Package
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Package
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetContractExternalClients(code As String, Optional tracking As Boolean = True) As ContractExternalClients Implements IContractExternalClientsRepository.GetContractExternalClients
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim ContractExternalClients As ContractExternalClients = Nothing

        If tracking Then
            ContractExternalClients = (From e In _context.ContractExternalClients.Include("ContractExternalClientsDetail").Include("ContractExternalClientsDefinitionRate")
                                       Where e.Code = code
                                       Select e).FirstOrDefault()
        Else
            ContractExternalClients = (From e In _context.ContractExternalClients.AsNoTracking.Include("ContractExternalClientsDetail").AsNoTracking.Include("ContractExternalClientsDefinitionRate").AsNoTracking
                                       Where e.Code = code
                                       Select e).FirstOrDefault()
        End If

        If ContractExternalClients IsNot Nothing Then
            ContractExternalClients.CustomerDescription = (From x In _context.Customer.AsNoTracking Where x.Id = ContractExternalClients.CustomerId Select String.Concat(x.Nit, " - ", x.Name)).FirstOrDefault()
            ContractExternalClients.ProductRateDescription = (From x In _context.ProductRate.AsNoTracking Where x.Id = ContractExternalClients.ProductRateId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            ContractExternalClients.WareHouseCodeName = (From x In _context.Warehouse.AsNoTracking Where x.Id = ContractExternalClients.WarehouseId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()

            If ContractExternalClients.ContractExternalClientsDetail IsNot Nothing AndAlso ContractExternalClients.ContractExternalClientsDetail.Count > 0 Then
                For Each itemDetail In ContractExternalClients.ContractExternalClientsDetail
                    Select Case itemDetail.Type
                        Case 1
                            itemDetail.TypeName = "Medicamento"
                        Case 2
                            itemDetail.TypeName = "Insumo"
                        Case 3
                            itemDetail.TypeName = "Producto"
                        Case 4
                            itemDetail.TypeName = "Servicio"
                    End Select

                    If itemDetail.AtcId IsNot Nothing Then
                        itemDetail.SourceCodeName = (From x In _context.ATC.AsNoTracking Where x.Id = itemDetail.AtcId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                    ElseIf itemDetail.ProductId IsNot Nothing Then
                        itemDetail.SourceCodeName = (From x In _context.InventoryProduct.AsNoTracking Where x.Id = itemDetail.ProductId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                    ElseIf itemDetail.CUPSEntityId IsNot Nothing Then
                        itemDetail.SourceCodeName = (From x In _context.CUPSEntity.AsNoTracking Where x.Id = itemDetail.CUPSEntityId Select String.Concat(x.Code, " - ", x.Description)).FirstOrDefault()
                    Else
                        itemDetail.SourceCodeName = (From x In _context.InventorySupplie.AsNoTracking Where x.Id = itemDetail.SupplieId Select String.Concat(x.Code, " - ", x.SupplieName)).FirstOrDefault()
                    End If

                    itemDetail.SuppliedByName = If(itemDetail.SuppliedBy = 1, "Cliente", "Cliente y Central Mezclas")
                Next
            End If

            If ContractExternalClients.ContractExternalClientsDefinitionRate IsNot Nothing AndAlso ContractExternalClients.ContractExternalClientsDefinitionRate.Count > 0 Then
                For Each item In ContractExternalClients.ContractExternalClientsDefinitionRate
                    item.DefinitionRateDescription = (From r In _context.DefinitionRate.AsNoTracking Where r.Id = item.DefinitionRateId Select String.Concat(r.Code, " - ", r.Name)).FirstOrDefault()
                Next
            End If

            Return ContractExternalClients
        Else
            Return New ContractExternalClients()
        End If
    End Function

    Public Function GetContractExternalClientsById(id As String, Optional tracking As Boolean = True) As ContractExternalClients Implements IContractExternalClientsRepository.GetContractExternalClientsById
        Dim res = (From bg In _context.ContractExternalClients Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.ContractExternalClients.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New ContractExternalClients
        End If
    End Function

    Public Function SP_ImportExceptionsRawMaterial(XmlObject As String) As List(Of SP_ImportExceptionsRawMaterial_Result) Implements IContractExternalClientsRepository.SP_ImportExceptionsRawMaterial
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportExceptionsRawMaterial(XmlObject).ToList
    End Function

    ''' <summary>
    ''' metodo para retornar la una definicion de tarifa por id del contrato centro de atencion externo y fecha
    ''' </summary>
    ''' <param name="ContractExternalClientsId"></param>
    ''' <param name="serviceDate"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetContractExternalClientsDefinitionRateByContractEIdServiceDate(ContractExternalClientsId As Integer, serviceDate As Date, Optional tracking As Boolean = True) As ContractExternalClientsDefinitionRate Implements IContractExternalClientsRepository.GetContractExternalClientsDefinitionRateByContractEIdServiceDate
        Dim ContractExternalClientsDefinitionRate As ContractExternalClientsDefinitionRate
        If tracking Then
            Dim res = (From cgdr In _context.ContractExternalClientsDefinitionRate Where cgdr.ContractExternalClientsId = ContractExternalClientsId Select cgdr).ToList()
            ContractExternalClientsDefinitionRate = res.Find(Function(x) serviceDate.Date >= x.InitialDate And serviceDate.Date <= x.EndDate)
            If ContractExternalClientsDefinitionRate Is Nothing Then
                Return New ContractExternalClientsDefinitionRate
            End If
        Else
            Dim res = (From cgdr In _context.ContractExternalClientsDefinitionRate.AsNoTracking() Where cgdr.ContractExternalClientsId = ContractExternalClientsId Select cgdr).ToList()
            ContractExternalClientsDefinitionRate = res.Find(Function(x) serviceDate.Date >= x.InitialDate And serviceDate.Date <= x.EndDate)
            If ContractExternalClientsDefinitionRate Is Nothing Then
                Return New ContractExternalClientsDefinitionRate
            End If
        End If

        Return ContractExternalClientsDefinitionRate
    End Function
End Class