'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions

Public Class DefinitionRateAdminService
    Implements IDefinitionRateAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _definitionRateRepository As IDefinitionRateRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

    Private _serviceOrderDetailRepository As IServiceOrderDetailRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal definitionRateRepository As IDefinitionRateRepository, ByVal secuenseDRepository As ISequenseContractDRepository, ByVal serviceOrderDetailRepository As IServiceOrderDetailRepository)
        If definitionRateRepository Is Nothing Then
            Throw New ArgumentNullException("definitionRateRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _definitionRateRepository = definitionRateRepository
        _secuenseDRepository = secuenseDRepository
        _serviceOrderDetailRepository = serviceOrderDetailRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDefinitionRate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DefinitionRate) Implements IDefinitionRateAdminService.ChangeStateDefinitionRate
        Dim DefinitionRate As DefinitionRate = _definitionRateRepository.GetDefinitionRate(code)
        DefinitionRate.Status = state
        Return SaveDefinitionRate(DefinitionRate, Nothing, Nothing, String.Empty, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="SurgicalGroup"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDefinitionRate(DefinitionRate As DefinitionRate, audit As AuditMessage) As ActionResult Implements IDefinitionRateAdminService.DeleteDefinitionRate
        If DefinitionRate Is Nothing Then
            Throw New ArgumentNullException("DefinitionRate")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, DefinitionRate.Company, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                command.CommandText = "DELETE  FROM [Contract].[DefinitionRateDetailSurgicalProcedures] WHERE [DefinitionRateDetailId] in (select id from [Contract].[DefinitionRateDetail] where [DefinitionRateId] = " + DefinitionRate.Id.ToString + ")"
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[DefinitionRateDetailCondition] WHERE [DefinitionRateDetailId] in (select id from [Contract].[DefinitionRateDetail] where [DefinitionRateId] = " + DefinitionRate.Id.ToString + ")"
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[DefinitionRateDetail] WHERE [DefinitionRateId]=" & DefinitionRate.Id
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[DefinitionRate] WHERE [Id]=" & DefinitionRate.Id
                command.ExecuteNonQuery()

                tx.Commit()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As UpdateException
                tx.Rollback()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                tx.Rollback()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            Finally
                cnx.Close()
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Elimina el listado que en el form se le dio click derecho eliminar
    ''' </summary>
    ''' <param name="ListDeleteDefinitionRateDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteList(ListDeleteDefinitionRateDetail As List(Of DefinitionRateDetail), Company As String) As ActionResult
        If ListDeleteDefinitionRateDetail Is Nothing OrElse ListDeleteDefinitionRateDetail.Count = 0 Then
            Throw New ArgumentNullException("ListDeleteDefinitionRateDetail")
        End If
        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, Company, False))
        cnx.Open()
        Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
        Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
        command.CommandTimeout = 30000
        command.CommandType = CommandType.Text
        Try

            Dim errors As New System.Text.StringBuilder()

            For Each itemDelete As DefinitionRateDetail In ListDeleteDefinitionRateDetail
                Dim currentError As New System.Text.StringBuilder()

                command.CommandText = $"SELECT COUNT(*) FROM [Billing].[ServiceOrderDetail] WHERE [DefinitionRateDetailId] = {itemDelete.Id}"
                Dim count As Integer = command.ExecuteScalar()
                If count > 0 Then
                    currentError.AppendLine($"El item ({itemDelete.RuleDescription}) no se pudo eliminar debido a que se relaciona con uno o más detalles de (Órdenes de Servicio).")
                End If

                command.CommandText = $"SELECT COUNT(*) FROM [Billing].[ServiceOrderDetail] WHERE DefinitionRateDetailConditionId IN (select id from [Contract].[DefinitionRateDetailCondition] where [DefinitionRateDetailId] = {itemDelete.Id})"
                count = command.ExecuteScalar()
                If count > 0 Then
                    currentError.AppendLine($"El item ({itemDelete.RuleDescription}) no se pudo eliminar debido a que las condiciones se relacionan con uno o más detalles de (Órdenes de Servicio).")
                End If


                'command.CommandText = $"SELECT COUNT(*) FROM [Contract].[DefinitionRateDetailCondition] WHERE [DefinitionRateDetailId] = {itemDelete.Id}"
                'count = command.ExecuteScalar()
                'If count > 0 Then
                '    currentError.AppendLine($"El item ({itemDelete.RuleDescription}) no se pudo eliminar debido a que se relaciona con uno o más detalles de (Definicion de tarifas para la liquidacion por Horarios).")
                'End If

                If currentError.Length > 0 Then
                    errors.AppendLine(currentError.ToString())
                    Continue For
                End If

                command.CommandText = "DELETE  FROM [Contract].[DefinitionRateDetailSurgicalProcedures] WHERE [DefinitionRateDetailId] in (select id from [Contract].[DefinitionRateDetail] where [Id] = " + itemDelete.Id.ToString + ")"
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[DefinitionRateDetailCondition] WHERE [DefinitionRateDetailId] in (select id from [Contract].[DefinitionRateDetail] where [Id] = " + itemDelete.Id.ToString + ")"
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[DefinitionRateDetail] WHERE [Id]=" & itemDelete.Id
                command.ExecuteNonQuery()
            Next
            tx.Commit()
            Return New ActionResult With {.StateResult = True, .Message = errors.ToString()}
        Catch ex As System.Data.SqlClient.SqlException
            tx.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As OptimisticConcurrencyException
            tx.Rollback()
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As UpdateException
            tx.Rollback()
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            tx.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Finally
            cnx.Close()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefinitionRate(code As String, audit As AuditMessage) As ActionResult(Of DefinitionRate) Implements IDefinitionRateAdminService.GetDefinitionRate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DefinitionRate As DefinitionRate = Me._definitionRateRepository.GetDefinitionRate(code.Trim())
            If DefinitionRate IsNot Nothing AndAlso DefinitionRate.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DefinitionRate)(DefinitionRate, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DefinitionRate) With {.StateResult = True, .ObjectEmbbeded = DefinitionRate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefinitionRateById(id As Integer, audit As AuditMessage) As ActionResult(Of DefinitionRate) Implements IDefinitionRateAdminService.GetDefinitionRateById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim DefinitionRate As DefinitionRate = Me._definitionRateRepository.GetDefinitionRateById(id)
            If DefinitionRate IsNot Nothing AndAlso DefinitionRate.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of DefinitionRate)(DefinitionRate, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of DefinitionRate) With {.StateResult = True, .ObjectEmbbeded = DefinitionRate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="DefinitionRate"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveDefinitionRate(DefinitionRate As DefinitionRate, ListDeleteDefinitionRateDetail As List(Of DefinitionRateDetail), listDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition), Company As String, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of DefinitionRate) Implements IDefinitionRateAdminService.SaveDefinitionRate
        If DefinitionRate Is Nothing Then
            Throw New ArgumentNullException("DefinitionRate")
        End If
        Dim unitOfWork As IUnitWork = Me._definitionRateRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim seq As ContractSequenceDetail = Nothing
                If DefinitionRate.Code Is Nothing OrElse DefinitionRate.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            DefinitionRate.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If
                Dim messageDelete As String = String.Empty
                'Elimino los detalles que se marcaron en el formulario para eliminar
                Dim listIds = listDeleteDefinitionRateDetailCondition?.FindAll(Function(f) f.Id > 0)?.Select(Function(s) s.Id)?.ToList()

                If ListDeleteDefinitionRateDetail IsNot Nothing AndAlso ListDeleteDefinitionRateDetail.Count > 0 Then
                    'valida que si tiene reglas eliminadas y cuenta con un orden de servicio 
                    If listIds IsNot Nothing AndAlso listIds?.Any() AndAlso _serviceOrderDetailRepository?.Any(Function(x) listIds.Contains(x.DefinitionRateDetailConditionId)) Then
                        Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {"No se puede actualizar el registro porque tiene una orden de servicios asociada!"}.ToList()}
                    End If
                    Dim result As ActionResult = DeleteList(ListDeleteDefinitionRateDetail, Company)
                    If result.StateResult = False Then
                        unitOfWork.RollbackChanges()
                        scope.Dispose()
                        Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {result.Message}.ToList()}
                    End If
                    messageDelete = result.Message
                End If

                Dim auxDefinitionRate As DefinitionRate = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of DefinitionRate)
                Dim status As Integer

                If DefinitionRate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    DefinitionRate.CreationUser = audit.CodeUser
                    DefinitionRate.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxDefinitionRate = DefinitionRate.OriginalValue
                    DefinitionRate.ModificationUser = audit.CodeUser
                    DefinitionRate.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._definitionRateRepository.SaveEntity(DefinitionRate)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of DefinitionRate)(DefinitionRate, audit, status, auxDefinitionRate)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                DefinitionRate.MarkAsUnchanged()

                scope.Complete()
                Return New ActionResult(Of DefinitionRate) With {.StateResult = True, .ObjectEmbbeded = DefinitionRate, .Message = messageDelete}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As DbUpdateException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of DefinitionRate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _definitionRateRepository = Nothing
            _secuenseDRepository = Nothing
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
