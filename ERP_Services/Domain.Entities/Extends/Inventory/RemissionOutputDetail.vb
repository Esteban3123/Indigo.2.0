Imports System.Runtime.Serialization
Partial Public Class RemissionOutputDetail
    <DataMember>
    Property CodeNameProduct As String

    ''' <summary>
    ''' propiedad que contiene la unidad de consumo del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property consumptionUnit As String
End Class
