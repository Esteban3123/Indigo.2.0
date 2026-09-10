CREATE TABLE [dbo].[HCLISTACD] (
    [CODCONSEC]    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC]   INT           NOT NULL,
    [DESPREENC]    VARCHAR (250) NOT NULL,
    [CAMPOTEXTO]   BIT           NULL,
    [CAMPOFECHA]   BIT           NULL,
    [CAMPOADJUNTO] BIT           NULL,
    [TIPOCAMPO]    INT           NULL,
    CONSTRAINT [PK_HCLISTACD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCLISTACD_HCLISTACC] FOREIGN KEY ([CODCONSECC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de campo del detalle de lista de chequeo (INT). Determina la naturaleza o categoría del campo de datos: texto, fecha, adjunto u otro tipo de control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tipo de campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'TIPOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, Sí/No) que marca si el campo permite adjuntar archivos, documentos o evidencia en la lista de chequeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo adjunto  si      no ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, Sí/No) que señala si el campo es de tipo fecha para capturar fechas en la lista de chequeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, Sí/No) que identifica si el campo es de tipo texto para respuestas libres o notas en la lista de chequeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el campo de texto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o enunciado de la pregunta del detalle de lista de chequeo (VARCHAR 250). Contiene la pregunta, ítem o criterio a evaluar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'DESPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pregunta de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'DESPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'DESPREENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la cabecera/encabezado de la lista de chequeo (INT, FK → HCLISTACC). Vincula el detalle con su lista padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo cabecera lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CODCONSECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CODCONSECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincremental único (INT IDENTITY) del detalle de la lista de chequeo. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Listado de campos o ítems de las listas de chequeo de historia clínica. Define los componentes individuales (texto, fecha o adjunto) que conforman cada lista de chequeo clínica utilizada en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACD';
