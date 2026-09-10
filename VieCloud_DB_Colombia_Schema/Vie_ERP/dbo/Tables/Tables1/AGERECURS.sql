CREATE TABLE [dbo].[AGERECURS] (
    [CODCONCEC]  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGOREC]  CHAR (6)      NOT NULL,
    [TIPORECUR]  CHAR (1)      NOT NULL,
    [DESCRIPREC] VARCHAR (100) NOT NULL,
    [ESTADOREC]  BIT           NOT NULL,
    CONSTRAINT [PK_AGERECURS_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del recurso (activo/inactivo). Bandera BIT que indica si el recurso físico o humano está disponible y en uso en la organización de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'ESTADOREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'ESTADOREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'ESTADOREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del recurso. Texto VARCHAR(100) que especifica características, nombre o detalles del recurso físico (equipos, insumos) o humano (profesional, especialidad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'DESCRIPREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'DESCRIPREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'DESCRIPREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de recurso: F=Físico (equipos, insumos, infraestructura) o H=Humano (profesionales de salud, personal administrativo). Carácter CHAR(1) que clasifica el recurso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'TIPORECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el Tipo de recurso F - Fisico, H - Humano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'TIPORECUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'TIPORECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del recurso. Identificador CHAR(6) que distingue cada recurso físico o humano en el sistema de gestión de recursos de la unidad funcional o centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODIGOREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODIGOREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODIGOREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo (PK). Identificador único autoincrementable INT IDENTITY que indexa cada registro de recurso en la tabla AGERECURS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de recursos de agendamiento (salas, equipos, profesionales u otros recursos) utilizados para programar citas y procedimientos médicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGERECURS';
