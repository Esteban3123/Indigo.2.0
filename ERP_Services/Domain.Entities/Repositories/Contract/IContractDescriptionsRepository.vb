'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2019
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IContractDescriptionsRepository
    Inherits IRepository(Of ContractDescriptions)

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractDescriptions(code As String) As ContractDescriptions

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractDescriptionsById(id As Integer) As ContractDescriptions

    ''' <summary>
    ''' Obtiene una lista de descripciones por codigos
    ''' </summary>
    ''' <param name="ListCode"></param>
    ''' <returns></returns>
    Function GetListContractDescriptions(ListCode As List(Of String)) As List(Of ContractDescriptions)

End Interface
