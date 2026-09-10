CREATE TABLE [dbo].[HCESCADETALLE] (
    [ID]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESCALAID] INT         NOT NULL,
    [RESULTADO]  INT         NOT NULL,
    [TIPOESCALA] INT         NULL,
    [AREAEVALU]  VARCHAR (1) NULL,
    CONSTRAINT [PK_HCESCADETALLE_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCADETALLE_HCESCALAS1] FOREIGN KEY ([HCESCALAID]) REFERENCES [dbo].[HCESCALAS] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área de evaluación del desarrollo (A, B, C, D); se completa únicamente para la Escala Abreviada de Desarrollo. VARCHAR(1), clasificación del área evaluada en pruebas del neurodesarrollo infantil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'AREAEVALU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se llena solo para la Escala Abreviada de desarrollo, que peuden ser A-B-C-D', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'AREAEVALU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'AREAEVALU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de escala EPOC (1=Escala Diseña, 2=Escala CAT, 18=Exacerbaciones); se registra solo cuando la escala sea Clasificación EPOC. INT, procedente del formulario de Clasificación EPOC para enfermedad pulmonar obstructiva crónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solo cuando la escala sea Clasificación EPOC se llena en estee campo la siguiente información:  1 - Escala Disena  2 - Escala CAT  18 - Exacerbaciones  Esta información viene del formulario de Clasificación EPOC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'TIPOESCALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación o valor numérico resultante de la aplicación de la escala de evaluación. INT, resultado cuantitativo de la medición clínica (desarrollo, EPOC, u otra escala).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la escala ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de escala en tabla HCESCALAS. INT NOT NULL, referencia a la historia clínica o evaluación matriz de la escala aplicada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabcera de la tabla HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'HCESCALAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) y consecutivo de la tabla HCESCADETALLE. INT IDENTITY, clave primaria para cada detalle de escala registrada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los resultados obtenidos en la aplicación de escalas de valoración clínica (como escalas de dolor, riesgo, funcionalidad, entre otras). Cada registro corresponde a un ítem o componente evaluado dentro de una escala, con su resultado y el área clínica evaluada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCADETALLE';
