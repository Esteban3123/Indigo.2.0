'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IAutoliquidationRepository
    Inherits IRepository(Of Autoliquidation)

    ''' <summary>
    ''' Obtiene una Autoliquidación por Grupo
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Autoliquidación</returns>
    ''' <remarks></remarks>
    Function GetAutoliquidationByGroupId(ByVal groupId As String) As List(Of Autoliquidation)

    ''' <summary>
    ''' Obtiene el listado de fechas de liquidacion de una empresa
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDateLiquidationCompany(ByVal companyId As Integer) As List(Of String)
    ''' <summary>
    ''' Procedimiento Almacenado para crear el archivo Plano de Autoliquidación
    ''' </summary>
    ''' <param name="PayrollDate"></param>
    ''' <param name="WorkCode"></param>
    ''' <returns></returns>
    Function GenerateAutoliquidation(PayrollDate As Date, WorkCode As String) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFile_Result)

    ''' <summary>
    ''' Función que carga los datos a verificar de la Autoliquidacion
    ''' </summary>
    ''' <param name="WorkCenterCode"></param>
    ''' <param name="PayrollDate"></param>
    ''' <returns></returns>
    Function ListVerifyAutoliquidation(WorkCenterCode As String, PayrollDate As Date) As List(Of VerifyAutoliquidationFile)

    ''' <summary>
    ''' FUNCIÓN PARA CONFIRMAR Y DESCONFIRMAR LOS REGISTROS
    ''' </summary>
    ''' <param name="WorkCenterCode"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <param name="FlagConfirm"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_ConfirmDisconfirmVerifyAutoliquidation(WorkCenterCode As String, PayrollDateLiquidated As Date, FlagConfirm As Boolean, codeUser As String) As SP_ConfirmDisconfirmVerifyAutoliquidation_Result

    ''' <summary>
    ''' Devuelve objeto VerifyAutoliquidationFile por Id
    ''' </summary>
    ''' <param name="IdVerifyAutoliquidation">Id VerifyAutoliquidationFile</param>
    ''' <returns></returns>
    Function GetVerifyAutoliquidationById(IdVerifyAutoliquidation As Integer) As VerifyAutoliquidationFile

    ''' <summary>
    ''' Valida que los grupos tengan parametrizados el SMMLVAmountExemption
    ''' </summary>
    ''' <param name="listLiquidation"></param>
    ''' <returns></returns>
    Function ValidateSMMLVAmountExemptionParametrization(listLiquidation As List(Of Liquidation)) As String

    Function SP_SaveMassiveVerifyAutoliquidation_Result(XMLObject As String, codeUser As String) As List(Of SP_SaveMassiveVerifyAutoliquidation_Result)

End Interface
