#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class Recognition

#Region "Properties"

    ''' <summary>
    ''' Unidad operativa
    ''' </summary>
    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Nit y nombre del tercer
    ''' </summary>
    <DataMember()>
    Public Property NameThirdParty As String

    ''' <summary>
    ''' Código y nombre de la dependencia
    ''' </summary>
    <DataMember()>
    Public Property NameDependency As String

#End Region

End Class

