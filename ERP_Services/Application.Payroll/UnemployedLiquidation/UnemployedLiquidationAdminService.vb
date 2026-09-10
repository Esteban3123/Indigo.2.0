'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 05-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class UnemployedLiquidationAdminService
    Implements IUnemployedLiquidationAdminService

#Region "Repositories"

    ''' <summary>
    ''' Repositorio de liquidacion de cesantías
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationRepository As IUnemployedLiquidationRepository

    ''' <summary>
    ''' Repositorio de liquidacion de cesantías para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationRepositoryToSave As IUnemployedLiquidationRepository

    ''' <summary>
    ''' Repositorio del detalle de liquidacion de cesantias 
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationDetailRepository As IUnemployedLiquidationDetailRepository

    ''' <summary>
    ''' Repositorio de liquidacion de nomina
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' repositorio de empleados
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Dominio de liquidacion de cesantías
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationDomain As IUnemployedLiquidationDomain

#End Region

#Region "Constructor"
    Public Sub New(unemployedLiquidationRepository As IUnemployedLiquidationRepository, unemployedLiquidationRepositoryToSave As IUnemployedLiquidationRepository, unemployedLiquidationDetailRepository As IUnemployedLiquidationDetailRepository, liquidationRepository As IPayrollLiquidationRepository, employeeRepository As IEmployeeRepository, unemployedLiquidationDomain As IUnemployedLiquidationDomain)

        If unemployedLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("unemployedLiquidationRepository Vacío")
        End If

        If unemployedLiquidationRepositoryToSave Is Nothing Then
            Throw New ArgumentNullException("unemployedLiquidationRepositoryDelete Vacío")
        End If

        If liquidationRepository Is Nothing Then
            Throw New ArgumentNullException("liquidationRepository Vacío")
        End If

        If employeeRepository Is Nothing Then
            Throw New ArgumentNullException("employeeRepository Vacío")
        End If

        If unemployedLiquidationDomain Is Nothing Then
            Throw New ArgumentNullException("unemployedLiquidationDomain Vacío")
        End If

        If unemployedLiquidationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("unemployedLiquidationDetailRepository Vacío")
        End If

        _unemployedLiquidationRepository = unemployedLiquidationRepository
        _unemployedLiquidationRepositoryToSave = unemployedLiquidationRepositoryToSave
        _unemployedLiquidationDetailRepository = unemployedLiquidationDetailRepository
        _liquidationRepository = liquidationRepository
        _employeeRepository = employeeRepository
        _unemployedLiquidationDomain = unemployedLiquidationDomain
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que liquida las cesantías anuales
    ''' </summary>
    ''' <param name="EmployeeToLiquidate">Empleado a liquidar, si la liq es parcial</param>
    ''' <param name="ListGroups">Listado de grupos a liquidar</param>
    ''' <param name="InitialDate">Fecha inicio de cesantías</param>
    ''' <param name="EndingDate">Fecha Fin de cesantías</param>
    ''' <param name="Confirm">Si confirma la liquidación</param>
    ''' <param name="Sanction">Si se tienen en cuenta los días de sanción</param>
    ''' <param name="audit">Variable de auditoria</param>
    ''' <returns>el ActionMessageResult</returns>
    ''' <remarks></remarks>
    Public Function UnemployedLiquidation(EmployeeToLiquidate As Employee, ListGroups As List(Of Group), InitialDate As Date, EndingDate As Date, Confirm As Boolean, Sanction As Boolean,
                                          SessionValues As SessionValues, Optional AuthorizationDate As Date = Nothing, Optional UnemployedRetirementReason As String = "", Optional ResolutionNumber As String = "",
                                          Optional ByVal UnemployedInterestPaidWithPayroll As Boolean = 0) As ActionMessageResult(Of List(Of UnemployedLiquidation)) Implements IUnemployedLiquidationAdminService.UnemployedLiquidation

        Dim actionResult As New ActionMessageResult(Of List(Of UnemployedLiquidation))
        actionResult.StateResult = True
        Dim ContainerVie = SessionValues.TransactionalContainer

        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerVie, False))
            Try

                Dim list_Employees As List(Of Employee) = New List(Of Employee)
                Dim LiquidationType As Boolean 'false = anual, true = parcial

                Dim ListUnemployedLiquidationExist = _unemployedLiquidationRepository.GetUnemployedLiquidationByGroup(ListGroups.FirstOrDefault().Id, EndingDate, True)
                If ListUnemployedLiquidationExist IsNot Nothing AndAlso ListUnemployedLiquidationExist.Count > 0 Then
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("999", "No se puede liquidar este grupo de Cesantias porque ya fueron confirmadas"))
                    Return actionResult
                End If


                If EmployeeToLiquidate IsNot Nothing Then 'liq parcial
                    list_Employees.Add(EmployeeToLiquidate)
                    LiquidationType = True
                Else
                    For Each itemGr As Group In ListGroups 'liq anual, por grupo
                        Dim listEmployees As List(Of Employee) = _employeeRepository.GetEmployeeByGroup(itemGr.Id, False, InitialDate:=InitialDate, EndingDate:=EndingDate)
                        If listEmployees IsNot Nothing AndAlso listEmployees.Count > 0 Then
                            For Each itemEm As Employee In listEmployees
                                list_Employees.Add(itemEm)
                            Next
                        End If
                    Next
                    LiquidationType = False
                End If
                If Not (list_Employees.Count > 0) Then 'No hay empleados para liquidar
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("999", "No hay empleados para liquidar"))
                    Return actionResult
                End If

                Dim ListUnemployedLiquidation = _unemployedLiquidationRepository.GetUnemployedLiquidationByGroup(ListGroups.FirstOrDefault().Id, EndingDate, False)

                If ListUnemployedLiquidation IsNot Nothing AndAlso ListUnemployedLiquidation.Count > 0 Then

                    cnx.Open()

                    Dim DeleteSentenceConcept As String = "DELETE FROM " &
                            ContainerVie & ".Payroll.UnemployedConcept " &
                            "WHERE IdUnemployementLiquidation in (SELECT Id FROM " &
                            ContainerVie & ".Payroll.UnemployedLiquidation " &
                            "WHERE Status = 0 and GroupId = " & ListGroups.FirstOrDefault().Id & " and Year = " & EndingDate.Year & ")"

                    Dim commandDetailConcept As New System.Data.SqlClient.SqlCommand(DeleteSentenceConcept, cnx)

                    commandDetailConcept.CommandTimeout = 30000
                    commandDetailConcept.CommandType = CommandType.Text
                    commandDetailConcept.ExecuteNonQuery()

                    Dim DeleteSentenceDetail As String = "DELETE FROM " &
                            ContainerVie & ".Payroll.UnemployedLiquidationDetail " &
                            "WHERE UnemployedLiquidationId in (SELECT Id FROM " &
                            ContainerVie & ".Payroll.UnemployedLiquidation " &
                            "WHERE Status = 0 and GroupId = " & ListGroups.FirstOrDefault().Id & " and Year = " & EndingDate.Year & ")"

                    Dim commandDetail As New System.Data.SqlClient.SqlCommand(DeleteSentenceDetail, cnx)

                    commandDetail.CommandTimeout = 30000
                    commandDetail.CommandType = CommandType.Text
                    commandDetail.ExecuteNonQuery()

                    Dim DeleteSentence As String = "DELETE FROM " &
                               ContainerVie & ".Payroll.UnemployedLiquidation " &
                               "WHERE Status = 0 and GroupId = " & ListGroups.FirstOrDefault().Id & " and Year = " & EndingDate.Year

                    Dim command As New System.Data.SqlClient.SqlCommand(DeleteSentence, cnx)

                    command.CommandTimeout = 30000
                    command.CommandType = CommandType.Text
                    command.ExecuteNonQuery()

                    'Dim da As New System.Data.SqlClient.SqlDataAdapter(Command)

                    cnx.Close()
                End If

                actionResult = _unemployedLiquidationDomain.YearlyLiquidation(list_Employees, InitialDate, EndingDate, Confirm, LiquidationType, Sanction, AuthorizationDate, UnemployedRetirementReason, ResolutionNumber, , , "", "", 0, UnemployedInterestPaidWithPayroll)
                If actionResult Is Nothing OrElse actionResult.ObjectEmbbeded Is Nothing Then
                    actionResult.StateResult = False
                    actionResult.MessageResult.Add(New MessageResult("999", "No se generó Liquidación de Cesantias"))
                    Return actionResult
                End If

                For Each item In actionResult.ObjectEmbbeded 'recorro los registros de cesantías a guardar
                    If item.UnemployedLiquidationDetail.Count > 0 Then
                        item.EmployeeId = item.Employee.Id
                        Dim _employeeTemp As Employee = item.Employee
                        item.Employee = Nothing
                        item.Status = False
                        _unemployedLiquidationRepositoryToSave.SaveEntity(item)

                    End If
                Next
                _unemployedLiquidationRepositoryToSave.UnitWork.Commit()

                If actionResult.StateResult = False Then
                    Return actionResult
                End If

                'Coloco la entidad empleado en cada registro de cesantía, para mostrarlo en rejilla
                For Each item In actionResult.ObjectEmbbeded
                    item.Employee = list_Employees.Find(Function(x) x.Id = item.EmployeeId)
                    If item.Status = True AndAlso item.ConfirmLiquidated = True Then
                        '/***** Auditoria Basica ********/
                        IndigoAuditBasic.Execute(item.GetType.Name, SessionValues.AuditMessageWcf.Functional, item.Id, SessionValues.AuditMessageWcf.NameUser, SessionValues.AuditMessageWcf.CodeUser, SessionValues.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Crear, SessionValues.AuditMessageWcf.Company, SessionValues.AuditMessageWcf.ContainerSecurity)
                        '/*****Auditoria Avanzada ******/
                        Dim auditObject As New IndigoAuditSimpleEntity(Of UnemployedLiquidation)(item, SessionValues.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Insert)
                        auditObject.Execute()
                    End If
                Next

                Return actionResult
            Catch ex As Exception
                cnx.Close()
                _unemployedLiquidationRepositoryToSave.UnitWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
                actionResult.StateResult = False
                actionResult.MessageResult.Add(New MessageResult("-999: Error", ex.Message.ToString()))
                Return actionResult
            End Try
        End Using

    End Function

    Public Function ConfirmUnemployedLiquidation(ListUnemploymentLiquidation As List(Of UnemployedLiquidation), SessionValues As SessionValues) As ActionResult(Of List(Of UnemployedLiquidation)) Implements IUnemployedLiquidationAdminService.ConfirmUnemployedLiquidation

        If ListUnemploymentLiquidation Is Nothing Then
            Throw New ArgumentNullException("ListUnemploymentLiquidation")
        End If

        Dim unitOfWork As IUnitWork = Me._unemployedLiquidationRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim GroupId As Integer
                Dim PeriodInitialDate As Date
                Dim PeriodEndDate As Date

                Dim ObjIncentivePayment = ListUnemploymentLiquidation.FirstOrDefault()

                GroupId = ObjIncentivePayment.GroupId
                PeriodInitialDate = ObjIncentivePayment.UnemployedInitialDate
                PeriodEndDate = ObjIncentivePayment.UnemployedEndingDate

                Dim ListMessage As New List(Of String)
                Dim resultStore = Me._unemployedLiquidationRepository.SP_GenerateJournalVouchersUnemployment(GroupId, PeriodInitialDate, PeriodEndDate, SessionValues.UserIndigo)

                If resultStore.Count = 0 Or resultStore.Any(Function(r) r.CodeMessage <> 0) Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    ListMessage = resultStore.Where(Function(r) r.CodeMessage <> 0).Select(Function(r) r.Message).ToList()
                    Return New ActionResult(Of List(Of UnemployedLiquidation)) With {.StateResult = False, .MessageResult = ListMessage}
                End If
                ListMessage = resultStore.Select(Function(r) r.Message).ToList()
                transaction.Complete()
                Return New ActionResult(Of List(Of UnemployedLiquidation)) With {.StateResult = True, .ObjectEmbbeded = ListUnemploymentLiquidation, .Message = ListMessage.FirstOrDefault()}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of List(Of UnemployedLiquidation)) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
                Return New ActionResult(Of List(Of UnemployedLiquidation)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que consulta las liquidaciones de grupos o empleados
    ''' </summary>
    ''' <param name="EmployeeToConsult">empleado a consultar</param>
    ''' <param name="ListGroups">listado de grupos a consultar</param>
    ''' <param name="Period">periodo</param>
    ''' <returns>los registros de liquidación de cesantías</returns>
    ''' <remarks></remarks>
    Public Function ConsultUnemployedLiquidation(EmployeeToConsult As Employee, ListGroups As List(Of Group), Optional Period As Integer = 0) As List(Of UnemployedLiquidation) Implements IUnemployedLiquidationAdminService.ConsultUnemployedLiquidation
        Try
            Dim ListUnemployedLiquidationToReturn As List(Of UnemployedLiquidation) = New List(Of UnemployedLiquidation)()

            '**************Obtengo el listado de los empleados a consultar******************'
            Dim list_Employees As List(Of Employee) = New List(Of Employee)
            If EmployeeToConsult IsNot Nothing Then 'liq parcial
                list_Employees.Add(EmployeeToConsult)
            Else
                For Each itemGr As Group In ListGroups 'liq anual, por grupo
                    Dim listEmployees As List(Of Employee) = _employeeRepository.GetEmployeeByGroup(itemGr.Id, False)
                    If listEmployees IsNot Nothing AndAlso listEmployees.Count > 0 Then
                        For Each itemEm As Employee In listEmployees
                            list_Employees.Add(itemEm)
                        Next
                    End If
                Next
            End If

            '***************Obtengo la fecha de consulta de las cesantías, si no hay periodo, son todas las cesantías existentes**************'
            Dim InitialDate As Date
            Dim EndingDate As Date
            If Period = 0 Then
                InitialDate = New Date(1, 1, 1)
                EndingDate = New Date(9999, 12, 31)
            Else
                InitialDate = New Date(Period, 1, 1)
                EndingDate = New Date(Period, 12, 31)
            End If

            '********teniendo el listado de empleados, mando a consultar*******'
            If list_Employees.Count > 0 Then
                For Each itemEmployee In list_Employees
                    Dim tempUnemLiq As List(Of UnemployedLiquidation) = _unemployedLiquidationRepository.GetUnemployedLiquidationEmployeeByDate(itemEmployee.Id, InitialDate, EndingDate)
                    If tempUnemLiq.Count > 0 Then
                        For Each itemUneLiq In tempUnemLiq
                            ListUnemployedLiquidationToReturn.Add(itemUneLiq)
                        Next
                    End If
                Next
            End If
            Return ListUnemployedLiquidationToReturn
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación por Clase de Concepto y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="conceptClass">Clase del Concepto</param>
    ''' <returns>List(Of LiquidationDetail)</returns>
    Public Function GetLiquidationDetailByContractIdConceptClass(ByVal Year As Integer, InitialContractNumber As Integer, conceptClass As String) As List(Of LiquidationDetail) Implements IUnemployedLiquidationAdminService.GetLiquidationDetailByContractIdConceptClass
        If String.IsNullOrEmpty(Year) Then
            Throw New ArgumentNullException("Year Vacio")
        End If
        If String.IsNullOrEmpty(InitialContractNumber) Then
            Throw New ArgumentNullException("InitialContractNumber Vacio")
        End If
        If String.IsNullOrEmpty(conceptClass) Then
            Throw New ArgumentNullException("conceptClass Vacio")
        End If
        Try
            Return _unemployedLiquidationRepository.GetLiquidationDetailByContractIdConceptClass(Year, InitialContractNumber, conceptClass)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of LiquidationDetail)
        End Try
    End Function

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación si Afecta para Cesantias y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="AffectUnemployement">Afecta Cesantías</param>
    ''' <returns> List(Of LiquidationDetail)</returns>
    Public Function GetLiquidationDetailByContractIdConceptAffectUnemployement(ByVal Year As Integer, InitialContractNumber As Integer, AffectUnemployement As Boolean) As List(Of LiquidationDetail) Implements IUnemployedLiquidationAdminService.GetLiquidationDetailByContractIdConceptAffectUnemployement
        If String.IsNullOrEmpty(Year) Then
            Throw New ArgumentNullException("Year Vacio")
        End If
        If String.IsNullOrEmpty(InitialContractNumber) Then
            Throw New ArgumentNullException("InitialContractNumber Vacio")
        End If
        If String.IsNullOrEmpty(AffectUnemployement) Then
            Throw New ArgumentNullException("AffectUnemployement Vacio")
        End If
        Try
            Return _unemployedLiquidationRepository.GetLiquidationDetailByContractIdConceptAffectUnemployement(Year, InitialContractNumber, AffectUnemployement)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of LiquidationDetail)
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _unemployedLiquidationDomain.Dispose()
            End If
            _unemployedLiquidationRepository = Nothing
            _unemployedLiquidationRepositoryToSave = Nothing
            _unemployedLiquidationDetailRepository = Nothing
            _liquidationRepository = Nothing
            _employeeRepository = Nothing
            _unemployedLiquidationDomain = Nothing
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
