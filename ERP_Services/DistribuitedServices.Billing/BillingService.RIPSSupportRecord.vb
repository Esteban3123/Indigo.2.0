'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Generated
' Created          : 2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports DistribuitedServices.Billing
Imports Domain.Entities
Imports System.Threading.Tasks

#End Region

Partial Class BillingService
    Implements IBillingServiceRIPSSupportRecord

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su identificador
    ''' </summary>
    ''' <param name="id">Identificador del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    Public Async Function GetRIPSSupportRecordByIdAsync(id As Integer) As Task(Of RIPSSupportRecord) Implements IBillingServiceRIPSSupportRecord.GetRIPSSupportRecordByIdAsync
        Using service As IRIPSSupportRecordAdminService = Container.Current.Resolve(Of IRIPSSupportRecordAdminService)()
            Return Await service.GetRIPSSupportRecordByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su código
    ''' </summary>
    ''' <param name="code">Código del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    Public Async Function GetRIPSSupportRecordByCodeAsync(code As String) As Task(Of RIPSSupportRecord) Implements IBillingServiceRIPSSupportRecord.GetRIPSSupportRecordByCodeAsync
        Using service As IRIPSSupportRecordAdminService = Container.Current.Resolve(Of IRIPSSupportRecordAdminService)()
            Return Await service.GetRIPSSupportRecordByCodeAsync(code)
        End Using
    End Function

    ''' <summary>
    ''' Crea un nuevo registro de soporte RIPS
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS</param>
    ''' <param name="operatingUnitId">ID de la unidad operativa</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequence">Identificador de la secuencia numérica para generar el código</param>
    ''' <returns>Resultado de la operación</returns>
    Public Async Function NewRIPSSupportRecordAsync(supportRecord As RIPSSupportRecord, operatingUnitId As Integer, audit As AuditMessage, Optional idSequence As Long = 0) As Task(Of ActionResult(Of RIPSSupportRecord)) Implements IBillingServiceRIPSSupportRecord.NewRIPSSupportRecordAsync
        Using service As IRIPSSupportRecordAdminService = Container.Current.Resolve(Of IRIPSSupportRecordAdminService)()
            Return Await service.NewRIPSSupportRecordAsync(supportRecord, operatingUnitId, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Confirma un registro de soporte RIPS cambiando su estado a Confirmed (1).
    ''' Una vez confirmado, el registro no se puede modificar, solo anular y crear uno nuevo.
    ''' </summary>
    ''' <param name="id">Identificador del registro de soporte RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult con el registro actualizado o mensaje de error</returns>
    Public Async Function ConfirmRIPSSupportRecordAsync(id As Integer, audit As AuditMessage) As Task(Of ActionResult(Of RIPSSupportRecord)) Implements IBillingServiceRIPSSupportRecord.ConfirmRIPSSupportRecordAsync
        Using service As IRIPSSupportRecordAdminService = Container.Current.Resolve(Of IRIPSSupportRecordAdminService)()
            Return Await service.ConfirmRIPSSupportRecordAsync(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los registros de soporte RIPS
    ''' </summary>
    ''' <returns>Lista de registros de soporte RIPS</returns>
    Public Async Function ListAllRIPSSupportRecordsAsync() As Task(Of List(Of RIPSSupportRecord)) Implements IBillingServiceRIPSSupportRecord.ListAllRIPSSupportRecordsAsync
        Using service As IRIPSSupportRecordAdminService = Container.Current.Resolve(Of IRIPSSupportRecordAdminService)()
            Return Await service.ListAllRIPSSupportRecordsAsync()
        End Using
    End Function

End Class

