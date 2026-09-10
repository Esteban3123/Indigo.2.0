'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class BillingConceptRepository
    Inherits GenericRepository(Of BillingConcept)
    Implements IBillingConceptRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene un concepto de facturacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetBillingConceptById(Id As Integer, Optional tracking As Boolean = True) As BillingConcept Implements IBillingConceptRepository.GetBillingConceptById
        Dim res As BillingConcept

        If tracking Then
            res = (From bc In _context.BillingConcept Where bc.Id = Id Select bc).FirstOrDefault()
        Else
            res = (From bc In _context.BillingConcept.AsNoTracking() Where bc.Id = Id Select bc).FirstOrDefault()
        End If

        If res IsNot Nothing Then
            If res.IVAId IsNot Nothing Then
                res.PercentageIVA = (From i In _context.GeneralLedgerIVA Where i.Id = res.IVAId Select i.Percentage).FirstOrDefault()
            End If

            If res.WithholdingTaxConceptId IsNot Nothing Then
                res.RetentionPercentageTax = (From r In _context.RetentionConcepts Where r.Id = res.WithholdingTaxConceptId Select r.Rate).FirstOrDefault()
            End If

            If res.WithholdingICAConceptId IsNot Nothing Then
                res.RetentionPercentageICA = (From r In _context.RetentionConcepts Where r.Id = res.WithholdingICAConceptId Select r.Rate).FirstOrDefault()
            End If

            Return res
        End If
        Return New BillingConcept
    End Function
End Class
