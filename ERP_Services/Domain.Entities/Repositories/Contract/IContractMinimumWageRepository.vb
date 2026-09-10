'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 16/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IContractMinimumWageRepository
    Inherits IRepository(Of ContractMinimumWage)

    ''' <summary>
    ''' Obtiene un salirio minimo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractMinimumWage(code As String) As ContractMinimumWage

    ''' <summary>
    ''' Obtiene un salrio por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractMinimumWageById(id As Integer) As ContractMinimumWage
End Interface
