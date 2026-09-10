CREATE TABLE [Inventory].[ReasonCancellationOfRequest] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [HCFARMEPCId]  NUMERIC (18)  NOT NULL,
    [HCMOANULBId]  CHAR (4)      NOT NULL,
    [Description]  VARCHAR (500) NOT NULL,
    [CreationUser] VARCHAR (20)  NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [HCFARMEPDId]  INT           NULL,
    CONSTRAINT [PK_ReasonCancellationOfRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPC] FOREIGN KEY ([HCFARMEPCId]) REFERENCES [dbo].[HCFARMEPC] ([CODCONCEC]),
    CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPD] FOREIGN KEY ([HCFARMEPDId]) REFERENCES [dbo].[HCFARMEPD] ([ID]),
    CONSTRAINT [FK_ReasonCancellationOfRequest_HCMOANULB] FOREIGN KEY ([HCMOANULBId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU])
);


GO
ALTER TABLE [Inventory].[ReasonCancellationOfRequest] NOCHECK CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPC];


GO
ALTER TABLE [Inventory].[ReasonCancellationOfRequest] NOCHECK CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPD];




GO
ALTER TABLE [Inventory].[ReasonCancellationOfRequest] NOCHECK CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPC];


GO
ALTER TABLE [Inventory].[ReasonCancellationOfRequest] NOCHECK CONSTRAINT [FK_ReasonCancellationOfRequest_HCFARMEPD];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de farmacia a anular; clave foránea a HCFARMEPD, referencia al renglón específico de medicamento/insumo en la solicitud de farmacia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de farmacia a anular ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de anulación; timestamp de auditoría del motivo de cancelación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la anulación; identificación del profesional o administrativo que creó el motivo de cancelación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del motivo de anulación; texto libre que explica por qué se cancela la solicitud o detalle de farmacia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la anulacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo general de anulación; clave foránea a HCMOANULB que clasifica la razón de cancelación (devolución, rechazo, error, etc.)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo de general ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCMOANULBId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/solicitud de farmacia a anular; clave foránea a HCFARMEPC que vincula al documento maestro de la solicitud de medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la solicitud de la tabla [HCFARMEPC]', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'HCFARMEPCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de razón de cancelación; clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivos o razones por las cuales se anula o cancela una solicitud en el módulo de farmacia/inventario. Registra el detalle textual del motivo de anulación, el usuario que la generó y la fecha, vinculando la cancelación a los registros de historia clínica de farmacia correspondientes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ReasonCancellationOfRequest';
