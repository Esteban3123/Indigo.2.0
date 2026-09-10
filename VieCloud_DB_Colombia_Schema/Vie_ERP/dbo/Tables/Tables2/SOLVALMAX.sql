CREATE TABLE [dbo].[SOLVALMAX] (
    [Auto]      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CATEAUTON] INT             NOT NULL,
    [CATDESCRI] VARCHAR (100)   NOT NULL,
    [UFUCODIGO] CHAR (10)       NOT NULL,
    [ValMAx]    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_SOLVALMAX] PRIMARY KEY CLUSTERED ([Auto] ASC),
    CONSTRAINT [FK_SOLVALMAX_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_SOLVALMAX_SOLCATEGO] FOREIGN KEY ([CATEAUTON]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo de solicitud permitido (NUMERIC 18,2) para la categoría de autorización en la unidad funcional; límite de monto en pesos para tramitación de solicitudes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'ValMAx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor maximo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'ValMAx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'ValMAx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10) que aplica el tope máximo; referencia a centro de atención, área clínica o departamento; FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la categoría de autorización o solicitud (VARCHAR 100); nombre legible de la clasificación de trámites.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de categorias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la categoría de autorización o solicitud; clave primaria en SOLCATEGO; agrupa solicitudes por tipo de trámite.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATEAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de las categorias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATEAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATEAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincrementable (INT IDENTITY) del registro de tope máximo; clave primaria de la tabla SOLVALMAX.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'Auto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'Auto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'Auto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores máximos autorizados por categoría de autonomía para cada unidad funcional. Permite controlar los límites de aprobación o solicitud según el nivel jerárquico o de autorización definido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX';
