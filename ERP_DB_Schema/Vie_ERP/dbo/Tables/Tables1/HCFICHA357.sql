CREATE TABLE [dbo].[HCFICHA357] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [TIPOUCI]             INT           NULL,
    [FECHAINGRES]         DATE          NULL,
    [REINGRESO]           BIT           NULL,
    [PACIREMITI]          BIT           NULL,
    [CASOIAD]             BIT           NULL,
    [NOMINSTIT]           VARCHAR (50)  NULL,
    [PARAUCIN]            VARCHAR (50)  NULL,
    [TIPOIAD]             INT           NULL,
    [CRITCLASIFNAV]       INT           NULL,
    [CRITCLASIFITSAC]     INT           NULL,
    [CRITCLASIFISTUAC]    INT           NULL,
    [FECHADIAGN]          DATE          NULL,
    [IADPOLIMICRO]        BIT           NULL,
    [VENTIMECANI]         BIT           NULL,
    [FECHAINSER1]         DATE          NULL,
    [FECHAINSER2]         DATE          NULL,
    [FECHAINSER3]         DATE          NULL,
    [FECHARETIRO1]        DATE          NULL,
    [FECHARETIRO2]        DATE          NULL,
    [FECHARETIRO3]        DATE          NULL,
    [CATETECENTR]         BIT           NULL,
    [CATETEURINA]         BIT           NULL,
    [CANCER]              BIT           NULL,
    [CORTICOTERA]         BIT           NULL,
    [DESNUTRICION]        BIT           NULL,
    [DIABETES]            BIT           NULL,
    [DIALISIS]            BIT           NULL,
    [ENFERENAL]           BIT           NULL,
    [EPOC]                BIT           NULL,
    [INMUNOSUPRE]         BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [VIHSIDA]             BIT           NULL,
    [INFECCIPREV]         BIT           NULL,
    [QUIMIOTERA]          BIT           NULL,
    [TRAUMATIS]           BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [NINGUNO]             BIT           NULL,
    [OTRO]                BIT           NULL,
    [OTRO2]               VARCHAR (50)  NULL,
    [FECHATOMAMU1]        DATE          NULL,
    [FECHATOMAMU2]        DATE          NULL,
    [FECHATOMAMU3]        DATE          NULL,
    [CODMUESTRA1]         INT           NULL,
    [CODMUESTRA2]         INT           NULL,
    [CODMUESTRA3]         INT           NULL,
    [CODPRUEBA1]          INT           NULL,
    [CODPRUEBA2]          INT           NULL,
    [CODPRUEBA3]          INT           NULL,
    [MICROORGA1]          VARCHAR (50)  NULL,
    [MICROORGA2]          VARCHAR (50)  NULL,
    [MICROORGA3]          VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA357] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA357_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA357_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA357] NOCHECK CONSTRAINT [CK_HCFICHA357_JSON];


GO
ALTER TABLE [dbo].[HCFICHA357] NOCHECK CONSTRAINT [FK_HCFICHA357_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON    Desde versión "V01_2020-03-06" -->  - DIPOSITIVO_UCI : bit (Dispositivo insertado en UCI : 0 - No , 1 - Si)  - EVENTO_48H: bit (El evento se desarrolló 48 horas después de retirado el catéter o ventilador : 0 - No , 1 - Si)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Microorganismo aislado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Microorganismo aislado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Microorganismo aislado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'MICROORGA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cód. de la prueba 3       1= IgG    2= Cultivo  3= Elisa   4= Hemocultivo  5= RT/PCR  6= Urocultivo  7=Cultivo de Liquido Pleural  8= Lavado broncoalveolar  9= Biopsia  10= Cultivo del parenquima pulmonar 11=Cultivo de secresiones respiratorias  12= Test de micro inmunofluorescencia para chalmydia  13=  Microinmunofluorescencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cód. de la prueba 2     1= IgG    2= Cultivo  3= Elisa   4= Hemocultivo  5= RT/PCR  6= Urocultivo  7=Cultivo de Liquido Pleural  8= Lavado broncoalveolar  9= Biopsia  10= Cultivo del parenquima pulmonar 11=Cultivo de secresiones respiratorias  12= Test de micro inmunofluorescencia para chalmydia  13=  Microinmunofluorescencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cód. de la prueba1      1= IgG    2= Cultivo  3= Elisa   4= Hemocultivo  5= RT/PCR  6= Urocultivo  7=Cultivo de Liquido Pleural  8= Lavado broncoalveolar  9= Biopsia  10= Cultivo del parenquima pulmonar 11=Cultivo de secresiones respiratorias  12= Test de micro inmunofluorescencia para chalmydia  13=  Microinmunofluorescencia
14=  Radioinmunoanalisis  15= Inmunofluorescencia (IFA) indirecta
16= Hisopado - Aspirado/ nasofaríngeo 17 = Lavado broncoalveolar (LBA) protegido  18 = Cepillado protegido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cod. de la muestra 3            1 = sangre total  2 = Orina   3 = tejido 4 = Espúto  5= Otros Líquidos estériles  6 = Moco  7 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cod. de la muestra2    1 = sangre total  2 = Orina   3 = tejido 4 = Espúto  5= Otros Líquidos estériles  6 = Moco  7 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Cod. de la muestra1      1 = sangre total  2 = Orina   3 = tejido 4 = Espúto  5= Otros Líquidos estériles  6 = Moco  7 = Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha de toma de la muestra 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha de toma de la muestra 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Fecha de toma de la muestra 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHATOMAMU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'texto otro 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(campo eliminado desde version V01_2020-03-06 ) Ninguno : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obesidad : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Traumatismo : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TRAUMATIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(campo eliminado desde version V01_2020-03-06 ) Quimioterapia : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'QUIMIOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Infección Previa : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INFECCIPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  VIH-SIDA : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VIHSIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(campo eliminado desde version V01_2020-03-06 ) Parálisis : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inmunosupresión : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'EPOC : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'EPOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad Renal : 0 - No , 1 - Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ENFERENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(campo eliminado desde version V01_2020-03-06 ) Diálisis : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diabetes : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desnutrición : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(campo eliminado desde version V01_2020-03-06 ) Corticoterapia : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CORTICOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cáncer : 0 - No , 1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cateter urinario  1 = true  0 = false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETEURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cateter central 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CATETECENTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha de retiro 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha de retiro 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha de retiro 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHARETIRO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha de insercion 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha de insercion 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha de insercion 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINSER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ventilador mecánico 1 = true     0= false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'VENTIMECANI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' IAD polimicrobiana (asociada a más de un microorganismo)  1 = true  2 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IADPOLIMICRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHADIAGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Criterio de clasificación para ISTU-AC   1 = Criterio 1a   2 = Criterio 2a   3 = Criterio 3     4 = Criterio 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFISTUAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de clasificación para ITS-AC  1 = Criterio 1  2 = Criterio 2    3 = Criterio 3 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFITSAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Criterio de clasificación NAV   1= NEU 1    2= NEU 2     3 = NEU 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CRITCLASIFNAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de IAD   1=NAV  2=ISTU-AC  3=ITS-AC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Para UCI-N, Peso al nacer (gramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PARAUCIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Nombre de la institución a la que se atribuye el caso de IAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'NOMINSTIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'caso IAD extrahospitalario 1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CASOIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'paciente remitdo 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'PACIREMITI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(campo eliminado desde version V01_2020-03-06)  Reingreso:   0 - No    1 - Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'REINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda fecha ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'FECHAINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de UCI relacionado con ocurrencia del evento  1 =UCI-A  2 =UCI-P  3 =UCI-N', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'TIPOUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA357', @level2type = N'COLUMN', @level2name = N'ID';

