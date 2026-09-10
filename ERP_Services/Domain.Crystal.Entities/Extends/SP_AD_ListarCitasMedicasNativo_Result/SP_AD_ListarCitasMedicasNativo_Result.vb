Imports System.Runtime.Serialization

Partial Public Class SP_AD_ListarCitasMedicasNativo_Result
    <DataMember()>
    Public Property InvoiceId As Integer?
    <DataMember()>
    Public Property InvoiceNumber As String
    <DataMember()>
    Public Property AdmissionNumberInvoice As String
    ''' <summary>
    ''' Indica que el registro tiene un id falso
    ''' </summary>
    <DataMember()>
    Property IsFalseId As Boolean
    <DataMember()>
    Property CantidadServicio As Integer = 1
    <DataMember()>
    Property HealthAdministratorIdInvoice As Integer?
    <DataMember()>
    Property CareGroupIdInvoice As Integer?

    ''' <summary>
    ''' tipo de actividad , tipo solicitud
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property ActivityType As Integer

    ''' <summary>
    ''' id de la sala
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property IDSALA As Integer

    ''' <summary>
    ''' Codigo de la Actividad Medica
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property CODACTMED As String

    ''' <summary>
    ''' propiedad extendida que almacena el tipo de servicio solo se llena si el tipo de actividad
    ''' es de tipo apoyo diagnostico
    ''' 1-"Laboratorios"; 2-"Imágenes Diagnostico"; 3- "Otros Procedimientos."
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property ServiceType As Integer?

    ''' <summary>
    ''' Codigo temporal el cual almacenara el cod de agascecita 
    ''' para identifica un codigo ya creado de un autogenerado en presentacion
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property CodeTmp As Integer

    ''' <summary>
    ''' Tipo de actividad de agendamiento 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property ACTIVICON As Integer?

End Class
