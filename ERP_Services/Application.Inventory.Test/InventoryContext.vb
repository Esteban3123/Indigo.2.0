'***********************************************************************
' Assembly         : Application.Inventory.Tests
' Author           : LFP
' Created          : 2017-07-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Inventory.EntranceVoucher
Imports Application.Inventory.PharmaceuticalDispensing
Imports Application.Inventory.Sequense
Imports Application.Inventory.TransferOrder
Imports DistributedServices.Inventory.Unity
Imports Domain.Entities
Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
#End Region

Public Class InventoryContext
    Inherits TestingContext

#Region "Fields"
    Public _entranceVoucherAdminService As IEntranceVoucherAdminService
    Public _EntranceVoucherRepository As IEntranceVoucherRepository
    Public _InventoryControlDocumentRepository As IInventoryControlDocumentRepository
    Public _InventorySequenceAdminService As IInventorySequenceAdminService


    Public _pharmaceuticalDispensingAdminService As IPharmaceuticalDispensingAdminService


    Public _TransferOrderAdminService As ITransferOrderAdminService
#End Region

#Region "Builder"
    Public Sub New()
        MyBase.New()

        _container = Container.Current
        _entranceVoucherAdminService = _container.Resolve(Of IEntranceVoucherAdminService)
        _EntranceVoucherRepository = _container.Resolve(Of IEntranceVoucherRepository)
        _InventorySequenceAdminService = _container.Resolve(Of IInventorySequenceAdminService)
        _pharmaceuticalDispensingAdminService = _container.Resolve(Of IPharmaceuticalDispensingAdminService)
        _TransferOrderAdminService = _container.Resolve(Of ITransferOrderAdminService)

    End Sub
#End Region

End Class
