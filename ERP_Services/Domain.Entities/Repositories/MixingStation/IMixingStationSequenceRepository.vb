'************************************************************
' Assembly         : Domain.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 24/04/2019
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
Public Interface IMixingStationSequenceRepository
    Inherits IRepository(Of MixingStationSequence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Function GetSequenceByIdForm(ByVal idForm As String) As MixingStationSequence

#End Region

End Interface