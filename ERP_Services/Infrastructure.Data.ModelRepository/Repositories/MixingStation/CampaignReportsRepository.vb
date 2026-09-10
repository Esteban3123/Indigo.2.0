'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2021-12-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class CampaignReportsRepository
    Inherits GenericRepository(Of CampaignReports)
    Implements ICampaignReportsRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork


    ''' <summary>
    ''' Obtengo una Lista de Campaign Reports
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function GetListCampaignReports(data As Tuple(Of Integer, Integer)) As List(Of CampaignReports) Implements ICampaignReportsRepository.GetListCampaignReports
        Dim campaignReports = New List(Of CampaignReports)()
        Dim EntityName As String = String.Empty
        Select Case data.Item2
            Case 1
                Dim resCampaignReports = (From cr In _context.CampaignReports Where cr.CampaignDetailId = data.Item1 And cr.EntityName = "InventoryRequest" Order By cr.EntityId Descending).ToList()
                If resCampaignReports IsNot Nothing AndAlso resCampaignReports.Any Then
                    resCampaignReports.ForEach(Sub(ls As CampaignReports)
                                                   Dim newItem = New CampaignReports
                                                   With newItem
                                                       .Id = ls.Id
                                                       .EntityId = ls.EntityId
                                                       .EntityName = ls.EntityName
                                                       .InventoryRequest = (From ir In _context.InventoryRequest Where ir.Id = ls.EntityId).FirstOrDefault()
                                                       campaignReports.Add(newItem)
                                                   End With
                                               End Sub)
                End If
            Case 2
                Dim resCampaignReports = (From cr In _context.CampaignReports Where cr.CampaignDetailId = data.Item1 And cr.EntityName = "TransferOrder" Order By cr.EntityId Descending).ToList()
                If resCampaignReports IsNot Nothing AndAlso resCampaignReports.Any Then
                    resCampaignReports.ForEach(Sub(ls As CampaignReports)
                                                   Dim newItem = New CampaignReports
                                                   With newItem
                                                       .Id = ls.Id
                                                       .EntityId = ls.EntityId
                                                       .EntityName = ls.EntityName
                                                       .TransferOrder = (From ir In _context.TransferOrder Where ir.Id = ls.EntityId).FirstOrDefault()
                                                       campaignReports.Add(newItem)
                                                   End With
                                               End Sub)
                End If
        End Select
        Return campaignReports
    End Function

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
