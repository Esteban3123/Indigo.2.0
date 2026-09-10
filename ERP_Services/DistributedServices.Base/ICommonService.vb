'***********************************************************************
' Assembly         : DistributedService.Base
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-03-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports System.Runtime.Serialization

#End Region

''' <summary>
''' Interface con los metodos comunes en el sistema, como traer la hora del servidor
''' </summary>
<ServiceContract()> _
Public Interface ICommonService

    ''' <summary>
    ''' Obtener la fecha/hora del servidor
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetServerDate() As DateTime

    ''' <summary>
    ''' Obtener la fecha/hora del servidor con un formato especifico
    ''' </summary>
    ''' <param name="format">el formato basado en una enumeracion</param>
    ''' <example>ShortDatetime</example>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCustomDateFormatDate(format As FormatDate) As String

    ''' <summary>
    ''' Obtener la fecha/hora del servidor con un formato especifico
    ''' </summary>
    ''' <param name="format">el formato en cadena de string ( http://msdn.microsoft.com/es-co/library/8kb3ddd4(v=VS.95).aspx )</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCustomDateStringFormat(format As String) As String

    ' ''' <summary>
    ' ''' Consulta los campos nulos para una tabla en un esquema especifico
    ' ''' </summary>
    ' ''' <param name="schema">Esquema</param>
    ' ''' <param name="nameTable">Nombre de la tabla</param>
    ' ''' <returns>DataSet con el conjunto de campos que son null</returns>
    ' ''' <remarks></remarks>
    '<OperationContract()> _
    'Function GetFieldsNULL(company As String, schema As String, nameTable As String) As DataSet

    ''' <summary>
    ''' Obtiene la credencial de autenticación usada por los servicios de notificación
    ''' </summary>
    ''' <returns>Credencial</returns>
    <OperationContract>
    Function GetNotificationServiceCredential() As CredentialBasic

End Interface

''' <summary>
''' Encapsula los datos básicos de una credencial de autenticación, como el nombre de usuario y contraseña
''' </summary>
''' <remarks>Usada básicamente para enviar la credencial de autenticación usada en el servicio de notificación</remarks>
<DataContract()>
Public NotInheritable Class CredentialBasic

    ''' <summary>
    ''' Obtiene o asigna el nombre de usuario
    ''' </summary>
    ''' <value>Nombre de usuario</value>
    ''' <returns>El nombre de usuario</returns>
    <DataMember()>
    Public Property User As String

    ''' <summary>
    ''' Obtiene o asigna la contraseña del usuario
    ''' </summary>
    ''' <value>Contraseña del usuario</value>
    ''' <returns>La contraseña del usuario</returns>
    <DataMember()>
    Public Property Passwd As String

End Class