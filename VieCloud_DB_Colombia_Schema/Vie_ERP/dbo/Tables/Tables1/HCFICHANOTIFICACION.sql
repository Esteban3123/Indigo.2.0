CREATE TABLE [dbo].[HCFICHANOTIFICACION] (
    [ID]                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO]          CHAR (10)                                                                        NULL,
    [IDETIPHIS]          CHAR (9)                                                                         NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [CODCENATE]          CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NOT NULL,
    [ESTADO]             INT                                                                              NOT NULL,
    [TIPOFICHA]          INT                                                                              NOT NULL,
    [CODUSUARIO]         CHAR (20)                                                                        NOT NULL,
    [FECHACREACION]      DATETIME                                                                         NOT NULL,
    [UPGD]               VARCHAR (50)                                                                     NULL,
    [RAZONSOCIAL]        VARCHAR (100)                                                                    NULL,
    [NOMBEVENTO]         VARCHAR (500)                                                                    NULL,
    [CODEVENTO]          VARCHAR (10)                                                                     NULL,
    [FECHANOTIFI]        DATE                                                                             NULL,
    [TIPODOCUMENTO]      INT                                                                              NULL,
    [TELEFONO]           VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')          NULL,
    [EDAD]               INT                                                                              NULL,
    [UNIDADMED]          INT                                                                              NULL,
    [SEXO]               INT                                                                              NULL,
    [CODPAIS]            VARCHAR (5)                                                                      NULL,
    [CODDEPARTAMENTO]    VARCHAR (50)                                                                     NULL,
    [AREA]               INT                                                                              NULL,
    [LOCALIDAD]          VARCHAR (150)                                                                    NULL,
    [BARRIO]             VARCHAR (150)                                                                    NULL,
    [CENTRORURAL]        VARCHAR (150)                                                                    NULL,
    [VEREDA]             VARCHAR (150)                                                                    NULL,
    [OCUPACION]          VARCHAR (5)                                                                      NULL,
    [TIPOREGIMEN]        INT                                                                              NULL,
    [ETNIA]              INT                                                                              NULL,
    [ADMINPLANBENE]      INT                                                                              NULL,
    [CODENTIDA]          VARCHAR (3)                                                                      NULL,
    [RESIDENCIA]         VARCHAR (200)                                                                    NULL,
    [DIRRESIDENCIA]      VARCHAR (150)                                                                    NULL,
    [FECHACONSULTA]      DATE                                                                             NULL,
    [FECHAINICOSINTO]    DATE                                                                             NULL,
    [CLASIFICACIONCASO]  INT                                                                              NULL,
    [HOSPITALIZADO]      BIT                                                                              NULL,
    [FECHAHOSP]          DATE                                                                             NULL,
    [CONDICIONFINAL]     INT                                                                              NULL,
    [FECHADIFUNCION]     DATE                                                                             NULL,
    [NUMCERTIFICADO]     VARCHAR (50)                                                                     NULL,
    [CAUSAMUERTE]        VARCHAR (500)                                                                    NULL,
    [TELNOTIFICA]        VARCHAR (15)                                                                     NULL,
    [NOMPROFESIONAL]     VARCHAR (200)                                                                    NULL,
    [CODDIAGNO]          CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [MOTIVODESCAR]       INT                                                                              NULL,
    [OBSERVADESCARTE]    VARCHAR (MAX)                                                                    NULL,
    [USUDESCARTA]        CHAR (20)                                                                        NULL,
    [FECHADESCARTE]      DATETIME                                                                         NULL,
    [USUREPORTASIVIGILA] CHAR (20)                                                                        NULL,
    [FECHAREPORSIVIGILA] DATETIME                                                                         NULL,
    [USUARIOVALIDO]      CHAR (20)                                                                        NULL,
    [FECHAVALIDO]        DATETIME                                                                         NULL,
    [ESTRATO]            INT                                                                              NULL,
    [FUENTE]             INT                                                                              NULL,
    [NACIONALIDAD]       VARCHAR (500)                                                                    NULL,
    [SEMANASGESTACION]   NUMERIC (18, 1)                                                                  NULL,
    [VERSION]            VARCHAR (20)                                                                     NULL,
    [JSON]               VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_HCFICHAGENERALES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHANOTIFICACION_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHANOTIFICACION_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFICHANOTIFICACION_HCDESCARTEFICHA] FOREIGN KEY ([MOTIVODESCAR]) REFERENCES [dbo].[HCDESCARTEFICHA] ([ID]),
    CONSTRAINT [FK_HCFICHANOTIFICACION_HCFICHANOTIFICACION] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID]),
    CONSTRAINT [FK_HCFICHANOTIFICACION_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCFICHANOTIFICACION] NOCHECK CONSTRAINT [CK_HCFICHANOTIFICACION_JSON];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFICHANOTIFICACION].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFICHANOTIFICACION].[TELEFONO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFICHANOTIFICACION].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON; almacena columnas dinámicas de la ficha de notificación; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del esquema de ficha de notificación; NULL = primera versión; VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'versión de la tabla cabecera o datos básicos de fichas de notificación ---> Si es valor nulo = primer version ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de gestación al momento de notificación; embarazo, gestación; NUMERIC(18,1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'SEMANASGESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas de Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'SEMANASGESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'SEMANASGESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nacionalidad del paciente; país de origen, procedencia; VARCHAR(500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NACIONALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nacionalidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NACIONALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NACIONALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuente de origen o reporte de la notificación; origen de caso, procedencia; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda fuente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FUENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estrato socioeconómico del paciente; clasificación económica; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ESTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ESTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ESTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de validación de la ficha por profesional autorizado; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAVALIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAVALIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAVALIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que validó la ficha de notificación; profesional validador; CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUARIOVALIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUARIOVALIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUARIOVALIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de reporte a vigilancia epidemiológica SIVIGILA; notificación vigilancia; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAREPORSIVIGILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAREPORSIVIGILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAREPORSIVIGILA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que reportó a vigilancia epidemiológica SIVIGILA; CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUREPORTASIVIGILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUREPORTASIVIGILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUREPORTASIVIGILA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se descartó el caso de la ficha; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHADESCARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha descarte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHADESCARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHADESCARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que generó descarte del caso; CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUDESCARTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario descarte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUDESCARTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'USUDESCARTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o justificación del descarte del caso; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'OBSERVADESCARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observción de descarte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'OBSERVADESCARTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'OBSERVADESCARTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de descarte del caso; referencia FK a HCDESCARTEFICHA; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'MOTIVODESCAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de descarte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'MOTIVODESCAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'MOTIVODESCAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 enmascarado; diagnóstico principal, código enfermedad; CHAR(4) PII Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo diagnostico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del profesional de salud que diligencia la ficha; médico, enfermero; VARCHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NOMPROFESIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del profesional que diligeció la ficha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NOMPROFESIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NOMPROFESIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto para notificación; número de celular, comunicación; VARCHAR(15)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TELNOTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TELNOTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TELNOTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa básica de muerte registrada en certificado defunción; diagnóstico mortal; VARCHAR(500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Causa básica de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de certificado de defunción del paciente; fallecimiento; VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMCERTIFICADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. certificado defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMCERTIFICADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMCERTIFICADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de defunción del paciente; fallecimiento, muerte; DATE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHADIFUNCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha defunción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHADIFUNCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHADIFUNCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición final del caso: 1=Vivo, 2=Muerto, 3=No sabe/No responde; desenlace clínico; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CONDICIONFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Condición final:   1=Vivo   2=Muerto   3=No sabe, no responde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CONDICIONFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CONDICIONFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de hospitalización del paciente; ingreso hospitalario; DATE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAHOSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el paciente fue hospitalizado: True=Sí, False=No; urgencia, internación; BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hospitalizado:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'HOSPITALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación epidemiológica: 1=Sospechoso, 2=Probable, 3=Confirmado laboratorio, 4=Confirmado clínico, 5=Confirmado nexo; categoría caso; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación inicial de caso:   1=Sospechoso   2=Probable   3=Conf. por laboratorio   4=Conf. clínica   5=Conf. nexo epidemiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONCASO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de síntomas del paciente; aparición síntomas, enfermedad; DATE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAINICOSINTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicio de sístomas:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAINICOSINTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHAINICOSINTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la consulta médica donde se identifica caso; atención, cita médica; DATE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHACONSULTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHACONSULTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHACONSULTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de residencia del paciente; domicilio, ubicación; VARCHAR(150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'DIRRESIDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'DIRRESIDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'DIRRESIDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento y municipio de residencia habitual; localización geográfica; VARCHAR(200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'RESIDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dpto y municipio residencia:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'RESIDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'RESIDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad prestadora de salud; EPS, institución; VARCHAR(3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administradora de planes de beneficios; aseguradora, ARP; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ADMINPLANBENE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administradora planes de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ADMINPLANBENE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ADMINPLANBENE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pertenencia étnica: 1=Indígena, 2=ROM/gitano, 3=Raizal, 4=Palenquero, 5=Negro/mulato/afrocolombiano, 6=Otro; población especial; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ETNIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pertenencia étnica:  1=Indígena   2=Rom, gitano   3=Raizal   4=Palenquero   5=Negro, mulato afro colombiano   6=Otro     Fichas distritales:  1. Indígena  2. ROM  3. Raizal  4. Caucásico  5. Palenquero  6. Mulato  7. Afrocolombiano  8. Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ETNIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ETNIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo régimen en salud: 1=Excepción, 2=Especial, 3=Contributivo, 4=Subsidiado, 5=No asegurado, 6=Indeterminado; cobertura aseguramiento; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOREGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de régimen en salud:   1=Exepción   2=Especial   3=Contributivo   4=Subsidiado   5=No asegurado   6=Indeterminado/pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOREGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOREGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ocupación laboral del paciente; profesión, actividad económica; VARCHAR(5)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'OCUPACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ocupación del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'OCUPACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'OCUPACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vereda o zona rural de ocurrencia; localidad rural; VARCHAR(150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'VEREDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vereda/zona', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'VEREDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'VEREDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cabecera municipal/centro poblado/rural disperso de caso; zona poblada; VARCHAR(150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CENTRORURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cabecera mpal/centro poblado/rural disperso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CENTRORURAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CENTRORURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Barrio o sector de ocurrencia del caso; localidad urbana; VARCHAR(150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'BARRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Barrio de ocurrencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'BARRIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'BARRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localidad administrativa de ocurrencia del caso; división geográfica; VARCHAR(150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'LOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localidad de ocurrencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'LOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'LOCALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área de ocurrencia: 1=Cabecera municipal, 2=Centro poblado, 3=Rural disperso; zona geográfica; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'AREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Área ocurrencia del caso:   1=Cabecera municipal   2=Centro poblado   3=Rural disperso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'AREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'AREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código departamento/municipio de ocurrencia del caso; región geográfica; VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODDEPARTAMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dpto/municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODDEPARTAMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODDEPARTAMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código país de ocurrencia del caso; nación donde sucedió; VARCHAR(5)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'País de ocurrencia del caso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo/género del paciente; M=Masculino, F=Femenino; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida edad: 1=Años, 2=Días, 3=Minutos, 4=Meses, 5=Horas, 6=No aplica; escala temporal; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UNIDADMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida:   1=Años   2=Días   3=Minutos   4=Meses   5=Horas   6=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UNIDADMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UNIDADMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del paciente en unidad especificada; años, meses, días; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto del paciente enmascarado; celular, comunicación; VARCHAR(15) PII Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo documento: 1=RC, 2=TI, 3=CC, 4=CE, 5=PA, 6=MS, 7=AS, 8=PE, 9=CN, 10=CD, 11=SC, 13=DE; identificación, cédula; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Documento (para Ficha Notificación):   1= RC - Registro Civil   2= TI - Tarjeta de Identidad   3= CC - Cédula de Ciudadanía   4= CE - Cédula de Extranjería   5= PA - Pasaporte   6= MS - Menor Sin Identificación   7= AS - Adulto Sin Identificación   8= PE - Permiso Especial de Permanencia   9= CN - Certificado de Nacido Vivo   10= CD - Carnet Diplomático   11= SC - Salvoconducto   13= DE - Documento Extranjero    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se registra la notificación del caso; reporte epidemiológico; DATE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHANOTIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHANOTIFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHANOTIFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del evento de salud notificable; código enfermedad, RIPS; VARCHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del evento de salud o enfermedad notificable; descripción diagnóstico; VARCHAR(500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NOMBEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NOMBEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NOMBEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón social de la institución prestadora; nombre centro de atención; VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'RAZONSOCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razon social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'RAZONSOCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'RAZONSOCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Primaria Generadora de Datos; centro de notificación, RIPS; VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UPGD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la UPGD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UPGD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UPGD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de ficha; registro inicial; DATETIME', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario/profesional que crea ficha; empleado, profesional salud; CHAR(20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo profesional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ficha de notificación (0-61): Ficha100, Ficha110, Ficha115, Ficha155, etc.; formulario epidemiológico; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOFICHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ficha  NoAplica = 0          Ficha100 = 1          Ficha110 = 2          Ficha115 = 3          Ficha155 = 4          Ficha205 = 5          Ficha210 = 6          Ficha348 = 7          Ficha356 = 8          Ficha549 = 9          Ficha560 = 10          Ficha580 = 11          Ficha605 = 12          Ficha610 = 13          Ficha670 = 14          Ficha875 = 15          Ficha217 = 16          Ficha365 = 17          Ficha450 = 18          Ficha760 = 19          Ficha230 = 20          Ficha342 = 21          Ficha850 = 22          Ficha298 = 23          Ficha300 = 24          Ficha813 = 25          Ficha825 = 26          Ficha465 = 27          Ficha113 = 28          Ficha215 = 29          Ficha228 = 30          Ficha305 = 31          Ficha770 = 32          Ficha591 = 33          Ficha750 = 34          Ficha740 = 35          Ficha720 = 36          Ficha310 = 37          Ficha340 = 38          Ficha420 = 39          Ficha430 = 40          Ficha440 = 41          Ficha455 = 42          Ficha200 = 43          Ficha357 = 44          Ficha452 = 45          Ficha453 = 46          Ficha895 = 47          Ficha535 = 48          Ficha800 = 49          Ficha710 = 50          Ficha730 = 51          Ficha550 = 52          Ficha345 = 53          Ficha351 = 54          Ficha352 = 55          Ficha355 = 56          Ficha220 = 57          Ficha831 = 58          Ficha620 = 59          Ficha330 = 60          Ficha346 = 61', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOFICHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'TIPOFICHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la ficha: 0=Inactiva, 1=Activa, etc.; estatus registro; INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional donde se atiende; departamento, unidad; CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución prestadora; hospital, clínica; CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente; FK a ADINGRESO; identificador atención; CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del paciente enmascarada (cédula, pasaporte, etc.); FK a INPACIENT; PII; VARCHAR(25) Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Identificación:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tipo de historia clínica del paciente; historia clínica; CHAR(9)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador tipo de historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de la ficha de notificación; identificador documento; CHAR(10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de ficha de notificación; clave primaria; INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fichas de notificación obligatoria de eventos en salud pública (SIVIGILA). Registra los datos clínicos, demográficos y epidemiológicos del paciente y el evento notificable (enfermedad de reporte obligatorio), incluyendo su seguimiento, validación, descarte y envío al sistema nacional de vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHANOTIFICACION';
