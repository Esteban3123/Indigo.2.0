'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface IInitialBalancePayrollRepository
    Inherits IRepository(Of JournalVouchers)

    ''' <summary>
    ''' Importa el archivo de excel y valida los datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ImportFileInitialBalancePayroll(data As String) As List(Of SP_ImportFileInitialBalancePayroll_Result)

    ''' <summary>
    ''' Guarda la información de saldo inicial
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveInitialBalancePayroll(Data As String, CodeUser As String) As List(Of SP_SaveInitialBalancePayroll_Result)

End Interface
