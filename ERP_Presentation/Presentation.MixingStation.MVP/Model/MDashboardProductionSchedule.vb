'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/03/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MDashboardProductionSchedule
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetProductionScheduleById(ByVal id As Integer) As Task(Of ActionResult(Of ProductionSchedule))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductionScheduleByIdAsync(id)
    End Function

    Public Async Function GetProductionSchedule(ByVal code As String) As Task(Of ActionResult(Of ProductionSchedule))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductionScheduleAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveProductionSchedule(args As Object, authorizeUsers As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail) As Task(Of ActionResult(Of SP_SaveProductionSchedule_Result))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveProductionScheduleAsync(parameter, authorizeUsers, campaignDetail, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function ValidateProductionOrder(campaignDetailIds As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ValidateProductionScheduleAsync(campaignDetailIds)
    End Function

    Public Async Function CampaingDetailStatusChange(ByVal CampaingDetailId As List(Of Integer), Action As Byte) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.CampaingDetailStatusChangeAsync(CampaingDetailId, Action, Me._sessionValues.AuditMessageWcf)
    End Function

    Async Function GetCampaignDetailIdsByBatchCode(cmConfigurationId As Integer, productionLineId As Integer, batchCode As String) As Task(Of List(Of Integer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCampaignDetailIdsByBatchCodeAsync(cmConfigurationId, productionLineId, batchCode)
    End Function


    ''' <summary>
    ''' Obtiene los detalles de los paquetes por campaña
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetItemsByCampaigns(campaignDetailId As Integer) As Task(Of ActionResult(Of SP_ListViewItemsCampaigns_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetItemsCampaignsAsync(campaignDetailId, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda los usuarios autorizados
    ''' </summary>
    ''' <param name="AuthorizeUserslist"></param>
    ''' <param name="ProcessingDate"></param>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Async Function SaveAuthorizeUsers(AuthorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail) As Task(Of ActionResult(Of CampaignDetailUsers))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveAuthorizeUsersAsync(AuthorizeUserslist, campaignDetail, _sessionValues.AuditMessageWcf)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
