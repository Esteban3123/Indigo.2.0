'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPTimeUnit

    ''' <summary>
    ''' Lista Todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns>Unidades de Tiempo</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllTimeUnit(session As SessionValues) As List(Of TimeUnit)

    ''' <summary>
    ''' Obtiene una Unidad de Tiempo
    ''' </summary>
    ''' <param name="code">Código de la Unidad de Tiempo</param>
    ''' <returns>Unidad de Tiempo</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetTimeUnit(ByVal code As String, session As SessionValues) As TimeUnit

    ''' <summary>
    ''' Almacena o Actualiza una Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveTimeUnit(ByVal timeUnit As TimeUnit, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina una Unidad de Tiempo
    ''' </summary>
    ''' <param name="timeUnit">Unidad de Tiempo</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteTimeUnit(ByVal timeUnit As TimeUnit, session As SessionValues) As ActionMessageResult(Of TimeUnit)



End Interface
