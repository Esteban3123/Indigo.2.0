CREATE TABLE [Payroll].[BankFileDetail] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankFileId]                INT           NOT NULL,
    [LiquidationId]             INT           NULL,
    [GroupId]                   INT           NOT NULL,
    [PositionId]                INT           NOT NULL,
    [FunctionalUnitId]          INT           NOT NULL,
    [ContractId]                INT           NOT NULL,
    [EmployeeId]                INT           NOT NULL,
    [EmployeeBankId]            INT           NOT NULL,
    [EmployeeBankTypeAccount]   INT           NOT NULL,
    [EmployeeBankAccountNumber] VARCHAR (100) NOT NULL,
    [BasicSalary]               NUMERIC (18)  NOT NULL,
    [TotalAccrued]              NUMERIC (18)  NOT NULL,
    [TotalDeducted]             NUMERIC (18)  NOT NULL,
    [TotalPaid]                 NUMERIC (18)  NOT NULL,
    [IncentivePaymentId]        INT           NULL,
    CONSTRAINT [PK_BankFileDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankFileDetail_Bank] FOREIGN KEY ([EmployeeBankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_BankFileDetail_BankFile] FOREIGN KEY ([BankFileId]) REFERENCES [Payroll].[BankFile] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_BankFileDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_BankFileDetail_IncentivePayment] FOREIGN KEY ([IncentivePaymentId]) REFERENCES [Payroll].[IncentivePayment] ([Id]),
    CONSTRAINT [FK_BankFileDetail_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BankFileDetail__BankFileId__LiquidationId]
    ON [Payroll].[BankFileDetail]([BankFileId] ASC, [LiquidationId] ASC) WHERE ([LiquidationId] IS NOT NULL);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total pagado al empleado (monto neto transferido a cuenta bancaria), NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalPaid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalPaid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total deducido de nómina (retenciones, aportes, descuentos), NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalDeducted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalDeducted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total devengado o causado (salario bruto antes de deducciones), NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario básico contractual del empleado (remuneración base), NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario Basico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta bancaria del empleado (IBAN/identificador de cuenta), VARCHAR(100), PII Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuenta bancaria (corriente/ahorros/nómina), INT, FK→Payroll.Bank', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankTypeAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cuenta bancaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankTypeAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankTypeAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del banco del empleado (entidad financiera), INT, FK→Payroll.Bank', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del banco del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeBankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del empleado (clave foránea), INT, FK→Payroll.Employee', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato laboral vinculado, INT, FK→Payroll.Contract', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional/centro de atención, INT, FK→Payroll.FunctionalUnit', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cargo o posición del empleado, INT, FK→Payroll.Position', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cargo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de nómina del empleado, INT, FK→Payroll.Group', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la liquidación de nómina asociada, INT, FK→Payroll.Liquidation (nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'LiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'LiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'LiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/archivo bancario padre, INT, FK→Payroll.BankFile', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BankFileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BankFileId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'BankFileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de detalle de archivo bancario, INT IDENTITY(1,1)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los registros incluidos en un archivo bancario de nómina. Contiene la información de pago por empleado: cuenta bancaria, salario, total devengado, deducciones y valor neto a transferir en cada dispersión de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BankFileDetail';
