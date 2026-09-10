'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IInterfaceNET


    Function CreatePayrollMovementAccount(ByVal ListCostDistribution As List(Of CostDistributions), LiquidationConfirm As List(Of Liquidation), ListEmployee As List(Of Employee), Group As Group) As ActionResult(Of List(Of InterfaceResult))

End Interface
