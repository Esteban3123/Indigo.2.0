CREATE TABLE [dbo].[HCANESUBC] (
    [CODCATEGO]     CHAR (2)     NOT NULL,
    [CODSUBCAT]     CHAR (2)     NOT NULL,
    [NOMSUBCAT]     VARCHAR (60) NOT NULL,
    [EDADMIN]       INT          CONSTRAINT [DF_EDADMIN] DEFAULT ((0)) NOT NULL,
    [EDADMAX]       INT          CONSTRAINT [DF_EDADMAX] DEFAULT ((120)) NOT NULL,
    [GENERO]        TINYINT      CONSTRAINT [DF_GENERO] DEFAULT ((3)) NOT NULL,
    [TIPO]          TINYINT      CONSTRAINT [DF_TIPO] DEFAULT ((1)) NOT NULL,
    [MANEJADECIMAL] BIT          NULL,
    CONSTRAINT [PK_HCANESUBC] PRIMARY KEY CLUSTERED ([CODCATEGO] ASC, [CODSUBCAT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la subcategoría de anestesia admite valores decimales (0=No, 1=Sí). Controla precisión numérica en parámetros anestésicos. Tipo: BIT, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si maneja decimal:   0 - No   1 - Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'MANEJADECIMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dato de la variable en la subcategoría anestésica (1=String/texto, 2=Numérico). Define formato de registro. Tipo: TINYINT, default=1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de variable:   1 - String   2 - Númerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género aplicable a la subcategoría de anestesia (1=Masculino, 2=Femenino, 3=Ambos). Restricción demográfica para protocolos anestésicos. Tipo: TINYINT, default=3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Género:   1  - Masculino   2 - Femenino   3 - Ambos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'GENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'GENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida en años para aplicar esta subcategoría de anestesia. Límite superior de rango etario. Tipo: INT, default=120.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Máxima en años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'EDADMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'EDADMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida en años para aplicar esta subcategoría de anestesia. Límite inferior de rango etario. Tipo: INT, default=0.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Mínima en años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'EDADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'EDADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción textual de la subcategoría de anestesia (ej: técnica anestésica, droga, procedimiento). Identificador legible. Tipo: VARCHAR(60).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'NOMSUBCAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la Subcategoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'NOMSUBCAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'NOMSUBCAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de dos caracteres identificador de la subcategoría dentro de una categoría de anestesia. Parte de clave primaria compuesta. Tipo: CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'CODSUBCAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Subcategoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'CODSUBCAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'CODSUBCAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de dos caracteres de la categoría padre en anestesia (ej: general, regional, local). Parte de clave primaria compuesta con CODSUBCAT. Tipo: CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Categoría en Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'CODCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC', @level2type = N'COLUMN', @level2name = N'CODCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subcategorías de anestesia utilizadas en historia clínica, con sus restricciones de aplicación por edad, género y tipo. Permite clasificar los registros anestésicos según categoría y subcategoría, controlando rangos etarios y sexo del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESUBC';
