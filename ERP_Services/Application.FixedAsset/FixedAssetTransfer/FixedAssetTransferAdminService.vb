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

#End Region

Public Class FixedAssetTransferAdminService
    Implements IFixedAssetTransferAdminService

#Region "Variables"

    'Repositorio de la aseguradora
    Private _FixedAssetTransferRepository As IFixedAssetTransferRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    'Repositorio de ingreso de activos
    Private _FixedAssetEntryRepository As IFixedAssetEntryRepository

    'Repositorio para physical
    Private _physicalAssetRepository As IFixedAssetPhysicalAssetRepository

    'Repositorio para el comprobante
    Private _accountingRepository As IAccountingDocumentAdminService

    'Repositorio de proveedor
    Private _supplierRepository As ISupplierRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(FixedAssetTransferRepository As IFixedAssetTransferRepository, sequenceRepository As IFixedAssetSequenceDetailRepository, FixedAssetEntryRepository As IFixedAssetEntryRepository,
                   physicalAssetRepository As IFixedAssetPhysicalAssetRepository, accountingRepository As IAccountingDocumentAdminService, supplierRepository As ISupplierRepository)
        If sequenceRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceRepository")
        End If
        If FixedAssetTransferRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetTransferRepository")
        End If
        If FixedAssetEntryRepository Is Nothing Then
            Throw New ArgumentNullException("FixedAssetEntryRepository")
        End If
        If physicalAssetRepository Is Nothing Then
            Throw New ArgumentNullException("physicalAssetRepository")
        End If
        If accountingRepository Is Nothing Then
            Throw New ArgumentNullException("accountingRepository")
        End If
        _sequenceRepository = sequenceRepository
        _FixedAssetTransferRepository = FixedAssetTransferRepository
        _FixedAssetEntryRepository = FixedAssetEntryRepository
        _physicalAssetRepository = physicalAssetRepository
        _accountingRepository = accountingRepository
        _supplierRepository = supplierRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma el registro
    ''' </summary>
    ''' <param name="FixedAssetTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmFixedAssetTransfer(FixedAssetTransfer As FixedAssetTransfer, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetTransfer) Implements IFixedAssetTransferAdminService.ConfirmFixedAssetTransfer
        If FixedAssetTransfer Is Nothing Then
            Throw New ArgumentNullException("FixedAssetTransfer")
        End If

        Dim unitWorkPhysicalAsset As IUnitWork = _physicalAssetRepository.UnitWork

        'Se consulta si hay parámetros de activo fijo
        Dim settingFixedAsset As SettingFixedAsset = _FixedAssetEntryRepository.GetSettingFixedAssetByOperatingUnidId(FixedAssetTransfer.OperatingUnitId)
        If settingFixedAsset Is Nothing Then
            Return New ActionResult(Of FixedAssetTransfer) With {.StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo para la unidad operativa escogida."}
        End If
        'Se valida que la fecha de traslado este en el mismo mes que la fecha de parametros
        If FixedAssetTransfer.DocumentDate.Month <> settingFixedAsset.ProcessDate.Month Then
            Return New ActionResult(Of FixedAssetTransfer) With {.StatusCode = eStatusResult.WARNING, .Message = "El mes de la fecha de traslado(" + FixedAssetTransfer.DocumentDate.ToString("MMMM") + ") debe ser el mismo a la fecha de proceso de parámetros(" + settingFixedAsset.ProcessDate.ToString("MMMM") + ")"}
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Se guarda el traslado de activos
                Dim resultSave As ActionResult(Of FixedAssetTransfer) = SaveFixedAssetTransfer(FixedAssetTransfer, audit, idSequense)
                If resultSave.StateResult = False Then
                    transaction.Dispose()
                    If resultSave.StatusCode = eStatusResult.EXCEPTION Then
                        Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = resultSave.Message}
                    ElseIf resultSave.StatusCode = eStatusResult.WARNING Then
                        Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultSave.Message}
                    End If
                End If

                'Se modifican la localizacion o responsable de physicalAsset segun corresponda y se crea el kardex
                Dim ListPhysical = CreateListPhysical(resultSave.ObjectEmbbeded, audit)
                If ListPhysical IsNot Nothing AndAlso ListPhysical.Count > 0 Then
                    'Se actualiza el physical y se crea el kardex
                    For Each physical In ListPhysical
                        _physicalAssetRepository.SaveEntity(physical)
                    Next
                End If

                'Se crea el comprobante contable siempre y cuando el tipo de traslado sea responsable o responsable/localización
                Dim resultAccounting As ActionMessageResult(Of JournalVouchers) = Nothing
                If FixedAssetTransfer.TransferType = 1 OrElse FixedAssetTransfer.TransferType = 3 Then
                    Dim JournalVouchers = CreateJournalVoucher(FixedAssetTransfer, settingFixedAsset)
                    If JournalVouchers.JournalVoucherDetails IsNot Nothing AndAlso JournalVouchers.JournalVoucherDetails.Count > 0 Then 'Si tiene detalles el comprobante es porque se realiza el comprobante
                        'Se guarda el comprobante
                        resultAccounting = _accountingRepository.SaveAccountingDocument(JournalVouchers, audit)
                        If resultAccounting.StateResult = False Then
                            unitWorkPhysicalAsset.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = resultAccounting.Message}
                        End If
                    End If
                End If

                Dim message As New StringBuilder
                message.AppendLine("El registro se guardó y confirmó con código: " + resultSave.ObjectEmbbeded.Code)
                If resultAccounting IsNot Nothing Then
                    'Se consulta que tipo de comprobante fue que genero
                    Dim journalVoucherType As JournalVoucherTypes = _FixedAssetEntryRepository.GetJournalVoucherType(settingFixedAsset.TransferJournalVoucherId)
                    message.AppendLine("Se generó comprobante contable " + journalVoucherType.Name.ToString + ": " + resultAccounting.ObjectEmbbeded.Consecutive.ToString)
                End If

                unitWorkPhysicalAsset.Commit()
                transaction.Complete()
                Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = True, .ObjectEmbbeded = FixedAssetTransfer, .StatusCode = eStatusResult.SUCCESS, .Message = message.ToString}
            Catch ex As OptimisticConcurrencyException
                unitWorkPhysicalAsset.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                unitWorkPhysicalAsset.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que construye el comprobante contable
    ''' </summary>
    ''' <param name="FixedAssetTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateJournalVoucher(FixedAssetTransfer As FixedAssetTransfer, SettingFixedAsset As SettingFixedAsset) As JournalVouchers
        Dim JournalVouchers As New JournalVouchers
        With JournalVouchers
            'Se arma la cabecera
            .IdJournalVoucher = SettingFixedAsset.TransferJournalVoucherId
            .VoucherDate = FixedAssetTransfer.DocumentDate
            .Status = 2
            .Detail = "Comprobante contable generado desde traslado de activos"
            .EntityId = FixedAssetTransfer.Id
            .EntityCode = FixedAssetTransfer.Code
            .EntityName = GetType(FixedAssetTransfer).Name
            .IsClosedYear = False

            'Se obtienen las localizaciones para de ellas sacar el centro de costo
            Dim SourceLocation = _FixedAssetEntryRepository.GetFixedAssetLocationById(FixedAssetTransfer.SourceLocationId)
            Dim TargetLocation = _FixedAssetEntryRepository.GetFixedAssetLocationById(FixedAssetTransfer.TargetLocationId)

            'Ids del physical para consultar
            Dim ListIds As List(Of Integer) = (From l In FixedAssetTransfer.FixedAssetTransferDetail Select l.PhysicalAssetId).ToList

            'Listado del physical para armar los detalles del comprobante
            Dim ListPhysical As List(Of FixedAssetPhysicalAsset) = _FixedAssetTransferRepository.GetListFixedAssetPhysicalAsset(ListIds, False)

            'Se arma los detalles con el listado del physical, siempre y cuando la cuenta que este alli registrada maneje centro de costo
            For Each physical In (From p In ListPhysical Where p.MainAccounts.HandlesCostCenter = True Select p).ToList
                'Si no es un Comodato Tercerizado ni Renting Operativo puesto que estos Tipo de Adquisición no Contabilizan
                If Not (physical.AdquisitionType = 8 OrElse physical.AdquisitionType = 10 OrElse physical.FixedAssetItem.FixedAssetItemCatalog.Classification = 3) Then
                    'Se genera un detalle al debito
                    Dim JournalVouchersDetail As New JournalVoucherDetails
                    JournalVouchersDetail.IdMainAccount = physical.MainAccountId
                    JournalVouchersDetail.IdThirdParty = _supplierRepository.GetSupplierById(physical.SupplierId, False).IdThirdParty
                    JournalVouchersDetail.IdCostCenter = TargetLocation.FunctionalUnit.CostCenterId
                    JournalVouchersDetail.DebitValue = physical.HistoricalValue
                    JournalVouchersDetail.CreditValue = 0
                    JournalVouchersDetail.Detail = "Detalle generado desde traslado de activos"
                    .JournalVoucherDetails.Add(JournalVouchersDetail)

                    'Se genera un detalle al credito
                    JournalVouchersDetail = New JournalVoucherDetails
                    JournalVouchersDetail.IdMainAccount = physical.MainAccountId
                    JournalVouchersDetail.IdThirdParty = _supplierRepository.GetSupplierById(physical.SupplierId, False).IdThirdParty
                    JournalVouchersDetail.IdCostCenter = SourceLocation.FunctionalUnit.CostCenterId
                    JournalVouchersDetail.DebitValue = 0
                    JournalVouchersDetail.CreditValue = physical.HistoricalValue
                    JournalVouchersDetail.Detail = "Detalle generado desde traslado de activos"
                    .JournalVoucherDetails.Add(JournalVouchersDetail)
                End If
            Next
        End With

        Return JournalVouchers
    End Function

    ''' <summary>
    ''' Metodo que construye un listado de physical para actualizarlos y crear el kardex
    ''' </summary>
    ''' <param name="FixedAssetTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateListPhysical(FixedAssetTransfer As FixedAssetTransfer, audit As AuditMessage) As List(Of FixedAssetPhysicalAsset)
        'Listado de id de physical para consultar
        Dim ListIds As List(Of Integer) = (From l In FixedAssetTransfer.FixedAssetTransferDetail Select l.PhysicalAssetId).ToList

        'Listado a devolver
        Dim ListPhysical As List(Of FixedAssetPhysicalAsset) = _FixedAssetTransferRepository.GetListFixedAssetPhysicalAsset(ListIds)

        'Clase de la localización
        Dim classLocation As Integer = 0

        For Each physical In ListPhysical
            'Se modifican los campos necesarios para el physical
            Select Case FixedAssetTransfer.TransferType
                Case 1 'Localización
                    physical.LocationId = FixedAssetTransfer.TargetLocationId
                    classLocation = _FixedAssetTransferRepository.GetClassOfLocation(FixedAssetTransfer.TargetLocationId)
                    If (classLocation = 1 OrElse classLocation = 2) AndAlso physical.InstallationDate Is Nothing Then 'Si la clase es 1=Administrativa u 2=Operativa y además el campo InstallationDate este nulo
                        physical.InstallationDate = FixedAssetTransfer.DocumentDate
                    End If
                Case 2 'Responsable
                    physical.ResponsibleId = FixedAssetTransfer.TargetResponsibleId
                Case 3 'Localización y Responsable
                    physical.LocationId = FixedAssetTransfer.TargetLocationId
                    physical.ResponsibleId = FixedAssetTransfer.TargetResponsibleId
                    classLocation = _FixedAssetTransferRepository.GetClassOfLocation(FixedAssetTransfer.TargetLocationId)
                    If (classLocation = 1 OrElse classLocation = 2) AndAlso physical.InstallationDate Is Nothing Then 'Si la clase es 1=Administrativa u 2=Operativa y además el campo InstallationDate este nulo
                        physical.InstallationDate = FixedAssetTransfer.DocumentDate
                    End If
            End Select

            'Se crea el nuevo objeto para insertar al physical
            Dim FixedAssetKardexItem As New FixedAssetKardexItem
            With FixedAssetKardexItem
                .MovementType = 1 'Entrada
                .PhysicalAssetId = physical.Id
                .PreviousLocationId = FixedAssetTransfer.SourceLocationId
                .PreviousResponsibleId = FixedAssetTransfer.SourceResponsibleId

                Select Case FixedAssetTransfer.TransferType
                    Case 1 'Localización
                        .LocationId = FixedAssetTransfer.TargetLocationId
                        .ResponsibleId = physical.ResponsibleId
                    Case 2 'Responsable
                        .ResponsibleId = FixedAssetTransfer.TargetResponsibleId
                        .LocationId = physical.LocationId
                    Case 3 'Localizació y Responsable
                        .LocationId = FixedAssetTransfer.TargetLocationId
                        .ResponsibleId = FixedAssetTransfer.TargetResponsibleId
                End Select

                .DocumentDate = FixedAssetTransfer.DocumentDate
                .EntityId = FixedAssetTransfer.Id
                .EntityCode = FixedAssetTransfer.Code
                .EntityName = GetType(FixedAssetTransfer).Name
                .AffectPhysical = True
                .CreationUser = audit.CodeUser
                .CreationDate = DateTime.Now
            End With

            physical.FixedAssetKardexItem.Add(FixedAssetKardexItem)
        Next

        Return ListPhysical
    End Function

    ''' <summary>
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetTransfer(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetTransfer) Implements IFixedAssetTransferAdminService.GetFixedAssetTransfer
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetTransfer As FixedAssetTransfer = Me._FixedAssetTransferRepository.GetFixedAssetTransfer(code.Trim())
            If FixedAssetTransfer IsNot Nothing AndAlso FixedAssetTransfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetTransfer)(FixedAssetTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = True, .ObjectEmbbeded = FixedAssetTransfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetTransferById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetTransfer) Implements IFixedAssetTransferAdminService.GetFixedAssetTransferById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim FixedAssetTransfer As FixedAssetTransfer = Me._FixedAssetTransferRepository.GetFixedAssetTransferById(Id)
            If FixedAssetTransfer IsNot Nothing AndAlso FixedAssetTransfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetTransfer)(FixedAssetTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = True, .ObjectEmbbeded = FixedAssetTransfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="FixedAssetTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetTransfer(FixedAssetTransfer As FixedAssetTransfer, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetTransfer) Implements IFixedAssetTransferAdminService.SaveFixedAssetTransfer
        If FixedAssetTransfer Is Nothing Then
            Throw New ArgumentNullException("FixedAssetTransfer")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetTransferRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As FixedAssetSequenceDetail = Nothing
                If FixedAssetTransfer.Code Is Nothing OrElse FixedAssetTransfer.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetTransfer.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            transaction.Dispose()
                            Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .Message = "El rango de la secuencia numérica ya se excedió", .StatusCode = eStatusResult.WARNING}
                        End If
                    Else
                        transaction.Dispose()
                        Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .Message = "El formulario no tiene parametrizada la secuencia numérica", .StatusCode = eStatusResult.WARNING}
                    End If
                End If

                Dim auxFixedAssetTransfer As FixedAssetTransfer = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetTransfer)
                Dim status As Integer

                If FixedAssetTransfer.ChangeTracker.State = ObjectState.Added Then
                    FixedAssetTransfer.CreationUser = audit.CodeUser
                    FixedAssetTransfer.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf FixedAssetTransfer.ChangeTracker.State = ObjectState.Modified Then
                    auxFixedAssetTransfer = FixedAssetTransfer.OriginalValue
                    If FixedAssetTransfer.Status = 1 Then
                        FixedAssetTransfer.ModificationUser = audit.CodeUser
                        FixedAssetTransfer.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    End If
                    If FixedAssetTransfer.Status = 2 Then
                        FixedAssetTransfer.ModificationUser = audit.CodeUser
                        FixedAssetTransfer.ModificationDate = DateTime.Now
                        FixedAssetTransfer.ConfirmationUser = audit.CodeUser
                        FixedAssetTransfer.ConfirmationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    End If
                    If FixedAssetTransfer.Status = 3 Then
                        FixedAssetTransfer.ModificationUser = audit.CodeUser
                        FixedAssetTransfer.ModificationDate = DateTime.Now
                        FixedAssetTransfer.AnnulmentUser = audit.CodeUser
                        FixedAssetTransfer.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                Me._FixedAssetTransferRepository.SaveEntity(FixedAssetTransfer)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetTransfer)(FixedAssetTransfer, audit, status, auxFixedAssetTransfer)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetTransfer.MarkAsUnchanged()

                transaction.Complete()
                Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = True, .ObjectEmbbeded = FixedAssetTransfer, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetTransfer) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
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
            _sequenceRepository = Nothing
            _FixedAssetTransferRepository = Nothing
            _FixedAssetEntryRepository = Nothing
            _physicalAssetRepository = Nothing
            _accountingRepository = Nothing
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
