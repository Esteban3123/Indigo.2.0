CREATE TABLE [dbo].[AGINDISPSALA] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDSALA]       INT       NOT NULL,
    [IDMOTIINDIS]  INT       NOT NULL,
    [FECININDISPO] DATETIME  NOT NULL,
    [FECFINDISPO]  DATETIME  NOT NULL,
    [FECHREGIS]    DATETIME  NOT NULL,
    [CODUSUARI]    CHAR (20) NOT NULL,
    CONSTRAINT [PK_AGINDISPSALA_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGINDISPSALA_AGENSALAC] FOREIGN KEY ([IDSALA]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC]),
    CONSTRAINT [FK_AGINDISPSALA_AGMOTINDPS] FOREIGN KEY ([IDMOTIINDIS]) REFERENCES [dbo].[AGMOTINDPS] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que creó el registro de indisponibilidad; trazabilidad de quién bloqueó la sala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario de Creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación o registro del indicador de indisponibilidad en el sistema; marca cuándo se registró la restricción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de término del período de indisponibilidad; marca el fin del bloqueo de la sala para agendamiento de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECFINDISPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de Indisponibilidad de sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECFINDISPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECFINDISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del período en el cual la sala queda indisponible; marca el comienzo del bloqueo de agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECININDISPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Indisponibilidad de sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECININDISPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'FECININDISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de indisponibilidad (FK a AGMOTINDPS.ID); causa por la cual la sala no está disponible (mantenimiento, desinfección, evento, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDMOTIINDIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion con tabla de Motivos de Indisponibilidad de salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDMOTIINDIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDMOTIINDIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sala (FK a AGENSALAC.CODCONCEC); referencia a la unidad funcional, consultorio o espacio físico que se marca como no disponible para agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion con salas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) del registro de indisponibilidad de sala en la tabla AGINDISPSALA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de indisponibilidades de salas de agendamiento: indica los períodos en que una sala está bloqueada o no disponible para citas, incluyendo el motivo, las fechas de inicio y fin de la indisponibilidad, y el usuario que registró el bloqueo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGINDISPSALA';
