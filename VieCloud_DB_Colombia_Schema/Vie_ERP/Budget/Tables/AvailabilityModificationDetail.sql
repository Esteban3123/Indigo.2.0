CREATE TABLE [Budget].[AvailabilityModificationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AvailabilityModificationId] INT             NOT NULL,
    [AvailabilityDetailId]       INT             NOT NULL,
    [Nature]                     TINYINT         NOT NULL,
    [Value]                      NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_AvailabilityModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailabilityModificationDetail_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_AvailabilityModificationDetail_AvailabilityModification] FOREIGN KEY ([AvailabilityModificationId]) REFERENCES [Budget].[AvailabilityModification] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18,2) del movimiento de disponibilidad presupuestaria; monto a débito o crédito según naturaleza del ajuste', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento: tipo de transacción (1=Débito/salida, 2=Crédito/entrada) que clasifica si aumenta o disminuye la disponibilidad presupuestaria', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza (Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de disponibilidad presupuestaria asociado; referencia a Budget.AvailabilityDetail para trazabilidad del rubro presupuestario modificado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del detalle de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera/encabezado de la modificación presupuestaria; vincula este detalle con el ajuste general de disponibilidad registrado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de la modificacion de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de modificación presupuestaria; clave primaria que singulariza cada línea de movimiento de disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la modificacion de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las modificaciones realizadas a la disponibilidad presupuestal. Registra cada línea de ajuste (incremento o reducción) aplicada sobre un rubro de disponibilidad dentro de una solicitud de modificación presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityModificationDetail';
