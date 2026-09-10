'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MedicalFeesNoteRepository
    Inherits GenericRepository(Of MedicalFeesNote)
    Implements IMedicalFeesNoteRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene las notas de causacion de honorarios medicos
    ''' </summary>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="InvoiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesNotesByAdmissionNumberAndInvoiceId(AdmissionNumber As String, InvoiceId As Integer) As List(Of MedicalFeesNote) Implements IMedicalFeesNoteRepository.GetMedicalFeesNotesByAdmissionNumberAndInvoiceId
        If AdmissionNumber Is String.Empty Then
            Throw New ArgumentNullException("AdmissionNumber")
        End If
        If InvoiceId = 0 Then
            Throw New ArgumentNullException("InvoiceId")
        End If
        Dim res = (From mfn In Me._context.MedicalFeesNote Where mfn.AdmissionNumber = AdmissionNumber AndAlso mfn.InvoiceId = InvoiceId Select mfn).ToList
        If res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function

#End Region

End Class
