'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface ICostSequenceRepository
    Inherits IRepository(Of CostSecuence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la cabecera de la secuencia por id del frontal
    ''' </summary>
    Function GetSequenseByIdForm(ByVal idForm As String) As CostSecuence

#End Region

End Interface