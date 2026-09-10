'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

<ServiceContract()>
Public Interface IPayrollInitialBalancePayroll

    ''' <summary>
    ''' Importa el archivo de excel y se valida la info
    ''' </summary>
    <OperationContract()>
    Function SP_ImportFileInitialBalancePayroll(data As List(Of ImportFileRow), session As SessionValues) As ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result))

    ''' <summary>
    ''' Guarda la informacion de los saldos iniciales
    ''' </summary>
    ''' <param name="ListInfo"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_SaveInitialBalancePayroll(ListInfo As List(Of SP_ImportFileInitialBalancePayroll_Result), session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
