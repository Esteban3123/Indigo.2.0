'************************************************************
' Assembly         : Domain.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IMedicalFeesLiquidationRepository
    Inherits IRepository(Of MedicalFeesLiquidation)

    ''' <summary>
    ''' Obtiene una liquidacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesLiquidation(code As String, medicalFeesContractId As Integer, optionConsult As Integer, Optional ByVal healthProfeesionalCode As String = Nothing) As MedicalFeesLiquidation

    ''' <summary>
    ''' Obtiene una liquidacion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesLiquidationById(id As Integer) As MedicalFeesLiquidation

    ''' <summary>
    ''' Obtiene una liquidacion por el id del contrato profesional de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesLiquidationByMedicalFeesContractId(MedicalFeesContractId As Integer) As MedicalFeesLiquidation

    ''' <summary>
    ''' Obtiene el id de la cuenta contable teniendo como parametro el id del detalle de la liquidacion
    ''' </summary>
    ''' <param name="medicalFeesLiquidationDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMainAccountIdByMedicalFeesLiquidationDeatil(medicalFeesLiquidationDetail As MedicalFeesLiquidationDetail) As Integer

    ''' <summary>
    ''' Obtiene el id del centro de costo, pero primero consulta que la cuenta contable maneje centro de costo
    ''' </summary>
    ''' <param name="medicalFeesCausationId"></param>
    ''' <param name="mainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCostCenterIdByMedicalFeesCausation(medicalFeesCausationId As Integer, mainAccountId As Integer) As Integer?

    ''' <summary>
    ''' Valida que la cuenta contable asociada a la linea de distribucion maneje centro costo
    ''' </summary>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateCostCenterBySupplierDistributionLineId(ByVal supplierDistributionLineId As Integer) As Boolean

    ''' <summary>
    ''' Guarda la liquidación de honorarios médicos
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveMedicalFeesLiquidation(xmlObject As String, codeUser As String) As SP_SaveMedicalFeesLiquidation_Result

    ''' <summary>
    ''' Confirma la liquidación de honorarios médicos
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmMedicalFeesLiquidation(xmlObject As String, codeUser As String) As SP_ConfirmMedicalFeesLiquidation_Result

    ''' <summary>
    ''' Anula la liquidación de honorarios médicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_AnnularMedicalFeesLiquidation(MedicalFeesLiquidationId As Integer, codeUser As String) As SP_AnnularMedicalFeesLiquidation_Result

End Interface
