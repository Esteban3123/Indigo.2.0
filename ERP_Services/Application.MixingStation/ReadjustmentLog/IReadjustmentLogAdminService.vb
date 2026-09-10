'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Diego A. Roldan
' Created          : 2023-01-13
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IReadjustmentLogAdminService
    Inherits IDisposable

    Function AddAdjustmentLog(requestMixingStationDetailId As Integer, requestPackageDetailStatusId As Integer, batchCode As String, audit As AuditMessage) As ActionResult

End Interface
