'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Diego Andres Roldan Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AgesPaymentsRepository
    Inherits GenericRepository(Of AgesPayments)
    Implements IAgesPaymentsRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una edad de pagos
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetAgesPaymentsById(Id As Integer) As AgesPayments Implements IAgesPaymentsRepository.GetAgesPaymentsById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From ap In _context.AgesPayments Where ap.Id = Id Select ap).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From ap In _context.AgesPayments.AsNoTracking() Where ap.Id = Id Select ap).FirstOrDefault()
            Return query
        Else
            Return New AgesPayments()
        End If
    End Function

    ''' <summary>
    ''' lista las edades de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPayments() As List(Of AgesPayments) Implements IAgesPaymentsRepository.ListAgesPayments
        Return (From ap In _context.AgesPayments Select ap).ToList()
    End Function

    ''' <summary>
    ''' lista las edades de pagos por unidad operativa
    ''' </summary>
    ''' <param name="UnitOperativeId">The unit operative identifier.</param>
    ''' <returns></returns>
    Public Function ListAgesPaymentsByUnitOperativeId(UnitOperativeId As Integer) As List(Of AgesPayments) Implements IAgesPaymentsRepository.ListAgesPaymentsByUnitOperativeId
        Return (From ap In _context.AgesPayments
                Join sp In _context.SettingPayments On ap.SettingPaymentId Equals sp.Id
                Where sp.IdOperatingUnit = UnitOperativeId Select ap).ToList()
    End Function

End Class