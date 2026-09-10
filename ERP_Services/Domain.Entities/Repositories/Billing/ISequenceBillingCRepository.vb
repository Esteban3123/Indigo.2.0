'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Ernesto Cordoba
' Created          : 06-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IBillingSequenceRepository
    Inherits IRepository(Of BillingSequence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Function GetSequenseByIdForm(ByVal idForm As String) As BillingSequence

#End Region
End Interface
