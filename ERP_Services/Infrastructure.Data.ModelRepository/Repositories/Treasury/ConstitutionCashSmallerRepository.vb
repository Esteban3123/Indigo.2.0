'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ConstitutionCashSmallerRepository
    Inherits GenericRepository(Of ConstitutionCashSmaller)
    Implements IConstitutionCashSmallerRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    Public Function GetConstitutionCashSmaller(code As String) As ConstitutionCashSmaller Implements IConstitutionCashSmallerRepository.GetConstitutionCashSmaller
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim query = (From cc In _context.ConstitutionCashSmaller Where cc.Code.Equals(code) Select cc).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then

            query.CashRegisterSmallerDescription = (From x In _context.CashRegisters.AsNoTracking Where x.Id = query.CashRegisterSmallerId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault

            If query.CashRegisterId IsNot Nothing Then
                query.CashRegisterDescription = (From x In _context.CashRegisters.AsNoTracking Where x.Id = query.CashRegisterId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault
            End If

            If query.EntityBankAccountId IsNot Nothing Then
                query.EntityBankAccountDescription = (From x In _context.EntityBankAccounts.AsNoTracking
                                                      Join b In _context.Bank.AsNoTracking On x.IdBank Equals b.Id
                                                      Where x.Id = query.EntityBankAccountId Select String.Concat(x.Code, " - ", b.Name)).FirstOrDefault
            End If

            query.OriginalValue = (From cc In _context.ConstitutionCashSmaller.AsNoTracking() Where cc.Code.Equals(code) Select cc).FirstOrDefault()
            Return query
        Else
            Return New ConstitutionCashSmaller()
        End If
    End Function

    ''' <summary>
    ''' Gets the cashing by identifier.
    ''' </summary>
    Public Function GetConstitutionCashSmallerById(id As Integer, tracking As Boolean) As ConstitutionCashSmaller Implements IConstitutionCashSmallerRepository.GetConstitutionCashSmallerById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim query As ConstitutionCashSmaller = Nothing
        If tracking Then
            query = (From cc In _context.ConstitutionCashSmaller Where cc.Id = id Select cc).FirstOrDefault()
        Else
            query = (From cc In _context.ConstitutionCashSmaller.AsNoTracking() Where cc.Id = id Select cc).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From cc In _context.ConstitutionCashSmaller.AsNoTracking() Where cc.Id = id Select cc).FirstOrDefault()
            Return query
        Else
            Return New ConstitutionCashSmaller()
        End If
    End Function

    ''' <summary>
    ''' Realiza el proceso de fondo de caja menor
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveConstitutionCashSmaller(xmlObject As String, codeUser As String) As SP_SaveConstitutionCashSmaller_Result Implements IConstitutionCashSmallerRepository.SP_SaveConstitutionCashSmaller
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveConstitutionCashSmaller(xmlObject, codeUser).SingleOrDefault
    End Function

End Class