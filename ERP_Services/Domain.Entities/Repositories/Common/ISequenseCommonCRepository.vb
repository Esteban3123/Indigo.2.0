'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Maintenance.Entities
Imports Domain.Base
Imports Domain.Entities
#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface ISequenseCommonCRepository
    Inherits IRepository(Of CommonSequence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Function GetSequenseByIdForm(ByVal idForm As String) As CommonSequence

#End Region

End Interface