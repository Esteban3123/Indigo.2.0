'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AccountPayableTransferDetailRepository
    Inherits GenericRepository(Of AccountPayableTransferDetail)
    Implements IAccountPayableTransferDetailRepository


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
    ''' Lista de detalle de traslado
    ''' </summary>
    ''' <param name="listIDAccountPayableTransferDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListAccountPayableTransferDetailByAccountPayableId(listIDAccountPayableTransferDetail As List(Of Integer)) As List(Of AccountPayableTransferDetail) Implements IAccountPayableTransferDetailRepository.GetListAccountPayableTransferDetailByAccountPayableId
        Dim res = (From aptd In _context.AccountPayableTransferDetail.Include("AccountPayable").Include("Refunds")
                   Where listIDAccountPayableTransferDetail.Contains(aptd.Id)
                   Select aptd).ToList()
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
            End If
    End Function
    ''' <summary>
    ''' Valida si es la ultima revision de traslado que se le realiza al oficio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateConfirmEvaluation(AccountPayableTransferId As Integer) As Boolean Implements IAccountPayableTransferDetailRepository.ValidateConfirmEvaluation
        Dim res = (From e In _context.AccountPayableTransferDetail
                  Where e.Status = 1 And e.AccountPayableTransferId = AccountPayableTransferId
                  Select e).Count()
        If res = 0 Then 'si no hay mas item en estado 1 - pendiente por aceptacion retornamos verdadero para que se confirme el oficio, de lo contrario false 
            Return True
        Else
            Return False
        End If
    End Function

End Class
