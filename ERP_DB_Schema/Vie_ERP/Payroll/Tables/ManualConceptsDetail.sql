CREATE TABLE [Payroll].[ManualConceptsDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ManualConceptId]       INT             NOT NULL,
    [PayrollDateLiquidated] DATE            NOT NULL,
    [Value]                 DECIMAL (18, 2) NULL,
    [State]                 TINYINT         NOT NULL,
    CONSTRAINT [PK_ManualConceptsDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManualConceptsDetail_ManualConcepts] FOREIGN KEY ([ManualConceptId]) REFERENCES [Payroll].[ManualConcepts] ([Id])
);


GO
ALTER TABLE [Payroll].[ManualConceptsDetail] NOCHECK CONSTRAINT [FK_ManualConceptsDetail_ManualConcepts];


GO
CREATE NONCLUSTERED INDEX [IX_ManualConceptsDetail__ManualConceptId]
    ON [Payroll].[ManualConceptsDetail]([ManualConceptId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Esperando Pago, 2 - Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Pagado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Nómina con que se Liquida', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'PayrollDateLiquidated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto Manual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'ManualConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ManualConceptsDetail', @level2type = N'COLUMN', @level2name = N'Id';

