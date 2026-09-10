Imports System.Runtime.Serialization

Partial Public Class PacienteObject

#Region "Properties"

    <DataMember()>
    Public Property idPaciente As Integer

    <DataMember()>
    Public Property tipoIdenPaciente As Integer

    <DataMember()>
    Public Property identPaciente As String

    <DataMember()>
    Public Property priNombrePaciente As String

    <DataMember()>
    Public Property segNombrePaciente As String

    <DataMember()>
    Public Property priApellidoPaciente As String

    <DataMember()>
    Public Property segApellidoPaciente As String

    <DataMember()>
    Public Property fechaNacimiento As String

    <DataMember()>
    Public Property genero As String

    <DataMember()>
    Public Property telefono1 As String

    <DataMember()>
    Public Property direccionPaciente As String

    <DataMember()>
    Public Property telefono2 As String

    <DataMember()>
    Public Property eMailPaciente As String

    <DataMember()>
    Public Property idAseguradorPaciente As String

    <DataMember()>
    Public Property nombreAseguradorPaciente As String

    <DataMember()>
    Public Property idTipoAfiliado As Integer

    <DataMember()>
    Public Property tipoAfiliado As String

#End Region

End Class
