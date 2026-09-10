CREATE TABLE [dbo].[HCNUTRICIOND] (
    [ID]          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]  INT             NOT NULL,
    [IDPARAMETRO] INT             NOT NULL,
    [APORTE]      DECIMAL (18, 2) NOT NULL,
    [VOLUMEN]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_HCNUTRICIOND] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTRICIOND_HCNUTRICIONC] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[HCNUTRICIONC] ([ID]),
    CONSTRAINT [FK_HCNUTRICIOND_HCPARANUTRICION] FOREIGN KEY ([IDPARAMETRO]) REFERENCES [dbo].[HCPARANUTRICION] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total (DECIMAL 18,2) del alimento, líquido o soporte nutricional expresado en unidades de medida (ml, cc, porción). Cantidad física administrada en nutrición enteral o suplementación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'VOLUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'VOLUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'VOLUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aporte nutricional cuantificado (DECIMAL 18,2). Cantidad de nutrientes, calorías, proteínas u otro componente nutricional suministrado o recomendado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'APORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Aporte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'APORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'APORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del parámetro nutricional (FK a HCPARANUTRICION). Referencia el tipo de nutriente, macronutriente o componente evaluado en la valoración nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDPARAMETRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id  de parametros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDPARAMETRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDPARAMETRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de nutrición (FK a HCNUTRICIONC). Relaciona este detalle con el registro principal de valoración o plan nutricional del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera; campo que me tiene la relacion con la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de cada registro de detalle nutricional en la tabla HCNUTRICIOND.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los aportes nutricionales registrados en la historia clínica de nutrición del paciente. Cada fila representa un parámetro nutricional específico (nutriente, componente o ítem) con su aporte y volumen dentro de un registro de valoración o seguimiento nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTRICIOND';
