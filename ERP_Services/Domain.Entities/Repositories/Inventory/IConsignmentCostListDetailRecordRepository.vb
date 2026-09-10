'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-02-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IConsignmentCostListDetailRecordRepository
    Inherits IRepository(Of ConsignmentCostListDetailRecord)



    ''' <summary>
    ''' Limita el guardado de informacion
    ''' </summary>
    Function LimitHistoricalRecords(consignmentCostListDetailId As Integer) As List(Of ConsignmentCostListDetailRecord)

End Interface
