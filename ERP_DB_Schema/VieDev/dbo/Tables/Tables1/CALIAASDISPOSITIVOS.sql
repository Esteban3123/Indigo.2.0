CREATE TABLE [dbo].[CALIAASDISPOSITIVOS] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]        INT           NOT NULL,
    [TIPOUCI]             INT           NOT NULL,
    [FECHAINGRESOUCI]     DATE          NOT NULL,
    [REINGRESO]           BIT           NOT NULL,
    [PACIENTEREMITIDO]    BIT           NOT NULL,
    [CASOIADEXTRAHOSPITA] BIT           NOT NULL,
    [NOMINSTITUCIONIAD]   VARCHAR (500) NULL,
    [PESONACER]           INT           NULL,
    [TIPOIAD]             INT           NOT NULL,
    [CRITERIONAV]         INT           NULL,
    [CRITERIOITS]         INT           NULL,
    [CRITERIOITSUAC]      INT           NULL,
    [FECHADIAGNOSTI]      DATE          NOT NULL,
    [IADPOLIMICROBIANA]   BIT           NOT NULL,
    [VENTILADORMECANICO]  BIT           NULL,
    [CATETERCENTRAL]      BIT           NULL,
    [CATETERURINARIO]     BIT           NULL,
    [FECHAINSER1]         DATE          NULL,
    [FECHAINSER2]         DATE          NULL,
    [FECHAINSER3]         DATE          NULL,
    [FECHARETIRO1]        DATE          NULL,
    [FECHARETIRO2]        DATE          NULL,
    [FECHARETIRO3]        DATE          NULL,
    [CANCER]              BIT           NULL,
    [CORTICOTERAPIA]      BIT           NULL,
    [DESNUTRICION]        BIT           NULL,
    [DIABETES]            BIT           NULL,
    [DIALISIS]            BIT           NULL,
    [EDADEXTREMA]         BIT           NULL,
    [ENFERMEDADRENAL]     BIT           NULL,
    [EPOC]                BIT           NULL,
    [INMUNOSUPRESION]     BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [VIH]                 BIT           NULL,
    [INFEPREVIA]          BIT           NULL,
    [QUIMIOTERAPIA]       BIT           NULL,
    [TRAUMATISMO]         BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [PREMATUREZ]          BIT           NULL,
    [NINGUNO]             BIT           NULL,
    [OTRO]                BIT           NULL,
    [CUALOTRO]            VARCHAR (500) NULL,
    [FECHATOMAMUESTRA1]   DATE          NULL,
    [FECHATOMAMUESTRA2]   DATE          NULL,
    [FECHATOMAMUESTRA3]   DATE          NULL,
    [CODMUESTRA1]         INT           NULL,
    [CODMUESTRA2]         INT           NULL,
    [CODMUESTRA3]         INT           NULL,
    [CODPRUEBA1]          VARCHAR (5)   NULL,
    [CODPRUEBA2]          VARCHAR (5)   NULL,
    [CODPRUEBA3]          VARCHAR (5)   NULL,
    [MICROORGANISMO1]     VARCHAR (15)  NULL,
    [MICROORGANISMO2]     VARCHAR (15)  NULL,
    [MICROORGANISMO3]     VARCHAR (15)  NULL,
    CONSTRAINT [PK_CALIAASDISPOSITIVOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALIAASDISPOSITIVOS_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID])
);


GO
ALTER TABLE [dbo].[CALIAASDISPOSITIVOS] NOCHECK CONSTRAINT [FK_CALIAASDISPOSITIVOS_CALREPORTE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Datos de laboratorio  Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'FECHATOMAMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PREMATUREZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TRAUMATISMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'QUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INFEPREVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'ENFERMEDADRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'EDADEXTREMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CORTICOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factores de riesgo endógenos  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cateter urinario   1 = Si0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERURINARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cateter central   1 = Si0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CATETERCENTRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ventilador mecánico  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'VENTILADORMECANICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IAD polimicrobiana  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'IADPOLIMICROBIANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio de clasificación para ITSUAC   1=Criterio 1a 2=Criterio 2a 3=Criterio 3   4 =Criterio 4 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITSUAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio de clasificación para ITSAC   1=Criterio 1 2=Criterio 23=Criterio 3 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIOITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Criterio de clasificación NAV  1=NEU 1  2=NEU 2  3=NEU 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CRITERIONAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de IAD   1=NAV  2=ISTU-AC  3=ITS-AC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para UCIN, peso al nacer  1== 750 gr  2=751- 1000gr  3=1001-1500 gr  4=1501-2500 gr  5==2501 gr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PESONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caso de IAD extrahospitalaria  1= Si 0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'CASOIADEXTRAHOSPITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paciente remitido  1= Si 0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'PACIENTEREMITIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reingreso  1= Si   0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'REINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de UCI relacionado con ocurrencia del evento  1 =UCI-A  2 =UCI-P  3 =UCI-N', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALIAASDISPOSITIVOS', @level2type = N'COLUMN', @level2name = N'TIPOUCI';

