Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class BillingSequenceDetail
    Inherits Entity(Of BillingSequenceDetail)

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitName As String

    <DataMember()>
    Public Property PatternName As String

#End Region

End Class
