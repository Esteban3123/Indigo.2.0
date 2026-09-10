'************************************************************
' Assembly         : Domain.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-10-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IElectronicDocumentRepository
    Inherits IRepository(Of ElectronicDocument)

    ''' <summary>
    ''' Obtiene un listado de facturas que no generaron documento electronico
    ''' </summary>
    ''' <returns></returns>
    Function GetInvoicesWithoutElectronicDocument() As List(Of Invoice)

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetElectronicDocumentById(ByVal Id As Integer, Optional tracking As Boolean = True) As ElectronicDocument

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id de la factura
    ''' </summary>
    ''' <param name="InvoiceId">The identifier.</param>
    ''' <returns></returns>
    Function GetElectronicDocumentByInvoiceId(ByVal InvoiceId As Integer, Optional tracking As Boolean = True) As ElectronicDocument

    ''' <summary>
    ''' Obtiene un listado de documento electronico de acuerdo con el estado en que se encuentren
    ''' </summary>
    ''' <param name="status">The identifier.</param>
    ''' <returns></returns>
    Function GetElectronicDocumentIdsByStatus(ByVal status As Integer()) As List(Of Integer)

    ''' <summary>
    ''' Lista todas las facturas electronicas
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Function ListElectronicDocumentsTypeInvoices(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument)

    ''' <summary>
    ''' Lista todas las notas debitos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Function ListElectronicDocumentsTypeDebitNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument)

    ''' <summary>
    ''' Lista todas las notas creditos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Function ListElectronicDocumentsTypeCreditNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument)

    ''' <summary>
    ''' Obtiene una lista de documentos electronicos por estado
    ''' </summary>
    ''' <param name="status">Lista de enteros para consultar.</param>
    ''' <returns></returns>
    Function GetListElectronicDocumentIdsByStatus(ByVal status As Integer()) As List(Of ElectronicDocument)

    ''' <summary>
    ''' Obtiene el mas reciente documento electronico de acuerdo con el origen
    ''' </summary>
    ''' <param name="entityId">Id del documento electronico a consultar.</param>
    ''' <param name="entityName">Tipo del documento electronico a consultar.</param>
    ''' <returns></returns>
    Function GetLastElectronicDocuments(ByVal entityId As Integer, ByVal entityName As String) As ElectronicDocument

End Interface
