CREATE TABLE [dbo].[AGCONSULT] (
    [CODCENATE] CHAR (10)      NOT NULL,
    [CODIGOCON] CHAR (6)       NOT NULL,
    [DESCRICON] NVARCHAR (100) NOT NULL,
    [ESTADOCON] BIT            NOT NULL,
    [UFUCODIGO] CHAR (10)      NULL,
    [ID]        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_AGCONSULT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGCONSULT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [IX_AGCONSULT] UNIQUE NONCLUSTERED ([CODIGOCON] ASC, [CODCENATE] ASC)
);






GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de registro de consultorio en tabla AGCONSULT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10) asociada al consultorio; nulo si no aplica. Referencia a estructura organizacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de unidad Funcional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del consultorio: BIT (0=Inactivo, 1=Activo). Indica disponibilidad para agendamientos y atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ESTADOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Consultorio(Activo o Inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ESTADOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ESTADOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del consultorio (NVARCHAR 100). Nombre legible para búsqueda de espacios de consulta y atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'DESCRICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'DESCRICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'DESCRICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del consultorio (CHAR 6) por centro de atención. Identificador operativo junto con CODCENATE en clave única.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODIGOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK). Referencia a ADCENATEN. Agrupa consultorios por sede, hospital o clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
CREATE NONCLUSTERED INDEX [IX_AGCONSULT_Consultorio]
    ON [dbo].[AGCONSULT]([CODIGOCON] ASC, [CODCENATE] ASC)
    INCLUDE([DESCRICON], [UFUCODIGO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de consulta o motivos de consulta disponibles para agendamiento, asociados a un centro de atención y opcionalmente a una unidad funcional. Permite configurar qué clases de consulta (general, especialidad, control, etc.) están habilitadas en cada sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT';
