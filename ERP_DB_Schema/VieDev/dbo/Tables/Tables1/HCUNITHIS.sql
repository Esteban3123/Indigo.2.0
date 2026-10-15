CREATE TABLE [dbo].[HCUNITHIS] (
    [CODCENATE]                        CHAR (10)    CONSTRAINT [DF_HCUNITHIS_CODCENATE] DEFAULT ((1)) NOT NULL,
    [UFUCODIGO]                        CHAR (10)    NOT NULL,
    [CODTIPHIS]                        CHAR (3)     NOT NULL,
    [CODBODEGA]                        VARCHAR (20) NOT NULL,
    [CODCENCOS]                        CHAR (14)    NOT NULL,
    [EXIHISING]                        CHAR (1)     NULL,
    [VISPROPLA]                        CHAR (1)     NULL,
    [VISSERPLA]                        CHAR (1)     NULL,
    [EXIINTRES]                        BIT          NULL,
    [EXDEVFENF]                        BIT          NULL,
    [EXHFENFEG]                        BIT          NULL,
    [EXHFENTRA]                        BIT          NULL,
    [EXEGRMEGE]                        BIT          NULL,
    [SOLMEDSIN]                        BIT          NULL,
    [REGINSSIN]                        BIT          NULL,
    [REGMEDSIN]                        BIT          NULL,
    [REGMEDSUS]                        BIT          NULL,
    [ESTSINRES]                        CHAR (1)     NULL,
    [PEREVOPAC]                        BIT          NULL,
    [SOLINSSIN]                        BIT          NULL,
    [PERDOSMAN]                        BIT          NULL,
    [PERFARINT]                        BIT          NULL,
    [PERFAREXT]                        BIT          NULL,
    [NUMHORFOR]                        INT          NULL,
    [PERFORADI]                        BIT          NULL,
    [INTMEDICA]                        BIT          NULL,
    [INTLABORA]                        BIT          NULL,
    [INTIMAGEN]                        BIT          NULL,
    [INTPATOLO]                        BIT          NULL,
    [INTPRNOQX]                        BIT          NULL,
    [INTPROCQX]                        BIT          NULL,
    [INTINTERC]                        BIT          NULL,
    [ABHCURGCE]                        BIT          NULL,
    [NUMHDESVE]                        CHAR (2)     NULL,
    [EVOULTFOL]                        BIT          NULL,
    [EVOHISING]                        BIT          NULL,
    [CONFOLESP]                        BIT          NULL,
    [NOTAEVOLU]                        BIT          NULL,
    [NOTEVOINT]                        BIT          NULL,
    [EVOHISINT]                        BIT          NULL,
    [INDAUDFOR]                        NUMERIC (18) NOT NULL,
    [VISADENFE]                        NCHAR (1)    NULL,
    [NOTADOWNT]                        BIT          NULL,
    [NOTARASSS]                        BIT          NULL,
    [PERMICTRL]                        BIT          NULL,
    [NOTANORTN]                        BIT          NULL,
    [VISESCNTN]                        BIT          NULL,
    [VISESCRAS]                        BIT          NULL,
    [VISESCDWN]                        BIT          NULL,
    [VISESCVAS]                        BIT          NULL,
    [NOTDIAOBL]                        TINYINT      NULL,
    [SOLMEINSU]                        CHAR (1)     NULL,
    [PERANUSER]                        CHAR (1)     NULL,
    [COPYPASTEMED]                     TINYINT      CONSTRAINT [DF_HCUNITHIS_COPYPASTEMED] DEFAULT ((1)) NOT NULL,
    [COPYPASTEENFER]                   TINYINT      CONSTRAINT [DF_HCUNITHIS_COPYPASTEENFER] DEFAULT ((1)) NOT NULL,
    [PERMODORME]                       TINYINT      NULL,
    [FACMECONINS]                      TINYINT      NULL,
    [HEMRES]                           TINYINT      CONSTRAINT [DF_HCUNITHIS_HEMRES] DEFAULT ((2)) NULL,
    [ALTAPUNTMINANES]                  BIT          NULL,
    [ESCALAALDRETEMOD]                 TINYINT      CONSTRAINT [DF_HCUNITHIS_ESCALAALDRETEMOD] DEFAULT ((2)) NOT NULL,
    [ESCALAALDRETE]                    TINYINT      CONSTRAINT [DF_HCUNITHIS_ESCALAALDRETE] DEFAULT ((2)) NOT NULL,
    [ESCALABROMAGE]                    TINYINT      CONSTRAINT [DF_HCUNITHIS_ESCALABROMAGE] DEFAULT ((2)) NOT NULL,
    [SOLICONFEHEMO]                    BIT          NULL,
    [PERMRECHEHEM]                     BIT          NULL,
    [RECMEDCHECK]                      BIT          NULL,
    [MAXCARACTERES]                    BIGINT       NULL,
    [NOTEVORN]                         BIT          NULL,
    [GENAINICIALURG]                   BIT          NULL,
    [PRIMERAVEZ]                       TINYINT      NULL,
    [VALIDAPREPARACIONMEZCLA]          BIT          NULL,
    [OXIGENOORDEN]                     BIT          NULL,
    [OXIGENOORDENSUS]                  BIT          NULL,
    [JUNTAMEDICA]                      BIT          NULL,
    [REGPCE]                           TINYINT      NULL,
    [REGPCEHF]                         TINYINT      NULL,
    [ACTPEHFCADA]                      INT          NULL,
    [HCFOLIOESPAMB]                    BIT          NULL,
    [DEFINIRBODEGAS]                   BIT          NULL,
    [MOSTRARINSUCERO]                  BIT          NULL,
    [SOLMEZLIQSINPRESCRIPCION]         BIT          NULL,
    [USUARIOCREACION]                  CHAR (20)    NULL,
    [FECHACREACION]                    DATETIME     NULL,
    [AllowDrugloans]                   BIT          NULL,
    [MinimumNumberHoursReplenishment]  INT          NULL,
    [AllowDiscountingNonStockMixtures] BIT          NULL,
    CONSTRAINT [PK_HCUNITHIS] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC, [CODTIPHIS] ASC),
    CONSTRAINT [FK_HCUNITHIS_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCUNITHIS_HCHISTORI] FOREIGN KEY ([CODTIPHIS]) REFERENCES [dbo].[HCHISTORI] ([CODTIPHIS]),
    CONSTRAINT [FK_HCUNITHIS_IHBODEGAS] FOREIGN KEY ([CODBODEGA]) REFERENCES [dbo].[IHBODEGAS] ([CODBODEGA]),
    CONSTRAINT [FK_HCUNITHIS_INCENTCOS] FOREIGN KEY ([CODCENCOS]) REFERENCES [dbo].[INCENTCOS] ([CODCENCOS]),
    CONSTRAINT [FK_HCUNITHIS_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCUNITHIS_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCUNITHIS] NOCHECK CONSTRAINT [FK_HCUNITHIS_ADcenaten];


GO
ALTER TABLE [dbo].[HCUNITHIS] NOCHECK CONSTRAINT [FK_HCUNITHIS_HCHISTORI];


GO
ALTER TABLE [dbo].[HCUNITHIS] NOCHECK CONSTRAINT [FK_HCUNITHIS_IHBODEGAS];


GO
ALTER TABLE [dbo].[HCUNITHIS] NOCHECK CONSTRAINT [FK_HCUNITHIS_INCENTCOS];


GO
ALTER TABLE [dbo].[HCUNITHIS] NOCHECK CONSTRAINT [FK_HCUNITHIS_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCUNITHIS] NOCHECK CONSTRAINT [FK_HCUNITHIS_SEGusuaru_1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir descontar mezclas y liquidos sin existencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'AllowDiscountingNonStockMixtures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de horas mínimas para la reposición de medicamentos en "Solicitud medicamentos e insumos" - Este parámetro determina el margen de horas para la sumatorio de productos entregados por farmacia en la columna de "Productos entregados"    importante para validar que la enfermeras no reponga cantidades superiores a la prescripción médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'MinimumNumberHoursReplenishment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para indicar si esta activo el prestamos de medicamentos por unidad funcional.    Nota: Este parametro lo lee el formulario de aplicacion de medicamentos.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'AllowDrugloans';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida preparación mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VALIDAPREPARACIONMEZCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PRIMERAVEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 - No genera Atencion Inicial de urgencia  1  - Genera Atencion Inicial de Urgencia    este parametro aplica solo para las unidades:      ''1:      Urgencias          ''2:      Hospitalizacion          ''5:     Unidades de Cuidado Intensivo Adulto          ''6:     Unidades de Cuidado Intermedio Adulto          ''7:     Unidades de Cuidado Intensivo Pediatrica          ''8:     Unidades de Cuidado Intermedio Pediatrica          ''9:     Unidades de Cuidado Intensivo Neonatal          ''10:     Unidades de Cuidado Intermedio Neonatal          ''11:    Unidades de Cuidado Basico Neonatal          ''19:     Cirugia          ''23:     Gineco -Obstetricia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'GENAINICIALURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No permitir dar alta sin puntuación mínima en alta de anestesia  0->No Marcado  1->Marcado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ALTAPUNTMINANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si tiene permiso para dar egreso a un paciente con hemocomponentes,   1-> No Permitir Alta Medica  2-> Descartar Reserva por Egreso de Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'HEMRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si tiene permiso para copiar y pegar en los richtextedit de     Dashboard Enfermeria, 1-> permite copiar y pegar 2-> No permite copiar y pegar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'COPYPASTEENFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si tiene permiso para copiar y pegar en los richtextedit de     Dashboard Medicos, 1-> permite copiar y pegar 2-> No permite copiar y pegar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'COPYPASTEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Anular Servicios  1-Permitir Anular Servicios por la  misma Especialidad Solicitante  2-Permitir Anular solo por el Medico Solicitante  3-Permitir a Cualquiera  4-Ninguno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'PERANUSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitud de medicamentos e insumos  1-Cantidades Exactas en Ordenes Medicas  2-Advertir si no Coinciden con las Ordenes Medicas  3-Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'SOLMEINSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diligencia formatos de notificacion obligatoria diagnosticos  0: Exigir Siempre  1: Alertar para el Diligenciamiento  2: Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'NOTDIAOBL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. Exigir visado historia final enfermeria  2. Advertir la ausencia de visados en historia clinica final de enefermeria  3. Ninguno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISADENFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estudios Sin Resultado:  1. No Permitir Alta Medica del Paciente  2. Anular Estudio Automaticamente a la Salida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'ESTSINRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar Servicios IPS  1: Todos  2: Todos con Alerta cuando no esten cubiertos por el Plan  3: Solo los cubiertos en el Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISSERPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar Productos de Inventarios  1: Todos  2: Todos con Alerta cuando no esten cubiertos por el Plan  3: Solo los cubiertos en el Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'VISPROPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir Historia de Ingreso por Unidad Funcional  1: Siempre exige Historia de Ingreso cuando cambia de Unidad Funcional    2: Permite de forma opcional Crear Historia de Ingreso por Unidad Funcional    3: No permite crear mas de una Hisoria de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNITHIS', @level2type = N'COLUMN', @level2name = N'EXIHISING';

