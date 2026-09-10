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

Public Class BillingItemsRestrictionAdminService
    Implements IBillingItemsRestrictionAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _BillingItemsRestrictionRepository As IBillingItemsRestrictionRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

    Private _billingItemsRestrictionDetailRepository As IBillingItemsRestrictionDetailRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal BillingItemsRestrictionRepository As IBillingItemsRestrictionRepository, ByVal secuenseDRepository As ISequenseContractDRepository, ByVal billingItemsRestrictionDetailRepository As IBillingItemsRestrictionDetailRepository)
        If BillingItemsRestrictionRepository Is Nothing Then
            Throw New ArgumentNullException("BillingItemsRestrictionRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _BillingItemsRestrictionRepository = BillingItemsRestrictionRepository
        _secuenseDRepository = secuenseDRepository
        _billingItemsRestrictionDetailRepository = billingItemsRestrictionDetailRepository
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
    Public Function ChangeStateBillingItemsRestriction(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of BillingItemsRestriction) Implements IBillingItemsRestrictionAdminService.ChangeStateBillingItemsRestriction
        Dim BillingItemsRestriction As BillingItemsRestriction = _BillingItemsRestrictionRepository.GetBillingItemsRestriction(code)
        BillingItemsRestriction.Status = state
        Return SaveBillingItemsRestriction(BillingItemsRestriction, Nothing, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBillingItemsRestriction(BillingItemsRestriction As BillingItemsRestriction, audit As AuditMessage) As ActionResult Implements IBillingItemsRestrictionAdminService.DeleteBillingItemsRestriction
        If BillingItemsRestriction Is Nothing Then
            Throw New ArgumentNullException("BillingItemsRestriction")
        End If
        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, BillingItemsRestriction.Company, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try

                command.CommandText = "DELETE  FROM [Contract].[BillingItemsRestrictionDetailCondition] WHERE [BillingItemsRestrictionDetailId] in (select id from [Contract].[BillingItemsRestrictionDetail] where [BillingItemsRestrictionId] = " + BillingItemsRestriction.Id.ToString + ")"
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[BillingItemsRestrictionDetail] WHERE [BillingItemsRestrictionId]=" & BillingItemsRestriction.Id
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[BillingItemsRestriction] WHERE [Id]=" & BillingItemsRestriction.Id
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
    ''' elimina detalles servicios no facturables
    ''' </summary>
    ''' <param name="ListDeleteBillingItemsRestrictionDetail"></param>
    ''' <param name="Company"></param>
    ''' <returns></returns>
    Private Function DeleteList(ListDeleteBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail), Company As String) As ActionResult
        If ListDeleteBillingItemsRestrictionDetail Is Nothing OrElse ListDeleteBillingItemsRestrictionDetail.Count = 0 Then
            Throw New ArgumentNullException("ListDeleteBillingItemsRestrictionDetail")
        End If
        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, Company, False))
        cnx.Open()
        Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
        Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
        command.CommandTimeout = 30000
        command.CommandType = CommandType.Text
        Try

            Dim errors As New System.Text.StringBuilder()

            For Each itemDelete As BillingItemsRestrictionDetail In ListDeleteBillingItemsRestrictionDetail

                command.CommandText = "DELETE  FROM [Contract].[BillingItemsRestrictionDetailCondition] WHERE [BillingItemsRestrictionDetailId] in (select id from [Contract].[BillingItemsRestrictionDetail] where [Id] = " + itemDelete.Id.ToString + ")"
                command.ExecuteNonQuery()

                command.CommandText = "DELETE  FROM [Contract].[BillingItemsRestrictionDetail] WHERE [Id]=" & itemDelete.Id
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
    Public Function GetBillingItemsRestriction(code As String, audit As AuditMessage) As ActionResult(Of BillingItemsRestriction) Implements IBillingItemsRestrictionAdminService.GetBillingItemsRestriction
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BillingItemsRestriction As BillingItemsRestriction = Me._BillingItemsRestrictionRepository.GetBillingItemsRestriction(code.Trim())

            Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = True, .ObjectEmbbeded = BillingItemsRestriction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBillingItemsRestrictionById(id As Integer, audit As AuditMessage) As ActionResult(Of BillingItemsRestriction) Implements IBillingItemsRestrictionAdminService.GetBillingItemsRestrictionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BillingItemsRestriction As BillingItemsRestriction = Me._BillingItemsRestrictionRepository.GetBillingItemsRestrictionById(id)
            If BillingItemsRestriction IsNot Nothing AndAlso BillingItemsRestriction.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BillingItemsRestriction)(BillingItemsRestriction, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = True, .ObjectEmbbeded = BillingItemsRestriction}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="BillingItemsRestriction"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBillingItemsRestriction(BillingItemsRestriction As BillingItemsRestriction, ListBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail), ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of BillingItemsRestriction) Implements IBillingItemsRestrictionAdminService.SaveBillingItemsRestriction

        If BillingItemsRestriction Is Nothing Then
            Throw New ArgumentNullException("BillingItemsRestriction")
        End If

        Dim unitOfWork As IUnitWork = Me._BillingItemsRestrictionRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Dim DetailUnitOfWork As IUnitWork = Me._billingItemsRestrictionDetailRepository.UnitWork
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim seq As ContractSequenceDetail = Nothing
                If BillingItemsRestriction.Code Is Nothing OrElse BillingItemsRestriction.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            BillingItemsRestriction.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Dim status As String
                If BillingItemsRestriction.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    BillingItemsRestriction.CreationUser = audit.CodeUser
                    BillingItemsRestriction.CreationDate = DateTime.Now
                    status = "Guardado"
                Else
                    BillingItemsRestriction.ModificationUser = audit.CodeUser
                    BillingItemsRestriction.ModificationDate = DateTime.Now
                    status = "Actualizado"
                End If

                Me._BillingItemsRestrictionRepository.SaveEntity(BillingItemsRestriction)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()

                If ListBillingItemsRestrictionDetail?.Any(Function(x) x.ChangeTracker.State = ObjectState.Deleted) Then
                    Me.DeleteList(ListBillingItemsRestrictionDetail.FindAll(Function(e) e.ChangeTracker.State = ObjectState.Deleted), BillingItemsRestriction.Company)
                End If

                ListBillingItemsRestrictionDetail?.FindAll(Function(e) e.ChangeTracker.State <> ObjectState.Deleted)?.ForEach(Sub(item)
                                                                                                                                  If item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                                                                                                                                      item.BillingItemsRestrictionId = BillingItemsRestriction.Id

                                                                                                                                  End If
                                                                                                                                  _billingItemsRestrictionDetailRepository.SaveEntity(item)
                                                                                                                              End Sub)
                DetailUnitOfWork.Commit()
                'Se marca la entidad como sin cambios
                BillingItemsRestriction.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = True, .ObjectEmbbeded = BillingItemsRestriction, .Message = $"El registro ha sido {status} con el codigo {BillingItemsRestriction?.Code}"}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As DbUpdateException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of BillingItemsRestriction) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles por id de cabecera
    ''' </summary>
    ''' <param name="idHeader"></param>
    ''' <returns></returns>
    Function GetItemsRestrictionDetailByIdHeader(idHeader As Integer) As ActionResult(Of List(Of BillingItemsRestrictionDetail)) Implements IBillingItemsRestrictionAdminService.GetItemsRestrictionDetailByIdHeader
        If idHeader = 0 Then
            Throw New ArgumentNullException("BillingItemsRestrictionId")
        End If
        Try
            Dim query = _billingItemsRestrictionDetailRepository.GetItemsRestrictionDetailByIdHeader(idHeader)

            Return New ActionResult(Of List(Of BillingItemsRestrictionDetail)) With {.StateResult = True, .ObjectEmbbeded = query, .Message = "Consulta exitosa"}
        Catch ex As Exception
            Return New ActionResult(Of List(Of BillingItemsRestrictionDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _BillingItemsRestrictionRepository = Nothing
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
