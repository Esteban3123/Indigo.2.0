'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Domain.Entities.Service
Imports Domain.Crystal
Imports System.Transactions

Public Class MedicalFeesNoteAdminService
    Implements IMedicalFeesNoteAdminService

#Region "Variables"

    ''' <summary>
    ''' Repositorio de notas de causacion de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _medicalFeesNotesRepository As IMedicalFeesNoteRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal medicalFeesNotesRepository As IMedicalFeesNoteRepository)
        If medicalFeesNotesRepository Is Nothing Then
            Throw New ArgumentNullException("medicalFeesNotesRepository Vacio")
        End If
        _medicalFeesNotesRepository = medicalFeesNotesRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene las notas de la causacion de honorarios medicos
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="InvoiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber As String, InvoiceId As Integer) As ActionResult(Of List(Of MedicalFeesNote)) Implements IMedicalFeesNoteAdminService.GetMedicalFeesNotesByAdmissionNumberAndInvoiceId
        If AdmissionNumber Is String.Empty Then
            Throw New ArgumentNullException("AdmissionNumber")
        End If
        If InvoiceId = 0 Then
            Throw New ArgumentNullException("InvoiceId")
        End If
        Try
            Dim ListMedicalFeesNotes As List(Of MedicalFeesNote) = Me._medicalFeesNotesRepository.GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber, InvoiceId)
            Return New ActionResult(Of List(Of MedicalFeesNote)) With {.StateResult = True, .ObjectEmbbeded = ListMedicalFeesNotes}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of MedicalFeesNote)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _medicalFeesNotesRepository = Nothing
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
