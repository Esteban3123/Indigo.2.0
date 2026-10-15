CREATE TABLE [dbo].[HCFICHA210] (
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
    CONSTRAINT [PK_HCFICHA210] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA210_JSON] CHECK (isjson([JSON])=(1))
);


GO
ALTER TABLE [dbo].[HCFICHA210] NOCHECK CONSTRAINT [CK_HCFICHA210_JSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VALORREGISTRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 = positivo  2= negativo  3=No procesado  4= inadecuado  5=Valor registrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '3-Dengue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '4-PCR  E0-Elisa NS1    2-IgM    3-IgG    5-Aislamiento viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra  4-Tejido     13-Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHARECEPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de toma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  RINON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'RINON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  MEDULA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MEDULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  MIOCARDIO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIOCARDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  CEREBRO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CEREBRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  PULMON= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'PULMON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  BRAZO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'BRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  HIGADO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIGADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestras  si seleciona  TEJIDOS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'TEJIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Conducta   0=No aplica   1=Ambulatoria   2=Hospitalización piso   4=Observación   5=Remisión para hospitalización   6=Unidad de cuidados intensivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CONDUCTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación final  0=No aplica  1=Dengue sin signos de alarma  2=Dengue con signos de alarma  3=Dengue grave', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CLASIFICACIONFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue grave 3  si seleciona  DAÑOORGANOS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DAÑOORGANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue grave 3  si seleciona  SHOCKDENGUE= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SHOCKDENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue grave 3  si seleciona  HEMOCOMPROMISO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMOCOMPROMISO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue grave 3  si seleciona  EXTRAVASACION= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'EXTRAVASACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  ACOMULACIONLIQUID= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ACOMULACIONLIQUID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  CAIDAPLAQUETAS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAIDAPLAQUETAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  AUMENTOHEMAT= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'AUMENTOHEMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  HIPOTERMIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTERMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  HEMORRAGIAS= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  HEPATOMEGALIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  HIPOTENSION= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  SOMNOLENCIA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  DIARREA= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  VOMITO= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue con signos de alarma 2  si seleciona  DOLORABDOMINAL= True si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORABDOMINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue sin signos de alarma 1  si seleciona    ERUPCIONRASH= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ERUPCIONRASH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue sin signos de alarma 1  si seleciona   ARTRALGIAS= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue sin signos de alarma 1  si seleciona   MIALGIAS= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue sin signos de alarma 1  si seleciona   DOLORRECTROO= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DOLORRECTROO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue sin signos de alarma 1  si seleciona   CAFALEA= True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'CAFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dengue sin signos de alarma 1  si seleciona   Fiebre = True   si no   Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Nombre del establecimiento donde estudia o trabaja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ESTABLECIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Algún familiar o conviviente ha tenido sintomatología  de dengue en los últimos 15 días  1=si     2=no    3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'SINTODENGUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Municipio / departamento al que se desplazó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desplazamiento en los últimos 15 días  True = si     false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'DESPLAZAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA210', @level2type = N'COLUMN', @level2name = N'ID';

