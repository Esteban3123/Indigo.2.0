CREATE TABLE [dbo].[SOLDETALL] (
    [DETAAUTON] INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTON]  INT          NOT NULL,
    [PROAUTON]  INT          NOT NULL,
    [DETCANSOL] INT          NOT NULL,
    [CATEAUTON] INT          NOT NULL,
    [DETCANCOM] INT          NULL,
    [ORDCONSEC] VARCHAR (50) NULL,
    [AUTOCOTI]  INT          NULL,
    [DETCANAUT] INT          NULL,
    CONSTRAINT [PK_SOLDETALL] PRIMARY KEY CLUSTERED ([DETAAUTON] ASC),
    CONSTRAINT [FK_SOLDETALL_SOLCATEGO] FOREIGN KEY ([CATEAUTON]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON]),
    CONSTRAINT [FK_SOLDETALL_SOLCOMPRA] FOREIGN KEY ([COMAUTON]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON]),
    CONSTRAINT [FK_SOLDETALL_SOLDETALL] FOREIGN KEY ([DETAAUTON]) REFERENCES [dbo].[SOLDETALL] ([DETAAUTON]),
    CONSTRAINT [FK_SOLDETALL_SOLPRODUC] FOREIGN KEY ([PROAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad autorizada del producto en el detalle de solicitud de compra. INT, referencia a cantidad aprobada por autorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la cantidad autorizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) de la cotización asociada al detalle. INT, FK implícita a tabla de cotizaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el Autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo o referencia de la orden de compra generada. VARCHAR(50), trazabilidad de compras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el consecutivo de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad comprada/adquirida del producto en este detalle. INT NULL, cantidad real de ejecución de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la cantidad comprada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) de la categoría del producto. INT, FK a [SOLCATEGO], clasificación de artículos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'CATEAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'CATEAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'CATEAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada del producto en la solicitud original. INT, demanda inicial antes de autorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene  la cantidad de la solicitud del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) del producto. INT, FK a [SOLPRODUC], referencia a catálogo de artículos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'PROAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'PROAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'PROAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) de la solicitud de compra padre. INT NOT NULL, FK a [SOLCOMPRA], encabezado de la transacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene el Autonumerico de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'COMAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autoincremental) del detalle de solicitud de compra. INT IDENTITY, PK de la tabla, línea individual de la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETAAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETAAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETAAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de solicitudes de compra o aprovisionamiento: registra cada ítem o línea de una solicitud, indicando cantidades solicitadas, comprometidas y autorizadas, vinculando la solicitud con su producto, categoría y cotización correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL';
