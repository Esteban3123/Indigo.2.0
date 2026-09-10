'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Common
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

Partial Class CommonERPService

    Implements ICommonERPTimeUnit

    ''' <summary>
    ''' Elimina una Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteTimeUnit(timeUnit As Domain.Entities.TimeUnit, session As SessionValues) As ActionMessageResult(Of TimeUnit) Implements ICommonERPTimeUnit.DeleteTimeUnit
        Using timeUnitAdminService As ITimeUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeUnitAdminService)()
            Return timeUnitAdminService.DeleteTimeUnit(timeUnit, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Unidad de Tiempo
    ''' </summary>
    ''' <param name="code">Código de la Unidad de Tiempo</param>
    ''' <returns>Unidad de Tiempo</returns>
    ''' <remarks></remarks>
    Public Function GetTimeUnit(code As String, session As SessionValues) As Domain.Entities.TimeUnit Implements ICommonERPTimeUnit.GetTimeUnit
        Using timeUnitAdminService As ITimeUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeUnitAdminService)()
            Return timeUnitAdminService.GetTimeUnit(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns>Unidades de Tiempo</returns>
    ''' <remarks></remarks>
    Public Function ListAllTimeUnit(session As SessionValues) As List(Of Domain.Entities.TimeUnit) Implements ICommonERPTimeUnit.ListAllTimeUnit
        Using timeUnitAdminService As ITimeUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeUnitAdminService)()
            Return timeUnitAdminService.ListAllTimeUnit()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveTimeUnit(timeUnit As Domain.Entities.TimeUnit, session As SessionValues) As Boolean Implements ICommonERPTimeUnit.SaveTimeUnit
        Using timeUnitAdminService As ITimeUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeUnitAdminService)()
            Return timeUnitAdminService.SaveTimeUnit(timeUnit, session.AuditMessageWcf)
        End Using
    End Function
End Class
