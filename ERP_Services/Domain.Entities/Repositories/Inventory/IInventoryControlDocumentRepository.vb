'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 16-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Entities

Public Interface IInventoryControlDocumentRepository
    Inherits IRepository(Of InventoryControlDocument)

    ''' <summary>
    ''' Obtiene un registro de control de inventarios por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInventoryControlDocumentById(Id As Integer) As InventoryControlDocument

    ''' <summary>
    ''' Obtiene un registro de control de inventarios por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    <Obsolete>
    Function GetInventoryControlDocumentByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As InventoryControlDocument

    ''' <summary>
    ''' Obtiene un registro de control de inventarios por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Function GetInventoryControlDocumentByDocumentNumberAsync(DocumentNumber As String, Optional DocumentType As Integer = 0) As Task(Of InventoryControlDocument)

End Interface
