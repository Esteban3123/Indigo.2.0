'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCFARMEPCRepository
    Inherits IRepository(Of HCFARMEPC)

    Function GetHCFARMEPCByConsecu(consecutive As Decimal) As HCFARMEPC

End Interface