'************************************************************
' Assembly         : Domain.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 24-04-2019
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface IMixingStationSequenceDetailRepository
    Inherits IRepository(Of MixingStationSequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenceDById(ByVal id As Int32) As MixingStationSequenceDetail

    ''' <summary>
    ''' Obtiene la secuencia numerica primero haciendo el update para bloquear la tabla y no hayan problemas de concurrencia
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetSequenceDetailUpdatedById(ByVal id As Int32) As MixingStationSequenceDetail

#End Region

End Interface