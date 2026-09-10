'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Giovanny Plazas Lozano
' Created          : 04-03-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Base
#End Region

Public Class ElectronicsRIPSRepository
    Inherits GenericRepository(Of ElectronicsRIPS)
    Implements IElectronicsRIPSRepository

    Private _context As IGlobalModelUnitOfWork
    Private ReadOnly _officialDateToRelease As Date

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
        _officialDateToRelease = New Date(2026, 1, 1)
    End Sub

    Public Function QueryElectronicsRIPSInvalidToRetry(take As Integer) As List(Of ElectronicsRIPS) Implements IElectronicsRIPSRepository.QueryElectronicsRIPSInvalidToRetry

        Dim query = (From rip In _context.ElectronicsRIPS.Include("ElectronicsProperties").AsNoTracking()
                     Where {EStatusERIPS.ValidateWrong, EStatusERIPS.Register}.Contains(rip.ElectronicsProperties.StatusRIPS) _
                         AndAlso rip.Retry <= 5 AndAlso rip.CreationDate >= _officialDateToRelease
                     Select rip)?.Take(take)

        If query.Any() Then
            Dim electronicsRipsInvoice = New List(Of ElectronicsRIPS)
            Dim electronicsRipsNotes = New List(Of ElectronicsRIPS)

            If query.Any(Function(x) x.ElectronicsProperties.EntityName = NameOf(EEntityNameERIPS.Invoice)) Then

                Dim listInvoiceIds = query.Where(Function(x) x.ElectronicsProperties.EntityName = NameOf(EEntityNameERIPS.Invoice)).Select(Function(d) d.ElectronicsProperties.EntityId)?.ToList()

                Dim invoices = (From i In _context.Invoice.AsNoTracking()
                                Join num In listInvoiceIds On i.Id Equals num
                                Select i).ToList()

                electronicsRipsInvoice = (From i In invoices
                                          Join q In query.Where(Function(e) e.ElectronicsProperties.EntityName = NameOf(EEntityNameERIPS.Invoice)).ToList() On i.Id Equals q.ElectronicsProperties.EntityId
                                          Select New ElectronicsRIPS With {.Id = q.Id,
                                                .ElectronicsPropertiesId = q.ElectronicsPropertiesId,
                                                .RadicateDate = q.RadicateDate,
                                                .sendDate = q.sendDate,
                                                .Retry = q.Retry,
                                                .CosmoDBId = q.CosmoDBId,
                                                .FilePath = q.FilePath,
                                                .CreationUser = q.CreationUser,
                                                .CreationDate = q.CreationDate,
                                                .ModificationUser = q.ModificationUser,
                                                .ModificationDate = q.ModificationDate,
                                                .DocumentNumber = i.InvoiceNumber,
                                                .EntityId = i.Id,
                                                .EntityName = q.ElectronicsProperties.EntityName,
                                                .DocumentType = i.DocumentType})?.ToList()
            End If

            If query.Any(Function(x) x.ElectronicsProperties.EntityName = NameOf(EEntityNameERIPS.BillingNote)) Then

                Dim listNoteIds = query.Where(Function(x) x.ElectronicsProperties.EntityName = NameOf(EEntityNameERIPS.BillingNote)).Select(Function(d) d.ElectronicsProperties.EntityId)?.ToList()

                Dim notes = (From i In _context.BillingNote.AsNoTracking()
                             Join num In listNoteIds On i.Id Equals num
                             Select i).ToList()

                electronicsRipsNotes = (From i In notes
                                        Join q In query.Where(Function(e) e.ElectronicsProperties.EntityName = NameOf(EEntityNameERIPS.BillingNote)).ToList() On i.Id Equals q.ElectronicsProperties.EntityId
                                        Select New ElectronicsRIPS With {.Id = q.Id,
                                                .ElectronicsPropertiesId = q.ElectronicsPropertiesId,
                                                .RadicateDate = q.RadicateDate,
                                                .sendDate = q.sendDate,
                                                .Retry = q.Retry,
                                                .CosmoDBId = q.CosmoDBId,
                                                .FilePath = q.FilePath,
                                                .CreationUser = q.CreationUser,
                                                .CreationDate = q.CreationDate,
                                                .ModificationUser = q.ModificationUser,
                                                .ModificationDate = q.ModificationDate,
                                                .EntityId = i.Id,
                                                .DocumentNumber = i.Code,
                                                .EntityName = q.ElectronicsProperties.EntityName,
                                                .DocumentType = i.GetDocumentType})?.ToList()
            End If

            Dim resultQuery = electronicsRipsInvoice.Union(electronicsRipsNotes).ToList()

            Return resultQuery

        Else
            Return New List(Of ElectronicsRIPS)
        End If
    End Function

    ''' <summary>
    ''' Obtiene Los RIPS electronicos Validos
    ''' </summary>
    ''' <param name="documentNumber"></param>
    ''' <param name="entityName"></param>
    ''' <returns></returns>
    Public Function GetValidElectronicRIPS(documentNumber As String, entityName As String) As ElectronicsRIPS Implements IElectronicsRIPSRepository.GetValidElectronicRIPS

        If String.IsNullOrEmpty(documentNumber) Then
            Throw New ArgumentNullException("El Numero del Documento es obligatorio para la busqueda del RIPS", NameOf(documentNumber))
        End If

        If String.IsNullOrEmpty(entityName) Then
            Throw New ArgumentNullException("El Tipo de documento es obligatorio para la busqueda del RIPS", NameOf(entityName))
        End If

        Try
            Dim parmas As List(Of (String, Object)) = New List(Of (String, Object)) From {
                ("@entityName", entityName),
                ("@documentNumber", documentNumber)
            }

            Dim query = Me.ExecuteQueryDR(Of ElectronicsRIPS)("
                /* facturas */
                select  r.*,
                        ep.CUV as CUV,
                        ep.StatusRIPS as StatusRIPS,
                        '' as BillingNoteInvoiceNumber
                from Billing.ElectronicsRIPS r WITH(NOLOCK)
                join Billing.ElectronicsProperties ep WITH(NOLOCK) on r.ElectronicsPropertiesId = ep.Id
                join Billing.Invoice i WITH(NOLOCK) on ep.EntityId = i.Id and ep.EntityName = 'Invoice'
                where ep.StatusRIPS = 2 
                  and ep.EntityName = @entityName 
                  and i.InvoiceNumber = @documentNumber
        
                UNION ALL

                /* 2. notas */
                select  r.*,
                        ep.CUV as CUV,
                        ep.StatusRIPS as StatusRIPS,
                        bnd.InvoiceNumber as BillingNoteInvoiceNumber
                from Billing.ElectronicsRIPS r WITH(NOLOCK)
                join Billing.ElectronicsProperties ep WITH(NOLOCK) on r.ElectronicsPropertiesId = ep.Id
                join Billing.BillingNote bn WITH(NOLOCK) on ep.EntityId = bn.Id and ep.EntityName = 'BillingNote'
                join Billing.BillingNoteDetail bnd WITH(NOLOCK) on bn.Id = bnd.BillingNoteId
                where ep.StatusRIPS = 2 
                  and ep.EntityName = @entityName 
                  and bn.Code = @documentNumber
        
                UNION ALL

                /* ajuste */
                select  r.*,        
                        ep.CUV as CUV,
                        ep.StatusRIPS as StatusRIPS,
                        '' as BillingNoteInvoiceNumber
                from Billing.ElectronicsRIPS r WITH(NOLOCK)
                join Billing.ElectronicsProperties ep WITH(NOLOCK) on r.ElectronicsPropertiesId = ep.Id
                where ep.StatusRIPS = 2 
                  and ep.EntityName = @entityName 
                  and r.CosmoDBId = @documentNumber
            ", parmas)

            Return query.FirstOrDefault()

        Catch ex As Exception
            Throw New Exception($"Error en repositorio RIPS para {entityName} {documentNumber}: {ex.Message}", ex)
        End Try

    End Function

End Class