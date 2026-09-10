CREATE TABLE [dbo].[AMRCMIPRESCUPS] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]     INT           NOT NULL,
    [CODSERIPS]     CHAR (20)     NOT NULL,
    [FECHA]         DATETIME      NOT NULL,
    [JUSTIFICACION] VARCHAR (600) NOT NULL,
    CONSTRAINT [PK_AMRCMIPRESCUPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AMRCMIPRESCUPS_HCJUSTECA] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCJUSTECA] ([CODCONCEC])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_AMRCMIPRESCUPS_CODCONCEC]
    ON [dbo].[AMRCMIPRESCUPS]([CODCONCEC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación textual de la habilitación del servicio. Texto descriptivo (VARCHAR 600) que explica motivos o condiciones de la habilitación regulatoria. Búsquedas: justificación, habilitación, motivo, aprobación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la habilitacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de habilitación (DATETIME). Timestamp de registro administrativo. Búsquedas: fecha creación, fecha habilitación, cuándo se habilitó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio en formato RIPS (CHAR 20). Identificador único del servicio de salud habilitado según nomenclatura RIPS-MINSALUD. Búsquedas: código servicio, RIPS, servicio habilitado, procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del concepto de habilitación (INT, FK a HCJUSTECA). Referencia al registro de justificación de habilitación en tablas maestras. Búsquedas: código habilitado, concepto habilitación, FK justificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria autoincremental del registro de habilitación de servicio. Búsquedas: ID, identificador, registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la justificación clínica o administrativa asociada a servicios CUPS prescritos en órdenes médicas. Relaciona cada concepto de prescripción con su código de servicio (CUPS), la fecha en que se generó y el texto de justificación ingresado por el profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRESCUPS';
