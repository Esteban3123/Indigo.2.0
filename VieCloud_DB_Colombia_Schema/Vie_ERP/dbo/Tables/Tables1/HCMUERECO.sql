CREATE TABLE [dbo].[HCMUERECO] (
    [AUTO]       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOLABOR]  INT            NOT NULL,
    [NUMUESTRA]  INT            NOT NULL,
    [ESTMUESTRA] INT            NOT NULL,
    [FECRECMUE]  DATETIME       NULL,
    [USURECMUE]  CHAR (20)      NULL,
    [OBSRVAMUE]  VARCHAR (300)  NULL,
    [RESULTADO]  BIT            NULL,
    [AUTODOCUM]  INT            NULL,
    [EXTDOCUME]  VARCHAR (10)   NULL,
    [INTERPRET]  VARCHAR (4000) NULL,
    CONSTRAINT [PK_HCMUERECO] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación clínica de la muestra (análisis, hallazgos, diagnóstico laboratorio). VARCHAR(4000), texto libre para resultado interpretado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión del archivo documento adjunto (PDF, JPG, DOC, etc.). VARCHAR(10), formato de archivo resultado o evidencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'EXTDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extension Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'EXTDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'EXTDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del documento adjunto a la muestra (FK documento). INT, referencia archivo resultado o imagen diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTODOCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Documento Adjunto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTODOCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTODOCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la muestra tiene documento resultado adjunto (0=Sin documento, 1=Con documento). BIT, marca presencia de evidencia digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si la muestra se adjunto un documento como resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y motivos de anulación de la muestra (rechazo, hemólisis, insuficiencia, etc.). VARCHAR(300), notas sobre estado especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'OBSRVAMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para especificar cuando se ha anulado una muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'OBSRVAMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'OBSRVAMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/profesional que recolectó la muestra (enfermero, laboratorista, técnico). CHAR(20), trazabilidad personal en cadena custodia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Recolecta Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'USURECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recolección/recepción de la muestra en laboratorio. DATETIME, timestamp para control de tiempos de procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Recoleccion Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la muestra (0=No recolectada, 1=Recolectada, 2=Anulada, 3=Con resultado, 4=Interpretada). INT, ciclo de vida laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'ESTMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Muetra 0:No Recolectada;1:Recolectada;2:Anulada;3:Resultado;4:Interpretada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'ESTMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'ESTMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial único de la muestra recolectada (identificador muestra). INT, código de seguimiento en laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Muestra Recolectada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico del laboratorio/orden de laboratorio (FK). INT, referencia a orden laboratorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico del Laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único del registro de recepción de muestra. INT IDENTITY, clave primaria para auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de recepción de muestras de laboratorio clínico. Guarda el estado, fecha de recepción, observaciones e interpretación de cada muestra asociada a una orden de laboratorio, así como el documento de resultado vinculado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMUERECO';
