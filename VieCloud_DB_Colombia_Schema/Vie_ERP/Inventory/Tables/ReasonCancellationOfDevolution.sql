CREATE TABLE [Inventory].[ReasonCancellationOfDevolution] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [HCDEVMEDCId]  INT           NOT NULL,
    [HCMOANULBId]  CHAR (4)      NOT NULL,
    [Description]  VARCHAR (500) NOT NULL,
    [CreationUser] VARCHAR (20)  NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [HCDEVMEDDId]  INT           NULL,
    CONSTRAINT [PK_ReasonCancellationOfDevolution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReasonCancellationOfDevolution_HCDEVMEDC] FOREIGN KEY ([HCDEVMEDCId]) REFERENCES [dbo].[HCDEVMEDC] ([CODCONCEC]),
    CONSTRAINT [FK_ReasonCancellationOfDevolution_HCDEVMEDD] FOREIGN KEY ([HCDEVMEDDId]) REFERENCES [dbo].[HCDEVMEDD] ([Id]),
    CONSTRAINT [FK_ReasonCancellationOfDevolution_HCMOANULB] FOREIGN KEY ([HCMOANULBId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de devolución de farmacia a anular, referencia a la línea específica del movimiento de medicamentos, FK a HCDEVMEDD (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de farmacia a anular ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de cancelación de devolución, timestamp de auditoría (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la cancelación de devolución, login del operador (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o justificación de la anulación de devolución de farmacia, motivo específico del proceso (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la anulacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo general de anulación, categoría de razón de cancelación, FK a HCMOANULB (CHAR 4)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo de general ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de solicitud de devolución de farmacia, referencia principal del movimiento, FK a HCDEVMEDC (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la solicitud de la tabla [HCFARMEPC]', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'HCDEVMEDCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de cancelación de devolución, clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivos o razones por las cuales se anuló o canceló una devolución de medicamentos en inventario. Registra el detalle de la anulación, el usuario que la realizó y las referencias a la devolución y al movimiento de anulación correspondientes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfDevolution';
