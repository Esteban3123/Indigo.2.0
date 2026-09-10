'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
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
Public Interface ISequenseContractDRepository
    Inherits IRepository(Of ContractSequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As ContractSequenceDetail

#End Region

End Interface