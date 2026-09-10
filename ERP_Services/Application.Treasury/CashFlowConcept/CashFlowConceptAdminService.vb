'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Hector Rodriguez Rubiano
' Created          : 05/11/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.SqlClient

#End Region

Public Class CashFlowConceptAdminService
    Implements ICashFlowConceptAdminService

#Region "Variables"
    Private Const FORM_NAME As String = "FrmCashFlowConcepts"

    Private _CashFlowConceptRepository As ICashFlowConceptRepository

    Private _CashReceiptConceptAdminService As ICashReceiptConceptAdminService

    Private _ExpenseConceptAdminService As IExpenseConceptAdminService

    Private _sequenceRepository As ISequenseTreasuryDRepository

#End Region

#Region "Builder"
    Public Sub New(ByVal CashFlowConceptRepository As ICashFlowConceptRepository, ByVal sequenseRepository As ISequenseTreasuryDRepository,
               ByVal cashReceiptConceptAdminService As ICashReceiptConceptAdminService,
               ByVal expenseConceptAdminService As IExpenseConceptAdminService)
        If (CashFlowConceptRepository Is Nothing) Then
            Throw New ArgumentNullException("CashFlowConceptRepository Vacio")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _CashFlowConceptRepository = CashFlowConceptRepository
        _sequenceRepository = sequenseRepository
        _CashReceiptConceptAdminService = cashReceiptConceptAdminService
        _ExpenseConceptAdminService = expenseConceptAdminService
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para eliminar un concepto de flujo de efectivo
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">TreasuryConcept Vacio</exception>
    Public Function DeleteCashFlowConcept(CashFlowConcept As CashFlowConcept, audit As AuditMessage) As ActionResult Implements ICashFlowConceptAdminService.DeleteCashFlowConcept
        If CashFlowConcept Is Nothing Then
            Throw New ArgumentNullException("CashFlowConcept")
        End If

        Dim resultado As String = String.Empty
        Dim concepts = _CashReceiptConceptAdminService.GetCashReceiptConceptByFlowConcept(CashFlowConcept.Id)
        Dim expenseConcepts = _ExpenseConceptAdminService.GetExpenseConceptByFlowConcept(CashFlowConcept.Id)

        If concepts IsNot Nothing AndAlso concepts.Count > 0 Then
            resultado = String.Format("El concepto de flujo de caja esta relacionado en los siguientes conceptos de recibo de caja: {0}{1}", vbNewLine, String.Join(", ", concepts.Select(Function(c) String.Format("{0} {1}", c.Code, c.Name)).ToList))
        End If
        If expenseConcepts IsNot Nothing AndAlso expenseConcepts.Count > 0 Then
            If String.IsNullOrEmpty(resultado) Then
                resultado = String.Format("El concepto de flujo de caja esta relacionado en los siguientes conceptos de egreso: {0}{1}", vbNewLine, String.Join(", ", expenseConcepts.Select(Function(c) String.Format("{0} {1}", c.Code, c.Description)).ToList))
            Else
                resultado = String.Format("{0}{1}En los conceptos de egreso:{1}{2}", resultado, vbNewLine, String.Join(", ", expenseConcepts.Select(Function(c) String.Format("{0} {1}", c.Code, c.Description)).ToList))
            End If

        End If

        If Not String.IsNullOrEmpty(resultado) Then
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-001"}), .Message = resultado}
        End If

        Dim unitOfWork As IUnitWork = Me._CashFlowConceptRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                CashFlowConcept.ModificationUser = audit.CodeUser
                CashFlowConcept.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CashFlowConcept)(CashFlowConcept, audit, status)
                CashFlowConcept.MarkAsDeleted()
                Me._CashFlowConceptRepository.SaveEntity(CashFlowConcept)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de flujo de efectivo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code Vacio</exception>
    Public Function GetCashFlowConceptByCode(code As String, audit As AuditMessage) As ActionResult(Of CashFlowConcept) Implements ICashFlowConceptAdminService.GetCashFlowConceptByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CashFlowConcept As CashFlowConcept = _CashFlowConceptRepository.GetCashFlowConceptByCode(code.Trim())
            If CashFlowConcept IsNot Nothing AndAlso CashFlowConcept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CashFlowConcept)(CashFlowConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CashFlowConcept) With {.StateResult = True, .ObjectEmbbeded = CashFlowConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashFlowConcept) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' metodo para guardar un concepto de flujo de efectivo
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">TreasuryConcept Vacio</exception>
    Public Function SaveCashFlowConcept(CashFlowConcept As CashFlowConcept, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CashFlowConcept) Implements ICashFlowConceptAdminService.SaveCashFlowConcept
        If CashFlowConcept Is Nothing Then
            Throw New ArgumentNullException("CashFlowConcept")
        End If
        Dim unitOfWork As IUnitWork = Me._CashFlowConceptRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(CashFlowConcept.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._sequenceRepository.GetSequenseDById(CInt(idSequense))
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            CashFlowConcept.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                            sequenseUnitOfWork.Commit()
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CashFlowConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), CashFlowConcept.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CashFlowConcept) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As CashFlowConcept = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CashFlowConcept)
                Dim status As Integer

                If CashFlowConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CashFlowConcept.CreationUser = audit.CodeUser
                    CashFlowConcept.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = CashFlowConcept.OriginalValue
                    CashFlowConcept.ModificationUser = audit.CodeUser
                    CashFlowConcept.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._CashFlowConceptRepository.SaveEntity(CashFlowConcept)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CashFlowConcept)(CashFlowConcept, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                CashFlowConcept.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CashFlowConcept) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = CashFlowConcept, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CashFlowConcept) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashFlowConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>    
    ''' <returns></returns>
    Public Function ChangeStateCashFlowConcept(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CashFlowConcept) Implements ICashFlowConceptAdminService.ChangeStateCashFlowConcept
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
            Dim CashFlowConcept As CashFlowConcept = Me._CashFlowConceptRepository.GetCashFlowConceptByCode(code.Trim())
            If CashFlowConcept IsNot Nothing AndAlso CashFlowConcept.Id > 0 Then
                CashFlowConcept.StatusConcept = state
            End If
            Dim result = Me.SaveCashFlowConcept(CashFlowConcept, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashFlowConcept) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un concepto de flujo de efectivo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCashFlowConceptById(id As Integer) As ActionResult(Of CashFlowConcept) Implements ICashFlowConceptAdminService.GetCashFlowConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim CashFlowConcept As CashFlowConcept = Me._CashFlowConceptRepository.GetCashFlowConceptById(id)
            If CashFlowConcept IsNot Nothing AndAlso CashFlowConcept.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CashFlowConcept)(CashFlowConcept, Nothing, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CashFlowConcept) With {.StateResult = True, .ObjectEmbbeded = CashFlowConcept}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CashFlowConcept) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ListCashFlowStatus(ByVal parameters As String, ByVal session As SessionValues) As DataSet Implements ICashFlowConceptAdminService.ListCashFlowStatus
        If String.IsNullOrEmpty(parameters) Then Throw New ArgumentNullException("parameters")

        Try
            Dim ds As DataSet
            Dim query1 As String
            query1 = String.Format("exec Treasury.SP_CashFlowStatus '{0}'", parameters)
            ds = Me.GetDatatable(query1, session, "Treasury_SP_CashFlowStatus")
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Comando"></param>
    ''' <param name="session"></param>
    ''' <param name="nameDt"></param>
    ''' <returns></returns>
    Private Function GetDatatable(ByVal Comando As String, ByVal session As SessionValues, ByVal nameDt As String) As DataSet
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As SqlConnection = New SqlConnection(connectionString)
            Dim _GetDatatable As DataSet
            Try

                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If

                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As DataSet = New DataSet()
                da.Fill(ds, nameDt)
                _GetDatatable = ds
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return _GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CashFlowConceptRepository = Nothing
            _sequenceRepository = Nothing
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
