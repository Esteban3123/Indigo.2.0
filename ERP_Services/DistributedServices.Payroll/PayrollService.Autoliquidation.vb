'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports System.Text

Partial Class PayrollService

    Implements IPayrollAutoliquidation

    ''' <summary>
    ''' Obtiene una Autoliquidación por Id del Grupo
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Lista de Autoliquidaciones</returns>
    ''' <remarks></remarks>
    Public Function GetAutoliquidationByGroupId(groupId As String, session As SessionValues) As List(Of Domain.Payroll.Entities.Autoliquidation) Implements IPayrollAutoliquidation.GetAutoliquidationByGroupId
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GetAutoliquidationByGroupId(groupId)
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Autoliquidación
    ''' </summary>
    ''' <param name="autoliquidation">Autoliquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAutoliquidation(autoliquidation As Domain.Payroll.Entities.Autoliquidation, session As SessionValues) As Boolean Implements IPayrollAutoliquidation.SaveAutoliquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.SaveAutoliquidation(autoliquidation, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el listado de fechas de liquidacion de una empresa
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDateLiquidationCompany(companyId As Integer, session As SessionValues) As List(Of String) Implements IPayrollAutoliquidation.GetDateLiquidationCompany
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GetDateLiquidationCompany(companyId)
        End Using
    End Function

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
    Public Function GenerateAutoLiquidation(companyId As Integer, periodLiquidation As String, workCenterId As Integer, isCorrection As Boolean, dateLiquidation As Date?, numberTemplate As String, session As SessionValues) As List(Of ActionMessageResult(Of StringBuilder)) Implements IPayrollAutoliquidation.GenerateAutoLiquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GenerateAutoLiquidation(companyId, periodLiquidation, workCenterId, isCorrection, dateLiquidation, numberTemplate)
        End Using
    End Function

    Public Function GenerateReportAutoliquidation(PeriodLiquidation As String, WorkCenterId As Integer?, session As SessionValues) As DataSet Implements IPayrollAutoliquidation.GenerateReportAutoliquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GenerateReportAutoliquidation(PeriodLiquidation, WorkCenterId, session)
        End Using
    End Function

    Public Function GenerateValidationAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFile_Result) Implements IPayrollAutoliquidation.GenerateValidationAutoliquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GenerateValidationAutoliquidation(WorkCenterId, PayrollDateLiquidated)
        End Using
    End Function
    ''' <summary>
    ''' Generamos el Archivo plano CR
    ''' </summary>
    ''' <param name="WorkCenterId"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function GenerateValidationAutoliquidationCR(WorkCenterId As Integer, PayrollDateLiquidated As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFileCR) Implements IPayrollAutoliquidation.GenerateValidationAutoliquidationCR
        Using AutoliquidationAdminCr As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdminCr.GenerateValidationAutoliquidationCR(WorkCenterId, PayrollDateLiquidated)
        End Using
    End Function

    Public Function ListVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, session As SessionValues) As List(Of Domain.Payroll.Entities.VerifyAutoliquidationFile) Implements IPayrollAutoliquidation.ListVerifyAutoliquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.ListVerifyAutoliquidation(WorkCenterId, PayrollDateLiquidated)
        End Using
    End Function

    Public Function ConfirmVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, FlagConfirm As Boolean, session As SessionValues) As ActionResult(Of List(Of Domain.Payroll.Entities.VerifyAutoliquidationFile)) Implements IPayrollAutoliquidation.ConfirmVerifyAutoliquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.ConfirmVerifyAutoliquidation(WorkCenterId, PayrollDateLiquidated, FlagConfirm, session)
        End Using
    End Function

    Public Function SaveVerifyAutoliquidation(VerifyAutoliquidation As Domain.Payroll.Entities.VerifyAutoliquidationFile, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.VerifyAutoliquidationFile) Implements IPayrollAutoliquidation.SaveVerifyAutoliquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.SaveVerifyAutoliquidation(VerifyAutoliquidation)
        End Using
    End Function

    Public Function SaveMassiveVerifyAutoliquidation(ListImportFileRow As List(Of ImportFileRow), session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IPayrollAutoliquidation.SaveMassiveVerifyAutoliquidation
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.SaveMassiveVerifyAutoliquidation(ListImportFileRow, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetVerifyAutoliquidationFileById(IdVerifyAutoliquidationFile As Integer, session As SessionValues) As Domain.Payroll.Entities.VerifyAutoliquidationFile Implements IPayrollAutoliquidation.GetVerifyAutoliquidationFileById
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GetVerifyAutoliquidationById(IdVerifyAutoliquidationFile)
        End Using
    End Function
    ''' <summary>
    ''' Conexion para referenciar la funcion de generar archivo plano con Presentacion
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GenerateCCSS(companyId As Integer, workCenterId As Integer, periodLiquidation As String, session As SessionValues) As ActionMessageResult(Of StringBuilder) Implements IPayrollAutoliquidation.GenerateCCSS
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GenerateCCSSFile(companyId, workCenterId, periodLiquidation)
        End Using
    End Function
    ''' <summary>
    ''' Conexion para referenciar la funcion de generar archivo plano con Presentacion
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="policyNumber"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GenerateINS(companyId As Integer, policyNumber As String, workCenterId As Integer?, periodLiquidation As String, session As SessionValues) As ActionMessageResult(Of StringBuilder) Implements IPayrollAutoliquidation.GenerateINS
        Using AutoliquidationAdmin As IAutoliquidationAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAutoliquidationAdminService)()
            Return AutoliquidationAdmin.GenerateINSFile(companyId, policyNumber, workCenterId, periodLiquidation)
        End Using
    End Function
End Class
