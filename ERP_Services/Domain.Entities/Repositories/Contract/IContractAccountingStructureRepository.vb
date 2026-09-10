'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IContractAccountingStructureRepository
    Inherits IRepository(Of ContractAccountingStructure)

    ''' <summary>
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractAccountingStructure(code As String) As ContractAccountingStructure

    ''' <summary>
    ''' Obtiene un grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractAccountingStructureById(id As Integer) As ContractAccountingStructure

End Interface
