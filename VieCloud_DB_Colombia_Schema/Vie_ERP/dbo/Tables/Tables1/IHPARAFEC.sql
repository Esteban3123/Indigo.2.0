CREATE TABLE [dbo].[IHPARAFEC] (
    [CODCONSEC] NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)    NOT NULL,
    [FECINIINV] DATETIME     NOT NULL,
    [FECFININV] DATETIME     NOT NULL,
    [CONFSUMIN] BIT          NOT NULL,
    [VEPRONPOS] CHAR (1)     NULL,
    CONSTRAINT [PK_IHPARAFEC] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_IHPARAFEC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetro de control y verificación de cantidades despachadas de productos NO POS (no cubiertos por plan). Valores: 0=Sin validación, 1=Advertencia al sobrepasar justificación, 2=Bloqueo de despacho si se excede cantidad autorizada. Glosa preventiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'VEPRONPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro de verificacion de cantidades despachadas de productos NO POS  0: Ninguno  1: Advertir cuando se sobrepase la cantidad de producto justificada  2: No permitir el despacho de productos cuando se supere la cantidad justificada ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'VEPRONPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'VEPRONPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (Sí/No) que indica si el centro confirma, sincroniza o envía la confirmación de suministros, insumos y productos al ERP. Controla integración de datos farmacéuticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CONFSUMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si realiza o no la confirmacion de suministros al ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CONFSUMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CONFSUMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de inventario, cierre o reconciliación. Marca el término del rango temporal de validación de cantidades de productos despachados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECFININV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final Inicial Inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECFININV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECFININV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período de inventario, cierre o reconciliación. Marca el comienzo del rango temporal de validación de cantidades de productos despachados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECINIINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECINIINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECINIINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o punto de servicio. Referencia a la tabla ADCENATEN (FK). Identifica la sede, clínica, hospital o IPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (identity) del registro de parámetros de facturación del centro. Consecutivo autoincremental numérico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de fechas de inventario por centro de atención: define los rangos de fechas de inicio y fin de inventario, si el suministro está confirmado y la posición del proceso de inventario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC';
