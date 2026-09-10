Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class MixingStationSequenceDetail
    Inherits Entity(Of MixingStationSequenceDetail)

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitName As String

    <DataMember()>
    Public Property PatternName As String

#End Region

End Class
