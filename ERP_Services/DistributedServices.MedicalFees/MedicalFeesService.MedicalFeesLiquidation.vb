'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
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
    ''' Confirma la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularMedicalFeesLiquidation(ByVal MedicalFeesLiquidation As MedicalFeesLiquidation, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation) Implements IMedicalFeesMedicalFeesLiquidation.AnnularMedicalFeesLiquidation
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.AnnularMedicalFeesLiquidation(MedicalFeesLiquidation, audit)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.AnnularMedicalFeesLiquidation(MedicalFeesLiquidation, audit)
    End Function

    ''' <summary>
    ''' Elimina la liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesLiquidation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMedicalFeesLiquidation(MedicalFeesLiquidation As Domain.Entities.MedicalFeesLiquidation, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IMedicalFeesMedicalFeesLiquidation.DeleteMedicalFeesLiquidation
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.DeleteMedicalFeesLiquidation(MedicalFeesLiquidation, audit)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.DeleteMedicalFeesLiquidation(MedicalFeesLiquidation, audit)
    End Function

    ''' <summary>
    ''' Obtiene la liquidacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidation(code As String, medicalFeesContractId As Integer, audit As AuditMessage, optionConsult As Integer, Optional ByVal healthProfeesionalCode As String = Nothing) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation) Implements IMedicalFeesMedicalFeesLiquidation.GetMedicalFeesLiquidation
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.GetMedicalFeesLiquidation(code, medicalFeesContractId, optionConsult, audit, healthProfeesionalCode)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.GetMedicalFeesLiquidation(code, medicalFeesContractId, optionConsult, audit, healthProfeesionalCode)
    End Function

    ''' <summary>
    ''' Obtiene la liquidacion por id
    ''' </summary>
    ''' <param name="MedicalFeesContractId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation) Implements IMedicalFeesMedicalFeesLiquidation.GetMedicalFeesLiquidationByMedicalFeesContractId
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId, audit)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId, audit)
    End Function

    ''' <summary>
    ''' Obtiene la liquidacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesLiquidationById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation) Implements IMedicalFeesMedicalFeesLiquidation.GetMedicalFeesLiquidationById
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.GetMedicalFeesLiquidationById(id, audit)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.GetMedicalFeesLiquidationById(id, audit)
    End Function

    ''' <summary>
    ''' Guarda y confrima la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAndConfirmMedicalFeesLiquidation(ByVal medicalFeesLiquidation As MedicalFeesLiquidation, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation) Implements IMedicalFeesMedicalFeesLiquidation.SaveAndConfirmMedicalFeesLiquidation
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.SaveAndConfirmMedicalFeesLiquidation(medicalFeesLiquidation, audit, idSequense)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.SaveAndConfirmMedicalFeesLiquidation(medicalFeesLiquidation, audit, idSequense)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesLiquidation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveMedicalFeesLiquidation(MedicalFeesLiquidation As Domain.Entities.MedicalFeesLiquidation, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation) Implements IMedicalFeesMedicalFeesLiquidation.SaveMedicalFeesLiquidation
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.SaveMedicalFeesLiquidation(MedicalFeesLiquidation, audit, idSequense)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.SaveMedicalFeesLiquidation(MedicalFeesLiquidation, audit, idSequense)
    End Function

    ''' <summary>
    ''' Valida que la cuenta contable que viene amarrada a la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateCostCenterBySupplierDistributionLineId(supplierDistributionLineId As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation) Implements IMedicalFeesMedicalFeesLiquidation.ValidateCostCenterBySupplierDistributionLineId
        Using service As IMedicalFeesLiquidationAdminService = Container.Current.Resolve(Of IMedicalFeesLiquidationAdminService)()
            Return service.ValidateCostCenterBySupplierDistributionLineId(supplierDistributionLineId)
        End Using
        'Return Me._medicalFeesLiquidationAdminService.ValidateCostCenterBySupplierDistributionLineId(supplierDistributionLineId)
    End Function

End Class
