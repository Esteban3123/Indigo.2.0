'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 14-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class IncentivePaymentDomain

    Implements IIncentivePaymentDomain
    ''' <summary>
    ''' Dominio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _LiquidationDomain As ILiquidationDomain


#Region "Constructor"
    Public Sub New(payrollLiquidationFunctions As ILiquidationDomain)
        _LiquidationDomain = payrollLiquidationFunctions
    End Sub
#End Region


    Public Function IncentivePaymentCalculate(employeeLiquidation As List(Of Liquidation), NumberOfIncentivePayment As Byte, PaymentType As Char, Period As Char, IncentiveStarDate As Date, IncentiveEndDate As Date, PayrollNextDate As Date) As ActionMessageResult(Of List(Of IncentivePayment)) Implements IIncentivePaymentDomain.IncentivePaymentCalculate
        Dim AverageIBCIncentivePaymentValue As Double
        Dim IncentivePaymentValue As Double
        Dim IncentivePayment As IncentivePayment
        Dim WorkDays As Integer
        'Dim LiquidationDomain As New liquidationD
        Dim SanctionDays As Integer
        Dim IncentivePaymentList As New List(Of IncentivePayment)

        Dim actionResult As ActionMessageResult(Of List(Of IncentivePayment)) = New ActionMessageResult(Of List(Of IncentivePayment))()
        Dim listContract = (From e In employeeLiquidation
                           Select e.ContractId).Distinct().ToList()

        Dim GroupId As Integer
        Dim ContractInitialDate As Date
        Dim BasicSalary As Double
        Dim ValueTransportingRelief As Double


        For Each contractId As Integer In listContract

            IncentivePayment = New IncentivePayment()

            Dim employeeLiquidationContract = employeeLiquidation.Where(Function(x) x.ContractId = contractId)

            ' Realizamos la Sumatoria de los IBC's de Primas
            For j As Integer = 0 To (employeeLiquidationContract.Count() - 1)
                AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue + employeeLiquidationContract(j).IBCIncentivePayment
                SanctionDays = SanctionDays + employeeLiquidationContract(j).SanctionDays

                ' Calculamos los días Trabajados del Empleado
                If employeeLiquidationContract(j).Contract.ContractInitialDate <= IncentiveStarDate Then
                    If NumberOfIncentivePayment = 1 Then
                        WorkDays = 360
                    Else
                        WorkDays = 180
                    End If
                Else
                    If employeeLiquidationContract(j).Contract.ContractInitialDate < IncentiveEndDate Then
                        WorkDays = _LiquidationDomain.Days360(employeeLiquidationContract(j).Contract.ContractInitialDate, IncentiveEndDate)
                    Else
                        If Year(employeeLiquidationContract(j).Contract.ContractInitialDate) > Year(IncentiveEndDate) Then
                            Continue For
                        End If
                        WorkDays = _LiquidationDomain.Days360(employeeLiquidationContract(j).Contract.ContractInitialDate, IncentiveEndDate)
                    End If
                End If
                If NumberOfIncentivePayment = 1 Then
                    AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue / 12
                Else
                    AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue / 6
                End If

                GroupId = employeeLiquidationContract(j).GroupId
                ContractInitialDate = employeeLiquidationContract(j).Contract.ContractInitialDate
                BasicSalary = employeeLiquidationContract(j).Contract.BasicSalary
                ValueTransportingRelief = employeeLiquidationContract(j).ValueTransportingRelief
                IncentivePayment.Contract = employeeLiquidationContract(j).Contract
            Next

            IncentivePaymentValue = (AverageIBCIncentivePaymentValue * WorkDays) / 360

            IncentivePayment.GroupId = GroupId
            IncentivePayment.PaymentType = PaymentType
            IncentivePayment.Period = Period
            IncentivePayment.PeriodInitialDate = IncentiveStarDate
            IncentivePayment.PeriodEndDate = IncentiveEndDate
            IncentivePayment.ContractId = contractId
            IncentivePayment.ContractInitialDate = ContractInitialDate
            IncentivePayment.BasicSalary = BasicSalary
            IncentivePayment.TransportHelpValue = ValueTransportingRelief
            IncentivePayment.AverageSalary = AverageIBCIncentivePaymentValue
            IncentivePayment.SanctionDays = SanctionDays
            IncentivePayment.WorkingDays = WorkDays
            IncentivePayment.PayrollNextDate = PayrollNextDate
            IncentivePayment.PaidDays = WorkDays - SanctionDays
            IncentivePayment.PaidValue = _LiquidationDomain.RoundedValues(IncentivePaymentValue)

            IncentivePaymentList.Add(IncentivePayment)

            AverageIBCIncentivePaymentValue = 0
            SanctionDays = 0
        Next

        actionResult.ObjectEmbbeded = IncentivePaymentList

        Return actionResult

    End Function

End Class
