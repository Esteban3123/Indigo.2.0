'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 25-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IMixingStationSequenceAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    Function GetSequenceByIdForm(idForm As String) As MixingStationSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Function GetNumericSequenceGroupById(ByVal id As Int32) As List(Of String)

    Function SaveSequence(ByVal seq As MixingStationSequence) As ActionResult
    Function GetCurrentSequenceByIdForm(form As String, operativeUnitId As Integer) As Long

#End Region

End Interface
