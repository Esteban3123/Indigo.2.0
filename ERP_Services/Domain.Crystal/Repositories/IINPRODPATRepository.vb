'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IINPRODPATRepository
    Inherits IRepository(Of INPRODPAT)

    Function GetListINPRODPATByCodesINPRODPAT(listCodes As List(Of String)) As List(Of INPRODPAT)

    Function GetINPRODPATByCodeProduct(productCode As String) As List(Of INPRODPAT)

End Interface
