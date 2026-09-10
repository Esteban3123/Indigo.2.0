'************************************************************
' Assembly         : Domain.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-10-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IElectronicDocumentDetailRepository
    Inherits IRepository(Of ElectronicDocumentDetail)

End Interface
