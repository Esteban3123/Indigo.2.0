CREATE TABLE [dbo].[HCFICHA560] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [GESTACION]           INT           NULL,
    [PARTOSVAG]           INT           NULL,
    [CESAREAS]            INT           NULL,
    [ABORTOS]             INT           NULL,
    [MUERTOS]             INT           NULL,
    [VIVOS]               INT           NULL,
    [NUMCPN]              INT           NULL,
    [SEMANACPN]           INT           NULL,
    [TIPOPARTO]           INT           NULL,
    [FECHAPARTO]          DATETIME      NULL,
    [SITIODEFUN]          INT           NULL,
    [PARTOATENDI]         INT           NULL,
    [SITIOPARTO]          INT           NULL,
    [NIVELATEN2]          INT           NULL,
    [MOMENTOMUER]         INT           NULL,
    [EDADGESTACIO]        INT           NULL,
    [EDADNEONATAL]        INT           NULL,
    [PESONACER]           INT           NULL,
    [TALLANACER]          INT           NULL,
    [SEXO]                INT           NULL,
    [CAUSAMUERTE]         INT           NULL,
    [CAUFETALES]          CHAR (4)      NULL,
    [CAUNEONATALES]       CHAR (4)      NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA560] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA560_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA560_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA560] NOCHECK CONSTRAINT [CK_HCFICHA560_JSON];




GO
ALTER TABLE [dbo].[HCFICHA560] NOCHECK CONSTRAINT [CK_HCFICHA560_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nuevas columnas en formato JSON, almacenamiento flexible de datos adicionales de la notificación perinatal (VARCHAR MAX, validado con ISJSON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación perinatal; valor nulo indica primera versión, incrementa en revisiones posteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 (CHAR 4), clasificación de diagnóstico principal del evento perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa neonatales (CHAR 4), código CIE-10 de causa de muerte en período neonatal (0-28 días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUNEONATALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa neonatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUNEONATALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUNEONATALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa fetales (CHAR 4), código CIE-10 de causa de muerte fetal o pérdida gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUFETALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa fetales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUFETALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUFETALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de muerte, clasificación principal del evento que generó la defunción perinatal o neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género/sexo del recién nacido: M (masculino), F (femenino), I (indeterminado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genero (M. masculino, F. Femenino, I. Indeterminado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla al nacer en centímetros, medida antropométrica del neonato al momento del nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TALLANACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TALLANACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TALLANACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso al nacer en gramos, medida antropométrica crítica del recién nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso al nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PESONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad neonatal al momento del evento, en días (período 0-28 días posparto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADNEONATAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADNEONATAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADNEONATAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional en semanas cumplidas al parto o al evento perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADGESTACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADGESTACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'EDADGESTACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento de la muerte respecto al parto: 1=anteparto, 2=intraparto, 3=prealta postparto, 5=postalta postparto, 6=reingreso postparto, 7=nunca asistió IPS en postparto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MOMENTOMUER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Momento en que ocurrio la muerte respecto al parto (1. anteparto, 2. intraparto, 3. prealta en postpart, 5. postalta en postparto, 6. reingreso en postparto, 7. no aplica. nunca fue a la institucion de salud en postparto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MOMENTOMUER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MOMENTOMUER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de atención donde ocurrió el parto: 1=baja complejidad, 2=mediana complejidad, 3=alta complejidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NIVELATEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de atención (1. baja complejidad, 2. mediana complejidad, 3. alta complejidad)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NIVELATEN2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NIVELATEN2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio del parto: 1=institucional, 2=domicilio, 3=otro lugar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio del parto (1. institucional, 2. domicilio, 3. otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional/persona que atendió el parto: 1=médico general, 2=obstetra, 3=enfermera, 4=auxiliar enfermería, 5=promotor, 6=partera, 7=otro, 8=auto-atendido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parto atendido por (1. médico general, 2. médico obstetra, 3. enfermera, 4. aux. enfermeria, 5. promotor, 6. partera, 7. otro, 8. ella misma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio de defunción: 6=domicilio, 7=otro, 8=baja complejidad, 9=mediana complejidad, 10=alta complejidad, 11=UCI, 12=traslado interinstitucional, 13=traslado a domicilio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio de defunción (6. domicilio, 8. baja complejidad, 9. mediana complejidad, 10. alta complejidad, 11. UCI, 12. traslado interinstitucional, 13. traslado a domicilio IPS, 7. otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del parto (DATETIME), registro del día y hora del nacimiento o evento perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'FECHAPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'FECHAPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'FECHAPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de parto: 1=vaginal, 2=cesárea, 3=instrumentado, 4=ignorado, 5=no nació', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del parto (1. vaginal, 2. cesárea, 3. instrumentado, 4. ignorado, 5. no nacio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semana de inicio del control prenatal (CPN), semana gestacional de primera atención prenatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEMANACPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semana inicio CPN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEMANACPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'SEMANACPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de controles prenatales (CPN) realizados durante la gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NUMCPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número CPN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NUMCPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'NUMCPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hijos vivos previos, antecedente obstétrico de gestaciones con resultado vivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'VIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de vivos (antecedentes prenatales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'VIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'VIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hijos muertos previos, antecedente obstétrico de pérdidas fetales o neonatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MUERTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de muertos (antecedentes prenatales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MUERTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'MUERTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de abortos previos, antecedente obstétrico de interrupciones gestacionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ABORTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de abortos (antecedentes prenatales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ABORTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ABORTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de partos por cesárea previos, antecedente obstétrico de intervenciones quirúrgicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CESAREAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de cesareas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CESAREAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'CESAREAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de partos vaginales previos, antecedente obstétrico de partos por vía vaginal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOSVAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de partos vaginales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOSVAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'PARTOSVAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestación actual o número de embarazo (G), secuencia gestacional de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ficha de notificación (FK → HCFICHANOTIFICACION.ID), vínculo a evento perinatal notificable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (PK), clave primaria de registro de historia clínica ficha 560', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de datos obstétricos y perinatales asociados a una ficha de notificación (ficha 560), incluyendo antecedentes gineco-obstétricos de la madre, información del parto, condiciones del recién nacido y causas de muerte fetal o neonatal. Se usa en la vigilancia epidemiológica de mortalidad perinatal y materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA560';
