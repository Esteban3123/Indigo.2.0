'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 30-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IInventoryRequestRepository
    Inherits IRepository(Of InventoryRequest)

    ''' <summary>
    ''' Obtiene una solicitud de inventario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryRequestByCode(code As String) As InventoryRequest

    ''' <summary>
    ''' Obtiene una solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryRequestById(id As Integer) As InventoryRequest

    ''' <summary>
    ''' CopyPaste/Import solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Function SP_CopyPasteAndImportRequests(xml As String) As List(Of SP_CopyPasteAndImportRequests_Result)

    ''' <summary>
    ''' CopyPaste/Import solicitudes medicamentos,insumos,otros
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Function SP_CopyPasteAndImportRequestsOtherDetail(xml As String) As List(Of SP_CopyPasteAndImportRequestsOtherDetail_Result)

End Interface
