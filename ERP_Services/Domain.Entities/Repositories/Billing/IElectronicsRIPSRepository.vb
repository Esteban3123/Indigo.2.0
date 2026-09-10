'************************************************************
' Assembly         : Domain.Billing
' Author           : Giovanny Plazas Lozano
' Created          : 04-03-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region


Public Interface IElectronicsRIPSRepository
    Inherits IRepository(Of ElectronicsRIPS)

    Function QueryElectronicsRIPSInvalidToRetry(take As Integer) As List(Of ElectronicsRIPS)

    Function GetValidElectronicRIPS(documentNumber As String, entityName As String) As ElectronicsRIPS
End Interface
