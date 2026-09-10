CREATE TABLE [dbo].[AGENSALAEQU] (
    [ID]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]    INT NOT NULL,
    [IDAGEQUIPTRA] INT NOT NULL,
    CONSTRAINT [PK_AGENSALAEQU] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGENSALAEQU_AGENSALAC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC]),
    CONSTRAINT [FK_AGENSALAEQU_AGEQUIPTRA] FOREIGN KEY ([IDAGEQUIPTRA]) REFERENCES [dbo].[AGEQUIPTRA] ([ID])
);




GO



GO





GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AGENSALAEQU]
    ON [dbo].[AGENSALAEQU]([CODCONCEC] ASC, [IDAGEQUIPTRA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del equipo de tratamiento (FK a AGEQUIPTRA). Referencia a equipos médicos, dispositivos terapéuticos o instrumentos de procedimiento asignados a la sala de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla de equipos de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de la sala de atención (FK a AGENSALAC). Identifica la unidad funcional, consultorio, quirófano o espacio donde se utiliza el equipo de tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Consecutivo de la Tabla de salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincrementable (IDENTITY). Clave primaria que registra cada asignación de equipo a sala en el catálogo de agendamiento y recursos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU', @level2type = N'COLUMN', @level2name = N'ID';


GO
CREATE NONCLUSTERED INDEX [IX_AGENSALAEQU_CODCONCEC_IDAGEQUIPTRA]
    ON [dbo].[AGENSALAEQU]([CODCONCEC] ASC)
    INCLUDE([IDAGEQUIPTRA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona salas de agendamiento con equipos o recursos asignados para la prestación del servicio. Permite saber qué equipos están vinculados a cada sala o concepto de atención en el módulo de agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAEQU';
