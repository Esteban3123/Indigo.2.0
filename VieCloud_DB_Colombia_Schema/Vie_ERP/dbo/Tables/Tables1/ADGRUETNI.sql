CREATE TABLE [dbo].[ADGRUETNI] (
    [CODGRUPOE] CHAR (3)     NOT NULL,
    [DESGRUPET] VARCHAR (70) NOT NULL,
    [TIPOET]    VARCHAR (3)  NULL,
    [ESTADO]    TINYINT      NULL,
    CONSTRAINT [PK_ADGRUETNI] PRIMARY KEY CLUSTERED ([CODGRUPOE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo (TINYINT, 1=Sí/Activo, 2=No/Inactivo). Bandera booleana que indica si el grupo étnico está disponible para selección en formularios de atención clínica o datos demográficos del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Activo: 1->Si 2->No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo étnico (VARCHAR 3). Clasificación o categoría del grupo étnico: indígena, afrodescendiente, raizal, ROM, mestizo, blanco u otra comunidad cultural registrada para propósitos de atención diferenciada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'TIPOET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo etinico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'TIPOET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'TIPOET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del grupo étnico (VARCHAR 70). Nombre o denominación completa del grupo étnico, comunidad indígena, afrocolombiana, raizal, ROM o población vulnerable catalogada según clasificación demográfica de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'DESGRUPET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'DESGRUPET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'DESGRUPET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo étnico (PK, CHAR 3). Identificador único del grupo étnico, poblacional o comunidad cultural registrada en el sistema de atención en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de grupos étnicos o etnias reconocidos, usado para clasificar la pertenencia étnica de los pacientes en el sistema de salud (indígena, afrocolombiano, raizal, rom, entre otros).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUETNI';
