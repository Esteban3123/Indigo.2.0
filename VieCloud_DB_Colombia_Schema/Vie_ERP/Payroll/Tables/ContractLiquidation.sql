CREATE TABLE [Payroll].[ContractLiquidation] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]         INT             NOT NULL,
    [ContractId]         INT             NOT NULL,
    [RetirementDate]     DATE            NOT NULL,
    [RetirementReasonId] TINYINT         NOT NULL,
    [Status]             NCHAR (1)       NOT NULL,
    [TotalAccrued]       DECIMAL (18, 2) NOT NULL,
    [TotalDeducted]      DECIMAL (18, 2) NOT NULL,
    [TotalPaid]          DECIMAL (18, 2) NOT NULL,
    [ResolutionNumber]   VARCHAR (50)    NULL,
    [ResolutionDate]     DATE            NULL,
    CONSTRAINT [PK_ContractLiquidation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractLiquidation_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ContractLiquidation_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_ContractLiquidation_RetirementReason] FOREIGN KEY ([RetirementReasonId]) REFERENCES [Payroll].[RetirementReason] ([Id])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ContractLiquidation__RetirementReasonId]
    ON [Payroll].[ContractLiquidation]([RetirementReasonId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ContractLiquidation__ContractId]
    ON [Payroll].[ContractLiquidation]([ContractId] ASC);


GO
CREATE NONCLUSTERED INDEX [iEmployeeId_Payroll_ContractLiquidation_487B499E]
    ON [Payroll].[ContractLiquidation]([EmployeeId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la resolución (DATE, nullable). Día en que se expide el acto administrativo que respalda la liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de resolución o acto administrativo (VARCHAR 50, nullable). Referencia de autorización o disposición que formaliza la liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Resolución', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total pagado o neto liquidado (DECIMAL 18,2). Monto final entregado al empleado: devengado menos deducido.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total deducido en descuentos (DECIMAL 18,2). Suma de retenciones, aportes, afiliaciones y descuentos legales aplicados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total devengado o acumulado (DECIMAL 18,2). Sumatoria de salarios, comisiones, bonificaciones y beneficios adeudados hasta la fecha de retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la liquidación (NCHAR 1: ''''C'''' confirmada, '''''''' sin confirmar). Indica si la liquidación fue procesada o está pendiente de validación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmado "C" o sin confirmar ""', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón de retiro o desvinculación (TINYINT, FK → Payroll.RetirementReason). Motivo: renuncia, despido, jubilación, vencimiento, etc.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razón de Retiro (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de retiro o término del contrato (DATE). Día en que el empleado cesa funciones o se desvincula.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'RetirementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato laboral (INT, FK → Payroll.Contract). Referencia el contrato a liquidar.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Contrato (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado (INT, FK → Payroll.Employee). Vincula al empleado cuyo contrato se liquida.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la liquidación de contrato (INT, PK). Referencia el proceso de liquidación de nómina al retiro del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la liquidacion de contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquidaciones de contratos laborales de empleados: registra el cierre económico de cada contrato al momento del retiro, incluyendo los valores devengados, deducidos y pagados, junto con la causa del retiro y la resolución que lo respalda.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidation';
