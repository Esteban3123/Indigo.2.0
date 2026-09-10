'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IInteropCostSequenceRepository
    Inherits IRepository(Of InteropCostSecuence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia por id del frontal
    ''' </summary>
    Function GetSequenseByIdForm(ByVal idForm As String) As InteropCostSecuence

#End Region

End Interface