#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class PaymentOrder

#Region "Properties"

    ''' <summary>
    ''' Unidad operativa usada para obtener la secuencia numerica si es el caso
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Nit y nombre del tercer
    ''' </summary>
    <DataMember()>
    Public Property NameThirdParty As String

#End Region

End Class
