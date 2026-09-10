#Region "Imports"

Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Treasury.Model
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports System.Globalization
Imports System.Net.Security
Imports System.Transactions

#End Region

Public Class BankReconciliationAutomaticAdminService
    Implements IBankReconciliationAutomaticAdminService

#Region "Properties"

    Private Const FORM_NAME As String = "FrmBankReconciliationAutomatic"

    Private _sequenseDRepository As ISequenseTreasuryDRepository
    Private _BankReconciliationAutomaticRepository As IBankReconciliationAutomaticRepository
    Private _AutomaticAssociationRepository As IBankReconciliationAutomaticAssociationRepository
    Private _BankRepository As IBankRepository
    Private _EntityBankAccount As IEntityBankAccountRepository
    Private _sequenseCRepository As ISequenseTreasuryCRepository
    Private _treasuryNoteAdminService As ITreasuryNoteAdminService
#End Region

#Region "Builder"

    Public Sub New(sequenseDRepository As ISequenseTreasuryDRepository, BankReconciliationAutomaticRepository As IBankReconciliationAutomaticRepository, AutomaticAssociationRepository As IBankReconciliationAutomaticAssociationRepository, BankRepository As IBankRepository, EntityBankaccount As IEntityBankAccountRepository, sequenseCRepository As ISequenseTreasuryCRepository, treasuryNoteAdminServices As ITreasuryNoteAdminService)
        If sequenseDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenseDRepository Vacio")
        End If
        If BankReconciliationAutomaticRepository Is Nothing Then
            Throw New ArgumentNullException("BankReconciliationAutomaticRepository Vacio")
        End If

        _sequenseDRepository = sequenseDRepository
        _BankReconciliationAutomaticRepository = BankReconciliationAutomaticRepository
        _AutomaticAssociationRepository = AutomaticAssociationRepository
        _BankRepository = BankRepository
        _EntityBankAccount = EntityBankaccount
        _sequenseCRepository = sequenseCRepository
        _treasuryNoteAdminService = treasuryNoteAdminServices
    End Sub

#End Region

#Region "Methods"

    Public Function GetBankReconciliationAutomaticById(id As Integer, audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic) Implements IBankReconciliationAutomaticAdminService.GetBankReconciliationAutomaticById
        Try
            Dim BankReconciliationAutomatic As BankReconciliationAutomatic = Me._BankReconciliationAutomaticRepository.GetBankReconciliationAutomaticById(id)
            If BankReconciliationAutomatic IsNot Nothing AndAlso BankReconciliationAutomatic.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BankReconciliationAutomatic)(BankReconciliationAutomatic, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = True, .ObjectEmbbeded = BankReconciliationAutomatic}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetBankReconciliationAutomaticByCode(code As String, audit As AuditMessage) As ActionResult(Of BankReconciliationAutomatic) Implements IBankReconciliationAutomaticAdminService.GetBankReconciliationAutomaticByCode
        Try
            Dim BankReconciliationAutomatic As BankReconciliationAutomatic = Me._BankReconciliationAutomaticRepository.GetBankReconciliationAutomaticByCode(code)
            If BankReconciliationAutomatic IsNot Nothing AndAlso BankReconciliationAutomatic.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BankReconciliationAutomatic)(BankReconciliationAutomatic, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = True, .ObjectEmbbeded = BankReconciliationAutomatic}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Async Function SaveBankReconciliationAutomaticAsync(bankReconciliationAutomatic As BankReconciliationAutomatic, audit As AuditMessage, Optional idSequense As Long = 0) As Task(Of ActionResult(Of BankReconciliationAutomatic)) Implements IBankReconciliationAutomaticAdminService.SaveBankReconciliationAutomaticAsync
        Dim unitOfWork As IUnitWork = Me._BankReconciliationAutomaticRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseDRepository.UnitWork
        Try
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled)
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(bankReconciliationAutomatic.Code) Then
                    Dim seq As TreasurySequenceDetail = Me._sequenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.TreasurySequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            bankReconciliationAutomatic.Code = res
                            seq.Next += 1
                            Me._sequenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of BankReconciliationAutomatic) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.TreasurySequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), bankReconciliationAutomatic.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of BankReconciliationAutomatic) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As BankReconciliationAutomatic = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of BankReconciliationAutomatic)
                Dim status As Integer

                If bankReconciliationAutomatic.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    bankReconciliationAutomatic.CreationUser = audit.CodeUser
                    bankReconciliationAutomatic.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _BankReconciliationAutomaticRepository.GetBankReconciliationAutomaticByCode(bankReconciliationAutomatic.Code)
                    UpdateBankReconciliationDetails(bankReconciliationAutomatic.BankReconciliationAutomaticDetail.ToList(), auxObjEntity.BankReconciliationAutomaticDetail.ToList())
                    bankReconciliationAutomatic.ModificationUser = audit.CodeUser
                    bankReconciliationAutomatic.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._BankReconciliationAutomaticRepository.SaveEntity(bankReconciliationAutomatic)
                Await unitOfWork.CommitAsync()

                Dim SaveAssociationList As List(Of BankReconciliationAutomaticAssociation) = bankReconciliationAutomatic.Association
                Dim unitWorkSaveAsociation As IUnitWork = _AutomaticAssociationRepository.UnitWork

                Try
                    Dim txSettingsTwo As New TransactionOptions()
                    txSettingsTwo.Timeout = TransactionManager.MaximumTimeout
                    txSettingsTwo.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                    'inicio la transaccion
                    Using scopeTwo As New TransactionScope(TransactionScopeOption.Required, txSettings, TransactionScopeAsyncFlowOption.Enabled)
                        For Each association In SaveAssociationList

                            ' PASO 1: Sincronizar propiedades FK desde propiedades de navegación
                            ' Forzar sincronización desde propiedades de navegación si están disponibles
                            ' Esto es especialmente importante para documentos nuevos que fueron guardados y ahora tienen Id > 0
                            If association.BankReconciliationAutomaticDetail IsNot Nothing Then
                                ' Buscar el detalle en la colección guardada para obtener el Id actualizado
                                Dim detailInCollection = bankReconciliationAutomatic.BankReconciliationAutomaticDetail.
                                    FirstOrDefault(Function(d) Object.ReferenceEquals(d, association.BankReconciliationAutomaticDetail))
                                If detailInCollection IsNot Nothing AndAlso detailInCollection.Id > 0 Then
                                    association.BankReconciliationAutomaticDetailId = detailInCollection.Id
                                    ' Asegurar que la referencia apunta al objeto actualizado
                                    association.BankReconciliationAutomaticDetail = detailInCollection
                                ElseIf association.BankReconciliationAutomaticDetail.Id > 0 Then
                                    association.BankReconciliationAutomaticDetailId = association.BankReconciliationAutomaticDetail.Id
                                End If
                            End If

                            If association.BankReconciliationAutomaticExtractDetail IsNot Nothing Then
                                ' Buscar el extracto en la colección guardada para obtener el Id actualizado
                                Dim extractInCollection = bankReconciliationAutomatic.BankReconciliationAutomaticExtractDetail.
                                    FirstOrDefault(Function(e) Object.ReferenceEquals(e, association.BankReconciliationAutomaticExtractDetail))
                                If extractInCollection IsNot Nothing AndAlso extractInCollection.Id > 0 Then
                                    association.BankReconciliationAutomaticExtractId = extractInCollection.Id
                                    ' Asegurar que la referencia apunta al objeto actualizado
                                    association.BankReconciliationAutomaticExtractDetail = extractInCollection
                                ElseIf association.BankReconciliationAutomaticExtractDetail.Id > 0 Then
                                    association.BankReconciliationAutomaticExtractId = association.BankReconciliationAutomaticExtractDetail.Id
                                End If
                            End If

                            ' PASO 2: Determinar si la asociación existe en la BD
                            Dim existingAssociationId As Integer = GetBankAssociation(
                                association.BankReconciliationAutomaticDetailId,
                                association.BankReconciliationAutomaticExtractId)
                            Dim isNewAssociation As Boolean = (existingAssociationId = 0)

                            ' PASO 3: Validar detalles usando métodos auxiliares
                            Dim detailValid As Boolean = IsAssociationDetailValid(
                                                            association.BankReconciliationAutomaticDetailId,
                                                            association.BankReconciliationAutomaticDetail,
                                                            bankReconciliationAutomatic.BankReconciliationAutomaticDetail,
                                                            Function(d) d.Id,
                                                            Function(d) d.Reconciled)

                            Dim extractValid As Boolean = IsAssociationDetailValid(
                                                            association.BankReconciliationAutomaticExtractId,
                                                            association.BankReconciliationAutomaticExtractDetail,
                                                            bankReconciliationAutomatic.BankReconciliationAutomaticExtractDetail,
                                                            Function(ed) ed.Id,
                                                            Function(ed) ed.Reconciled)


                            ' PASO 4: Aplicar lógica según sea nueva o existente
                            If isNewAssociation Then
                                ' ASOCIACIÓN NUEVA: Solo agregar si ambos detalles son válidos y están reconciliados
                                If detailValid AndAlso extractValid Then
                                    association.MarkAsAdded()
                                End If
                                ' Si no es válida, no se agrega (no hacer nada, no se marca para eliminación)
                            Else
                                ' ASOCIACIÓN EXISTENTE: Actualizar o eliminar según validez de detalles
                                association.Id = existingAssociationId
                                If detailValid AndAlso extractValid Then
                                    ' Ambos detalles existen y están reconciliados: mantener o actualizar
                                    association.MarkAsModified()
                                Else
                                    ' Algún detalle no existe o se desconcilió: eliminar asociación
                                    association.MarkAsDeleted()
                                End If
                            End If
                        Next

                        ' PASO 5: Filtrar solo las asociaciones con estado definido
                        Dim associationsToSave = SaveAssociationList.Where(
                            Function(a) a.ChangeTracker.State = ObjectState.Added OrElse
                                        a.ChangeTracker.State = ObjectState.Modified OrElse
                                        a.ChangeTracker.State = ObjectState.Deleted).ToList()

                        ' Solo guardar si hay asociaciones con cambios
                        If associationsToSave.Any() Then
                            Await _AutomaticAssociationRepository.SaveEntityMassiveAsync(associationsToSave)
                        End If

                        Await unitWorkSaveAsociation.CommitAsync()
                        'confirmo la transaccion
                        scopeTwo.Complete()
                    End Using
                Catch ex As OptimisticConcurrencyException
                    unitWorkSaveAsociation.RollbackChanges()
                    Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
                Catch ex As Exception
                    unitWorkSaveAsociation.RollbackChanges()
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
                End Try
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of BankReconciliationAutomatic)(bankReconciliationAutomatic, audit, status, auxObjEntity)
                auditProcess.Execute()
                'Se marca la entidad como sin cambios
                bankReconciliationAutomatic.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = bankReconciliationAutomatic, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankReconciliationAutomatic) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza o elimina los detalles que ya no pertenecen a la Conciliación Bancaria Automática
    ''' </summary>
    ''' <param name="newDetails"></param>
    ''' <param name="oldDetails"></param>
    Private Sub UpdateBankReconciliationDetails(newDetails As List(Of BankReconciliationAutomaticDetail), oldDetails As List(Of BankReconciliationAutomaticDetail))
        Dim newIds = New HashSet(Of Integer)(newDetails.Select(Function(d) d.Id))

        ' 1. Eliminar detalles antiguos que no están en los nuevos
        For Each oldDetail In oldDetails.Where(Function(d) Not newIds.Contains(d.Id)).ToList()
            oldDetail.MarkAsDeleted()
        Next

        ' 2. Actualizar detalles existentes
        For Each newDetail In newDetails.Where(Function(d) d.Id <> 0)
            Dim existingDetail = oldDetails.FirstOrDefault(Function(d) d.Id = newDetail.Id)
            If existingDetail IsNot Nothing Then
                existingDetail.MarkAsModified()
            End If
        Next

        ' 3. Añadir nuevos detalles
        For Each newDetail In newDetails.Where(Function(d) d.Id = 0)
            newDetail.MarkAsAdded()
        Next
    End Sub

    ''' <summary>
    ''' Valida si un detalle existe en la colección y está reconciliado
    ''' </summary>
    ''' <typeparam name="T">El tipo de detalle a validar</typeparam>
    ''' <param name="detailId">El ID del detalle a buscar</param>
    ''' <param name="detail">La referencia al objeto detalle (propiedad de navegación)</param>
    ''' <param name="collection">La colección donde buscar el detalle</param>
    ''' <param name="getId">Función para obtener el ID de un detalle</param>
    ''' <param name="isReconciled">Función para verificar si un detalle está reconciliado</param>
    ''' <returns>True si el detalle existe y está reconciliado, False en caso contrario</returns>
    Private Function IsAssociationDetailValid(Of T As Class)(
        detailId As Integer,
        detail As T,
        collection As IEnumerable(Of T),
        getId As Func(Of T, Integer),
        isReconciled As Func(Of T, Boolean)) As Boolean

        If collection Is Nothing Then
            Return False
        End If

        ' Buscar por Id si está disponible
        If detailId > 0 Then
            Dim byId = collection.FirstOrDefault(Function(d) getId(d) = detailId)
            If byId IsNot Nothing Then
                Return isReconciled(byId)
            End If
        End If

        ' Buscar por referencia de objeto si la propiedad de navegación está disponible
        If detail IsNot Nothing Then
            Dim byReference = collection.FirstOrDefault(Function(d) Object.ReferenceEquals(d, detail))
            If byReference IsNot Nothing Then
                Return isReconciled(byReference)
            End If
        End If

        Return False
    End Function


    ''' <summary>
    ''' Obtiene los detalles de las Notas relacionadas a la Cuenta Bancaria
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GetBankReconciliationAutomaticDetails(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticDetail)) Implements IBankReconciliationAutomaticAdminService.GetBankReconciliationAutomaticDetails
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim result = Me._BankReconciliationAutomaticRepository.SP_GetBankReconciliationAutomaticDetails(xmlCriterias)
            Dim listBankReconciliationAutomaticDetail As New List(Of BankReconciliationAutomaticDetail)

            ' Obtener todos los IDs tipo Nota de una vez
            Dim noteTypeIds = result.Where(Function(d) d.DocumentType = eDocumentTypeReconciliation.TreasuryNote).Select(Function(d) d.EntityId).ToList()
            Dim allCashReceipts As Dictionary(Of Integer, List(Of CashReceipts)) = Nothing

            If noteTypeIds.Any() Then
                allCashReceipts = Me._BankReconciliationAutomaticRepository.GetListCashReceiptsBulk(noteTypeIds)
            End If

            For Each detail In result
                listBankReconciliationAutomaticDetail.Add(New BankReconciliationAutomaticDetail With
            {
                .Id = detail.Id,
                .DocumentType = detail.DocumentType,
                .Nature = detail.Nature,
                .Value = detail.Value,
                .EntityId = detail.EntityId,
                .ListCashReceipts = If(detail.DocumentType = eDocumentTypeReconciliation.TreasuryNote AndAlso allCashReceipts IsNot Nothing AndAlso allCashReceipts.ContainsKey(detail.EntityId),
                    allCashReceipts(detail.EntityId),
                    Nothing),
                .EntityCode = detail.EntityCode,
                .EntityName = detail.EntityName,
                .Reconciled = detail.Reconciled,
                .Comments = detail.Comments,
                .DocumentDate = detail.DocumentDate,
                .ThirdPartyNitName = detail.ThirdPartyNitName,
                .NitThirdParty = detail.NitThirdParty,
                .DocumentNumber = detail.DocumentNumber,
                .Observations = detail.Observations,
                .ReconciledStatus = detail.ReconciledStatus,
                .CreationUser = detail.CreationUser,
                .ConfirmationUser = detail.ConfirmationUser
            })
            Next

            Return New ActionResult(Of List(Of BankReconciliationAutomaticDetail)) With {.StateResult = True, .ObjectEmbbeded = listBankReconciliationAutomaticDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of BankReconciliationAutomaticDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function


    ''' <summary>
    ''' Obtiene la asociación de la Conciliación en la tabla pivote
    ''' </summary>
    ''' <param name="DetailId"></param>
    ''' <param name="DetailExtractId"></param>
    ''' <returns></returns>
    Public Function GetBankAssociation(DetailId As Integer, DetailExtractId As Integer)
        Try
            Dim BankAssociation As BankReconciliationAutomaticAssociation = _BankReconciliationAutomaticRepository.GetBankAssociation(DetailId, DetailExtractId)
            If BankAssociation.Id > 0 Then
                Return BankAssociation.Id
            Else
                Return 0
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankReconciliationAutomaticAssociation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los detalles del extracto de la cuenta bancaria relacionada
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GetUploadBankStatementsDetailByUploadBankStatementsId(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) Implements IBankReconciliationAutomaticAdminService.GetUploadBankStatementsDetailByUploadBankStatementsId
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)
            Dim result = Me._BankReconciliationAutomaticRepository.SP_GetBankReconciliationAutomaticExtractDetails(xmlCriterias)
            Dim listBankReconciliationAutomaticExtractDetail = result.Select(Function(detail) _
            New BankReconciliationAutomaticExtractDetail With {
                .Id = IIf(detail.BankReconciliationAutomaticExtractDetailId Is Nothing, 0, detail.BankReconciliationAutomaticExtractDetailId),
                .BankReconciliationAutomaticExtractDetailId = detail.BankReconciliationAutomaticExtractDetailId,
                .UploadBankStatementsDetailId = detail.UploadBankStatementsDetailId,
                .DocumentDate = detail.TransactionDate,
                .ConsecutiveBank = detail.ConsecutiveBank,
                .TransactionCode = detail.TransactionCode,
                .DescriptionTransaction = detail.DescriptionTransaction,
                .DocumentType = detail.DocumentType,
                .BankCheck = detail.BankCheck,
                .PaymentReferenceOne = detail.PaymentReferenceOne,
                .PaymentReferenceTwo = detail.PaymentReferenceTwo,
                .Nature = detail.Nature,
                .Reconciled = detail.Reconciled,
                .CodeNoteReconciled = detail.CodeNoteReconciled,
                .Value = detail.Value
            }).ToList()
            Return New ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) With {.StateResult = True, .ObjectEmbbeded = listBankReconciliationAutomaticExtractDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Funcion que trae las reglas de consignacion bancaria 
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function GetBankAutomaticRecognitionRules(entityId As Integer) As ActionResult(Of List(Of BankAutomaticRecognitionRules)) Implements IBankReconciliationAutomaticAdminService.GetBankAutomaticRecognitionRules
        Try
            Dim BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules) = _BankReconciliationAutomaticRepository.BankAutomaticRecognitionRules(entityId)
            If BankAutomaticRecognitionRules.Any() Then
                Return New ActionResult(Of List(Of BankAutomaticRecognitionRules)) With {.StateResult = True, .ObjectEmbbeded = BankAutomaticRecognitionRules}
            Else
                Return New ActionResult(Of List(Of BankAutomaticRecognitionRules)) With {
                .StateResult = False,
                .Message = "No se encuentran Reglas de reconocimiento Automático",
                .ObjectEmbbeded = New List(Of BankAutomaticRecognitionRules)()}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of BankAutomaticRecognitionRules)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Crea el objeto TreasuryNote
    ''' </summary>
    ''' <param name="BankAutomaticRecognitionRules"></param>
    ''' <param name="BankReconciliationAutomatic"></param>
    ''' <param name="BankReconciliationAutomaticExtractDetail"></param>
    ''' <returns></returns>
    Public Function MakeObjectNoteBank(BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules), BankReconciliationAutomatic As BankReconciliationAutomatic, BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail), Optional idOperativeUnit As Integer = 0) As ActionResult(Of TreasuryNote) Implements IBankReconciliationAutomaticAdminService.MakeObjectNoteBank
        Try
            'Creacion cabecera
            Dim treasuryNote As New TreasuryNote
            Dim detail As New List(Of TreasuryNoteDetail)
            Dim entityBankAccount = _EntityBankAccount.GetEntityBankAccountById(BankReconciliationAutomatic.EntityBankAccountId)

            Dim description As String
            If BankAutomaticRecognitionRules IsNot Nothing Then
                description = "Gastos bancarios"
                detail = MakeObjectDetailNoteBank(BankAutomaticRecognitionRules, BankReconciliationAutomaticExtractDetail)
            Else
                description = "Nota conciliación terceros pendientes por identificar"
                detail = MakeObjectDetailNoteBank(Nothing, BankReconciliationAutomaticExtractDetail, entityBankAccount)
            End If

            If Not detail.Any() Then
                Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = "Noy hay detalles para la nota"}
            End If
            Dim CreditValue, DebitValue As Double

            With treasuryNote
                .Code = String.Empty
                .NoteType = 1
                .EntityBankAccountId = BankReconciliationAutomatic.EntityBankAccountId
                .NoteDate = BankReconciliationAutomatic.DocumentDate
                .Status = 1
                .Description = $"{description} Cuenta bancaria {entityBankAccount.Bank.Name} {entityBankAccount.Number} mes de " &
                           $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(BankReconciliationAutomatic.DocumentDate.Month)} " &
                           $"de {BankReconciliationAutomatic.DocumentDate.Year}"
                .MainAccountId = entityBankAccount.IdMainAccount
                .OperatingUnitId = idOperativeUnit
                .CurrencyId = entityBankAccount.CurrencyId
                For Each item In detail
                    If item.Nature = eNature.Debit Then
                        DebitValue += item.Value
                    Else item.Nature = eNature.Credit
                        CreditValue += item.Value
                    End If
                    treasuryNote.TreasuryNoteDetail.Add(item)
                Next
                .Nature = If(CreditValue > DebitValue, eNature.Debit, eNature.Credit)
                .Value = Math.Abs(CreditValue - DebitValue)
            End With

            Return New ActionResult(Of TreasuryNote) With {.StateResult = True, .ObjectEmbbeded = treasuryNote}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of TreasuryNote) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para crear el detalle de la nota
    ''' </summary>
    ''' <param name="BankAutomaticRecognitionRules"></param>
    ''' <param name="BankReconciliationAutomaticExtractDetail"></param>
    ''' <returns></returns>
    Public Function MakeObjectDetailNoteBank(BankAutomaticRecognitionRules As List(Of BankAutomaticRecognitionRules),
                                          BankReconciliationAutomaticExtractDetail As List(Of BankReconciliationAutomaticExtractDetail),
                                          Optional entityBankAccount As EntityBankAccounts = Nothing) As List(Of TreasuryNoteDetail) _
    Implements IBankReconciliationAutomaticAdminService.MakeObjectDetailNoteBank

        Try
            If BankAutomaticRecognitionRules IsNot Nothing Then ' Nota Gastos Bancarios
                Dim UploadBankStatementsList = BankReconciliationAutomaticExtractDetail.
                                       Select(Function(x) x.UploadBankStatementsDetailId).
                                       Distinct().
                                       ToList()

                Dim UploadBankStatementsDetailList As List(Of UploadBankStatementsDetail) =
            _BankReconciliationAutomaticRepository.GetUploadBankStatementsDescriptionList(UploadBankStatementsList)

                Dim bankId = BankAutomaticRecognitionRules.Select(Function(x) x.BankId).FirstOrDefault()
                Dim BankD = _BankRepository.GetBankById(bankId)
                Dim BankDList As New List(Of Bank) From {BankD}

                Dim noteConcept = BankAutomaticRecognitionRules.
                          Select(Function(x) x.NoteConceptsId).
                          ToList()

                Dim noteConceptlist = _BankReconciliationAutomaticRepository.getNoteconcepts(noteConcept)


                Dim baseList = (From a In BankReconciliationAutomaticExtractDetail
                                Join b In UploadBankStatementsDetailList On b.Id Equals a.UploadBankStatementsDetailId
                                Join c In BankAutomaticRecognitionRules On b.DescriptionTransaction.ToLower() Equals c.DescriptionTransaction.ToLower()
                                Join d In BankDList On c.BankId Equals d.Id
                                Join e In noteConceptlist On e.Id Equals c.NoteConceptsId
                                Select New TreasuryNoteDetail With {
                            .NoteConceptId = c.NoteConceptsId,
                            .MainAccountId = c.MainAccountsId,
                            .CostCenterId = c.CostCenterId,
                            .ThirdPartyId = d.ThirdPartyId,
                            .Nature = e.Nature,
                            .Value = a.Value,
                            .FullNameMainAccount = "",
                            .FullNameNature = If(a.Nature = eNature.Debit, "Débito", "Crédito"),
                            .FullNameThird = If(String.IsNullOrEmpty(d.Name), "", d.Name),
                            .NoteConceptCode = If(String.IsNullOrEmpty(e.Code), "", e.Code),
                            .NoteConceptName = If(String.IsNullOrEmpty(e.Description), "", e.Description),
                            .FullNameCostCenter = "",
                            .IdCashFlowConcept = e.IdCashFlowConcept
                        }).ToList()


                Dim conceptList = baseList.
            GroupBy(Function(x) x.NoteConceptId).
            Select(Function(grp)
                       Dim firstItem = grp.First()
                       Return New TreasuryNoteDetail With {
                           .NoteConceptId = firstItem.NoteConceptId,
                           .MainAccountId = firstItem.MainAccountId,
                           .CostCenterId = firstItem.CostCenterId,
                           .ThirdPartyId = firstItem.ThirdPartyId,
                           .Nature = firstItem.Nature,
                           .Value = grp.Sum(Function(x) x.Value), ' se suma el total de los valores
                           .FullNameMainAccount = firstItem.FullNameMainAccount,
                           .FullNameNature = firstItem.FullNameNature,
                           .FullNameThird = firstItem.FullNameThird,
                           .NoteConceptCode = firstItem.NoteConceptCode,
                           .NoteConceptName = firstItem.NoteConceptName,
                           .FullNameCostCenter = firstItem.FullNameCostCenter,
                           .IdCashFlowConcept = firstItem.IdCashFlowConcept
                       }
                   End Function).ToList()

                Return conceptList

            Else 'Nota de Tesoería Terceros por Identificar

                Dim listTreasuryNoteDetail As New List(Of TreasuryNoteDetail)
                Dim bankRecognitionRule As BankAutomaticRecognitionRules

                Dim res = GetBankAutomaticRecognitionRules(entityBankAccount.Id)
                If res IsNot Nothing Then
                    bankRecognitionRule = res.ObjectEmbbeded.Where(Function(b) b.TypeOfItemPendingInReconciliation = 2).FirstOrDefault
                    If bankRecognitionRule Is Nothing Then
                        Return New List(Of TreasuryNoteDetail)
                    End If
                Else
                    Return New List(Of TreasuryNoteDetail)
                End If
                'Obtenemos detalles del extracto checkeados para crear la Nota que sean de naturaleza Crédito
                Dim extractDetailChecked = BankReconciliationAutomaticExtractDetail.Where(Function(x) x.Checked).ToList()

                ' Se crean los detalles de la nota por cada Detalle del Extracto Checkeado
                For Each extractDetail As BankReconciliationAutomaticExtractDetail In extractDetailChecked
                    Dim treasuryNoteDetail As New TreasuryNoteDetail
                    With treasuryNoteDetail
                        .NoteConceptId = bankRecognitionRule.NoteConceptsId
                        .MainAccountId = bankRecognitionRule.MainAccountsId
                        .ThirdPartyId = entityBankAccount.Bank.ThirdPartyId
                        .CostCenterId = bankRecognitionRule.CostCenterId
                        .IdCashFlowConcept = bankRecognitionRule.NoteConcepts.IdCashFlowConcept
                        If extractDetail.Nature = eNature.Debit Then 'Los detalles de la Nota deben quedar con la Naturaleza contraria
                            .Nature = eNature.Credit
                        Else
                            .Nature = eNature.Debit
                        End If
                        .Value = extractDetail.Value
                    End With
                    listTreasuryNoteDetail.Add(treasuryNoteDetail)
                Next
                Return listTreasuryNoteDetail
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of TreasuryNoteDetail)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la secuencia de notas
    ''' </summary>
    ''' <param name="idForm"></param>
    ''' <returns></returns>
    Public Function GetNoteSecuence(idForm As String, Optional _idOperativeUnit As Integer? = Nothing) As ActionResult(Of Integer?) Implements IBankReconciliationAutomaticAdminService.GetNoteSecuence
        If idForm Is Nothing OrElse idForm.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("idForm")
        End If
        Try
            Dim TreasurySequence = _sequenseCRepository.GetSequenseByIdForm(idForm)
            Dim _idCurrentSequence As Integer = 0

            If TreasurySequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _idCurrentSequence = TreasurySequence.TreasurySequenceDetail(0).Id
            ElseIf TreasurySequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If TreasurySequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = _idOperativeUnit) Then
                    _idCurrentSequence = TreasurySequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = _idOperativeUnit).Id
                Else
                    Throw New Exception("No se encontro secuencia numerica para la unidad operativa")
                End If
            Else
                Throw New Exception("No se encontro secuencia numerica ")
            End If

            Return New ActionResult(Of Integer?) With {.StateResult = True, .ObjectEmbbeded = _idCurrentSequence}
        Catch ex As Exception
            Return New ActionResult(Of Integer?) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Método que arma las partidas pendientes por conciliar
    ''' </summary>
    ''' <param name="bankAutomaticDetail"></param>
    ''' <param name="bankExtractDetail"></param>
    Private Function CreatePendingItemsToReconciled(bankAutomaticDetail As List(Of BankReconciliationAutomaticDetail), bankExtractDetail As List(Of BankReconciliationAutomaticExtractDetail)) As List(Of PendingItemsToReconciled) Implements IBankReconciliationAutomaticAdminService.CreatePendingItemsToReconciled
        Dim listPendingItemsToReconciled As New List(Of PendingItemsToReconciled)
        If bankAutomaticDetail IsNot Nothing Then
            'Agregamos los Documentos pendientes por conciliar de Libro de Bancos
            For Each document In bankAutomaticDetail.Where(Function(x) Not x.Reconciled).ToList()
                Dim newItemPendingToReconciled1 As New PendingItemsToReconciled
                With newItemPendingToReconciled1
                    .Origin = 1
                    .DocumentDate = document.DocumentDate
                    .DocumentType = document.DocumentType
                    .Nature = document.Nature
                    .Value = document.Value
                    .ReconciledStatus = If(document.ReconciledStatus, 1)
                    .DocumentDetail = document
                End With
                listPendingItemsToReconciled.Add(newItemPendingToReconciled1)
            Next
        End If
        If bankExtractDetail IsNot Nothing Then
            'Agregamos los detalles del Extracto pendientes por conciliar
            For Each extractDetail In bankExtractDetail
                Dim newItemPendingToReconciled2 As New PendingItemsToReconciled
                With newItemPendingToReconciled2
                    .Origin = 2
                    .DocumentDate = extractDetail.DocumentDate
                    .DocumentType = extractDetail.DocumentType
                    .Nature = extractDetail.Nature
                    .Value = extractDetail.Value
                    .ReconciledStatus = 1
                    .ExtractDetail = extractDetail
                End With
                listPendingItemsToReconciled.Add(newItemPendingToReconciled2)
            Next
        End If
        Return listPendingItemsToReconciled
    End Function

    ''' <summary>
    ''' Divide una lista de PendingItemsToReconciled en dos listas separadas según su origen
    ''' </summary>
    ''' <param name="pendingItems"></param>
    ''' <returns>Tupla con lista de BankReconciliationAutomaticDetail y lista de BankReconciliationAutomaticExtractDetail</returns>
    Private Function SplitPendingItemsToReconciled(pendingItems As List(Of PendingItemsToReconciled)) As Tuple(Of List(Of BankReconciliationAutomaticDetail), List(Of BankReconciliationAutomaticExtractDetail)) Implements IBankReconciliationAutomaticAdminService.SplitPendingItemsToReconciled
        Dim documentDetailList As New List(Of BankReconciliationAutomaticDetail)
        Dim extractDetailList As New List(Of BankReconciliationAutomaticExtractDetail)

        If pendingItems IsNot Nothing Then
            For Each item In pendingItems
                If item.Origin = 1 AndAlso item.DocumentDetail IsNot Nothing Then
                    ' Libro de Bancos
                    documentDetailList.Add(item.DocumentDetail)
                ElseIf item.Origin = 2 AndAlso item.ExtractDetail IsNot Nothing Then
                    ' Extracto Bancario
                    extractDetailList.Add(item.ExtractDetail)
                End If
            Next
        End If

        Return Tuple.Create(documentDetailList, extractDetailList)
    End Function

    ''' <summary>
    ''' Encuentra conjunto de sumas de extratos bancarios que pueden conciliar un valor de Documentos de Tesorería
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    Public Function FindSubsetsWithinToleranceA(items As List(Of BankReconciliationAutomaticExtractDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticExtractDetail) Implements IBankReconciliationAutomaticAdminService.FindSubsetsWithinToleranceA
        Dim queue As New Queue(Of Tuple(Of List(Of BankReconciliationAutomaticExtractDetail), Decimal, Integer))()

        For i = 0 To items.Count - 1
            Dim list = New List(Of BankReconciliationAutomaticExtractDetail) From {items(i)}
            Dim sum = items(i).Value
            queue.Enqueue(Tuple.Create(list, sum, i))
        Next

        While queue.Count > 0
            Dim current = queue.Dequeue()
            Dim subset = current.Item1
            Dim sum = current.Item2
            Dim lastIndex = current.Item3

            If sum >= minTarget AndAlso sum <= maxTarget Then
                Return subset
            End If

            If sum > maxTarget Then
                Continue While
            End If

            For j = lastIndex + 1 To items.Count - 1
                Dim newSubset = New List(Of BankReconciliationAutomaticExtractDetail)(subset)
                newSubset.Add(items(j))
                queue.Enqueue(Tuple.Create(newSubset, sum + items(j).Value, j))
            Next
        End While

        Return New List(Of BankReconciliationAutomaticExtractDetail)()
    End Function

    ''' <summary>
    ''' Encuentra conjunto de sumas de Documentos de Tesorería que pueden conciliar un valor de Extracto Bancario
    ''' </summary>
    ''' <param name="items"></param>
    ''' <param name="minTarget"></param>
    ''' <param name="maxTarget"></param>
    ''' <returns></returns>
    Public Function FindSubsetsWithinToleranceB(items As List(Of BankReconciliationAutomaticDetail), minTarget As Decimal, maxTarget As Decimal) As List(Of BankReconciliationAutomaticDetail) Implements IBankReconciliationAutomaticAdminService.FindSubsetsWithinToleranceB
        Dim queue As New Queue(Of Tuple(Of List(Of BankReconciliationAutomaticDetail), Decimal, Integer))()

        For i = 0 To items.Count - 1
            Dim list = New List(Of BankReconciliationAutomaticDetail) From {items(i)}
            Dim sum = items(i).Value
            queue.Enqueue(Tuple.Create(list, sum, i))
        Next

        While queue.Count > 0
            Dim current = queue.Dequeue()
            Dim subset = current.Item1
            Dim sum = current.Item2
            Dim lastIndex = current.Item3

            If sum >= minTarget AndAlso sum <= maxTarget Then
                Return subset
            End If

            If sum > maxTarget Then
                Continue While
            End If

            For j = lastIndex + 1 To items.Count - 1
                Dim newSubset = New List(Of BankReconciliationAutomaticDetail)(subset)
                newSubset.Add(items(j))
                queue.Enqueue(Tuple.Create(newSubset, sum + items(j).Value, j))
            Next
        End While

        Return New List(Of BankReconciliationAutomaticDetail)()
    End Function

    ''' <summary>
    ''' Obtiene las partidas pendientes por conciliar para el segmento libro de bancos
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Function GetPendingItemsBankBook(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticDetail)) Implements IBankReconciliationAutomaticAdminService.GetPendingItemsBankBook
        Try
            Dim result = _BankReconciliationAutomaticRepository.GetPendingItemsBankBook(entityBankAccountId, documentDate)
            If result IsNot Nothing Then
                Return New ActionResult(Of List(Of BankReconciliationAutomaticDetail)) With {.StateResult = True, .ObjectEmbbeded = result}
            Else
                Return New ActionResult(Of List(Of BankReconciliationAutomaticDetail)) With {.StateResult = True, .ObjectEmbbeded = New List(Of BankReconciliationAutomaticDetail)}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of BankReconciliationAutomaticDetail)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las partidas pendientes por conciliar para el segmento Extracto bancario
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Function GetPendingItemsExtract(entityBankAccountId As Integer, documentDate As Date) As ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) Implements IBankReconciliationAutomaticAdminService.GetPendingItemsExtract
        Try
            Dim result = _BankReconciliationAutomaticRepository.GetPendingItemsExtract(entityBankAccountId, documentDate)
            If result IsNot Nothing Then
                Return New ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) With {.StateResult = True, .ObjectEmbbeded = result}
            Else
                Return New ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) With {.StateResult = True, .ObjectEmbbeded = New List(Of BankReconciliationAutomaticExtractDetail)}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Función auxiliar que calcula el valor y naturaleza real de un documento considerando ListCashReceipts
    ''' </summary>
    ''' <param name="detail"></param>
    ''' <returns>Tupla con (valor calculado, naturaleza resultante)</returns>
    Public Function CalculateRealValueAndNature(detail As BankReconciliationAutomaticDetail) As Tuple(Of Decimal, Byte) Implements IBankReconciliationAutomaticAdminService.CalculateRealValueAndNature
        ' Validar si se debe calcular con CashReceipts
        If Not ShouldCalculateCashReceipts(detail) Then
            Return Tuple.Create(detail.Value, detail.Nature)
        End If

        ' Calcular el total de CashReceipts
        Dim cashReceiptsTotal As Decimal = CalculateCashReceiptsTotal(detail.ListCashReceipts)

        ' Calcular valor y naturaleza según la naturaleza original
        Return CalculateByNature(detail.Value, detail.Nature, cashReceiptsTotal)
    End Function

    ''' <summary>
    ''' Determina si se debe calcular con CashReceipts
    ''' </summary>
    Private Function ShouldCalculateCashReceipts(detail As BankReconciliationAutomaticDetail) As Boolean
        Return detail.DocumentType = eDocumentTypeReconciliation.TreasuryNote AndAlso
           detail.ListCashReceipts IsNot Nothing AndAlso
           detail.ListCashReceipts.Any()
    End Function

    ''' <summary>
    ''' Calcula el total de los valores de CashReceipts
    ''' </summary>
    Private Function CalculateCashReceiptsTotal(cashReceipts As List(Of CashReceipts)) As Decimal
        Return cashReceipts.Sum(Function(cr) cr.Value)
    End Function

    ''' <summary>
    ''' Calcula el valor y naturaleza resultante según la naturaleza original
    ''' </summary>
    Private Function CalculateByNature(originalValue As Decimal, originalNature As Byte, cashReceiptsTotal As Decimal) As Tuple(Of Decimal, Byte)
        If originalNature = eNature.Debit Then
            Return CalculateForDebitNature(originalValue, cashReceiptsTotal)
        Else
            Return CalculateForCreditNature(originalValue, cashReceiptsTotal)
        End If
    End Function

    ''' <summary>
    ''' Calcula valores cuando la naturaleza original es débito
    ''' </summary>
    Private Function CalculateForDebitNature(originalValue As Decimal, cashReceiptsTotal As Decimal) As Tuple(Of Decimal, Byte)
        Dim resultValue As Decimal = originalValue + cashReceiptsTotal
        Return Tuple.Create(resultValue, CByte(eNature.Debit))
    End Function

    ''' <summary>
    ''' Calcula valores cuando la naturaleza original es crédito
    ''' </summary>
    Private Function CalculateForCreditNature(originalValue As Decimal, cashReceiptsTotal As Decimal) As Tuple(Of Decimal, Byte)
        Dim resultValue As Decimal = originalValue - cashReceiptsTotal

        ' Si el resultado es negativo, invertir signo y cambiar naturaleza a débito
        If resultValue < 0 Then
            Return Tuple.Create(Math.Abs(resultValue), CByte(eNature.Debit))
        Else
            Return Tuple.Create(resultValue, CByte(eNature.Credit))
        End If
    End Function

    ''' <summary>
    ''' Función que valida las diferencias que existen en los items a Conciliar
    ''' </summary>
    ''' <param name="documentDetails"></param>
    ''' <param name="extractDetails"></param>
    ''' <returns></returns>
    Public Function ValidateManualReconciliation(documentDetails As List(Of BankReconciliationAutomaticDetail), extractDetails As List(Of BankReconciliationAutomaticExtractDetail)) As ActionResult Implements IBankReconciliationAutomaticAdminService.ValidateManualReconciliation
        Dim invalidCriteria As New List(Of String)

        ' Validación Tipo de Documento 
        Dim documentTypes As New List(Of Integer)
        For Each d In documentDetails
            documentTypes.Add(CInt(d.DocumentType))
            ' Si es tipo Nota y tiene ListCashReceipts, agregar también tipo Recibo de Caja
            If d.DocumentType = eDocumentTypeReconciliation.TreasuryNote AndAlso d.ListCashReceipts IsNot Nothing AndAlso d.ListCashReceipts.Any() Then
                documentTypes.Add(eDocumentTypeReconciliation.CashReceipt)
            End If
        Next
        documentTypes = documentTypes.Distinct().OrderBy(Function(x) x).ToList()

        Dim extractTypes = extractDetails.Select(Function(e) CInt(e.DocumentType)).Distinct.OrderBy(Function(x) x).ToList()
        Dim documentTypeValidation As Boolean = documentTypes.SequenceEqual(extractTypes)
        If Not documentTypeValidation Then invalidCriteria.Add("el Tipo de Documento")

        ' Validación Valor y Naturaleza 
        Dim creditDocument As Decimal = 0
        Dim debitDocument As Decimal = 0

        ' Calcular valores reales considerando ListCashReceipts
        For Each d In documentDetails
            Dim realValues = CalculateRealValueAndNature(d)
            Dim realValue = realValues.Item1
            Dim realNature = realValues.Item2

            If realNature = eNature.Debit Then
                debitDocument += realValue
            Else
                creditDocument += realValue
            End If
        Next

        Dim valueDocument = Math.Abs(creditDocument - debitDocument)
        Dim natureDocument As Byte = If(creditDocument < debitDocument, eNature.Debit, eNature.Credit)

        Dim creditExtract = extractDetails.Where(Function(e) e.Nature = eNature.Credit).Sum(Function(e) e.Value)
        Dim debitExtract = extractDetails.Where(Function(e) e.Nature = eNature.Debit).Sum(Function(e) e.Value)
        Dim valueExtract = Math.Abs(creditExtract - debitExtract)
        Dim natureExtract As Byte = If(creditExtract < debitExtract, eNature.Debit, eNature.Credit)

        ' Validación especial: si el valor del documento es 0 debido a ListCashReceipts
        If valueDocument = 0 Then
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = "Los documentos seleccionados tienen un valor de 0 debido a sus naturalezas"}
        End If

        If natureDocument <> natureExtract Then invalidCriteria.Add("la Naturaleza")

        Dim valueDifference As Decimal = Math.Abs(valueDocument - valueExtract)
        If valueDifference <> 0 Then
            invalidCriteria.Add("el Valor")

            If valueDifference > 150 Then
                Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = "La diferencia en el valor es superior a $150"}
            End If
        End If

        If invalidCriteria.Count = 0 Then
            Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True}
        Else
            Dim messageValidation As String = String.Join(", ", invalidCriteria)
            Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .StateResult = False, .Message = messageValidation}
        End If
    End Function

    ''' <summary>
    ''' Busca coincidencias entre extractos bancarios y documentos de tesorería para conciliación automática
    ''' </summary>
    ''' <param name="extractList">Lista de extractos bancarios</param>
    ''' <param name="documentList">Lista de documentos de tesorería</param>
    ''' <param name="existingAssociations">Lista de asociaciones existentes (opcional)</param>
    ''' <returns>Resultado con las listas actualizadas y las asociaciones (existentes y nuevas)</returns>
    Public Async Function FindCoincidencesAsync(extractList As List(Of BankReconciliationAutomaticExtractDetail), documentList As List(Of BankReconciliationAutomaticDetail), Optional existingAssociations As List(Of BankReconciliationAutomaticAssociation) = Nothing) As Task(Of ActionResult(Of BankReconciliationCoincidencesResult)) Implements IBankReconciliationAutomaticAdminService.FindCoincidencesAsync
        Try
            If extractList Is Nothing Then
                extractList = New List(Of BankReconciliationAutomaticExtractDetail)
            End If
            If documentList Is Nothing Then
                documentList = New List(Of BankReconciliationAutomaticDetail)
            End If

            Dim result = Await FindCoincidencesInternalAsync(extractList, documentList, existingAssociations)
            Return New ActionResult(Of BankReconciliationCoincidencesResult) With {.StateResult = True, .ObjectEmbbeded = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BankReconciliationCoincidencesResult) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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

            _sequenseDRepository = Nothing
            _BankReconciliationAutomaticRepository = Nothing
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
