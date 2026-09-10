'***********************************************************************
' Assembly         : DistributedServices.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IMedicalFeesMedicalFeesCausation

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMedicalFeesCausation(ListMedicalFeesCausation As List(Of Domain.Entities.MedicalFeesCausation), ByVal ListMedicalFeesNotes As List(Of MedicalFeesNote), ByVal ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.MedicalFeesCausation))

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMedicalFeesCausation(ListInfo As List(Of Tuple(Of Integer, Integer)), Company As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer, Integer)))

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMedicalFeesCausationFromRemoveFees(MedicalFeesCausationId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicalFeesCausationById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesCausation)

    ''' <summary>
    ''' Calcula el valor a causar
    ''' </summary>
    ''' <param name="CupsEntityId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="RateManualId"></param>
    ''' <param name="ValueTotal"></param>
    ''' <param name="presentation"></param>
    ''' <param name="IPSServiceId"></param>
    ''' <param name="IPSServiceCodeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function CausedValue(MedicalFeesCausationId As Integer, RateManualType As Integer, CupsEntityId As Integer, CareGroupId As Integer, RateManualId As Integer, ValueTotal As Decimal, presentation As Integer, IPSServiceId As Integer, IPSServiceCodeName As String,
                         healthProfessionalCode As String, thirdPartyDescription As String, IPSServiceIdParent As Integer?, ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId As Integer,
                         MedicalFeesContractId As Integer, Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation))

    ''' <summary>
    ''' Metodo que causa los valores masivamente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CauseMassively(ListNoSurgical As List(Of NoQxEntity), ListSurgical As List(Of QxEntity), Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity)))

    <OperationContract()>
    Function CauseInvoice(invoiceDetail As ViewListNoSurgical, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation))

    <OperationContract()>
    Function CauseInvoiceQx(invoiceDetail As ViewListSurgicalAndPackage, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation))

End Interface
