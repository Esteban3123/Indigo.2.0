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
    [ContributorTypeSubtypeId]      INT             NULL,
    CONSTRAINT [PK_Employee__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_Employee_ContributorTypeSubtype] FOREIGN KEY ([ContributorTypeSubtypeId]) REFERENCES [Payroll].[ContributorTypeSubtype] ([Id]),
    CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY ([EmployeeTypeId]) REFERENCES [Payroll].[EmployeeType] ([Id]),
    CONSTRAINT [FK_Employee_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Employee_WorkCenter] FOREIGN KEY ([WorkCenterId]) REFERENCES [Payroll].[WorkCenter] ([Id])
);
GO
CREATE NONCLUSTERED INDEX [IX_Employee__ThirdPartyId]
    ON [Payroll].[Employee]([ThirdPartyId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Employee__CostCenterId]
    ON [Payroll].[Employee]([CostCenterId] ASC);

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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código asegurado CCSS (Caja Costarricense de Seguro Social), identificador único del empleado ante la entidad aseguradora costarricense, VARCHAR(25)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InsuredCCSSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Asegurado de la sucursal de la CCSS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InsuredCCSSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InsuredCCSSCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno de empleado para Costa Rica, identificador alternativo de nómina, VARCHAR(10)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InternalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno para Costa Rica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InternalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InternalCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pensión complementaria o aportes adicionales a fondo de pensiones, NUMERIC(18,2), default 0', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'SupplementaryPension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pensión complementaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'SupplementaryPension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'SupplementaryPension';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación en el extranjero, se registra cuando ContributorAbroad=1, DATETIME', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DateFilingAbroad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicacion en el exterior, se guarda si ContributorAbroad esta en yes o 1', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DateFilingAbroad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DateFilingAbroad';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si cotizante labora en el extranjero (1=Sí, 0=No), cotización internacional, BIT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ContributorAbroad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si/No Cotizante en el exterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ContributorAbroad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ContributorAbroad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK referencia al proceso de selección de candidatos en reclutamiento, INT, relación con [Payroll].[CandidateSelectionProcess]', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Proceso de selección de candidatos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CandidateSelectionProcessId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin de incapacidad permanente, registro de terminación de invalidez, DATE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EndDatePermanentInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Fin Incapacidad Permanente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EndDatePermanentInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EndDatePermanentInability';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de incapacidad permanente, registro de invalidez, DATE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InitialDatePermanentInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio Incapacidad Permanente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InitialDatePermanentInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'InitialDatePermanentInability';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incapacidad permanente del empleado (1=Sí incapacitado, 0=No), estado de invalidez, BIT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PermanentInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incapacitado Permanente  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PermanentInability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PermanentInability';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reubicación laboral del empleado (1=Reubicado, 0=Sin cambio), cambio de puesto o centro, BIT, default 0', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Relocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reubicado (1 - Si, 0 - No)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Relocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Relocation';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte a salud no obligatorio para retención en la fuente, contribución voluntaria, NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'HealthContributorRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aporte a Salud NO Obligatorio para Retefuente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'HealthContributorRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'HealthContributorRTF';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de declarante fiscalmente (1=No Declarante, 2=Declarante), categoría tributaria, TINYINT, default 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DeclarantType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Declarante  1 - No Declarante   2 - Declarante', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DeclarantType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DeclarantType';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del empleado en nómina (1=Activo, 0=Inactivo), BIT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'State';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha última modificación del registro, auditoría, DATE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DateModified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha modificacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DateModified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'DateModified';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación, auditoría, VARCHAR(10)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'UserModified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'UserModified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'UserModified';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de procedimiento para retención en la fuente (1=Procedimiento 1, 2=Procedimiento 2), método tributario, TINYINT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Procedimiento para Retención en la Fuente: 1 - Procedimiento 1; 2 - Procedimiento 2', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ProcedureTypeRTF';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autorización para imprimir referencia laboral desde portal autoservicio del empleado (1=Permitido, 0=No), control de acceso, BIT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AllowJobReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si permiti imprimir la referencia laboral desde el portal autoservicio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AllowJobReference';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AllowJobReference';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella biométrica del empleado, datos biométricos, VARBINARY(MAX)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeFootprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Huella del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeFootprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeFootprint';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta de almacenamiento de foto del empleado, ubicación archivo, VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PhotoPath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicacion foto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PhotoPath';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PhotoPath';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha última liquidación de vacaciones, registro de liquidación, DATE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'VacationLastDateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fech ultima liquidacion vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'VacationLastDateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'VacationLastDateLiquidation';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK centro de trabajo del empleado, relación con [Payroll].[WorkCenter]([Id]), INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'WorkCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'WorkCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'WorkCenterId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK centro de costo para imputación de nómina, relación con [Payroll].[CostCenter]([Id]), INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id centro de costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'CostCenterId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empleado extranjero pensionado (1=Sí, 0=No), jubilación internacional, BIT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'RetiredForeign';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extranjero pensionado, 1 si, 0 no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'RetiredForeign';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'RetiredForeign';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Afiliación sindical (1=Ninguno, 2=Convencionado, 3=Sindicalizado, 4=Pacto Colectivo), TINYINT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'TradeUnion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sindicato  1 - Ninguno  2 - Convencionado  3 - Sindicalizado  4 - Pacto Colectivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'TradeUnion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'TradeUnion';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK tipo de pensionado, categoría pensional, INT, relación con [Payroll].[PensionaryType]', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PensionaryTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de pensionado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PensionaryTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PensionaryTypeId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de pensión (1=Activo, 0=Suspendido), situación prestacional, BIT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PensionaryStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de pension 1 - Activo 0 - suspendido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PensionaryStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'PensionaryStatus';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK tipo de empleado (1=default), relación con [Payroll].[EmployeeType]([Id]), INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID  de tipo de empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EmployeeTypeId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de pensión del empleado (1=Pensionado, 0=No pensionado), BIT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Pensionary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El empleado esta pensionado 1- si 0-no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Pensionary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Pensionary';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor promedio de salud año anterior, base para cálculo de retención, DECIMAL(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AverageYearHealth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor promedio salud año vigencia anterior (para calculo de la retencion)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AverageYearHealth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AverageYearHealth';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje aporte a riesgos profesionales, cobertura ARP, DECIMAL(8,4)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje riesgos profesionales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskPercentage';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor deducción educación/estudios, DECIMAL(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EducationDeductionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Deduccion Estudio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EducationDeductionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'EducationDeductionValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor deducción vivienda/arrendamiento, DECIMAL(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'HousingDeductionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor deduccion vivienda', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'HousingDeductionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'HousingDeductionValue';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha ingreso del empleado a la empresa, inicio de relación laboral, DATE', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AdmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de ingreso del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AdmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'AdmissionDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK identificación del tercero/persona natural empleado, relación con [Common].[ThirdParty]([Id]), INT', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de empleado en nómina, clave primaria, INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de empleados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de empleados de nómina. Contiene la información laboral, contractual y de deducciones de cada trabajador: fecha de ingreso, centro de costo, tipo de empleado, afiliaciones a pensión y salud, riesgos profesionales, vacaciones, incapacidades permanentes y datos de huella o foto.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a la combinación tipo/subtipo de cotizante (PILA). Referencia a [Payroll].[ContributorTypeSubtype]([Id]). INT NULL.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'ContributorTypeSubtypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de vacaciones pendientes o disponibles que le quedan al empleado por disfrutar, saldo de vacaciones acumuladas no tomadas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'RemainingVacationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Employee', @level2type = N'COLUMN', @level2name = N'RemainingVacationDays';
