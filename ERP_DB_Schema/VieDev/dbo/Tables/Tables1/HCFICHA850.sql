CREATE TABLE [dbo].[HCFICHA850] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [MECAPROBA]           INT           NULL,
    [NOMMADRE]            VARCHAR (50)  NULL,
    [TIPOID]              VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [IDENTGEN]            INT           NULL,
    [DONOSANGRE]          BIT           NULL,
    [TIPOPRUEBA]          INT           NULL,
    [FECHARESUL]          DATE          NULL,
    [VALORCARGA]          VARCHAR (50)  NULL,
    [ESTADCLINI]          INT           NULL,
    [CANDIESO]            BIT           NULL,
    [CANDIVIA]            BIT           NULL,
    [TUBERPULM]           BIT           NULL,
    [CANCERCERV]          BIT           NULL,
    [TUBEREXTRA]          BIT           NULL,
    [COCCIDIO]            BIT           NULL,
    [CITOMEGA]            BIT           NULL,
    [RETINICITO]          BIT           NULL,
    [ENCEFALOVIH]         BIT           NULL,
    [OTRASMICRO]          BIT           NULL,
    [HISTOEXTRA]          BIT           NULL,
    [ISOSPOCRON]          BIT           NULL,
    [HERPESZOST]          BIT           NULL,
    [HISTODISEM]          BIT           NULL,
    [LINFBURKI]           BIT           NULL,
    [NEUMOPNEUMO]         BIT           NULL,
    [NEUMORECU]           BIT           NULL,
    [LINFOINMU]           BIT           NULL,
    [CRIPTOCRON]          BIT           NULL,
    [CRIPTOEXTRA]         BIT           NULL,
    [SARCOKAPOSI]         BIT           NULL,
    [SINDROEMAC]          BIT           NULL,
    [LEUCOMULTI]          BIT           NULL,
    [SEPTIRECUR]          BIT           NULL,
    [TOXOCERE]            BIT           NULL,
    [HEPATB]              BIT           NULL,
    [HEPATC]              BIT           NULL,
    [MENINGITIS]          BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA850] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA850_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA850_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA850] NOCHECK CONSTRAINT [CK_HCFICHA850_JSON];


GO
ALTER TABLE [dbo].[HCFICHA850] NOCHECK CONSTRAINT [FK_HCFICHA850_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Meningitis      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MENINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Hepatitis C      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Hepatitis B      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HEPATB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Toxoplasmosis Cerebral    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TOXOCERE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Septicemia Recurrente por Salmonella    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SEPTIRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Leucoencefalopatía Multifocal    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LEUCOMULTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Síndrome de Emaciación    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SINDROEMAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Sarcoma de Kaposi    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'SARCOKAPOSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Criptococosis Extrapulmonar    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Criptosporidiasis Crónica    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CRIPTOCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Linfoma Inmunoblástico     1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFOINMU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Neumonía Recurrente (Más de 2 episodios en un año)     1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMORECU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Neumonía por Pneumocystis      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NEUMOPNEUMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Linfoma de Burkitt      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'LINFBURKI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Histoplasmosis Diseminada      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTODISEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Herpes zoster en Multiples Dermatomas      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HERPESZOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  IsosporidiasisCrónica      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ISOSPOCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Histoplasmosis Extrapulmonar      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'HISTOEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Otras Microbactérias      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'OTRASMICRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Encefalopatía por VIH      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ENCEFALOVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona Retinitis por Citomegalovirus      1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'RETINICITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona   Citomegalovirosis    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CITOMEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona   Coccidiodomicosis    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'COCCIDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona   Tuberculosis Extrapulmonar    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBEREXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona Cáncer Cervical Invasivo    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANCERCERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona Tuberculosis Pulmonar    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TUBERPULM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Candidiasis de las Vías Areas    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Enfermedades asociadas si, se selecciona  Candidiasis Esofágica    1 =  true    0  = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CANDIESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Estado clínico  1 = Vih  2 = Sida   3 = Muerto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ESTADCLINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Valor de la carga viral (N° de copias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'VALORCARGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la  Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guara el Tipo de prueba de confirmación de diagnóstico  1 =Western Blot   2 = Carga Viral   3 = Prueba Rápida    4 =Elisa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOPRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda ¿Donó sangre en los 12 meses anteriores?  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'DONOSANGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Identidad de género', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDENTGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Número de identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo ID (Documento Identificación) de la Madre --> desde versión ''V01_2020-03-06'' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   9 = CN   10=PT  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Nombre de la madre (En caso de transmisión materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'NOMMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  Mecanismo probable de transmisión      1 = Heterosexual  2=Homosexual    3 = Bisexual     4 = Materno Infantil    5 = Transfusión Sanguínea     6 =Usuarios drogas IV     7 = Accidente de Trabajo    8 =Transplante de Órganos    9 = Piercing  10=Hemodiálisis    11 =Tatuajes 12 =Centro estético  13  =Acupuntura   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'MECAPROBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA850', @level2type = N'COLUMN', @level2name = N'ID';

