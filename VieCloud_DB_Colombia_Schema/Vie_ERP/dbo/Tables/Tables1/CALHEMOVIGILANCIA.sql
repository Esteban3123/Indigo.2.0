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



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plan de mejoramiento en la cadena transfusional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PLANMEJORAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Localización de la posible causa de la RAT dentro de la cadena transfusional  1=Selección del donante  2=Recolección de la unidad  3=Identificación de la unidad y el receptor  4=Procesamiento de los hemocomponentes  5=Almacenamiento de los hemocomponentes  6=Distribución y transporte de los hemocomponentes  7=Transfusión del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOCALIZACIONRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estatus de la investigación  1 = EN PROGRESO (la información está siendo recolectada y analizada por el equipo de trabajo)  2 = CONCLUIDA (el caso se ha cerrado tras realizar la investigación y llegar a las conclusiones respectivas)  3 = NO PUDO SER REALIZADA (anotar los motivos por los cuales aún no se ha concluido la investigación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ESTATUSINVESTIGACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Otras Infeccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREOTRASINFECCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'III. INFECCIONES TRANSMITIDAS POR LA TRANSFUSIÓN  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKOTRASINFECCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Infeccion Bacteriana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINFECCIONBACTERIANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'III. INFECCIONES TRANSMITIDAS POR LA TRANSFUSIÓN  1 = True 0 = False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CHEKINFECCIONBACTERIANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Infeccion Viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINFECIONVIRAL';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NOMBRE MÉDICO RESPONSABLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MEDICORESPONSABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Severidad de la reacción   1 = LEVE (morbilidad limitada a corto o largo plazo)  2 = MODERADA (morbilidad a largo plazo)  3 = SEVERA (morbilidad inmediata que arriesga vida del paciente)  4 = MUERTE  5 = NO DETERMINADA (especificar causas por las que no se determinó)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SEVERIDADREACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tinción de Gram', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TINCIONGRAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Resultados del cultivo microbiológico de la unidad No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSRESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' PRE TRANSFUSIÓN Resultados del cultivo microbiológico de la unidad No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRERESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Sensibilización de eritrocitos-Coombs directo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSSENSIBILIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' PRE TRANSFUSIÓN Sensibilización de eritrocitos-Coombs directo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRESENSIBILIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemólisis %', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HEMOLISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hb libre g/dL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HBLIBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' PRE TRANSFUSIÓN Pruebas de hemólisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREPRUEBASHEMOLISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Hemoglobinuria del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' PRE TRANSFUSIÓN Hemoglobinuria del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' POS TRANSFUSIÓN Bilirrubinas del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSBILIRRUBINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' PRE TRANSFUSIÓN Bilirrubinas del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREBILIRRUBINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' POS TRANSFUSIÓN Creatinina del paciente MG/DL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' PRE TRANSFUSIÓN Creatinina del paciente MG/DL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRECREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN BUN del paciente MG/DL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSBUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' PRE TRANSFUSIÓN BUN del paciente MG/DL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREBUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IDENTIFICACION DE LA UNIDAD Enzima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENRASTREO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Rastreo Anticuerpos irregulares Enzima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENZIMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRE TRANSFUSIÓN Rastreo Anticuerpos irregulares Enzima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENZIMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IDENTIFICACION DE LA UNIDAD coombs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENRASTREO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Rastreo Anticuerpos irregulares coombs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'COOMBS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRE TRANSFUSIÓN Rastreo Anticuerpos irregulares coombs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'COOMBS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IDENTIFICACION DE LA UNIDAD Pruebas cruzadas mayores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENPRUEBAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Pruebas cruzadas mayores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSPRUEBAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRE TRANSFUSIÓN Pruebas cruzadas mayores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREPRUEBAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IDENTIFICACION DE LA UNIDAD Hemoclasificación de la unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENHEMOUNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Hemoclasificación de la unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSHEMOUNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRE TRANSFUSIÓN Hemoclasificación de la unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREHEMOUNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IDENTIFICACION DE LA UNIDAD Hemoclasificación del receptor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENHEMORECPTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS TRANSFUSIÓN Hemoclasificación del receptor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSHEMORECEPTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos de Laboratorio  PRE TRANSFUSIÓN Hemoclasificación del receptor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREHEMORECEPTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros escrito por el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTROS';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DURACIONTRANSFU2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DURACIONTRANSFU1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ml administrados hasta inicio RAT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MLADMINISTRADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ml administrados hasta inicio RAT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MLADMINISTRADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha vencimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha vencimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación de la unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENTIFICACIONUNIDAD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación de la unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDENTIFICACIONUNIDAD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo Sanguíneo factor Rh', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'GRUPORH2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo Sanguíneo factor Rh', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'GRUPORH1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Banco Sangre distribuidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'BANCOSANGRE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Banco Sangre distribuidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'BANCOSANGRE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MODIFICADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MODIFICADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HEMOCOMPONENTE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HEMOCOMPONENTE1';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POSTRANSFUSIÓN Frecuencia respiratoria (respiraciones/minuto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSFRECUENCIARESPIRATORIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRETRANSFUSIÓN Frecuencia respiratoria (respiraciones/minuto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREFRECUENCIARESPIRATORIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POSTRANSFUSIÓN Frecuencia cardiaca (latidos/minuto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSFFRECUENCIACARDIACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRETRANSFUSIÓN Frecuencia cardiaca (latidos/minuto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREFRECUENCIACARDIACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POSTRANSFUSIÓN Presión arterial (mm/Hg):', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSPRESIONARTERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRETRANSFUSIÓN Presión arterial (mm/Hg):', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PREPRESIONARTERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POSTRANSFUSIÓN Temperatura (°C)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'POSTEMPERATURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos y síntomas clínicos  PRETRANSFUSIÓN Temperatura (°C)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PRETEMPERATURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de inicio reacción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HORAINICIOREACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicio de la reacción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAINICIOREACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de inicio transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'HORAINICIOTRANSFUSION';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo por el cual se realiza la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MOTIVOREALIZATRANSFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicación previa a la transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MEDICACIONPREVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo sanguíneo (ABO, RH)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'GRUPOSANGUINEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otro diagnostico seleccionado que viene desde la Tabla INDIAGNOS donde ESTADO=1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRODIAGNOSTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'diagnostico Principal  seleccionado que viene desde la Tabla INDIAGNOS donde ESTADO=1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICOPRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Compromiso inmunológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'COMPROMISOINMUNOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes patologicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTEPATOLOGICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes obstétricos (GPA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTEOBSTETRICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de reacción transfusional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TIPOREACCIONTRANSFUSIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes de reacciones adversas transfusionales  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTEREACCIONESADVERSAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Componentes Trasfuncidos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'COMPONENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'diagnostico seleccionado que viene desde la Tabla INDIAGNOS donde ESTADO=1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha actual del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos de la historia clinica Antecedentes Transfusionales  1 = True = Si  2= False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ANTETRANSFUSIONALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo del Usuario que esta conectado   ejemplo: INg de sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de la empresa que esta conectado  ejemplo : ADMINISTRADOR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REPORTEREALIZADOPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Empresa a la cual esta conectado el usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EMPRESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de hemovigilancia: documenta los reportes de reacciones adversas transfusionales en pacientes, incluyendo antecedentes clínicos, signos vitales antes y después de la transfusión, síntomas presentados, hemocomponentes administrados, tratamiento aplicado, resultados de laboratorio y clasificación de la reacción según severidad e imputabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALHEMOVIGILANCIA';
