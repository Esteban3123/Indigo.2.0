Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base
Imports System.Data.Entity.Infrastructure

Public Class InventoryConsignmentTransferRepository
    Inherits GenericRepository(Of ConsignmentTransfer)
    Implements IInventoryConsignmentTransferRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

    Public Function GetByCode(code As String) As ConsignmentTransfer Implements IInventoryConsignmentTransferRepository.GetByCode
        Dim query = _context.ConsignmentTransfer _
            .Include("Currency") _
            .Include("ConsignmentTransferDetail.ConsignmentTransferDetailBatchSerial") _
            .Where(Function(m) m.Code = code).FirstOrDefault()

        If query IsNot Nothing Then
            Dim wh = _context.Warehouse.AsNoTracking().Where(Function(m) m.Id = query.WarehouseId).Select(Function(m) New With {Key m.Code, Key m.Name}).FirstOrDefault()

            If wh IsNot Nothing Then
                query.WarehouseCodeName = $"{wh.Code} - {wh.Name}"
            End If

            If query.ConsignmentTransferDetail.Any() Then
                For Each item In query.ConsignmentTransferDetail
                    Dim twh = _context.Warehouse.AsNoTracking().Where(Function(m) m.Id = item.WarehouseId).Select(Function(m) New With {Key m.Code, Key m.Name}).FirstOrDefault()
                    If twh IsNot Nothing Then
                        item.WarehouseCodeName = $"{twh.Code} - {twh.Name}"
                    End If

                    Dim prod = _context.InventoryProduct.AsNoTracking() _
                        .Include("ProductSubGroup").AsNoTracking() _
                        .Where(Function(m) m.Id = item.ProductId) _
                        .Select(Function(m) New With {Key m.Code, Key m.Name, m.ProductSubGroup.HandlesBatch}) _
                        .FirstOrDefault()
                    If prod IsNot Nothing Then
                        item.ProductCodeName = $"{prod.Code} - {prod.Name}"
                        item.HandlesBatch = prod.HandlesBatch
                    End If

                    For Each batch In item.ConsignmentTransferDetailBatchSerial
                        Dim batchCode = _context.PhysicalInventory.AsNoTracking().Include("BatchSerial").AsNoTracking() _
                            .Where(Function(m) m.Id = batch.PhysicalInventoryId).Select(Function(m) m.BatchSerial.BatchCode).FirstOrDefault()
                        batch.CodeBatchSerial = batchCode
                    Next
                Next
            End If
        End If

        Return query
    End Function

    '''' <summary>
    '''' Valida los datos importados desde Excel o Copy/Paste mediante SP
    '''' </summary>
    '''' <param name="xmlObject">XML con los datos a validar (incluye SourceWarehouseId automáticamente)</param>
    '''' <returns>Lista de resultados con las validaciones</returns>
    Public Function SetConsignmentTransferDetailFromFile(xmlObject As String) As List(Of SP_SetConsignmentTransferDetailFromFile_Result) Implements IInventoryConsignmentTransferRepository.SetConsignmentTransferDetailFromFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim parameters = New List(Of (String, Object)) From {
            ("@XmlObject", xmlObject)
        }
        Return Me.ExecuteStoredProcedure(Of SP_SetConsignmentTransferDetailFromFile_Result)("[Inventory].[SP_SetConsignmentTransferDetailFromFile]", parameters).ToList()
    End Function

#End Region

End Class
