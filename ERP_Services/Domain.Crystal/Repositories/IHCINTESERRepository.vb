'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCINTESERRepository
    Inherits IRepository(Of HCINTESER)

    Function GetHCINTESERByCodserIpsAndCentAten(codserips As String, codCentAten As String) As HCINTESER

End Interface