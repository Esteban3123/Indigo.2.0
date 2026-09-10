Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class PharmaceuticalDispensingTransferRepository
    Inherits GenericRepository(Of PharmaceuticalDispensingTransfer)
    Implements IPharmaceuticalDispensingTransferRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

#End Region

#Region "Methods"

    Public Function GetPharmaceuticalDispensingTransferById(id As Integer) As PharmaceuticalDispensingTransfer Implements IPharmaceuticalDispensingTransferRepository.GetPharmaceuticalDispensingTransferById
        Dim res = (From re In _context.PharmaceuticalDispensingTransfer.Include("PharmaceuticalDispensingTransferDetail") Where re.Id = id Select re).FirstOrDefault()

        If res IsNot Nothing Then
            Dim warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select w).FirstOrDefault()
            res.WarehouseCodeName = warehouse.Code + " - " + warehouse.Name
            res.Prefix = warehouse.Prefix

            If res.PharmaceuticalDispensingTransferDetail IsNot Nothing AndAlso res.PharmaceuticalDispensingTransferDetail.Count > 0 Then
                For Each item As PharmaceuticalDispensingTransferDetail In res.PharmaceuticalDispensingTransferDetail
                    Dim pddbs = (From d In _context.PharmaceuticalDispensingDetailBatchSerial.AsNoTracking().
                                     Include("PharmaceuticalDispensingDetail").AsNoTracking().
                                     Include("PharmaceuticalDispensingDetail.PharmaceuticalDispensing").AsNoTracking().
                                     Include("PharmaceuticalDispensingDetail.InventoryProduct").AsNoTracking()
                                 Where d.Id = item.PharmaceuticalDispensingDetailBatchSerialId Select d).FirstOrDefault()

                    item.PharmaceuticalDispensingCode = pddbs.PharmaceuticalDispensingDetail.PharmaceuticalDispensing.Code
                    item.PharmaceuticalDispensingDetailId = pddbs.PharmaceuticalDispensingDetailId
                    item.ProductId = pddbs.PharmaceuticalDispensingDetail.ProductId
                    item.ProductCodeName = String.Format("{0} - {1}", pddbs.PharmaceuticalDispensingDetail.InventoryProduct.Code, pddbs.PharmaceuticalDispensingDetail.InventoryProduct.Name)
                Next
            End If

            res.OriginalValue = (From re In _context.PharmaceuticalDispensingTransfer.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
            Return res
        Else
            Return New PharmaceuticalDispensingTransfer
        End If
    End Function

    Public Function GetPharmaceuticalDispensingTransferByCode(code As String) As PharmaceuticalDispensingTransfer Implements IPharmaceuticalDispensingTransferRepository.GetPharmaceuticalDispensingTransferByCode
        Dim res = (From re In _context.PharmaceuticalDispensingTransfer.Include("PharmaceuticalDispensingTransferDetail") Where re.Code = code Select re).FirstOrDefault()

        If res IsNot Nothing Then
            Dim warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select w).FirstOrDefault()
            res.WarehouseCodeName = warehouse.Code + " - " + warehouse.Name
            res.Prefix = warehouse.Prefix

            If res.PharmaceuticalDispensingTransferDetail IsNot Nothing AndAlso res.PharmaceuticalDispensingTransferDetail.Count > 0 Then
                For Each item As PharmaceuticalDispensingTransferDetail In res.PharmaceuticalDispensingTransferDetail
                    Dim pddbs = (From d In _context.PharmaceuticalDispensingDetailBatchSerial.AsNoTracking().
                                     Include("PharmaceuticalDispensingDetail").AsNoTracking().
                                     Include("PharmaceuticalDispensingDetail.PharmaceuticalDispensing").AsNoTracking().
                                     Include("PharmaceuticalDispensingDetail.InventoryProduct").AsNoTracking()
                                 Where d.Id = item.PharmaceuticalDispensingDetailBatchSerialId Select d).FirstOrDefault()

                    item.PharmaceuticalDispensingCode = pddbs.PharmaceuticalDispensingDetail.PharmaceuticalDispensing.Code
                    item.PharmaceuticalDispensingDetailId = pddbs.PharmaceuticalDispensingDetailId
                    item.ProductId = pddbs.PharmaceuticalDispensingDetail.ProductId
                    item.ProductCodeName = String.Format("{0} - {1}", pddbs.PharmaceuticalDispensingDetail.InventoryProduct.Code, pddbs.PharmaceuticalDispensingDetail.InventoryProduct.Name)
                Next
            End If

            res.OriginalValue = (From re In _context.PharmaceuticalDispensingTransfer.AsNoTracking() Where re.Code = code Select re).FirstOrDefault()
            Return res
        Else
            Return New PharmaceuticalDispensingTransfer
        End If
    End Function

    Public Function SP_SavePharmaceuticalDispensingTransfer(pharmaceuticalDispensingTransferXml As String, userCode As String) As SP_SavePharmaceuticalDispensingTransfer_Result Implements IPharmaceuticalDispensingTransferRepository.SP_SavePharmaceuticalDispensingTransfer
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SavePharmaceuticalDispensingTransfer(pharmaceuticalDispensingTransferXml, userCode).SingleOrDefault()
    End Function

#End Region

End Class
