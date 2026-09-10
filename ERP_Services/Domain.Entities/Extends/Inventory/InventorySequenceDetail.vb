Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class InventorySequenceDetail
    Inherits Entity(Of InventorySequenceDetail)

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitName As String

    <DataMember()>
    Public Property TypeName As String

    <DataMember()>
    Public Property PatternName As String

#End Region

End Class
