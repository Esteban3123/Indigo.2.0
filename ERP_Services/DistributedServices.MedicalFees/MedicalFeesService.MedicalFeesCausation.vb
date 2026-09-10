'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.MedicalFees
Imports Microsoft.Practices.Unity

Partial Class MedicalFeesService

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="ListInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesCausation(ListInfo As List(Of Tuple(Of Integer, Integer)), Company As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer, Integer))) Implements IMedicalFeesMedicalFeesCausation.DeleteMedicalFeesCausation
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.DeleteMedicalFeesCausation(ListInfo, audit, Company)
        End Using
        'Return Me._medicalFeesCausationAdminService.DeleteMedicalFeesCausation(ListInfo, audit, Company)
    End Function

    ''' <summary>
    ''' Elimina una causacion desde la opcion de eliminar honorario
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesCausationFromRemoveFees(MedicalFeesCausationId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IMedicalFeesMedicalFeesCausation.DeleteMedicalFeesCausationFromRemoveFees
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.DeleteMedicalFeesCausationFromRemoveFees(MedicalFeesCausationId, audit)
        End Using
        'Return Me._medicalFeesCausationAdminService.DeleteMedicalFeesCausationFromRemoveFees(MedicalFeesCausationId, audit)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesCausationById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesCausation) Implements IMedicalFeesMedicalFeesCausation.GetMedicalFeesCausationById
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.GetMedicalFeesCausationById(id, audit)
        End Using
        'Return Me._medicalFeesCausationAdminService.GetMedicalFeesCausationById(id, audit)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="ListMedicalFeesCausation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMedicalFeesCausation(ListMedicalFeesCausation As List(Of Domain.Entities.MedicalFeesCausation), ByVal ListMedicalFeesNotes As List(Of MedicalFeesNote), ByVal ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.MedicalFeesCausation)) Implements IMedicalFeesMedicalFeesCausation.SaveMedicalFeesCausation
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.SaveMedicalFeesCausation(ListMedicalFeesCausation, ListMedicalFeesNotes, ListDeleteMedicalFeesNotes, audit)
        End Using
        'Return Me._medicalFeesCausationAdminService.SaveMedicalFeesCausation(ListMedicalFeesCausation, ListMedicalFeesNotes, ListDeleteMedicalFeesNotes, audit)
    End Function

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
    Public Function CausedValue(MedicalFeesCausationId As Integer, RateManualType As Integer, CupsEntityId As Integer, CareGroupId As Integer, RateManualId As Integer, ValueTotal As Decimal, presentation As Integer, IPSServiceId As Integer, IPSServiceCodeName As String,
                                healthProfessionalCode As String, thirdPartyDescription As String, IPSServiceIdParent As Integer?,
                                ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId As Integer, MedicalFeesContractId As Integer,
                                Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As Domain.Base.Entities.ActionResult(Of List(Of CupsHomologation)) Implements IMedicalFeesMedicalFeesCausation.CausedValue
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.CausedValue(MedicalFeesCausationId, RateManualType, CupsEntityId, CareGroupId, RateManualId, ValueTotal, presentation, IPSServiceId, IPSServiceCodeName, healthProfessionalCode, thirdPartyDescription, IPSServiceIdParent,
                                                             ServiceOrderDetailId, ServiceOrderDetailSurgicalId, MedicalFeesContractId, ListCupsHomologation)
        End Using
        'Return _medicalFeesCausationAdminService.CausedValue(MedicalFeesCausationId, RateManualType, CupsEntityId, CareGroupId, RateManualId, ValueTotal, presentation, IPSServiceId, IPSServiceCodeName, healthProfessionalCode, thirdPartyDescription, IPSServiceIdParent,
        '                                                     ServiceOrderDetailId, ServiceOrderDetailSurgicalId, MedicalFeesContractId, ListCupsHomologation)
    End Function

    ''' <summary>
    ''' Metodo que causa los valores masivamente
    ''' </summary>
    ''' <param name="ListNoSurgical"></param>
    ''' <param name="ListSurgical"></param>
    ''' <param name="ListCupsHomologation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CauseMassively(ListNoSurgical As List(Of Domain.Entities.NoQxEntity), ListSurgical As List(Of Domain.Entities.QxEntity), Optional ListCupsHomologation As List(Of Domain.Entities.CupsHomologation) = Nothing) As Domain.Base.Entities.ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity))) Implements IMedicalFeesMedicalFeesCausation.CauseMassively
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.CauseMassively(ListNoSurgical, ListSurgical, ListCupsHomologation)
        End Using
    End Function

    Public Function CauseInvoice(invoiceDetail As ViewListNoSurgical, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements IMedicalFeesMedicalFeesCausation.CauseInvoice
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.CauseInvoice(invoiceDetail, audit)
        End Using
    End Function

    Public Function CauseInvoiceQx(invoiceDetail As ViewListSurgicalAndPackage, audit As AuditMessage, Optional listCupsHomologations As List(Of CupsHomologation) = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements IMedicalFeesMedicalFeesCausation.CauseInvoiceQx
        Using service As IMedicalFeesCausationAdminService = Container.Current.Resolve(Of IMedicalFeesCausationAdminService)()
            Return service.CauseInvoiceQx(invoiceDetail, audit)
        End Using
    End Function
End Class
