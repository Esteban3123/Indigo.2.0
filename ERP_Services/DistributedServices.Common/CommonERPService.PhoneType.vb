'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 26-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Common
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

Partial Class CommonERPService

    ''' <summary>
    ''' Elimina un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePhoneType(phoneType As Domain.Entities.PhoneType, session As SessionValues) As ActionMessageResult(Of PhoneType) Implements ICommonERPPhoneType.DeletePhoneType
        Using phoneTypeAdmin As IPhoneTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPhoneTypeAdminService)()
            Return phoneTypeAdmin.DeletePhoneType(phoneType, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de codigo especifico
    ''' </summary>
    ''' <param name="code">Codigo del tipo de telefono</param>
    ''' <returns>Tipo de telefono</returns>
    ''' <remarks></remarks>
    Public Function GetPhoneType(code As String, session As SessionValues) As Domain.Entities.PhoneType Implements ICommonERPPhoneType.GetPhoneType
        Using phoneTypeAdmin As IPhoneTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPhoneTypeAdminService)()
            Return phoneTypeAdmin.GetPhoneType(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los tipos de niveles
    ''' </summary>
    ''' <returns>Lista de tipos de niveles</returns>
    ''' <remarks></remarks>
    Public Function ListAllPhoneType(session As SessionValues) As List(Of Domain.Entities.PhoneType) Implements ICommonERPPhoneType.ListAllPhoneType
        Using phoneTypeAdmin As IPhoneTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPhoneTypeAdminService)()
            Return phoneTypeAdmin.ListAllPhoneType()
        End Using
    End Function

    ''' <summary>
    ''' Graba o actualiza un tipo de telefono
    ''' </summary>
    ''' <param name="phoneType">Tipo de telefono</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SavePhoneType(phoneType As Domain.Entities.PhoneType, session As SessionValues) As Boolean Implements ICommonERPPhoneType.SavePhoneType
        Using phoneTypeAdmin As IPhoneTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPhoneTypeAdminService)()
            Return phoneTypeAdmin.SavePhoneType(phoneType, session.AuditMessageWcf)
        End Using
    End Function
End Class
