'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-6-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class InvoiceEntityCapitedRepository
    Inherits GenericRepository(Of InvoiceEntityCapitated)
    Implements IInvoiceEntityCapitedRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una factura de entidad capitada por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetInvoiceEntityCapitated(code As String) As InvoiceEntityCapitated Implements IInvoiceEntityCapitedRepository.GetInvoiceEntityCapitated
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From i In _context.InvoiceEntityCapitated.Include("Invoice").Include("CareGroup").Include("Currency") Where i.Code.Equals(code) Select i).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.FullNameCareGroup = query.CareGroup.Code + " - " + query.CareGroup.Name
            query.CodeNameCategory = (From c In _context.InvoiceCategories.AsNoTracking() Where c.Id = query.InvoiceCategoryId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()

            If query.PreviousRIPSInvoice IsNot Nothing AndAlso query.PreviousRIPSInvoice > 0 Then

                Dim queryPrevious = (From c In _context.InvoiceEntityCapitated.AsNoTracking().Include("Invoice").AsNoTracking()
                                     Where c.Id = query.PreviousRIPSInvoice
                                     Select c).FirstOrDefault()
                query.PreviousInvoiceNumberRips = $"{queryPrevious?.Invoice?.InvoiceNumber}"
            End If

            query.OriginalValue = query
            Return query
        Else
            Return New InvoiceEntityCapitated()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetInvoiceEntityCapitatedById(Id As Integer) As InvoiceEntityCapitated Implements IInvoiceEntityCapitedRepository.GetInvoiceEntityCapitatedById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From i In _context.InvoiceEntityCapitated Where i.Id = Id Select i).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From i In _context.InvoiceEntityCapitated.AsNoTracking() Where i.Id = Id Select i).FirstOrDefault()
            Return query
        Else
            Return New InvoiceEntityCapitated()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un listado de facturas de tipo de registros de servicio dentro de un periodo y grupo de atención establecido
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="careGroupId">Grupo de atención</param>
    ''' <returns></returns>
    Public Async Function GetCapitationControlRegistry(initialDate As DateTime, finalDate As DateTime, careGroupId As Integer) As Task(Of List(Of InvoiceDetail)) Implements IInvoiceEntityCapitedRepository.GetCapitationControlRegistry
        Dim query = Await (From id In _context.InvoiceDetail.AsNoTracking()
                           Join i In _context.Invoice.AsNoTracking() On id.InvoiceId Equals i.Id
                           Join cg In _context.CareGroup.AsNoTracking() On cg.Id Equals i.CareGroupId
                           Where i.CareGroupId = careGroupId And i.InvoiceDate >= initialDate And i.InvoiceDate <= finalDate And i.DocumentType = 5 And i.Status = 1
                           Select id).ToListAsync()
        Return query
    End Function

    ''' <summary>
    ''' Guarda, Actualiza o Confirma una factura de monto fijo
    ''' </summary>
    ''' <param name="invoiceEntityCapitatedXml">Xml de la factura</param>
    ''' <param name="userCode">Código del usuario</param>
    ''' <returns></returns>
    Public Function SP_SaveInvoiceEntityCapitated(invoiceEntityCapitatedXml As String, userCode As String) As SP_SaveInvoiceEntityCapitated_Result Implements IInvoiceEntityCapitedRepository.SP_SaveInvoiceEntityCapitated
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveInvoiceEntityCapitated(invoiceEntityCapitatedXml, userCode).SingleOrDefault()
    End Function

End Class