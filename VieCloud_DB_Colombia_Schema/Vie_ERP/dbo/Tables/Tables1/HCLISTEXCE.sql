CREATE TABLE [dbo].[HCLISTEXCE] (
    [AUTO]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC] INT       NOT NULL,
    [UFUCODIGO]  CHAR (10) NOT NULL,
    CONSTRAINT [PK_HCLISTEXCE] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCLISTEXCE_HCLISTACC] FOREIGN KEY ([CODCONSECC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional a la que se aplica la excepción en la lista de chequeo; identifica el centro de atención, área clínica o servicio (ej: urgencias, quirófano, laboratorio) que requiere excepción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional aplicar Excepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo (FK) de la cabecera de la lista de chequeo (HCLISTACC) a la que pertenece esta excepción; vincula el registro con el chequeo padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Cabecera de la Lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'CODCONSECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la tabla; clave primaria que genera automáticamente un número secuencial para cada excepción registrada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Listado de excepciones en historia clínica: registra las combinaciones de consecutivo de configuración y unidad funcional que están excluidas o marcadas como excepción en alguna regla o proceso clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTEXCE';
