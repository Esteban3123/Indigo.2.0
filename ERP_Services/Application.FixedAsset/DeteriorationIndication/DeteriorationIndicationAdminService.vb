#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
#End Region

Public Class DeteriorationIndicationAdminService
    Implements IDeteriorationIndicationAdminService

    'Repositorio de indicios de deterioro
    Private _FixedAssetDeteriorationIndicationRepository As IFixedAssetDeteriorationIndicationRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    'Nombre del formulario
    Private Const FORM_NAME As String = "FrmDeteriorationIndication"

    ''' <summary>
    ''' inicia los repositorios
    ''' </summary>
    ''' <param name="FixedAssetDeteriorationIndicationRepository"></param>
    ''' <param name="sequenceRepository"></param>
    Public Sub New(ByVal FixedAssetDeteriorationIndicationRepository As IFixedAssetDeteriorationIndicationRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
        If (FixedAssetDeteriorationIndicationRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de FixedAssetDeteriorationIndicationRepository vacio")
        End If
        _sequenceRepository = sequenceRepository
        _FixedAssetDeteriorationIndicationRepository = FixedAssetDeteriorationIndicationRepository
    End Sub

    Public Function GetDeteriorationIndication(Code As String) As DeteriorationIndications Implements IDeteriorationIndicationAdminService.GetDeteriorationIndication
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo del indicio vacio")
        End If
        Try
            Return _FixedAssetDeteriorationIndicationRepository.GetDeteriorationIndicationByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New DeteriorationIndications()
        End Try
    End Function

    Public Function ListAllDeteriorationIndications() As List(Of DeteriorationIndications) Implements IDeteriorationIndicationAdminService.ListAllDeteriorationIndications
        Try
            Return _FixedAssetDeteriorationIndicationRepository.ListAllDeteriorationIndications()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveDeteriorationIndication(DeteriorationIndication As DeteriorationIndications, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DeteriorationIndications) Implements IDeteriorationIndicationAdminService.SaveDeteriorationIndication
        If DeteriorationIndication Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetDeteriorationIndicationRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(DeteriorationIndication.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            DeteriorationIndication.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of DeteriorationIndications) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), DeteriorationIndication.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of DeteriorationIndications) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As DeteriorationIndications = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of DeteriorationIndications)
                Dim status As Integer

                If DeteriorationIndication.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    DeteriorationIndication.CreationUser = audit.CodeUser
                    DeteriorationIndication.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = DeteriorationIndication.OriginalValue
                    DeteriorationIndication.ModificationUser = audit.CodeUser
                    DeteriorationIndication.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._FixedAssetDeteriorationIndicationRepository.SaveEntity(DeteriorationIndication)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of DeteriorationIndications)(DeteriorationIndication, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                DeteriorationIndication.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of DeteriorationIndications) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = DeteriorationIndication, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of DeteriorationIndications) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DeteriorationIndications) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function DeteriorationIndicationChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of DeteriorationIndications) Implements IDeteriorationIndicationAdminService.DeteriorationIndicationChangeState
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
            Dim ObjDeteriorationIndication As DeteriorationIndications = Me._FixedAssetDeteriorationIndicationRepository.GetDeteriorationIndicationByCode(code.Trim())
            If ObjDeteriorationIndication IsNot Nothing AndAlso ObjDeteriorationIndication.Id > 0 Then
                ObjDeteriorationIndication.Status = state
            End If
            Dim result = Me.SaveDeteriorationIndication(ObjDeteriorationIndication, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DeteriorationIndications) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function DeleteDeteriorationIndication(DeteriorationIndication As DeteriorationIndications, audit As AuditMessage) As ActionResult Implements IDeteriorationIndicationAdminService.DeleteDeteriorationIndication
        If DeteriorationIndication Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetDeteriorationIndicationRepository.UnitWork
        Try
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
                DeteriorationIndication.ModificationUser = audit.CodeUser
                DeteriorationIndication.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DeteriorationIndications)(DeteriorationIndication, audit, status)

                DeteriorationIndication.MarkAsDeleted()
                Me._FixedAssetDeteriorationIndicationRepository.SaveEntity(DeteriorationIndication)
                unitOfWork.Commit()
                auditProcess.Execute()
                transaction.Complete()
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

    Public Function GetListReportDeteriorationIndicationsByPhysicalAsset(Year As Integer, Month As Integer, ReportType As Integer, InitialPlate As String, FinalPlate As String, LegalBookId As Integer, InitialMainAccount As String, FinalMainAccount As String, session As SessionValues) As DataSet Implements IDeteriorationIndicationAdminService.GetListReportDeteriorationIndicationsByPhysicalAsset
        Try
            Dim ds As New DataSet
            Dim query1 As String = "exec [FixedAsset].[SP_ReportDeteriorationIndicationsByPhysicalAsset] " & Year & ", " & Month & ", " & ReportType
            If InitialPlate IsNot Nothing AndAlso FinalPlate IsNot Nothing Then
                query1 &= ", '" & InitialPlate & "', '" & FinalPlate & "'"
            Else
                query1 &= ", '0', 'ZZZZZZZZZ'"
            End If

            query1 &= ", " & LegalBookId
            If InitialMainAccount IsNot Nothing AndAlso FinalMainAccount IsNot Nothing Then
                query1 &= ", '" & InitialMainAccount & "', '" & FinalMainAccount & "'"
            Else
                query1 &= ", '0', 'ZZZZZZZZZ'"
            End If

            Dim dt1 = Me.GetDatatable(query1, session, "DeteriorationIndicationsByPhysicalAsset")
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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _FixedAssetDeteriorationIndicationRepository = Nothing
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
