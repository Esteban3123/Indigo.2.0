CREATE TABLE [dbo].[HCNIVIMPN] (
    [CODNIVIMP] CHAR (2)   NOT NULL,
    [DESNIVIMP] CHAR (100) NOT NULL,
    [COLNIVIMP] CHAR (50)  NOT NULL,
    [TIPO]      INT        CONSTRAINT [DF_HCNIVIMPN_TIPO] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCNIVIMPN] PRIMARY KEY CLUSTERED ([CODNIVIMP] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nivel de imputación: 1=Enfermería, 3=Instrumentador Quirúrgico, 4=Químico Farmacéutico. Clasificación del profesional de la salud responsable de la atención (INT, default=1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Enfermeria  3 - Instrumetador Quirurgico   4-Quimico Farmaceutico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código hexadecimal del color asignado al nivel de imputación para identificación visual en reportes y interfaces (CHAR 50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'COLNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Hexadecimal del Color', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'COLNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'COLNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del nivel de imputación: nombre o título del rol profesional de salud (Enfermería, Instrumentador, Farmacéutico, etc.) (CHAR 100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'DESNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Nivel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'DESNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'DESNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del nivel de imputación: identificador único alfanumérico de 2 caracteres, clave primaria para clasificar profesionales de la salud según su rol en la atención (CHAR 2, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nivel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de niveles de importancia o prioridad utilizados en la historia clínica, como clasificaciones de severidad, urgencia o relevancia clínica. Permite categorizar y distinguir visualmente registros según su nivel de criticidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNIVIMPN';
