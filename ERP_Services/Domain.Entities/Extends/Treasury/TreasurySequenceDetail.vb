Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class TreasurySequenceDetail
    Inherits Entity(Of TreasurySequenceDetail)

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitName As String

    <DataMember()>
    Public Property TypeName As String

    <DataMember()>
    Public Property PatternName As String

#End Region

End Class