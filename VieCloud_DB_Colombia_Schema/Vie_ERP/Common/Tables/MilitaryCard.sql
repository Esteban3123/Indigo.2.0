CREATE TABLE [Common].[MilitaryCard] (
    [Id]    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]  VARCHAR (3)  NOT NULL,
    [Name]  VARCHAR (50) NOT NULL,
    [State] BIT          NOT NULL,
    CONSTRAINT [PK_MilitaryCard] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la libreta militar: 1=Activo, 0=Inactivo, bit booleano para control de vigencia', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1 Activo, 0 Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación de la libreta militar, varchar(50), describe el tipo o categoría de carnet militar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la libreta militar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la libreta militar, varchar(3), identificador alfanumérico corto para clasificación o tipo de libreta', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la libreta militar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la libreta militar, clave primaria de la tabla MilitaryCard', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la libreta militar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de libreta o tarjeta militar utilizados como documento de identidad. Permite identificar la clase de credencial militar asociada a un paciente o persona registrada en el sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'MilitaryCard';
