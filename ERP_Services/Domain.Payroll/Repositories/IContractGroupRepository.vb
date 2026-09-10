'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IContractGroupRepository
    Inherits IRepository(Of ContractGroup)

    ''' <summary>
    ''' Lista todos los grupos de contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllContractGroup() As List(Of ContractGroup)

    ''' <summary>
    ''' Obtiene un Grupos de contrato especifico
    ''' </summary>
    ''' <param name="code">Codigo del Grupo de contrato</param>
    ''' <returns>Grupo de contrato</returns>
    ''' <remarks></remarks>
    Function GetContractGroup(ByVal code As String, Optional tracking As Boolean = True) As ContractGroup

End Interface
