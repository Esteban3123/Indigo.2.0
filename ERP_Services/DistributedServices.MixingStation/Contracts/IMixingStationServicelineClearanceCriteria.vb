'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Andres Alarcon
' Created          : 21/02/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IMixingStationServicelineClearanceCriteria

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="_lineClearanceCriteria">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SaveLineClearanceCriteria(ByVal _lineClearanceCriteria As LineClearanceCriteria, idSequence As Int64, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria)

    ''' <summary>
    ''' Elimina un criterio de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function DeleteLineClearanceCriteria(ByVal _lineClearanceCriteria As LineClearanceCriteria, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza un criterio de despeje de linea
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function UpdateLineClearanceCriteria(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria)

    ''' <summary>
    ''' Obtiene un criterio de despeje de linea mediante el codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLineClearanceCriteriaByCode(ByVal code As String, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria)

End Interface
