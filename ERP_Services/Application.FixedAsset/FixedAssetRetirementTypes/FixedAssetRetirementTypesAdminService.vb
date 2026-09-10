#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Text
Imports Domain.Entities.Service
Imports Application.Payments
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class FixedAssetRetirementTypesAdminService
    Implements IFixedAssetRetirementTypesAdminService

#Region "Variables"

    'FixedAssetRetirementTypes
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _fixedAssetRetirementTypesRepository As IFixedAssetRetirementTypesRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IFixedAssetSequenceDetailRepository

    Public Const FORM_NAME As String = "FrmFixedAssetRetirementTypes"

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal FixedAssetRetirementTypesRepository As IFixedAssetRetirementTypesRepository,
                   ByVal secuenseDRepository As IFixedAssetSequenceDetailRepository)
        If FixedAssetRetirementTypesRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetRetirementTypesRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _fixedAssetRetirementTypesRepository = FixedAssetRetirementTypesRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Trae todas los Tipos de baja de activos fijos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllFixedAssetRetirementTypes(audit As AuditMessage) As List(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesAdminService.GetAllFixedAssetRetirementTypes
        Try
            Dim fixedAssetRetirementTypes = Me._fixedAssetRetirementTypesRepository.GetAll()
            For Each item As FixedAssetRetirementTypes In fixedAssetRetirementTypes
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetRetirementTypes)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return fixedAssetRetirementTypes
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda los Tipos de baja de activos fijos
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveFixedAssetRetirementTypes(FixedAssetRetirementTypes As FixedAssetRetirementTypes, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesAdminService.SaveFixedAssetRetirementTypes
        If FixedAssetRetirementTypes Is Nothing Then
            Throw New ArgumentNullException("FixedAssetRetirementTypes")
        End If
        Dim unitOfWork As IUnitWork = Me._fixedAssetRetirementTypesRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                Dim seq As FixedAssetSequenceDetail = Nothing

                Dim validation = _fixedAssetRetirementTypesRepository?.Any(Function(x) True)
                If validation Is Nothing OrElse Not validation Then
                    _fixedAssetRetirementTypesRepository.ExecuteNonQuery(String.Format("SET IDENTITY_INSERT FixedAsset.FixedAssetRetirementTypes ON
                                                                                        INSERT into FixedAsset.FixedAssetRetirementTypes (Id,Code,Name, RecordReplacementValue, CreationUser,CreationDate) VALUES 
														                                                                                        (1,'001','Perdida', 1,'999',{0}),
														                                                                                        (2,	'002',	'Siniestro',	0,	'999',	{0}),
														                                                                                        (3,	'003',	'Perdida Reposicion',	1,	'999',	{0}),
														                                                                                        (4,	'004',	'Bienes Inservibles',	0,	'999',	{0}),
														                                                                                        (5,	'005',	'Obsolescencia',	1,	'999',	{0})
                                                                                        SET IDENTITY_INSERT FixedAsset.FixedAssetRetirementTypes OFF
                                                                                        ", Format(Date.Now, "yyyy-MM-dd")))
                End If

                If FixedAssetRetirementTypes.Code Is Nothing OrElse FixedAssetRetirementTypes.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetRetirementTypes.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetRetirementTypes) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), FixedAssetRetirementTypes.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetRetirementTypes) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxFixedAssetRetirementTypes As FixedAssetRetirementTypes = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetRetirementTypes)
                Dim status As Integer

                If FixedAssetRetirementTypes.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    FixedAssetRetirementTypes.CreationUser = audit.CodeUser
                    FixedAssetRetirementTypes.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxFixedAssetRetirementTypes = FixedAssetRetirementTypes.OriginalValue
                    FixedAssetRetirementTypes.ModificationUser = audit.CodeUser
                    FixedAssetRetirementTypes.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._fixedAssetRetirementTypesRepository.SaveEntity(FixedAssetRetirementTypes)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetRetirementTypes)(FixedAssetRetirementTypes, audit, status, auxFixedAssetRetirementTypes)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetRetirementTypes.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetRetirementTypes) With {.StateResult = True, .ObjectEmbbeded = FixedAssetRetirementTypes}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetRetirementTypes) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetRetirementTypes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un Tipo de baja de activo fijo
    ''' </summary>
    ''' <param name="FixedAssetRetirementTypes"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteCompanyType(FixedAssetRetirementTypes As FixedAssetRetirementTypes, audit As AuditMessage) As ActionResult Implements IFixedAssetRetirementTypesAdminService.DeleteFixedAssetRetirementTypes
        If FixedAssetRetirementTypes Is Nothing Then
            Throw New ArgumentNullException("FixedAssetRetirementTypes")
        End If
        Dim unitOfWork As IUnitWork = Me._fixedAssetRetirementTypesRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetRetirementTypes)
            auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetRetirementTypes)(FixedAssetRetirementTypes, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._fixedAssetRetirementTypesRepository.DeleteEntity(FixedAssetRetirementTypes)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Tipo de baja de activo fijo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetRetirementTypesByCode(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesAdminService.GetFixedAssetRetirementTypesByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetRetirementTypes As FixedAssetRetirementTypes = Me._fixedAssetRetirementTypesRepository.GetFixedAssetRetirementTypesByCode(code.Trim())
            If FixedAssetRetirementTypes IsNot Nothing AndAlso FixedAssetRetirementTypes.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetRetirementTypes)(FixedAssetRetirementTypes, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetRetirementTypes) With {.StateResult = True, .ObjectEmbbeded = FixedAssetRetirementTypes}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetRetirementTypes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Tipo de baja de activo fijo por id
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetRetirementTypesById(id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes) Implements IFixedAssetRetirementTypesAdminService.GetFixedAssetRetirementTypesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetRetirementTypes As FixedAssetRetirementTypes = Me._fixedAssetRetirementTypesRepository.GetFixedAssetRetirementTypesById(id)
            If FixedAssetRetirementTypes IsNot Nothing AndAlso FixedAssetRetirementTypes.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetRetirementTypes)(FixedAssetRetirementTypes, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetRetirementTypes) With {.StateResult = True, .ObjectEmbbeded = FixedAssetRetirementTypes}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetRetirementTypes) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _fixedAssetRetirementTypesRepository = Nothing
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
