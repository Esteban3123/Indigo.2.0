CREATE TABLE [dbo].[HCNUTPAREFD] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCNUTPAREC]   INT             NOT NULL,
    [IDHCPARNUTC]    INT             NOT NULL,
    [IDHCPARNUTFORD] INT             NOT NULL,
    [NAMEFORM]       CHAR (50)       NULL,
    [RESULT]         NUMERIC (18, 3) NOT NULL,
    [FORRESULT]      VARCHAR (8000)  NULL,
    [RRESULT]        VARCHAR (1000)  NOT NULL,
    [COLOR]          VARCHAR (20)    NULL,
    CONSTRAINT [PK__HCNUTPAR__3214EC2799E607CB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTPAREC_HCNUTPAREFD] FOREIGN KEY ([IDHCNUTPAREC]) REFERENCES [dbo].[HCNUTPAREC] ([ID]),
    CONSTRAINT [FK_HCPARNUTC_HCNUTPAREFD] FOREIGN KEY ([IDHCPARNUTC]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_HCPARNUTFORD_HCNUTPAREFD] FOREIGN KEY ([IDHCPARNUTFORD]) REFERENCES [dbo].[HCPARNUTFORD] ([ID])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color asociado al resultado (VARCHAR 20); indicador visual de estado, rango o clasificación del resultado de la fórmula.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el color ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula reemplazada o sustituida para recalcular el valor de la fórmula farmacéutica (VARCHAR 1000); fórmula alternativa o corregida tras revisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula reemplazada para calcular el valor de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula/expresión empleada para calcular el valor final (VARCHAR 8000); contiene la definición matemática o algoritmo de cálculo aplicado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'FORRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula empleada para calcular el valor de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'FORRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'FORRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado numérico calculado de la fórmula farmacéutica/nutricional (NUMERIC 18,3); valor final tras aplicar la fórmula de cálculo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la fórmula farmacéutica o nutricional (CHAR 50); etiqueta legible del componente en prescripción parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'NAMEFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'NAMEFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'NAMEFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de parametrización de fórmulas farmacéuticas/nutricionales (FK → HCPARNUTFORD); referencia la plantilla o definición de la fórmula.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTFORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la pametrizacion de las formulas farmaceuticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTFORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTFORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de parametrización de nutrición parenteral (FK → HCPARNUTC); referencia la configuración y definición del componente nutricional empleado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la pametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de prescripción de nutrición parenteral (FK → HCNUTPAREC); vincula el detalle al encabezado de la orden/ingreso nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro detallado de fórmula nutricional parenteral; consecutivo de la tabla HCNUTPAREFD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de parámetros de fórmulas en la evaluación nutricional del paciente. Almacena los valores calculados de cada parámetro nutricional asociado a una fórmula de valoración, incluyendo el resultado numérico, su representación formateada y el color de semáforo para interpretación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD';
