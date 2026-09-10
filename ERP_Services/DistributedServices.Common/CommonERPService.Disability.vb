'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class CommonERPService

    ''' <summary>
    ''' Elimina una discapacidad
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteDisability(disability As Disability, session As SessionValues) As ActionMessageResult(Of Disability) Implements ICommonERPDisability.DeleteDisability
        Using disabilityAdmin As IDisabilityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDisabilityAdminService)()
            Return disabilityAdmin.DeleteDisability(disability, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Discapacidad especifica
    ''' </summary>
    ''' <param name="code">Codigo Discapacidad</param>
    ''' <returns>Discapacidad</returns>
    ''' <remarks></remarks>
    Public Function GetDisability(code As String, session As SessionValues) As Disability Implements ICommonERPDisability.GetDisability
        Using disabilityAdmin As IDisabilityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDisabilityAdminService)()
            Return disabilityAdmin.GetDisability(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las Discapacidades
    ''' </summary>
    ''' <returns>Lista de Discapacidades</returns>
    ''' <remarks></remarks>
    Public Function ListAllDisability(session As SessionValues) As List(Of Disability) Implements ICommonERPDisability.ListAllDisability
        Using disabilityAdmin As IDisabilityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDisabilityAdminService)()
            Return disabilityAdmin.ListAllDisability()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza una Discapacidad
    ''' </summary>
    ''' <param name="disability">Discapacidad</param>
    ''' <param name="audit">Objeto de Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveDisability(disability As Disability, session As SessionValues) As Boolean Implements ICommonERPDisability.SaveDisability
        Using disabilityAdmin As IDisabilityAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDisabilityAdminService)()
            Return disabilityAdmin.SaveDisability(disability, session.AuditMessageWcf)
        End Using
    End Function
End Class
