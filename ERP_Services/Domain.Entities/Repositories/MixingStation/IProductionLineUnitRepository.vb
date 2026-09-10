'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 21/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Public Interface IProductionLineUnitRepository
    Inherits IRepository(Of ProductionLineUnitDoseType)

    Function ListAllProductionLineUnitDoseType(Id_ProductionLine As Integer) As List(Of Tuple(Of Integer, String))

End Interface