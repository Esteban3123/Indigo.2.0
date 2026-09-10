'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Dynamic
Imports System

Public Class CareGroupRepository
    Inherits GenericRepository(Of CareGroup)
    Implements ICareGroupRepository

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


    Public Function ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId As Integer) As List(Of CareGroupInvoiceCategories) Implements ICareGroupRepository.ListCareGroupInvoiceCategoriesByCareGroupId
        Dim res = (From e In _context.CareGroupInvoiceCategories Where e.CareGroupId = careGroupId Select e).ToList()
        For Each item In res
            item.CodeNameInvoiceCategory = (From c In _context.InvoiceCategories.AsNoTracking() Where c.Id = item.InvoiceCategoriesId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
        Next
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroup(code As String) As CareGroup Implements ICareGroupRepository.GetCareGroup
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CareGroup In Me._context.CareGroup.Include("CareGroupDefinitionRate").Include("CareGroupBillingItemsRestriction").Include("GroupersCareGroup").Include("ControlByTypeFunctionalUnit").Include("CareGroupPackage").Include("CareGroupMixLiquidation")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.ContractId IsNot Nothing Then
                Dim contractCG = (From c In _context.Contract.AsNoTracking Where c.Id = res.ContractId Select c).FirstOrDefault
                res.ContractDescription = contractCG.Code + " - " + contractCG.ContractName
            End If

            If res.CareGroupPackage IsNot Nothing AndAlso res.CareGroupPackage.Count > 0 Then

                res.CareGroupPackage.ToList().ForEach(Sub(i As CareGroupPackage)
                                                          Dim ObjContractPackage = (From x In _context.ContractPackage.AsNoTracking() Where x.Id = i.ContractPackageId Select x).FirstOrDefault
                                                          i.PackageCodeName = String.Join(" - ", ObjContractPackage.Code, ObjContractPackage.Name)
                                                      End Sub)
            End If

            If _context.CompanyType?.Any() Then
                res.NameEntityType = (From f In _context.CompanyType Where f.Id = res.EntityType Select f)?.FirstOrDefault.Name
            End If

            Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.CostCenterId Select cc).FirstOrDefault
            res.CostCenterDescription = costCenter.Code + " - " + costCenter.Name

            Dim requirementTemplate = (From rt In _context.RequirementTemplate.AsNoTracking Where rt.Id = res.RequirementsTemplateId Select rt).FirstOrDefault
            res.RequirementTemplateDescription = requirementTemplate.Code + " - " + requirementTemplate.Name

            Dim prodcutTemplate = (From pt In _context.ProductRate.AsNoTracking Where pt.Id = res.ProductRateId Select pt).FirstOrDefault
            res.ProductTemplateDescription = prodcutTemplate.Code + " - " + prodcutTemplate.Name

            Dim procedureTemplate = (From ptem In _context.ProcedureTemplate.AsNoTracking Where ptem.Id = res.ProcedureTemplateId Select ptem).FirstOrDefault
            res.ProcedureTemplateDescription = procedureTemplate.Code + " - " + procedureTemplate.Name

            If res.ContractAccountingStructureId IsNot Nothing Then
                Dim contractAccountingStructure = (From c In _context.ContractAccountingStructure.AsNoTracking Where c.Id = res.ContractAccountingStructureId Select c).FirstOrDefault
                res.ContractAccountingStructureDescription = contractAccountingStructure.Code + " - " + contractAccountingStructure.Name
            End If

            If res.TechnicalNoteId IsNot Nothing Then
                res.TechnicalNoteDescription = (From p In _context.TechnicalNote.AsNoTracking Where res.TechnicalNoteId = p.Id Select p.Code + " - " + p.Name).FirstOrDefault
            End If

            If res.BillingConceptCopayId.HasValue Then
                res.BillingConceptCopayCodeName = (From c In _context.BillingConcept.AsNoTracking() Where c.Id = res.BillingConceptCopayId.Value Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
            End If

            If res.BillingBudgetId IsNot Nothing Then
                Dim budget = (From b In _context.Budget.AsNoTracking().Include("Category").AsNoTracking().Include("Category.FinancialSource").AsNoTracking().Include("RevenueType").AsNoTracking() Where b.Id = res.BillingBudgetId).FirstOrDefault()
                If budget IsNot Nothing Then
                    res.BillingBudgetDescription = String.Concat(budget.Category.Code, " - ", budget.Category.Name, " - ", budget.Category.FinancialSource.Code, " - ", budget.Category.FinancialSource.Name, " - ", budget.RevenueType.Code, " - ", budget.RevenueType.Name)

                    Dim budgetaryValidity = (From bh In _context.BudgetHeader.AsNoTracking()
                                             Join bv In _context.BudgetaryValidity.AsNoTracking()
                                                On bh.BudgetaryValidityId Equals bv.Id
                                             Where bh.Id = budget.BudgetHeaderId Select bv).FirstOrDefault()

                    If budgetaryValidity IsNot Nothing Then
                        res.BudgetaryValidityId = budgetaryValidity.Id
                        res.BudgetaryValidityDescription = budgetaryValidity.Year

                        res.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                        res.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                    End If
                End If
            End If

            If res.PromissoryNoteBudgetId IsNot Nothing Then
                Dim budget = (From b In _context.Budget.AsNoTracking().Include("Category").AsNoTracking().Include("Category.FinancialSource").AsNoTracking().Include("RevenueType").AsNoTracking() Where b.Id = res.PromissoryNoteBudgetId).FirstOrDefault()
                If budget IsNot Nothing Then
                    res.PromissoryNoteBudgetDescription = String.Concat(budget.Category.Code, " - ", budget.Category.Name, " - ", budget.Category.FinancialSource.Code, " - ", budget.Category.FinancialSource.Name, " - ", budget.RevenueType.Code, " - ", budget.RevenueType.Name)
                End If
            End If

            If res.AccountReceivablePreviousValidityBudgetId IsNot Nothing Then
                Dim budget = (From b In _context.Budget.AsNoTracking().Include("Category").AsNoTracking().Include("Category.FinancialSource").AsNoTracking().Include("RevenueType").AsNoTracking() Where b.Id = res.AccountReceivablePreviousValidityBudgetId).FirstOrDefault()
                If budget IsNot Nothing Then
                    res.AccountReceivablePreviousValidityBudgetDescription = String.Concat(budget.Category.Code, " - ", budget.Category.Name, " - ", budget.Category.FinancialSource.Code, " - ", budget.Category.FinancialSource.Name, " - ", budget.RevenueType.Code, " - ", budget.RevenueType.Name)
                End If
            End If

            If res.PortfolioRecoveryBudgetId IsNot Nothing Then
                Dim budget = (From b In _context.Budget.AsNoTracking().Include("Category").AsNoTracking().Include("Category.FinancialSource").AsNoTracking().Include("RevenueType").AsNoTracking() Where b.Id = res.PortfolioRecoveryBudgetId).FirstOrDefault()
                If budget IsNot Nothing Then
                    res.PortfolioRecoveryBudgetDescription = String.Concat(budget.Category.Code, " - ", budget.Category.Name, " - ", budget.Category.FinancialSource.Code, " - ", budget.Category.FinancialSource.Name, " - ", budget.RevenueType.Code, " - ", budget.RevenueType.Name)
                End If
            End If

            If res.CareGroupDefinitionRate IsNot Nothing AndAlso res.CareGroupDefinitionRate.Count > 0 Then
                res.CareGroupDefinitionRate.ToList.ForEach(Sub(item)
                                                               Dim definitionRate = (From d In _context.DefinitionRate.AsNoTracking Where d.Id = item.DefinitionRateId Select d).FirstOrDefault
                                                               item.DefinitionRateDescription = definitionRate.Code + " - " + definitionRate.Name
                                                           End Sub)
            End If

            If res.CareGroupBillingItemsRestriction IsNot Nothing AndAlso res.CareGroupBillingItemsRestriction.Count > 0 Then
                res.CareGroupBillingItemsRestriction.ToList.ForEach(Sub(item)
                                                                        Dim BillingItemsRestriction = (From d In _context.BillingItemsRestriction.AsNoTracking Where d.Id = item.BillingItemsRestrictionId Select d).FirstOrDefault
                                                                        item.BillingItemsRestrictionDescription = BillingItemsRestriction.Code + " - " + BillingItemsRestriction.Name
                                                                    End Sub)
            End If

            If res.GroupersCareGroup IsNot Nothing AndAlso res.GroupersCareGroup.Count > 0 Then
                res.GroupersCareGroup.ToList().ForEach(Sub(i)
                                                           i.GrouperName = (From g In _context.Groupers.AsNoTracking() Where g.Id = i.GroupersId Select g.Description).FirstOrDefault()
                                                       End Sub)
            End If

            res.OriginalValue = (From g In _context.CareGroup.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New CareGroup()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un grupo de atencion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroupById(id As Integer) As CareGroup Implements ICareGroupRepository.GetCareGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.CareGroup.Include("Contract").Include("RequirementTemplate").AsNoTracking.Include("RequirementTemplate.Requirement").AsNoTracking Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then

            If res.ContractId IsNot Nothing Then
                res.ContractDescription = res.Contract.Code + " - " + res.Contract.ContractName

                Dim healthAdministrator = (From ha In _context.HealthAdministrator.AsNoTracking Where ha.Id = res.Contract.HealthAdministratorId Select ha).FirstOrDefault()
                res.Contract.ThirdPartyId = healthAdministrator.ThirdPartyId
                res.Contract.HealthAdministratorDescription = healthAdministrator.Code + " - " + healthAdministrator.Name
            End If

            res.OriginalValue = (From d As CareGroup In Me._context.CareGroup.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res
        Else
            Return New CareGroup()
        End If
    End Function

    Public Function GetCareGroupByIdSimple(id As Integer) As CareGroup Implements ICareGroupRepository.GetCareGroupByIdSimple
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.CareGroup.AsNoTracking Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New CareGroup()
        End If
    End Function

    Public Function GetCareGroupByIdWithAssociations(Id As Integer) As CareGroup Implements ICareGroupRepository.GetCareGroupByIdWithAssociations
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.CareGroup.AsNoTracking.Include("ContractAccountingStructure").AsNoTracking.Include("Contract").AsNoTracking Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then
            Return res
        Else
            Return New CareGroup()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un grupo de atencion por id para eliminar
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroupByIdForDelete(id As Integer) As CareGroup Implements ICareGroupRepository.GetCareGroupByIdForDelete
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.CareGroup.Include("CareGroupRate").Include("CareGroupRate.RateByUnitType").Include("CareGroupRate.RateBySpecialty").Include("CareGroupRate.RateByFunctionalUnit").Include("CareGroupRate.RateByFixed").Include("CareGroupRate.RateByStandard").Include("CareGroupRate.RateByFormula")
                   Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing Then

            'If res.CareGroupRate IsNot Nothing AndAlso res.CareGroupRate.Count > 0 Then
            '    For Each item As CareGroupRate In res.CareGroupRate
            '        Dim cups = (From c In _context.CUPSEntity.AsNoTracking Where c.Id = item.CupsEntityId Select c).FirstOrDefault
            '        item.CodeNameCUPS = cups.Code + " - " + cups.Description
            '    Next
            'End If

            Return res
        Else
            Return New CareGroup()
        End If
    End Function


    ''' <summary>
    ''' obtinene un grupo de atencion con los campos requeridos
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCareGroupPOCOById(id As Integer) As Dynamic.ExpandoObject Implements ICareGroupRepository.GetCareGroupPOCOById

        Dim careGroupTmp = (From cg In _context.CareGroup.AsNoTracking() Where cg.Id = id).FirstOrDefault()
        Dim contract As Contract = Nothing
        Dim healthAdministrator As HealthAdministrator = Nothing
        If careGroupTmp.ContractId IsNot Nothing Then
            contract = (From c In _context.Contract.AsNoTracking() Where c.Id = careGroupTmp.ContractId Select c).FirstOrDefault()
            healthAdministrator = (From ha In _context.HealthAdministrator.AsNoTracking() Where ha.Id = contract.HealthAdministratorId Select ha).FirstOrDefault()
        End If
        Dim careGroup As Object = New ExpandoObject()

        With careGroup
            .CareGroupId = careGroupTmp.Id
            .FolioType = careGroupTmp.CareGroupType
            .LiquidationType = careGroupTmp.LiquidationType
            If contract IsNot Nothing Then
                .ContractEntityId = contract.ContractEntityId
                .HealthAdministratorId = healthAdministrator.Id
                .ThirdPartyId = healthAdministrator.ThirdPartyId
            Else
                .ContractEntityId = Nothing
                .HealthAdministratorId = Nothing
                .ThirdPartyId = Nothing
            End If
        End With
        Return careGroup
    End Function

    ''' <summary>
    ''' Guarda el listado de careGroupRate
    ''' </summary>
    ''' <param name="ListCareGroupRate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveList(ListCareGroupRate As List(Of CareGroupRate)) As List(Of CareGroupRate) Implements ICareGroupRepository.SaveList
        'Return Me._context.CareGroupRate.AddRange(ListCareGroupRate).ToList()
    End Function

    Public Function GetGroupersCareGroup(CareGroupId As Integer, grouperId As Integer) As GroupersCareGroup Implements ICareGroupRepository.GetGroupersCareGroup
        If CareGroupId = 0 Then
            Throw New ArgumentNullException("CareGroupId")
        End If
        If grouperId = 0 Then
            Throw New ArgumentNullException("grouperId")
        End If
        Dim res = (From d In Me._context.GroupersCareGroup.Include("GroupersCareGroupCups").Include("GroupersCareGroupActivities").AsNoTracking Where d.CareGroupId = CareGroupId And d.GroupersId = grouperId Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then

            If res.GroupersCareGroupCups IsNot Nothing Then

                For Each ObjGroupersCareGroupCups As GroupersCareGroupCups In res.GroupersCareGroupCups

                    Dim Cups = (From c In _context.CUPSEntity Where c.Id = ObjGroupersCareGroupCups.CUPSEntityId Select c).FirstOrDefault()
                    ObjGroupersCareGroupCups.DescriptionCups = Cups.Description
                Next


            End If

            res.OriginalValue = (From d As GroupersCareGroup In Me._context.GroupersCareGroup.AsNoTracking() Where d.CareGroupId = CareGroupId And d.GroupersId = grouperId Select d).SingleOrDefault()
            Return res
        Else
            Return New GroupersCareGroup()
        End If
    End Function


    ''' <summary>
    ''' Lists the care group ids recognition.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCareGroupIdsRecognition(operatingUnitId As Integer) As List(Of Integer) Implements ICareGroupRepository.ListCareGroupIdsRecognition
        Dim query = (From rcd In _context.RevenueControlDetail.AsNoTracking()
                     Join cg In _context.CareGroup.AsNoTracking() On cg.Id Equals rcd.CareGroupId
                     Where rcd.Status = 1 AndAlso cg.OperativeUnitId = operatingUnitId
                     Select rcd.CareGroupId).ToList()
        If query IsNot Nothing Then
            Return query.Distinct().ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
