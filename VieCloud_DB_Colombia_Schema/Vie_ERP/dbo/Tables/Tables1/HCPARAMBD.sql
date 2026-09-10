CREATE TABLE [dbo].[HCPARAMBD] (
    [ID]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARAMBID]    INT NOT NULL,
    [HCVALPARACLID] INT NOT NULL,
    CONSTRAINT [PK_HCPARAMBD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARAMBD_HCPARAMB] FOREIGN KEY ([HCPARAMBID]) REFERENCES [dbo].[HCPARAMB] ([ID]),
    CONSTRAINT [FK_HCPARAMBD_HCVALPARACL] FOREIGN KEY ([HCVALPARACLID]) REFERENCES [dbo].[HCVALPARACL] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del valor predeterminado del parámetro paraclínico; referencia a tabla HCVALPARACL para obtener el valor configurado por defecto en exámenes, laboratorios o estudios complementarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCVALPARACLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del valor predeterminado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCVALPARACLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCVALPARACLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del analito o parámetro del examen paraclínico ambulatorio; referencia a tabla HCPARAMB que contiene el código del analito solicitado en pruebas de laboratorio, imagen o procedimiento diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del analito del paraclinico ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'HCPARAMBID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la relación entre parámetro paraclínico ambulatorio y su valor predeterminado; clave primaria de la tabla HCPARAMBD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación entre parámetros de historia clínica y sus valores de clasificación. Vincula cada parámetro configurado en historia clínica con los valores de lista o catálogo que le corresponden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARAMBD';
