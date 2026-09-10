'***********************************************************************
' Assembly         : Application.Crystal
' Author           : J. Kevin Garay 
' Created          : 11-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
#End Region
Public Class ExternalConsultationAdminService
    Implements IExternalConsultationAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de estancias
    ''' </summary>
    Private _externalConsultationRepository As IExternalConsultationRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(externalConsultationRepository As IExternalConsultationRepository)
        _externalConsultationRepository = externalConsultationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Listars the citas medicas.
    ''' </summary>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="atentionCenterCode">The atention center code.</param>
    ''' <returns></returns>
    Public Function ListarCitasMedicas(patientCode As String, atentionCenterCode As String) As List(Of SP_AD_ListarCitasMedicasNativo_Result) Implements IExternalConsultationAdminService.ListarCitasMedicas
        Try
            Return _externalConsultationRepository.ListarCitasMedicas(patientCode, atentionCenterCode)
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

            End If
            _externalConsultationRepository = Nothing
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