CREATE TABLE [Payroll].[Contract] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RowType]                      TINYINT       NOT NULL,
    [InitialContractNumber]        INT           NOT NULL,
    [ResolutionNumber]             VARCHAR (50)  NULL,
    [ResolutionDate]               DATE          NULL,
    [PosesionDate]                 DATE          NULL,
    [CertificateOfficeNumber]      VARCHAR (50)  NULL,
    [EmployeeId]                   INT           NOT NULL,
    [PositionId]                   INT           NOT NULL,
    [FunctionalUnitId]             INT           NOT NULL,
    [ContractTypeId]               INT           NOT NULL,
    [JobBondingDate]               DATE          NOT NULL,
    [ContractInitialDate]          DATE          NOT NULL,
    [ContractEndingDate]           DATE          NOT NULL,
    [BasicSalary]                  NUMERIC (18)  NOT NULL,
    [Status]                       TINYINT       NOT NULL,
    [RetirementReasonId]           TINYINT       NULL,
    [RetirementDate]               DATE          NULL,
    [PaymentPeriod]                TINYINT       NOT NULL,
    [PaymentType]                  TINYINT       NOT NULL,
    [TrialPeriod]                  BIT           NOT NULL,
    [TrialPeriodTime]              INT           NULL,
    [TrialPeriodSalaryPercentage]  INT           NULL,
    [ContractCreationDate]         DATETIME      NOT NULL,
    [CreationUserId]               VARCHAR (20)  NOT NULL,
    [ModificationDate]             DATETIME      NOT NULL,
    [TypeOfPensionContribution]    TINYINT       NOT NULL,
    [Valid]                        BIT           NOT NULL,
    [Notes]                        VARCHAR (255) NULL,
    [GroupId]                      INT           NOT NULL,
    [BankId]                       INT           NOT NULL,
    [BankAccountNumber]            VARCHAR (20)  NOT NULL,
    [BankAccountType]              INT           NOT NULL,
    [LiquidationPayroll]           TINYINT       NOT NULL,
    [ContractModificationReasonId] INT           NULL,
    [BaseIncome]                   DECIMAL (18)  NULL,
    [IncomeDailyBase]              INT           NULL,
    [HoursDaily]                   TINYINT       NOT NULL,
    [LastLiquidationDate]          DATE          NULL,
    [ModificationUserId]           VARCHAR (20)  NOT NULL,
    [LastModificationDate]         DATE          NOT NULL,
    [Contingency]                  TINYINT       CONSTRAINT [DF_Contract_Contingency] DEFAULT ((0)) NULL,
    [OrganizationChartPositionId]  INT           NULL,
    [WorkPlaceId]                  INT           NULL,
    [InsuredCCSSCode]              VARCHAR (25)  NULL,
    [ParameterACCAI]               BIT           DEFAULT ((0)) NULL,
    [IsExtemporaneousChange]       BIT           CONSTRAINT [DF_Contract_IsExtemporaneousChange] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Contract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contract_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_Contract_ContractModificationReason] FOREIGN KEY ([ContractModificationReasonId]) REFERENCES [Payroll].[ContractModificationReason] ([Id]),
    CONSTRAINT [FK_Contract_ContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Payroll].[ContractType] ([Id]),
    CONSTRAINT [FK_Contract_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Contract_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_Contract_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_Contract_OrganizationChartPosition] FOREIGN KEY ([OrganizationChartPositionId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_Contract_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_Contract_RetirementReason] FOREIGN KEY ([RetirementReasonId]) REFERENCES [Payroll].[RetirementReason] ([Id]),
    CONSTRAINT [FK_Contract_WorkPlace] FOREIGN KEY ([WorkPlaceId]) REFERENCES [HumanTalent].[WorkPlace] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Contract__EmployeeId]
    ON [Payroll].[Contract]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Contract__PositionId]
    ON [Payroll].[Contract]([PositionId] ASC);


GO
ALTER INDEX [IX_Contract__PositionId]
    ON [Payroll].[Contract] DISABLE;




GO
CREATE NONCLUSTERED INDEX [iFunctionalUnitId_Payroll_Contract_5A9740F2]
    ON [Payroll].[Contract]([FunctionalUnitId] ASC, [Valid] ASC)
    INCLUDE ([EmployeeId], [PositionId]);




GO
CREATE NONCLUSTERED INDEX [IX_Contract__GroupId]
    ON [Payroll].[Contract]([GroupId] ASC);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si el cambio/modificación del contrato es extemporáneo (anterior al mes actual). 1=Sí, 0=No. Referencia retroactiva de vigencia contractual.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'IsExtemporaneousChange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el cambio de contrato es extemporáneo (fecha anterior al mes actual). 1 = Sí, 0 = No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'IsExtemporaneousChange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'IsExtemporaneousChange';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del lugar de trabajo/sede donde labora el empleado, registrado desde módulo Talento Humano. FK→HumanTalent.WorkPlace.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'WorkPlaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lugar de trabajo, se registra desde talento humano.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'WorkPlaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'WorkPlaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador de la posición en el Organigrama/estructura jerárquica. Se completa solo cuando integración Talento Humano está activa. FK→HumanTalent.OrganizationChartPosition.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la posición del Organigrama. Únicamente se llena, cuando el sistema está integrado con Talento Humano', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Tipo de contrato de contingencia/cobertura: 0=Ninguna, 1=Licencias, 2=Vacaciones, 3=Incapacidades/Permisos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Contingency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contrato de Contigencia:  0 - Ninguna  1 - Licencias  2 - Vacaciones  3 - Incapacidades', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Contingency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Contingency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de última modificación/actualización del registro del contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LastModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LastModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LastModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Identificador del usuario que realizó la última modificación del contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del usuario que modifico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de última liquidación/nómina pagada al empleado. Referencia para cálculo de aportes.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultima fecha de liquidacion de nomina del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Número de horas laborales diarias que trabaja el empleado según contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'HoursDaily';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de horas laborales diarias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'HoursDaily';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'HoursDaily';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Ingreso base diario para cálculo de cotizaciones/aportes. Base para liquidación de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'IncomeDailyBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso base cotizacion diario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'IncomeDailyBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'IncomeDailyBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18). Ingreso base de cotización mensual. Referencia para cálculo de prestaciones sociales y pensión.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BaseIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso base cotizacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BaseIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BaseIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador de la razón/motivo de modificación del contrato. FK→Payroll.ContractModificationReason.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractModificationReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Razones de Modificación del Contrato (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractModificationReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractModificationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Indica si el contrato genera liquidación de nómina: 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LiquidationPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion nomina 1-Si 0-No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LiquidationPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'LiquidationPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Tipo de cuenta bancaria donde se deposita nómina: 1=Ahorros, 2=Corriente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankAccountType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cuenta: 1 - Ahorros 2 - Corriente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankAccountType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankAccountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Número de cuenta bancaria del empleado para consignación de salario. PII Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero cuenta bancaria empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del banco donde el empleado tiene cuenta de nómina. FK→Payroll.Bank.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del banco donde tiene la cuenta de nomina (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del grupo/categoría de empleados para clasificación de nómina. FK→Payroll.Group.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(255). Notas, observaciones o comentarios adicionales sobre el contrato y sus condiciones especiales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Notes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notas o Comentarios del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Notes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Notes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si el contrato es vigente/activo. 1=Vigente, 0=No vigente. Solo un contrato vigente por empleado en simultaneidad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Valid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica Si un Contrato es Vigente o No. 1-Vigente 0- No Solo debe haber un contrato vigente por empleado asi tenga dos contratos temporalmente activos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Valid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Valid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Tipo de cotizante pensional: 1=Normal, 2=Especial, 3=Alto riesgo, 4=Servidor público.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cotizante pension  1 - Normal  2 - Especial  3 - Alto riesgo  4 - Servidor publico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha efectiva en que entra en vigencia la modificación contractual.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual entra en vigencia la modificaion del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20). Identificador del usuario que creó/registró el contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que modifico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CreationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora de creación del registro del contrato en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractCreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractCreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractCreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Porcentaje del sueldo base a pagar durante período de prueba (ej: 80% = 80).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriodSalaryPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del sueldo a pagar durante el periodo de prueba', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriodSalaryPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriodSalaryPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Duración del período de prueba en días. Plazo de evaluación inicial del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriodTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de periodo de pruebas en dias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriodTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriodTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si el empleado está en período de prueba: 1=Sí, 0=No. Período evaluativo inicial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta en periodo de prueba 1 - Si 0 - No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TrialPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Forma de pago de nómina: 1=Cheque, 2=Efectivo, 3=Consignación bancaria, 4=Desprendible.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de pago: 1 - Cheque 2 - Efectivo 3 - Consignacion 4 - Desprendible', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Período/frecuencia de pago: 1=Mensual, 2=Quincenal.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo de Pago: 1 Mensual - 2 Quincenal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de retiro/desvinculación del empleado. Registrada si hubo liquidación contractual.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RetirementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RetirementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RetirementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Identificador del motivo de retiro/desvinculación del empleado. FK→Payroll.RetirementReason. Se completa en liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Motivo de Retiro (FK). En caso de que el usuario le hayan realizado Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Estado del contrato: 1=Activo, 2=Liquidado, 3=Anulado, 4=Reemplazado, 5=Parcialmente retirado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Contrato:1-Activo  2-Liquidado 3-Anulado 4-Reemplazado 5-Parcialmente Retirado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18). Sueldo/salario base mensual del empleado. Referencia para cálculo nómina y prestaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sueldo Basico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de finalización/vencimiento del contrato. Referencia para contratos a plazo fijo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de inicio/vigencia del contrato. Punto de partida para cálculos de antigüedad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de vinculación laboral inicial del empleado a la institución.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'JobBondingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vinculacion laboral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'JobBondingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'JobBondingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del tipo de contrato (indefinido, plazo fijo, obra labor, etc.). FK→Payroll.ContractType.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id del tipo de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador de la unidad funcional/departamento donde labora el empleado. FK→Payroll.FunctionalUnit.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del cargo/nivel de la posición del empleado. FK→Payroll.Position.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del nivel de cargo (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del empleado titular del contrato. PII referencial. FK→Payroll.Employee.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50). Número del Acta de Posesión. Aplica para entidades públicas; documenta toma de posesión.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CertificateOfficeNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Acta Posesión', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CertificateOfficeNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'CertificateOfficeNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de Posesión/toma de cargo. Solo para Entidades Públicas; registro de acta oficial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PosesionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Posesión (Únicamente para Entidades Públicas)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PosesionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'PosesionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATE. Fecha de la Resolución de nombramiento/contratación en entidades públicas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50). Número de Resolución administrativa de nombramiento/contratación pública.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Resolucion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Número del contrato base/inicial. Referencia al primer contrato del cual derivan novedades.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nùmero Contrato Inicial o Base - Hace referencia al id del primer contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Tipo de registro: 1=Contrato base/original, 2=Novedad de contrato (modificación).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RowType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Registro 1- Contrato Base 2- Novedad Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RowType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'RowType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. Identificador único del contrato. Clave primaria del registro contractual.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos laborales del personal: registra cada vínculo contractual de un empleado con la organización, incluyendo tipo de contrato, cargo, salario básico, período de prueba, datos bancarios para pago de nómina, fechas de inicio y fin, y motivos de retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del asegurado ante la Caja Costarricense de Seguro Social (CCSS); número de aseguramiento o afiliación a la seguridad social del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InsuredCCSSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'InsuredCCSSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador que señala si el contrato aplica el parámetro ACCAI (Asociación Costarricense de Contadores e Auditores Internos u organismo regulador equivalente); activa o desactiva reglas especiales de cálculo o reporte asociadas a esa entidad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ParameterACCAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'ParameterACCAI';
