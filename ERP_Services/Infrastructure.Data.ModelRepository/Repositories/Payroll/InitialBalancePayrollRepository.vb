'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class InitialBalancePayrollRepository
    Inherits GenericRepository(Of JournalVouchers)
    Implements IInitialBalancePayrollRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Importa el archivo de excel y valida la información
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SP_ImportFileInitialBalancePayroll(data As String) As List(Of SP_ImportFileInitialBalancePayroll_Result) Implements IInitialBalancePayrollRepository.SP_ImportFileInitialBalancePayroll
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportFileInitialBalancePayroll(data).ToList()
    End Function

    ''' <summary>
    ''' Guarda la informacion de los saldos iniciales
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveInitialBalancePayroll(Data As String, CodeUser As String) As List(Of SP_SaveInitialBalancePayroll_Result) Implements IInitialBalancePayrollRepository.SP_SaveInitialBalancePayroll
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveInitialBalancePayroll(Data, CodeUser).ToList()
    End Function

End Class
