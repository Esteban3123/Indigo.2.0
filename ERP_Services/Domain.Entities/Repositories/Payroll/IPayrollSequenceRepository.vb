'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan Carlos Bermudez
' Created          : 15-07-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IPayrollSequenceRepository
    Inherits IRepository(Of PayrollSequence)

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Function GetSequenseByIdForm(ByVal idForm As String) As PayrollSequence

End Interface
