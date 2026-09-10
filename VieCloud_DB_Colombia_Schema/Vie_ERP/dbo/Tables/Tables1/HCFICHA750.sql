CREATE TABLE [dbo].[HCFICHA750] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CONDMOMEDIAG]        INT           NULL,
    [CTRLPRENEMBACT]      BIT           NULL,
    [EDADGESTPRIMCTRL]    VARCHAR (50)  NULL,
    [DIAGEMBACT]          BIT           NULL,
    [PRUEBATREPO]         BIT           NULL,
    [EDGESTREAPRUE]       VARCHAR (50)  NULL,
    [CUAL]                INT           NULL,
    [RESULTADO]           BIT           NULL,
    [PRUEBANOTREPO]       BIT           NULL,
    [EDGESTREAPRUE2]      VARCHAR (50)  NULL,
    [RESULTADO2]          INT           NULL,
    [PENICILINA]          INT           NULL,
    [FECHAAPLI]           DATE          NULL,
    [TRATCONTAC]          BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA750] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA750_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA750_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA750] NOCHECK CONSTRAINT [CK_HCFICHA750_JSON];




GO
ALTER TABLE [dbo].[HCFICHA750] NOCHECK CONSTRAINT [CK_HCFICHA750_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON; tipo VARCHAR(MAX) con validación ISJSON; almacena campos dinámicos de la notificación de sífilis materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; NULL indica primera versión; controla iteraciones de actualización del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tratamiento de contactos en sífilis materna; BIT (1=sí, 2=no); indica si se realizó tratamiento preventivo a parejas sexuales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'TRATCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tratamiento de contactos (TRATAMIENTO MATERNO, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'TRATCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'TRATCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aplicación de la primera dosis de penicilina benzatínica; tipo DATE; parte del protocolo de tratamiento materno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha aplicación primera dosis en la  (TRATAMIENTO MATERNO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'FECHAAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Penicilina benzatínica - número de dosis aplicadas; INT (1=0 dosis, 2=1 dosis, 3=2 dosis, 4=3 dosis); tratamiento materno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PENICILINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Penicilina benzatínica - número de Dosis  (TRATAMIENTO MATERNO, (1. 0 dosis, 2. 1 dosis, 3. 2 dosis, 4. 3 dosis))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PENICILINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PENICILINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de prueba no treponémica (VDRL o RPR); INT con diluciones (1-11 correspondientes a ≤2 DILS hasta 2048 DILS); serología materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO2 ( PRUEBA NO TREPONEMICA (VDRL ó RPR), (1. (3) <= 2 DILS, 2. (4) 4 DILS, 3. (5) 8 DILS, 4. (6) 16 DILS, 5. (7) 32 DILS, 6. (8) 64 DILS, 7. (9) 128 DILS, 8. (10) 256 DILS, 9. (11) 512 DILS, 10. (12) 1024 DILS), 11. (13) 2048 DILS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional a realización de prueba no treponémica; VARCHAR(50) en semanas; permite seguimiento temporal de serología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad gestacional a la realización de la prueba (PRUEBA TREPONÉMICA, (semanas))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba no treponémica (VDRL o RPR) realizada; BIT (1=sí, 2=no); complementa diagnóstico treponémico en gestante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBANOTREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba no treponémica (PRUEBA NO TREPONEMICA (VDRL ó RPR, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBANOTREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBANOTREPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de prueba treponémica; BIT (1=positivo, 2=negativo); confirmación diagnóstica de sífilis en embarazada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO ( PRUEBA TREPONÉMICA, (1. positivo, 2. negativo))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba treponémica utilizada; INT (4=rápida, 5=otra); especifica metodología de confirmación serológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual (PRUEBA TREPONÉMICA, (4. Prueba rápida, 5. Otra))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional a realización de prueba treponémica; VARCHAR(50) en semanas; documenta momento del tamizaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad gestacional a la realización de la prueba  (PRUEBA TREPONÉMICA, (semanas))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDGESTREAPRUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba treponémica realizada; BIT (1=sí, 2=no); indica screening inicial en notificación de sífilis gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBATREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba treponémica (PRUEBA TREPONÉMICA, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBATREPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'PRUEBATREPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico de sífilis en embarazo actual; INT (1=primera vez, 2=reinfección); clasificación de caso materno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'DIAGEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnóstico embarazo actual (DIAGNÓSTICO MATERNO, (1. primera vez, 2. Reinfección))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'DIAGEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'DIAGEMBACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al primer control prenatal; VARCHAR(50) en semanas; establece baseline de seguimiento obstétrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad gestacional al primer control prenatal (DIAGNÓSTICO MATERNO, (semanas))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMCTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control prenatal en embarazo actual realizado; BIT (1=sí, 2=no); evaluación de acceso a atención obstétrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CTRLPRENEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Control prenatal en embarazo actual? (DIAGNÓSTICO MATERNO, (1. si, 2. no))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CTRLPRENEMBACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CTRLPRENEMBACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición maternal al momento del diagnóstico; INT (1=embarazo, 2=parto, 3=puerperio); contexto clínico de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CONDMOMEDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condición al momento del diagnóstico (DIAGNÓSTICO MATERNO, (1. embarazo, 2. parto, 3. puerperio))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CONDMOMEDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CONDMOMEDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 de sífilis; CHAR(4); clasificación estandarizada de la enfermedad notificable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con ficha padre HCFICHANOTIFICACION; INT FK; vincula datos específicos de sífilis materna a notificación general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo; INT IDENTITY(1,1); clave primaria de la ficha de notificación de sífilis gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación 750 (sífilis gestacional y congénita): registra los datos clínicos y de laboratorio para la vigilancia epidemiológica de sífilis en gestantes, incluyendo diagnóstico, control prenatal, pruebas treponémicas y no treponémicas, resultados, tratamiento con penicilina y seguimiento al contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA750';
