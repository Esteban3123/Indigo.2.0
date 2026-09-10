'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 07-12-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IINCUPSIPSRepository
    Inherits IRepository(Of INCUPSIPS)

    Function GetINCUPSIPSByCODSERIPS(CODSERIPS As String) As INCUPSIPS

End Interface