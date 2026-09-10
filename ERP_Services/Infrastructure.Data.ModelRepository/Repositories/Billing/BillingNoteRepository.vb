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

#End Region

Public Class BillingNoteRepository
    Inherits GenericRepository(Of BillingNote)
    Implements IBillingNoteRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetBillingNoteById(Id As Integer, Optional tracking As Boolean = True) As BillingNote Implements IBillingNoteRepository.GetBillingNoteById
        Dim res As BillingNote
        If tracking = True Then
            res = (From a As BillingNote _
                   In Me._context.BillingNote
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        Else
            res = (From a As BillingNote _
                   In Me._context.BillingNote.AsNoTracking()
                   Where a.Id = Id
                   Select a).FirstOrDefault()
        End If
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From b In _context.BillingNote.AsNoTracking() Where b.Id = Id Select b).FirstOrDefault()
            Return res
        Else
            Return New BillingNote
        End If
    End Function

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id con agregados
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetBillingNoteByIdWithAggregates(Id As Integer) As BillingNote Implements IBillingNoteRepository.GetBillingNoteByIdWithAggregates
        Dim res As BillingNote = (From a As BillingNote _
            In Me._context.BillingNote.
                                      Include("BillingNoteDetail").
                                      Include("BillingNoteDetail.Invoice").
                                      Include("BillingNoteDetail.BillingNoteDetailTax")
                                  Where a.Id = Id
                                  Select a).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then

            Dim noteTypeDetailQuery = Me.GetPortfolioNoteTypeDetailByBillingNoteId(Id)
            For Each detail In res.BillingNoteDetail
                detail.NoteTypeDetails = noteTypeDetailQuery?.FindAll(Function(x) x.InvoiceId = detail.InvoiceId)
                Dim electronicDocument = (From ed In _context.ElectronicDocument.AsNoTracking() Where ed.EntityId = detail.InvoiceId And ed.EntityName = "Invoice").FirstOrDefault()
                If electronicDocument Is Nothing Or detail.ConceptId = 7 Then
                    detail.DianVersion = 0
                Else
                    detail.DianVersion = electronicDocument.DianVersion
                    detail.Status = electronicDocument.Status
                End If
            Next
            Return res
        Else
            Return New BillingNote
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="billingNoteId"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteTypeDetailByBillingNoteId(billingNoteId As Integer) As List(Of NoteTypeDetail)

        If billingNoteId = 0 Then
            Return Nothing
        End If
        Dim parmas As List(Of (String, Object)) = New List(Of (String, Object)) From {("@BillingNoteId", billingNoteId)}

        Dim query = Me.ExecuteQueryDR(Of NoteTypeDetail)("	
	                SELECT 
						id.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId, 
						p.Code, 
						p.Name, 
						p.CodeAlternative,
						p.CodeAlternativeTwo,
						bg.Code + ' - ' + bg.Name BillingGroup,
						1 AS InvoicedQuantity, 
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code as TaxCode,
						NULL NameAlternative,
						NULL NameAlternativeTwo
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
						JOIN Inventory.InventoryProduct p WITH(NOLOCK) ON sod.ProductId = p.Id
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
						LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
						LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
						LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, p.BillingGroupId, cups.BillingGroupId)
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =2 and bn.Id =@BillingNoteId

					UNION ALL

					SELECT 
						id.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId,  
						ips.Code, 
						ips.Name, 
						ce.Code CodeAlternative,
						ce.RIPSCode CodeAlternativeTwo,
						bg.Code + ' - ' + bg.Name BillingGroup,
						1 AS InvoicedQuantity,
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code TaxCode,
						ce.Description NameAlternative,
						ce.RIPSDescription NameAlternativeTwo
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetail' and pnarad.EntityId = id.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id=id.ServiceOrderDetailId
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
						JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = sod.IPSServiceId
						JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId 
						JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ce.BillingGroupId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =1 and  bn.Id =@BillingNoteId

                    UNION ALL
                    
                   SELECT 
						ids.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId,  
						ips.Code, 
						ips.Name, 
						NULL CodeAlternative,
						NULL CodeAlternativeTwo,
						NULL BillingGroup,
						1 AS InvoicedQuantity,
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code TaxCode,
						NULL NameAlternative,
						NULL NameAlternativeTwo
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.InvoiceDetailSurgical ids WITH(NOLOCK) on pnarad.EntityName ='InvoiceDetailSurgical' and pnarad.EntityId = ids.Id
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on ids.InvoiceDetailId = id.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on id.ServiceOrderDetailId=sod.Id
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =id.InvoiceId
						JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE pn.NoteType = 6 and sod.RecordType =1 and bn.Id =@BillingNoteId
            
                    UNION ALL

					SELECT 
						ids.Id,
						id.Id as InvoiceDetailId,
						i.Id as InvoiceId,
						bnd.Id as BillingNoteDetailId,  
						ips.Code, 
						ips.Name, 
						NULL CodeAlternative,
						NULL CodeAlternativeTwo,
						NULL BillingGroup,
						1 AS InvoicedQuantity,
						pnarad.BaseValue AS UnitValue, 
						pnarad.BaseValue, 
						pnarad.Value TotalAdjustmentValue,
						COALESCE(pnarad.TaxValue,0) TaxValue, 
						COALESCE(pnarad.TaxPercentage,0) TaxPercentage, 
						tax.Code TaxCode,
						NULL NameAlternative,
						NULL NameAlternativeTwo
					FROM Billing.BillingNote bn WITH (NOLOCK)
						JOIN Billing.BillingNoteDetail bnd WITH (NOLOCK) ON bn.Id = bnd.BillingNoteId
						JOIN Portfolio.PortfolioNote pn WITH (NOLOCK) ON pn.Id = bn.EntityId AND 'PortfolioNote' = bn.EntityName
						JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pn.Id = pnara.PortfolioNoteId
						JOIN Portfolio.PortfolioNoteAccountReceivableDetail pnarad WITH(NOLOCK) ON pnara.Id = pnarad.PortfolioNoteAccountReceivableId
						JOIN Billing.ServiceOrderDetailSurgical ids WITH(NOLOCK) on pnarad.EntityName ='ServiceOrderDetailSurgical' and pnarad.EntityId = ids.Id
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on ids.ServiceOrderDetailId=sod.Id
						JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on ar.Id = pnara.AccountReceivableId
						JOIN Billing.Invoice i WITH(NOLOCK) on i.Id =ar.InvoiceId
						JOIN Billing.InvoiceDetail id WITH(NOLOCK) on i.Id=id.InvoiceId and id.ServiceOrderDetailId=sod.Id
						JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = ids.IPSServiceId
						LEFT JOIN GeneralLedger.GeneralLedgerIVA tax WITH(NOLOCK) ON tax.Id = pnarad.TaxId
					WHERE (pn.NoteType = 6 or ISNULL(pn.EntityName,'')='Glosas') and sod.RecordType =1 and bn.Id =@BillingNoteId

            ", parmas)?.ToList()

        If query?.Any() Then
            Return query
        Else
            Return Nothing
        End If
    End Function


End Class
