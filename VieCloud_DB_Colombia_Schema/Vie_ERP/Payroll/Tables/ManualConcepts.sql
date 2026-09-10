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



GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_ManualConcepts__EmployeeId]
    ON [Payroll].[ManualConcepts]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ManualConcepts__ConceptId]
    ON [Payroll].[ManualConcepts]([ConceptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Base de retención (DECIMAL 18,0); monto o base sobre el cual se calcula el porcentaje de retención, descuento o deducción en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Base de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención (DECIMAL 6,3); tasa o porcentaje aplicado sobre la base para calcular el monto de retención, descuento o deducción', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de retención (FK → GeneralLedger.RetentionConcepts); identificador del tipo de retención, descuento o deducción aplicada al empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de retencion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'RetentionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del concepto manual (TINYINT): 1=Activo, 2=Finalizado, 3=Suspendido; indica el estado actual de vigencia del concepto en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Activo, 2 - Finalizado, 3 - Suspendido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de cuotas (DECIMAL 18,2); monto monetario de cada cuota o abono en caso de conceptos fraccionados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Cuotas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuotas (TINYINT); cantidad total de cuotas o abonos en que se divide el concepto manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Cuotas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de pago (TINYINT): 1=1ª Quincena, 2=2ª Quincena, 3=Ambas quincenas, 4=Mensual; periodicidad en que se paga el concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidFormat';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Pago: 1 - 1era Quincena, 2 - 2da Quincena, 3 - Ambas, 4 - Mensual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidFormat';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidFormat';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pagar hasta fin de contrato (BIT); indica si el concepto se paga hasta la fecha de terminación del contrato del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidEndContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pagar Hasta Fin de Contrato (SI / NO)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidEndContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PaidEndContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del concepto manual (VARCHAR 200); texto descriptivo ingresado por el usuario para identificar o detallar el concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción que se le ingresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del concepto (FK → Payroll.Concept); referencia al concepto de nómina asociado (bonificación, descuento, prima, etc.)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha fin de pago en nómina (DATE); fecha final hasta la cual el concepto se incluye en el procesamiento de nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha fin de pago con nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicio de pago en nómina (DATE); fecha inicial desde la cual el concepto comienza a procesarse en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha inicio pago con nomina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'PayrollInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicio del concepto manual (DATE); fecha en que entra en vigencia o se activa el concepto manual para el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio de concepto manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de unidad funcional (FK → Payroll.FunctionalUnit); identificador de la unidad funcional, departamento o área de trabajo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de sucursal (FK → Payroll.BranchOffice); identificador de la sede, oficina o centro de atención donde labora el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Sucursal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de centro de costo (FK → Payroll.CostCenter); identificador del centro de costo al que se imputa el gasto del concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del contrato (FK → Payroll.Contract); referencia única al contrato de trabajo asociado al empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del contrato (INT); número o identificación del contrato de trabajo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del empleado (FK → Payroll.Employee); identificador único del empleado al cual se asigna el concepto manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del grupo (FK → Payroll.Group); identificador del grupo o categoría a la que pertenece el concepto o empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo (INT); número secuencial único para identificar el registro dentro del grupo de conceptos manuales', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Consecutivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proceso de nómina (TINYINT): 1=Nómina, 2=Retroactivo, 3=Prima de Junio, 4=Prima de Diciembre, 5=Liquidación de contrato; tipo de proceso en el que se incluye el concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso: 1 - Nómina, 2 - Retroactivo, 3 - Prima de Junio, 4 - Prima de Diciembre, 5 - Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Process';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Process';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autonumérico (INT IDENTITY); identificador único y clave primaria del registro de concepto manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos manuales de nómina: registra los devengados o deducciones ingresados manualmente para un empleado en un período de liquidación, asociados a un contrato, centro de costo, unidad funcional y sucursal específicos. Permite configurar cuotas, retenciones y la forma de pago de cada concepto.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConcepts';
