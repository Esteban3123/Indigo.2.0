Imports System.Runtime.Serialization
Public Class ExemptIncome
    <DataMember>
    Property YearLiquidated As Integer

    <DataMember>
    Property RegisterStatus As String
    <DataMember>
    Property IsNew As Boolean = 0
End Class
