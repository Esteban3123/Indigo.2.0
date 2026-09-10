'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IAnnualizedCashFlowTransferRepository
    Inherits IRepository(Of AnnualizedCashFlowTransfer)

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowTrasnferByCode(code As String, type As Integer) As AnnualizedCashFlowTransfer
    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowTransferById(id As Integer) As AnnualizedCashFlowTransfer

End Interface
