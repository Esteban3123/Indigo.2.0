CREATE TABLE [dbo].[AGACTIMEDEAPB] (
    [Id]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED] CHAR (3) NOT NULL,
    [Codigo]    INT      NOT NULL,
    [Intervalo] INT      NOT NULL,
    CONSTRAINT [PK_AGACTIMEDEAPB] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AGACTIMEDEAPB_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intervalo en días; rango de tiempo en días entre repeticiones de actividades médicas o procedimientos; duración en días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Intervalo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Intervalo en días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Intervalo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Intervalo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (Id) de la EAPB o Entidad Administradora de Prestadores de Salud; identificador único de la aseguradora o administradora de planes de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código (Id) de la EAPB o Entidad Administradora de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Actividad Médica (FK a AGACTIMED); clasificación de procedimiento, servicio, examen, intervención o consulta médica; referencia a actividad clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Actividad Médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la tabla AGACTIMEDEAPB; clave primaria; correlativo interno del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id o Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de intervalos de tiempo para actividades médicas en el agendamiento. Define cuántos minutos o unidades de tiempo corresponden a cada tipo de actividad médica dentro de un bloque de agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMEDEAPB';
