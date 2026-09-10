'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 16-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity

Public Class InventoryControlDocumentRepository
    Inherits GenericRepository(Of InventoryControlDocument)
    Implements IInventoryControlDocumentRepository


    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' consulta por umero de documento el registro control de inventario
    ''' </summary>
    ''' <param name="DocumentNumber"></param>
    ''' <param name="DocumentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlDocumentByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As InventoryControlDocument Implements IInventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumber
        If String.IsNullOrEmpty(DocumentNumber) Then
            Throw New ArgumentNullException("DocumentNumber")
        End If
        If DocumentType = 0 Then
            Throw New ArgumentNullException("DocumentType")
        End If
        Dim query = (From pc In _context.InventoryControlDocument Where pc.DocumentNumber.Equals(DocumentNumber) And pc.DocumentType = DocumentType Select pc).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From pc In _context.InventoryControlDocument.AsNoTracking() Where pc.DocumentNumber.Equals(DocumentNumber) And pc.DocumentType = DocumentType Select pc).FirstOrDefault()
            Return query
        Else
            Return New InventoryControlDocument()
        End If
    End Function

    ''' <summary>
    ''' consulta por umero de documento el registro control de inventario
    ''' </summary>
    ''' <param name="DocumentNumber"></param>
    ''' <param name="DocumentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryControlDocumentByDocumentNumberAsync(DocumentNumber As String, Optional DocumentType As Integer = 0) As Task(Of InventoryControlDocument) Implements IInventoryControlDocumentRepository.GetInventoryControlDocumentByDocumentNumberAsync
        If String.IsNullOrEmpty(DocumentNumber) Then
            Throw New ArgumentNullException("DocumentNumber")
        End If

        If DocumentType = 0 Then
            Throw New ArgumentNullException("DocumentType")
        End If

        Dim query = Await (From pc In _context.InventoryControlDocument
                           Where pc.DocumentNumber.Equals(DocumentNumber) And pc.DocumentType = DocumentType
                           Select pc).FirstOrDefaultAsync()

        If query IsNot Nothing Then
            query.OriginalValue = (From pc In _context.InventoryControlDocument.AsNoTracking() Where pc.DocumentNumber.Equals(DocumentNumber) And pc.DocumentType = DocumentType Select pc).FirstOrDefault()
            Return query
        Else
            Return New InventoryControlDocument()
        End If
    End Function

    ''' <summary>
    ''' consulta por Id el control de inventario
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlDocumentById(Id As Integer) As InventoryControlDocument Implements IInventoryControlDocumentRepository.GetInventoryControlDocumentById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From pc In _context.InventoryControlDocument Where pc.Id = Id Select pc).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From pc In _context.InventoryControlDocument.AsNoTracking() Where pc.Id = Id Select pc).FirstOrDefault()
            Return query
        Else
            Return New InventoryControlDocument()
        End If
    End Function
End Class
