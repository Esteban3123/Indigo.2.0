Imports System.Runtime.Serialization

Partial Public Class ContractPackageProduct

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre y codigo del CUPS
    ''' </summary>
    <DataMember()>
    Public Property ProductCodName As String

    ''' <summary>
    ''' Obtiene o establece el porcentaje de un detalle
    ''' </summary>
    Public Property Percentage As Decimal

    ''' <summary>
    ''' Obtiene o establece el Valor total unitario de un detalle
    ''' </summary>
    <DataMember()>
    Public Property UnitValueTotal As Decimal


#End Region

End Class
