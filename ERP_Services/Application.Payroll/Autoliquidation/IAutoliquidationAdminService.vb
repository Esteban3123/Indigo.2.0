'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 17-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text

Public Interface IAutoliquidationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una Autoliquidación por Id del Grupo
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Autiliquidation</returns>
    ''' <remarks></remarks>
    Function GetAutoliquidationByGroupId(groupId As String) As List(Of Autoliquidation)

    ''' <summary>
    ''' Almacena una Autoliquidación
    ''' </summary>
    ''' <param name="autoliquidation">Objeto Autoliquidation</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveAutoliquidation(autoliquidation As Autoliquidation, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene el listado de fechas de liquidacion de una empresa
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDateLiquidationCompany(ByVal companyId As Integer) As List(Of String)

    ''' <summary>
    ''' Genera todo el proceso de autoliquidación y el plano
    ''' </summary>
    ''' <param name="companyId">Id de la compañia</param>
    ''' <param name="periodLiquidation">Periodo de liquidación</param>
    ''' <param name="workCenterId">Id del centro de trabajo</param>
    ''' <param name="isCorrection">si es correccion</param>
    ''' <param name="dateLiquidation">Fecha de liquidacion</param>
    ''' <param name="numberTemplate">numero de plantilla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateAutoLiquidation(companyId As Integer, periodLiquidation As String, workCenterId As Integer, isCorrection As Boolean, dateLiquidation As Nullable(Of Date), numberTemplate As String) As List(Of ActionMessageResult(Of StringBuilder))

    ''' <summary>
    ''' GEnero el Excel de Autoliquidación
    ''' </summary>
    ''' <param name="PayrollDate"></param>
    ''' <param name="WorkCenter"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function GenerateReportAutoliquidation(PeriodLiquidation As String, WorkCenterId As Integer?, session As SessionValues) As DataSet

    ''' <summary>
    ''' GEnero el 
    ''' </summary>
    ''' <param name="WorkCenterCode"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <returns></returns>
    Function GenerateValidationAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFile_Result)

    ''' <summary>
    ''' Función para listar los datos de Verificación del archivo de Autoliquidación
    ''' </summary>
    ''' <param name="WorkCenterCode"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <returns></returns>
    Function ListVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date) As List(Of VerifyAutoliquidationFile)

    Function ConfirmVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, FlagConfirm As Boolean, SessionValues As SessionValues) As ActionResult(Of List(Of VerifyAutoliquidationFile))

    Function SaveVerifyAutoliquidation(VerifyAutoliquidationFile As VerifyAutoliquidationFile) As ActionResult(Of VerifyAutoliquidationFile)

    Function SaveMassiveVerifyAutoliquidation(ListImportFileRow As List(Of ImportFileRow), Audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))

    Function GetVerifyAutoliquidationById(IfVerifyAutoliquidationFile As Integer) As VerifyAutoliquidationFile
    ''' <summary>
    ''' Funcion para generar archivo plano CCSS
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Function GenerateCCSSFile(companyId As Integer, workCenterId As Integer, periodLiquidation As String) As ActionMessageResult(Of StringBuilder)
    ''' <summary>
    ''' Genera la autoliquidacionCR
    ''' </summary>
    ''' <param name="WorkCenterId"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <returns></returns>
    Function GenerateValidationAutoliquidationCR(WorkCenterId As Integer, PayrollDateLiquidated As Date) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFileCR)

    ''' <summary>
    ''' Funcion para generar archivo plano INS
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="policyNumber"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Function GenerateINSFile(companyId As Integer, policyNumber As String, workCenterId As Integer, periodLiquidation As String) As ActionMessageResult(Of StringBuilder)


End Interface
