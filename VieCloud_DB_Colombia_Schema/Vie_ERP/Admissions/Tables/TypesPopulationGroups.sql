CREATE TABLE [Admissions].[TypesPopulationGroups] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]           VARCHAR (3)   NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    [MinimumAge]     INT           NOT NULL,
    [UnitMinimumAge] INT           NOT NULL,
    [MaximumAge]     INT           NOT NULL,
    [UnitMaximumAge] INT           NOT NULL,
    [AppliesSex]     INT           NOT NULL,
    [Image]          INT           CONSTRAINT [DF_TypesPopulationGroups_Image] DEFAULT ((1)) NOT NULL,
    [Status]         BIT           NOT NULL,
    CONSTRAINT [PK_TypesPopulationGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_TypesPopulationGroups] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT): True=Activo, False=Inactivo. Indica si el grupo poblacional está vigente para uso en admisiones/atenciones. Sinónimos: estado vigencia, actividad registro, habilitado.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado  True -> Activo   False -> Inactivo ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de imagen predefinida (BIT, default=1). Indica si el grupo poblacional tiene imagen/icono asociado en la interfaz. Sinónimos: imagen predeterminada, ícono grupo, recurso visual.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Image';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'las imagenes quedan predefinidas  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Image';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Image';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo al que aplica el grupo poblacional (INT): 1=Hombre, 2=Mujeres, 3=Ambos. Criterio de inclusión por género. Sinónimos: aplica sexo, género aplicable, restricción género.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'AppliesSex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Hombre  2 - Mujeres  3 - AMbos', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'AppliesSex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'AppliesSex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para edad máxima (INT): 1=Años, 2=Meses, 3=Días. Define escala temporal del MaximumAge. Sinónimos: unidad edad máxima, período máximo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Años  2 - Meses  3 - Dias  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima aplicable al grupo poblacional (INT). Valor numérico usado junto con UnitMaximumAge para definir rango etario superior. Sinónimos: edad final, edad límite máxima, tope de edad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad maxima al que aplica el grupo poblacional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para edad mínima (INT): 1=Años, 2=Meses, 3=Días. Define escala temporal del MinimumAge. Sinónimos: unidad edad mínima, período mínimo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - años  2 - Meses  3 - Dias  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima aplicable al grupo poblacional (INT). Valor numérico usado junto con UnitMinimumAge para definir rango etario inferior. Sinónimos: edad inicial, edad de inicio, límite inferior edad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad minima al que aplica el grupo poblacional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del tipo/grupo de población (VARCHAR 100). Ej: gestantes, menores, adultos mayores, población vulnerable. Sinónimos: clasificación poblacional, denominación grupo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos Grupos de Población (nombre)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del tipo de grupo poblacional (VARCHAR 3, clave única). Identificador alfanumérico corto para clasificación de población. Sinónimos: código de grupo, tipo de población.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del tipo de grupo poblacional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY), consecutivo autonumérico de la tabla TypesPopulationGroups. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de grupos poblacionales utilizados en admisiones, definiendo rangos de edad, sexo y características para clasificar a los pacientes según su perfil demográfico (por ejemplo: neonatos, pediátricos, adultos mayores, gestantes).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups';
