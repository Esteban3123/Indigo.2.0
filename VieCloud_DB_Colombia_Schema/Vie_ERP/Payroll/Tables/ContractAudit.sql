CREATE TABLE [Payroll].[ContractAudit] (
    [Id]         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractId] INT           NOT NULL,
    [Type]       VARCHAR (100) NOT NULL,
    [FieldName]  VARCHAR (100) NOT NULL,
    [ValueOld]   VARCHAR (MAX) NULL,
    [ValueNew]   VARCHAR (MAX) NULL,
    [UserCode]   VARCHAR (20)  NOT NULL,
    [Date]       DATETIME      NOT NULL,
    CONSTRAINT [PK_ContractAudit] PRIMARY KEY CLUSTERED ([Id] ASC)
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
CREATE NONCLUSTERED INDEX [IX_ContractAudit_Type]
    ON [Payroll].[ContractAudit]([Type] ASC);


GO
ALTER INDEX [IX_ContractAudit_Type]
    ON [Payroll].[ContractAudit] DISABLE;


GO
CREATE NONCLUSTERED INDEX [IX_ContractAudit_Date]
    ON [Payroll].[ContractAudit]([Date] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ContractAudit_ContractId]
    ON [Payroll].[ContractAudit]([ContractId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría de cambios realizados sobre los contratos de nómina. Almacena el historial de modificaciones indicando qué campo fue alterado, el valor anterior y el nuevo, quién realizó el cambio y cuándo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de auditoría.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato de nómina que fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o categoría del cambio realizado sobre el contrato (por ejemplo: creación, modificación, eliminación).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del campo o atributo del contrato que fue modificado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'FieldName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'FieldName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor anterior del campo antes del cambio, valor original previo a la modificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'ValueOld';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'ValueOld';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor nuevo del campo después del cambio, valor actualizado tras la modificación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'ValueNew';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'ValueNew';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que realizó el cambio en el contrato.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó la modificación, marca temporal del cambio.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'Date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractAudit', @level2type = N'COLUMN', @level2name = N'Date';
