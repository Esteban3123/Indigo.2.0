Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Partial Public Class ConfirmationObject

#Region "Properties"

    <DataMember()>
    Public Property idOperador As Integer

    <DataMember()>
    Public Property idTipoDespacho As Integer

    <DataMember()>
    Public Property tipoMovimiento As String

    <DataMember()>
    Public Property recetarioOMedica As String

    <DataMember()>
    Public Property idProducto As Integer?

    <DataMember()>
    Public Property idLote As Integer?

    <DataMember()>
    Public Property alfaNumericoLote As String

    <DataMember()>
    Public Property cantidad As Decimal

    <DataMember()>
    Public Property cantidadAEntregar As Decimal

    <DataMember()>
    Public Property idConfirmacion As Integer

    <DataMember()>
    Public Property mensaje As String

#End Region

End Class