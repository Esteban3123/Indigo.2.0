'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IAnnualizedCashFlowModificationRepository
    Inherits IRepository(Of AnnualizedCashFlowModification)
    ''' <summary>
    ''' obtiene una modificacion del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowModificationByCode(code As String, type As Integer) As AnnualizedCashFlowModification
    ''' <summary>
    ''' obtiene una modificacion del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowModificationById(id As Integer) As AnnualizedCashFlowModification

End Interface
