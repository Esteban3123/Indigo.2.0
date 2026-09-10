Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Partial Public Class WebServiceObject

#Region "Properties"

    <DataMember()>
    Public Property paciente As PacienteObject

    <DataMember()>
    Public Property solicitudDetalle As List(Of SolicitudDetalleObject)

    <DataMember()>
    Public Property JsonSolicitud As String

#End Region

End Class
