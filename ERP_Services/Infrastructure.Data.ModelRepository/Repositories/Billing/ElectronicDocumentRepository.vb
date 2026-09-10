'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-10-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Configuration

#End Region

Public Class ElectronicDocumentRepository
    Inherits GenericRepository(Of ElectronicDocument)
    Implements IElectronicDocumentRepository

#Region "Fields"

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un listado de facturas que no generaron documento electronico
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInvoicesWithoutElectronicDocument() As List(Of Invoice) Implements IElectronicDocumentRepository.GetInvoicesWithoutElectronicDocument
        Dim res = (From i As Invoice In Me._context.Invoice
                   Join ba As BillingAuthorization In Me._context.BillingAuthorization.AsNoTracking
                       On i.BillingAuthorizationId Equals ba.Id
                   Where ba.InvoiceType = 3 And Not Me._context.ElectronicDocument.Any(Function(ed) ed.EntityName = "Invoice" AndAlso ed.EntityId = i.Id)
                   Select i).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of Invoice)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetElectronicDocumentById(Id As Integer, Optional tracking As Boolean = True) As ElectronicDocument Implements IElectronicDocumentRepository.GetElectronicDocumentById
        Dim res As ElectronicDocument
        If tracking = True Then
            res = (From a As ElectronicDocument _
                   In Me._context.ElectronicDocument
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        Else
            res = (From a As ElectronicDocument _
                   In Me._context.ElectronicDocument.AsNoTracking()
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New ElectronicDocument
        End If
    End Function

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id de la factura
    ''' </summary>
    ''' <param name="InvoiceId">The identifier.</param>
    ''' <returns></returns>
    Public Function GetElectronicDocumentByInvoiceId(InvoiceId As Integer, Optional tracking As Boolean = True) As ElectronicDocument Implements IElectronicDocumentRepository.GetElectronicDocumentByInvoiceId
        Dim res As ElectronicDocument
        If tracking = True Then
            res = (From a As ElectronicDocument In Me._context.ElectronicDocument
                   Where a.EntityId = InvoiceId AndAlso a.EntityName = "Invoice"
                   Select a).FirstOrDefault()
        Else
            res = (From a As ElectronicDocument In Me._context.ElectronicDocument.AsNoTracking()
                   Where a.EntityId = InvoiceId AndAlso a.EntityName = "Invoice"
                   Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New ElectronicDocument
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado de documento electronico de acuerdo con el estado en que se encuentren
    ''' </summary>
    ''' <param name="status">The identifier.</param>
    ''' <returns></returns>
    Public Function GetElectronicDocumentIdsByStatus(ByVal status As Integer()) As List(Of Integer) Implements IElectronicDocumentRepository.GetElectronicDocumentIdsByStatus
        Dim res = (From ed As ElectronicDocument _
                       In Me._context.ElectronicDocument
                   Where status.Contains(ed.Status)
                   Select ed.Id).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of Integer)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado de documentos electronicos de acuerdo con el estado en que se encuentren
    ''' </summary>
    ''' <param name="status">Lista de enteros para consultar.</param>
    ''' <returns></returns>
    Public Function GetListElectronicDocumentIdsByStatus(ByVal status As Integer()) As List(Of ElectronicDocument) Implements IElectronicDocumentRepository.GetListElectronicDocumentIdsByStatus
        Dim dateBilling = GetFechaMinima()
        Dim res = (From ed As ElectronicDocument _
                In Me._context.ElectronicDocument
                   Where status.Contains(ed.Status) And ed.CreationDate >= dateBilling
                   Select ed).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of ElectronicDocument)()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el mas reciente documento electronico de acuerdo con el origen
    ''' </summary>
    ''' <param name="entityId">Id del documento electronico a consultar.</param>
    ''' <param name="entityName">Tipo del documento electronico a consultar.</param>
    ''' <returns></returns>
    Public Function GetLastElectronicDocuments(ByVal entityId As Integer, ByVal entityName As String) As ElectronicDocument Implements IElectronicDocumentRepository.GetLastElectronicDocuments
        Dim res = (From a As ElectronicDocument In Me._context.ElectronicDocument.AsNoTracking()
                   Where a.EntityId = entityId AndAlso a.EntityName = entityName
                   Order By a.Id Descending
                   Select a).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New ElectronicDocument()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la fecha mínima para el procesamiento de facturas desde configuración o una fecha por defecto.
    ''' </summary>
    ''' <returns>Fecha mínima de corte</returns>
    Public Function GetFechaMinima() As DateTime
        Dim defaultMindate As New DateTime(2025, 6, 23, 0, 0, 0)
        Dim minDateStr As String = ConfigurationManager.AppSettings("ElectronicDocumentMinDate")

        If DateTime.TryParse(minDateStr, Nothing) Then
            Return DateTime.Parse(minDateStr)
        End If
        Return defaultMindate
    End Function

    ''' <summary>
    ''' Lista todas las facturas electronicas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeInvoices(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IElectronicDocumentRepository.ListElectronicDocumentsTypeInvoices
        Dim documentTypes = {98, 99}
        Dim res = (From ed As ElectronicDocument _
                In Me._context.ElectronicDocument.AsNoTracking().Include("ElectronicDocumentDetail").AsNoTracking()
                   Where ed.OperatingUnitId = OperatingUnitId AndAlso Not documentTypes.Contains(ed.DocumentType) AndAlso (StatusId Is Nothing OrElse ed.Status = StatusId)
                   Select ed).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of ElectronicDocument)()
        End If
    End Function

    ''' <summary>
    ''' Lista todas las notas debito
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeDebitNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IElectronicDocumentRepository.ListElectronicDocumentsTypeDebitNotes
        Dim res = (From ed As ElectronicDocument _
                In Me._context.ElectronicDocument.AsNoTracking().Include("ElectronicDocumentDetail").AsNoTracking()
                   Where ed.OperatingUnitId = OperatingUnitId AndAlso ed.DocumentType = 98 AndAlso (StatusId Is Nothing OrElse ed.Status = StatusId)
                   Select ed).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of ElectronicDocument)()
        End If
    End Function

    ''' <summary>
    ''' Lista todas las notas credito
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeCreditNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IElectronicDocumentRepository.ListElectronicDocumentsTypeCreditNotes
        Dim res = (From ed As ElectronicDocument _
                In Me._context.ElectronicDocument.AsNoTracking().Include("ElectronicDocumentDetail").AsNoTracking()
                   Where ed.OperatingUnitId = OperatingUnitId AndAlso ed.DocumentType = 99 AndAlso (StatusId Is Nothing OrElse ed.Status = StatusId)
                   Select ed).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return New List(Of ElectronicDocument)()
        End If
    End Function

#End Region

End Class
