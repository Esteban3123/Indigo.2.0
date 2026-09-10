'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CardRepository
    Inherits GenericRepository(Of Cards)
    Implements ICardRepository


    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtener un registro de tarjeta
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetCard(code As String) As Cards Implements ICardRepository.GetCard
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As Cards In Me._context.Cards.Include("CardCostCenter") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As Cards In Me._context.Cards.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Dim thirdParty = (From tp In _context.ThirdParty Where tp.Id = res.IdThirdParty Select tp).FirstOrDefault()
            res.CodeNameThirdParty = thirdParty.Nit + " - " + thirdParty.Name
            Dim cashReceiptConceptCommision = (From crcc In _context.CashReceiptConcepts Where crcc.Id = res.IdCashReceiptConceptCommision Select crcc).FirstOrDefault()
            res.CodeNameCashReceiptConceptCommision = cashReceiptConceptCommision.Code + " - " + cashReceiptConceptCommision.Name
            Dim cashReceiptsConceptRFT = (From crcrtf In _context.CashReceiptConcepts Where crcrtf.Id = res.IdCashReceiptConceptRTF Select crcrtf).FirstOrDefault()
            res.CodeNameCashReceiptConceptRTF = cashReceiptsConceptRFT.Code + " - " + cashReceiptsConceptRFT.Name
            Dim cashReceiptConceptICA = (From crcica In _context.CashReceiptConcepts Where crcica.Id = res.IdCashReceiptConceptICA Select crcica).FirstOrDefault()
            res.CodeNamedCashReceiptConceptICA = cashReceiptConceptICA.Code + " - " + cashReceiptConceptICA.Name
            Dim retentionCommision = (From rc In _context.RetentionConcepts Where rc.Id = res.IdRetentionConceptCommision Select rc).FirstOrDefault()
            res.CodeNameRetentionConceptCommision = retentionCommision.Code + " - " + retentionCommision.Name
            Dim retentionICA = (From rica In _context.RetentionConcepts Where rica.Id = res.IdRetentionConceptICA Select rica).FirstOrDefault()
            res.CodeNameRetentionConceptICA = retentionICA.Code + " - " + retentionICA.Name
            Dim retentionRTF = (From rrtf In _context.RetentionConcepts Where rrtf.Id = res.IdRetentionConceptRTF Select rrtf).FirstOrDefault()
            res.CodeNameRetentionConceptRTF = retentionRTF.Code + " - " + retentionRTF.Name

            res.HandlesCostCenterCommision = (From c In _context.CashReceiptConcepts.AsNoTracking() Join m In _context.MainAccounts.AsNoTracking() On c.IdMainAccount Equals m.Id Where c.Id = res.IdCashReceiptConceptCommision Select m.HandlesCostCenter).FirstOrDefault()
            res.HandlesCostCenterRFT = (From c In _context.CashReceiptConcepts.AsNoTracking() Join m In _context.MainAccounts.AsNoTracking() On c.IdMainAccount Equals m.Id Where c.Id = res.IdCashReceiptConceptRTF Select m.HandlesCostCenter).FirstOrDefault()
            res.HandlesCostCenterICA = (From c In _context.CashReceiptConcepts.AsNoTracking() Join m In _context.MainAccounts.AsNoTracking() On c.IdMainAccount Equals m.Id Where c.Id = res.IdCashReceiptConceptICA Select m.HandlesCostCenter).FirstOrDefault()

            If res.CommisionCostCenterId IsNot Nothing Then
                res.CodeNameCostCenterCommision = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = res.CommisionCostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
            End If

            If res.ICACostCenterId IsNot Nothing Then
                res.CodeNameCostCenterICA = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = res.ICACostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
            End If

            If res.RTFCostCenterId IsNot Nothing Then
                res.CodeNameCostCenterRTF = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = res.RTFCostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
            End If

            If res.CardCostCenter IsNot Nothing AndAlso res.CardCostCenter.Any() Then
                For Each cc In res.CardCostCenter
                    cc.OperatingUnitCodeName = (From o In _context.OperatingUnit.AsNoTracking() Where o.Id = cc.OperatingUnitId Select String.Concat(o.UnitCode, " - ", o.UnitName)).FirstOrDefault()
                    cc.CostCenterCodeName = (From c In _context.CostCenter.AsNoTracking() Where c.Id = cc.CostCenterId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
                Next
            End If


            Return res
        Else
            Return New Cards()
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener una tarjeta por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCardById(id As Integer) As Cards Implements ICardRepository.GetCardById
        Dim res = (From d As Cards In Me._context.Cards.Include("CardCostCenter") Where d.Id = id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As Cards In Me._context.Cards.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New Cards()
        End If
    End Function
End Class
