'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 17-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports System.Text
Imports System.Net.Http
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Data.Entity.Core
Imports System.Configuration
Imports Newtonsoft.Json

Public Class AutoliquidationAdminService
    Implements IAutoliquidationAdminService

    'Repositorio de Autoliquidación
    Private _autoliquidationRepository As IAutoliquidationRepository

    ''' <summary>
    ''' Repositorio de Autoliquidaion
    ''' </summary>
    Private _verifyAutoliquidationRepository As IVerifyAutoliquidationRepository

    ''' <summary>
    ''' Repositorio de compañia
    ''' </summary>
    ''' <remarks></remarks>
    Private _companyRepository As ICompanyRepository
    ''' <summary>
    ''' Repositorio de centro de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    Private _workCenterRepository As IWorkCenterRepository

    ''' <summary>
    ''' Repositorio de liquidacion de nomina
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' dominio de la liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _autoliquidationDomain As Domain.Payroll.Entities.IAutoliquidationDomain

    Private _autoliquidationNewDomain As Domain.Payroll.IAutoliquidationDomain

    Private _endpointsRepository As Domain.Security.IEndpointsRepository

    ''' <summary>
    ''' inicia el repositorio de Conceptos
    ''' </summary>
    ''' <param name="autoliquidationRepository">Repositorio de Autoliquidación</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal autoliquidationRepository As IAutoliquidationRepository, companyRepository As ICompanyRepository,
                   workCenterRepository As IWorkCenterRepository, liquidationRepository As IPayrollLiquidationRepository,
                   autoliquidationDomain As Domain.Payroll.Entities.IAutoliquidationDomain, autoliquidationNewDomain As Domain.Payroll.IAutoliquidationDomain, verifyAutoliquidationRepository As Domain.Payroll.IVerifyAutoliquidationRepository,
                   endpointsRepository As Domain.Security.IEndpointsRepository)
        If (autoliquidationRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Autoliquidación vacio")
        End If
        _autoliquidationRepository = autoliquidationRepository
        _companyRepository = companyRepository
        _workCenterRepository = workCenterRepository
        _liquidationRepository = liquidationRepository
        _autoliquidationDomain = autoliquidationDomain
        _autoliquidationNewDomain = autoliquidationNewDomain
        _verifyAutoliquidationRepository = verifyAutoliquidationRepository
        _endpointsRepository = endpointsRepository
    End Sub

    ''' <summary>
    ''' Almacena una Autoliquidación
    ''' </summary>
    ''' <param name="groupId">Id del Grupo</param>
    ''' <returns>Autoliquidacion</returns>
    ''' <remarks></remarks>
    Public Function GetAutoliquidationByGroupId(groupId As String) As List(Of Autoliquidation) Implements IAutoliquidationAdminService.GetAutoliquidationByGroupId
        If String.IsNullOrEmpty(groupId) Then
            Throw New ArgumentNullException("groupId vacio")
        End If
        Try
            Return _autoliquidationRepository.GetAutoliquidationByGroupId(groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Autoliquidation)
        End Try
    End Function

    ''' <summary>
    ''' Almacena una 
    ''' </summary>
    ''' <param name="autoliquidation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAutoliquidation(autoliquidation As Autoliquidation, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IAutoliquidationAdminService.SaveAutoliquidation
        If autoliquidation Is Nothing Then
            Throw New ArgumentNullException("autoliquidation vacio")
        End If
        Dim unitWork As IUnitWork = _autoliquidationRepository.UnitWork
        Try
            'Valido si se va a guardar o a actualizar
            _autoliquidationRepository.SaveEntity(autoliquidation)
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de fechas de liquidacion de una empresa
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDateLiquidationCompany(companyId As Integer) As List(Of String) Implements IAutoliquidationAdminService.GetDateLiquidationCompany
        Try
            Return _autoliquidationRepository.GetDateLiquidationCompany(companyId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of String)
        End Try
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
    Public Function GenerateAutoLiquidation(companyId As Integer, periodLiquidation As String, workCenterId As Integer, isCorrection As Boolean, dateLiquidation As Date?, numberTemplate As String) As List(Of ActionMessageResult(Of StringBuilder)) Implements IAutoliquidationAdminService.GenerateAutoLiquidation
        Try
            Dim company As Company = _companyRepository.GetCompanyById(companyId)
            Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(workCenterId)
            Dim listLiquidation = _liquidationRepository.GetLiquidationByPeriod(periodLiquidation)
            Dim ListLiquidationWorkCenter As New List(Of Liquidation)

            If workCenterId > 0 Then
                ListLiquidationWorkCenter = listLiquidation.Where(Function(y) y.WorkCenterId = workCenterId).ToList
            Else
                ListLiquidationWorkCenter = listLiquidation
            End If
            ''se separa la lista por los tipos de empleado segun la planilla
            Dim totalData As List(Of ActionMessageResult(Of StringBuilder)) = New List(Of ActionMessageResult(Of StringBuilder))

            Dim listESpreadsheet = _autoliquidationNewDomain.GenerateArchive(company, workCenter, periodLiquidation, isCorrection, dateLiquidation, numberTemplate, listLiquidation, "E")
            Dim listKSpreadsheet = _autoliquidationNewDomain.GenerateArchive(company, workCenter, periodLiquidation, isCorrection, dateLiquidation, numberTemplate, listLiquidation, "K")

            totalData.Add(listESpreadsheet)
            totalData.Add(listKSpreadsheet)
            Return totalData
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Dim totalData As List(Of ActionMessageResult(Of StringBuilder)) = New List(Of ActionMessageResult(Of StringBuilder))
            totalData.Add(New ActionMessageResult(Of StringBuilder) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex), .MessageResult = Nothing})
            Return totalData
        End Try
    End Function
    ''' <summary>
    ''' Metodo para crear el archivo plano CCSS
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Public Function GenerateCCSS(companyId As Integer, workCenterId As Integer, periodLiquidation As String) As ActionMessageResult(Of StringBuilder) Implements IAutoliquidationAdminService.GenerateCCSSFile
        Dim FileCCSSList As New ActionMessageResult(Of StringBuilder)
        Try

            Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(workCenterId)
            Dim listLiquidation = _liquidationRepository.GetLiquidationByPeriodAndWorkCenter(periodLiquidation, workCenterId)
            Dim company As Company = _companyRepository.GetCompanyById(companyId)

            FileCCSSList = _autoliquidationNewDomain.GenerateCCSS(company, workCenterId, periodLiquidation, listLiquidation)

            Return FileCCSSList
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            FileCCSSList.StateResult = False
            FileCCSSList.Message = ex.Message.ToString
        End Try
    End Function
    ''' <summary>
    ''' Metodo para crear el archivo plano INS
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="policyNumber"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Public Function GenerateINS(companyId As Integer, policyNumber As String, workCenterId As Integer, periodLiquidation As String) As ActionMessageResult(Of StringBuilder) Implements IAutoliquidationAdminService.GenerateINSFile
        Dim FileINSList As New ActionMessageResult(Of StringBuilder)
        Try

            Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(workCenterId)
            Dim listLiquidation = _liquidationRepository.GetLiquidationByPeriodAndWorkCenter(periodLiquidation, workCenter.Code)
            Dim company As Company = _companyRepository.GetCompanyById(companyId)

            FileINSList = _autoliquidationNewDomain.GenerateINS(company, policyNumber, workCenter, periodLiquidation, listLiquidation)

            Return FileINSList
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            FileINSList.StateResult = False
            FileINSList.Message = ex.Message.ToString
        End Try
    End Function
    ''' <summary>
    ''' Genero el Excel de Autoliquidación
    ''' </summary>
    ''' <param name="PeriodLiquidation"></param>
    ''' <param name="WorkCenterId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GenerateReportAutoliquidation(PeriodLiquidation As String, WorkCenterId As Integer?, session As SessionValues) As DataSet Implements IAutoliquidationAdminService.GenerateReportAutoliquidation
        If PeriodLiquidation = String.Empty Then
            Throw New ArgumentNullException("PayrollDate")
        End If
        Try

            Dim CodeWorkCenter As String = String.Empty

            If WorkCenterId IsNot Nothing Then
                Dim ObjWorkCenter = _workCenterRepository.GetWorkCenterById(WorkCenterId)
                If ObjWorkCenter IsNot Nothing Then
                    CodeWorkCenter = ObjWorkCenter.Code
                End If
            End If

            Dim periodDateLiquidation As Date = New Date(PeriodLiquidation.Substring(0, 4), PeriodLiquidation.Substring(5, 2), 1)

            Dim PayrollDate As Date = DateSerial(periodDateLiquidation.Year, periodDateLiquidation.Month + 1, 0)

            Dim ds As New DataSet
            Dim query1 As String = String.Empty

            If PeriodLiquidation IsNot Nothing And CodeWorkCenter <> String.Empty Then
                query1 = "exec [Payroll].[SP_AutoliquidationFile] '" & PayrollDate & "'," & CodeWorkCenter & ""
            Else
                query1 = "exec [Payroll].[SP_AutoliquidationFile] '" & PayrollDate & "', NULL "
            End If

            Dim dt1 = Me.GetDatatable(query1, session, "Autoliquidation")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable

            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using

    End Function

    Public Function GenerateValidationAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFile_Result) Implements IAutoliquidationAdminService.GenerateValidationAutoliquidation
        Try

            Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(WorkCenterId)

            Dim ListObjAutoliquidation = _autoliquidationRepository.GenerateAutoliquidation(PayrollDateLiquidated, workCenter.Code)

            Return ListObjAutoliquidation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function
    ''' <summary>
    ''' Funcion para generar el Archico plano en CR
    ''' </summary>
    ''' <param name="WorkCenterId"></param>
    ''' <param name="PayrollDateLiquidated"></param>
    ''' <returns></returns>
    Public Function GenerateValidationAutoliquidationCR(WorkCenterId As Integer, PayrollDateLiquidated As Date) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFileCR) Implements IAutoliquidationAdminService.GenerateValidationAutoliquidationCR
        Try

            Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(WorkCenterId)


            Dim ListObjAutoliquidation = _autoliquidationRepository.ExecuteStoredProcedure(Of SP_AutoliquidationFileCR)("[Payroll].[SP_AutoliquidationFileCR]", {("@PayrollDate", PayrollDateLiquidated),
                                                                                                                                                                            ("@WorkCenterCode", workCenter.Code)}).ToList()


            Return ListObjAutoliquidation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function
    Public Function ListVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date) As List(Of VerifyAutoliquidationFile) Implements IAutoliquidationAdminService.ListVerifyAutoliquidation
        Try

            Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(WorkCenterId)

            Return _autoliquidationRepository.ListVerifyAutoliquidation(workCenter.Code, PayrollDateLiquidated)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function ConfirmVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, FlagConfirm As Boolean, SessionValues As SessionValues) As ActionResult(Of List(Of VerifyAutoliquidationFile)) Implements IAutoliquidationAdminService.ConfirmVerifyAutoliquidation

        Dim unitWork As IUnitWork = _verifyAutoliquidationRepository.UnitWork
        Dim resultActionResult As New ActionResult(Of List(Of VerifyAutoliquidationFile))

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)

            Try

                Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(WorkCenterId)

                Dim resultStore = Me._autoliquidationRepository.SP_ConfirmDisconfirmVerifyAutoliquidation(workCenter.Code, PayrollDateLiquidated, FlagConfirm, SessionValues.UserIndigo)
                If resultStore.CodeResult <> 0 Then
                    unitWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of List(Of VerifyAutoliquidationFile)) With {.StateResult = False, .MessageResult = {resultStore.MessageResult}.ToList, .Message = resultStore.MessageResult}
                End If

                transaction.Complete()
                Return New ActionResult(Of List(Of VerifyAutoliquidationFile)) With {.StateResult = True, .ObjectEmbbeded = Nothing, .Message = resultStore.MessageResult}
            Catch ex As Exception
                unitWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
                resultActionResult.StateResult = False
                resultActionResult.Message = ex.Message.ToString
            End Try
        End Using

        Return resultActionResult

    End Function

    Public Function SaveVerifyAutoliquidation(VerifyAutoliquidationFile As VerifyAutoliquidationFile) As ActionResult(Of VerifyAutoliquidationFile) Implements IAutoliquidationAdminService.SaveVerifyAutoliquidation

        Dim unitWork As IUnitWork = _verifyAutoliquidationRepository.UnitWork
        Dim resultActionResult As New ActionResult(Of VerifyAutoliquidationFile)

        Try
            _verifyAutoliquidationRepository.SaveEntity(VerifyAutoliquidationFile)

            unitWork.Commit()

            resultActionResult.StateResult = True
            resultActionResult.Message = "Se Guardó Correctamente"

        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            resultActionResult.StateResult = False
            resultActionResult.Message = ex.Message.ToString
        End Try

        Return resultActionResult

    End Function

    Public Function SaveMassiveVerifyAutoliquidation(ListImportFileRow As List(Of ImportFileRow), Audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAutoliquidationAdminService.SaveMassiveVerifyAutoliquidation

        If ListImportFileRow Is Nothing OrElse ListImportFileRow.Count = 0 Then
            Throw New ArgumentNullException("ListInfo")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Objeto Xml

                Dim xmlObject = ConvertToXmlBySave(ListImportFileRow)

                'Se envia la info al sp
                Dim resultStore = _autoliquidationRepository.SP_SaveMassiveVerifyAutoliquidation_Result(xmlObject, Audit.CodeUser)

                If resultStore Is Nothing OrElse resultStore.Count = 0 Then 'Si no hay datos
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "No se pudo guardar los registros"}
                End If

                If (From x In resultStore Where x.CodeResult = 888 Select x).Count > 0 Then 'Se valida si hay algun error de catch en el sql
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = (From x In resultStore Where x.CodeResult = 888 Select x.MessageResult).FirstOrDefault()}
                End If

                'Listado de errores y de oks
                Dim ListReturn As New List(Of Tuple(Of String, Integer))

                'Se recorre la info para armar el listado de ok o error
                resultStore.ForEach(Sub(x) ListReturn.Add(New Tuple(Of String, Integer)(x.MessageResult, IIf(x.CodeResult = 0, 1, 2))))

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListReturn}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.Message}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Metodo que me convierte el listado de datos en xml cuando es para guardar
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertToXmlBySave(ListInfo As List(Of ImportFileRow)) As String
        'String de xml que se arma con el listado
        Dim builder As StringBuilder = New StringBuilder()

        'Cantidad de item para validar 
        Dim count As Integer = 0

        builder.Append("<Data>")

        For Each info In ListInfo

            builder.Append("<Row>")
            builder.Append("<Id>" & info.Row(0) & "</Id>")
            builder.Append("<Nit>" & info.Row(2) & "</Nit>")
            builder.Append("<IBCOtrosParafiscales>" & info.Row(9) & "</IBCOtrosParafiscales>")
            builder.Append("<VSP>" & info.Row(10) & "</VSP>")
            builder.Append("<FechaInicioVSP>" & info.Row(11) & "</FechaInicioVSP>")
            builder.Append("<ValorVSP>" & info.Row(12) & "</ValorVSP>")
            builder.Append("<VST>" & info.Row(13) & "</VST>")
            builder.Append("<SLN>" & info.Row(14) & "</SLN>")
            builder.Append("<FechaInicioSLN>" & info.Row(15) & "</FechaInicioSLN>")
            builder.Append("<FechaFinalSLN>" & info.Row(16) & "</FechaFinalSLN>")
            builder.Append("<BaseCalculoSLN>" & info.Row(17) & "</BaseCalculoSLN>")
            builder.Append("<IBCSLN>" & info.Row(18) & "</IBCSLN>")
            builder.Append("<IGE>" & info.Row(19) & "</IGE>")
            builder.Append("<FechaInicioIGE>" & info.Row(20) & "</FechaInicioIGE>")
            builder.Append("<FechaFinalIGE>" & info.Row(21) & "</FechaFinalIGE>")
            builder.Append("<BaseCalculoIGE>" & info.Row(22) & "</BaseCalculoIGE>")
            builder.Append("<BaseIGE>" & info.Row(23) & "</BaseIGE>")
            builder.Append("<LMA>" & info.Row(24) & "</LMA>")
            builder.Append("<FechaInicioLMA>" & info.Row(25) & "</FechaInicioLMA>")
            builder.Append("<FechaFinLMA>" & info.Row(26) & "</FechaFinLMA>")
            builder.Append("<BaseCalculoLMA>" & info.Row(27) & "</BaseCalculoLMA>")
            builder.Append("<IBCLMA>" & info.Row(28) & "</IBCLMA>")
            builder.Append("<VAC>" & info.Row(29) & "</VAC>")
            builder.Append("<FechaInicioVacaciones>" & info.Row(30) & "</FechaInicioVacaciones>")
            builder.Append("<FechaFinVacaciones>" & info.Row(31) & "</FechaFinVacaciones>")
            builder.Append("<BaseCalculoVAC>" & info.Row(32) & "</BaseCalculoVAC>")
            builder.Append("<BaseLiqVAC>" & info.Row(33) & "</BaseLiqVAC>")
            builder.Append("<IRL>" & info.Row(34) & "</IRL>")
            builder.Append("<FechaInicioIRL>" & info.Row(35) & "</FechaInicioIRL>")
            builder.Append("<FechaFinIRL>" & info.Row(36) & "</FechaFinIRL>")
            builder.Append("<BaseCalculoIRL>" & info.Row(37) & "</BaseCalculoIRL>")
            builder.Append("<BaseIRL>" & info.Row(38) & "</BaseIRL>")
            builder.Append("<DiasAFP>" & info.Row(42) & "</DiasAFP>")
            builder.Append("<DiasEPS>" & info.Row(43) & "</DiasEPS>")
            builder.Append("<DiasARP>" & info.Row(44) & "</DiasARP>")
            builder.Append("<DiasCCF>" & info.Row(45) & "</DiasCCF>")
            builder.Append("<ValorVST>" & info.Row(48) & "</ValorVST>")
            builder.Append("<IBCAFP>" & info.Row(49) & "</IBCAFP>")
            builder.Append("<IBCEPS>" & info.Row(50) & "</IBCEPS>")
            builder.Append("<IBCARP>" & info.Row(51) & "</IBCARP>")
            builder.Append("<IBCCCF>" & info.Row(52) & "</IBCCCF>")
            builder.Append("<TarifaAFP>" & Replace(info.Row(53).ToString, ",", ".") & "</TarifaAFP>")
            builder.Append("<AporteAFP>" & info.Row(54) & "</AporteAFP>")
            builder.Append("<FSPSubcuentaSolidaridad>" & info.Row(55) & "</FSPSubcuentaSolidaridad>")
            builder.Append("<FSPSubcuentaSubsistencia>" & info.Row(56) & "</FSPSubcuentaSubsistencia>")
            builder.Append("<TarifaEPS>" & Replace(info.Row(57).ToString, ",", ".") & "</TarifaEPS>")
            builder.Append("<AporteEPS>" & info.Row(58) & "</AporteEPS>")
            builder.Append("<TarifaARP>" & Replace(info.Row(59).ToString, ",", ".") & "</TarifaARP>")
            builder.Append("<AporteARP>" & info.Row(61) & "</AporteARP>")
            builder.Append("<TarifaCCF>" & Replace(info.Row(62).ToString, ",", ".") & "</TarifaCCF>")
            builder.Append("<AporteCCF>" & info.Row(63) & "</AporteCCF>")
            builder.Append("<TarifaSENA>" & Replace(info.Row(64).ToString, ",", ".") & "</TarifaSENA>")
            builder.Append("<AporteSENA>" & info.Row(65) & "</AporteSENA>")
            builder.Append("<TarifaICBF>" & Replace(info.Row(66).ToString, ",", ".") & "</TarifaICBF>")
            builder.Append("<AporteICBF>" & info.Row(67) & "</AporteICBF>")
            builder.Append("<TarifaEspecialPensiones>" & info.Row(68) & "</TarifaEspecialPensiones>")
            builder.Append("<Observaciones>" & info.Row(69) & "</Observaciones>")


            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Public Function GetVerifyAutoliquidationFileById(IdVerifyAutoliquidationFile As Integer) As VerifyAutoliquidationFile Implements IAutoliquidationAdminService.GetVerifyAutoliquidationById
        Try
            Return _autoliquidationRepository.GetVerifyAutoliquidationById(IdVerifyAutoliquidationFile)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function



    ''' <summary>
    ''' Genera el archivo plano PILA AT2 invocando el microservicio de seguridad social.
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <param name="periodLiquidation">Periodo de liquidación en formato YYYY-MM</param>
    ''' <returns>Resultado con el contenido del archivo plano como StringBuilder</returns>
    Public Function GeneratePilaFile(companyId As Integer, periodLiquidation As String) As ActionMessageResult(Of StringBuilder) Implements IAutoliquidationAdminService.GeneratePilaFile
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim errorMessage As String = Nothing
        Dim bytes = ExecutePilaRequest(companyId, periodLiquidation, "api/v1/pila/generate", errorMessage)
        If bytes IsNot Nothing Then
            result.StateResult = True
            result.ObjectEmbbeded = New StringBuilder(Encoding.GetEncoding(1252).GetString(bytes))
        Else
            result.StateResult = False
            result.Message = errorMessage
        End If
        Return result
    End Function

    ''' <summary>
    ''' Genera el reporte Excel PILA invocando el microservicio de seguridad social.
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <param name="periodLiquidation">Periodo de liquidación en formato YYYY-MM</param>
    ''' <returns>Resultado con el contenido del archivo Excel como arreglo de bytes</returns>
    Public Function GeneratePilaExcel(companyId As Integer, periodLiquidation As String) As ActionMessageResult(Of Byte()) Implements IAutoliquidationAdminService.GeneratePilaExcel
        Dim result As New ActionMessageResult(Of Byte())
        Dim errorMessage As String = Nothing
        Dim bytes = ExecutePilaRequest(companyId, periodLiquidation, "api/v1/pila/excel", errorMessage)
        If bytes IsNot Nothing Then
            result.StateResult = True
            result.ObjectEmbbeded = bytes
        Else
            result.StateResult = False
            result.Message = errorMessage
        End If
        Return result
    End Function

    ''' <summary>
    ''' Método privado que centraliza la llamada al microservicio PILA.
    ''' Construye el request, obtiene la URL desde Security.Endpoints y retorna los bytes de la respuesta.
    ''' </summary>
    ''' <param name="companyId">Id de la empresa</param>
    ''' <param name="periodLiquidation">Periodo de liquidación en formato YYYY-MM</param>
    ''' <param name="path">Path del endpoint a invocar (ej: api/v1/pila/generate)</param>
    ''' <param name="errorMessage">Mensaje de error en caso de fallo</param>
    ''' <returns>Bytes de la respuesta, o Nothing si ocurrió un error</returns>
    Private Function ExecutePilaRequest(companyId As Integer, periodLiquidation As String, path As String, ByRef errorMessage As String) As Byte()
        Try
            Dim company As Company = _companyRepository.GetCompanyById(companyId)
            If company Is Nothing Then
                errorMessage = "No se encontró la empresa con Id: " & companyId
                Return Nothing
            End If

            Dim endpoint = _endpointsRepository.GetEndpointsByContainerCode(ServerSessionValues.Current.CurrentContainer, "social-security")
            If endpoint Is Nothing OrElse String.IsNullOrEmpty(endpoint.UrlBase) Then
                errorMessage = "No se encontró el endpoint 'social-security' para el container actual"
                Return Nothing
            End If

            Dim parts = periodLiquidation.Split("-"c)
            Dim requestBody = New With {
                .year = Integer.Parse(parts(0)),
                .month = Integer.Parse(parts(1)),
                .contributor_name = company.Name,
                .contributor_document_number = company.Nit,
                .contributor_document_type = If(company.ThirdParty IsNot Nothing AndAlso company.ThirdParty.Person IsNot Nothing, GetPilaDocumentType(company.ThirdParty.Person.IdentificationType), "NIT"),
                .verification_digit = If(company.ThirdParty IsNot Nothing AndAlso Not String.IsNullOrEmpty(company.ThirdParty.DigitVerification), company.ThirdParty.DigitVerification, "0"),
                .planilla_type = "E",
                .occupational_risk_insurance_code = If(company.Fund IsNot Nothing, company.Fund.Code, ""),
                .payment_period = periodLiquidation,
                .presentation_mode = "U",
                .contributor_type = "01",
                .operator_code = ""
            }

            Dim jsonContent As String = JsonConvert.SerializeObject(requestBody)
            Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12
            Net.ServicePointManager.ServerCertificateValidationCallback = Function(s, cert, chain, errors) True

            Using client As New HttpClient()
                Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")
                client.DefaultRequestHeaders.Add("x-container", ServerSessionValues.Current.CurrentContainer)
                Dim response = client.PostAsync($"{endpoint.UrlBase}/{path}", content).GetAwaiter().GetResult()

                If Not response.IsSuccessStatusCode Then
                    errorMessage = $"Error microservicio PILA: {CInt(response.StatusCode)} {response.StatusCode}"
                    Return Nothing
                End If

                Return response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult()
            End Using

        Catch ex As HttpRequestException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            errorMessage = "Error de conexión con microservicio PILA: " & ex.Message
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            errorMessage = ex.Message
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Mapea el tipo de identificación del ERP (0-15) al código PILA correspondiente.
    ''' </summary>
    ''' <param name="identificationType">Valor entero de Person.IdentificationType</param>
    ''' <returns>Código de tipo de documento para PILA (CC, CE, NIT, etc.)</returns>
    Private Shared Function GetPilaDocumentType(identificationType As Integer) As String
        Select Case identificationType
            Case 0 : Return "CC"
            Case 1 : Return "CE"
            Case 2 : Return "TI"
            Case 3 : Return "RC"
            Case 4 : Return "PA"
            Case 5 : Return "AS"
            Case 6 : Return "MS"
            Case 7 : Return "NIT"
            Case Else : Return "NIT"
        End Select
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _autoliquidationNewDomain.Dispose()
                _autoliquidationDomain.Dispose()
            End If
            _autoliquidationRepository = Nothing
            _companyRepository = Nothing
            _workCenterRepository = Nothing
            _liquidationRepository = Nothing
            _autoliquidationDomain = Nothing
            _autoliquidationNewDomain = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


