Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Partial Public Class ResultSearchObject

#Region "Properties"

    <DataMember()>
    Public Property idTipoDespacho As Integer

    <DataMember()>
    Public Property recetarioOMedica As String

    <DataMember()>
    Public Property tipoIdenPaciente As Integer

    <DataMember()>
    Public Property identPaciente As String

    <DataMember()>
    Public Property altoCosto As Boolean

    <DataMember()>
    Public Property fechaRecetarioOMedica As DateTime

#End Region

End Class