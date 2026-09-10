CREATE TABLE [dbo].[CALSEGDESCA] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]    INT           NOT NULL,
    [IDCALMOTIANULAC] INT           NOT NULL,
    [OBSERVACION]     VARCHAR (200) NOT NULL,
    [FECHAREGISTRO]   DATETIME      NOT NULL,
    [USUARIOREGISTRO] CHAR (20)     NOT NULL,
    CONSTRAINT [PK_CALSEGDESCA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALSEGDESCA_CALMOTIANULAC] FOREIGN KEY ([IDCALMOTIANULAC]) REFERENCES [dbo].[CALMOTIANULAC] ([ID]),
    CONSTRAINT [FK_CALSEGDESCA_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALSEGDESCA_SEGusuaru] FOREIGN KEY ([USUARIOREGISTRO]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de sistema (SEGusuaru.CODUSUARI) que realizó el registro de descarte o anulación. Identificación del operador responsable. Tipo: CHAR(20), FK a SEGusuaru.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del registro de descarte o anulación de calidad. Timestamp de creación del descarte. Auditoría temporal del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o comentario (VARCHAR 200) justificativo del descarte o anulación de reporte de calidad. Notas adicionales del motivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del motivo de descarte o anulación en tabla CALMOTIANULAC. Referencia a razón de rechazo: incompleto, error, duplicado, inadecuado, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALMOTIANULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del motivo de descarte o anulacion, tabla CALMOTIANULAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALMOTIANULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALMOTIANULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del reporte de calidad en tabla CALREPORTE. Referencia al reporte de control de calidad descartado o anulado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY 1,1) de la tabla CALSEGDESCA. Clave primaria del registro de seguimiento de descarte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de descargos o anulaciones de seguimiento en el módulo de calendario/agenda, donde se almacena el motivo de anulación, la observación justificativa y el usuario que realizó el descargo junto con su fecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA';
