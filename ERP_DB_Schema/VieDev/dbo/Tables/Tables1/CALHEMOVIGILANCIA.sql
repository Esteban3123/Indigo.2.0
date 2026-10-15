CREATE TABLE [dbo].[CALHEMOVIGILANCIA] (
    [ID]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]              INT           NOT NULL,
    [EMPRESA]                   CHAR (3)      NULL,
    [REPORTEREALIZADOPOR]       VARCHAR (100) NULL,
    [CARGO]                     VARCHAR (100) NULL,
    [ANTETRANSFUSIONALES]       BIT           NULL,
    [FECHA]                     DATE          NULL,
    [DIAGNOSTICO]               CHAR (4)      NULL,
    [COMPONENTE]                VARCHAR (100) NULL,
    [ANTEREACCIONESADVERSAS]    BIT           NULL,
    [FECHAANTECEDENTE]          DATE          NULL,
    [TIPOREACCIONTRANSFUSIONAL] VARCHAR (200) NULL,
    [ANTEOBSTETRICO]            VARCHAR (200) NULL,
    [ANTEPATOLOGICOS]           VARCHAR (200) NULL,
    [COMPROMISOINMUNOLO]        VARCHAR (200) NULL,
    [DIAGNOSTICOPRINCIPAL]      CHAR (4)      NULL,
    [OTRODIAGNOSTICO]           CHAR (4)      NULL,
    [GRUPOSANGUINEO]            VARCHAR (50)  NULL,
    [MEDICACIONPREVIA]          VARCHAR (200) NULL,
    [MOTIVOREALIZATRANSFUSION]  VARCHAR (200) NULL,
    [MOMENTOPRESENTACION]       INT           NULL,
    [HORA]                      VARCHAR (5)   NULL,
    [DIAS]                      INT           NULL,
    [MESES]                     INT           NULL,
    [FECHATRANSFUSION]          DATE          NULL,
    [HORAINICIOTRANSFUSION]     VARCHAR (5)   NULL,
    [FECHAINICIOREACCION]       DATE          NULL,
    [HORAINICIOREACCION]        VARCHAR (5)   NULL,
    [PRETEMPERATURA]            INT           NULL,
    [POSTEMPERATURA]            INT           NULL,
    [PREPRESIONARTERIAL]        VARCHAR (10)  NULL,
    [POSPRESIONARTERIAL]        VARCHAR (10)  NULL,
    [PREFRECUENCIACARDIACA]     VARCHAR (10)  NULL,
    [POSFFRECUENCIACARDIACA]    VARCHAR (10)  NULL,
    [PREFRECUENCIARESPIRATORIA] VARCHAR (10)  NULL,
    [POSFRECUENCIARESPIRATORIA] VARCHAR (10)  NULL,
    [FIEBRE]                    BIT           NULL,
    [ESCALOFRIO]                BIT           NULL,
    [HIPOTENSION]               BIT           NULL,
    [HIPERTENSION]              BIT           NULL,
    [OLIGUARIA]                 BIT           NULL,
    [CONVULSIONES]              BIT           NULL,
    [HEMORRAGIA]                BIT           NULL,
    [URTICARIA]                 BIT           NULL,
    [NAUSEAS]                   BIT           NULL,
    [ICTERICIA]                 BIT           NULL,
    [TAQUICARDIA]               BIT           NULL,
    [SOMNOLENCIA]               BIT           NULL,
    [DOLORLUMBAR]               BIT           NULL,
    [DOLORTORACICO]             BIT           NULL,
    [DOLORINFUSION]             BIT           NULL,
    [CAFALEA]                   BIT           NULL,
    [PRURITO]                   BIT           NULL,
    [CONFUSION]                 BIT           NULL,
    [HIPOXEMIA]                 BIT           NULL,
    [PALIDEZ]                   BIT           NULL,
    [DISNEA]                    BIT           NULL,
    [TOS]                       BIT           NULL,
    [CIANOSIS]                  BIT           NULL,
    [ESTUPOR]                   BIT           NULL,
    [ARRITMIAS]                 BIT           NULL,
    [PARESTESIAS]               BIT           NULL,
    [TETANIA]                   BIT           NULL,
    [ERITRODERMIA]              BIT           NULL,
    [ORTOPNEA]                  BIT           NULL,
    [ANSIEDAD]                  BIT           NULL,
    [ERITEMA]                   BIT           NULL,
    [EDEMA]                     BIT           NULL,
    [CHOQUE]                    BIT           NULL,
    [DIARREA]                   BIT           NULL,
    [PETEQUIAS]                 BIT           NULL,
    [PURPURA]                   BIT           NULL,
    [SANGRECOMPLETA]            BIT           NULL,
    [ERITROCITOS]               BIT           NULL,
    [PLAQUETAS]                 BIT           NULL,
    [PLASMAFRESCO]              BIT           NULL,
    [PLASMACONGELADO]           BIT           NULL,
    [CRIOPRECIPITADO]           BIT           NULL,
    [HEMOCOMPONENTE1]           VARCHAR (50)  NULL,
    [HEMOCOMPONENTE2]           VARCHAR (50)  NULL,
    [MODIFICADO1]               VARCHAR (50)  NULL,
    [MODIFICADO2]               VARCHAR (50)  NULL,
    [BANCOSANGRE1]              VARCHAR (50)  NULL,
    [BANCOSANGRE2]              VARCHAR (50)  NULL,
    [GRUPORH1]                  VARCHAR (50)  NULL,
    [GRUPORH2]                  VARCHAR (50)  NULL,
    [IDENTIFICACIONUNIDAD1]     VARCHAR (50)  NULL,
    [IDENTIFICACIONUNIDAD2]     VARCHAR (50)  NULL,
    [FECHAVENCIMIENTO1]         VARCHAR (50)  NULL,
    [FECHAVENCIMIENTO2]         VARCHAR (50)  NULL,
    [MLADMINISTRADO1]           VARCHAR (50)  NULL,
    [MLADMINISTRADO2]           VARCHAR (50)  NULL,
    [DURACIONTRANSFU1]          VARCHAR (50)  NULL,
    [DURACIONTRANSFU2]          VARCHAR (50)  NULL,
    [INTERRUPCION]              BIT           NULL,
    [VASOPRESORES]              BIT           NULL,
    [ANTIHISTAMINICOS]          BIT           NULL,
    [SUPLENCIADEO2]             BIT           NULL,
    [BROCONDILATADORES]         BIT           NULL,
    [LIQUIDOENDOVENOSO]         BIT           NULL,
    [ANALGESICOS]               BIT           NULL,
    [ANTIPIRETICOS]             BIT           NULL,
    [DIURETICOS]                BIT           NULL,
    [GASESARTERIALES]           BIT           NULL,
    [ELECTROLITOS]              BIT           NULL,
    [ESTEROIDES]                BIT           NULL,
    [CUADROHEMATICO]            BIT           NULL,
    [ELECTROCARDIGRAMA]         BIT           NULL,
    [OTROS]                     VARCHAR (100) NULL,
    [PREHEMORECEPTOR]           VARCHAR (50)  NULL,
    [POSHEMORECEPTOR]           VARCHAR (50)  NULL,
    [IDENHEMORECPTOR]           VARCHAR (50)  NULL,
    [PREHEMOUNIDAD]             VARCHAR (50)  NULL,
    [POSHEMOUNIDAD]             VARCHAR (50)  NULL,
    [IDENHEMOUNIDAD]            VARCHAR (50)  NULL,
    [PREPRUEBAS]                VARCHAR (50)  NULL,
    [POSPRUEBAS]                VARCHAR (50)  NULL,
    [IDENPRUEBAS]               VARCHAR (50)  NULL,
    [COOMBS1]                   VARCHAR (50)  NULL,
    [COOMBS2]                   VARCHAR (50)  NULL,
    [IDENRASTREO1]              VARCHAR (50)  NULL,
    [ENZIMA1]                   VARCHAR (50)  NULL,
    [ENZIMA2]                   VARCHAR (50)  NULL,
    [IDENRASTREO2]              VARCHAR (50)  NULL,
    [PREBUN]                    VARCHAR (50)  NULL,
    [POSBUN]                    VARCHAR (50)  NULL,
    [PRECREATININA]             VARCHAR (50)  NULL,
    [POSCREATININA]             VARCHAR (50)  NULL,
    [PREBILIRRUBINA]            VARCHAR (50)  NULL,
    [POSBILIRRUBINA]            VARCHAR (50)  NULL,
    [PREHEMOGLO]                VARCHAR (50)  NULL,
    [POSHEMOGLO]                VARCHAR (50)  NULL,
    [PREPRUEBASHEMOLISIS]       VARCHAR (50)  NULL,
    [HBLIBRE]                   VARCHAR (50)  NULL,
    [HEMOLISIS]                 VARCHAR (50)  NULL,
    [PRESENSIBILIZACION]        VARCHAR (50)  NULL,
    [POSSENSIBILIZACION]        VARCHAR (50)  NULL,
    [PRERESULTADO]              VARCHAR (50)  NULL,
    [POSRESULTADO]              VARCHAR (50)  NULL,
    [TINCIONGRAM]               VARCHAR (50)  NULL,
    [SEVERIDADREACCION]         INT           NULL,
    [MEDICORESPONSABLE]         CHAR (20)     NULL,
    [IMPUTABILIDAD]             INT           NULL,
    [CHEKHEMOLISISNOINMUNE]     BIT           NULL,
    [CHEKHIPOTENSION]           BIT           NULL,
    [CHEKREACCIONHEMOLITICAS]   BIT           NULL,
    [CHEKREACCIONALERGICA]      BIT           NULL,
    [CHEKTRALI]                 BIT           NULL,
    [CHEKHIPERTENSION]          BIT           NULL,
    [CHEKSOBRECARGA]            BIT           NULL,
    [CHEKHIPOTERMIA]            BIT           NULL,
    [CHEKTOXICIDAD]             BIT           NULL,
    [CHEKTRASTORNOS]            BIT           NULL,
    [CHEKREACCIONFEBRIL]        BIT           NULL,
    [CHEKREACCIONHEMOLITICA]    BIT           NULL,
    [CHEKPURPURA]               BIT           NULL,
    [CHEKENFERMEDADINJERTO]     BIT           NULL,
    [CHEKINMUNOMODULACION]      BIT           NULL,
    [CHEKSOBRECARGAHIERRO]      BIT           NULL,
    [CHEKINFECIONVIRAL]         BIT           NULL,
    [NOMBREINFECIONVIRAL]       VARCHAR (200) NULL,
    [CHEKINFECCIONBACTERIANA]   BIT           NULL,
    [NOMBREINFECCIONBACTERIANA] VARCHAR (200) NULL,
    [CHEKOTRASINFECCIONES]      BIT           NULL,
    [NOMBREOTRASINFECCIONES]    VARCHAR (200) NULL,
    [ESTATUSINVESTIGACION]      INT           NULL,
    [LOCALIZACIONRAT]           INT           NULL,
    [PLANMEJORAMIENTO]          VARCHAR (MAX) NULL,
    CONSTRAINT [PK_CALHEMOVIGILANCIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALHEMOVIGILANCIA_CALHEMOVIGILANCIA] FOREIGN KEY ([ID]) REFERENCES [dbo].[CALHEMOVIGILANCIA] ([ID]),
    CONSTRAINT [FK_CALHEMOVIGILANCIA_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALHEMOVIGILANCIA_INDIAGNOS] FOREIGN KEY ([DIAGNOSTICO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_CALHEMOVIGILANCIA_INDIAGNOS1] FOREIGN KEY ([OTRODIAGNOSTICO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_CALHEMOVIGILANCIA_INDIAGNOS2] FOREIGN KEY ([DIAGNOSTICOPRINCIPAL]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_CALHEMOVIGILANCIA_INEMPRESU] FOREIGN KEY ([EMPRESA]) REFERENCES [dbo].[INEMPRESU] ([INDCODEMP])
);


GO
ALTER TABLE [dbo].[CALHEMOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALHEMOVIGILANCIA_CALHEMOVIGILANCIA];


GO
ALTER TABLE [dbo].[CALHEMOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALHEMOVIGILANCIA_CALREPORTE];


GO
ALTER TABLE [dbo].[CALHEMOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALHEMOVIGILANCIA_INDIAGNOS];


GO
ALTER TABLE [dbo].[CALHEMOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALHEMOVIGILANCIA_INDIAGNOS1];


GO
ALTER TABLE [dbo].[CALHEMOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALHEMOVIGILANCIA_INDIAGNOS2];


GO
ALTER TABLE [dbo].[CALHEMOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALHEMOVIGILANCIA_INEMPRESU];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización de la posible causa de la RAT dentro de la cadena transfusional  1=Selección del donante  2=Recolección de la unidad  3=Identificación de la unidad y el receptor  4=Procesamiento de los hemocomponentes  5=Almacenamiento de los hemocomponentes  6=Distribución y transporte de los hemocomponentes  7=Transfusión del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOCALIZACIONRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estatus de la investigación  1 = EN PROGRESO (la información está siendo recolectada y analizada por el equipo de trabajo)  2 = CONCLUIDA (el caso se ha cerrado tras realizar la investigación y llegar a las conclusiones respectivas)  3 = NO PUDO SER REALIZADA (anotar los motivos por los cuales aún no se ha concluido la investigación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ESTATUSINVESTIGACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'III. INFECCIONES TRANSMITIDAS POR LA TRANSFUSIÓN  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKOTRASINFECCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'III. INFECCIONES TRANSMITIDAS POR LA TRANSFUSIÓN  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKINFECCIONBACTERIANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'III. INFECCIONES TRANSMITIDAS POR LA TRANSFUSIÓN  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKINFECIONVIRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'II. REACCIONES TRANSFUSIONALES TARDÍAS NO INFECCIOSAS  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKSOBRECARGAHIERRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'II. REACCIONES TRANSFUSIONALES TARDÍAS NO INFECCIOSAS  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKINMUNOMODULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'II. REACCIONES TRANSFUSIONALES TARDÍAS NO INFECCIOSAS  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKENFERMEDADINJERTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'II. REACCIONES TRANSFUSIONALES TARDÍAS NO INFECCIOSAS  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKPURPURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'II. REACCIONES TRANSFUSIONALES TARDÍAS NO INFECCIOSAS  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKREACCIONHEMOLITICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKREACCIONFEBRIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKTRASTORNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKTOXICIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKHIPOTERMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKSOBRECARGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKHIPERTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKTRALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKREACCIONALERGICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKREACCIONHEMOLITICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  1 = True   0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKHIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultados de la investigación y conclusiones (Definición de caso)  I. REACCIONES TRANSFUSIONALES AGUDAS NO INFECCIOSAS  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKHEMOLISISNOINMUNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imputabilidad  1 =GRADO 0: EXCLUIDA (la evidencia permite descartar a la transfusión como causa de la reacción adversa)  2 =GRADO 1: POSIBLE (la evidencia no permite establecer a la transfusión como causa de la reacción adversa; podría explicarse por causas diferentes)  3 =GRADO 2: PROBABLE (la evidencia claramente está a favor de atribuir la causa de la reacción adversa con la administración del producto sanguíneo)  4 =GRADO 3: DEFINITIVA (la evidencia es concluyente para atribuir a la transfusión como causa de la reacción adversa)  5 =NO EVALUABLE (no existen datos suficientes para determinar que la reacción adversa; está relacionada con la administración del producto sanguíneo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IMPUTABILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Severidad de la reacción   1 = LEVE (morbilidad limitada a corto o largo plazo)  2 = MODERADA (morbilidad a largo plazo)  3 = SEVERA (morbilidad inmediata que arriesga vida del paciente)  4 = MUERTE  5 = NO DETERMINADA (especificar causas por las que no se determinó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SEVERIDADREACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos de Laboratorio  PRE TRANSFUSIÓN Hemoclasificación del receptor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREHEMORECEPTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ELECTROCARDIGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CUADROHEMATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ESTEROIDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ELECTROLITOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'GASESARTERIALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIURETICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTIPIRETICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANALGESICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LIQUIDOENDOVENOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'BROCONDILATADORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SUPLENCIADEO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTIHISTAMINICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'VASOPRESORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manejo médico de la RAT  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'INTERRUPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CRIOPRECIPITADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PLASMACONGELADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PLASMAFRESCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PLAQUETAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ERITROCITOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemocomponente (s) Transfundido (s)  1 = True = Chequeado0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SANGRECOMPLETA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PURPURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PETEQUIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHOQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EDEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ERITEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANSIEDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORTOPNEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ERITRODERMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TETANIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ARRITMIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ESTUPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CIANOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISNEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PALIDEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HIPOXEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CONFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRURITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DOLORINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DOLORTORACICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DOLORLUMBAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SOMNOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TAQUICARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ICTERICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NAUSEAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'URTICARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HEMORRAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OLIGUARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HIPERTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ESCALOFRIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado General  1 = True = Chequeado  0 = False = sin Chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos y síntomas clínicos  PRETRANSFUSIÓN Temperatura (°C)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRETEMPERATURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la transfusión  Fecha seleccionada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHATRANSFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion de la RAT  Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MESES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion de la RAT  Diá', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion de la RAT  Hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion de la RAT  Momento de presentación de la reacción    1= Durante la transfusion  2=Postransfusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MOMENTOPRESENTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes de reacciones adversas transfusionales  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTEREACCIONESADVERSAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos de la historia clinica Antecedentes Transfusionales  1 = True = Si  2= False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTETRANSFUSIONALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo del Usuario que esta conectado   ejemplo: INg de sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de la empresa que esta conectado  ejemplo : ADMINISTRADOR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REPORTEREALIZADOPOR';

