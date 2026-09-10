Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Partial Public Class SolicitudObject

#Region "Properties"

    <DataMember()>
    Public Property idPaciente As Integer

    <DataMember()>
    Public Property idProfesional As Integer

    <DataMember()>
    Public Property solicitudDetalle As List(Of SolicitudDetalleObject)

    <DataMember()>
    Public Property profesional As ProfesionalObject

    <DataMember()>
    Public Property paciente As PacienteObject

#End Region

End Class
