Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class GeneralLedgerSequenceDetail
    Inherits Entity(Of GeneralLedgerSequenceDetail)

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitName As String

    <DataMember()>
    Public Property PatternName As String

#End Region

End Class
