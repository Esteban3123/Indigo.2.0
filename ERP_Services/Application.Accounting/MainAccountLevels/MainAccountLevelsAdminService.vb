Imports Domain.Accounting
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Accounting
Imports Infrastructure.CrossCutting
Imports Infrastructure.Data.ModelRepository
Imports System.Data.Entity.Core.Mapping
Imports System.Text

Public Class MainAccountLevelsAdminService
    Implements IMainAccountLevelsAdminService, Inject


#Region "Variables"

    Private _mainAccountLevelsRepository As IMainAccountLevelsRepository

    Private _secuenseDRepository As ISequenseAccountingDRepository

    Public Const FORM_NAME As String = "FrmMainAccountLevels"

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal MainAccountLevelsRepository As IMainAccountLevelsRepository, secuenseDRepository As ISequenseAccountingDRepository)
        If MainAccountLevelsRepository Is Nothing Then
            Throw New ArgumentNullException("MainAccountLevelsRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _mainAccountLevelsRepository = MainAccountLevelsRepository
        _secuenseDRepository = secuenseDRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Trae todas los Niveles de cuntas contables
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllMainAccountLevels(ByVal audit As AuditMessage) As List(Of MainAccountLevels) Implements IMainAccountLevelsAdminService.GetAllMainAccountLevels

        Try
            Dim MainAccountLevels = Me._mainAccountLevelsRepository.GetAll()
            For Each item As MainAccountLevels In MainAccountLevels
                Dim auditObject As New IndigoAuditSimpleEntity(Of MainAccountLevels)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return MainAccountLevels
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' guarda el Nivele de cunta contable
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveMainAccountLevels(MainAccountLevels As MainAccountLevels, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of MainAccountLevels) Implements IMainAccountLevelsAdminService.SaveMainAccountLevels
        If MainAccountLevels Is Nothing Then
            Throw New ArgumentNullException("MainAccountLevels Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _mainAccountLevelsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                Dim seq As GeneralLedgerSequenceDetail = Nothing

                Dim DigitMainAccount = MainAccountLevels.digits
                Dim LevelMainACcount = MainAccountLevels.Level

                'Valida que el digito y el nivel no se repitan
                'validamos que el numero sea mayor al anterios y calculamos longitud de estos dos 
                Dim validateLevel = _mainAccountLevelsRepository.GetByFilter(Function(d) d.Level <= LevelMainACcount)
                Dim validateDigit = _mainAccountLevelsRepository.GetByFilter(Function(d) d.digits <= DigitMainAccount)
                Dim LevelMax = validateLevel.OrderByDescending(Function(s) s.Level).FirstOrDefault().Level

                Dim str = New StringBuilder
                If validateDigit?.Any() Then

                    'valida que el nivel nuevo no exista
                    If validateLevel.Any(Function(b) b.Level = LevelMainACcount) Then
                        str.AppendLine("El Nivel ya existe")
                        Return New ActionResult(Of MainAccountLevels) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = str.ToString()}
                    End If

                    'valida que el digito nuevo no exista
                    If validateDigit.Any(Function(d) d.digits = DigitMainAccount) Then
                        str.AppendLine("El Digito ya existe")
                        Return New ActionResult(Of MainAccountLevels) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = str.ToString()}
                    End If

                    'valida que el nivel anterior exista - si la resta es diferente de 1 entra al mensaje
                    If (LevelMainACcount - LevelMax) <> 1 Then
                        str.AppendLine("El nivel ha guardar debe ser consecutivo")
                        Return New ActionResult(Of MainAccountLevels) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = str.ToString()}
                    End If

                    'hace el calculo de length
                    Dim xs = validateDigit.OrderByDescending(Function(f) f.digits).FirstOrDefault
                    MainAccountLevels.Length = DigitMainAccount - xs.digits
                Else
                    MainAccountLevels.Length = MainAccountLevels.digits
                End If

                'Secuencia de codigo
                If MainAccountLevels.Code Is Nothing OrElse MainAccountLevels.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            MainAccountLevels.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of MainAccountLevels) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), MainAccountLevels.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of MainAccountLevels) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxAccounting As MainAccountLevels = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of MainAccountLevels)
                Dim status As Integer

                If MainAccountLevels.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    MainAccountLevels.CreationUser = audit.CodeUser
                    MainAccountLevels.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxAccounting = MainAccountLevels.OriginalValue
                    MainAccountLevels.ModificationUser = audit.CodeUser
                    MainAccountLevels.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._mainAccountLevelsRepository.SaveEntity(MainAccountLevels)
                UnitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of MainAccountLevels)(MainAccountLevels, audit, status, auxAccounting)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                MainAccountLevels.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of MainAccountLevels) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .ObjectEmbbeded = MainAccountLevels, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            UnitOfWork.RollbackChanges()
            Return New ActionResult(Of MainAccountLevels) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MainAccountLevels) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un Nivele de cuenta contable
    ''' </summary>
    ''' <param name="MainAccountLevels"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteMainAccountLevels(MainAccountLevels As MainAccountLevels, audit As AuditMessage) As ActionResult Implements IMainAccountLevelsAdminService.DeleteMainAccountLevels
        If MainAccountLevels Is Nothing Then
            Throw New ArgumentNullException("MainAccountLevels")
        End If
        Dim unitOfWork As IUnitWork = Me._mainAccountLevelsRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of MainAccountLevels)
            auditProcess = New IndigoAuditSimpleEntity(Of MainAccountLevels)(MainAccountLevels, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._mainAccountLevelsRepository.DeleteEntity(MainAccountLevels)
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
    ''' Obtiene un Nivele de cunta contable por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMainAccountLevelsByCode(code As String, audit As AuditMessage) As ActionResult(Of MainAccountLevels) Implements IMainAccountLevelsAdminService.GetMainAccountLevelsByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim mainaccountlevels As MainAccountLevels = Me._mainAccountLevelsRepository.GetMainAccountLevelsByCode(code)
            If mainaccountlevels IsNot Nothing AndAlso mainaccountlevels.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MainAccountLevels)(mainaccountlevels, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of MainAccountLevels) With {.StateResult = True, .ObjectEmbbeded = mainaccountlevels}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MainAccountLevels) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Nivele de cunta contable por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMainAccountLevelsById(id As Integer, audit As AuditMessage) As ActionResult(Of MainAccountLevels) Implements IMainAccountLevelsAdminService.GetMainAccountLevelsById
        If Not (id > 0) Then
            Throw New ArgumentNullException("id Vacio")
        End If
        Try
            Dim mainaccountlevels As MainAccountLevels = Me._mainAccountLevelsRepository.GetMainAccountLevelsById(id)
            If mainaccountlevels IsNot Nothing AndAlso mainaccountlevels.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of MainAccountLevels)(mainaccountlevels, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of MainAccountLevels) With {.StateResult = True, .ObjectEmbbeded = mainaccountlevels}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of MainAccountLevels) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
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
            _mainAccountLevelsRepository = Nothing
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
