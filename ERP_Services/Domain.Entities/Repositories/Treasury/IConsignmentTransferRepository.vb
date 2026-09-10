'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base

Public Interface IConsignmentTransferRepository
    Inherits IRepository(Of Consignment)

    Function ListConsignmentMassiveConfirm(listDocuments As List(Of String)) As List(Of Consignment)

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por id
    ''' </summary>
    Function GetConsignmentTransferById(ByVal Id As Integer, Optional tracking As Boolean = False) As Consignment

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetConsignmentTransfer(ByVal code As String, Optional tracking As Boolean = False) As Consignment

    Function SP_ReverseConsignment(TreasuryNoteId As Integer, UserCode As String) As List(Of SP_ReverseConsignment_Result)

End Interface