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

    ''' <summary>
    ''' Lista de estructuras contables activas (Status=1) ordenadas por Code.
    ''' Retorna entidades planas sin enriquecer con descripciones de MainAccounts.
    ''' </summary>
    Function GetActiveList() As List(Of ContractAccountingStructure)

    ''' <summary>
    ''' Obtiene estructuras contables activas filtradas por una lista de Codes.
    ''' Pensado para validación batch en cargas masivas (saldos iniciales).
    ''' </summary>
    Function GetListByCodes(codes As List(Of String)) As List(Of ContractAccountingStructure)

End Interface
