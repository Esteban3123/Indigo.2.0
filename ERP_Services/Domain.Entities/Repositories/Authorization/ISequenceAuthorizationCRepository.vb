'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface ISequenceAuthorizationCRepository
    Inherits IRepository(Of AuthorizationSequence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Function GetSequenseByIdForm(ByVal idForm As String) As AuthorizationSequence

#End Region

End Interface
