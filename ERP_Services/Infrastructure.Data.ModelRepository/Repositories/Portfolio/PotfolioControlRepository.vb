'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PortfolioControlRepository
    Inherits GenericRepository(Of PortfolioControl)
    Implements IPortfolioControlRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de control de cartera por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <param name="DocumentType"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' DocumentNumber
    ''' or
    ''' DocumentType
    ''' </exception>
    Public Function GetPortfolioControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As PortfolioControl Implements IPortfolioControlRepository.GetPortfolioControlByDocumentNumber
        If String.IsNullOrEmpty(DocumentNumber) Then
            Throw New ArgumentNullException("DocumentNumber")
        End If
        If DocumentType = 0 Then
            Throw New ArgumentNullException("DocumentType")
        End If
        Dim query = (From pc In _context.PortfolioControl Where pc.DocumentNumber.Equals(DocumentNumber) And pc.DocumentType = DocumentType Select pc).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From pc In _context.PortfolioControl.AsNoTracking() Where pc.DocumentNumber.Equals(DocumentNumber) And pc.DocumentType = DocumentType Select pc).FirstOrDefault()
            Return query
        Else
            Return New PortfolioControl()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de cartera por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetPortfolioControlById(Id As Integer) As PortfolioControl Implements IPortfolioControlRepository.GetPortfolioControlById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From pc In _context.PortfolioControl Where pc.Id = Id Select pc).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From pc In _context.PortfolioControl.AsNoTracking() Where pc.Id = Id Select pc).FirstOrDefault()
            Return query
        Else
            Return New PortfolioControl()
        End If
    End Function
End Class
