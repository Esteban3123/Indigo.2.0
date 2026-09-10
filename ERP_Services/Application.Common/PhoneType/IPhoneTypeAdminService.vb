'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 26-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Public Interface IPhoneTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los tipos de niveles
    ''' </summary>
    ''' <returns>Lista de tipos de niveles</returns>
    ''' <remarks></remarks>
    Function ListAllPhoneType() As List(Of PhoneType)

    ''' <summary>
    ''' Obtiene un tipo de codigo especifico
    ''' </summary>
    ''' <param name="code">Codigo del tipo de telefono</param>
    ''' <returns>Tipo de telefono</returns>
    ''' <remarks></remarks>
    Function GetPhoneType(ByVal code As String) As PhoneType

    ''' <summary>
    ''' Graba o actualiza un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SavePhoneType(ByVal phoneType As PhoneType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePhoneType(ByVal phoneType As PhoneType, ByVal audit As AuditMessage) As ActionMessageResult(Of PhoneType)

End Interface
