CREATE TABLE [dbo].[HKADENDUM] (
    [ID]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [numero_solicitud]        INT           NULL,
    [codigo_interno_servicio] INT           NULL,
    [cups]                    VARCHAR (20)  NULL,
    [item]                    INT           NULL,
    [fecha]                   DATE          NULL,
    [hora]                    TIME (7)      NULL,
    [resultados]              VARCHAR (MAX) NULL,
    [codigo_radiologo]        VARCHAR (20)  NULL,
    CONSTRAINT [PK_HKADEMDUM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario o identificador del profesional radiólogo (VARCHAR 20). Referencia al especialista que genera o certifica el adendum.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de usuario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_radiologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto del adendum o complemento al reporte original (VARCHAR MAX). Contiene hallazgos adicionales, aclaraciones o ajustes diagnósticos radiológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'resultados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Adendum', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'resultados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'resultados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de envío o reporte del adendum en formato HH:MM:SS (TIME). Marca de tiempo de la transmisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'hora';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de envio  HH:MM:SS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'hora';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'hora';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envío o reporte del adendum en formato YYYY-MM-DD (DATE). Registro temporal del complemento diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio YYYY-MM-DD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'fecha';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo que indica la cantidad de adendum asociados a una solicitud (INT). Permite rastrear versiones o adiciones al reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'item';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo que nos indica cuantos adendum tiene una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'item';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'item';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Clasificación Única de Procedimientos en Salud) del procedimiento realizado (VARCHAR 20). Estandarización nacional colombiana.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Códificacion única de procedimientos en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'cups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del servicio prestador o unidad funcional (INT). Identifica el departamento o área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código interno del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'codigo_interno_servicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número y código único identificador de la solicitud de servicio de salud (INT). Referencia a la orden o petición clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador y  código único de solictud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'numero_solicitud';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT). Clave primaria de la tabla de adendum.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autoincrementable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de addenda o resultados adicionales asociados a solicitudes de servicios diagnósticos (imágenes, radiología u otros procedimientos). Guarda el detalle del resultado, el servicio solicitado y el profesional que lo reporta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HKADENDUM';
