'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/11/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo

#End Region

Public Class MPatientExternalCareCenter
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

    Public Async Function GetPatientExternalCareCenter(ByVal code As String) As Task(Of PatientExternalCareCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetPatientExternalCareCenterAsync(code, _sessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetPerson(ByVal code As String) As Task(Of Person)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetPersonByIdentificationAsync(code, _sessionValues)
    End Function

    Public Async Function GetPatientExternalCareCenterById(ByVal id As Integer) As Task(Of PatientExternalCareCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetPatientExternalCareCenterByIdAsync(id)
    End Function

    Public Async Function SavePatientExternalCareCenter(ByVal PatientExternalCareCenter As PatientExternalCareCenter, ByVal idSequence As Int64) As Task(Of ActionResult(Of PatientExternalCareCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SavePatientExternalCareCenterAsync(PatientExternalCareCenter, Me._sessionValues.AuditMessageWcf, idSequence)
    End Function

    Public Async Function DeletePatientExternalCareCenter(ByVal PatientExternalCareCenter As PatientExternalCareCenter) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeletePatientExternalCareCenterAsync(PatientExternalCareCenter, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function ChangeStatePatientExternalCareCenter(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of PatientExternalCareCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ChangeStatePatientExternalCareCenterAsync(code, state, Me._sessionValues.AuditMessageWcf)
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
