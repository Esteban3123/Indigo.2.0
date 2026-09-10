CREATE TABLE [dbo].[CALTIPOCLASE] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TIPOACCION]    INT           NOT NULL,
    [CODIGO]        VARCHAR (5)   NOT NULL,
    [DESCRIPCION]   VARCHAR (400) NULL,
    [IDPADRE]       INT           NULL,
    [CONTROLVIGILA] INT           NULL,
    CONSTRAINT [PK_CALTIPOCLASE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALTIPOCLASE_CALTIPOCLASE] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[CALTIPOCLASE] ([ID])
);


GO
ALTER TABLE [dbo].[CALTIPOCLASE] NOCHECK CONSTRAINT [FK_CALTIPOCLASE_CALTIPOCLASE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vigilancia o control epidemiológico asociado (INT): 1=Tecnovigilancia, 2=Farmacovigilancia, 3=Hemovigilancia, 4=Reactivovigilancia, 5=IAAS-Dispositivos, 6=Infección quirúrgica, 7=Ninguno. PII sensible en contexto de salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'CONTROLVIGILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control y vigilancia:  1=Tecnovigilancia  2=Farmacovigilancia  3=Hemovigilancia  4=Reactivovigilancia  5=IAAS-Dispositivos  6=Infección de localización quirúrgica  7=Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'CONTROLVIGILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'CONTROLVIGILA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro padre (INT, FK recursiva a ID). Establece jerarquía de clases permitiendo relaciones parent-child dentro de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID padre es igual a ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'IDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual detallada (VARCHAR 400) de la clase de tipo. Proporciona contexto humano del registro para interpretación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del registo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único (VARCHAR 5) que identifica la clase. Identificador funcional del registro para búsqueda y referencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de acción o evento clínico registrado (INT): 1=Acción Insegura (datos tipográficos/morfológicos), 2=Acción Clínica Inesperada. Clasifica la naturaleza del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'TIPOACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Guardar o actualizar datos de registros Tipografico  1 = Accion Insegura    Guarda o actualiza datos de registros morfologicos    2 = Accion clinica Insesperada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'TIPOACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'TIPOACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la clase de tipo. Clave primaria de la tabla CALTIPOCLASE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos y clases de calidad o vigilancia, organizado en jerarquías. Define las categorías y acciones de control utilizadas en procesos de seguimiento y vigilancia clínica o administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTIPOCLASE';
