Imports System.Data.Entity.Core
Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class ElectronicSupportDocumentAdjustmentNoteAdminService
    Implements IElectronicSupportDocumentAdjustmentNoteAdminService, Inject

    ''' <summary>
    ''' Nombre del formulario
    ''' </summary>
    Private Const FORM_NAME As String = "FrmBillingAuthorization"
    ''' <summary>
    ''' Repositorio
    ''' </summary>
    Private _secuenseDRepository As IBillingSequenceDetailRepository
    ''' <summary>
    ''' 
    ''' </summary>
    Private _electronicSupportDocumentAdjustmentNoteRepository As IElectronicSupportDocumentAdjustmentNoteRepository

    Private _electronicSupportDocumentRepository As IElectronicSupportDocumentRepository


    ''' <summary>
    ''' Builder
    ''' </summary>
    ''' <param name="electronicSupportDocumentAdjustmentNoteRepository"></param>
    ''' <param name="secuenseDRepository"></param>
    Public Sub New(electronicSupportDocumentAdjustmentNoteRepository As IElectronicSupportDocumentAdjustmentNoteRepository,
                   secuenseDRepository As IBillingSequenceDetailRepository,
                   electronicSupportDocumentRepository As IElectronicSupportDocumentRepository)
        _secuenseDRepository = secuenseDRepository
        _electronicSupportDocumentRepository = electronicSupportDocumentRepository
        _electronicSupportDocumentAdjustmentNoteRepository = electronicSupportDocumentAdjustmentNoteRepository
    End Sub

    ''' <summary>
    ''' Consulta por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetElectronicSupportDocumentAdjustmentNoteByCode(code As String, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IElectronicSupportDocumentAdjustmentNoteAdminService.GetElectronicSupportDocumentAdjustmentNoteByCode
        If code Is String.Empty Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim electronicSupportDocumentNote = _electronicSupportDocumentAdjustmentNoteRepository _
                .FirstOrDefault(Function(m) m.Code = code, includes:={"ElectronicSupportDocument"})

            If electronicSupportDocumentNote IsNot Nothing Then
                electronicSupportDocumentNote.ElectronicSupportDocumentFullName = $"Código: {electronicSupportDocumentNote.ElectronicSupportDocument.Code} - Fecha: {electronicSupportDocumentNote.ElectronicSupportDocument.DocumentDate}"
                electronicSupportDocumentNote.ElectronicSupportDocumentDate = electronicSupportDocumentNote.ElectronicSupportDocument?.DocumentDate
                electronicSupportDocumentNote.ElectronicSupportDocumentValue = electronicSupportDocumentNote.ElectronicSupportDocument?.TotalValue
            End If

            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = True, .ObjectEmbbeded = electronicSupportDocumentNote}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = False, .Message = {ex.Message}.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Consulta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetElectronicSupportDocumentAdjustmentNoteById(id As Integer) As ElectronicSupportDocumentAdjustmentNote Implements IElectronicSupportDocumentAdjustmentNoteAdminService.GetElectronicSupportDocumentAdjustmentNoteById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return _electronicSupportDocumentAdjustmentNoteRepository.FindById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un documento
    ''' </summary>
    ''' <param name="electronicDocumentNote"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IElectronicSupportDocumentAdjustmentNoteAdminService.SaveElectronicSupportDocumentAdjusmentNote
        If electronicDocumentNote Is Nothing Then
            Throw New ArgumentNullException("electronicDocumentNote")
        End If

        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                New TransactionOptions() With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(electronicDocumentNote.Code) Then
                    Dim seq As BillingSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            electronicDocumentNote.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), electronicDocumentNote.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ElectronicSupportDocumentAdjustmentNote = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ElectronicSupportDocumentAdjustmentNote)
                Dim status As Integer

                If electronicDocumentNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    electronicDocumentNote.CreationUser = audit.CodeUser
                    electronicDocumentNote.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = electronicDocumentNote.OriginalValue
                    electronicDocumentNote.ModificationUser = audit.CodeUser
                    electronicDocumentNote.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                If electronicDocumentNote.Status = 2 Then
                    If electronicDocumentNote.ElectronicSupportDocument Is Nothing Then
                        electronicDocumentNote.ElectronicSupportDocument = _electronicSupportDocumentRepository _
                            .FirstOrDefault(Function(m) m.Id = electronicDocumentNote.ElectronicSupportDocumentId, False)
                    End If

                    electronicDocumentNote.CUDS = electronicDocumentNote.GetCUDSCode()
                    electronicDocumentNote.ConfirmationUser = audit.CodeUser
                    electronicDocumentNote.ConfirmationDate = Date.Now
                End If

                _electronicSupportDocumentAdjustmentNoteRepository.SaveEntity(electronicDocumentNote)
                _electronicSupportDocumentAdjustmentNoteRepository.UnitWork.Commit()

                auditProcess = New IndigoAuditSimpleEntity(Of ElectronicSupportDocumentAdjustmentNote)(electronicDocumentNote, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                electronicDocumentNote.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = electronicDocumentNote, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            _electronicSupportDocumentAdjustmentNoteRepository.UnitWork.RollbackChanges()
            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            _electronicSupportDocumentAdjustmentNoteRepository.UnitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el estado de un documento
    ''' </summary>
    ''' <param name="Ids"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateStateElectronicSupportDocumentsAdjusmentNote(Ids As List(Of Integer), audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IElectronicSupportDocumentAdjustmentNoteAdminService.UpdateStateElectronicSupportDocumentsAdjusmentNote
        Dim messages As New StringBuilder
        Dim errors As New StringBuilder
        If Ids.Any() Then
            For Each Id In Ids
                Dim resendStatus As Byte = 66 'Fuerza el reenvio
                Dim result = forwardDocument(Id, resendStatus, audit)
                If result.StateResult Then
                    messages.AppendLine(result.Message)
                Else
                    errors.AppendLine(result.Message)
                End If
            Next
            If errors.Length = 0 Then
                Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = True, .Message = String.Format("Se reenviaron correctamente los siguientes documentos: {0}", messages.ToString())}
            Else
                Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = False, .Message = errors.ToString()}
            End If
        Else
            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = False, .Message = "No se recibió ningún documento para reenviar"}
        End If
    End Function

    Public Function ConfirmElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IElectronicSupportDocumentAdjustmentNoteAdminService.ConfirmElectronicSupportDocumentAdjusmentNote
        Throw New NotImplementedException()
    End Function

    Public Function AnulateElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IElectronicSupportDocumentAdjustmentNoteAdminService.AnulateElectronicSupportDocumentAdjusmentNote
        Throw New NotImplementedException()
    End Function

#Region "Private functions"

    ''' <summary>
    ''' Actualiza el estado de un documento para ser reenviado
    ''' </summary>
    ''' <param name="IdElectronicSupportDocumentAdjustmentNote"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function forwardDocument(IdElectronicSupportDocumentAdjustmentNote As Integer, status As Byte, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote)
        Dim unitWork = _electronicSupportDocumentAdjustmentNoteRepository.UnitWork
        Try
            Dim documentType = String.Empty
            Dim electronicSupportDocumentAdjustmentNote = _electronicSupportDocumentAdjustmentNoteRepository.GetByFilter(Function(f) f.Id = IdElectronicSupportDocumentAdjustmentNote).FirstOrDefault()
            If electronicSupportDocumentAdjustmentNote?.Id > 0 Then
                If electronicSupportDocumentAdjustmentNote.StatusElectronic = 3 Then
                    Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = False, .Message = "No se puede reenviar el documento debido a que se encuentra en estado válido"}
                End If
                electronicSupportDocumentAdjustmentNote.StatusElectronic = status
                electronicSupportDocumentAdjustmentNote.MarkAsModified
                _electronicSupportDocumentAdjustmentNoteRepository.SaveEntity(electronicSupportDocumentAdjustmentNote)
                unitWork.Commit()

                Dim auditProcess = New IndigoAuditSimpleEntity(Of ElectronicSupportDocumentAdjustmentNote)(electronicSupportDocumentAdjustmentNote, audit, Infrastructure.CrossCutting.Audit.Actions.Update, electronicSupportDocumentAdjustmentNote.OriginalValue)
                auditProcess.Execute()
            End If

            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = True, .Message = $"{electronicSupportDocumentAdjustmentNote.Code}, "}
        Catch ex As Exception
            unitWork.RollbackChanges()
            Return New ActionResult(Of ElectronicSupportDocumentAdjustmentNote) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function
#End Region

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: eliminar el estado administrado (objetos administrados)
            End If

            ' TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            ' TODO: establecer los campos grandes como NULL
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
End Class
