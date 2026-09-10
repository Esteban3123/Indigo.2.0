'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan F. Tamayo Puertas
' Created          : 2013-07-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz de servicio de parametros de tiempo
''' </summary>
Public Interface ITimeParametersAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un conjunto de parametros de tiempo por su numero de Id
    ''' </summary>
    ''' <param name="Id">Id del conjunto de parametros</param>
    ''' <returns>El conjunto de parametros</returns>
    Function GetTimeParameters(Id As String) As TimeParameters

    ''' <summary>
    ''' Obtiene el primero o por defecto conjunto de parametros de tiempo
    ''' </summary>
    ''' <returns>El conjunto de parametros</returns>
    Function GetTimeParametersSingleOrDefault(ByVal _IdIOperatingUnit As Integer, ByVal Entity As String) As TimeParameters

    ''' <summary>
    ''' Lista todos los conjuntos de parametros de tiempo
    ''' </summary>
    ''' <returns>Lista de conjuntos de parametros</returns>
    Function ListAllTimeParameters(ByVal _IdIOperatingUnit As Integer, Optional Control As String = "") As List(Of TimeParameters)

    ''' <summary>
    ''' Elimina un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Conjunto de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    Function DeleteTimeParameters(ByVal obj As TimeParameters, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Graba o actualiza un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Conjunto de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    Function SaveTimeParameters(ByVal obj As TimeParameters, ByVal audit As AuditMessage) As ActionResult(Of TimeParameters)
    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    Function ListControlParametersTime(Invoice As String, Entity As String, ByVal _IdIOperatingUnit As Integer) As List(Of ControlParametersTime)
    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    Function ListControlParametersTimeMassive(controlParameter As TimeParameters, Invoice As String, listGlosas As List(Of TrazabilityParametersTime), listReiterations As List(Of TrazabilityParametersTime), listConciliations As List(Of TrazabilityParametersTime)) As List(Of ControlParametersTime)
    ''' <summary>
    ''' Graba o actualiza una lista de parametros de tiempo
    ''' </summary>
    ''' <param name="listParameters">Lista de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    Function SaveTimeListParameters(listParameters As List(Of TimeParameters), ByVal listConceptGloss As List(Of Domain.Entities.ConceptGlosas), ByVal Session As SessionValues) As ActionResult(Of List(Of TimeParameters))

End Interface
