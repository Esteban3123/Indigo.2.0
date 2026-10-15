CREATE TABLE [Payroll].[Employee] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ThirdPartyId]                  INT             NOT NULL,
    [AdmissionDate]                 DATE            NOT NULL,
    [HousingDeductionValue]         DECIMAL (18)    NULL,
    [EducationDeductionValue]       DECIMAL (18)    NULL,
    [ProfessionalRiskPercentage]    DECIMAL (8, 4)  NULL,
    [AverageYearHealth]             DECIMAL (18)    NULL,
    [Pensionary]                    BIT             NOT NULL,
    [EmployeeTypeId]                INT             CONSTRAINT [DF_Employee_EmployeeTypeId] DEFAULT ((1)) NULL,
    [PensionaryStatus]              BIT             NULL,
    [PensionaryTypeId]              INT             NULL,
    [TradeUnion]                    TINYINT         NOT NULL,
    [RetiredForeign]                BIT             NULL,
    [CostCenterId]                  INT             NULL,
    [WorkCenterId]                  INT             NULL,
    [VacationLastDateLiquidation]   DATE            NOT NULL,
    [PhotoPath]                     VARCHAR (100)   NULL,
    [EmployeeFootprint]             VARBINARY (MAX) NULL,
    [AllowJobReference]             BIT             CONSTRAINT [DF_Employee_AllowJobReference] DEFAULT ((1)) NOT NULL,
    [ProcedureTypeRTF]              TINYINT         NULL,
    [UserModified]                  VARCHAR (10)    NOT NULL,
    [DateModified]                  DATE            NOT NULL,
    [State]                         BIT             NOT NULL,
    [DeclarantType]                 TINYINT         CONSTRAINT [DF_Employee_DeclarantType] DEFAULT ((2)) NOT NULL,
    [HealthContributorRTF]          NUMERIC (18)    NULL,
    [Relocation]                    BIT             CONSTRAINT [DF_Employee_Relocation] DEFAULT ((0)) NULL,
    [PermanentInability]            BIT             CONSTRAINT [DF_Employee_PermanentInability] DEFAULT ((0)) NULL,
    [InitialDatePermanentInability] DATE            NULL,
    [EndDatePermanentInability]     DATE            NULL,
    [CandidateSelectionProcessId]   INT             NULL,
    [ForeignobligedQuotePension]    BIT             CONSTRAINT [DF_Employee_ForeignobligedQuotePension] DEFAULT ((0)) NOT NULL,
    [ContributorAbroad]             BIT             CONSTRAINT [DF__Employee__Contri__18A8C27E] DEFAULT ((0)) NOT NULL,
    [DateFilingAbroad]              DATETIME        NULL,
    [SupplementaryPension]          NUMERIC (18, 2) CONSTRAINT [DF__Employee__Supple__58AE38FA] DEFAULT ((0)) NOT NULL,
    [InternalCode]                  VARCHAR (10)    NULL,
    [RemainingVacationDays]         INT             CONSTRAINT [DF_Employee_RemainingVacationDays] DEFAULT ((0)) NOT NULL,
    [InsuredCCSSCode]               VARCHAR (25)    NULL,
    CONSTRAINT [PK_Employee__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY ([EmployeeTypeId]) REFERENCES [Payroll].[EmployeeType] ([Id]),
    CONSTRAINT [FK_Employee_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Employee_WorkCenter] FOREIGN KEY ([WorkCenterId]) REFERENCES [Payroll].[WorkCenter] ([Id])
);


GO
ALTER TABLE [Payroll].[Employee] NOCHECK CONSTRAINT [FK_Employee_CostCenter];


GO
ALTER TABLE [Payroll].[Employee] NOCHECK CONSTRAINT [FK_Employee_EmployeeType];


GO
ALTER TABLE [Payroll].[Employee] NOCHECK CONSTRAINT [FK_Employee_ThirdParty];


GO
ALTER TABLE [Payroll].[Employee] NOCHECK CONSTRAINT [FK_Employee_WorkCenter];


GO
CREATE TRIGGER [Payroll].[CreateUserPortal]
	ON [Payroll].[Employee]
	AFTER INSERT
AS
BEGIN
	IF EXISTS 
	(
		SELECT 1 
		FROM INSERTED i
		JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
		LEFT JOIN SelfService.PortalUser pu ON tp.Nit = UserCode
		WHERE pu.Id IS NULL
	)
	BEGIN
		-- Insertamos en la Tabla de Usuarios del Portal
		INSERT INTO SelfService.PortalUser 
			(UserCode, [Password], LockedUser, [Status])
			SELECT DISTINCT tp.Nit, '85939fe85efe7afcf72e5bdac8e2e0c80ee54c96', 0, 1
			FROM INSERTED i
			JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
			LEFT JOIN SelfService.PortalUser pu ON tp.Nit = UserCode
			WHERE pu.Id IS NULL
	END

	IF EXISTS 
	(
		SELECT 1 
		FROM INSERTED i
		JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
		JOIN SelfService.PortalUser pu ON tp.Nit = UserCode
		LEFT JOIN SelfService.PortalUserCompany puc ON pu.Id = puc.PortalUserId
		WHERE puc.Id IS NULL
	)
	BEGIN
		-- AHORA INSERTO EN LA TABLA DE SelfService.PortalUserCompany
		INSERT INTO SelfService.PortalUserCompany 
			(PortalUserId, CompanyName, TransactionalContainer, EmployeeId)
			SELECT DISTINCT pu.Id, 'DESARROLLO', 'INDIGO999', i.Id
			FROM INSERTED i
			JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
			JOIN SelfService.PortalUser pu ON tp.Nit = UserCode
			LEFT JOIN SelfService.PortalUserCompany puc ON pu.Id = puc.PortalUserId
			WHERE puc.Id IS NULL
	END

	IF EXISTS 
	(
		SELECT 1 
		FROM INSERTED i
		JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
		JOIN SelfService.PortalUser pu ON tp.Nit = UserCode
		LEFT JOIN SelfService.RolePortalUser rpu ON pu.Id = rpu.PortalUserId
		WHERE rpu.Id IS NULL
	)
	BEGIN
		-- Ahora inserto en la tabla de Roles
		INSERT INTO SelfService.RolePortalUser 
			(PortalUserId, RoleId)
			SELECT DISTINCT pu.Id, 2
			FROM INSERTED i
			JOIN Common.ThirdParty tp ON i.ThirdPartyId = tp.Id
			JOIN SelfService.PortalUser pu ON tp.Nit = UserCode
			LEFT JOIN SelfService.RolePortalUser rpu ON pu.Id = rpu.PortalUserId
			WHERE rpu.Id IS NULL
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Asegurado de la sucursal de la CCSS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InsuredCCSSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo Interno para Costa Rica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InternalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pensión complementaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'SupplementaryPension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pensión extranjera obligada a cotizar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ForeignobligedQuotePension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Proceso de selección de candidatos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incapacitado Permanente  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PermanentInability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Declarante  1 - No Declarante   2 - Declarante', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DeclarantType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sindicato  1 - Ninguno  2 - Convencionado  3 - Sindicalizado  4 - Pacto Colectivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'TradeUnion';

