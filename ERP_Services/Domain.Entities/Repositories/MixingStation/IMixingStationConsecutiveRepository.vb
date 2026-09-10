'************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 01-12-2021
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del repositorio Consecutivos.
''' </summary>
Public Interface IMixingStationConsecutiveRepository
    Inherits IRepository(Of MixingStationConsecutive)

End Interface
