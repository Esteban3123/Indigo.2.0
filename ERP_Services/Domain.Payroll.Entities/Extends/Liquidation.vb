Imports System.Runtime.Serialization

Partial Public Class Liquidation

    <DataMember()>
    Property OperatingUnitId As Integer

    <DataMember()>
    Property FullNameEmployee As String

    <DataMember()>
    Property NitEmployee As String

    <DataMember()>
    Property TaxBase As Double

    <DataMember()>
    Property AFCAccount As Decimal

    <DataMember()>
    Property TotalExemptIncomeandDeductions As Decimal

    <DataMember()>
    Property ExemptIncome As Decimal

    <DataMember()>
    Property ExemptIncomeControl As Decimal

    <DataMember()>
    Property TotalExemptIncomeandDeductionsControl As Decimal

    <DataMember()>
    Property Subtotal As Decimal

    <DataMember()>
    Property CalamityDays As Decimal

    <DataMember()>
    Property PaidLeaveDays As Decimal
    <DataMember()>
    Property VacationDaysInCash As Decimal

    <DataMember()>
    Property EmployerProfessionalDisabilityDays As Decimal
    <DataMember()>
    Property ERPProfessionalDisabilityDays As Decimal

    Public ReadOnly Property ErrorLiquidation() As Nullable(Of Boolean)
        Get
            If Me.Message.Any(Function(x) x.Error = True) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

End Class
