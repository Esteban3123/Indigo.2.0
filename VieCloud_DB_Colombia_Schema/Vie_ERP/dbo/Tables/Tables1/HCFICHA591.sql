CREATE TABLE [dbo].[HCFICHA591] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMAPE]              VARCHAR (50)  NULL,
    [TIPOID]              VARCHAR (50)  NULL,
    [NUMIDEN]             VARCHAR (50)  NULL,
    [EDAD]                VARCHAR (50)  NULL,
    [NUMHIJVIVOS]         VARCHAR (50)  NULL,
    [NUMHIJMUERT]         VARCHAR (50)  NULL,
    [ESTADCONYU]          INT           NULL,
    [ULTIMOANIO]          VARCHAR (50)  NULL,
    [NECROPSIA]           BIT           NULL,
    [CAUSAA]              VARCHAR (MAX) NULL,
    [CAUSAB]              VARCHAR (MAX) NULL,
    [CAUSAC]              VARCHAR (MAX) NULL,
    [CAUSAD]              VARCHAR (MAX) NULL,
    [OTROESTAD]           VARCHAR (MAX) NULL,
    [CAUSAPROB]           VARCHAR (MAX) NULL,
    [CLASIFIN]            INT           NULL,
    [HISTCLINICA]         BIT           NULL,
    [PRUEBLAB]            BIT           NULL,
    [INTERROG]            BIT           NULL,
    [ASISMED]             BIT           NULL,
    [SITIODEF]            INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA591] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA591_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA591_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA591] NOCHECK CONSTRAINT [CK_HCFICHA591_JSON];




GO
ALTER TABLE [dbo].[HCFICHA591] NOCHECK CONSTRAINT [CK_HCFICHA591_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON (VARCHAR MAX); almacena nuevas columnas y extensiones de la ficha de notificación de defunción, validado con restricción ISJSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación de defunción (VARCHAR 20); nulo indica primera versión, ejemplo V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio o lugar de defunción (INT): 1=Hospital/clínica, 2=Centro/puesto de salud, 3=Casa/domicilio, 4=Lugar de trabajo, 5=Vía pública, 6=Otro sitio, 9=Sin información', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'SITIODEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el sitio de defunción (1. Hospital / clínica, 2. Centro / puesto de salud, 3. Casa/domicilio, 4. Lugar de trabajo, 5. Vía pública, 6. Otro sitio, 9. Sin información)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'SITIODEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'SITIODEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de asistencia médica en defunción (BIT); marca si la causa de muerte fue determinada por profesional de salud o médico certificante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ASISMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la asistencia médica (checked or unchecked, causa de muerte determinada por)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ASISMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ASISMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de interrogatorio a familiares o testigos (BIT); marca si se obtuvieron causas de defunción mediante entrevista a allegados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'INTERROG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el Interrogatorio a familiares o testigos (checkout or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'INTERROG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'INTERROG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de pruebas de laboratorio (BIT); marca si las causas de defunción fueron determinadas o confirmadas por resultados de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'PRUEBLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la prueba de laboratorio (checkout or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'PRUEBLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'PRUEBLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de historia clínica consultada (BIT); marca si las causas de defunción se determinaron mediante revisión de registro clínico del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'HISTCLINICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la historia clinica (checkout or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'HISTCLINICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'HISTCLINICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación final de la defunción (INT): 1=Muerte por desnutrición (DNT), 2=Muerte por infección respiratoria aguda (IRA), 3=Muerte por edad avanzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CLASIFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la clasificación final (1. muerte por dnt, 2. muerte por ira, 3. muerte por edad)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CLASIFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CLASIFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa probable de muerte sin certificación médica (VARCHAR MAX); diagnóstico presunto sin validación profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAPROB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa probable de muerte sin certificación médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAPROB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAPROB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros estados civiles o situaciones adicionales (VARCHAR MAX); información complementaria del estado conyugal o personal del fallecido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'OTROESTAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda estado otro estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'OTROESTAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'OTROESTAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de defunción encadenada D, causa más remota (VARCHAR MAX); parte de cadena de causalidad; diagnóstico CIE-10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la causa directa D (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de defunción encadenada C, causa intermedia (VARCHAR MAX); parte de cadena de causalidad en el certificado de defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la causa directa C (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de defunción encadenada B, causa intermedia (VARCHAR MAX); contribuye a la causa directa de defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la causa directa B (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de defunción encadenada A, causa directa, inmediata (VARCHAR MAX); afección o complicación que provocó directamente la muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la causa directa A (causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CAUSAA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de necropsia realizada (BIT); marca si se practicó autopsia para determinar causas de defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NECROPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la necropsia (checkout, or uncheckout, causas de defunción)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NECROPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NECROPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último año aprobado o validado (VARCHAR 50); año de cierre o validación de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ULTIMOANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el ultimo año aprobado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ULTIMOANIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ULTIMOANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado conyugal de la madre (INT): 1=Unión libre ≥2 años, 2=Unión libre <2 años, 3=Divorciada, 4=Viuda; contexto materno o perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ESTADCONYU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el estado conyugal (1. No esta casada y lleva dos o mas años viviendo con pareja, 2. No esta casada y lleva menos de dos años viviendo con pareja, 3. Divorciada, 4. Viuda)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ESTADCONYU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ESTADCONYU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hijos fallecidos (VARCHAR 50); cantidad de descendientes muertos; contexto materno o familiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJMUERT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el numero de hijos muertos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJMUERT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJMUERT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hijos vivos (VARCHAR 50); cantidad de hijos sobrevivientes de la madre; datos vitales familiares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJVIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el número de hijos vivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJVIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMHIJVIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de la madre (VARCHAR 50); años cumplidos; contexto perinatal o mortalidad materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la edad de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación de la madre (VARCHAR 50, PII); cédula, documento o equivalente; datos demográficos maternos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMIDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el número de identificación (datos de la madre del menor)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMIDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NUMIDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación/documento de la madre (VARCHAR 50): RC, TI, CC, CE, PA, MS, AS, PE, CN, CD, SC, DE; desde versión V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo ID (Documento Identificación) de la Madre --> desde versión ''''V01_2020-03-06'''' es con una enumeración:  1= RC - Registro Civil   2= TI - Tarjeta de Identidad   3= CC - Cédula de Ciudadanía   4= CE - Cédula de Extranjería   5= PA - Pasaporte   6= MS - Menor Sin Identificación   7= AS - Adulto Sin Identificación   8= PE - Permiso Especial de Permanencia   9= CN - Certificado de Nacido Vivo   10= CD - Carnet Diplomático   11= SC - Salvoconducto   13= DE - Documento Extranjero    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre y apellido de la madre o fallecido (VARCHAR 50); identificación nominal; datos demográficos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NOMAPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nombre y apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NOMAPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'NOMAPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (CHAR 4); clasificación internacional de enfermedades y causas de defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación padre (INT NOT NULL, FK); relación con tabla HCFICHANOTIFICACION; agrupa datos de evento de defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, clave primaria (INT IDENTITY); consecutivo secuencial de registros en tabla HCFICHA591', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de muerte o defunción (ficha 591 del SIVIGILA). Registra los datos del fallecido, causas básica e inmediata de la muerte, información obstétrica, fuentes de confirmación diagnóstica y clasificación final del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA591';
