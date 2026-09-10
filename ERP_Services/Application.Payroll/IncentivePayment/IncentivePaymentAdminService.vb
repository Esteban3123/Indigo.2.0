'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports System.Transactions
Imports System.Data.Entity.Core

Public Class IncentivePaymentAdminService
    Implements IIncentivePaymentAdminService


    'Repositorio de Centros de Estudio
    Private _IncentivePaymentRepository As IIncentivePaymentRepository

    ''' <summary>
    ''' Dominio de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _incentivePaymentDomain As Domain.Payroll.IIncentivePaymentDomain


    ''' <summary>
    ''' Repositorio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquitationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de Grupo
    ''' </summary>
    ''' <remarks></remarks>
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Repositorio de Bancos
    ''' </summary>
    ''' <remarks></remarks>
    Private _BankRepository As IBankRepository

    ''' <summary>
    ''' Repositorio de Empresas
    ''' </summary>
    ''' <remarks></remarks>
    Private _CompanyRepository As ICompanyRepository

    ''' <summary>
    ''' Repositorio de Parámetros de Nomina
    ''' </summary>
    ''' <remarks></remarks>
    Private _payrollSettings As IPayrollSettingsRepository

    ''' <summary>
    ''' inicia el repositorio de Primas
    ''' </summary>
    ''' <param name="repository">Repositorio de Centros de estudios</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IIncentivePaymentRepository, incentivePaymentDomain As Domain.Payroll.IIncentivePaymentDomain, liquidationRepository As IPayrollLiquidationRepository, groupRepository As IGroupRepository, BankRepository As IBankRepository, CompanyRepository As ICompanyRepository, payrollSettingsRepository As IPayrollSettingsRepository)
        If (repository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Primas vacio")
        End If
        _IncentivePaymentRepository = repository
        _incentivePaymentDomain = incentivePaymentDomain
        _liquitationRepository = liquidationRepository
        _groupRepository = groupRepository
        _BankRepository = BankRepository
        _CompanyRepository = CompanyRepository
        _payrollSettings = payrollSettingsRepository
    End Sub

    ''' <summary>
    ''' Obtiene una lista de Primas por Fecha Inicio, Fecha Fin y Id del Grupo
    ''' </summary>
    ''' <param name="StrGroupId">Id del Grupo</param>
    ''' <param name="MaxPremiumByYear">Máximo Primas por Año</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="VarYear">Año</param>
    ''' <returns>Lista de Primas</returns>
    ''' <remarks></remarks>
    Public Function GetIncentivePaymentByPeriodGroupId(StrGroupId As String, MaxPremiumByYear As Byte, period As Char, VarYear As Integer, SessionValues As SessionValues, Status As Byte) As ActionMessageResult(Of List(Of IncentivePayment)) Implements IIncentivePaymentAdminService.GetIncentivePaymentByPeriodGroupId

        Dim ListIncentivePayment As New List(Of IncentivePayment)
        Dim ReturnListIncentivePayment As New ActionMessageResult(Of List(Of IncentivePayment))
        Dim VarListMessageResult As New List(Of MessageResult)
        Dim ArrListIncentivePayment As New List(Of IncentivePayment)

        If String.IsNullOrEmpty(StrGroupId) Then
            Throw New ArgumentNullException("Id del Grupo Vacio")
        End If
        If String.IsNullOrEmpty(MaxPremiumByYear) Then
            Throw New ArgumentNullException("Máximo Primas Año Vacío")
        End If
        If String.IsNullOrEmpty(period) Then
            Throw New ArgumentNullException("Periodo Vacío")
        End If
        Try
            Dim ArrayGroups() As String = Split(StrGroupId, ",")
            Dim groupId As String

            'Se obtiene el valor de MaxPremiumYear
            Dim IncentivePayment = _groupRepository.GetGroupById(StrGroupId)
            MaxPremiumByYear = CType(IncentivePayment.PayrollParameter.MaxPremiumByYear, Byte)

            Dim DatesDictionary = _incentivePaymentDomain.GetInitialEndDatePeriod(MaxPremiumByYear, period, VarYear, SessionValues.IndigoCompanyType)

            Dim StarDate = DatesDictionary("InitialDate")
            Dim EndDate = DatesDictionary("EndDate")

            For G As Integer = 0 To (ArrayGroups.Count() - 1)
                groupId = ArrayGroups.GetValue(G)
                ListIncentivePayment = _IncentivePaymentRepository.GetIncentivePaymentByPayrollDateGroupId(groupId, StarDate, EndDate, 2)

                If ListIncentivePayment IsNot Nothing Then
                    For i As Integer = 0 To ListIncentivePayment.Count() - 1
                        ArrListIncentivePayment.Add(ListIncentivePayment.Item(i))
                    Next

                    ReturnListIncentivePayment.ObjectEmbbeded = ArrListIncentivePayment
                Else
                    'No hay liquidación
                    VarListMessageResult.Add(New MessageResult("-002"))
                    ReturnListIncentivePayment.MessageResult = VarListMessageResult
                End If

            Next

            Return ReturnListIncentivePayment

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar Primas
    ''' </summary>
    ''' <param name="incentivePayment">Objeto Primas</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteIncentivePayment(incentivePayment As IncentivePayment, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IIncentivePaymentAdminService.DeleteIncentivePayment
        Dim unitWork As IUnitWork = _IncentivePaymentRepository.UnitWork
        Try
            incentivePayment.MarkAsDeleted()
            _IncentivePaymentRepository.DeleteEntity(incentivePayment)
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Confirma las primas
    ''' </summary>
    ''' <param name="listIncentivePayment"></param>
    ''' <param name="SessionValues"></param>
    ''' <returns></returns>
    Public Function SaveIncentivePayment(listIncentivePayment As List(Of IncentivePayment), SessionValues As SessionValues) As ActionResult(Of List(Of IncentivePayment)) Implements IIncentivePaymentAdminService.SaveIncentivePayment
        If listIncentivePayment Is Nothing Then
            Throw New ArgumentNullException("listIncentivePayment")
        End If

        Dim unitOfWork As IUnitWork = Me._IncentivePaymentRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                Dim GroupId As Integer
                Dim PeriodInitialDate As Date
                Dim PeriodEndDate As Date
                Dim Period As Integer

                Dim ObjIncentivePayment = listIncentivePayment.FirstOrDefault()

                'Listado de mensajes
                Dim ListMessage As New List(Of String)

                GroupId = ObjIncentivePayment.GroupId
                PeriodInitialDate = ObjIncentivePayment.PeriodInitialDate
                PeriodEndDate = ObjIncentivePayment.PeriodEndDate
                Period = ObjIncentivePayment.Period

                Dim resultStore = Me._IncentivePaymentRepository.SP_GenerateJournalVouchersIncentivePayment(GroupId, PeriodInitialDate, PeriodEndDate, Period, SessionValues.UserIndigo)
                If resultStore IsNot Nothing AndAlso resultStore.Any(Function(x) x.CodeMessage <> 0) Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    ListMessage = resultStore.Where(Function(x) x.CodeMessage <> 0).Select(Function(x) x.Message).ToList()
                    Return New ActionResult(Of List(Of IncentivePayment)) With {.StateResult = False, .MessageResult = ListMessage}
                End If
                ListMessage = resultStore.Select(Function(r) r.Message).ToList()
                transaction.Complete()
                Return New ActionResult(Of List(Of IncentivePayment)) With {.StateResult = True, .ObjectEmbbeded = listIncentivePayment, .MessageResult = ListMessage}

            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of List(Of IncentivePayment)) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", SessionValues)
                Return New ActionResult(Of List(Of IncentivePayment)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Calcula las Primas de un Grupo o Grupos
    ''' </summary>
    ''' <param name="strGroupId">String de ID's de Grupos</param>
    ''' <param name="period">Periodo o Semestre de la Prima</param>
    ''' <param name="MaxPremiumByYear"># de Primas por Año del Grupo</param>
    ''' <param name="employeeNit">Nit del Empleado</param>
    ''' <param name="valueExtraIncentivePayment">Valor de Prima Extra</param>
    ''' <param name="offset">Índice de inicio del lote (-1 = modo legacy, 0 = primer lote, >0 = lotes siguientes)</param>
    ''' <returns>Action Result de Primas (Lista)</returns>
    ''' <remarks></remarks>
    Public Function CalculateIncentivePayment(strGroupId As String, period As Char, MaxPremiumByYear As Byte, VarYear As Integer, PaymentType As Char, SessionValues As SessionValues, Optional employeeNit As String = "", Optional valueExtraIncentivePayment As Double = 0, Optional employeeList As List(Of Employee) = Nothing, Optional offset As Integer = -1) As ActionMessageResult(Of List(Of IncentivePayment)) Implements IIncentivePaymentAdminService.CalculateIncentivePayment

        Dim unitWorkSave As IUnitWork = _IncentivePaymentRepository.UnitWork
        Dim ContainerGenesis = SessionValues.TransactionalContainer

        Dim PayrollSettings = _payrollSettings.GetSettingPayroll()
        ' Obteniendo fechas de PayrollParameter
        Dim StarDate As Date?
        Dim EndDate As Date?
        Dim _groupinfo = _groupRepository.GetGroupById(strGroupId)

        If period = "1" Then
            StarDate = _groupinfo.PayrollParameter.StartDateIncentivePayment1
            EndDate = _groupinfo.PayrollParameter.EndDateIncentivePayment1
        Else
            StarDate = _groupinfo.PayrollParameter.StartDateIncentivePayment2
            EndDate = _groupinfo.PayrollParameter.EndDateIncentivePayment2
        End If

        Dim connectionString = Utils.GetEntityConnectionString(ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False)

        Dim cnx As New SqlClient.SqlConnection(connectionString)

        Try

            cnx.Open()
            Dim txSettings As New TransactionOptions()
            Dim MaxTimeOut As New TimeSpan(1, 30, 0)
            txSettings.Timeout = MaxTimeOut
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted

            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)

                Dim groupId As String
                Dim actionResult As New ActionMessageResult(Of List(Of IncentivePayment))
                Dim VarListMessageResult As New List(Of MessageResult)

                Dim ArrListIncentivePayment As New List(Of IncentivePayment)
                Dim ListConsultIncentivePayment As List(Of IncentivePayment)

                If StarDate Is Nothing Or EndDate Is Nothing Then 'Si no hay valores de PayrollParameter toma los de PayrollSettings
                    If PayrollSettings Is Nothing Then
                        actionResult.StateResult = False
                        actionResult.Message = "No se ha parametrizado los Parámetros de Nómina"
                        Return actionResult
                    End If

                    If period = "1" Then
                        If PayrollSettings.ServicesIncentivePaymentConceptId Is Nothing Then
                            actionResult.StateResult = False
                            actionResult.Message = "No se ha parametrizado El Concepto de Pago de Primas de Junio en Parámetros de Nómina"
                            Return actionResult
                        End If
                    Else
                        If PayrollSettings.ChristmasIncentivePaymentConceptId Is Nothing Then
                            actionResult.StateResult = False
                            actionResult.Message = "No se ha parametrizado El Concepto de Pago de Primas de Diciembre en Parámetros de Nómina"
                            Return actionResult
                        End If
                    End If

                    If period = "1" Then
                        StarDate = PayrollSettings.InitialDateServicesIncentivePayment
                        EndDate = PayrollSettings.EndDateServicesIncentivePayment
                    Else
                        StarDate = PayrollSettings.InitialDateChristmasIncentivePayment
                        EndDate = PayrollSettings.EndDateChristmasIncentivePayment
                    End If
                End If
                Dim ArrayGroups() As String = Split(strGroupId, ",")

                For G As Integer = 0 To (ArrayGroups.Count() - 1)
                    groupId = ArrayGroups.GetValue(G)
                    Dim group = _groupRepository.GetGroupById(groupId)
                    Dim PayrollNextDate = group.NextDateLiquidation
                    Dim ListIncentivePayment = _IncentivePaymentRepository.GetIncentivePaymentByPayrollDateGroupId(groupId, StarDate, EndDate, 2)

                    If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
                        VarListMessageResult.Add(New MessageResult("-001"))
                        actionResult.MessageResult = VarListMessageResult
                        actionResult.StateResult = False
                        Return actionResult
                    Else
                        'Verificacion registros previos
                        Dim previousCount = _IncentivePaymentRepository.GetIncentivePaymentCountByDateGroup(
                            groupId,
                            StarDate.Value,
                            EndDate.Value,
                            1,      ' Status = 1 (En liquidación - sin confirmar)
                            period
                        )
                        ' Solo borrar si:
                        ' - Hay registros previos (previousCount > 0)
                        ' - offset = -1 (modo legacy) O offset = 0 (primer lote)
                        If previousCount > 0 AndAlso (offset = -1 OrElse offset = 0) Then
                            Try
                                ''Borra detalles
                                Dim DeleteSentenceDetail As String =
                                    "DELETE FROM " & ContainerGenesis & ".Payroll.IncentivePaymentDetail " &
                                    "WHERE IncentivePaymentId IN (SELECT Id FROM " & ContainerGenesis & ".Payroll.IncentivePayment " &
                                    "WHERE GroupId = " & groupId & " " &
                                    "AND CAST(PeriodInitialDate AS DATE) = '" & StarDate.Value.ToString("yyyy-MM-dd") & "' " &
                                    "AND CAST(PeriodEndDate AS DATE) = '" & EndDate.Value.ToString("yyyy-MM-dd") & "' " &
                                    "AND RegisterStatus = 1 " &
                                    "AND Period = " & period & ")"

                                Dim commandDetail As New SqlClient.SqlCommand(DeleteSentenceDetail, cnx)
                                commandDetail.CommandTimeout = 30000
                                commandDetail.CommandType = CommandType.Text

                                Dim deletedDetails As Integer = commandDetail.ExecuteNonQuery()

                                'Borra encabezado
                                Dim DeleteSentence As String =
                                    "DELETE FROM " & ContainerGenesis & ".Payroll.IncentivePayment " &
                                    "WHERE GroupId = " & groupId & " " &
                                    "AND CAST(PeriodInitialDate AS DATE) = '" & StarDate.Value.ToString("yyyy-MM-dd") & "' " &
                                    "AND CAST(PeriodEndDate AS DATE) = '" & EndDate.Value.ToString("yyyy-MM-dd") & "' " &
                                    "AND RegisterStatus = 1 " &
                                    "AND Period = " & period

                                Dim command As New SqlClient.SqlCommand(DeleteSentence, cnx)
                                command.CommandTimeout = 30000
                                command.CommandType = CommandType.Text

                                Dim deletedHeaders As Integer = command.ExecuteNonQuery()
                            Catch ex As Exception
                                Throw
                            End Try

                        End If
                        ' Si viene employeeList desde Presentation, úsala (patrón nómina con lotes)
                        ' Si no, procesa todo el grupo (modo legacy)
                        actionResult = _incentivePaymentDomain.IncentivePaymentCalculate(group, PaymentType, period, StarDate, EndDate, SessionValues, Nothing, Nothing, False, "", "", 0, employeeList)

                        If actionResult.ObjectEmbbeded Is Nothing Then
                            VarListMessageResult.Add(New MessageResult(actionResult.Message))
                            actionResult.MessageResult = VarListMessageResult
                            actionResult.StateResult = False
                            Return actionResult
                        End If

                        ListConsultIncentivePayment = actionResult.ObjectEmbbeded

                        ' Guardar todo el lote (sin commits parciales internos)
                        For i As Integer = 0 To ListConsultIncentivePayment?.Count() - 1
                            ArrListIncentivePayment.Add(ListConsultIncentivePayment.Item(i))
                            ListConsultIncentivePayment.Item(i).ChangeTracker.State = ObjectState.Added
                            _IncentivePaymentRepository.SaveEntity(ListConsultIncentivePayment.Item(i))
                        Next
                        ' Commit único al final de este lote
                        unitWorkSave.Commit()
                        actionResult.ObjectEmbbeded = ArrListIncentivePayment
                    End If
                Next
                scope.Complete()
                If actionResult.ObjectEmbbeded.Count() > 0 Then
                    'Se liquidaron correctamente pero no fue confirmada
                    VarListMessageResult.Add(New MessageResult("002"))
                    actionResult.MessageResult = VarListMessageResult
                    actionResult.StateResult = True
                    Return actionResult
                Else
                    'No liquidó nada
                    VarListMessageResult.Add(New MessageResult("-002"))
                    actionResult.MessageResult = VarListMessageResult
                    actionResult.StateResult = True
                    Return actionResult
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionMessageResult(Of List(Of IncentivePayment))
        Finally
            If cnx IsNot Nothing AndAlso cnx.State = ConnectionState.Open Then
                cnx.Close()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Función que selecciona el Banco y arma el archivo Plano para Bancos
    ''' </summary>
    ''' <param name="ListIncentivePayment">Lista de Primas</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionMessageResult(StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function GenerateBankFile(PeriodEndDate As Date, BankId As Integer, AccountNumber As String, AccountType As Integer, CompanyId As Integer) As ActionMessageResult(Of StringBuilder) Implements IIncentivePaymentAdminService.GenerateBankFile

        Dim VarListMessageResult As New List(Of MessageResult)
        Dim BankFile As New ActionMessageResult(Of StringBuilder)
        Dim ListIncentivePayment As List(Of IncentivePayment)

        ListIncentivePayment = _IncentivePaymentRepository.GetListIncentivePaymentBankFile(PeriodEndDate)
        Dim Bank = _BankRepository.GetBankById(BankId, False)
        Dim Company = _CompanyRepository.GetCompanyById(CompanyId)

        If ListIncentivePayment IsNot Nothing Then
            ListIncentivePayment = ListIncentivePayment.Where(Function(x) x.Contract.BankId = BankId).ToList()
            BankFile = _incentivePaymentDomain.SelectBankEmployee(ListIncentivePayment, Bank, AccountNumber, AccountType, Company)
            BankFile.StateResult = True
        Else
            BankFile.StateResult = False
        End If

        Return BankFile

    End Function

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConfirmIncentivenDates() As List(Of Date) Implements IIncentivePaymentAdminService.GetConfirmIncentivenDates
        Return _IncentivePaymentRepository.GetConfirmIncentivenDates()
    End Function


    Public Function GetHeadIncentivePayment(Year As Integer, Period As Integer) As List(Of IncentivePayment) Implements IIncentivePaymentAdminService.GetHeadIncentivePayment

        Dim EndDatePeriod As Date

        If Period = 1 Then
            EndDatePeriod = New Date(Year, 6, 30)
        Else
            EndDatePeriod = New Date(Year, 12, 31)
        End If

        Dim ListGroups = _groupRepository.ListAllGroups()
        Dim ListIncentivePayment As New List(Of IncentivePayment)

        For Each G As Group In ListGroups

            Dim HeadLiquidation = _IncentivePaymentRepository.GetHeadIncentivePayment(G.Id, EndDatePeriod, Period)

            If HeadLiquidation IsNot Nothing Then
                ListIncentivePayment.AddRange(HeadLiquidation)
            End If

        Next

        Return ListIncentivePayment

    End Function

    Public Function GetDetailIncentivePayment(GroupId As Integer, Year As Integer, Period As Integer) As List(Of IncentivePayment) Implements IIncentivePaymentAdminService.GetDetailIncentivePayment
        Dim EndDatePeriod As Date

        If Period = 1 Then
            EndDatePeriod = New Date(Year, 6, 30)
        Else
            EndDatePeriod = New Date(Year, 12, 31)
        End If


        Dim HeadLiquidation = _IncentivePaymentRepository.GetDetailIncentivePaymentDetail(GroupId, EndDatePeriod, Period)

        If HeadLiquidation IsNot Nothing AndAlso HeadLiquidation.Count > 0 Then
            Return HeadLiquidation
        Else
            Return Nothing
        End If

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _incentivePaymentDomain.Dispose()
            End If
            _IncentivePaymentRepository = Nothing
            _incentivePaymentDomain = Nothing
            _liquitationRepository = Nothing
            _groupRepository = Nothing
            _BankRepository = Nothing
            _CompanyRepository = Nothing
            _payrollSettings = Nothing
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
