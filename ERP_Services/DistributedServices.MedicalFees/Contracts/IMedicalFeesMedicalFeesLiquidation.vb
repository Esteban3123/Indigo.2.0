'***********************************************************************
' Assembly         : DistributedServices.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IMedicalFeesMedicalFeesLiquidation

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMedicalFeesLiquidation(MedicalFeesLiquidation As Domain.Entities.MedicalFeesLiquidation, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation)

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMedicalFeesLiquidation(MedicalFeesLiquidation As Domain.Entities.MedicalFeesLiquidation, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicalFeesLiquidation(code As String, medicalFeesContractId As Integer, audit As AuditMessage, optionConsult As Integer, Optional ByVal healthProfeesionalCode As String = Nothing) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation)

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicalFeesLiquidationById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation)

    ''' <summary>
    ''' Confirma la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function AnnularMedicalFeesLiquidation(ByVal MedicalFeesLiquidation As MedicalFeesLiquidation, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation)

    ''' <summary>
    ''' Guarda y confirma la liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAndConfirmMedicalFeesLiquidation(ByVal medicalFeesLiquidation As MedicalFeesLiquidation, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MedicalFeesLiquidation)

    ''' <summary>
    ''' Valida que la cuenta contable que viene amarrada en la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ValidateCostCenterBySupplierDistributionLineId(ByVal supplierDistributionLineId As Integer) As ActionResult(Of MedicalFeesLiquidation)

End Interface
