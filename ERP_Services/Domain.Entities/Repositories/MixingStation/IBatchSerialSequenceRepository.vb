'************************************************************
' Assembly         : Domain.MixingStation
' Author           : Giovanny Plazas L
' Created          : 06-06-2022
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del repositorio Consecutivos de lotes
''' </summary>
Public Interface IBatchSerialSequenceRepository
    Inherits IRepository(Of BatchSerialSequence)

End Interface
