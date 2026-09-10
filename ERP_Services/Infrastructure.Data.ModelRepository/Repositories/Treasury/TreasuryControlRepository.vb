'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class TreasuryControlRepository
    Inherits GenericRepository(Of TreasuryControl)
    Implements ITreasuryControlRepository

    'Contexto de Tesoreria
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetTreasuryControlById(Id As Integer) As TreasuryControl Implements ITreasuryControlRepository.GetTreasuryControlById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From tc In _context.TreasuryControl Where tc.Id = Id Select tc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From tc In _context.TreasuryControl.AsNoTracking() Where tc.Id = Id Select tc).FirstOrDefault()
            Return query
        Else
            Return New TreasuryControl()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">DocumentNumber</exception>
    Public Function GetTreasuryControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As TreasuryControl Implements ITreasuryControlRepository.GetTreasuryControlByDocumentNumber
        If String.IsNullOrEmpty(DocumentNumber) Then
            Throw New ArgumentNullException("DocumentNumber")
        End If
        If DocumentType = 0 Then
            Throw New ArgumentNullException("DocumentType")
        End If
        Dim query = (From tc In _context.TreasuryControl Where tc.DocumentNumber.Equals(DocumentNumber) And tc.DocumentType = DocumentType Select tc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From tc In _context.TreasuryControl.AsNoTracking() Where tc.DocumentNumber.Equals(DocumentNumber) And tc.DocumentType = DocumentType Select tc).FirstOrDefault()
            Return query
        Else
            Return New TreasuryControl()
        End If
    End Function

End Class