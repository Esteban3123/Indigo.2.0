'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-12-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ContractExternalClientsDetailRepository
    Inherits GenericRepository(Of ContractExternalClientsDetail)
    Implements IContractExternalClientsDetailRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetRawMaterialExceptionsByCampaignDetailId(campaignDetailId As Integer) As List(Of ContractExternalClientsDetail) Implements IContractExternalClientsDetailRepository.GetRawMaterialExceptionsByCampaignDetailId
        Dim entityName = GetType(RequestUnitDoseExternalCareCenterMaquila).Name
        Dim query = (From ce In _context.RequestMixingStationDetail
                     Join rud In _context.RequestUnitDoseExternalCareCenterMaquila On ce.EntityId Equals rud.Id
                     Join ru In _context.RequestUnitDoseExternalCareCenter On rud.RequestUnitDoseExternalCareCenterId Equals ru.Id
                     Join ec In _context.ExternalCareCenter On ru.ExternalCareCenterId Equals ec.Id
                     Join cex In _context.ContractExternalClientsDetail On ec.ContractExternalClientsId Equals cex.ContractExternalClientsId
                     Join co In _context.ContractExternalClients On cex.ContractExternalClientsId Equals co.Id
                     Where ce.CampaignDetailId = campaignDetailId AndAlso ce.EntityName = entityName AndAlso co.ManagesMaquila = True
                     Select cex).AsNoTracking().ToList()

        Return query
    End Function

    Public Function GetRawMaterialExceptionByCampaignDetailIdAndItemId(campaignDetailId As Integer, atcId As Integer?, supplyId As Integer?, productId As Integer?) As ContractExternalClientsDetail Implements IContractExternalClientsDetailRepository.GetRawMaterialExceptionByCampaignDetailIdAndItemId
        Dim entityName = GetType(RequestUnitDoseExternalCareCenterMaquila).Name
        Dim query = (From ce In _context.RequestMixingStationDetail
                     Join rud In _context.RequestUnitDoseExternalCareCenterMaquila On ce.EntityId Equals rud.Id
                     Join ru In _context.RequestUnitDoseExternalCareCenter On rud.RequestUnitDoseExternalCareCenterId Equals ru.Id
                     Join ec In _context.ExternalCareCenter On ru.ExternalCareCenterId Equals ec.Id
                     Join cex In _context.ContractExternalClientsDetail On ec.ContractExternalClientsId Equals cex.ContractExternalClientsId
                     Join co In _context.ContractExternalClients On cex.ContractExternalClientsId Equals co.Id
                     Join cd In _context.CampaignDetail On cd.Id Equals ce.CampaignDetailId
                     Where ce.CampaignDetailId = campaignDetailId AndAlso ce.EntityName = entityName AndAlso co.ManagesMaquila = True AndAlso cex.AtcId = atcId AndAlso cex.SupplieId = supplyId AndAlso cex.ProductId = productId
                     Select cex).AsNoTracking().FirstOrDefault()
        Return query
    End Function

    Public Function GetContractExternalClientByCampaignDetailId(campaignDetailId As Integer) As ContractExternalClients Implements IContractExternalClientsDetailRepository.GetContractExternalClientByCampaignDetailId
        Dim entityName = GetType(RequestUnitDoseExternalCareCenterMaquila).Name
        Dim query = (From ce In _context.RequestMixingStationDetail
                     Join rud In _context.RequestUnitDoseExternalCareCenterMaquila On ce.EntityId Equals rud.Id
                     Join ru In _context.RequestUnitDoseExternalCareCenter On rud.RequestUnitDoseExternalCareCenterId Equals ru.Id
                     Join ec In _context.ExternalCareCenter On ru.ExternalCareCenterId Equals ec.Id
                     Join co In _context.ContractExternalClients On ec.ContractExternalClientsId Equals co.Id
                     Where ce.CampaignDetailId = campaignDetailId AndAlso ce.EntityName = entityName AndAlso co.ManagesMaquila = True
                     Select co).AsNoTracking().FirstOrDefault()
        Return query
    End Function

End Class

