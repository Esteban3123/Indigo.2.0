'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 05-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Public Interface ICMMixingProducitonLineRepository
    Inherits IRepository(Of CMMixingProducitonLine)

    Function ListAllCMMixingProducitonLine(Id_MixingStation As Integer) As List(Of Tuple(Of Integer, String))

End Interface
