CREATE TABLE [Payroll].[ManualConcepts] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Process]             TINYINT         CONSTRAINT [DF_ManualConcepts_Process] DEFAULT ((1)) NOT NULL,
    [Consecutive]         INT             NOT NULL,
    [GroupId]             INT             NOT NULL,
    [EmployeeId]          INT             NOT NULL,
    [ContractNumber]      INT             NOT NULL,
    [ContractId]          INT             NOT NULL,
    [CostCenterId]        INT             NOT NULL,
    [BranchOfficeId]      INT             NOT NULL,
    [FunctionalUnitId]    INT             NOT NULL,
    [InitialDate]         DATE            NOT NULL,
    [PayrollInitialDate]  DATE            NOT NULL,
    [PayrollEndingDate]   DATE            NOT NULL,
    [ConceptId]           INT             NOT NULL,
    [Description]         VARCHAR (200)   NOT NULL,
    [PaidEndContract]     BIT             NOT NULL,
    [PaidFormat]          TINYINT         NOT NULL,
    [QuoteNumber]         TINYINT         NOT NULL,
    [QuoteValue]          DECIMAL (18, 2) NULL,
    [State]               TINYINT         NOT NULL,
    [RetentionId]         INT             NULL,
    [RetentionPercentage] DECIMAL (6, 3)  NULL,
    [RetentionBase]       DECIMAL (18)    NULL,
    CONSTRAINT [PK_ManualConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManualConcepts_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ManualConcepts_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_ManualConcepts_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_ManualConcepts_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_BranchOffice];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Concept];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Contract];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_CostCenter];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Employee];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_FunctionalUnit];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Group];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_RetentionConcept];


GO
CREATE NONCLUSTERED INDEX [IX_ManualConcepts__EmployeeId]
    ON [Payroll].[ManualConcepts]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ManualConcepts__ConceptId]
    ON [Payroll].[ManualConcepts]([ConceptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Base de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Activo, 2 - Finalizado, 3 - Suspendido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Cuotas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Cuotas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Pago: 1 - 1era Quincena, 2 - 2da Quincena, 3 - Ambas, 4 - Mensual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidFormat';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pagar Hasta Fin de Contrato (SI / NO)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidEndContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción que se le ingresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha fin de pago con nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha inicio pago con nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio de concepto manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Sucursal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Consecutivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso: 1 - Nómina, 2 - Retroactivo, 3 - Prima de Junio, 4 - Prima de Diciembre, 5 - Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Id';

