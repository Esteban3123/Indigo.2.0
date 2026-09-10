'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Duván Albeiro Mejia Cortes 
' Created          : 2021-12-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Class CampaignReportsAdminService
    Implements ICampaignReportsAdminService, Inject

    ''' <summary>
    ''' repository
    ''' </summary>
    Private ReadOnly _campaignReportsRepository As ICampaignReportsRepository

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(campaignReportsRepository As ICampaignReportsRepository)
        _campaignReportsRepository = campaignReportsRepository
    End Sub

#Region "Functions"

    ''' <summary>
    ''' Guardar un Movimiento de documentos en CampaignReports
    ''' </summary>
    ''' <typeparam name="TEntity"></typeparam>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function SaveReports(Of TEntity As {IObjectWithChangeTracker})(
        campaignDetailId As Integer,
        entityId As Integer,
        Optional ProcessName As String = Nothing
    ) As ActionResult(Of CampaignReports) Implements ICampaignReportsAdminService.SaveReports
        Try

            Dim newReports As New CampaignReports With {
                .CampaignDetailId = campaignDetailId,
                .EntityId = entityId,
                .EntityName = GetType(TEntity).Name
            }

            If Not String.IsNullOrEmpty(ProcessName) Then
                newReports.EntityName += ProcessName
            End If

            _campaignReportsRepository.SaveEntity(newReports)
            _campaignReportsRepository.UnitWork.Commit()

            Return New ActionResult(Of CampaignReports) With {.StateResult = True, .ObjectEmbbeded = newReports}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CampaignReports) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtener una Lista Movimientos por CampaignDetail y tipo reporte
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="Audit"></param>
    ''' <returns></returns>
    Public Function GetListCampaignReportsByAction(Data As Tuple(Of Integer, Integer), Audit As AuditMessage) As List(Of CampaignReports) Implements ICampaignReportsAdminService.GetListCampaignReportsByAction
        Try
            If Data Is Nothing Then
                Throw New ArgumentNullException("Data")
            End If
            Return _campaignReportsRepository.GetListCampaignReports(Data)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
