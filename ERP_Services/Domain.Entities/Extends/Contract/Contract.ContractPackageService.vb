Imports System.Runtime.Serialization

Partial Public Class ContractPackageService

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre y codigo del CUPS
    ''' </summary>
    <DataMember()>
    Public Property CupsCodeName As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la descripcion relacionada
    ''' </summary>
    <DataMember()>
    Public Property ContractDescriptionName As String

    ''' <summary>
    ''' Obtiene o establece el Valor total unitario de un detalle
    ''' </summary>
    <DataMember()>
    Public Property UnitValueTotal As Decimal

    <DataMember()>
    Public Property Percentage As Decimal

#End Region

End Class
