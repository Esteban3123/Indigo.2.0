CREATE TABLE [dbo].[HCFICHA580] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [DESPLAZAMIENTO]      BIT           NULL,
    [UBICACION]           CHAR (20)     NULL,
    [SINTODENGUE]         INT           NULL,
    [ESTABLECIMIENTO]     VARCHAR (200) NULL,
    [FIEBRE]              BIT           NULL,
    [CAFALEA]             BIT           NULL,
    [DOLORRECTROO]        BIT           NULL,
    [MIALGIAS]            BIT           NULL,
    [ARTRALGIAS]          BIT           NULL,
    [ERUPCIONRASH]        BIT           NULL,
    [DOLORABDOMINAL]      BIT           NULL,
    [VOMITO]              BIT           NULL,
    [DIARREA]             BIT           NULL,
    [SOMNOLENCIA]         BIT           NULL,
    [HIPOTENSION]         BIT           NULL,
    [HEPATOMEGALIA]       BIT           NULL,
    [HEMORRAGIAS]         BIT           NULL,
    [HIPOTERMIA]          BIT           NULL,
    [AUMENTOHEMAT]        BIT           NULL,
    [CAIDAPLAQUETAS]      BIT           NULL,
    [ACOMULACIONLIQUID]   BIT           NULL,
    [EXTRAVASACION]       BIT           NULL,
    [HEMOCOMPROMISO]      BIT           NULL,
    [SHOCKDENGUE]         BIT           NULL,
    [DAÑOORGANOS]         BIT           NULL,
    [CLASIFICACIONFINAL]  INT           NULL,
    [CONDUCTA]            INT           NULL,
    [TEJIDOS]             BIT           NULL,
    [HIGADO]              BIT           NULL,
    [BRAZO]               BIT           NULL,
    [PULMON]              BIT           NULL,
    [CEREBRO]             BIT           NULL,
    [MIOCARDIO]           BIT           NULL,
    [MEDULA]              BIT           NULL,
    [RINON]               BIT           NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECEPCION]      DATE          NULL,
    [MUESTRA]             INT           NULL,
    [PRUEBA]              VARCHAR (2)   NULL,
    [AGENTE]              INT           NULL,
    [RESULTADO]           INT           NULL,
    [FECHARESULTADO]      DATE          NULL,
    [VALORREGISTRADO]     VARCHAR (200) NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA580] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA580_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA580_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA580] NOCHECK CONSTRAINT [CK_HCFICHA580_JSON];


GO
ALTER TABLE [dbo].[HCFICHA580] NOCHECK CONSTRAINT [FK_HCFICHA580_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor registrado del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de resultado del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el resultado del examen ((1- Positivo, 2- Negativo, 3- No procesado, 4- Inadecuado, 6. Valor registrado), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el agente (3. dengue, datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la prueba ((4. PCR,  E0 Elisa NS1, 2. IgM, 3. IgG, 5. Aislamiento viral), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada del examen ((4. tejido, 13. suero), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de recepción del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de toma del examen (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada del riñon (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'RINON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada de la medula (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MEDULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada del miocardio (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada cerebro (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CEREBRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada del pulmon (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'PULMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada del bazo (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'BRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada hígado (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIGADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la muestra tomada de tejidos (Checked or Unchecked, en caso de mortalidad por dengue)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'TEJIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la 7.2 conducta ((0. no aplica, 1. ambulatoria, 2. hospitalización piso, 3. unidades de cuidados intensivos, 4. observación, 5. remisión para hospitalización), CLASIFICACIÓN FINAL Y ATENCION DEL CASO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CONDUCTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la clasificación final ((0. no aplica, 1. dengue sin signos de alarma, 2. dengue con signos de alarma, 3. dengue grave), CLASIFICACIÓN FINAL Y ATENCION DEL CASO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el daño grave de órganos (dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el shock por dengue (Checked or Unchecked, dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la hemorragía con compromiso hemodinámico (Checked or Unchecked, dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la extravasación severa de plasma (Checked or Unchecked, dengue grave)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la acomulación de liquidos "Checked or Unchecked" (dengue con signo de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la caida de plaquetas (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el aumento de hematocrito  (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la hipotermia (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guara las hemorragias importantes en mucosas (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda la hepatomegalia (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda la hipotensión (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la somnolencia o irritabilidad (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la diarrea (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el vomito persistente (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el dolor abdominal intenso y continuo, (Checked or Unchecked, dengue con signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda erupción o rash (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda ARTRALGIAS (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la mialgias (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el dolor retroocular (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda cefalea (Checked or Unchecked, dengue sin signo de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'CAFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fiebre (Checked or Unchecked, dengue sin signos de alarma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre del establecimiento donde estudia o trabaja  (Checked or Uncheked, datos especificos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Algún familiar o conviviente ha tenido sintomatología de dengue en los últimos 15 días? (1. si, 2. no, 3. desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la ubicación "código" al que se desplazo (País, Departamento o municipio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el desplazamiento de los ultimos 15 días (1. si, 2. no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA580', @level2type = N'COLUMN', @level2name = N'ID';

