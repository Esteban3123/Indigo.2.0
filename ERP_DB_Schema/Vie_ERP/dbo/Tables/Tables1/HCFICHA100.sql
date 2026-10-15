CREATE TABLE [dbo].[HCFICHA100] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [FECHACCIDENTE]       DATE          NULL,
    [DIRACCIDENTE]        VARCHAR (200) NULL,
    [ACTIVIDADACCI]       INT           NULL,
    [OTRACTIVIDAD]        VARCHAR (200) NULL,
    [TIPOATENCION]        INT           NULL,
    [OTRATENCION]         VARCHAR (200) NULL,
    [SOMETIDAPRAC]        INT           NULL,
    [OTRAPRACTICA]        VARCHAR (200) NULL,
    [LOCAMORDEDURA]       INT           NULL,
    [HUELLACOLMILLO]      BIT           NULL,
    [VIOSERPIENTE]        BIT           NULL,
    [SERPCAPTURADA]       BIT           NULL,
    [AGENGENERO]          INT           NULL,
    [OTROAGENGENE]        VARCHAR (200) NULL,
    [AGENNOMBRE]          INT           NULL,
    [OTROAGENNOM]         VARCHAR (200) NULL,
    [EDEMA]               BIT           NULL,
    [DOLOR]               BIT           NULL,
    [ERITEMA]             BIT           NULL,
    [FLICTENAS]           BIT           NULL,
    [PARESTESIAS]         BIT           NULL,
    [EQUIMOSIS]           BIT           NULL,
    [HEMATOMAS]           BIT           NULL,
    [OTRAMANILOCAL]       BIT           NULL,
    [CUALOTRAML]          VARCHAR (200) NULL,
    [NAUSEA]              BIT           NULL,
    [HIPOTENSION]         BIT           NULL,
    [DEBILIDADMUSCU]      BIT           NULL,
    [HEMATEMESIS]         BIT           NULL,
    [DIFIHABLAR]          BIT           NULL,
    [VOMITO]              BIT           NULL,
    [DOLORABDOMI]         BIT           NULL,
    [OLIGURIA]            BIT           NULL,
    [HEMATURINA]          BIT           NULL,
    [DISFAGIA]            BIT           NULL,
    [SIALORREA]           BIT           NULL,
    [FASCIESNEU]          BIT           NULL,
    [CIANOSIS]            BIT           NULL,
    [HEMATOQUEXIA]        BIT           NULL,
    [DIARREA]             BIT           NULL,
    [ALTERAVISION]        BIT           NULL,
    [EPISTAXIS]           BIT           NULL,
    [VERTIGO]             BIT           NULL,
    [BRADICARDIA]         BIT           NULL,
    [ALTERASENSO]         BIT           NULL,
    [GINGIVORRAGIA]       BIT           NULL,
    [PTOSISPALPEBRAL]     BIT           NULL,
    [OTRAMANISISTE]       BIT           NULL,
    [CUALOTRAMS]          VARCHAR (200) NULL,
    [CELULITIS]           BIT           NULL,
    [ABSCESO]             BIT           NULL,
    [NECROSIS]            BIT           NULL,
    [MIONECROSIS]         BIT           NULL,
    [FASCEITIS]           BIT           NULL,
    [ALTERACIRCU]         BIT           NULL,
    [SINDROMECOM]         BIT           NULL,
    [OTRACOMLILOCA]       BIT           NULL,
    [CUALOTRACL]          VARCHAR (200) NULL,
    [ANEMIAGUDA]          BIT           NULL,
    [EDEMACEREBRAL]       BIT           NULL,
    [SHOCKHIPO]           BIT           NULL,
    [FALLAVENTILA]        BIT           NULL,
    [SHOCKSEPTICO]        BIT           NULL,
    [COMA]                BIT           NULL,
    [IRA]                 BIT           NULL,
    [CID]                 BIT           NULL,
    [HEMORRAGIAIN]        BIT           NULL,
    [OTRACOMPSISTE]       BIT           NULL,
    [CUALOTRACS]          VARCHAR (200) NULL,
    [GRACEDADACCI]        INT           NULL,
    [EMPLEOSUERO]         BIT           NULL,
    [TIEMPOTRANS]         NUMERIC (18)  NULL,
    [TIPOSUERO]           INT           NULL,
    [REACCIONES]          INT           NULL,
    [DOSISUERO]           NUMERIC (18)  NULL,
    [TIEMPOADM]           CHAR (5)      NULL,
    [REMITIDO]            BIT           NULL,
    [TRATAMIENTOQX]       BIT           NULL,
    [TIPOTRATAQX]         INT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [FABRICANTE]          INT           NULL,
    [OTROFABRICANTE]      VARCHAR (MAX) NULL,
    [LOTE]                VARCHAR (15)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    [TIEMPOTRANSDIAS]     INT           NULL,
    CONSTRAINT [PK_HCFICHA100] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA100_JSON] CHECK (isjson([JSON])=(1))
);


GO
ALTER TABLE [dbo].[HCFICHA100] NOCHECK CONSTRAINT [CK_HCFICHA100_JSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANSDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote que escribio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otro fabricante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fabricante   1=Probiol  2=Bioclon  3=INS (Instituto Nacional de salud)  4=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de tratamiento quirúrgico  1=Drenaje de absceso  2=Limpieza quirúrgica  3=Desbridamiento  4=Fasciotomía  5=Injerto de piel  6=Amputación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOTRATAQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tratamiento quirúrgico  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Remitido a otra institución  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REMITIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(antigua version) --> Tiempo de administración de suero (Horas-Minutos)      /    (desde version V1_2020-03-06) --> Tiempo de administración de suero (Horas)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis de suero (ampollas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOSISUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reacciones a la aplicación del suero  1=Ninguna  2=Localizada  3=Generalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REACCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de suero antiofídico -->     1 = Antiviperido (Bothrops, Lachesis, Crotálus)   2 = Anti-elapidídico (Micrurus sp: coral verdadera)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOSUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo transcurrido (h)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Empleó Suero  true = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EMPLEOSUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gravedad del accidente  1=Leve  2=Moderado  3=Grave  4=No envenenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GRACEDADACCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otro Complicaciones sistémicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado OTRACOMPSISTE= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMPSISTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado HEMORRAGIAIN= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado CID= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado IRA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado COMA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'COMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado SHOCKSEPTICO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKSEPTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado FALLAVENTILA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FALLAVENTILA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado SHOCKHIPO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKHIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado EDEMACEREBRAL= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMACEREBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones sistémicas  Si   Seleccionado ANEMIAGUDA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ANEMIAGUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otro Complicaciones locales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado OTRACOMLILOCA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMLILOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado SINDROMECOM= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SINDROMECOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado ALTERACIRCU= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERACIRCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado FASCEITIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCEITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado MIONECROSIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'MIONECROSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado NECROSIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NECROSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado ABSCESO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ABSCESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Complicaciones locales  Si   Seleccionado CELULITIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CELULITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otro Manifestaciones sistemicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado OTRAMANISISTE= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANISISTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado PTOSISPALPEBRAL= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PTOSISPALPEBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado GINGIVORRAGIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GINGIVORRAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado ALTERASENSO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERASENSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado BRADICARDIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado VERTIGO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERTIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado EPISTAXIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EPISTAXIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado ALTERAVISION= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERAVISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado DIARREA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado HEMATOQUEXIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOQUEXIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado CIANOSIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CIANOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado FASCIESNEU= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCIESNEU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado SIALORREA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SIALORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado DISFAGIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado HEMATURINA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado OLIGURIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OLIGURIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado DOLORABDOMI= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLORABDOMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado VOMITO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado DIFIHABLAR= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIFIHABLAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado HEMATEMESIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATEMESIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado DEBILIDADMUSCU= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DEBILIDADMUSCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado HIPOTENSION= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones sistémicas  Si     Seleccionado NAUSEA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NAUSEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cual otra Manilocal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAML';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado OTRAMANILOCAL= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANILOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado HEMATOMAS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado EQUIMOSIS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EQUIMOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado PARESTESIAS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado FLICTENAS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FLICTENAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado Eritema= True  si no  Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ERITEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado Dolor= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manifestaciones locales Item   Si   Seleccionado Edema = True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro Age Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENNOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente agresor, nombre común', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro Age Genero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENGENE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente agresor género:  1=Bothrops  2=Crotalus  3=Micrurus  4=lachesis  5=pelamis(serpiente de mar)  6=Colubrido  7=Sin identificar  8=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENGENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Serpiente capturada  True = Sifalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SERPCAPTURADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona vió a serpiente que la mordió  True = Sifalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VIOSERPIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Evidencia huellas de colmillos  True = Si  false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HUELLACOLMILLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización mordedura  1=Cabeza (cara)  2=Miembros superiores  3=Miembros inferiores  4=Tórax anterior  5=Abdomen  6=Espalda  7=Cuello  8=Genitales  9=Glúteos  10=Dedos de pie y de mano  11=Dedos de mano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOCAMORDEDURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otra Practica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAPRACTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Persona sometida a prácticas no médicas?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SOMETIDAPRAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otro Atencion?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1=Incisión  2=Punción  3=Sangría  4=Torniquete  5=Inmovilización del enfermo  6=Inmovilización del miembro  7=Succión mecánica  8=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cual otro actividad?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad que realizaba al momento del accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ACTIVIDADACCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección lugar accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIRACCIDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Accidnete ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FECHACCIDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ID';

