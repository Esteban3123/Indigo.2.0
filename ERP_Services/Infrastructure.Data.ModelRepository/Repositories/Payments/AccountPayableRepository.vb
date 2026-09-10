'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class AccountPayableRepository
    Inherits GenericRepository(Of AccountPayable)
    Implements IAccountPayableRepository, Inject

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function SP_ImportBillsToAccountPayable(XmlObject As String, XmlParameters As String) As List(Of SP_ImportBillsToAccountPayable_Result) Implements IAccountPayableRepository.SP_ImportBillsToAccountPayable
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportBillsToAccountPayable(XmlObject, XmlParameters).ToList
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayable(code As String, Optional tracking As Boolean = True) As AccountPayable Implements IAccountPayableRepository.GetAccountPayable
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AccountPayable In Me._context.AccountPayable Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New AccountPayable()
        End If
    End Function

    ''' <summary>
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, Optional tracking As Boolean = True) As CostCenter Implements IAccountPayableRepository.GetCostCenterById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.CostCenter Where d.Id = id Select d)
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As CostCenter In Me._context.CostCenter.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New CostCenter()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de cuentas por pagar por unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListAccountPayableByFilingUnitId(FilingUnitId As Integer) As List(Of AccountPayable) Implements IAccountPayableRepository.GetListAccountPayableByFilingUnitId
        If FilingUnitId = 0 Then
            Throw New ArgumentNullException("FilingUnitId")
        End If
        Dim listAccountPayable As List(Of AccountPayable)
        listAccountPayable = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking
                              Where d.FilingUnitId = FilingUnitId AndAlso d.Status <> 3
                              Select d).ToList
        If listAccountPayable IsNot Nothing AndAlso listAccountPayable.Count > 0 Then

            For Each item As AccountPayable In listAccountPayable
                'Se consulta el nombre del proveedor y de la linea de distribucion
                Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where item.IdSuppliersDistributionLines = sdl.Id Select sdl).FirstOrDefault
                Dim supplier = (From s In _context.Supplier.AsNoTracking.Include("ThirdParty").AsNoTracking Where supplierDistributionLine.IdSupplier = s.Id Select s).FirstOrDefault
                Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
                item.DescriptionDistributionLine = distributionLine.Code + " - " + distributionLine.Name
                item.DescriptionSupplier = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name
                item.ContributionType = supplier.ThirdParty.ContributionType

                If item.PositionId IsNot Nothing Then
                    item.PositionCodeName = (From p In _context.Position.AsNoTracking Where item.PositionId = p.Id Select p.Code + " - " + p.Name).FirstOrDefault
                End If

                'Código y nombre de la unidad de radicación
                Dim filingUnit = (From fu In _context.FilingUnit.AsNoTracking Where fu.Id = item.FilingUnitId Select fu).FirstOrDefault
                item.DescriptionFilingUnit = filingUnit.Code + " - " + filingUnit.Name

                'Código y nombre del tipo de proveedor
                Dim supplierType = (From st In _context.SupplierType.AsNoTracking Where st.Id = item.SupplierTypeId Select st).FirstOrDefault
                item.DescriptionSupplierType = supplierType.Code + " - " + supplierType.Name

                'Numero y nombre de la cuenta contable
                Dim mainAccount = (From a In _context.MainAccounts.AsNoTracking Where item.IdAccount = a.Id Select a).FirstOrDefault
                item.NumberNameMainAccount = mainAccount.Number + " - " + mainAccount.Name

                'Se consulta y se valida el código y nombre de la actividad económica
                Dim economicActivity = GetEconomicActivity(item.IdEconomicActivity)
                If economicActivity IsNot Nothing Then
                    item.CodeNameEconomicActivity = economicActivity.Code + " - " + economicActivity.Name
                End If

                If item.IdCostCenter IsNot Nothing Then
                    'Codigo y nombre del centro de costo
                    Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where item.IdCostCenter = cc.Id Select cc).FirstOrDefault
                    item.DescriptionCostCenter = costCenter.Code + " - " + costCenter.Name
                Else
                    item.DescriptionCostCenter = String.Empty
                End If
            Next

            Return listAccountPayable
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountPayableMassiveConfirm(listDocuments As List(Of String)) As List(Of AccountPayable) Implements IAccountPayableRepository.ListAccountPayableMassiveConfirm
        Return (From cr In _context.AccountPayable.Include("DeferredCausation").Include("AccountPayableDetailConcept").Include("AccountPayableDetailConcept.AccountPayableDetailConceptLiquidation").Include("AccountPayableShares") Where listDocuments.Contains(cr.Code) Select cr).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el listado de facturas por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByCode(code As String, Optional tracking As Boolean = True) As List(Of AccountPayable) Implements IAccountPayableRepository.GetAccountPayableByCode
        Dim listAccountPayable As List(Of AccountPayable)
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        listAccountPayable = (From d As AccountPayable In Me._context.AccountPayable.
                                  Include("AccountPayableDetailConcept").
                                  Include("AccountPayableDetailConcept.AccountPayableDetailConceptLiquidation").
                                  Include("AccountPayableDetailConcept.AccountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationAdjusments").
                                  Include("AccountPayableDetailConcept.AccountPayableDetailConceptLiquidation.AccountPayableDetailConceptLiquidationValuesModificated").
                                  Include("AccountPayableShares").
                                  Include("AccountPayableCommitments").Include("Currency").AsNoTracking()
                              Where d.Code.Equals(code.Trim()) Select d).ToList

        If listAccountPayable?.Any() Then
            'Dictionaries
            Dim dictionaryConcepts As New Dictionary(Of Integer, AccountPayableConcepts)()
            Dim dictionaryRetentionConcepts As New Dictionary(Of Integer, RetentionConcepts)()
            Dim dictionaryGeneralLedgerIVA As New Dictionary(Of Integer, GeneralLedgerIVA)()
            Dim dictionaryMainAccounts As New Dictionary(Of Integer, MainAccounts)()
            Dim dictionaryThirdParties As New Dictionary(Of Integer, ThirdParty)()
            Dim dictionaryCostCenters As New Dictionary(Of Integer, CostCenter)()
            Dim dictionaryDocumentSupport As New Dictionary(Of Integer, BillingAuthorization)()
            Dim dictionaryCommitmentDetail As New Dictionary(Of Integer, CommitmentDetail)()

            'Items Individuals
            Dim conceptDetail As AccountPayableConcepts = Nothing
            Dim mainAccountDetail As MainAccounts = Nothing
            Dim mainAccountHeader As MainAccounts = Nothing
            Dim thirdPartyDetail As ThirdParty = Nothing
            Dim costCenterDetail As CostCenter = Nothing
            Dim documentSupport As BillingAuthorization = Nothing
            Dim commitmentDetail As CommitmentDetail = Nothing

            Dim firstAccountPayable = listAccountPayable.FirstOrDefault()

            'Se consulta el nombre del proveedor, de la linea de distribucion y el tipo de proveedor
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where firstAccountPayable.IdSuppliersDistributionLines = sdl.Id Select sdl).FirstOrDefault
            Dim supplier = (From s In _context.Supplier.AsNoTracking.Include("ThirdParty").AsNoTracking Where supplierDistributionLine.IdSupplier = s.Id Select s).FirstOrDefault
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
            Dim supplierType = (From st In _context.SupplierType.AsNoTracking Where st.Id = firstAccountPayable.SupplierTypeId Select st).FirstOrDefault
            Dim positionCodeName = If(firstAccountPayable.PositionId Is Nothing, String.Empty, (From p In _context.Position.AsNoTracking Where firstAccountPayable.PositionId = p.Id Select p.Code + " - " + p.Name).FirstOrDefault)

            'Código y nombre de la unidad de radicación
            Dim filingUnit = (From fu In _context.FilingUnit.AsNoTracking Where fu.Id = firstAccountPayable.FilingUnitId Select fu).FirstOrDefault

            'Numero y nombre de la cuenta contable
            mainAccountHeader = (From a In _context.MainAccounts.AsNoTracking Where firstAccountPayable.IdAccount = a.Id Select a).FirstOrDefault
            dictionaryMainAccounts.Add(mainAccountHeader.Id, mainAccountHeader)


            'Codigo y nombre del centro de costo
            Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where firstAccountPayable.IdCostCenter = cc.Id Select cc).FirstOrDefault
            If costCenter IsNot Nothing Then
                dictionaryCostCenters.Add(costCenter.Id, costCenter)
            End If

            Dim originalValue = (From d As AccountPayable In Me._context.AccountPayable.Include("AccountPayableDetailConcept").Include("AccountPayableDetailConcept.AccountPayableDetailConceptLiquidation").Include("AccountPayableShares") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()

            For Each item As AccountPayable In listAccountPayable
                item.DescriptionDistributionLine = distributionLine.Code + " - " + distributionLine.Name
                item.PositionCodeName = positionCodeName
                item.DescriptionSupplier = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name
                item.DescriptionSupplierType = supplierType.Code + " - " + supplierType.Name
                item.ContributionType = supplier.ThirdParty.ContributionType
                item.Declarant = IIf(supplier.Declarant, 1, 2)
                item.IndependentEmployee = supplier.IndependentEmployee
                item.DescriptionFilingUnit = filingUnit.Code + " - " + filingUnit.Name
                item.NumberNameMainAccount = mainAccountHeader.Number + " - " + mainAccountHeader.Name
                item.DescriptionCostCenter = If(costCenter Is Nothing, String.Empty, costCenter.Code + " - " + costCenter.Name)
                'Especifica si la cuenta por pagar esta en un traslado
                item.IfTransferContainsAccountPayable = GetIfTransferContainsAccountPayable(item.Id)
                item.CurrencyAbbreviation = item?.Currency?.Abbreviation
                'Se consulta la actividad económica
                Dim economicActivity = GetEconomicActivity(item.IdEconomicActivity)
                'Verifica si la actividad económica existe
                If economicActivity IsNot Nothing Then
                    item.CodeNameEconomicActivity = economicActivity.Code + " - " + economicActivity.Name
                End If

                If item.HandlesDocumentSupport AndAlso item.DocumentSupportId IsNot Nothing Then
                    'Codigo y nombre de la resolucion de Documento Soporte
                    If Not dictionaryDocumentSupport.ContainsKey(item.DocumentSupportId) Then
                        documentSupport = (From ba In _context.BillingAuthorization.AsNoTracking Where item.DocumentSupportId = ba.Id Select ba).FirstOrDefault
                        dictionaryDocumentSupport.Add(item.DocumentSupportId, documentSupport)
                    Else
                        documentSupport = dictionaryDocumentSupport(item.DocumentSupportId)
                    End If

                    If documentSupport IsNot Nothing Then
                        item.DescriptionDocumentSupport = documentSupport.Code + " - " + documentSupport.Name
                    End If
                End If

                'Recorremos el detalle para pegarles las descripciones
                For Each itemDetail As AccountPayableDetailConcept In item.AccountPayableDetailConcept

                    'Codigo y nombre del concepto de pago del detalle
                    If Not dictionaryConcepts.ContainsKey(itemDetail.IdConceptAccountPayable) Then
                        conceptDetail = (From pc In _context.AccountPayableConcepts.AsNoTracking Where itemDetail.IdConceptAccountPayable = pc.Id Select pc).FirstOrDefault
                        dictionaryConcepts.Add(itemDetail.IdConceptAccountPayable, conceptDetail)
                    Else
                        conceptDetail = dictionaryConcepts(itemDetail.IdConceptAccountPayable)
                    End If
                    itemDetail.DescriptionPaymentConcept = conceptDetail.Code + " - " + conceptDetail.Name

                    If itemDetail.RateIva IsNot Nothing Then
                        Dim RateIva As GeneralLedgerIVA = Nothing
                        'nombre del registro de IVA del detalle
                        If Not dictionaryGeneralLedgerIVA.ContainsKey(itemDetail.RateIva) Then
                            RateIva = (From d As GeneralLedgerIVA In _context.GeneralLedgerIVA.AsNoTracking
                                       Where itemDetail.RateIva = d.Id
                                       Select d).FirstOrDefault

                            dictionaryGeneralLedgerIVA.Add(itemDetail.RateIva, RateIva)
                        Else
                            RateIva = dictionaryGeneralLedgerIVA(itemDetail.RateIva)
                        End If

                        itemDetail.NameIVA = RateIva.Name
                    End If

                    If itemDetail.IdRetentionConcept IsNot Nothing Then
                        Dim RetentionConceptDetail As RetentionConcepts = Nothing
                        'Codigo y nombre del concepto de retencion del detalle
                        If Not dictionaryRetentionConcepts.ContainsKey(itemDetail.IdRetentionConcept) Then
                            RetentionConceptDetail = (From d As RetentionConcepts In _context.RetentionConcepts.AsNoTracking
                                                      Where itemDetail.IdRetentionConcept = d.Id
                                                      Select d).FirstOrDefault

                            dictionaryRetentionConcepts.Add(itemDetail.IdRetentionConcept, RetentionConceptDetail)
                        Else
                            RetentionConceptDetail = dictionaryRetentionConcepts(itemDetail.IdRetentionConcept)
                        End If

                        itemDetail.DescriptionRetentionConcept = RetentionConceptDetail.Code + " - " + RetentionConceptDetail.Name
                    End If

                    'Numero y nombre de la cuenta contable del detalle
                    If Not dictionaryMainAccounts.ContainsKey(itemDetail.IdAccount) Then
                        mainAccountDetail = (From m In _context.MainAccounts.AsNoTracking Where itemDetail.IdAccount = m.Id Select m).FirstOrDefault
                        dictionaryMainAccounts.Add(itemDetail.IdAccount, mainAccountDetail)
                    Else
                        mainAccountDetail = dictionaryMainAccounts(itemDetail.IdAccount)
                    End If
                    itemDetail.NumberNameMainAccount = mainAccountDetail.Number + " - " + mainAccountDetail.Name

                    'Nit y nombre del tercero
                    If Not dictionaryThirdParties.ContainsKey(itemDetail.IdThirdParty) Then
                        thirdPartyDetail = (From x In _context.ThirdParty.AsNoTracking Where itemDetail.IdThirdParty = x.Id Select x).FirstOrDefault
                        dictionaryThirdParties.Add(itemDetail.IdThirdParty, thirdPartyDetail)
                    Else
                        thirdPartyDetail = dictionaryThirdParties(itemDetail.IdThirdParty)
                    End If
                    itemDetail.DescriptionThirdParty = thirdPartyDetail.Nit + " - " + thirdPartyDetail.Name

                    'Codigo y nombre del centro de costo del detalle
                    If itemDetail.IdCostCenter IsNot Nothing Then
                        If Not dictionaryCostCenters.ContainsKey(itemDetail.IdCostCenter) Then
                            costCenterDetail = (From cc In _context.CostCenter.AsNoTracking Where itemDetail.IdCostCenter = cc.Id Select cc).FirstOrDefault
                            dictionaryCostCenters.Add(itemDetail.IdCostCenter, costCenterDetail)
                        Else
                            costCenterDetail = dictionaryCostCenters(itemDetail.IdCostCenter)
                        End If
                        itemDetail.DescriptionCostCenter = costCenterDetail.Code + " - " + costCenterDetail.Name
                    Else
                        itemDetail.DescriptionCostCenter = String.Empty
                    End If

                    If conceptDetail.EmployeeCategoryRetention Then
                        Dim retentionConcept As RetentionConcepts
                        If conceptDetail.ThreeEightThreeRetentionConceptId IsNot Nothing Then
                            itemDetail.RetentionConceptId383 = conceptDetail.ThreeEightThreeRetentionConceptId
                            retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where rc.Id = conceptDetail.ThreeEightThreeRetentionConceptId Select rc).FirstOrDefault
                            itemDetail.RetentionConceptDescription383 = retentionConcept.Code + " - " + retentionConcept.Name
                        End If
                        If conceptDetail.ThreeEightFourRetentionConceptId IsNot Nothing Then
                            itemDetail.RetentionConceptId384 = conceptDetail.ThreeEightFourRetentionConceptId
                            retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where rc.Id = conceptDetail.ThreeEightFourRetentionConceptId Select rc).FirstOrDefault
                            itemDetail.RetentionConceptDescription384 = retentionConcept.Code + " - " + retentionConcept.Name
                        End If
                    End If
                Next

                If item.AccountPayableCommitments IsNot Nothing AndAlso item.AccountPayableCommitments.Any() Then
                    For Each apc In item.AccountPayableCommitments
                        If Not dictionaryCommitmentDetail.ContainsKey(apc.CommitmentDetailId) Then
                            commitmentDetail = (From c In _context.CommitmentDetail.AsNoTracking.Include("Commitment").AsNoTracking.Include("Category").AsNoTracking.Include("Category.FinancialSource").AsNoTracking.Include("RevenueType").AsNoTracking
                                                Where c.Id = apc.CommitmentDetailId Select c).FirstOrDefault
                            dictionaryCommitmentDetail.Add(apc.CommitmentDetailId, commitmentDetail)
                        Else
                            commitmentDetail = dictionaryCommitmentDetail(apc.CommitmentDetailId)
                        End If

                        apc.CommitmentCode = commitmentDetail.Commitment.Code
                        apc.CommitmentDocument = commitmentDetail.Commitment.Document
                        apc.CategoryCodeName = String.Format("{0} - {1}", commitmentDetail.Category.Code, commitmentDetail.Category.Name)
                        apc.FinancialSourceCodeName = String.Format("{0} - {1}", commitmentDetail.Category.FinancialSource.Code, commitmentDetail.Category.FinancialSource.Name)
                        apc.RevenueTypeCodeName = String.Format("{0} - {1}", commitmentDetail.RevenueType.Code, commitmentDetail.RevenueType.Name)
                        apc.Balance = commitmentDetail.Balance

                        If item.BudgetaryValidityId Is Nothing Then
                            Dim budgetaryValidity = (From bv In _context.BudgetaryValidity.AsNoTracking() Where bv.Id = commitmentDetail.Commitment.BudgetaryValidityId Select bv).FirstOrDefault()

                            If budgetaryValidity IsNot Nothing Then
                                item.BudgetaryValidityId = budgetaryValidity.Id
                                item.BudgetaryValidityDescription = budgetaryValidity.Year

                                item.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                                item.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                            End If
                        End If
                    Next
                End If

                'Se obtiene el detalle del compromiso
                If item.CommitmentDetailId IsNot Nothing Then
                    If Not dictionaryCommitmentDetail.ContainsKey(item.CommitmentDetailId) Then
                        commitmentDetail = (From c In _context.CommitmentDetail.AsNoTracking.Include("Commitment").AsNoTracking.Include("Category").AsNoTracking.Include("Category.FinancialSource").AsNoTracking.Include("RevenueType").AsNoTracking
                                            Where c.Id = item.CommitmentDetailId Select c).FirstOrDefault
                        dictionaryCommitmentDetail.Add(item.CommitmentDetailId, commitmentDetail)
                    Else
                        commitmentDetail = dictionaryCommitmentDetail(item.CommitmentDetailId)
                    End If

                    item.AccountPayableCommitments.Add(New AccountPayableCommitments With {
                        .CommitmentCode = commitmentDetail.Commitment.Code,
                        .CommitmentDocument = commitmentDetail.Commitment.Document,
                        .CategoryCodeName = String.Format("{0} - {1}", commitmentDetail.Category.Code, commitmentDetail.Category.Name),
                        .FinancialSourceCodeName = String.Format("{0} - {1}", commitmentDetail.Category.FinancialSource.Code, commitmentDetail.Category.FinancialSource.Name),
                        .RevenueTypeCodeName = String.Format("{0} - {1}", commitmentDetail.RevenueType.Code, commitmentDetail.RevenueType.Name),
                        .Balance = commitmentDetail.Balance,
                        .Value = item.InvoiceValue
                    })

                    If item.BudgetaryValidityId Is Nothing Then
                        Dim budgetaryValidity = (From bv In _context.BudgetaryValidity.AsNoTracking() Where bv.Id = commitmentDetail.Commitment.BudgetaryValidityId Select bv).FirstOrDefault()

                        If budgetaryValidity IsNot Nothing Then
                            item.BudgetaryValidityId = budgetaryValidity.Id
                            item.BudgetaryValidityDescription = budgetaryValidity.Year

                            item.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                            item.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                        End If
                    End If
                End If

                item.OriginalValue = originalValue
            Next

            Return listAccountPayable
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Se obtiene la actividad económica de la cuenta por pagar
    ''' </summary>
    ''' <param name="idEconomicActivity"></param>
    ''' <returns></returns>
    Private Function GetEconomicActivity(idEconomicActivity As Integer?)
        If idEconomicActivity.HasValue Then
            Return (From ea In _context.EconomicActivity.AsNoTracking Where idEconomicActivity.Value = ea.Id Select ea).FirstOrDefault
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el listado de facturas por id del proveedor
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdSupplier(idSupplier As Integer, Optional tracking As Boolean = True) As List(Of AccountPayable) Implements IAccountPayableRepository.GetAccountPayableByIdSupplier
        Dim listAccountPayable As List(Of AccountPayable)
        If idSupplier = 0 OrElse idSupplier = Nothing Then
            Throw New ArgumentNullException("code")
        End If
        listAccountPayable = (From d As AccountPayable In Me._context.AccountPayable Where d.IdSupplier = idSupplier Select d).ToList()
        If listAccountPayable IsNot Nothing AndAlso listAccountPayable.Count > 0 Then
            listAccountPayable = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking() Where d.IdSupplier = idSupplier Select d).ToList
            Return listAccountPayable
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una factura por id del tercero y el estado
    ''' </summary>
    Public Function GetAccountPayableByIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, Optional tracking As Boolean = True) As List(Of AccountPayable) Implements IAccountPayableRepository.GetAccountPayableByIdThirdIdAccountAndState
        Dim listAccountPayable As List(Of AccountPayable)
        If state = 0 OrElse IdThird = Nothing Then
            Throw New ArgumentNullException("code")
        End If
        listAccountPayable = (From d As AccountPayable In Me._context.AccountPayable.Include("AccountPayableDetailConcept").Include("AccountPayableShares")
                              Where d.Supplier.IdThirdParty = IdThird And d.IdAccount = IdAccount And d.Status = CByte(state) And d.Balance > 0 Select d).ToList()
        Return listAccountPayable
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de la factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="formAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByBillNumber(code As String, idSupplier As Integer, Optional formAccountPayable As Boolean = True) As AccountPayable Implements IAccountPayableRepository.GetAccountPayableByBillNumber
        'If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
        '    Throw New ArgumentNullException("code")
        'End If
        Dim res = (From d As AccountPayable In Me._context.AccountPayable.Include("AccountPayableShares") Where d.BillNumber.Equals(code) And d.IdSupplier = idSupplier Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking() Where d.BillNumber.Equals(code) And d.IdSupplier = idSupplier Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New AccountPayable()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetAccountPayableById(Id As Integer, Optional tracking As Boolean = True) As AccountPayable Implements IAccountPayableRepository.GetAccountPayableById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res As List(Of AccountPayable)
        If tracking Then
            res = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking.Include("AccountPayableDetailConcept").AsNoTracking.Include("AccountPayableShares").AsNoTracking.Include("Currency").AsNoTracking() Where d.Id = Id Select d).ToList()
        Else
            res = (From d As AccountPayable In Me._context.AccountPayable.Include("AccountPayableDetailConcept").Include("AccountPayableShares").Include("Currency").AsNoTracking() Where d.Id = Id Select d).ToList()
        End If
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking.Include("AccountPayableDetailConcept").AsNoTracking.Include("AccountPayableShares").AsNoTracking().Include("Currency").AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New AccountPayable()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cxp por id para notas
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdForNotes(Id As Integer, Optional tracking As Boolean = True) As AccountPayable Implements IAccountPayableRepository.GetAccountPayableByIdForNotes
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As AccountPayable In Me._context.AccountPayable.Include("AccountPayableShares") Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As AccountPayable In Me._context.AccountPayable.Include("AccountPayableShares").AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New AccountPayable()
        End If
    End Function

    ''' <summary>
    ''' obtiene las cuotas de una factura por el id de la factura, id del tercero y el estado
    ''' </summary>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, Optional tracking As Boolean = True) As List(Of AccountPayableShares) Implements IAccountPayableRepository.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState
        Dim listAccountPayable As List(Of AccountPayableShares)
        If IdThird = 0 OrElse IdThird = Nothing Then
            Throw New ArgumentNullException("IdThird")
        End If
        If IdAccount = 0 OrElse IdThird = Nothing Then
            Throw New ArgumentNullException("IdAccount")
        End If
        If state = 0 OrElse IdThird = Nothing Then
            Throw New ArgumentNullException("state")
        End If
        Dim zero As Decimal = 0
        listAccountPayable = (From d As AccountPayableShares In Me._context.AccountPayableShares.AsNoTracking()
                              Join ap As AccountPayable In _context.AccountPayable.AsNoTracking() On ap.Id Equals d.IdAccountPayable
                              Join s As Supplier In _context.Supplier.AsNoTracking() On ap.IdSupplier Equals s.Id
                              Where s.IdThirdParty = IdThird _
                              And ap.IdAccount = IdAccount _
                              And ap.Status = CByte(state) _
                              And d.Balance > zero Select d).ToList()
        Return listAccountPayable
    End Function

    ''' <summary>
    ''' Obtiene las facturas que tiene un proveedor y que esten confrimadas
    ''' </summary>
    ''' <param name="idSupplier"></param>
    ''' <param name="status"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByIdSupplierAndState(idSupplier As Integer, status As Integer, Optional tracking As Boolean = True) As List(Of AccountPayable) Implements IAccountPayableRepository.GetAccountPayableByIdSupplierAndState
        Dim listAccountPayable As List(Of AccountPayable)
        If idSupplier = 0 OrElse idSupplier = Nothing Then
            Throw New ArgumentNullException("code")
        End If
        listAccountPayable = (From d As AccountPayable In Me._context.AccountPayable Where d.IdSupplier = idSupplier And d.Status = status Select d).ToList()
        If listAccountPayable IsNot Nothing AndAlso listAccountPayable.Count > 0 Then
            listAccountPayable = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking() Where d.IdSupplier = idSupplier And d.Status = status Select d).ToList
            Return listAccountPayable
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene una cuota de factura por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetAccountPayableShareById(Id As Integer) As AccountPayableShares Implements IAccountPayableRepository.GetAccountPayableShareById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As AccountPayableShares In Me._context.AccountPayableShares Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Dim IdAccountPayable = res(0).IdAccountPayable
            Dim variables = (From ap In _context.AccountPayable Where ap.Id = IdAccountPayable Select New With {Key .BillNumber = ap.BillNumber, .BillDate = ap.BillDate}).FirstOrDefault()
            res(0).InvoiceBillNumber = variables.BillNumber
            res(0).InvoiceBillDate = variables.BillDate
            res(0).OriginalValue = (From d As AccountPayableShares In Me._context.AccountPayableShares.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New AccountPayableShares()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de cuentas asociadas a un tercero cuenta y estado
    ''' </summary>
    Public Function GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird As Integer, IdAccount As Integer, state As Short, Optional tracking As Boolean = True) As Integer Implements IAccountPayableRepository.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState
        If IdThird = 0 OrElse IdThird = Nothing Then
            Throw New ArgumentNullException("IdThird")
        End If
        If IdAccount = 0 OrElse IdThird = Nothing Then
            Throw New ArgumentNullException("IdAccount")
        End If
        If state = 0 OrElse IdThird = Nothing Then
            Throw New ArgumentNullException("state")
        End If
        Dim Zero As Integer = 0
        Dim Query As Integer = (From d As AccountPayableShares In Me._context.AccountPayableShares.AsNoTracking()
                                Join ap As AccountPayable In _context.AccountPayable.AsNoTracking() On ap.Id Equals d.IdAccountPayable
                                Join s As Supplier In _context.Supplier.AsNoTracking() On ap.IdSupplier Equals s.Id
                                Where s.IdThirdParty = IdThird _
                                And ap.IdAccount = IdAccount _
                                And ap.Status = CByte(state) _
                                And d.Balance > Zero Select d).Count()
        Return Query

    End Function

    ''' <summary>
    ''' Consulta las cuotas de la factura
    ''' </summary>
    ''' <param name="idAccountPayable"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableSharesByIdAccountPayable(idAccountPayable As Integer, Optional tracking As Boolean = True) As List(Of AccountPayableShares) Implements IAccountPayableRepository.GetAccountPayableSharesByIdAccountPayable
        Dim listAccountPayableShares As List(Of AccountPayableShares)
        If idAccountPayable = 0 OrElse idAccountPayable = Nothing Then
            Throw New ArgumentNullException("idAccountPayable")
        End If
        listAccountPayableShares = (From d As AccountPayableShares In Me._context.AccountPayableShares Where d.IdAccountPayable = idAccountPayable Select d Order By d.Share).ToList()
        If listAccountPayableShares IsNot Nothing AndAlso listAccountPayableShares.Count > 0 Then
            listAccountPayableShares = (From d As AccountPayableShares In Me._context.AccountPayableShares.AsNoTracking() Where d.IdAccountPayable = idAccountPayable Select d Order By d.Share).ToList
            Return listAccountPayableShares
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la validacion si la cxp consultada maneja causacion diferida
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValidationDeferredCausation(accountPayableId As Integer) As Boolean Implements IAccountPayableRepository.GetValidationDeferredCausation
        Dim accountPayable = (From ap In _context.AccountPayable.AsNoTracking.Include("AccountPayableDetailConcept").AsNoTracking Where ap.Id = accountPayableId Select ap).FirstOrDefault
        Dim cont = accountPayable.AccountPayableDetailConcept.ToList.FindAll(Function(item) item.DeferredCausation = True).Count
        If cont > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCheckExistAccountPayable() As Boolean Implements IAccountPayableRepository.GetCheckExistAccountPayable
        Dim listAccountPayable = (From ap In _context.AccountPayable.AsNoTracking Select ap).ToList
        If listAccountPayable IsNot Nothing AndAlso listAccountPayable.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro que tiene el mas alto numero consecutivo de radicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUltimateRegisterConsecutiveFiling() As AccountPayable Implements IAccountPayableRepository.GetUltimateRegisterConsecutiveFiling
        Dim accountPayable As AccountPayable = _context.AccountPayable.OrderByDescending(Function(x) x.NumberFiling).Take(1).FirstOrDefault
        Return accountPayable
    End Function

    ''' <summary>
    ''' Obtiene una cuenta por pagar por el numero de la factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountsPayableByBillNumber(BillNumber As String) As AccountPayable Implements IAccountPayableRepository.GetAccountsPayableByBillNumber
        If BillNumber Is Nothing OrElse BillNumber.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("BillNumber")
        End If
        Dim res = (From d As AccountPayable In Me._context.AccountPayable Where d.BillNumber.Equals(BillNumber.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As AccountPayable In Me._context.AccountPayable.AsNoTracking() Where d.BillNumber.Equals(BillNumber.Trim()) Select d).FirstOrDefault()
            Return res(0)
        Else
            Return New AccountPayable()
        End If
    End Function

    ''' <summary>
    ''' Obtiene todas las lineas de distribución de un proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDistributionLines() As List(Of Domain.Entities.SuppliersDistributionLines) Implements IAccountPayableRepository.GetAllDistributionLines
        Dim listDistributionLines = (From d As SuppliersDistributionLines In Me._context.SuppliersDistributionLines Select d).ToList()
        If listDistributionLines IsNot Nothing AndAlso listDistributionLines.Count > 0 Then
            listDistributionLines = (From d As SuppliersDistributionLines In Me._context.SuppliersDistributionLines.AsNoTracking().Include("DistributionLines").Include("Supplier") Select d).ToList()
            Return listDistributionLines
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Valida si la cuenta por pagar esta asociada a un traslado
    ''' </summary>
    ''' <param name="AccountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIfTransferContainsAccountPayable(AccountPayableId As Integer) As Boolean Implements IAccountPayableRepository.GetIfTransferContainsAccountPayable
        If AccountPayableId = 0 Then
            Throw New ArgumentNullException("AccountPayableId")
        End If
        Dim cont = (From t In _context.AccountPayableTransferDetail.AsNoTracking Where t.AccountPayableId = AccountPayableId Select t).Count
        If cont > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Sub SaveAccountPayableList(accountPayableList As List(Of AccountPayable)) Implements IAccountPayableRepository.SaveAccountPayableList
        _context.AccountPayable.AddRange(accountPayableList)
    End Sub

    Public Function ListAccountPayableById(listId As List(Of Integer?)) As List(Of AccountPayable) Implements IAccountPayableRepository.ListAccountPayableById
        Return (From cr In _context.AccountPayable.Include("AccountPayableShares").AsNoTracking() Where listId.Contains(cr.Id) Select cr).ToList()
    End Function

    Public Function GetAccountPayableByBillNumberAndMainAccount(code As String, idSupplier As Integer, mainAccountId As Integer) As AccountPayable Implements IAccountPayableRepository.GetAccountPayableByBillNumberAndMainAccount
        Return (From d As AccountPayable In Me._context.AccountPayable.Include("AccountPayableShares")
                Where d.BillNumber.Equals(code) And d.IdSupplier = idSupplier And d.IdAccount = mainAccountId And d.Status = 2
                Select d).FirstOrDefault()
    End Function

    Public Function ValidateAccountPayableByCostDistributionDirectCostId(CostDistributionDirectCostId As Integer, AccountPayableId As Integer) As AccountPayable Implements IAccountPayableRepository.ValidateAccountPayableByCostDistributionDirectCostId
        Return (From x In _context.AccountPayable.AsNoTracking Where x.CostDistributionDirectCostId = CostDistributionDirectCostId AndAlso x.Status <> 3 AndAlso x.Id <> AccountPayableId Select x).FirstOrDefault
    End Function

    Public Function ValidateBillValueIsSameInAccountPayableAndCostDistributionDirectCost(AccountPayableId As Integer) As CostDistributionDirectCost Implements IAccountPayableRepository.ValidateBillValueIsSameInAccountPayableAndCostDistributionDirectCost
        Return (From x In _context.CostDistributionDirectCost.AsNoTracking Where x.AccountPayableId = AccountPayableId AndAlso x.Status <> 3 Select x).FirstOrDefault
    End Function

    Function SP_ConfirmAccountsPayable(AccountsPayableXml As String, codeUser As String, isMassiveConfirm As Boolean) As List(Of SP_ConfirmAccountsPayable_Result) Implements IAccountPayableRepository.SP_ConfirmAccountsPayable
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmAccountsPayable(AccountsPayableXml, codeUser, isMassiveConfirm).ToList()
    End Function

    ''' <summary>
    ''' Guarda la cuenta por pagar
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveAccountsPayable(Xml As String, UserCode As String) As SP_SaveAccountsPayable_Result Implements IAccountPayableRepository.SP_SaveAccountsPayable
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAccountsPayable(Xml, UserCode).SingleOrDefault()
    End Function


    ''' <summary>
    '''  Esta función actúa como un intermediario para llamar a GetSupportPaymentSuppliers en el servicio IAccountPayableAdminService
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <returns></returns>
    Public Function SP_SupportPaymentSuppliers(Xml As String) As List(Of SP_SupportPaymentSuppliers_Result) Implements IAccountPayableRepository.SP_SupportPaymentSuppliers
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SupportPaymentSuppliers(Xml).ToList()
    End Function

    Public Function GetCostCenterByCode(Code As String) As CostCenter Implements IAccountPayableRepository.GetCostCenterByCode
        Return (From e In _context.CostCenter.AsNoTracking()
                Where e.Code = Code
                Select e).FirstOrDefault()
    End Function
End Class