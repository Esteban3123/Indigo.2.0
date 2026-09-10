CREATE TABLE [dbo].[HCTURNREC] (
    [CONSECUTI]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESPECI]       CHAR (3)  NOT NULL,
    [COLORESPE]       INT       NOT NULL,
    [CODCENATE]       CHAR (10) NOT NULL,
    [USUARIOCREACION] CHAR (20) NULL,
    [FECHACREACION]   DATETIME  NULL,
    CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCTURNREC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCTURNREC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCTURNREC_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCTURNREC__CODCENATE]
    ON [dbo].[HCTURNREC]([CODCENATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de turno/recurso; tipo DATETIME; auditoría de cuándo se registró la asignación en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (operador, administrativo) que creó el registro; FK a SEGusuaru; PII: Identification_Ofuscado; rastreo de quién registró el turno/recurso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (clínica, hospital, unidad funcional); FK a ADCENATEN; identifica la sede donde aplica la especialidad y turno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color asignado a la especialidad para identificación visual en agenda/turnos; código numérico de display; facilita distinción de especialidades en interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'COLORESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'COLORESPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'COLORESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica (cardiología, pediatría, urgencia, etc.); FK a INESPECIA; clasificación del servicio de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y secuencial del registro de turno/recurso; INT IDENTITY; clave primaria para auditoría y referencia interna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de turnos por especialidad médica y centro de atención. Guarda la configuración de turnos de atención asignados a cada especialidad, incluyendo el color visual asociado y el centro donde se prestan los servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTURNREC';
