'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PaymentControlRepository
    Inherits GenericRepository(Of PaymentsControl)
    Implements IPaymentControlRepository

    'Contexto de Tesoreria
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de control de pagos por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPaymentControlById(Id As Integer) As PaymentsControl Implements IPaymentControlRepository.GetPaymentControlById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From tc In _context.PaymentsControl Where tc.Id = Id Select tc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From tc In _context.PaymentsControl.AsNoTracking() Where tc.Id = Id Select tc).FirstOrDefault()
            Return query
        Else
            Return New PaymentsControl()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de control de pago por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">DocumentNumber</exception>
    Public Function GetPaymentControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As PaymentsControl Implements IPaymentControlRepository.GetPaymentControlByDocumentNumber
        If String.IsNullOrEmpty(DocumentNumber) Then
            Throw New ArgumentNullException("DocumentNumber")
        End If
        If DocumentType = 0 Then
            Throw New ArgumentNullException("DocumentType")
        End If
        Dim query = (From tc In _context.PaymentsControl Where tc.DocumentNumber.Equals(DocumentNumber) And tc.DocumentType.Equals(DocumentType) Select tc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From tc In _context.PaymentsControl.AsNoTracking() Where tc.DocumentNumber.Equals(DocumentNumber) And tc.DocumentType.Equals(DocumentType) Select tc).FirstOrDefault()
            Return query
        Else
            Return New PaymentsControl()
        End If
    End Function

End Class