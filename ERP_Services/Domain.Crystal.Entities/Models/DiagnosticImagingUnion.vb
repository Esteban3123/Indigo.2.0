Imports System.Runtime.Serialization
Imports Domain.Base.Entities

<DataContract>
Public Class DiagnosticImagingUnion
    ''' <summary>
    ''' Identificacion del paciente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property IPCODPACI As String
    ''' <summary>
    ''' numero de ingreso del paciente
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property NUMINGRES As String
    ''' <summary>
    ''' Medico que realizo la lectura de la imagen diagnostica (Codigo del profesional)
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property MEDREALEC As String
    ''' <summary>
    ''' codigo del cups
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CODSERIPS As String
    ''' <summary>
    ''' nit del medico
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property CODIGONIT As String
    ''' <summary>
    ''' Id de la tabla tercero del medico
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property ThirdPartyId As Integer?
End Class
