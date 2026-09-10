CREATE TABLE [dbo].[AGFARMEONCOD] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGFARMAONCOC] INT             NOT NULL,
    [CODPRODUC]      CHAR (20)       NOT NULL,
    [CANPEDPRO]      INT             NOT NULL,
    [CANENTPRO]      INT             NOT NULL,
    [CANPENPRO]      INT             NOT NULL,
    [ESTADO]         INT             NOT NULL,
    [TIPO]           INT             NOT NULL,
    [CODVIAADM]      VARCHAR (20)    NULL,
    [CODUNIMED]      VARCHAR (20)    NULL,
    [DOSIS]          NUMERIC (18, 2) NULL,
    [OBSERVACI]      VARCHAR (200)   NULL,
    CONSTRAINT [PK_AGFARMEONCOD__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGCITAPROD_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_AGCITAPROD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_AGCITAPROD_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_AGFARMEONCOD_AGFARMEONCOC] FOREIGN KEY ([IDAGFARMAONCOC]) REFERENCES [dbo].[AGFARMEONCOC] ([ID])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales sobre el medicamento, insumo oncológico o su administración (VARCHAR 200, PII sensible)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento o insumo a administrar, expresada en unidades de medida (NUMERIC 18,2, ej: mg, ml, unidades)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida del producto: miligramos, mililitros, unidades, comprimidos, etc. (FK → INUNIMEDI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración: oral, intravenosa, intramuscular, tópica, etc. (FK → HCVIAADMI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Via Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del producto: 1=Medicamento farmacéutico, 2=Medicamento como insumo oncológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Medicamentos  2 - Medicamento Como Insumo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del producto en la solicitud oncológica: 1=Pendiente de entregar, 2=Entregado, 3=Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Producto:  1. Pendiente  2. Entregado  3. Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad aún pendiente de entregar del producto solicitado (INT, diferencia entre solicitado y entregado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPENPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pendiente de Entregar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPENPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPENPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad ya entregada o dispensada del producto en la solicitud oncológica (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Entregada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total solicitada o pedida del producto medicamento/insumo oncológico (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico o insumo oncológico en catálogo (FK → IHLISTPRO, CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación a la solicitud cabecera de medicamentos/insumos oncológicos (FK → AGFARMEONCOC.ID)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'IDAGFARMAONCOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion tabla cabecera de Solicitudes medicamento/insumos oncología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'IDAGFARMAONCOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'IDAGFARMAONCOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de cada línea de producto en la solicitud oncológica (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos/medicamentos oncológicos asociados a una orden o fórmula de farmacia oncológica. Registra cada ítem solicitado, las cantidades pedidas, entregadas y pendientes, junto con la dosis, vía de administración y unidad de medida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGFARMEONCOD';
