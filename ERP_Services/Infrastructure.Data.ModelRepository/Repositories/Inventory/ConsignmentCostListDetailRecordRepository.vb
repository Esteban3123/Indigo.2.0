'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Oscar Stiven Astudillo reyes
' Created          : 2024-02-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ConsignmentCostListDetailRecordRepository
    Inherits GenericRepository(Of ConsignmentCostListDetailRecord)
    Implements IConsignmentCostListDetailRecordRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

    ''' <summary>
    ''' Obtener los IDs de los 4 registros más recientes para este detalle 
    ''' </summary>
    ''' <param name="consignmentCostListDetailId"></param>
    Public Function LimitHistoricalRecords(consignmentCostListDetailId As Integer) As List(Of ConsignmentCostListDetailRecord) Implements IConsignmentCostListDetailRecordRepository.LimitHistoricalRecords
        Dim recentRecordsIds = _context.ConsignmentCostListDetailRecord.AsNoTracking().
        Where(Function(record) record.ConsignmentCostListDetailId = consignmentCostListDetailId).
        OrderByDescending(Function(record) record.CreationDate).
        Take(4).
        Select(Function(record) record.Id).
        ToList()
        ' Identificar los registros que no están entre los 4 más recientes y eliminarlos
        Dim recordsToDelete = _context.ConsignmentCostListDetailRecord.AsNoTracking().
        Where(Function(record) record.ConsignmentCostListDetailId = consignmentCostListDetailId AndAlso Not recentRecordsIds.Contains(record.Id)).
        ToList()
        Return recordsToDelete
    End Function


End Class
