'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordPaymentsRepository
    Inherits GenericRepository(Of BlockRecordPayments)
    Implements IBlockRecordPaymentsRepository

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
    ''' Obtiene el registro si esta bloqueado o no
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordPaymentsByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordPayments Implements IBlockRecordPaymentsRepository.GetBlockRecordPaymentsByIdformAndIdRecord
        If tracking Then
            Dim blockRecordPayments = From e In _context.BlockRecordPayments
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecordPayments.Count > 0 Then
                Return blockRecordPayments.FirstOrDefault
            Else
                Return New BlockRecordPayments()
            End If
        Else
            Dim blockRecordPayments = (From e In _context.BlockRecordPayments.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecordPayments IsNot Nothing > 0 Then
                Return blockRecordPayments
            Else
                Return New BlockRecordPayments()
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro si esta bloqueado o no
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="Consecutive"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordPaymentsByIdformAndConsecutive(IdForm As String, Consecutive As String, Optional tracking As Boolean = True) As BlockRecordPayments Implements IBlockRecordPaymentsRepository.GetBlockRecordPaymentsByIdformAndConsecutive
        If tracking Then
            Dim blockRecordPayments As New BlockRecordPayments()

            If IdForm = 730 Then 'Cuentas por pagar
                blockRecordPayments = (From brp In _context.BlockRecordPayments
                                       Join ap In _context.AccountPayable.AsNoTracking() ON brp.IdRecord Equals ap.Id
                                       Where brp.IdForm = IdForm AndAlso ap.Code = Consecutive
                                       Select brp).FirstOrDefault()
            End If

            If blockRecordPayments IsNot Nothing Then
                Return blockRecordPayments
            Else
                Return New BlockRecordPayments()
            End If
        Else
            Dim blockRecordPayments As New BlockRecordPayments()

            If IdForm = 730 Then 'Cuentas por pagar
                blockRecordPayments = (From brp In _context.BlockRecordPayments.AsNoTracking()
                                       Join ap In _context.AccountPayable.AsNoTracking() ON brp.IdRecord Equals ap.Id
                                       Where brp.IdForm = IdForm AndAlso ap.Code = Consecutive
                                       Select brp).FirstOrDefault()
            End If

            If blockRecordPayments IsNot Nothing Then
                Return blockRecordPayments
            Else
                Return New BlockRecordPayments()
            End If
        End If
    End Function
End Class
