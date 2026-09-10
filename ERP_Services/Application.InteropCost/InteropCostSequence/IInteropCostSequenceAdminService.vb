'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' define los servicios disponibles para todas las operaciones
''' con las secuencias numericas para el modulo de costos
''' </summary>
Public Interface IInteropCostSequenceAdminService
    Inherits IDisposable

#Region "Methods"

    Function SaveSequence(ByVal seq As InteropCostSecuence) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Function GetNumericSequenseGroupById(ByVal id As Int32) As List(Of String)

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia por id del frontal
    ''' </summary>
    Function GetSequenseByIdForm(ByVal idForm As String) As InteropCostSecuence

#End Region

End Interface