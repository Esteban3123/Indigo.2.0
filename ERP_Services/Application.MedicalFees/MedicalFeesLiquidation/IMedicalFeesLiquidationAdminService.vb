'***********************************************************************
' Assembly         : Application.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IMedicalFeesLiquidationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveMedicalFeesLiquidation(ByVal MedicalFeesLiquidation As MedicalFeesLiquidation, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of MedicalFeesLiquidation)

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteMedicalFeesLiquidation(ByVal MedicalFeesLiquidation As MedicalFeesLiquidation, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetMedicalFeesLiquidation(ByVal code As String, medicalFeesContractId As Integer, optionConsult As Integer, ByVal audit As AuditMessage, Optional ByVal healthProfeesionalCode As String = Nothing) As ActionResult(Of MedicalFeesLiquidation)

    ''' <summary>
    ''' Obtiene la entidad por id del contrato profesional de la salud
    ''' </summary>
    ''' <returns></returns>
    Function GetMedicalFeesLiquidationByMedicalFeesContractId(ByVal MedicalFeesContractId As Integer, ByVal audit As AuditMessage) As ActionResult(Of MedicalFeesLiquidation)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetMedicalFeesLiquidationById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of MedicalFeesLiquidation)

    ''' <summary>
    ''' Confirma la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AnnularMedicalFeesLiquidation(ByVal MedicalFeesLiquidation As MedicalFeesLiquidation, ByVal audit As AuditMessage) As ActionResult(Of MedicalFeesLiquidation)

    ''' <summary>
    ''' Guarda y confirma la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAndConfirmMedicalFeesLiquidation(ByVal medicalFeesLiquidation As MedicalFeesLiquidation, ByVal audit As AuditMessage, Optional ByVal idSequense As Long = 0) As ActionResult(Of MedicalFeesLiquidation)

    ''' <summary>
    ''' Valida que la cuenta contable amarrada a la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateCostCenterBySupplierDistributionLineId(ByVal supplierDistributionLineId As Integer) As ActionResult(Of MedicalFeesLiquidation)

End Interface
