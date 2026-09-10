CREATE TABLE [dbo].[RISLECTURA] (
    [AUTO]          INT                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRISORDEN]    INT                NOT NULL,
    [ESTADO]        INT                NOT NULL,
    [FECREGISTRO]   DATETIMEOFFSET (7) NOT NULL,
    [USUCREAREG]    VARCHAR (20)       NOT NULL,
    [FECCONFIRMADA] DATETIMEOFFSET (7) NULL,
    [USUCONFIRMA]   VARCHAR (20)       NULL,
    [FECANULA]      DATETIMEOFFSET (7) NULL,
    [USUANULA]      VARCHAR (20)       NULL,
    [MOTANULA]      VARCHAR (50)       NULL,
    [JUSANULA]      NVARCHAR (200)     NULL,
    [FECTRASNCRIP]  DATETIMEOFFSET (7) NULL,
    [USUTRANSCRIP]  VARCHAR (20)       NULL,
    CONSTRAINT [PK_RISLECTURA] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realiza la transcripción de la lectura RIS (radiología, imagen diagnóstica). Profesional de salud o técnico responsable de convertir la lectura a formato texto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUTRANSCRIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario quien realiza la transcripción ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUTRANSCRIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUTRANSCRIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIMEOFFSET) cuando se completa la transcripción de la lectura de imagen diagnóstica o RIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECTRASNCRIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de cuando se transcribe la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECTRASNCRIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECTRASNCRIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación detallada (NVARCHAR 200) de la anulación de la lectura RIS. Contexto adicional del motivo de anulación registrado por el profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'JUSANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'justificación de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'JUSANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'JUSANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de anulación seleccionado por el usuario del registro de lectura RIS. Categoría predefinida de causa de anulación (VARCHAR 50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'MOTANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'motivo de anulación seleccionado por el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'MOTANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'MOTANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (profesional de salud o administrativo) que anula/cancela el registro de lectura RIS después de validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario que anula la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIMEOFFSET) cuando se anula o cancela el registro de lectura RIS. Solo posible tras estado validada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de cuando se anula la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (radiólogo, profesional de diagnóstico) que confirma/valida la lectura RIS como correcta y completa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUCONFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario que confirma la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUCONFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUCONFIRMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIMEOFFSET) cuando se confirma/valida la lectura RIS. Marca transición a estado validada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECCONFIRMADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de cuando se confirma la lectura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECCONFIRMADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECCONFIRMADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que crea el registro inicial de lectura RIS en el sistema. Profesional que inicia la orden de transcripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUCREAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUCREAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'USUCREAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIMEOFFSET) de creación del registro de lectura RIS en el sistema. Timestamp inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de cuando se crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ciclo de vida de la lectura RIS: 1=En transcripción (no finalizada), 2=Validar (única activa), 3=Validada (única activa, anulable), 4=Anulada (múltiples posibles). Transición unidireccional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la lectura  1. En transcripción: una transcripción no finalizada  2. Validar: solo una puede estar en este estado  3. Validada: solo una puede estar en este estado  4. muchas pueden estar en este estado. Solo puede ser anulada después de estar en estado 3-validada  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de lecturas e interpretaciones de órdenes de imágenes diagnósticas (RIS). Guarda el ciclo de vida de la lectura: creación, confirmación, anulación y transcripción del informe radiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno autogenerado para cada registro de lectura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la orden de imagen diagnóstica (RIS) que está siendo leída o interpretada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'IDRISORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISLECTURA', @level2type = N'COLUMN', @level2name = N'IDRISORDEN';
