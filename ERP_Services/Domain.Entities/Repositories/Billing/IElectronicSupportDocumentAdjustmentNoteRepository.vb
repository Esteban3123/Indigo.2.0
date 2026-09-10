'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 2022-07-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base

Public Interface IElectronicSupportDocumentAdjustmentNoteRepository
    Inherits IRepository(Of ElectronicSupportDocumentAdjustmentNote)

    Function GetElectronicAdjustmentWithAggregateNoteById(AdjustmentNoteId As Integer) As ElectronicSupportDocumentAdjustmentNote

End Interface
