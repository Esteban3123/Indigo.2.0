CREATE TABLE [dbo].[ADPARCALT] (
    [CODCENATE] CHAR (10)    NOT NULL,
    [CODINTDIA] CHAR (1)     NOT NULL,
    [NOMDIACAL] CHAR (10)    NOT NULL,
    [CODENTIDA] CHAR (9)     NOT NULL,
    [TRIAGECLA] CHAR (1)     NOT NULL,
    [00]        CHAR (1)     NOT NULL,
    [01]        CHAR (1)     NOT NULL,
    [02]        CHAR (1)     NOT NULL,
    [03]        CHAR (1)     NOT NULL,
    [04]        CHAR (1)     NOT NULL,
    [05]        CHAR (1)     NOT NULL,
    [06]        CHAR (1)     NOT NULL,
    [07]        CHAR (1)     NOT NULL,
    [08]        CHAR (1)     NOT NULL,
    [09]        CHAR (1)     NOT NULL,
    [10]        CHAR (1)     NOT NULL,
    [11]        CHAR (1)     NOT NULL,
    [12]        CHAR (1)     NOT NULL,
    [13]        CHAR (1)     NOT NULL,
    [14]        CHAR (1)     NOT NULL,
    [15]        CHAR (1)     NOT NULL,
    [16]        CHAR (1)     NOT NULL,
    [17]        CHAR (1)     NOT NULL,
    [18]        CHAR (1)     NOT NULL,
    [19]        CHAR (1)     NOT NULL,
    [20]        CHAR (1)     NOT NULL,
    [21]        CHAR (1)     NOT NULL,
    [22]        CHAR (1)     NOT NULL,
    [23]        CHAR (1)     NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADPARCALT_1] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [CODINTDIA] ASC, [CODENTIDA] ASC, [TRIAGECLA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18), identificador único del registro de auditoría para trazabilidad y control interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 23:00 (11 PM), flag de ocupación/actividad para franja nocturna tardía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [23] = 11 PM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 22:00 (10 PM), flag de ocupación/actividad para franja nocturna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [22] = 10 PM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 21:00 (9 PM), flag de ocupación/actividad para franja vespertina tardía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [21] = 9 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 20:00 (8 PM), flag de ocupación/actividad para franja vespertina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'20';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [20] = 8 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'20';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 19:00 (7 PM), flag de ocupación/actividad para franja vespertina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'19';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [19] = 7 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'19';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 18:00 (6 PM), flag de ocupación/actividad para franja vespertina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'18';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [18] = 6 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'18';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 17:00 (5 PM), flag de ocupación/actividad para franja vespertina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'17';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [17] = 5 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'17';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 16:00 (4 PM), flag de ocupación/actividad para franja tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [16] = 4 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 15:00 (3 PM), flag de ocupación/actividad para franja tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'15';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [15] = 3 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'15';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 14:00 (2 PM), flag de ocupación/actividad para franja tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'14';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [14] = 2 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'14';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'14';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 13:00 (1 PM), flag de ocupación/actividad para franja tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'13';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia [13] = 1 PM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'13';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 12:00 (12 PM), flag de ocupación/actividad para franja mediodía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [12] = 12 PM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 11:00 (11 AM), flag de ocupación/actividad para franja matutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [11] = 11 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 10:00 (10 AM), flag de ocupación/actividad para franja matutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [10] = 10 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 09:00 (9 AM), flag de ocupación/actividad para franja matutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'09';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [09] = 9 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'09';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'09';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 08:00 (8 AM), flag de ocupación/actividad para franja matutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'08';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [08] = 8 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'08';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'08';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 07:00 (7 AM), flag de ocupación/actividad para franja matutina temprana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'07';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [07] = 7 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'07';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'07';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 06:00 (6 AM), flag de ocupación/actividad para franja madrugada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'06';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [06] = 6 AM ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'06';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'06';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 05:00 (5 AM), flag de ocupación/actividad para franja madrugada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'05';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [05] = 5 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'05';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'05';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 04:00 (4 AM), flag de ocupación/actividad para franja madrugada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'04';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [04] = 4 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'04';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'04';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 03:00 (3 AM), flag de ocupación/actividad para franja madrugada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'03';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [03] = 3 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'03';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'03';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 02:00 (2 AM), flag de ocupación/actividad para franja madrugada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'02';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia   [02] = 2 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'02';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'02';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 01:00 (1 AM), flag de ocupación/actividad para franja nocturna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'01';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia  [01] = 1 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'01';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'01';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador horario 00:00 (12 AM), flag de ocupación/actividad para franja medianoche', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'00';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guardo hora del dia [00] = 12 AM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'00';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'00';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de clasificación del triage (CHAR 1), restricción: solo acepta categoría III o IV según protocolo de urgencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Clasificacion del TRIAGE    Nota: Solo acepta III o IV', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'TRIAGECLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad (CHAR 9, PK+FK), identificador único de la institución/asegurador, vinculado a INEntidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del día del calendario (CHAR 10), denominación literal del día (lunes, martes, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'NOMDIACAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Dia del Calendario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'NOMDIACAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'NOMDIACAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del día del calendario (CHAR 1, PK), código numérico o literal para identificar día de la semana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODINTDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Dia del Calendario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODINTDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODINTDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, PK), especifica el centro donde ingresa el paciente (urgencia, consulta, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calendario de disponibilidad horaria por franja (hora a hora, de 00 a 23) para cada combinación de centro de atención, entidad, clasificación de triage y tipo de día. Permite configurar qué horas están habilitadas para la atención según el día calendario y la urgencia del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALT';
