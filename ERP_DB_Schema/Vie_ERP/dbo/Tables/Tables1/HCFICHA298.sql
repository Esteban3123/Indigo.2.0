CREATE TABLE [dbo].[HCFICHA298] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VACU1]               INT           NULL,
    [DOSIS1]              INT           NULL,
    [VIA1]                INT           NULL,
    [SITIO1]              INT           NULL,
    [FECHA1]              DATE          NULL,
    [LOTE1]               VARCHAR (50)  NULL,
    [VACU2]               INT           NULL,
    [DOSIS2]              INT           NULL,
    [VIA2]                INT           NULL,
    [SITIO2]              INT           NULL,
    [FECHA2]              DATE          NULL,
    [LOTE2]               VARCHAR (50)  NULL,
    [VACU3]               INT           NULL,
    [DOSIS3]              INT           NULL,
    [VIA3]                INT           NULL,
    [SITIO3]              INT           NULL,
    [FECHA3]              DATE          NULL,
    [LOTE3]               VARCHAR (50)  NULL,
    [VACU4]               INT           NULL,
    [DOSIS4]              INT           NULL,
    [VIA4]                INT           NULL,
    [SITIO4]              INT           NULL,
    [FECHA4]              DATE          NULL,
    [LOTE4]               NCHAR (10)    NULL,
    [ADENITIS]            BIT           NULL,
    [TIEMPOTRANS]         VARCHAR (50)  NULL,
    [UNIMEDI]             INT           NULL,
    [ANTEPAT]             INT           NULL,
    [CUAL1]               VARCHAR (50)  NULL,
    [ANTEALERG]           INT           NULL,
    [CUAL2]               VARCHAR (50)  NULL,
    [ANTEPREV]            INT           NULL,
    [CUAL3]               VARCHAR (50)  NULL,
    [ESTADFINAL]          INT           NULL,
    [CLASCASO]            INT           NULL,
    [ABSCESO]             BIT           NULL,
    [LINFADE]             BIT           NULL,
    [FIEBRE]              BIT           NULL,
    [CONVUFEBR]           BIT           NULL,
    [CONVUSINF]           BIT           NULL,
    [EPIHIPOTO]           BIT           NULL,
    [PARESTESIA]          BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [ENCEFALO]            BIT           NULL,
    [MENINGI]             BIT           NULL,
    [URTICARIA]           BIT           NULL,
    [ECZEMA]              BIT           NULL,
    [CHOQUE]              BIT           NULL,
    [GUILLAIN]            BIT           NULL,
    [CELULITIS]           BIT           NULL,
    [LLANTO]              BIT           NULL,
    [RUMOR]               BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA298] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA298_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA298_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA298] NOCHECK CONSTRAINT [CK_HCFICHA298_JSON];


GO
ALTER TABLE [dbo].[HCFICHA298] NOCHECK CONSTRAINT [FK_HCFICHA298_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON     Desde versión "V03_2021-05-23" -->  - FATIGA   - DOLORCABEZA   - MIALGIA   - ARTRALGIA   - NAUSEAS   - OTROS   - CUALOTROS   - FABRICANTE1,  FABRICANTE2,  FABRICANTE3,  FABRICANTE4 : 20 caracteres alfanumericos    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rumor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'RUMOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llanto Persistente Mayor a 3 Horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LLANTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Celulitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CELULITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guillain Barre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'GUILLAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Choque Anafilactico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CHOQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Eczema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ECZEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Urticaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'URTICARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meningitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'MENINGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Encefalopatía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ENCEFALO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'PARESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Episodio Hipotónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'EPIHIPOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Convulsión Sin Fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUSINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Convulsión Febril', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CONVUFEBR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre Mayor a 38.5°C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Linfadenitis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LINFADE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hallazgos Semiológicos   Absceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ABSCESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación Final del Caso  1=Caso Relacionado con la Vacuna  2=Caso Relacionado con el Programa  3=Caso Coincidente  4=Caso no Concluyente o Desconocido  5=Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CLASCASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Final del Paciente  1 = Recuperación sin Secuelas  2 = Recuperación con Secuelas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ESTADFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuáles 3 lo que escribio usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene antecedentes Previos de Reacción a Vacunas  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuáles 2 lo que escribio usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene antecedentes Alérgicos  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEALERG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuáles lo que escribio usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CUAL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene antecedentes Patológicos  True = Si      False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ANTEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida  1= meses  2=dias  3=horas  4=Minutos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'UNIMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Transcurrido entre la Aplicación y los Síntomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hallazgos Semiológicos   Adenitis post BCG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ADENITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de administracion(dd/mm/aaaa) 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio 4  1=HOMBRO DER.2=HOMBRO IZQ.3=BRAZO DER.4=BRAZO IZQ.5=GLÚTEO DER.6=GLÚTEO IZQ.7=MUSLO DER.8=MUSLO IZQ.9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vía  4  1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis 4  1=PRIMERA2=SEGUNDA3=TERCERA4=ADICIONAL RN5=ÚNICA6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna 4  1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de administracion(dd/mm/aaaa) 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio 3  1=HOMBRO DER.2=HOMBRO IZQ.3=BRAZO DER.4=BRAZO IZQ.5=GLÚTEO DER.6=GLÚTEO IZQ.7=MUSLO DER.8=MUSLO IZQ.9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vía  3  1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis 3  1=PRIMERA2=SEGUNDA3=TERCERA4=ADICIONAL RN5=ÚNICA6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna 3  1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de administracion(dd/mm/aaaa) 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio 2  1=HOMBRO DER.  2=HOMBRO IZQ.  3=BRAZO DER.  4=BRAZO IZQ.  5=GLÚTEO DER.  6=GLÚTEO IZQ.  7=MUSLO DER.  8=MUSLO IZQ.  9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vía  2  1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis 2  1=PRIMERA  2=SEGUNDA  3=TERCERA  4=ADICIONAL RN  5=ÚNICA  6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna 2  1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'LOTE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de administracion(dd/mm/aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'FECHA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sitio  1=HOMBRO DER.  2=HOMBRO IZQ.  3=BRAZO DER.  4=BRAZO IZQ.  5=GLÚTEO DER.  6=GLÚTEO IZQ.  7=MUSLO DER.  8=MUSLO IZQ.  9=ORAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'SITIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vía   1=ORAL  2=INTRADÉRMICA  3=SUBCUTÁNEA  4=INTRAMUSCULAR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VIA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis   1=PRIMERA  2=SEGUNDA  3=TERCERA  4=ADICIONAL RN  5=ÚNICA  6=REFUERZO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'DOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacuna   1=BCG  2=DPT  3=ANTIPOLIO ORAL  4=HB  5=HiB   6=PENTAVALENTE  7=TRIPLE VIRAL   8=F.A.  9=SR-  10=Td/TD  11=INFLUENZA  12=Tdap  13=ANTINEUMOCOCO  14=ANTIVARICELA  15=ANTIROTAVIRICA  16=OTRA  17=HEPATITIS A  18=Anti VPH  19=ANTIMENINGOCOCO  20=ANTIRRABICA  21=ANTIPOLIO INYECTABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'VACU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA298', @level2type = N'COLUMN', @level2name = N'ID';

