'***********************************************************************
' Assembly         : Infraestructura.Base.MensajeAuditoria
' Author           : Walter Sierra
' Created          : 08/04-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization

''' <summary>
''' clase con los datos formados en el mensaje de los servicios WCF para llevar la auditoria
''' </summary>
<DataContract()>
Public Class AuditMessage
    ''' <summary>
    ''' el id del usuario.
    ''' </summary>
    ''' <value>el id del usuario.</value>
    <DataMember>
    Property IdUser As Integer
    ''' <summary>
    ''' el codigo usuario.
    ''' </summary>
    ''' <value>el codigo usuario.</value>
    <DataMember>
    Property CodeUser As String
    ''' <summary>
    ''' el nombre del PC en donde se ejecuto la aplicacion.
    ''' </summary>
    ''' <value>el nombre del PC.</value>
    <DataMember>
    Property ComputerName As String
    ''' <summary>
    ''' obtiene el usuario windows que ejecuto la aplicacion.
    ''' </summary>
    ''' <value>el usuario windows.</value>
    <DataMember>
    Property WindowsUser As String
    ''' <summary>
    ''' obtiene el tag o el nombre del funcional que consume los servicios
    ''' </summary>
    ''' <value>el funcional.</value>
    <DataMember>
    Property Functional As String
    ''' <summary>
    ''' obtiene el código de la compañia
    ''' </summary>
    ''' <value>el código de la compañia.</value>
    <DataMember>
    Property Company As String
    ''' <summary>
    ''' obtiene el tipo de la compañia - 1 - privada - 2 - Publica
    ''' </summary>
    ''' <value>el código de la compañia.</value>
    <DataMember>
    Property CompanyType As Integer
    ''' <summary>
    ''' Nombre del usuario
    ''' </summary>
    <DataMember>
    Property NameUser As String
    ''' <summary>
    ''' Contenedor Seguridad
    ''' </summary>
    <DataMember>
    Property ContainerSecurity As String
    ''' <summary>
    ''' Especifica si existe integracion de dispensacion (1- Integracion Nativa 2- Integracion con Heon)
    ''' </summary>
    <DataMember>
    Property DispensingIntegration As Byte

End Class
