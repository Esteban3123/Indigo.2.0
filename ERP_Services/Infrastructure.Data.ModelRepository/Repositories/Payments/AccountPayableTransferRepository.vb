'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AccountPayableTransferRepository
    Inherits GenericRepository(Of AccountPayableTransfer)
    Implements IAccountPayableTransferRepository

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
    ''' Obtiene un traslado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableTransfer(code As String, Optional tracking As Boolean = True) As AccountPayableTransfer Implements IAccountPayableTransferRepository.GetAccountPayableTransfer
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AccountPayableTransfer In Me._context.AccountPayableTransfer.Include("AccountPayableTransferDetail") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.AccountPayableTransferDetail IsNot Nothing AndAlso res.AccountPayableTransferDetail.Count > 0 Then
                For Each itemDetail As AccountPayableTransferDetail In res.AccountPayableTransferDetail
                    If res.TranferType = 1 Then 'cuenta por pagar
                        Dim _accountPayable = (From ap In _context.AccountPayable.AsNoTracking Where ap.Id = itemDetail.AccountPayableId Select ap).FirstOrDefault
                        Dim _supplier = (From s In _context.Supplier.AsNoTracking Where s.Id = _accountPayable.IdSupplier Select s).FirstOrDefault
                        itemDetail.AccountPayableSupplierDescription = _supplier.Code + " - " + _supplier.Name
                        itemDetail.AccountPayableConsecutive = _accountPayable.Code
                        itemDetail.AccountPayableBillNumber = _accountPayable.BillNumber
                        itemDetail.AccountPayableDocumentDate = _accountPayable.DocumentDate
                    Else
                        Dim refund = (From r In _context.Refunds.AsNoTracking() Where r.Id = itemDetail.RefundId Select r).FirstOrDefault()
                        itemDetail.AccountPayableConsecutive = refund.Code
                        itemDetail.AccountPayableDocumentDate = refund.InitialDate
                    End If
                    

                    Select Case itemDetail.Status
                        Case 1
                            itemDetail.StatusName = "Pendiente por Aceptación"
                        Case 2
                            itemDetail.StatusName = "Aceptada"
                        Case 3
                            itemDetail.StatusName = "Rechazada"
                        Case 4
                            itemDetail.StatusName = "Anulada"
                    End Select

                    If itemDetail.RejectionReasonId IsNot Nothing Then
                        Dim rejection = (From r In _context.AccountPayableRejectionReason.AsNoTracking Where r.Id = itemDetail.RejectionReasonId Select r).FirstOrDefault
                        itemDetail.RejectionReasonDescription = rejection.Code + " - " + rejection.Name
                    End If
                Next
            End If

            res.OriginalValue = (From d As AccountPayableTransfer In Me._context.AccountPayableTransfer.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New AccountPayableTransfer()
        End If
    End Function

    ''' <summary>
    ''' Consulta un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableTransferById(id As String, Optional tracking As Boolean = True) As AccountPayableTransfer Implements IAccountPayableTransferRepository.GetAccountPayableTransferById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As List(Of AccountPayableTransfer)
        If tracking = True Then
            res = (From d In Me._context.AccountPayableTransfer Where d.Id = id Select d).ToList()
        Else
            res = (From d In Me._context.AccountPayableTransfer.AsNoTracking Where d.Id = id Select d).ToList()
        End If
        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New AccountPayableTransfer()
        End If
    End Function

    ''' <summary>
    ''' Consulta los detalles del traslado por id de cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableTransferDetailByAccountPayableId(accountPayableId As Integer) As AccountPayableTransferDetail Implements IAccountPayableTransferRepository.GetAccountPayableTransferDetailByAccountPayableId
        If accountPayableId = 0 Then
            Throw New ArgumentNullException("accountPayableId")
        End If
        Dim res = (From aptd In _context.AccountPayableTransferDetail.AsNoTracking Where aptd.AccountPayableId = accountPayableId AndAlso aptd.Status = 1 Select aptd).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function

    Public Function GetAccountPayableTransferDetailByRefundId(refundId As Integer) As AccountPayableTransferDetail Implements IAccountPayableTransferRepository.GetAccountPayableTransferDetailByRefundId
        Dim res = (From aptd In _context.AccountPayableTransferDetail.AsNoTracking Where aptd.RefundId = refundId AndAlso aptd.Status = 1 Select aptd).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function

    Public Function GetAccountPayableFilingUnitSourceIdAccountPayableId(accountPayableId As Integer, FilingUnitSourceId As Integer) As AccountPayable Implements IAccountPayableTransferRepository.GetAccountPayableFilingUnitSourceIdAccountPayableId
        Return (From ap In _context.AccountPayable.AsNoTracking()
                Where ap.FilingUnitId = FilingUnitSourceId And ap.Id = accountPayableId Select ap).FirstOrDefault
    End Function

    Public Function GetAccountPayableFilingUnitSourceIdRefundId(refundId As Integer, FilingUnitSourceId As Integer) As Refunds Implements IAccountPayableTransferRepository.GetAccountPayableFilingUnitSourceIdRefundId
        Return (From r In _context.Refunds.AsNoTracking()
                Where r.FilingUnitId = FilingUnitSourceId And r.Id = refundId Select r).FirstOrDefault
    End Function

End Class
