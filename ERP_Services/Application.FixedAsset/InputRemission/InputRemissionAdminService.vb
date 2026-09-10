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

#End Region

Public Class FixedAssetRemissionEntranceAdminService

    Implements IFixedAssetRemissionEntranceAdminService

    'Repositorio de la aseguradora
    Private _FixedAssetRemissionEntranceRepository As IFixedAssetFixedAssetRemissionEntranceRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    Private _PurchaseOrderRepository As IFixedAssetPurchaseOrderRepository

    Private _physicalAssetRepository As IFixedAssetPhysicalAssetRepository

    Private _settingFixedAssetResporitory As ISettingFixedAssetRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="FixedAssetRemissionEntranceRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal FixedAssetRemissionEntranceRepository As IFixedAssetFixedAssetRemissionEntranceRepository, sequenceRepository As IFixedAssetSequenceDetailRepository,
                   PurchaseOrderRepository As IFixedAssetPurchaseOrderRepository, physicalAssetRepository As IFixedAssetPhysicalAssetRepository, settingFixedAssetResporitory As ISettingFixedAssetRepository)
        If (FixedAssetRemissionEntranceRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio FixedAssetRemissionEntranceRepository vacio")
        End If
        If (physicalAssetRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio physicalAssetRepository vacio")
        End If
        _sequenceRepository = sequenceRepository
        _FixedAssetRemissionEntranceRepository = FixedAssetRemissionEntranceRepository
        _PurchaseOrderRepository = PurchaseOrderRepository
        _physicalAssetRepository = physicalAssetRepository
        _settingFixedAssetResporitory = settingFixedAssetResporitory
    End Sub

    Public Function DeleteFixedAssetRemissionEntrance(FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, audit As AuditMessage) As Boolean Implements IFixedAssetRemissionEntranceAdminService.DeleteFixedAssetRemissionEntrance
        If FixedAssetRemissionEntrance Is Nothing Then
            Throw New ArgumentNullException("FixedAssetRemissionEntrance vacio")
        End If
        Dim unitWork As IUnitWork = _FixedAssetRemissionEntranceRepository.UnitWork
        'Dim unitWorkAdministrative As IUnitWork = _AdministrativeRepository.UnitWork
        'Dim unitWorkAsistential As IUnitWork = _AsistentialRepository.UnitWork
        Try

            FixedAssetRemissionEntrance.MarkAsDeleted()

            _FixedAssetRemissionEntranceRepository.DeleteEntity(FixedAssetRemissionEntrance)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetRemissionEntrance)(FixedAssetRemissionEntrance, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            'unitWorkAdministrative.RollbackChanges()
            'unitWorkAsistential.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetFixedAssetRemissionEntranceByCode(code As String) As FixedAssetRemissionEntrance Implements IFixedAssetRemissionEntranceAdminService.GetFixedAssetRemissionEntranceByCode
        Try
            Return _FixedAssetRemissionEntranceRepository.GetFixedAssetFixedAssetRemissionEntranceByCode(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New FixedAssetRemissionEntrance()
        End Try
    End Function

    Public Function SaveFixedAssetRemissionEntrance(FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetRemissionEntrance) Implements IFixedAssetRemissionEntranceAdminService.SaveFixedAssetRemissionEntrance
        If FixedAssetRemissionEntrance Is Nothing Then
            Throw New ArgumentNullException("Aseguradora vacio")
        End If
        Dim unitOfWork As IUnitWork = _FixedAssetRemissionEntranceRepository.UnitWork
        Dim unitWorkPurchase As IUnitWork = _PurchaseOrderRepository.UnitWork
        Dim unitWorkPhysicalAsset As IUnitWork = _physicalAssetRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Se consulta si hay parámetros de activo fijo
                Dim ObjFixedAssetSettings = _settingFixedAssetResporitory.GetSettingFixedAssetByOperatingUnitId(FixedAssetRemissionEntrance.OperatingUnitId)
                If ObjFixedAssetSettings Is Nothing AndAlso ObjFixedAssetSettings.Id = 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetRemissionEntrance) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo para la unidad operativa escogida."}
                End If
                'Se valida que existan parámetros definidos para el libro oficial en la unidad operativa actual
                If ObjFixedAssetSettings.SettingFixedAssetByLegalBook Is Nothing OrElse ObjFixedAssetSettings.SettingFixedAssetByLegalBook.Where(Function(d) d.OfficialBook = True AndAlso d.StatusBook = True).Count() = 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of FixedAssetRemissionEntrance) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = "No existe parámetros de activo fijo en unidad operativa escogida para el libro oficial."}
                End If

                Dim seq As Domain.Entities.FixedAssetSequenceDetail = Nothing
                Dim auxFixedAssetItemCatalog = FixedAssetRemissionEntrance.OriginalValue

                If FixedAssetRemissionEntrance.Code Is Nothing OrElse FixedAssetRemissionEntrance.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetRemissionEntrance.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of Domain.Entities.FixedAssetRemissionEntrance) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of Domain.Entities.FixedAssetRemissionEntrance) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                If FixedAssetRemissionEntrance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    FixedAssetRemissionEntrance.CreationDate = Date.Now()
                    FixedAssetRemissionEntrance.CreationUser = audit.CodeUser
                End If

                If FixedAssetRemissionEntrance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    FixedAssetRemissionEntrance.ModificationDate = Date.Now()
                    FixedAssetRemissionEntrance.ModificationUser = audit.CodeUser
                End If


                If FixedAssetRemissionEntrance.Status = 2 Then
                    FixedAssetRemissionEntrance.ConfirmationDate = Date.Now()
                    FixedAssetRemissionEntrance.ConfirmationUser = audit.CodeUser
                End If

                If FixedAssetRemissionEntrance.Status = 3 Then
                    FixedAssetRemissionEntrance.AnnulmentDate = Date.Now()
                    FixedAssetRemissionEntrance.AnnulmentUser = audit.CodeUser
                End If


                Dim ListErrors As New StringBuilder
                For Each detail In FixedAssetRemissionEntrance.FixedAssetRemissionEntranceItem
                    Dim query = From item In detail.FixedAssetRemissionEntranceItemDetail.AsEnumerable()
                                Group item By Key = item.Plate Into Group
                                Where Group.Count > 1
                                Select New With {
                                        .Terminal = Key,
                                        .Rows = Group
                                    }

                    For Each item In query
                        For Each row In item.Rows
                            ListErrors.AppendLine("La placa " + row.Plate + " se repite")
                            Continue For
                        Next
                    Next

                Next

                If FixedAssetRemissionEntrance.Status = 2 Then 'Si se esta confirmando
                    'Se modifica las ordenes de compra que se llamaron
                    If FixedAssetRemissionEntrance.FixedAssetRemissionEntranceItem IsNot Nothing AndAlso FixedAssetRemissionEntrance.FixedAssetRemissionEntranceItem.Count > 0 Then
                        'Dim ListErrors As New StringBuilder
                        For Each detail In FixedAssetRemissionEntrance.FixedAssetRemissionEntranceItem
                            If detail.RemissionSource = 2 Then 'Orden de compra
                                Dim PurchaseOrderEquipment = _PurchaseOrderRepository.GetFixedAssetPurchaseOrderItemById(detail.PurchaseOrderItemId)
                                If PurchaseOrderEquipment IsNot Nothing Then

                                    'Se valida que las cantidades esten dentro del rango
                                    If detail.Quantity > PurchaseOrderEquipment.OutstandingQuantity Then
                                        ListErrors.AppendLine("La cantidad del articulo " + detail.NameEquipment + " es mayor a la cantidad de la orden de compra")
                                        Continue For
                                    End If
                                    ''Se reduce las cantidades disponibles y aumenta la cantidades legalizadas en la orden de compra
                                    PurchaseOrderEquipment.OutstandingQuantity = PurchaseOrderEquipment.OutstandingQuantity - detail.Quantity
                                    PurchaseOrderEquipment.CancelledQuantity += detail.Quantity
                                    PurchaseOrderEquipment.MarkAsModified()
                                    Me._PurchaseOrderRepository.SaveEntity(PurchaseOrderEquipment.FixedAssetPurchaseOrder)
                                End If
                            End If


                        Next
                        If ListErrors.Length > 0 Then
                            unitOfWork.RollbackChanges()
                            transaction.Dispose()
                            Return New ActionResult(Of FixedAssetRemissionEntrance) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ListErrors.ToString}
                        End If

                        Dim UnitValue As Decimal = 0
                        Dim PercentageIVA As Decimal = 0
                        Dim IvaValue As Decimal = 0
                        Dim IvaCost As Boolean = False

                        Dim SettingFixedAssetLegalBookOfficial = ObjFixedAssetSettings.SettingFixedAssetByLegalBook.Where(Function(d) d.OfficialBook = True AndAlso d.StatusBook = True).FirstOrDefault()
                        IvaCost = SettingFixedAssetLegalBookOfficial.IvaCost

                        For Each remissionEntranceItem In FixedAssetRemissionEntrance.FixedAssetRemissionEntranceItem
                            If IvaCost = True Then '' Si en los Parámetros maneja IVA AL COSTO
                                PercentageIVA = remissionEntranceItem.IvaPercentage
                                UnitValue = remissionEntranceItem.UnitValue
                                IvaValue = (UnitValue * PercentageIVA) / 100
                            End If

                            If remissionEntranceItem.FixedAssetRemissionEntranceItemDetail IsNot Nothing AndAlso remissionEntranceItem.FixedAssetRemissionEntranceItemDetail.Count > 0 Then
                                Dim ListPhysycalAsset As New List(Of FixedAssetPhysicalAsset)
                                For Each remissionEntranceItemDetail In remissionEntranceItem.FixedAssetRemissionEntranceItemDetail
                                    Dim ObjFixedAssetPhysicalAsset = _physicalAssetRepository.GetFixedAssetPhysicalAssetByPlate(remissionEntranceItemDetail.Plate)

                                    If ObjFixedAssetPhysicalAsset IsNot Nothing And ObjFixedAssetPhysicalAsset.Id > 0 Then
                                        ListErrors.AppendLine("La placa " + remissionEntranceItemDetail.Plate + " Ya existe en el Inventario Físico")
                                        Continue For
                                    End If

                                    If ListErrors.Length > 0 Then
                                        unitOfWork.RollbackChanges()
                                        transaction.Dispose()
                                        Return New ActionResult(Of FixedAssetRemissionEntrance) With {.StateResult = False, .Message = ListErrors.ToString, .StatusCode = eStatusResult.WARNING}
                                    End If

                                    Dim FixedAssetPhysicalAsset As New FixedAssetPhysicalAsset
                                    With FixedAssetPhysicalAsset
                                        .ItemId = remissionEntranceItem.ItemId
                                        .MainAccountId = _FixedAssetRemissionEntranceRepository.GetMainAccountWithItemId(.ItemId, FixedAssetRemissionEntrance.AdquisitionType)
                                        .Serie = remissionEntranceItemDetail.Serie
                                        .Plate = remissionEntranceItemDetail.Plate
                                        .LocationId = remissionEntranceItemDetail.LocationId
                                        .ResponsibleId = remissionEntranceItemDetail.ResponsibleId
                                        .SupplierId = _FixedAssetRemissionEntranceRepository.GetSupplierBySupplierDistributionLineId(FixedAssetRemissionEntrance.SupplierDistributionLineId)
                                        .HistoricalValue = remissionEntranceItem.UnitValue + IvaValue
                                        .FairValue = remissionEntranceItem.UnitValue
                                        .TrademarkId = remissionEntranceItem.TrademarkId
                                        .Model = remissionEntranceItem.Model
                                        .PolicyId = remissionEntranceItem.PolicyId
                                        .HandlesWarranty = remissionEntranceItemDetail.HandlesWarranty
                                        .WarrantyExpirationDate = remissionEntranceItemDetail.WarrantyExpirationDate
                                        .AdquisitionType = FixedAssetRemissionEntrance.AdquisitionType
                                        .AdquisitionDate = remissionEntranceItemDetail.AdquisitionDate
                                        .Depreciate = remissionEntranceItemDetail.Depreciate
                                        .StatusAssetId = remissionEntranceItemDetail.StatusAssetId
                                        .Status = True

                                        Dim FixedAssetKardexItem As New FixedAssetKardexItem
                                        FixedAssetKardexItem.MovementType = 1
                                        FixedAssetKardexItem.ResponsibleId = remissionEntranceItemDetail.ResponsibleId
                                        FixedAssetKardexItem.LocationId = remissionEntranceItemDetail.LocationId
                                        FixedAssetKardexItem.DocumentDate = FixedAssetRemissionEntrance.RemisionDate
                                        FixedAssetKardexItem.EntityId = FixedAssetRemissionEntrance.Id
                                        FixedAssetKardexItem.EntityCode = FixedAssetRemissionEntrance.Code
                                        FixedAssetKardexItem.EntityName = GetType(FixedAssetRemissionEntrance).ToString
                                        FixedAssetKardexItem.ImportedEntityId = Nothing
                                        FixedAssetKardexItem.ImportedEntityCode = Nothing
                                        FixedAssetKardexItem.ImportedEntityName = Nothing
                                        FixedAssetKardexItem.AffectPhysical = False
                                        FixedAssetKardexItem.CreationUser = audit.CodeUser
                                        FixedAssetKardexItem.CreationDate = DateTime.Now
                                        .FixedAssetKardexItem.Add(FixedAssetKardexItem)

                                        'Si es un comodato Tercerizado o un Renting Operativo no agregar información contable
                                        If .AdquisitionType <> 8 AndAlso .AdquisitionType <> 10 Then
                                            If remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBook IsNot Nothing AndAlso remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBook.Count > 0 Then
                                                For Each remissionEntranceItemDetailBook In remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBook
                                                    Dim FixedAssetPhysicalAssetDetailBook As New FixedAssetPhysicalAssetDetailBook
                                                    FixedAssetPhysicalAssetDetailBook.LegalBookId = remissionEntranceItemDetailBook.LegalBookId
                                                    FixedAssetPhysicalAssetDetailBook.LifeTime = remissionEntranceItemDetailBook.LifeTime
                                                    FixedAssetPhysicalAssetDetailBook.UnitLifeTime = remissionEntranceItemDetailBook.UnitLifeTime
                                                    FixedAssetPhysicalAssetDetailBook.ValorizationDays = 0
                                                    FixedAssetPhysicalAssetDetailBook.DaysPendingDepreciate = 0
                                                    FixedAssetPhysicalAssetDetailBook.DepreciatedDays = 0
                                                    FixedAssetPhysicalAssetDetailBook.DepreciationType = remissionEntranceItemDetailBook.DepreciationType
                                                    FixedAssetPhysicalAssetDetailBook.TotalProductionUnit = remissionEntranceItemDetailBook.TotalProductionUnit
                                                    FixedAssetPhysicalAssetDetailBook.PercentageRescue = remissionEntranceItemDetailBook.PercentageRescue
                                                    FixedAssetPhysicalAssetDetailBook.Valorization = 0
                                                    FixedAssetPhysicalAssetDetailBook.Devaluation = 0
                                                    FixedAssetPhysicalAssetDetailBook.AdjustedValue = 0
                                                    FixedAssetPhysicalAssetDetailBook.TransactionValue = 0
                                                    FixedAssetPhysicalAssetDetailBook.DepreciatedValue = 0
                                                    FixedAssetPhysicalAssetDetailBook.DepreciatedValuePart = 0
                                                    FixedAssetPhysicalAssetDetailBook.ResidualValue = 0
                                                    FixedAssetPhysicalAssetDetailBook.ResidualValuePart = 0
                                                    .FixedAssetPhysicalAssetDetailBook.Add(FixedAssetPhysicalAssetDetailBook)
                                                Next
                                            End If
                                        End If

                                        If remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPart IsNot Nothing AndAlso remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPart.Count > 0 Then
                                            For Each remissionEntranceItemDetailPart In remissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPart
                                                Dim FixedAssetPhysicalAssetParts As New FixedAssetPhysicalAssetParts
                                                FixedAssetPhysicalAssetParts.PartAccesoriesConsumiblesId = remissionEntranceItemDetailPart.PartAccesoriesConsumiblesId
                                                FixedAssetPhysicalAssetParts.DepreciatePart = remissionEntranceItemDetailPart.DepreciatePart
                                                FixedAssetPhysicalAssetParts.HistoricalValue = remissionEntranceItemDetailPart.Value

                                                If remissionEntranceItemDetailPart.FixedAssetRemissionEntranceItemDetailPartBook IsNot Nothing AndAlso remissionEntranceItemDetailPart.FixedAssetRemissionEntranceItemDetailPartBook.Count > 0 Then
                                                    For Each remissionEntranceItemDetailPartBook In remissionEntranceItemDetailPart.FixedAssetRemissionEntranceItemDetailPartBook
                                                        Dim FixedAssetPhysicalAssetPartsDetailBook As New FixedAssetPhysicalAssetPartsDetailBook
                                                        FixedAssetPhysicalAssetPartsDetailBook.LegalBookId = remissionEntranceItemDetailPartBook.LegalBookId
                                                        FixedAssetPhysicalAssetPartsDetailBook.LifeTime = remissionEntranceItemDetailPartBook.LifeTime
                                                        FixedAssetPhysicalAssetPartsDetailBook.UnitLifeTime = remissionEntranceItemDetailPartBook.UnitLifeTime
                                                        FixedAssetPhysicalAssetPartsDetailBook.ValorizationDays = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.DaysPendingDepreciate = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.DepreciatedDays = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.DepreciationType = remissionEntranceItemDetailPartBook.DepreciationType
                                                        FixedAssetPhysicalAssetPartsDetailBook.TotalProductionUnit = remissionEntranceItemDetailPartBook.TotalProductionUnit
                                                        FixedAssetPhysicalAssetPartsDetailBook.PercentageRescue = remissionEntranceItemDetailPartBook.PercentageRescue
                                                        FixedAssetPhysicalAssetPartsDetailBook.Valorization = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.Devaluation = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.AdjustedValue = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.TransactionValue = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.DepreciatedValue = 0
                                                        FixedAssetPhysicalAssetPartsDetailBook.ResidualValue = 0
                                                        FixedAssetPhysicalAssetParts.FixedAssetPhysicalAssetPartsDetailBook.Add(FixedAssetPhysicalAssetPartsDetailBook)
                                                    Next
                                                End If

                                                .FixedAssetPhysicalAssetParts.Add(FixedAssetPhysicalAssetParts)
                                            Next
                                        End If

                                    End With

                                    ListPhysycalAsset.Add(FixedAssetPhysicalAsset)

                                Next


                                'Se guarda en el kardex y physycal
                                For Each physical In ListPhysycalAsset
                                    _physicalAssetRepository.SaveEntity(physical)
                                Next

                            End If

                        Next

                    End If
                End If

                Me._FixedAssetRemissionEntranceRepository.SaveEntity(FixedAssetRemissionEntrance)
                unitOfWork.Commit()
                unitWorkPurchase.Commit()
                unitWorkPhysicalAsset.Commit()
                sequenseUnitOfWork.Commit()

                FixedAssetRemissionEntrance.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of FixedAssetRemissionEntrance) With {.StateResult = True, .ObjectEmbbeded = FixedAssetRemissionEntrance, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of FixedAssetRemissionEntrance) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

                Dim message = ex.Message
                If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
                    message = ex.InnerException.Message
                    If ex.InnerException.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.InnerException.Message) Then
                        message = ex.InnerException.InnerException.Message
                    End If
                End If

                Return New ActionResult(Of FixedAssetRemissionEntrance) With {.StateResult = False, .Message = message, .StatusCode = eStatusResult.EXCEPTION}
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
            _FixedAssetRemissionEntranceRepository = Nothing
            _PurchaseOrderRepository = Nothing
            _physicalAssetRepository = Nothing
            _settingFixedAssetResporitory = Nothing
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
