CREATE TABLE [Payroll].[PayrollSequenceDetail] (
    [Id]                INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PayrollSequenceId] INT    NOT NULL,
    [IdSequense]        INT    NOT NULL,
    [IdOperatingUnit]   INT    NULL,
    [Next]              BIGINT CONSTRAINT [DF_PayrollSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_PayrollSequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PayrollSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PayrollSequenceDetail_PayrollSequence] FOREIGN KEY ([PayrollSequenceId]) REFERENCES [Payroll].[PayrollSequence] ([Id]),
    CONSTRAINT [FK_PayrollSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_Sequense];




GO



GO



GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_Sequense];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Próximo número a generar en la secuencia de nómina (contador BIGINT, por defecto inicia en 1, usado para numeración automática de recibos/documentos de pago)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad operativa (centro de atención/unidad funcional) asignada a la secuencia; solo poblado cuando el ámbito es UO (Unidad Operativa), en caso contrario es NULL (FK a Common.OperatingUnit)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la secuencia base o plantilla de secuencia (FK a Common.Sequense, referencia a configuración de numeración)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera/encabezado de secuencia de nómina (FK a Payroll.PayrollSequence, relación padre-hijo)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'PayrollSequenceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'PayrollSequenceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'PayrollSequenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de secuencia de nómina (clave primaria, IDENTITY INT)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las secuencias de nómina por unidad operativa. Registra el orden y el siguiente número disponible para cada secuencia dentro de un proceso de liquidación de nómina, permitiendo controlar la numeración consecutiva de los registros de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'PayrollSequenceDetail';
