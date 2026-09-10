'***********************************************************************
' Assembly         : DistributedService.Payroll
' Author           : Cristhian Salazar
' Created          : 07/07/2013
'
' Last Modified By : Daniel Arevalo 
' Last Modified On : 07-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities

#End Region


<ServiceContract()>
Public Interface IPayrollService
    Inherits IPayrollEducationLevels, IPayrollFunds, IPayrollGroup
    Inherits IPayrollPositionLevel, IPayrollProfessions, IPayrollRetirementReason, IPayrollLanguage, IPayrollCompany, IPayrollKinship, IPayrollCommon
    Inherits IPayrollBranchOffice, IPayrollFunctionalUnit, IPayrollCostCenter, IPayrollBank, IPayrollPensionaryType, IPayrollWorkCenter, IPayrollStudyType
    Inherits IPayrollContractGroup, IPayrollProfessionalRisk, IPayrollStudyCenter, IPayrollContributorType, IPayrollContractType, IPayrollContractTemplate, IPayrollPosition, IPayrollRetention
    Inherits IPayrollNovelty, IPayrollConcept, IPayrollEmployee, IPayrollJobBondingType, IPayrollScheduleTemplate, IPayrollParameter, IPayrollSchedule, IPayrollAuthorizationConcept, IPayrollLiquidation
    Inherits IPayrollScheduleDetail, IPayrollEmployeeType, IPayrollContractModificationReason, IPayrollAutoliquidation, IPayrollVacationPeriod, IPayrollUnemployedLiquidation, IPayrollKindsAgreements, IPayrollAgreements
    Inherits IPayrollIncentivePayment, IPayrollContractLiquidation, IPayrollManualConcepts, IPayrollBankFile, IPayrollBankFileDetail, IPayrollAuditoryBankFile, IPayrollAccountingStructure, IPayrollConceptAccountingStructure
    Inherits IPayrollCostDistribution, IPayrollNationalSavingsFund, IPayrollVacationRequest, IPayrollIncreaseSalary, IPayrollSequence, IPayrollTradeUnion, IPayrollBlockRecordPayroll, IPayrollSettings
    Inherits IPayrollResumptionHoliday, IPayrollInitialBalancePayroll, IPayrollBlockSchedule, IPayrollIAgreementsMassive, IPayrollSportPractice, IPayrollFreeTimeUse, IPayrollDiagnosedDisease, IPayrollForeclousure, IPayrollFileForeclousure
    Inherits IPayrollReligiousBeliefs, IPayrollEthnicGroups, IPayrollPermissionSchedule, IPayrollMassiveContract, IPayrollElectronicPayroll, IPayrollMinimumSalary, IPayrollContributorSubtype, IPayrollLicensingConcepts, IPayrollMaritalStatus, IPayrollHumanTalentParameterization
    Inherits IPayrollElectronicPayrollConcepts, IPayrollIVacationSummary
    Inherits IPayrollContributorTypeSubtype
End Interface
