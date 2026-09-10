'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMedicalFeesCausationAdminService
    Inherits IDisposable

    Function GetViewListNoSurgical(invoiceNumbers As List(Of String)) As List(Of ViewListNoSurgical)

    Function GetCupsEntityIdsByServiceTypes(cupsEntityIds As List(Of Integer), serviceTypes As List(Of Byte)) As List(Of Integer)

    Function GetViewListDiagnosticImaging(serviceOrderDetailId As Integer) As List(Of ViewListDiagnosticImaging)

    Function GetViewListDiagnosticImagingAmbulatory(serviceOrderDetailId As Integer) As List(Of ViewListDiagnosticImagingAmbulatory)

    Function ListViewSurgicalAndPackageByInvoiceNumber(invoiceNumbers As List(Of String)) As List(Of ViewListSurgicalAndPackage)

    ''' <summary>
    ''' Obtiene los IDs de ThirdParty válidos (existen y están activos) de una lista de IDs
    ''' </summary>
    ''' <param name="thirdPartyIds">Lista de IDs de ThirdParty a validar</param>
    ''' <returns>Lista de IDs válidos</returns>
    Function GetValidThirdPartyIds(thirdPartyIds As List(Of Integer)) As List(Of Integer)

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMedicalFeesCausation(ByVal ListMedicalFeesCausation As List(Of MedicalFeesCausation), ByVal ListMedicalFeesNotes As List(Of MedicalFeesNote), ByVal ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote), ByVal audit As AuditMessage) As ActionResult(Of List(Of MedicalFeesCausation))

    ''' <summary>
    ''' Guarda o Actualiza la entidad MedicalFeesCausation de forma asincrona
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMedicalFeesCausationAsync(ByVal ListMedicalFeesCausation As List(Of MedicalFeesCausation), ByVal ListMedicalFeesNotes As List(Of MedicalFeesNote), ByVal ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote), ByVal audit As AuditMessage) As Task(Of ActionResult(Of List(Of MedicalFeesCausation)))

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteMedicalFeesCausation(ListInfo As List(Of Tuple(Of Integer, Integer)), ByVal audit As AuditMessage, Company As String) As ActionResult(Of List(Of Tuple(Of String, Integer, Integer)))

    ''' <summary>
    ''' Elimina una causacion desde la opcion de eliminar honorario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteMedicalFeesCausationFromRemoveFees(ByVal MedicalFeesCausationId As Integer, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetMedicalFeesCausationById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of MedicalFeesCausation)

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
    Function CausedValue(MedicalFeesCausationId As Integer, RateManualType As Integer, CupsEntityId As Integer, CareGroupId As Integer, RateManualId As Integer, ValueTotal As Decimal, presentation As Integer, IPSServiceId As Integer,
                         IPSServiceCodeName As String, healthProfessionalCode As String, thirdPartyDescription As String,
                         IPSServiceIdParent As Integer?, ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId As Integer, MedicalFeesContractId As Integer,
                         Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation))

    ''' <summary>
    ''' Metodo que causa los valores masivamente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CauseMassively(ListNoSurgical As List(Of NoQxEntity), ListSurgical As List(Of QxEntity), Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity)))

    Function CauseInvoice(invoiceDetail As ViewListNoSurgical, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation))
    Function GetCausationInvoice(invoiceDetail As ViewListNoSurgical, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation)))
    Function CauseInvoiceQx(invoiceDetail As ViewListSurgicalAndPackage, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation))
    Function GetCausationInvoiceQx(invoiceDetail As ViewListSurgicalAndPackage, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of (causation As MedicalFeesCausation, homologations As List(Of CupsHomologation)))

    ''' <summary>
    ''' Procesa causaciones automáticas para órdenes de servicio CUPS no reconocidas.
    ''' Solo Registrado (sin factura), CUPS, sin causación activa. Excluye liquidados.
    ''' Procesa todas las unidades operativas.
    ''' </summary>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="batchSize">Tamaño del batch por iteración (default 500 para ejecución manual)</param>
    ''' <returns>Resultado con conteos y detalle por item procesado</returns>
    Function ProcessUnrecognizedCausations(audit As AuditMessage, Optional batchSize As Integer = 500) As ActionResult(Of UnrecognizedProcessingResult)
End Interface
