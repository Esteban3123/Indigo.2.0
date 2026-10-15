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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  True -> Activo   False -> Inactivo ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'las imagenes quedan predefinidas  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Image';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Hombre  2 - Mujeres  3 - AMbos', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'AppliesSex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Años  2 - Meses  3 - Dias  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad maxima al que aplica el grupo poblacional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - años  2 - Meses  3 - Dias  ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'UnitMinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad minima al que aplica el grupo poblacional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos Grupos de Población (nombre)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del tipo de grupo poblacional', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TypesPopulationGroups', @level2type = N'COLUMN', @level2name = N'Id';

