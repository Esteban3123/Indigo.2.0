CREATE TABLE [dbo].[AGEQUIPTRAD] (
    [ID]           INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGEQUIPTRA] INT      NOT NULL,
    [CODACTMED]    CHAR (3) NULL,
    CONSTRAINT [PK_AGEQUIPTRAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGEQUIPTRAD_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGEQUIPTRAD_AGEQUIPTRA] FOREIGN KEY ([IDAGEQUIPTRA]) REFERENCES [dbo].[AGEQUIPTRA] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad médica (CHAR 3); identificador de la prestación o procedimiento realizado. Referencia a AGACTIMED para clasificar servicios de salud, consultas, procedimientos, exámenes o intervenciones. Clave foránea FK_AGEQUIPTRAD_AGACTIMED.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código de la actividad médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación del equipo de tratamiento; identificador numérico que vincula el registro de detalle con la configuración del equipo médico o de tratamiento en la tabla AGEQUIPTRA. Clave foránea FK_AGEQUIPTRAD_AGEQUIPTRA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion del equipo de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) de la tabla de detalle de equipos por servicios IPS; clave primaria que indexa cada asociación entre equipo de tratamiento y actividad médica en la unidad funcional o centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de detalle de equipo x servicios IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de traducciones o equivalencias de actividades médicas asociadas a equipos de agendamiento. Permite mapear códigos de actividades médicas a los equipos o recursos programados en el sistema de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRAD';
