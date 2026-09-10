Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class BudgetSequenceDetail
    Inherits Entity(Of BudgetSequenceDetail)

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitName As String

    <DataMember()>
    Public Property PatternName As String

#End Region

End Class
