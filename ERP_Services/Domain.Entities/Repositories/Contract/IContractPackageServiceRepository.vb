'************************************************************
' Assembly         : Domain.Contract
' Author           : Diego A. Roldán
' Created          : 2021-08-24
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base

''' <summary>
''' Repositorio de servicios relacionados a paquetes
''' </summary>
Public Interface IContractPackageServiceRepository
    Inherits IRepository(Of ContractPackageService)

    Function GetAllContractPackageServiceByContractPackageId(contractPackageId As Integer, Optional tracking As Boolean = True) As List(Of ContractPackageService)

End Interface
