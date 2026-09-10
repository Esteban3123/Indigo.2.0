'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
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
Imports Domain.Entities.Service
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

Public Class CareGroupAdminService
    Implements ICareGroupAdminService


#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupRepository As ICareGroupRepository
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _careGroupCommitRepository As ICareGroupRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository
    ''' <summary>
    ''' Variable de tipo repositorio para manual tarifario
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualRepository As IRateManualRepository
    ''' <summary>
    ''' Servicios de aplicacion para el detalle de grupo de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractServices As IContractServices
#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal careGroupRepository As ICareGroupRepository, ByVal secuenseDRepository As ISequenseContractDRepository, rateManualRepository As IRateManualRepository,
                   careGroupCommitRepository As ICareGroupRepository, contracServices As IContractServices)
        If careGroupRepository Is Nothing Then
            Throw New ArgumentNullException("careGroupRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _careGroupRepository = careGroupRepository
        _secuenseDRepository = secuenseDRepository
        _rateManualRepository = rateManualRepository

        _careGroupCommitRepository = careGroupCommitRepository

        _contractServices = contracServices
    End Sub

#End Region

#Region "Methods"
    Public Function ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId As Integer) As List(Of CareGroupInvoiceCategories) Implements ICareGroupAdminService.ListCareGroupInvoiceCategoriesByCareGroupId
        Try
            Return _careGroupRepository.ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId)
        Catch ex As Exception
            Return New List(Of CareGroupInvoiceCategories)
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateCareGroup(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of CareGroup) Implements ICareGroupAdminService.ChangeStateCareGroup
        Dim CareGroup As CareGroup = _careGroupRepository.GetCareGroupById(id)
        CareGroup.Status = state
        CareGroup.MarkAsModified()
        Return SaveCareGroup(CareGroup, audit)
    End Function

    ''' <summary>
    ''' Elimina un grupo de atencion
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteCareGroup(id As Integer, Company As String, audit As AuditMessage) As ActionResult Implements ICareGroupAdminService.DeleteCareGroup
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If

        Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, Company, False))
            cnx.Open()
            Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
            Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text

            Try
                'Se eliminan todos los detalles de definición de tarifas
                command.CommandText = "DELETE  FROM [Contract].[CareGroupDefinitionRate] WHERE [CareGroupId] = " & id
                command.ExecuteNonQuery()

                'Se eliminan todos los detalles de facturas
                command.CommandText = "DELETE  FROM [Contract].[CareGroupInvoiceCategories] WHERE [CareGroupId] = " & id
                command.ExecuteNonQuery()

                'Se elimina la cabecera
                command.CommandText = "DELETE  FROM [Contract].[CareGroup] WHERE [Id] = " & id
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
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroup(code As String, audit As AuditMessage) As ActionResult(Of CareGroup) Implements ICareGroupAdminService.GetCareGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CareGroup As CareGroup = Me._careGroupRepository.GetCareGroup(code.Trim())
            If CareGroup IsNot Nothing AndAlso CareGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CareGroup)(CareGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CareGroup) With {.StateResult = True, .ObjectEmbbeded = CareGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CareGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un grupo de atencion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of CareGroup) Implements ICareGroupAdminService.GetCareGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CareGroup As CareGroup = Me._careGroupRepository.GetCareGroupById(id)
            If CareGroup IsNot Nothing AndAlso CareGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CareGroup)(CareGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CareGroup) With {.StateResult = True, .ObjectEmbbeded = CareGroup}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CareGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un grupo de atencion
    ''' </summary>
    ''' <param name="CareGroup"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCareGroup(CareGroup As CareGroup, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CareGroup) Implements ICareGroupAdminService.SaveCareGroup
        If CareGroup Is Nothing Then
            Throw New ArgumentNullException("CareGroup")
        End If
        Dim unitOfWork As IUnitWork = Me._careGroupCommitRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadUncommitted
        'inicio la transaccion
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As ContractSequenceDetail = Nothing
                If CareGroup.Code Is Nothing OrElse CareGroup.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            CareGroup.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of CareGroup) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of CareGroup) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                If CareGroup.Id > 0 Then
                    CareGroup.OperativeUnitId = CareGroup.OriginalValue.OperativeUnitId
                End If

                Dim auxCareGroup As CareGroup = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CareGroup)
                Dim status As Integer
                If CareGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CareGroup.CreationUser = audit.CodeUser
                    CareGroup.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxCareGroup = CareGroup.OriginalValue
                    CareGroup.ModificationUser = audit.CodeUser
                    CareGroup.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                CareGroup.CareGroupPackage.ToList().ForEach(Sub(i As CareGroupPackage)
                                                                If i.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                                                                    i.CreationUser = audit.CodeUser
                                                                    i.CreationDate = DateTime.Now
                                                                Else
                                                                    i.ModificationUser = audit.CodeUser
                                                                    i.ModificationDate = DateTime.Now
                                                                End If
                                                            End Sub)


                Me._careGroupCommitRepository.SaveEntity(CareGroup)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CareGroup)(CareGroup, audit, status, auxCareGroup)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                CareGroup.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of CareGroup) With {.StateResult = True, .ObjectEmbbeded = CareGroup}
            Catch ex As OptimisticConcurrencyException
                sequenseUnitOfWork.RollbackChanges()
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of CareGroup) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As DbUpdateException
                sequenseUnitOfWork.RollbackChanges()
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of CareGroup) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                sequenseUnitOfWork.RollbackChanges()
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CareGroup) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    Public Function GetGroupersCareGroup(CareGroupId As Integer, grouperId As Integer, ByVal audit As AuditMessage) As GroupersCareGroup Implements ICareGroupAdminService.GetGroupersCareGroup
        If CareGroupId = 0 Then
            Throw New ArgumentNullException("CareGroupId")
        End If
        If grouperId = 0 Then
            Throw New ArgumentNullException("grouperId")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim GroupersCareGroup As GroupersCareGroup = Me._careGroupRepository.GetGroupersCareGroup(CareGroupId, grouperId)
            If GroupersCareGroup IsNot Nothing And GroupersCareGroup.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of GroupersCareGroup)(GroupersCareGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return GroupersCareGroup
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New GroupersCareGroup
        End Try
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _contractServices.Dispose()
            End If
            _careGroupRepository = Nothing
            _secuenseDRepository = Nothing
            _rateManualRepository = Nothing
            _careGroupCommitRepository = Nothing
            _contractServices = Nothing
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
