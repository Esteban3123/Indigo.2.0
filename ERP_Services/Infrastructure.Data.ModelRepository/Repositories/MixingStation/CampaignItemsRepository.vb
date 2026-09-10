'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Duván Mejía Cortes
' Created          : 22/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class CampaignItemsRepository
    Inherits GenericRepository(Of CampaignDetailItems)
    Implements ICampaignItemsRepository, Inject

    ''' <summary>
    ''' Contexto de Configuración de Central de Mezclas
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de Configuración de Central de Mezclas
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lisar los Medicamentos en Estado Picking 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetProductsItems(campaignDetailId As Integer) As List(Of CampaignDetailItems) Implements ICampaignItemsRepository.GetProductsItems
        Dim items = (From a In _context.CampaignDetailItems.Include("InventoryProduct").Include("ATC").Include("InventorySupplie") Where a.CampaignDetailId = campaignDetailId Select a).ToList()

        If items.Any() Then
            For Each item In items
                item.GroupName = IIf(item.ItemType = 1, "Principal", IIf(item.ItemType = 2, "Canasta", IIf(item.ItemType = 4, "Solicitudes Manuales", "Otro")))

                If item.ProductId.HasValue Then
                    item.ProductCodeName = $"{item.InventoryProduct.Code} - {item.InventoryProduct.Name}"
                End If

                If item.SupplyId.HasValue Then
                    item.SupplyCodeName = $"{item.InventorySupplie.Code} - {item.InventorySupplie.SupplieName}"
                End If

                If item.AtcId.HasValue Then
                    item.AtcCodeName = $"{item.ATC.Code} - {item.ATC.Name}"
                End If
            Next
        End If
        Return items
    End Function

    ''' <summary>
    ''' Lisar los Medicamentos en Estado Validacion 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetProductsItemsValidation(campaignDetailId As Integer) As List(Of CampaignDetailItems) Implements ICampaignItemsRepository.GetProductsItemsValidation
        Dim res = (From a In _context.CampaignDetailItems.Include("InventoryProduct").Include("ATC").Include("InventorySupplie")
                   Where a.CampaignDetailId = campaignDetailId
                   Select a).ToList()

        If res IsNot Nothing Then
            Dim deliveredQuantities As New Dictionary(Of String, Integer)

            Dim campaignDetailValidations = (From a In _context.CampaignDetailValidation.AsNoTracking().Include("InventoryProduct").AsNoTracking()
                                             Where a.CampaignDetailId = campaignDetailId
                                             Select a).ToList()

            If campaignDetailValidations IsNot Nothing Then
                For Each validation In campaignDetailValidations
                    If deliveredQuantities.ContainsKey(validation.Key) Then
                        deliveredQuantities(validation.Key) += validation.DeliveredQuantity
                    Else
                        deliveredQuantities.Add(validation.Key, validation.DeliveredQuantity)
                    End If
                Next
            End If

            For Each s In res
                s.GroupName = IIf(s.ItemType = 1, "Principal", IIf(s.ItemType = 2, "Canasta", IIf(s.ItemType = 4, "Solicitudes Manuales", "Otro")))
                If s.AtcId IsNot Nothing Then
                    s.AtcCodeName = $"{s.ATC.Code} - {s.ATC.Name}"
                ElseIf s.SupplyId IsNot Nothing Then
                    s.SupplyCodeName = $"{s.InventorySupplie.Code} - {s.InventorySupplie.SupplieName}"
                ElseIf s.ProductId IsNot Nothing Then
                    s.ProductCodeName = $"{s.InventoryProduct.Code} - {s.InventoryProduct.Name}"
                End If

                If deliveredQuantities.ContainsKey(s.ItemByType()) Then
                    Dim deliveredQuantity = deliveredQuantities(s.ItemByType())

                    If deliveredQuantity < s.RequestQuantity Then
                        s.DeliveredQuantity = deliveredQuantity
                    Else
                        s.DeliveredQuantity = s.RequestQuantity
                    End If

                    deliveredQuantities(s.ItemByType()) -= s.DeliveredQuantity
                End If
            Next
        End If

        Return res
    End Function

End Class
