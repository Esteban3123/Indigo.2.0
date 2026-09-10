'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IContractTypeRepository
    Inherits IRepository(Of ContractType)

    ''' <summary>
    ''' Lista todos los Tipos de Contrato
    ''' </summary>
    ''' <returns>Tipos de Contrato</returns>
    ''' <remarks></remarks>
    Function ListAllContractType() As List(Of ContractType)

    ''' <summary>
    ''' Obtiene un Tipo de Contrato
    ''' </summary>
    ''' <param name="code">Código del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractType(ByVal code As String, Optional desatach As Boolean = True) As ContractType

    ''' <summary>
    ''' Obtiene un Tipo de Contrato por ID
    ''' </summary>
    ''' <param name="code">Id del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractTypeById(ByVal ID As String) As ContractType

End Interface
