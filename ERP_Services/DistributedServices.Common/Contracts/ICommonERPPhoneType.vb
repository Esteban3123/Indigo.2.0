'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 26-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPPhoneType

    ''' <summary>
    ''' Lista todos los tipos de niveles
    ''' </summary>
    ''' <returns>Lista de tipos de niveles</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllPhoneType(session As SessionValues) As List(Of PhoneType)

    ''' <summary>
    ''' Obtiene un tipo de codigo especifico
    ''' </summary>
    ''' <param name="code">Codigo del tipo de telefono</param>
    ''' <returns>Tipo de telefono</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetPhoneType(ByVal code As String, session As SessionValues) As PhoneType

    ''' <summary>
    ''' Graba o actualiza un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SavePhoneType(ByVal phoneType As PhoneType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeletePhoneType(ByVal phoneType As PhoneType, session As SessionValues) As ActionMessageResult(Of PhoneType)

End Interface
