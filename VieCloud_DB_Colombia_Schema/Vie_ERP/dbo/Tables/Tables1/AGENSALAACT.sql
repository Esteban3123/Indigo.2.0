CREATE TABLE [dbo].[AGENSALAACT] (
    [ID]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT      NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    CONSTRAINT [PK_AGENSALAACT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGENSALAACT_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGENSALAACT_AGENSALAC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de relación con actividades y medidas médicas (FK a AGACTIMED). Identifica el tipo de actividad o medida clínica asociada a la sala de atención. INT(3), clave foránea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo relacion Actividades Medidas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de relación con salas de atención (FK a AGENSALAC). Referencia la unidad funcional o sala donde se ejecuta la actividad médica. INT, clave foránea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Relacion con tabla de salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de la asociación entre sala y actividad médica. INT, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de actividades médicas asociadas a cada concepto de sala de espera o agenda. Relaciona los conceptos de cita o turno con las actividades o procedimientos médicos habilitados para esa configuración de agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAACT';
