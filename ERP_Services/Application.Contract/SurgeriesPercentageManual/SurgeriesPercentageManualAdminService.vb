'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class SurgeriesPercentageManualAdminService
    Implements ISurgeriesPercentageManualAdminService

    Private _surgeriesPercentageManualRepository As ISurgeriesPercentageManualRepository

    Public Sub New(surgeriesPercentageManualRepository As ISurgeriesPercentageManualRepository)
        If surgeriesPercentageManualRepository Is Nothing Then
            Throw New ArgumentNullException("surgeriesPercentageManualRepository")
        End If
        _surgeriesPercentageManualRepository = surgeriesPercentageManualRepository
    End Sub

    ''' <summary>
    ''' metodo para obtener las tarifas para los eventos en la orden de servicio
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <param name="InterventionType"></param>
    ''' <returns></returns>
    Public Function GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId As Integer, InterventionType As Integer) As SurgeriesPercentageManual Implements ISurgeriesPercentageManualAdminService.GetSurgeriesPercentageManualByRateManualIdInterventionType
        Try
            Return _surgeriesPercentageManualRepository.GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId, InterventionType)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New SurgeriesPercentageManual
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            Me._surgeriesPercentageManualRepository = Nothing
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
