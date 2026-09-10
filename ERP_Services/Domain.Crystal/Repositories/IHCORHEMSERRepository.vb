'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Hector Rodriguez
' Created          : 23-07-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCORHEMSERRepository
    Inherits IRepository(Of HCORHEMSER)

    Function GetHCORHEMSERByID(id As Integer) As HCORHEMSER

End Interface