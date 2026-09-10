'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

Public Interface IInitialBalancePayrollAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Importa el archivo de excel y se valida la información
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Function SP_ImportFileInitialBalancePayroll(data As List(Of ImportFileRow)) As ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result))

    ''' <summary>
    ''' Guarda la informacion de los saldos iniciales
    ''' </summary>
    ''' <param name="ListInfo"></param>
    ''' <returns></returns>
    Function SP_SaveInitialBalancePayroll(ListInfo As List(Of SP_ImportFileInitialBalancePayroll_Result), Audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
