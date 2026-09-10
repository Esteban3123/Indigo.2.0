CREATE TABLE [Budget].[AvailabilityExtensionDetail] (
    [Id]                      INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AvailabilityExtensionId] INT      NOT NULL,
    [AvailabilityId]          INT      NOT NULL,
    [ExtensionDay]            INT      NOT NULL,
    [AvailabilityDate]        DATETIME NOT NULL,
    [ExpirationDate]          DATETIME NOT NULL,
    CONSTRAINT [PK_AvailabilityExtensionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AvailabilityExtensionDetail_Availability] FOREIGN KEY ([AvailabilityId]) REFERENCES [Budget].[Availability] ([Id]),
    CONSTRAINT [FK_AvailabilityExtensionDetail_AvailabilityExtension] FOREIGN KEY ([AvailabilityExtensionId]) REFERENCES [Budget].[AvailabilityExtension] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expiración o vencimiento de la extensión de disponibilidad. Tipo: DATETIME. Marca el momento en que la prórroga de disponibilidad deja de ser válida.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de extensión', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExpirationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de la disponibilidad o extensión. Tipo: DATETIME. Momento desde el cual la disponibilidad (recurso, profesional, unidad funcional) está activa o disponible.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días de extensión o prórroga de disponibilidad. Tipo: INT. Número de días adicionales concedidos para la disponibilidad.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExtensionDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día de extensión', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExtensionDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'ExtensionDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la disponibilidad extendida. Tipo: INT. Referencia a [Budget].[Availability]. Vincula el detalle a su disponibilidad de origen (recurso, profesional, centro de atención).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la extensión o prórroga de disponibilidad. Tipo: INT. Referencia a [Budget].[AvailabilityExtension]. Agrupa detalles de la misma extensión.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityExtensionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de disponibilidad de extensión', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityExtensionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityExtensionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la tabla AvailabilityExtensionDetail. Tipo: INT IDENTITY. Clave primaria que identifica cada registro de detalle de extensión.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las extensiones de disponibilidad presupuestal: registra cada día adicional otorgado a una disponibilidad de presupuesto, con sus fechas de vigencia y vencimiento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AvailabilityExtensionDetail';
