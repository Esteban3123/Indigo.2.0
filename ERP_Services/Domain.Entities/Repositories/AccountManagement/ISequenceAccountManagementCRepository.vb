
'************************************************************
' Assembly         : Domain.Entities
' Author           : Felix Camilo Salazar Roldan
' Created          : 13-12-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IAccountManagementSequenceRepository
    Inherits IRepository(Of AccountManagementSequence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Function GetSequenseByIdForm(ByVal idForm As String) As AccountManagementSequence

#End Region
End Interface

