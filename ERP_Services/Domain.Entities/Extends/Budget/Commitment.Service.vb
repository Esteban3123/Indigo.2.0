#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class Commitment

#Region "Properties"

    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Nit y nombre del tercer
    ''' </summary>
    <DataMember()>
    Public Property NameThirdParty As String

#End Region

End Class
