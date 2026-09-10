'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 29/08/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IDefectsUnitRepository
    Inherits IRepository(Of DefectsUnitDoseType)

    Function ListAllDefectsUnitDoseType(Id_Defects As Integer) As List(Of Tuple(Of Integer, String))
End Interface
