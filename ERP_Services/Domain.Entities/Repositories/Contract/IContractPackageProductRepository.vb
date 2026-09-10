'************************************************************
' Assembly         : Domain.Contract
' Author           : Diego A. Roldán
' Created          : 2021-08-24
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base

Public Interface IContractPackageProductRepository
    Inherits IRepository(Of ContractPackageProduct)

    Function GetAllContractPackageProductByContractPackageId(contractPackageId As Integer, Optional tracking As Boolean = True) As List(Of ContractPackageProduct)

End Interface
