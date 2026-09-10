'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-06-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class BillingReversalReasonRepository
    Inherits GenericRepository(Of BillingReversalReason)
    Implements IBillingReversalReasonRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una razón de anulacóon por código
    ''' </summary>
    Public Function GetReversalReason(code As String, tracking As Boolean) As BillingReversalReason Implements IBillingReversalReasonRepository.GetReversalReason
        Dim query = Nothing
        If tracking Then
            query = (From r As BillingReversalReason In _context.BillingReversalReason Where r.Code.Equals(code) Select r).FirstOrDefault()
        Else
            query = (From r As BillingReversalReason In _context.BillingReversalReason.AsNoTracking() Where r.Code.Equals(code) Select r).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From r As BillingReversalReason In _context.BillingReversalReason.AsNoTracking() Where r.Code.Equals(code) Select r).FirstOrDefault()
            Return query
        Else
            Return New BillingReversalReason()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una razón ade anulación por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetReversalReasonById(id As Integer, tracking As Boolean) As BillingReversalReason Implements IBillingReversalReasonRepository.GetReversalReasonById
        Dim query = Nothing
        If tracking Then
            query = (From r As BillingReversalReason In _context.BillingReversalReason Where r.Id = id Select r).FirstOrDefault()
        Else
            query = (From r As BillingReversalReason In _context.BillingReversalReason.AsNoTracking() Where r.Id = id Select r).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From r As BillingReversalReason In _context.BillingReversalReason.AsNoTracking() Where r.Id = id Select r).FirstOrDefault()
            Return query
        Else
            Return New BillingReversalReason()
        End If
    End Function

End Class
