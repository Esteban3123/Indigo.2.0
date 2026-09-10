CREATE TABLE [dbo].[HCFICHA217] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [FIEBRE]              BIT           NULL,
    [ARTRALGIA]           BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [RASH]                BIT           NULL,
    [VOMITO]              BIT           NULL,
    [PIEL]                BIT           NULL,
    [HIGADO]              BIT           NULL,
    [BAZO]                BIT           NULL,
    [PULMON]              BIT           NULL,
    [CEREBRO]             BIT           NULL,
    [MIOCARDIO]           BIT           NULL,
    [MEDULA]              BIT           NULL,
    [RINON]               BIT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA217] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA217_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA217_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA217] NOCHECK CONSTRAINT [CK_HCFICHA217_JSON];




GO
ALTER TABLE [dbo].[HCFICHA217] NOCHECK CONSTRAINT [CK_HCFICHA217_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales y dinámicos en formato JSON (VARCHAR MAX), validados con constraintISJSON. Almacena campos extendidos de la ficha de notificación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación (VARCHAR 20). Null = primera versión; valores posteriores indican revisiones o actualizaciones del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CHAR 4). Referencia al código CIE-10 u otro estándar diagnóstico asociado a la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de riñón/renal tomada (BIT). True = muestra recolectada; Null = no se tomó muestra de órgano renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RINON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona RINON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RINON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RINON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de médula ósea tomada (BIT). True = muestra recolectada; Null = no se tomó muestra de médula.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MEDULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona MEDULA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MEDULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MEDULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de miocardio/corazón tomada (BIT). True = muestra recolectada; Null = no se tomó muestra cardíaca.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona MIOCARDIO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de cerebro/líquido cefalorraquídeo (LCR) tomada (BIT). True = muestra recolectada; Null = no se tomó muestra neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEREBRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona CEREBRO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEREBRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEREBRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de pulmón/respiratoria tomada (BIT). True = muestra recolectada; Null = no se tomó muestra pulmonar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PULMON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona PULMON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PULMON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PULMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de bazo tomada (BIT). True = muestra recolectada; Null = no se tomó muestra de órgano linfoide.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'BAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona BAZO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'BAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'BAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de hígado tomada (BIT). True = muestra recolectada; Null = no se tomó muestra hepática.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'HIGADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona HIGADO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'HIGADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'HIGADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muestra de piel/lesión cutánea tomada (BIT). True = muestra recolectada; Null = no se tomó muestra dermatológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestras (Seleccione las muestras tomadas)  Si selecciona PIEL= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'PIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma: vómito/emesis reportado (BIT). True = paciente presenta vómito; Null = síntoma ausente o no evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Síntomas (Seleccione los que se presenten)  Si selecciona   VOMITO= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma: exantema/rash cutáneo reportado (BIT). True = paciente presenta rash; Null = síntoma ausente o no evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RASH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Síntomas (Seleccione los que se presenten)  Si selecciona   RASH= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RASH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'RASH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma: cefalea/dolor de cabeza reportado (BIT). True = paciente presenta cefalea; Null = síntoma ausente o no evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Síntomas (Seleccione los que se presenten)  Si selecciona   CEFALEA= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma: artralgia/dolor articular reportado (BIT). True = paciente presenta artralgia; Null = síntoma ausente o no evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'ARTRALGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Síntomas (Seleccione los que se presenten)  Si selecciona   ARTRALGIA= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'ARTRALGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'ARTRALGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma: fiebre/temperatura elevada reportada (BIT). True = paciente presenta fiebre; Null = síntoma ausente o no evaluado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Síntomas (Seleccione los que se presenten)  Si selecciona   Fiebre = True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación (INT, FK). Referencia a HCFICHANOTIFICACION. Vincula síntomas y muestras a la notificación epidemiológica principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autonumérico del registro (INT, PK, IDENTITY 1,1). Clave primaria de la tabla de síntomas y muestras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de la ficha de notificación epidemiológica (formulario 217) para enfermedades transmitidas por vectores como dengue, zika o chikungunya. Almacena los síntomas y órganos afectados reportados en cada caso notificado, junto con el diagnóstico CIE-10 asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA217';
