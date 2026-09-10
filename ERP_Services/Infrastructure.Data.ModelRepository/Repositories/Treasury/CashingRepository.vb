'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CashingRepository
    Inherits GenericRepository(Of CheckCashing)
    Implements ICashingRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    Public Function GetCashing(code As String) As CheckCashing Implements ICashingRepository.GetCashing
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim query = (From cc In _context.CheckCashing.Include("VoucherTransaction") Where cc.Code.Equals(code) Select cc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From cc In _context.CheckCashing.AsNoTracking() Where cc.Code.Equals(code) Select cc).FirstOrDefault()

            If query.VoucherTransaction IsNot Nothing AndAlso query.VoucherTransaction.Id > 0 Then
                Dim IdEntityBankAccount As Integer? = query.VoucherTransaction.IdEntityBankAccount
                Dim varEntityBankAccount = (From EA In _context.EntityBankAccounts
                                            Join B In _context.Bank On EA.IdBank Equals B.Id
                                            Where EA.Id = IdEntityBankAccount
                                            Select New With {Key .Code = EA.Code, Key .Name = B.Name}).FirstOrDefault()

                With query.VoucherTransaction
                    .FullNameEntityBankAccount = String.Concat(varEntityBankAccount.Code, " - ", varEntityBankAccount.Name)
                    .FullNameThird = (From t In _context.ThirdParty Where t.Id = query.VoucherTransaction.IdThirdParty Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
                    .FullNameMainAccount = (From ma In _context.MainAccounts Where ma.Id = query.VoucherTransaction.IdMainAccount Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End With
            End If
            
            Return query
        Else
            Return New CheckCashing()
        End If
    End Function

    ''' <summary>
    ''' Gets the cashing by identifier.
    ''' </summary>
    Public Function GetCashingById(id As Integer, tracking As Boolean) As CheckCashing Implements ICashingRepository.GetCashingById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim query As CheckCashing = Nothing
        If tracking Then
            query = (From cc In _context.CheckCashing.Include("VoucherTransaction") Where cc.Id = id Select cc).FirstOrDefault()
        Else
            query = (From cc In _context.CheckCashing.AsNoTracking() Where cc.Id = id Select cc).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From cc In _context.CheckCashing.AsNoTracking() Where cc.Id = id Select cc).FirstOrDefault()
            Return query
        Else
            Return New CheckCashing()
        End If
    End Function

End Class