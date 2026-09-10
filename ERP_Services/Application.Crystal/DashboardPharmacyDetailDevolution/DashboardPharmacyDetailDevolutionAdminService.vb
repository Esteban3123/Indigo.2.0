'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos E. Cordoba
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Crystal
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class DashboardPharmacyDetailDevolutionAdminService
    Implements IDashboardPharmacyDetailDevolutionAdminService

    Private _dashboardPharmacyDetailDevolutionRepository As IDashboardPharmacyDetailDevolutionRepository

    Public Sub New(dashboardPharmacyDetailDevolutionRepository As IDashboardPharmacyDetailDevolutionRepository)
        If dashboardPharmacyDetailDevolutionRepository Is Nothing Then
            Throw New ArgumentNullException("dashboardPharmacyDetailDevolutionRepository")
        End If
        _dashboardPharmacyDetailDevolutionRepository = dashboardPharmacyDetailDevolutionRepository
    End Sub

    ''' <summary>
    ''' lista los detalle de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    Public Function ListDashboardPharmacyDetailDevolution(consecutive As Decimal, patientCode As String, admission As String) As List(Of Entities.ViewDashboardPharmacyDetailDevolution) Implements IDashboardPharmacyDetailDevolutionAdminService.ListDashboardPharmacyDetailDevolution
        Try
            Return _dashboardPharmacyDetailDevolutionRepository.ListDashboardPharmacyDetailDevolution(consecutive, patientCode, admission)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution)
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _dashboardPharmacyDetailDevolutionRepository = Nothing
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
