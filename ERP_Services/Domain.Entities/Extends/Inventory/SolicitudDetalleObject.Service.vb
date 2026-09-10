Imports System.Runtime.Serialization

Partial Public Class SolicitudDetalleObject

#Region "Properties"

    <DataMember()>
    Public Property recetarioOMedica As Integer

    <DataMember()>
    Public Property fechaRecetarioOMedica As String

    <DataMember()>
    Public Property idAutorizacion As Integer

    <DataMember()>
    Public Property IdTipoDespacho As Integer

    <DataMember()>
    Public Property tipoDespacho As String

    <DataMember()>
    Public Property idIngreso As Integer

    <DataMember()>
    Public Property diagnosticoCIE10 As String

    <DataMember()>
    Public Property descripcionDiagnosticoCIE10 As String

    <DataMember()>
    Public Property idIPSMinSalud As String

    <DataMember()>
    Public Property ipsNombre As String

    <DataMember()>
    Public Property fechaVencimiento As String

    <DataMember()>
    Public Property valorAPagar As Decimal

    <DataMember()>
    Public Property UnidadFuncional As String

    <DataMember()>
    Public Property idProducto As Integer

    <DataMember()>
    Public Property codigoProducto As String

    <DataMember()>
    Public Property nombreProducto As String

    <DataMember()>
    Public Property altoCosto As Boolean

    <DataMember()>
    Public Property pos As Boolean

    <DataMember()>
    Public Property cantidad As Decimal

    <DataMember()>
    Public Property idUnidadMedida As Integer

    <DataMember()>
    Public Property unidadMedida As String

    <DataMember()>
    Public Property posologia As String

    <DataMember()>
    Public Property idProfesional As Integer

    <DataMember()>
    Public Property tipoProducto As String

    <DataMember()>
    Public Property periodicidad As String

    <DataMember()>
    Public Property unidosis As String

    <DataMember()>
    Public Property unidadMedidaDosis As String

    <DataMember()>
    Public Property profesional As ProfesionalObject

#End Region

End Class
