'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan F. Tamayo Puertas
' Created          : 2013-07-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Partial Class GlosasService

    ''' <summary>
    ''' Obtiene un conjunto de parametros de tiempo por su numero de Id
    ''' </summary>
    ''' <param name="Id">Id del conjunto de parametros</param>
    ''' <returns>El conjunto de parametros</returns>
    Public Function GetTimeParameters(Id As String, session As SessionValues) As TimeParameters Implements IGlosasTimeParameters.GetTimeParameters
        Using timeParameters As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
            Return timeParameters.GetTimeParameters(Id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el primero o por defecto conjunto de parametros de tiempo
    ''' </summary>
    ''' <returns>El conjunto de parametros</returns>
    Public Function GetTimeParametersSingleOrDefault(Entity As String, session As SessionValues, _IdIOperatingUnit As Integer) As TimeParameters Implements IGlosasTimeParameters.GetTimeParametersSingleOrDefault
        Using timeParameters As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
            Return timeParameters.GetTimeParametersSingleOrDefault(_IdIOperatingUnit, Entity)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los conjuntos de parametros de tiempo
    ''' </summary>
    ''' <returns>Lista de conjuntos de parametros</returns>
    Public Function ListAllTimeParameters(session As SessionValues, _IdIOperatingUnit As Integer, Optional Control As String = "") As List(Of TimeParameters) Implements IGlosasTimeParameters.ListAllTimeParameters
        Using timeParameters As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
            Return timeParameters.ListAllTimeParameters(_IdIOperatingUnit, Control)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    Function ListControlParametersTime(Invoice As String, Entity As String, session As SessionValues, _IdIOperatingUnit As Integer) As List(Of ControlParametersTime) Implements IGlosasTimeParameters.ListControlParametersTime
        Using timeParameters As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
            Return timeParameters.ListControlParametersTime(Invoice, Entity, _IdIOperatingUnit)
        End Using
    End Function

    ''' <summary>
    ''' Graba o actualiza un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Conjunto de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    Public Function SaveTimeParameters(obj As TimeParameters, session As SessionValues) As Domain.Base.Entities.ActionResult(Of TimeParameters) Implements IGlosasTimeParameters.SaveTimeParameters
        Using timeParameters As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
            Return timeParameters.SaveTimeParameters(obj, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Graba o actualiza una lista de parametros de tiempo
    ''' </summary>
    ''' <param name="ListParameters">Lista de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    Public Function SaveListTimeParameters(ListParameters As List(Of TimeParameters), ByVal listConceptGloss As List(Of Domain.Entities.ConceptGlosas), session As SessionValues) As Domain.Base.Entities.ActionResult(Of List(Of TimeParameters)) Implements IGlosasTimeParameters.SaveListTimeParameters
        Using timeParameters As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
            Return timeParameters.SaveTimeListParameters(ListParameters, listConceptGloss, session)
        End Using
    End Function

    ''' <summary>
    ''' Eliminar un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Parametros de Tiempo</param>
    ''' <param name="session">Objeto Session</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteTimeParameters(obj As TimeParameters, session As SessionValues) As ActionResult Implements IGlosasTimeParameters.DeleteTimeParameters
        Using timeParameters As ITimeParametersAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITimeParametersAdminService)()
            Return timeParameters.DeleteTimeParameters(obj, session.AuditMessageWcf)
        End Using
    End Function

End Class
