'***********************************************************************
' Assembly         : Application.Accounting.PUCAdminService
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 02-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Events.Models
Imports System.Dynamic
Imports System.Configuration
Imports Infrastructure.CrossCutting.Queue

#End Region
Public Class PUCAdminService
    Implements IPUCAdminService, Inject

    Private _IPUCRepository As IPUCRepository
    Private _ILevelRepository As IAccountLevelRepository
    Private _IThirdPartyRepository As IThirdPartyRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue
    Private _StateEntity As Boolean
    Private dt1 As DataTable

#Region "Builder"
    Public Sub New(ByVal pucRepository As Domain.Entities.IPUCRepository, levelRepository As IAccountLevelRepository, IThirdPartyRepository As IThirdPartyRepository, FactoryQueue As IFactoryQueue)
        If pucRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio vacio")
        End If
        _IPUCRepository = pucRepository
        _ILevelRepository = levelRepository
        _IThirdPartyRepository = IThirdPartyRepository
        _factoryQueue = FactoryQueue
        _StateEntity = False
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Funcion para eliminar las cuentas contables
    ''' </summary>
    ''' <param name="accounting">The accounting.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteAccounting(accounting As MainAccounts, audit As AuditMessage) As ActionResult Implements IPUCAdminService.DeleteAccounting
        If accounting Is Nothing Then
            Throw New ArgumentNullException("accounting")
        End If
        Dim unitOfWork As IUnitWork = Me._IPUCRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of MainAccounts)
            auditProcess = New IndigoAuditSimpleEntity(Of MainAccounts)(accounting, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._IPUCRepository.DeleteEntity(accounting)
            unitOfWork.Commit()
            auditProcess.Execute()

            accounting.MarkAsDeleted()
            TriggerEvent(accounting, audit)

            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Indica si la cuenta contable maneja centro de costo
    ''' </summary>
    ''' <param name="idAccount"></param>
    ''' <returns></returns>
    Private Function MainAccountHandlesCostCenter(idAccount As Integer) As Boolean Implements IPUCAdminService.MainAccountHandlesCostCenter
        Dim account = _IPUCRepository.GetAccountById(idAccount, False)
        If account?.HandlesCostCenter Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Indica si la cuenta contable maneja tercero
    ''' </summary>
    ''' <param name="idAccount"></param>
    ''' <returns></returns>
    Public Function MainAccountHandlesThirdParty(idAccount As Integer) As Boolean Implements IPUCAdminService.MainAccountHandlesThirdParty
        Return (_IPUCRepository.GetAccountById(idAccount, False)?.HandlesThirdParty).GetValueOrDefault()
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountById(id As Integer, tracking As Boolean) As MainAccounts Implements IPUCAdminService.GetAccountById
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("id Vacio")
        End If
        Try
            Return _IPUCRepository.GetAccountById(id, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MainAccounts()
        End Try
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountByCode(code As String, tracking As Boolean) As MainAccounts Implements IPUCAdminService.GetAccountByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("id Vacio")
        End If
        Try
            Return _IPUCRepository.GetAccountByCode(code, tracking)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MainAccounts()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una cuenta contable por codigo y libro oficial
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="LegalBookId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountByCodeAndLegalBookId(code As String, LegalBookId As Integer) As MainAccounts Implements IPUCAdminService.GetAccountByCodeAndLegalBookId
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code Vacio")
        End If
        If LegalBookId = 0 Then
            Throw New ArgumentNullException("LegalBookId Vacío")
        End If
        Try
            Return _IPUCRepository.GetAccountByCodeAndLegalBookId(code, LegalBookId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New MainAccounts()
        End Try
    End Function

    ''' <summary>
    ''' Funcion para guardar la cuenta contable
    ''' </summary>
    ''' <param name="mainAccount"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function Sp_InsertPUC(mainAccount As MainAccounts, audit As AuditMessage) As ActionResult(Of MainAccounts) Implements IPUCAdminService.Sp_InsertPUC
        Dim UnitOfWork As IUnitWork = _IPUCRepository.UnitWork
        Dim codeTemp As String = ""
        Dim _AccounTem As MainAccounts = Nothing
        Dim mainAccountToTrigger As MainAccounts = Nothing
        Dim level = _ILevelRepository.GetAccountLevelById(mainAccount.IdAccountLevel)
        If mainAccount.Number.Length > 1 Then
            If level.digits >= 4 Then
                mainAccount.AllowsMovement = True
            Else
                mainAccount.AllowsMovement = False
            End If
            codeTemp = mainAccount.Number.Substring(0, level.digits - level.Length)
            _AccounTem = _IPUCRepository.ValidateAccountParentByCodeAndLegalBookId(codeTemp, mainAccount.LegalBookId)
            If _AccounTem Is Nothing Then
                UnitOfWork.RollbackChanges()
                Return New ActionResult(Of MainAccounts) With {.StateResult = False, .MessageResult = {"a-0001"}.ToList()}
            End If
        End If

        If mainAccount.Id = 0 Then
            _StateEntity = True
        End If

        Using Transaction As New TransactionScope
            Try
                If _AccounTem Is Nothing Then
                    mainAccount.IdParent = Nothing
                Else
                    'Se actualiza el padre con el AllowMovements = False
                    _AccounTem.AllowsMovement = False
                    _AccounTem.MarkAsModified()
                    Dim resultUpdate = UpdatePuc(_AccounTem, audit)
                    If resultUpdate.StateResult = False Then
                        UnitOfWork.RollbackChanges()
                        Transaction.Dispose()
                        Return New ActionResult(Of MainAccounts) With {.StateResult = False, .MessageResult = {"a-0002"}.ToList()}
                    End If

                    mainAccount.IdParent = _AccounTem.Id
                End If

                mainAccount.CreationUser = audit.CodeUser
                mainAccount.CreationDate = DateTime.Now

                'Se guarda el PUC
                _IPUCRepository.SaveEntity(mainAccount)
                UnitOfWork.Commit()

                'Guardar referencia para trigger de evento fuera del TransactionScope
                If _StateEntity Then
                    mainAccountToTrigger = mainAccount
                End If

                Transaction.Complete()
                mainAccount.MarkAsUnchanged()
            Catch ex As Exception
                UnitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of MainAccounts) With {.StateResult = False, .MessageResult = {"a-0002"}.ToList()}
            End Try
        End Using

        'Ejecutar el evento FUERA del TransactionScope para evitar conflictos de transacciones distribuidas
        'En este punto la cuenta ya está guardada y el trigger SQL ya se ejecutó
        If mainAccountToTrigger IsNot Nothing Then
            Try
                TriggerEvent(mainAccountToTrigger, audit)
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            End Try
        End If

        Return New ActionResult(Of MainAccounts) With {.StateResult = True, .ObjectEmbbeded = mainAccount}
    End Function

    ''' <summary>
    ''' Funcion para obtener todas las cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAcounts() As List(Of MainAccounts) Implements IPUCAdminService.GetAllAcounts
        Try
            Return _IPUCRepository.GetAllAcounts()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Validates the account parent.
    ''' </summary>
    ''' <param name="codeAccount">The code account.</param>
    ''' <returns></returns>
    Public Function ValidateAccountParent(codeAccount As String, legalBookId As Integer) As MainAccounts Implements IPUCAdminService.ValidateAccountParent
        Try
            Return _IPUCRepository.ValidateAccountParent(codeAccount, legalBookId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Updates the puc.
    ''' </summary>
    ''' <param name="puc">The puc.</param>
    ''' <returns></returns>
    Public Function UpdatePuc(puc As MainAccounts, audit As AuditMessage) As ActionResult(Of MainAccounts) Implements IPUCAdminService.UpdatePuc
        Dim UnitOfWork As IUnitWork = _IPUCRepository.UnitWork
        Try
            'Si la naturaleza se cambia se tiene que validar que la cuenta no tenga movimientos
            'sino no la deja cambiar
            Dim Validate As Boolean = False
            Dim mainAccountValidate = _IPUCRepository.GetAccountById(puc.Id, False)
            If mainAccountValidate IsNot Nothing Then 'Si existe la cuenta contable
                If mainAccountValidate.Nature IsNot Nothing Then 'Si la naturaleza que esta en la BD es dfte a nulo
                    If mainAccountValidate.Nature <> puc.Nature Then 'Si la naturaleza es diferente de la BD al del registro que viene desde presentation se valida que no tenga movimientos la cuenta contable
                        Validate = True
                    End If
                Else 'Si es nula la naturaleza se pregunta sobre la clase contable
                    If mainAccountValidate.MainAccountClasses.Nature <> puc.Nature Then
                        Validate = True
                    End If
                End If
            End If

            'Se valida si la cuenta tiene moviemientos
            If Validate Then
                Dim validateMovements = _IPUCRepository.ValidateMovementsOfMainAccount(puc.Id)
                If validateMovements Then
                    Return New ActionResult(Of MainAccounts) With {.StateResult = False, .MessageResult = {"-0003"}.ToList()}
                End If
            End If


            Dim auditProcess As IndigoAuditSimpleEntity(Of MainAccounts)
            Dim status As Integer
            Dim AuxPuc As MainAccounts = Nothing
            If puc.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                puc.CreationDate = DateTime.Now
                puc.CreationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                puc.ModificationDate = DateTime.Now
                puc.ModificationUser = audit.CodeUser
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxPuc = puc.OriginalValue
            End If
            auditProcess = New IndigoAuditSimpleEntity(Of MainAccounts)(puc, audit, status, AuxPuc)
            Me._IPUCRepository.SaveEntity(puc)

            If Not _StateEntity Then
                TriggerEvent(puc, audit)
            End If

            UnitOfWork.Commit()
            auditProcess.Execute()
            puc.MarkAsUnchanged()
            Return New ActionResult(Of MainAccounts) With {.StateResult = True, .ObjectEmbbeded = puc}
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of MainAccounts) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MainAccounts) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' </exception>
    Public Function UpdateStatePUC(code As String, LegalBookId As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of MainAccounts) Implements IPUCAdminService.UpdateStatePUC
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim account As MainAccounts = Me._IPUCRepository.GetAccountByCodeAndLegalBookId(code.Trim(), LegalBookId)
            If account IsNot Nothing AndAlso account.Id > 0 Then
                account.Status = state
                account.MarkAsModified()
            End If
            Return Me.UpdatePuc(account, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MainAccounts) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Valida si la cuenta contable tiene movimientos
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateMovementsOfMainAccount(MainAccountId As Integer) As Boolean Implements IPUCAdminService.ValidateMovementsOfMainAccount
        Try
            Return _IPUCRepository.ValidateMovementsOfMainAccount(MainAccountId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de disponibilidades para cargar el datasource del reporte Boletín de deudores morosos del estado
    ''' </summary>
    ''' <param name="DateCourt"></param>
    ''' <param name="ReportValue"></param>
    ''' <param name="value"></param>
    ''' <param name="PeriodType"></param>
    ''' <param name="PeriodValue"></param>
    ''' <param name="ThirdPartyStart"></param>
    ''' <param name="ThirdPartyEnd"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportBulletinDefaultersState(DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportBulletinDefaultersState
        If DateCourt = String.Empty Then
            Throw New ArgumentNullException("DateCourt")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            If value = Nothing And ThirdPartyStart Is Nothing And ThirdPartyEnd Is Nothing OrElse value = 0 And ThirdPartyStart Is Nothing And ThirdPartyEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportBulletinDefaultersState] '" & DateCourt & "'," & ReportValue & ", NULL ," & PeriodType & "," & PeriodValue & ",NULL , NULL"
            ElseIf value = Nothing OrElse value = 0 Then
                query1 = "exec [GeneralLedger].[SP_ReportBulletinDefaultersState] '" & DateCourt & "'," & ReportValue & ", NULL ," & PeriodType & "," & PeriodValue & ",'" & ThirdPartyStart & "','" & ThirdPartyEnd & "'"
            ElseIf ThirdPartyStart Is Nothing And ThirdPartyEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportBulletinDefaultersState] '" & DateCourt & "'," & ReportValue & "," & value & "," & PeriodType & "," & PeriodValue & ",NULL , NULL"
            End If

            Dim dt1 = Me.GetDatatable(query1, session, "ReportBulletinDefaultersState")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable

        Dim connectionString = String.Empty
        If ConfigurationManager.ConnectionStrings("CONX_GENESIS_REPORTS") IsNot Nothing Then
            connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS_REPORTS, String.Empty, session.TransactionalContainer, False)
        Else
            connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        End If
        Using conexion As New SqlConnection(connectionString)
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

    ''' <summary>
    ''' Genera el archivo plano de boletin deudores morosos
    ''' </summary>
    ''' <param name="TypeReport"></param>
    ''' <param name="DateCourt"></param>
    ''' <param name="ReportValue"></param>
    ''' <param name="value"></param>
    ''' <param name="PeriodType"></param>
    ''' <param name="PeriodValue"></param>
    ''' <param name="ThirdPartyStart"></param>
    ''' <param name="ThirdPartyEnd"></param>
    ''' <param name="EntityCode"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveBulletinDefaultersState(TypeReport As Integer, DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, EntityCode As String, session As SessionValues) As Text.StringBuilder Implements IPUCAdminService.GenerateArchiveBulletinDefaultersState
        Dim result As New StringBuilder()
        Dim data As DataSet = GetListReportBulletinDefaultersState(DateCourt, ReportValue, value, PeriodType, PeriodValue, ThirdPartyStart, ThirdPartyEnd, session)

        DateCourt = CDate(DateCourt)
        Dim monthPrint As String = Month(DateCourt)
        If monthPrint.Length = 1 Then
            monthPrint = "0" & monthPrint
        End If
        Dim datePrint As String = "1" & monthPrint & monthPrint

        Dim dtBulletinDefaulters As New DataTable
        If data IsNot Nothing Then
            dtBulletinDefaulters = data.Tables("ReportBulletinDefaultersState")
        End If

        If dtBulletinDefaulters.Rows.Count > 0 Then
            Dim lineHead As String = Utils.StringPad("S", 5, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(EntityCode, 15, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(datePrint, 8, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(Year(DateCourt), 8, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("CGN2009_BDME_REPORTE_SEMESTRAL", 35, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(CDate(DateCourt), 10, " ", Utils.PadType.STR_PAD_RIGHT)
            result.Append(lineHead)
            If TypeReport <> 1 Then 'tipo de detallado

                For Each item As DataRow In dtBulletinDefaulters.Rows
                    Dim ThirPartyTypeValue As Integer = item("ThirPartyTypeValue")
                    Dim DocumentCode As String = item("DocumentCode")
                    Dim ThirdPartyNit As String = item("ThirdPartyNit")
                    Dim ThirdPartyTypeIdentificationValue As Integer = item("ThirdPartyTypeIdentificationValue")
                    Dim ThirdPartyName As String = item("ThirdPartyName")
                    Dim Balance As Decimal = item("Balance")

                    Dim lineDet As String = vbCrLf

                    lineDet &= Utils.StringPad("D", 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(1, 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(ThirPartyTypeValue, 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(DocumentCode, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(ThirdPartyNit, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(ThirdPartyTypeIdentificationValue, 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(ThirdPartyName, 100, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(Balance, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(1, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    result.Append(lineDet)
                Next
            Else
                Dim query = From rows In dtBulletinDefaulters Group rows By Nit = rows.Field(Of String)("ThirdPartyNit") Into BulletinDefaulters = Group
                            Select New With {
                                Key Nit,
                                .ThirPartyTypeValue = BulletinDefaulters(0).Item("ThirPartyTypeValue"),
                                .ThirdPartyNit = Nit,
                                .ThirdPartyTypeIdentificationValue = BulletinDefaulters(0).Item("ThirdPartyTypeIdentificationValue"),
                                .ThirdPartyName = BulletinDefaulters(0).Item("ThirdPartyName"),
                                .Balance = BulletinDefaulters.Sum(Function(r) Convert.ToDecimal(r.Item("Balance")))
                           }

                For Each item In query
                    Dim lineDet As String = vbCrLf
                    lineDet &= Utils.StringPad("D", 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(1, 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(item.ThirPartyTypeValue, 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad("1", 15, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(item.ThirdPartyNit, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(item.ThirdPartyTypeIdentificationValue, 5, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(item.ThirdPartyName, 100, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(item.Balance, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineDet &= Utils.StringPad(1, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    result.Append(lineDet)
                Next

            End If
        End If

        Return result

    End Function

    ''' <summary>
    ''' Genera el archivo plano de CGN002
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="LevelSubAccount"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateFileCGN002(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, LevelSubAccount As Boolean, BookId As Integer, Session As SessionValues) As StringBuilder Implements IPUCAdminService.GenerateFileCGN002
        'Variable que se devuelve para generar el arvhivo plano
        Dim result As New StringBuilder()
        'Se obtiene el listado por medio de un dataSet
        Dim data As DataSet = GetListReportCGN002(DateStart, DateEnd, AccountStart, AccountEnd, LevelSubAccount, BookId, Session)

        Dim dtCGN002 As New DataTable
        If data IsNot Nothing Then
            dtCGN002 = data.Tables("ReportCGN002")
        End If

        Dim spaces = "    "
        Dim monthPrintInitial As String = DateStart.ToString("MM") 'Se captura el mes de la fecha inicial
        Dim monthPrintEnd As String = DateEnd.ToString("MM") 'Se captura el mes de la fecha final
        Dim datePrint As String = "1" & monthPrintInitial & monthPrintEnd 'Se arma la fecha que se imprime en el archivo
        Dim thirdParty = _IThirdPartyRepository.GetThirdPartyByNit(Session.IndigoCompanyNit, False) 'Codigo de la entidad

        If dtCGN002.Rows.Count > 0 Then
            'Se arma la cabecera del archivo plano
            Dim lineHead As String = String.Empty
            lineHead &= String.Format("{0}{1}", "S", spaces)
            lineHead &= String.Format("{0}{1}", thirdParty.EntityCode, spaces)
            lineHead &= String.Format("{0}{1}", datePrint, spaces)
            lineHead &= String.Format("{0}{1}", Year(DateStart), spaces)
            lineHead &= String.Format("{0}{1}", "CGN2015_002_OPERACIONES_RECIPROCAS_CONVERGENCIAS", spaces)
            result.Append(lineHead)

            'Se reccorre los rows del dataRow para armar el archivo plano
            For Each item As DataRow In dtCGN002.Rows
                Dim AccountCode As String = item("mainAccountCode") 'Codigo cuenta contable
                Dim NitThirdParty As String = IIf(IsDBNull(item("thirdPartyNit")) = True, Nothing, item("thirdPartyNit")) 'Nit Tercero
                Dim BalanceCurrent As Decimal = item("balanceCurrent") 'Saldo Credito
                Dim BalanceNoCurrent As Decimal = item("balanceNotCurrent") 'Saldo No Credito
                Dim LevelAccount As Integer = item("LevelAccount") 'Nivel de la cuenta

                'Cuenta que se imprime en el archivo plano
                Dim AccountPrint As String = GenerateAccountCodePrint(AccountCode, LevelAccount)

                Dim lineDet As String = vbCrLf

                lineDet &= Utils.StringPad("D", 5, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(AccountPrint, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(NitThirdParty, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(BalanceCurrent, 23, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(BalanceNoCurrent, 23, " ", Utils.PadType.STR_PAD_RIGHT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Metodo que genera la cuenta que se genera en el archivo plano
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateAccountCodePrint(AccountCode As String, LevelAccount As Integer) As String
        'Cuenta que se imprime en el archivo plano
        Dim AccountPrint As String = String.Empty
        'Se recorre la cantidad de niveles para asi mismo crear el AccountPrint
        For i = 1 To LevelAccount Step 1
            Select Case i
                Case 1 'Nivel 1
                    AccountPrint = AccountCode.Chars(0)
                Case 2 'Nivel 2
                    AccountPrint = AccountPrint & AccountCode.Chars(1)
                Case 3 'Nivel 3
                    AccountPrint = AccountPrint & AccountCode.Chars(2) & AccountCode.Chars(3)
                Case 4 'Nivel 4
                    AccountPrint = AccountPrint & AccountCode.Chars(4) & AccountCode.Chars(5)
                Case 5 'Nivel 5
                    AccountPrint = AccountPrint & AccountCode.Chars(6) & AccountCode.Chars(7)
                Case 6 'Nivel 6
                    AccountPrint = AccountPrint & AccountCode.Chars(8) & AccountCode.Chars(9)
            End Select
            If i <> LevelAccount Then 'Cuando la cantidad que se esta recorriendo es diferente al nivel se agrega un punto
                AccountPrint = AccountPrint & "."
            End If
        Next
        Return AccountPrint
    End Function

    ''' <summary>
    ''' Obtiene los datos del StoreProcedure SP_ReportCGN002
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="LevelSubAccount"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCGN002(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, LevelSubAccount As Boolean, BookId As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportCGN002
        If DateStart = Nothing Then
            Throw New ArgumentNullException("DateStart")
        End If
        If DateEnd = Nothing Then
            Throw New ArgumentNullException("DateEnd")
        End If
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            If AccountStart Is Nothing And AccountEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCGN002] '" & DateStart & "','" & DateEnd & "',NULL,NULL," & LevelSubAccount & "," & BookId
            Else
                query1 = "exec [GeneralLedger].[SP_ReportCGN002] '" & DateStart & "','" & DateEnd & "','" & AccountStart & "','" & AccountEnd & "'," & LevelSubAccount & "," & BookId
            End If


            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCGN002")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Genera el archivo plano de CGN001
    ''' </summary>
    ''' <param name="DateStart">Fecha inicial</param>
    ''' <param name="DateEnd">Fecha final</param>
    ''' <param name="AccountStart">Cuenta contable inicial</param>
    ''' <param name="AccountEnd">Cuenta contable final</param>
    ''' <param name="AccountingZero">Cuenta cero</param>
    ''' <param name="BookId">Libro oficial</param>
    ''' <param name="Session">Variable de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateFileCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, InThousands As Boolean, Session As SessionValues) As StringBuilder Implements IPUCAdminService.GenerateFileCGN001
        'Variable que se devuelve para generar el arvhivo plano
        Dim result As New StringBuilder()
        'Se obtiene el listado por medio de un dataSet
        Dim data As DataSet = GetListReportCGN001(DateStart, DateEnd, AccountStart, AccountEnd, AccountingZero, BookId, Session)

        Dim dtCGN001 As New DataTable

        If data IsNot Nothing Then
            dtCGN001 = data.Tables("ReportCGN001")
        End If
        Dim culture As New Globalization.CultureInfo("en-US")
        Dim spaces = "    "
        Dim monthPrintInitial As String = DateStart.ToString("MM") 'Se captura el mes de la fecha inicial
        Dim monthPrintEnd As String = DateEnd.ToString("MM") 'Se captura el mes de la fecha final
        Dim datePrint As String = "1" & monthPrintInitial & monthPrintEnd 'Se arma la fecha que se imprime en el archivo
        Dim thirdParty = _IThirdPartyRepository.GetThirdPartyByNit(Session.IndigoCompanyNit, False) 'Codigo de la entidad

        If dtCGN001.Rows.Count > 0 Then
            'Se arma la cabecera del archivo plano
            Dim lineHead As String = String.Empty
            lineHead &= String.Format("{0}{1}", "S", spaces)
            lineHead &= String.Format("{0}{1}", thirdParty.EntityCode, spaces)
            lineHead &= String.Format("{0}{1}", datePrint, spaces)
            lineHead &= String.Format("{0}{1}", Year(DateStart), spaces)
            lineHead &= String.Format("{0}{1}", "CGN2015_001_SALDOS_Y_MOVIMIENTOS_CONVERGENCIAS", spaces)
            lineHead &= String.Format("{0}", CDate(DateEnd).ToString("dd-MM-yyyy"))
            result.Append(lineHead)

            'Se reccorre los rows del dataRow para armar el archivo plano
            For Each item As DataRow In dtCGN001.Rows
                Dim AccountCode As String = item("mainAccountCode") 'Codigo cuenta contable
                Dim AccountPreviousBalance As Decimal = item("mainAccountPreviousBalance") 'Saldo previo de la cuenta
                Dim DebitValue As Decimal = item("debitValue") 'Valor debito
                Dim CreditValue As Decimal = item("creditValue") 'Valor credito
                Dim AccountNewBalance As Decimal = item("mainAccountNewBalance") 'Nuevo saldo de la cuenta
                Dim BalanceCurrent As Decimal = item("balanceCurrent") 'Saldo credito
                Dim BalanceNoCurrent As Decimal = item("balanceNotCurrent") 'Saldo no credito
                Dim LevelAccount As Integer = item("LevelAccount") 'Nivel de la cuenta

                If InThousands = True Then
                    AccountPreviousBalance = Math.Round((AccountPreviousBalance / 1000), 0)
                    DebitValue = Math.Round((DebitValue / 1000), 0)
                    CreditValue = Math.Round((CreditValue / 1000), 0)
                    AccountNewBalance = Math.Round((AccountNewBalance / 1000), 0)
                    BalanceCurrent = Math.Round((BalanceCurrent / 1000), 0)
                    BalanceNoCurrent = Math.Round((BalanceNoCurrent / 1000), 0)
                End If

                'Cuenta que se imprime en el archivo plano
                Dim AccountPrint As String = GenerateAccountCodePrint(AccountCode, LevelAccount)

                Dim lineDet As String = vbCrLf

                lineDet &= "D" & vbTab
                lineDet &= AccountPrint & vbTab
                lineDet &= AccountPreviousBalance.ToString("F2", culture) & vbTab
                lineDet &= DebitValue.ToString("F2", culture) & vbTab
                lineDet &= CreditValue.ToString("F2", culture) & vbTab
                lineDet &= AccountNewBalance.ToString("F2", culture) & vbTab
                lineDet &= BalanceCurrent.ToString("F2", culture) & vbTab
                lineDet &= BalanceNoCurrent.ToString("F2", culture) & vbTab
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al StoreProcedure de SP_ReportCGN001
    ''' </summary>
    ''' <param name="DateStart">Fecha inicial</param>
    ''' <param name="DateEnd">Fecha final</param>
    ''' <param name="AccountStart">Cuenta contable inicial</param>
    ''' <param name="AccountEnd">Cuenta contable final</param>
    ''' <param name="AccountingZero">Cuenta cero</param>
    ''' <param name="BookId">Libro oficial</param>
    ''' <param name="Session">Variable de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportCGN001
        If DateStart = Nothing Then
            Throw New ArgumentNullException("DateStart")
        End If
        If DateEnd = Nothing Then
            Throw New ArgumentNullException("DateEnd")
        End If
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            If AccountStart Is Nothing And AccountEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCGN001] '" & DateStart & "','" & DateEnd & "',NULL, NULL," & AccountingZero & "," & BookId
            Else
                query1 = "exec [GeneralLedger].[SP_ReportCGN001] '" & DateStart & "','" & DateEnd & "','" & AccountStart & "','" & AccountEnd & "'," & AccountingZero & "," & BookId
            End If
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCGN001")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportGeneralBalance
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportGeneralBalance(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportGeneralBalance
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportGeneralBalance] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportGeneralBalance")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportLedgerAndBalance
    ''' </summary>
    ''' <param name="month"></param>
    ''' <param name="ano"></param>
    ''' <param name="AccountingZero"></param>
    ''' <param name="BookId"></param>
    ''' <param name="accountLevel"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportLedgerAndBalance(month As Integer, ano As Integer, AccountingZero As Boolean, BookId As Integer, accountLevel As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportLedgerAndBalance
        If month = Nothing OrElse month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        If ano = Nothing OrElse ano = 0 Then
            Throw New ArgumentNullException("ano")
        End If
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        If accountLevel = Nothing OrElse accountLevel = 0 Then
            Throw New ArgumentNullException("accountLevel")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            query1 = "exec [GeneralLedger].[SP_ReportLedgerAndBalance] " & month & "," & ano & "," & AccountingZero & "," & BookId & "," & accountLevel & ",0"
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportLedgerAndBalance")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportRetentions
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportRetentions(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportRetentions
        If criterias Is Nothing Then
            Throw New ArgumentNullException("criterias")
        End If

        ' se convienten a xml los criterios yfiltros
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim xmlFilters = Utils.DictionaryToXML(filters)

        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty

            ' se ejecuta el sp indicado dependiendo del tipo de reporte
            Select Case criterias("ReportType")
                Case 1
                    query1 = "exec [GeneralLedger].[SP_ReportRetentions]  '" & xmlCriterias & "', '" & xmlFilters & "'"
                    dt1 = Me.GetDatatable(query1, Session, "ReportRetentions")
                Case 2
                    query1 = "exec [GeneralLedger].[SP_ReportStatementSaleIVA]  '" & xmlCriterias & "', '" & xmlFilters & "'"
                    dt1 = Me.GetDatatable(query1, Session, "ReportStatementSaleIVA")
                Case 3
                    query1 = "exec [GeneralLedger].[SP_ReportStatementPurchaseIVA]  '" & xmlCriterias & "', '" & xmlFilters & "'"
                    dt1 = Me.GetDatatable(query1, Session, "ReportStatementPurchaseIVA")
            End Select

            If dt1 IsNot Nothing Then
                ds.Tables.Add(dt1.Copy())
            End If
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetSource
    ''' </summary>
    ''' <param name="LegalBookId"></param>
    ''' <param name="Year"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="NitStart"></param>
    ''' <param name="NitEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCertificateRetSource(LegalBookId As Integer, Year As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, InitialDate As DateTime, FinalDate As DateTime, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportCertificateRetSource
        Try
            Dim ds As New DataSet
            Dim query1 As String = "EXEC [GeneralLedger].[SP_ReportCertificateRetSource] " & LegalBookId & "," & Year & ","
            If AccountStart IsNot Nothing AndAlso AccountEnd IsNot Nothing Then
                query1 &= "'" & AccountStart & "','" & AccountEnd & "',"
            Else
                query1 &= "NULL,NULL,"
            End If
            If NitStart IsNot Nothing AndAlso NitEnd IsNot Nothing Then
                query1 &= "'" & NitStart & "','" & NitEnd & "'"
            Else
                query1 &= "NULL,NULL"
            End If
            query1 &= ",'" & InitialDate.ToString("yyyyMMdd") & "','" & FinalDate.ToString("yyyyMMdd") & " 23:59:59'"
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCertificateRetSource")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetIVA
    ''' </summary>
    ''' <param name="DateInitial"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCertificateRetIVA(DateInitial As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, RetencionType As Integer, LegalBookId As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportCertificateRetIVA
        If DateInitial = Nothing Then
            Throw New ArgumentNullException("DateInitial")
        End If
        If DateEnd = Nothing Then
            Throw New ArgumentNullException("DateEnd")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            If AccountStart Is Nothing AndAlso AccountEnd Is Nothing AndAlso NitStart Is Nothing AndAlso NitEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetIVA] '" & DateInitial & "','" & DateEnd & "',NULL,NULL,NULL,NULL," & RetencionType & "," & LegalBookId & ""
            ElseIf AccountStart Is Nothing AndAlso AccountEnd Is Nothing AndAlso NitStart IsNot Nothing AndAlso NitEnd IsNot Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetIVA] '" & DateInitial & "','" & DateEnd & "',NULL,NULL,'" & NitStart & "','" & NitEnd & "'," & RetencionType & "," & LegalBookId & ""
            ElseIf AccountStart IsNot Nothing AndAlso AccountEnd IsNot Nothing AndAlso NitStart Is Nothing AndAlso NitEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetIVA] '" & DateInitial & "','" & DateEnd & "','" & AccountStart & "','" & AccountEnd & "',NULL,NULL," & RetencionType & "," & LegalBookId & ""
            ElseIf AccountStart IsNot Nothing AndAlso AccountEnd IsNot Nothing AndAlso NitStart IsNot Nothing AndAlso NitEnd IsNot Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetIVA] '" & DateInitial & "','" & DateEnd & "','" & AccountStart & "','" & AccountEnd & "','" & NitStart & "','" & NitEnd & "'," & RetencionType & "," & LegalBookId & ""
            End If
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCertificateRetIVA")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetICA
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="NitStart"></param>
    ''' <param name="NitEnd"></param>
    ''' <param name="RetencionType"></param>
    ''' <param name="LegalBookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportCertificateRetICA(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, RetencionType As Integer, LegalBookId As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportCertificateRetICA
        If DateStart = Nothing Then
            Throw New ArgumentNullException("DateInitial")
        End If
        If DateEnd = Nothing Then
            Throw New ArgumentNullException("DateEnd")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            If AccountStart Is Nothing AndAlso AccountEnd Is Nothing AndAlso NitStart Is Nothing AndAlso NitEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetICA] '" & DateStart.ToString("yyyy-MM-dd") & "','" & DateEnd.ToString("yyyy-MM-dd") & "',NULL,NULL,NULL,NULL," & RetencionType & "," & LegalBookId & ""
            ElseIf AccountStart Is Nothing AndAlso AccountEnd Is Nothing AndAlso NitStart IsNot Nothing AndAlso NitEnd IsNot Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetICA] '" & DateStart.ToString("yyyy-MM-dd") & "','" & DateEnd.ToString("yyyy-MM-dd") & "',NULL,NULL,'" & NitStart & "','" & NitEnd & "'," & RetencionType & "," & LegalBookId & ""
            ElseIf AccountStart IsNot Nothing AndAlso AccountEnd IsNot Nothing AndAlso NitStart Is Nothing AndAlso NitEnd Is Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetICA] '" & DateStart.ToString("yyyy-MM-dd") & "','" & DateEnd.ToString("yyyy-MM-dd") & "','" & AccountStart & "','" & AccountEnd & "',NULL,NULL," & RetencionType & "," & LegalBookId & ""
            ElseIf AccountStart IsNot Nothing AndAlso AccountEnd IsNot Nothing AndAlso NitStart IsNot Nothing AndAlso NitEnd IsNot Nothing Then
                query1 = "exec [GeneralLedger].[SP_ReportCertificateRetICA] '" & DateStart.ToString("yyyy-MM-dd") & "','" & DateEnd.ToString("yyyy-MM-dd") & "','" & AccountStart & "','" & AccountEnd & "','" & NitStart & "','" & NitEnd & "'," & RetencionType & "," & LegalBookId & ""
            End If
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCertificateRetICA")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportInventoryAndBalance
    ''' </summary>
    ''' <param name="monthStart"></param>
    ''' <param name="monthEnd"></param>
    ''' <param name="ano"></param>
    ''' <param name="AccountingZero"></param>
    ''' <param name="BookId"></param>
    ''' <param name="accountLevel"></param>
    ''' <param name="allowThirdParty"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportInventoryAndBalance(monthStart As Integer, monthEnd As Integer, ano As Integer, AccountingZero As Boolean, BookId As Integer, accountLevel As Integer, AllowThirdParty As Boolean, InitialAccount As Integer, FinalAccount As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportInventoryAndBalance
        If monthStart = Nothing OrElse monthStart = 0 Then
            Throw New ArgumentNullException("month")
        End If
        If monthEnd = Nothing OrElse monthEnd = 0 Then
            Throw New ArgumentNullException("month")
        End If
        If ano = Nothing OrElse ano = 0 Then
            Throw New ArgumentNullException("ano")
        End If
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        If accountLevel = Nothing OrElse accountLevel = 0 Then
            Throw New ArgumentNullException("accountLevel")
        End If
        If FinalAccount = 0 Then
            FinalAccount = 99999999
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            query1 = "exec [GeneralLedger].[SP_ReportInventoryAndBalance] " & monthStart & "," & monthEnd & "," & ano & "," & AccountingZero & "," & BookId & "," & accountLevel & "," & AllowThirdParty & "," & InitialAccount & "," & FinalAccount
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportInventoryAndBalance")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportAuxiliar
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="Summarized"></param>
    ''' <param name="Criteria"></param>
    ''' <param name="SubCriteria"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Status"></param>
    ''' <param name="OrderBy"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="NitStart"></param>
    ''' <param name="NitEnd"></param>
    ''' <param name="CostCenterStart"></param>
    ''' <param name="CostCenterEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportAuxiliar(InitialDate As Date, EndDate As Date, Summarized As Boolean, Criteria As Integer, SubCriteria As Integer, BookId As Integer, Status As Integer,
                                          OrderBy As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, CostCenterStart As String,
                                          CostCenterEnd As String, AccumulatedBalance As Boolean, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportAuxiliar
        If InitialDate = Nothing Then
            Throw New ArgumentNullException("InitialDate")
        End If
        If EndDate = Nothing Then
            Throw New ArgumentNullException("EndDate")
        End If
        If Criteria = Nothing OrElse Criteria = 0 Then
            Throw New ArgumentNullException("Criteria")
        End If
        If SubCriteria = Nothing OrElse SubCriteria = 0 Then
            Throw New ArgumentNullException("SubCriteria")
        End If
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        If Status = Nothing OrElse Status = 0 Then
            Throw New ArgumentNullException("Status")
        End If
        If OrderBy = Nothing OrElse OrderBy = 0 Then
            Throw New ArgumentNullException("OrderBy")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},",
                                                    InitialDate.ToString("yyyy-MM-dd"),
                                                    EndDate.ToString("yyyy-MM-dd"),
                                                    Summarized,
                                                    Criteria,
                                                    SubCriteria,
                                                    BookId,
                                                    Status,
                                                    OrderBy,
                                                    AccumulatedBalance
                                                 )

            'Filtro de Cuentas contables
            If AccountStart <> String.Empty AndAlso AccountEnd <> String.Empty Then
                query1 = query1 & AccountStart & "," & AccountEnd & ","
            Else
                query1 = query1 & "0,ZZZZZZZZZZ,"
            End If

            'Filtro de Terceros
            If NitStart <> String.Empty AndAlso NitEnd <> String.Empty Then
                query1 = query1 & NitStart & "," & NitEnd & ","
            Else
                query1 = query1 & "0,ZZZZZZZZZZ,"
            End If

            'Filtro de Centros de Costos
            If CostCenterStart <> String.Empty AndAlso CostCenterEnd <> String.Empty Then
                query1 = query1 & CostCenterStart & "," & CostCenterEnd & ","
            Else
                query1 = query1 & "0,ZZZZZZZZZZ"
            End If
            Dim data = ConvertToXml(query1)
            query1 = "EXEC [GeneralLedger].[SP_ReportAuxiliar] '" & data & "'"

            Dim dt1 = Me.GetDatatable(query1, Session, "ReportAuxiliar")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function ConvertToXml(Params As String) As String
        Dim data As New StringBuilder()
        data.Append("<Data>")
        data.Append($"<InitialDate>{Params.Split(",")(0)}</InitialDate>")
        data.Append($"<EndDate>{Params.Split(",")(1)}</EndDate>")
        data.Append($"<Summarized>{Params.Split(",")(2)}</Summarized>")
        data.Append($"<Criteria>{Params.Split(",")(3)}</Criteria>")
        data.Append($"<SubCriteria>{Params.Split(",")(4)}</SubCriteria>")
        data.Append($"<BookId>{Params.Split(",")(5)}</BookId>")
        data.Append($"<Status>{Params.Split(",")(6)}</Status>")
        data.Append($"<OrderBy>{Params.Split(",")(7)}</OrderBy>")
        data.Append($"<AccumulatedBalance>{Params.Split(",")(8)}</AccumulatedBalance>")
        data.Append($"<AccountStart>{Params.Split(",")(9)}</AccountStart>")
        data.Append($"<AccountEnd>{Params.Split(",")(10)}</AccountEnd>")
        data.Append($"<ThirdPartyStart>{Params.Split(",")(11)}</ThirdPartyStart>")
        data.Append($"<ThirdPartyEnd>{Params.Split(",")(12)}</ThirdPartyEnd>")
        data.Append($"<CostCenterStart>{Params.Split(",")(13)}</CostCenterStart>")
        data.Append($"<CostCenterEnd>{Params.Split(",")(14)}</CostCenterEnd>")
        data.Append("</Data>")
        Return data.ToString()
    End Function

    ''' <summary>
    ''' Convierte los parámetros a XML incluyendo paginación
    ''' </summary>
    Public Function ConvertToXmlPaged(Params As String) As String
        Dim data As New StringBuilder()
        data.Append("<Data>")
        data.Append($"<InitialDate>{Params.Split(",")(0)}</InitialDate>")
        data.Append($"<EndDate>{Params.Split(",")(1)}</EndDate>")
        data.Append($"<Summarized>{Params.Split(",")(2)}</Summarized>")
        data.Append($"<Criteria>{Params.Split(",")(3)}</Criteria>")
        data.Append($"<SubCriteria>{Params.Split(",")(4)}</SubCriteria>")
        data.Append($"<BookId>{Params.Split(",")(5)}</BookId>")
        data.Append($"<Status>{Params.Split(",")(6)}</Status>")
        data.Append($"<OrderBy>{Params.Split(",")(7)}</OrderBy>")
        data.Append($"<AccumulatedBalance>{Params.Split(",")(8)}</AccumulatedBalance>")
        data.Append($"<AccountStart>{Params.Split(",")(9)}</AccountStart>")
        data.Append($"<AccountEnd>{Params.Split(",")(10)}</AccountEnd>")
        data.Append($"<ThirdPartyStart>{Params.Split(",")(11)}</ThirdPartyStart>")
        data.Append($"<ThirdPartyEnd>{Params.Split(",")(12)}</ThirdPartyEnd>")
        data.Append($"<CostCenterStart>{Params.Split(",")(13)}</CostCenterStart>")
        data.Append($"<CostCenterEnd>{Params.Split(",")(14)}</CostCenterEnd>")
        data.Append($"<PageNumber>{Params.Split(",")(15)}</PageNumber>")
        data.Append($"<PageSize>{Params.Split(",")(16)}</PageSize>")
        data.Append("</Data>")
        Return data.ToString()
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportAuxiliar con soporte para paginación
    ''' </summary>
    Public Function GetListReportAuxiliarPaged(InitialDate As Date, EndDate As Date, Summarized As Boolean, Criteria As Integer, SubCriteria As Integer, BookId As Integer, Status As Integer,
                                          OrderBy As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, CostCenterStart As String,
                                          CostCenterEnd As String, AccumulatedBalance As Boolean, PageNumber As Integer, PageSize As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportAuxiliarPaged
        If InitialDate = Nothing Then
            Throw New ArgumentNullException("InitialDate")
        End If
        If EndDate = Nothing Then
            Throw New ArgumentNullException("EndDate")
        End If
        If Criteria = Nothing OrElse Criteria = 0 Then
            Throw New ArgumentNullException("Criteria")
        End If
        If SubCriteria = Nothing OrElse SubCriteria = 0 Then
            Throw New ArgumentNullException("SubCriteria")
        End If
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        If Status = Nothing OrElse Status = 0 Then
            Throw New ArgumentNullException("Status")
        End If
        If OrderBy = Nothing OrElse OrderBy = 0 Then
            Throw New ArgumentNullException("OrderBy")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},",
                                                    InitialDate.ToString("yyyy-MM-dd"),
                                                    EndDate.ToString("yyyy-MM-dd"),
                                                    Summarized,
                                                    Criteria,
                                                    SubCriteria,
                                                    BookId,
                                                    Status,
                                                    OrderBy,
                                                    AccumulatedBalance
                                                 )

            'Filtro de Cuentas contables
            If AccountStart <> String.Empty AndAlso AccountEnd <> String.Empty Then
                query1 = query1 & AccountStart & "," & AccountEnd & ","
            Else
                query1 = query1 & "0,ZZZZZZZZZZ,"
            End If

            'Filtro de Terceros
            If NitStart <> String.Empty AndAlso NitEnd <> String.Empty Then
                query1 = query1 & NitStart & "," & NitEnd & ","
            Else
                query1 = query1 & "0,ZZZZZZZZZZ,"
            End If

            'Filtro de Centros de Costos
            If CostCenterStart <> String.Empty AndAlso CostCenterEnd <> String.Empty Then
                query1 = query1 & CostCenterStart & "," & CostCenterEnd & ","
            Else
                query1 = query1 & "0,ZZZZZZZZZZ,"
            End If

            ' Agregar parámetros de paginación
            query1 = query1 & PageNumber & "," & PageSize

            Dim data = ConvertToXmlPaged(query1)
            Dim comando As String = "EXEC [GeneralLedger].[SP_ReportAuxiliar] '" & data & "'"

            ' Obtener cadena de conexión
            Dim connectionString = String.Empty
            If ConfigurationManager.ConnectionStrings("CONX_GENESIS_REPORTS") IsNot Nothing Then
                connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS_REPORTS, String.Empty, Session.TransactionalContainer, False)
            Else
                connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, Session.TransactionalContainer, False)
            End If

            ' Ejecutar y llenar el DataSet con múltiples tablas
            Using conexion As New SqlConnection(connectionString)
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As New SqlDataAdapter(comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                da.Fill(ds)

                ' Nombrar las tablas para fácil acceso
                If ds.Tables.Count >= 2 Then
                    ds.Tables(0).TableName = "TotalRecords"
                    ds.Tables(1).TableName = "ReportAuxiliar"
                ElseIf ds.Tables.Count = 1 Then
                    ds.Tables(0).TableName = "ReportAuxiliar"
                End If

                conexion.Close()
            End Using

            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure [Budget].[SP_ReportExpenditureBudgetSituation]
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="CodeCategoryStart"></param>
    ''' <param name="CodeCategoryEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportExpenditureBudgetSituation(ValidityId As Integer, CodeCategoryStart As String, CodeCategoryEnd As String, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportExpenditureBudgetSituation
        If ValidityId = Nothing Then
            Throw New ArgumentNullException("ValidityId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            If CodeCategoryStart Is Nothing AndAlso CodeCategoryEnd Is Nothing Then
                query1 = "exec [Budget].[SP_ReportExpenditureBudgetSituation] '" & ValidityId & "',NULL,NULL"
            Else
                query1 = "exec [Budget].[SP_ReportExpenditureBudgetSituation] '" & ValidityId & "','" & CodeCategoryStart & "','" & CodeCategoryEnd & "'"
            End If
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportExpenditureBudgetSituation")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure spReportCostMeasurementUnit
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer, Session As SessionValues, container As String) As DataSet Implements IPUCAdminService.GetListReportCostMeasurementUnit
        If initialMonth = Nothing OrElse initialMonth = 0 Then
            Throw New ArgumentNullException("initialMonth")
        End If
        If initialYear = Nothing OrElse initialYear = 0 Then
            Throw New ArgumentNullException("initialYear")
        End If
        If endMonth = Nothing OrElse endMonth = 0 Then
            Throw New ArgumentNullException("endMonth")
        End If
        If endYear = Nothing OrElse endYear = 0 Then
            Throw New ArgumentNullException("endYear")
        End If
        If ProductionCenterId = Nothing OrElse ProductionCenterId = 0 Then
            Throw New ArgumentNullException("ProductionCenterId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty

            'si vienen los filtros de centro de costo vacios
            If initialMeasurementUnitCode = String.Empty And endMeasurementUnitCode = String.Empty Then
                query1 = "exec [InteropCost].[spReportCostMeasurementUnit] " & initialMonth & "," & initialYear & "," & endMonth & "," & endYear & ",NULL,NULL," & ProductionCenterId & ",'" & container & "'"
                'si vienen los filtros de tercero y centro de costo vacios
            Else
                query1 = "exec [InteropCost].[spReportCostMeasurementUnit] " & initialMonth & "," & initialYear & "," & endMonth & "," & endYear & ",'" & initialMeasurementUnitCode & "','" & endMeasurementUnitCode & "'," & ProductionCenterId & ",'" & container & "'"
            End If
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCostMeasurementUnit")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportListMainAccountsFixedAssetByStatusAndBookId realizado para cargar las cuentas contables de Activos fijos
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReporMainAccountsFixedAssetByStatusAndBookId(Status As Boolean, BookId As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReporMainAccountsFixedAssetByStatusAndBookId
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            query1 = "exec [FixedAsset].[SP_ReportListMainAccountsFixedAssetByStatusAndBookId] " & Status & "," & BookId

            Dim dt1 = Me.GetDatatable(query1, Session, "ReportFixedAssetByStatusAndBookId")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportListReportSubAccount realizado para cargar los datos del reporte de Subcuentas de Activos fijos
    ''' </summary>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="ItemStart"></param>
    ''' <param name="ItemEnd"></param>
    ''' <param name="EquipmentTypeStart"></param>
    ''' <param name="EquipmentTypeEnd"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportSubAccount(AccountStart As String, AccountEnd As String, ItemStart As String, ItemEnd As String, EquipmentTypeStart As String, EquipmentTypeEnd As String, BookId As Boolean, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportSubAccount
        If BookId = Nothing OrElse BookId = 0 Then
            Throw New ArgumentNullException("BookId")
        End If
        Try
            Dim ds As New DataSet
            If String.IsNullOrEmpty(AccountStart) AndAlso String.IsNullOrEmpty(AccountEnd) Then
                AccountStart = "0"
                AccountEnd = "Z"
            End If
            If String.IsNullOrEmpty(ItemStart) AndAlso String.IsNullOrEmpty(ItemEnd) Then
                ItemStart = "0"
                ItemEnd = "Z"
            End If
            If String.IsNullOrEmpty(EquipmentTypeStart) AndAlso String.IsNullOrEmpty(EquipmentTypeEnd) Then
                EquipmentTypeStart = "0"
                EquipmentTypeEnd = "Z"
            End If

            Dim query1 As String = "exec [FixedAsset].[SP_ReportListSubAccount] '" & AccountStart & "','" & AccountEnd & "','" & ItemStart & "','" & ItemEnd & "','" & EquipmentTypeStart & "','" & EquipmentTypeEnd & "'," & BookId
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportSubAccount")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportIncomeAndWithholding realizado para cargar los datos del reporte Ingresos y Retenciones de Nómina 
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="EmployeeId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportIncomeAndWithholding(Year As Integer, EmployeeId As Integer, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportIncomeAndWithholding
        If EmployeeId = Nothing OrElse EmployeeId = 0 Then
            Throw New ArgumentNullException("EmployeeId")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            query1 = "exec [Payroll].[SP_ReportIncomeAndWithholding] " & Year & "," & EmployeeId
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportIncomeAndWithholding")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamdo al storeProcedure [SP_ReportCircular015] realizado para reportar el xml de la circular 015
    ''' </summary>
    ''' <param name="InitialDate">Fecha Inicial</param>
    ''' <param name="EndDate">Fecha Final</param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportCircular015(InitialDate As Date, EndDate As Date, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportCircular015
        If InitialDate = Nothing Then
            Throw New ArgumentNullException("InitialDate")
        End If
        If EndDate = Nothing Then
            Throw New ArgumentNullException("EndDate")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            query1 = "exec [Billing].[SP_ReportCircular015] '" & InitialDate.ToString("yyyy-MM-dd") & "','" & EndDate.ToString("yyyy-MM-dd") & "','" & Session.IndigoCompanyNit & "'"
            Dim dt1 = Me.GetDatatable(query1, Session, "ReportCircular015")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportOperatingResultProductionCenter realizado para cargar los datos del reporte de Estructura Organizacional ProductionCenter
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="Container"></param>
    ''' <param name="CodePCenterIni"></param>
    ''' <param name="CodePCenterFin"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, Session As SessionValues) As DataSet Implements IPUCAdminService.GetListReportOperatingProductionCenter
        If InitialMonth = Nothing OrElse InitialMonth = 0 Then
            Throw New ArgumentNullException("InitialMonth")
        End If
        If Year = Nothing OrElse Year = 0 Then
            Throw New ArgumentNullException("Year")
        End If
        If EndMonth = Nothing OrElse EndMonth = 0 Then
            Throw New ArgumentNullException("EndMonth")
        End If
        Try
            Dim ds As New DataSet
            Dim query1 As String = String.Empty
            query1 = "exec [InteropCost].[SP_ReportOperatingResultProductionCenter] " & InitialMonth & "," & EndMonth & "," & Year & ",'" & Container & "','" & CodePCenterIni & "','" & CodePCenterFin & "'"
            Dim dt1 = Me.GetDatatable(query1, Session, "OperatingProductionCenter")
            ds.Tables.Add(dt1.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Valida si las cuentas contables manejan centro de costo
    ''' </summary>
    ''' <param name="idProfit"></param>
    ''' <param name="idLost"></param>
    ''' <returns></returns>
    Public Function ValidateHandleCostCenter(idProfit As Integer?, idLost As Integer?) As ActionResult Implements IPUCAdminService.GetHadleCostCenterByMainAccountId
        Try
            Dim request = _IPUCRepository.GetHadleCostCenterByMainAccountId(idProfit, idLost)

            If request IsNot Nothing AndAlso request.HandlesCostCenter Then
                Return New ActionResult With {.StateResult = True, .Message = ""}
            Else
                Return New ActionResult With {.StateResult = False, .Message = ""}
            End If

        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ex.Message.ToString}
        End Try
    End Function


#Region "Events"

    Public Sub TriggerEvent(MainAccount As MainAccounts, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = MainAccount.ChangeTracker.State.ToString().ToLower()

        Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(MainAccount, audit.CodeUser, ChangeTracker, DittoSourceType.mainAccounts)
        Dim Queue As IIndigoQueue = _factoryQueue.CreateQueue()
        Queue.Publish(eventData)
    End Sub

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _IPUCRepository = Nothing
            _ILevelRepository = Nothing
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
