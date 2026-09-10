
Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class AccountManagementSequenceDetail
    Inherits Entity(Of AccountManagementSequenceDetail)

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitName As String

    <DataMember()>
    Public Property PatternName As String

#End Region

End Class

