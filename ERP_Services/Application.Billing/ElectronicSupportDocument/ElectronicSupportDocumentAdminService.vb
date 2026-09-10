#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Application.Security
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Text
Imports Application.Treasury
Imports Application.EventHandlers.Model
Imports Application.EventHandlers
Imports Application.EventHandlers.Enums.Enums

#End Region

Public Class ElectronicSupportDocumentAdminService
    Implements IElectronicSupportDocumentAdminService
    Private Const FORM_NAME As String = "FrmElectronicSupportDocument"

#Region "Properties"

    'Repositorios
    Private _electronicSupportDocumentRepository As IElectronicSupportDocumentRepository
    Private _sequenceDetailRepository As IBillingSequenceDetailRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _billingAuthorizationRepository As IBillingAuthorizationRepository
    Private _settingsAccountRepository As ISettingsAccountRepository
    Private ReadOnly _eventProxy As IEventProxy


#End Region

#Region "Builder"

    Public Sub New(ElectronicSupportDocumentRepository As IElectronicSupportDocumentRepository, sequenceDetailRepository As IBillingSequenceDetailRepository, ThirdPartyRepository As IThirdPartyRepository,
            BillingAuthorizationRepository As IBillingAuthorizationRepository, settingsAccountRepository As ISettingsAccountRepository,
            eventProxy As IEventProxy)

        Me._electronicSupportDocumentRepository = ElectronicSupportDocumentRepository
        _sequenceDetailRepository = sequenceDetailRepository
        _thirdPartyRepository = ThirdPartyRepository
        _billingAuthorizationRepository = BillingAuthorizationRepository
        Me._settingsAccountRepository = settingsAccountRepository
        Me._eventProxy = eventProxy
    End Sub

#End Region

#Region "Methods"

    Public Function GetElectronicSupportDocumentById(id As Integer) As ActionResult(Of ElectronicSupportDocument) Implements IElectronicSupportDocumentAdminService.GetElectronicSupportDocumentById
        Try
            If id = 0 Then
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = "el Id no puede ser 0"}
            End If
            Dim Result = Me._electronicSupportDocumentRepository.GetByFilter(Function(x) x.Id = id).FirstOrDefault

            If Result Is Nothing OrElse Result.Id = 0 Then
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = "No se encontró el documento"}
            End If
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = True, .ObjectEmbbeded = Result, .Message = ""}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    Public Function GetElectronicSupportDocumentByCode(code As String, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument) Implements IElectronicSupportDocumentAdminService.GetElectronicSupportDocumentByCode
        Try
            If String.IsNullOrEmpty(code) Then
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = "El codigo no puede ser vacio"}
            End If

            Dim Result = Me._electronicSupportDocumentRepository.GetByFilter(Function(x) x.Code = code, True, {"ThirdParty", "BillingAuthorization"}).FirstOrDefault

            If Result Is Nothing OrElse Result.Id = 0 Then
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .ObjectEmbbeded = New ElectronicSupportDocument, .Message = $"No existe el documento con codigo {code} "}
            End If

            Result.SupplierCodeName = $"{Result.ThirdParty?.Nit} - {Result.ThirdParty?.Name}"
            Result.SupplierNit = Result.ThirdParty?.Nit
            Result.BillingAuthorizationCodeName = $"{Result.BillingAuthorization?.Code} - {Result.BillingAuthorization?.Name}"
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = True, .ObjectEmbbeded = Result, .Message = ""}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = ex.Message, .ObjectEmbbeded = New ElectronicSupportDocument}
        End Try
    End Function

    ''' <summary>
    ''' funcion para guardar y confirmar
    ''' </summary>
    ''' <param name="ElectronicSupportDocument"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveConfirmElectronicSupportDocument(ElectronicSupportDocument As ElectronicSupportDocument, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult Implements IElectronicSupportDocumentAdminService.SaveConfirmElectronicSupportDocument
        Try
            If ElectronicSupportDocument Is Nothing Then
                Throw New Exception("La entidad viene vacia")
            End If
            Dim ToConfirm As Boolean = ElectronicSupportDocument.ToConfirm

            Dim ResultSave = SaveElectronicSupportDocument(ElectronicSupportDocument, audit, idSequence)
            If ResultSave Is Nothing OrElse Not ResultSave?.StateResult Then
                Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ResultSave?.Message, .MessageResult = ResultSave?.MessageResult}
            End If

            Dim MessageX = IIf(ElectronicSupportDocument.ChangeTracker.State = ObjectState.Added, "Se guardó", "Se Actualizó")

            If Not ToConfirm Then
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = $"{MessageX} Correctamente"}
            End If

            Dim GetResult = GetElectronicSupportDocumentByCode(ElectronicSupportDocument.Code, audit)

            If GetResult Is Nothing OrElse Not GetResult?.StateResult OrElse GetResult?.ObjectEmbbeded Is Nothing Then
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.WARNING, .Message = $"{MessageX} pero No se confirmó"}
            End If
            Dim _electronicSDToConfirm = GetResult.ObjectEmbbeded
            Dim ResultConfirm = ConfirmElectronicSupportDocument(_electronicSDToConfirm, audit)

            If ResultConfirm?.StateResult Then
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = $"{MessageX} y confirmó Correctamente"}
            Else
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = $"{MessageX} pero no se confirmó", .MessageResult = {ResultConfirm?.Message}.ToList}
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function UpdateStateElectronicSupportDocuments(Ids As List(Of Integer), audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument) Implements IElectronicSupportDocumentAdminService.UpdateStateElectronicSupportDocuments
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
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = True, .Message = String.Format("Se reenviaron correctamente los siguientes documentos: {0}", messages.ToString())}
            Else
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = errors.ToString()}
            End If
        Else
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = "No se recibió ningún documento para reenviar"}
        End If
    End Function

    ''' <summary>
    ''' Guardar
    ''' </summary>
    ''' <param name="ElectronicSupportDocument">The reversal reason.</param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveElectronicSupportDocument(ElectronicSupportDocument As ElectronicSupportDocument, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ElectronicSupportDocument)

        Dim unitOfWork As IUnitWork = Me._electronicSupportDocumentRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(ElectronicSupportDocument.Code) Then
                    Dim seq As BillingSequenceDetail = Me._sequenceDetailRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            ElectronicSupportDocument.Code = res
                            seq.Next += 1
                            Me._sequenceDetailRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ElectronicSupportDocument) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.BillingSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), ElectronicSupportDocument.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ElectronicSupportDocument) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ElectronicSupportDocument = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ElectronicSupportDocument)
                Dim status As Integer

                If ElectronicSupportDocument.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    ElectronicSupportDocument.CreationUser = audit.CodeUser
                    ElectronicSupportDocument.CreationDate = DateTime.Now
                    Dim _customerThirdPartyId = _thirdPartyRepository.GetByFilter(Function(d) d.Nit = ElectronicSupportDocument.IndigoCompanyNit, False)?.FirstOrDefault?.Id
                    If _customerThirdPartyId Is Nothing Then
                        Throw New Exception("El Nit de la empresa es incorrecto")
                    End If
                    ElectronicSupportDocument.CustomerThirdPartyId = _customerThirdPartyId
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = ElectronicSupportDocument.OriginalValue
                    ElectronicSupportDocument.ModificationUser = audit.CodeUser
                    ElectronicSupportDocument.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If
                Me._electronicSupportDocumentRepository.SaveEntity(ElectronicSupportDocument)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ElectronicSupportDocument)(ElectronicSupportDocument, audit, status, auxObjEntity)
                auditProcess.Execute()

                scope.Complete()
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = MessageResult, .ObjectEmbbeded = ElectronicSupportDocument}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' confirmar
    ''' </summary>
    ''' <param name="ElectronicSupportDocument"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ConfirmElectronicSupportDocument(ElectronicSupportDocument As ElectronicSupportDocument, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument)

        Dim unitOfWork As IUnitWork = Me._electronicSupportDocumentRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                If ElectronicSupportDocument?.BillingAuthorization Is Nothing OrElse ElectronicSupportDocument?.BillingAuthorization?.FinalInvoice < ElectronicSupportDocument?.BillingAuthorization?.Consecutive _
                    OrElse ElectronicSupportDocument?.BillingAuthorization.FinalDate < ElectronicSupportDocument?.RadicationDate Then
                    Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {"-999"}.ToList, .Message = "Surgió un error con la autorización de facturacion"}
                End If

                'Consulto los parametros de Contabilidad definidos para la unidad operativa
                Dim settingsAccount = Me._settingsAccountRepository.GetSettingAccountSimple(ElectronicSupportDocument.OperativeUnitId)
                If settingsAccount Is Nothing OrElse settingsAccount.Id = 0 OrElse String.IsNullOrEmpty(settingsAccount.SoftwarePin) Then
                    Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = "No se encontro parametros de contabilidad para la unidad operativa seleccionada", .MessageResult = {"-999"}.ToList}
                End If

                ElectronicSupportDocument.DocumentNumber = $"{ElectronicSupportDocument.BillingAuthorization.InvoicePrefix}{ElectronicSupportDocument.BillingAuthorization.Consecutive}"
                ElectronicSupportDocument.BillingAuthorization.Consecutive += 1
                ElectronicSupportDocument.SoftwarePin = settingsAccount.SoftwarePin
                ElectronicSupportDocument.Environment = IIf(settingsAccount.SupportDocumentEnvironment, 1, 2)
                ElectronicSupportDocument.CUDS = ElectronicSupportDocument.GetCUDSCode()

                Dim MessageResult As String = String.Empty
                MessageResult = ResourceManager.GetString("UpdateMessage")
                ElectronicSupportDocument.ModificationUser = audit.CodeUser
                ElectronicSupportDocument.ModificationDate = DateTime.Now
                ElectronicSupportDocument.Status = True

                Me._electronicSupportDocumentRepository.SaveEntity(ElectronicSupportDocument)
                unitOfWork.Commit()
                scope.Complete()
                Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = MessageResult, .ObjectEmbbeded = ElectronicSupportDocument}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#End Region

#Region "Private Functions"

    Private Function forwardDocument(IdElectronicSupportDocument As Integer, status As Byte, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument)
        Dim unitWork = _electronicSupportDocumentRepository.UnitWork
        Try
            Dim documentType = String.Empty
            Dim electronicSupportDocument = _electronicSupportDocumentRepository.GetByFilter(Function(f) f.Id = IdElectronicSupportDocument).FirstOrDefault()
            If electronicSupportDocument?.Id > 0 Then
                If electronicSupportDocument.StatusElectronic = 3 Then
                    Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = "No se puede reenviar el documento debido a que se encuentra en estado válido"}
                End If
                electronicSupportDocument.StatusElectronic = status
                electronicSupportDocument.MarkAsModified
                _electronicSupportDocumentRepository.SaveEntity(electronicSupportDocument)

                Me.publishElectronicSupporDocumentMessages(electronicSupportDocument, audit)

                unitWork.Commit()

                Dim auditProcess = New IndigoAuditSimpleEntity(Of ElectronicSupportDocument)(electronicSupportDocument, audit, Infrastructure.CrossCutting.Audit.Actions.Update, electronicSupportDocument.OriginalValue)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = True, .Message = $"{electronicSupportDocument.Code}, "}
        Catch ex As Exception
            unitWork.RollbackChanges()
            Return New ActionResult(Of ElectronicSupportDocument) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para publicar el documento soporte a partir de cuentas por pagar
    ''' </summary>
    ''' <param name="electronicSupportDocument"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Function publishElectronicSupporDocumentMessages(electronicSupportDocument As ElectronicSupportDocument, audit As AuditMessage) As ActionResult(Of List(Of String))
        If electronicSupportDocument Is Nothing Then
            Throw New ArgumentNullException("listAccountPayable")
        End If

        Dim data As New List(Of SupportDocumentEvent)

        data.Add(New SupportDocumentEvent With {
            .EntityId = electronicSupportDocument.EntityId,
            .EntityCode = electronicSupportDocument.EntityCode,
            .EntityName = electronicSupportDocument.EntityName,
            .DocumentDate = electronicSupportDocument.DocumentDate})

        _eventProxy.Publish(New EventData(data, NameOf(EventType.SupportDocument), NameOf(EventAction.added), audit.Company, audit.CodeUser, DateTime.Now().GetTimestamp))

        Return New ActionResult(Of List(Of String)) With {.StateResult = True}
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If

            Me._electronicSupportDocumentRepository = Nothing
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
