'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IContractRepository
    Inherits IRepository(Of Contract)

    ''' <summary>
    ''' Gets the contract by identifier with aggregates.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetContractByIdWithAggregates(id As Integer) As Contract

    ''' <summary>
    ''' Obtiene un contrato por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContract(code As String) As Contract

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractById(id As Integer) As Contract

    ''' <summary>
    ''' Obtiene un contrato con el id del caregroup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractByCareGroupIdForMedicalFees(CareGroupId As Integer) As Contract

    ''' <summary>
    ''' Proceso de contratos
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_SaveContract(xmlData As String, codeUser As String) As SP_SaveContract_Result

End Interface
