'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IContractEntityRepository
    Inherits IRepository(Of ContractEntity)

    ''' <summary>
    ''' Obtiene una entidad de contrato por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractEntity(code As String) As ContractEntity

    ''' <summary>
    ''' Obtiene una entidad de contrato por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractEntityById(id As Integer) As ContractEntity

End Interface
