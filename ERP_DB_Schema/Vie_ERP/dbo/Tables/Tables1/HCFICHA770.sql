CREATE TABLE [dbo].[HCFICHA770] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMMADRE]            VARCHAR (50)  NULL,
    [EDADMADRE]           VARCHAR (50)  NULL,
    [LLANTONACER]         BIT           NULL,
    [MAMANORMAL]          BIT           NULL,
    [DEJOMAMAR]           BIT           NULL,
    [QUEFECHA]            DATE          NULL,
    [HIPERTEMIA]          BIT           NULL,
    [FONTANELA]           BIT           NULL,
    [RIGIDEZNUCA]         BIT           NULL,
    [TRISMUS]             BIT           NULL,
    [CONVULSIONES]        BIT           NULL,
    [ESPASMOS]            BIT           NULL,
    [CONTRACCIONES]       BIT           NULL,
    [OPISTOTONOS]         BIT           NULL,
    [LLANTOEXCE]          BIT           NULL,
    [SEPSISUMBI]          BIT           NULL,
    [NUMEMBA]             VARCHAR (50)  NULL,
    [ASISTICONTROL]       BIT           NULL,
    [EXPLINOASIS]         VARCHAR (50)  NULL,
    [ATENMED]             BIT           NULL,
    [ATENDENF]            BIT           NULL,
    [ATENDAUX]            BIT           NULL,
    [ATENPROM]            BIT           NULL,
    [ATENOTRO]            BIT           NULL,
    [OTROQUIEN]           VARCHAR (50)  NULL,
    [NUMCONTROL]          VARCHAR (50)  NULL,
    [ULTICONTROL]         DATE          NULL,
    [LUGAREMB]            BIT           NULL,
    [QUEMUNI]             VARCHAR (50)  NULL,
    [ANTEVAC]             BIT           NULL,
    [NUMDOSIS]            VARCHAR (50)  NULL,
    [EXPLINORECIB]        VARCHAR (50)  NULL,
    [FECHTD1]             DATE          NULL,
    [FECHTD2]             DATE          NULL,
    [FECHTD3]             DATE          NULL,
    [FECHTD4]             DATE          NULL,
    [LUGARPART]           BIT           NULL,
    [QUIENATENPAR]        INT           NULL,
    [APLISUST]            BIT           NULL,
    [CUALSUST]            VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA770] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA770_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA770_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA770] NOCHECK CONSTRAINT [CK_HCFICHA770_JSON];


GO
ALTER TABLE [dbo].[HCFICHA770] NOCHECK CONSTRAINT [FK_HCFICHA770_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Cuál sustancia? (FACTORES DE RIESGO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CUALSUST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Se aplicaron sustancias en el muñón umbilical? (FACTORES DE RIESGO, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'APLISUST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quién atendió al parto (FACTORES DE RIESGO, (1. Médico, 2. enfermera, 3. Auxiliar, 4. Promotora, 5. Partera Complementada, 6. Partera no Complementada, 7. Familiar, 8. Sola, 9. Otro))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUIENATENPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar del parto (FACTORES DE RIESGO, (1. Casa, 2. Institución de salud))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGARPART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la dosis 4 (ANTECEDENTES VACUNALES, TD4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la dosis 3 (ANTECEDENTES VACUNALES, TD3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la dosis 2 (ANTECEDENTES VACUNALES, TD2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la dosis 1 (ANTECEDENTES VACUNALES, TD1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Si marcó no, explique porqué  no recibió la vacuna (ANTECEDENTES VACUNALES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINORECIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis TdaP ó TD (ANTECEDENTES VACUNALES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Antecedentes de vacunación antitetánica? ( ANTECEDENTES VACUNALES, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ANTEVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'En caso negativo,  ¿en qué municipio? (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEMUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿ Vivió la madre en el mismo lugar durante el embarazo? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGAREMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Último control prenatal (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ULTICONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de controles (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Sí marco otro quién? (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Atendido por otro? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Atendido por promotor (a)? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENPROM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Atendido por auxiliar? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDAUX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Atendido por enfermero (a)? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Atendido por médico? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Sí marcó no, explique porqué no asistió a control prenatal (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINOASIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Asistió a control prenatal? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ASISTICONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de embarazos con el del caso (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMEMBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Sepsis umbilical? (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'SEPSISUMBI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Llanto excesivo? (DATOS CLÌNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTOEXCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Opistótonos (DATOS CLÌNICOS, (1. Sí, 2. No)) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contracciones (DATOS CLÌNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONTRACCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Espasmos (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ESPASMOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convulsiones (DATOS CLÌNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TRISMUS (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'TRISMUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rigidez en la nuca (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fontanela abonbada (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FONTANELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hipertemia (INFORMACIÓN GENERAL , (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'HIPERTEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'En que fecha (INFORMACIÓN GENERAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Dejo de mamar? (INFORMACIÓN GENERAL , (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'DEJOMAMAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Mamaba normal al nacer? (INFORMACIÓN GENERAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'MAMANORMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Llanto al nacer? (INFORMACIÓN GENERAL , (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de la madre (INFORMACIÓN GENERAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EDADMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NOMMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ID';

