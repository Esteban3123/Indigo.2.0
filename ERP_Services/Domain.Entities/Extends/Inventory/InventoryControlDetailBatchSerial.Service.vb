Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

Partial Public Class InventoryControlDetailBatchSerial

    ''' <summary>
    ''' Descripción del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductCodeName As String

    ''' <summary>
    ''' Descripción del producto
    ''' </summary>
    <DataMember()>
    Public Property BatchSerialCode As String

    ''' <summary>
    ''' Descripción del producto
    ''' </summary>
    <DataMember()>
    Public Property StatusName As String

    ''' <summary>
    ''' define si fue seleccionado
    ''' </summary>
    <DataMember()>
    Public Property Selected As Boolean

End Class
