'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Generated
' Created          : 2024
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.Threading.Tasks

#End Region

<ServiceContract()>
Public Interface IBillingServiceRIPSSupportRecord

#Region "Methods"

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su identificador
    ''' </summary>
    ''' <param name="id">Identificador del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    <OperationContract()>
    Function GetRIPSSupportRecordByIdAsync(id As Integer) As Task(Of RIPSSupportRecord)

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su código
    ''' </summary>
    ''' <param name="code">Código del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    <OperationContract()>
    Function GetRIPSSupportRecordByCodeAsync(code As String) As Task(Of RIPSSupportRecord)

    ''' <summary>
    ''' Crea un nuevo registro de soporte RIPS
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS</param>
    ''' <param name="operatingUnitId">ID de la unidad operativa</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequence">Identificador de la secuencia numérica para generar el código</param>
    ''' <returns>Resultado de la operación</returns>
    <OperationContract()>
    Function NewRIPSSupportRecordAsync(supportRecord As RIPSSupportRecord, operatingUnitId As Integer, audit As AuditMessage, Optional idSequence As Long = 0) As Task(Of ActionResult(Of RIPSSupportRecord))

    ''' <summary>
    ''' Confirma un registro de soporte RIPS cambiando su estado a Confirmed (1).
    ''' Una vez confirmado, el registro no se puede modificar, solo anular y crear uno nuevo.
    ''' </summary>
    ''' <param name="id">Identificador del registro de soporte RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult con el registro actualizado o mensaje de error</returns>
    <OperationContract()>
    Function ConfirmRIPSSupportRecordAsync(id As Integer, audit As AuditMessage) As Task(Of ActionResult(Of RIPSSupportRecord))

    ''' <summary>
    ''' Lista todos los registros de soporte RIPS
    ''' </summary>
    ''' <returns>Lista de registros de soporte RIPS</returns>
    <OperationContract()>
    Function ListAllRIPSSupportRecordsAsync() As Task(Of List(Of RIPSSupportRecord))

#End Region

End Interface

