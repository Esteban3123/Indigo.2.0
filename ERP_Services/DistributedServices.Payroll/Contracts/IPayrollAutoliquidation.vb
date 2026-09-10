'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 18-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text

<ServiceContract>
Public Interface IPayrollAutoliquidation

    ''' <summary>
    ''' Almacena o Actualiza las Autoliquidaciones
    ''' </summary>
    ''' <param name="autoliquidation">Autoliquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveAutoliquidation(ByVal autoliquidation As Autoliquidation, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una lista de Autoliquidaciones por Id del Grupo
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAutoliquidationByGroupId(groupId As String, session As SessionValues) As List(Of Autoliquidation)

    ''' <summary>
    ''' Obtiene el listado de fechas de liquidacion de una empresa
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetDateLiquidationCompany(ByVal companyId As Integer, session As SessionValues) As List(Of String)

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
    <OperationContract()>
    Function GenerateAutoLiquidation(companyId As Integer, periodLiquidation As String, workCenterId As Integer, isCorrection As Boolean, dateLiquidation As Nullable(Of Date), numberTemplate As String, session As SessionValues) As List(Of ActionMessageResult(Of StringBuilder))

    <OperationContract()>
    Function GenerateReportAutoliquidation(PeriodLiquidation As String, WorkCenterId As Integer?, session As SessionValues) As DataSet

    <OperationContract()>
    Function GenerateValidationAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFile_Result)

    <OperationContract()>
    Function ListVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, session As SessionValues) As List(Of VerifyAutoliquidationFile)

    <OperationContract()>
    Function ConfirmVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, FlagConfirm As Boolean, session As SessionValues) As ActionResult(Of List(Of VerifyAutoliquidationFile))

    <OperationContract()>
    Function SaveVerifyAutoliquidation(VerifyAutoliquidationFile As VerifyAutoliquidationFile, session As SessionValues) As ActionResult(Of VerifyAutoliquidationFile)

    <OperationContract()>
    Function SaveMassiveVerifyAutoliquidation(ListImportFileRow As List(Of ImportFileRow), session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer)))

    <OperationContract()>
    Function GetVerifyAutoliquidationFileById(IdVerifyAutoliquidationFile As Integer, session As SessionValues) As Domain.Payroll.Entities.VerifyAutoliquidationFile

    <OperationContract()>
    Function GenerateCCSS(companyId As Integer, workCenterId As Integer, periodLiquidation As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)
    <OperationContract>
    Function GenerateValidationAutoliquidationCR(WorkCenterId As Integer, PayrollDateLiquidated As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFileCR)

    <OperationContract()>
    Function GenerateINS(companyId As Integer, policyNumber As String, workCenterId As Integer?, periodLiquidation As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)

    <OperationContract()>
    Function GeneratePilaFile(companyId As Integer, periodLiquidation As String, session As SessionValues) As ActionMessageResult(Of StringBuilder)

    <OperationContract()>
    Function GeneratePilaExcel(companyId As Integer, periodLiquidation As String, session As SessionValues) As ActionMessageResult(Of Byte())
End Interface
