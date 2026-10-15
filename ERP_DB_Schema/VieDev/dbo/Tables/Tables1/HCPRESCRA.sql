CREATE TABLE [dbo].[HCPRESCRA] (
    [IDETIPHIS]                                  CHAR (9)        NOT NULL,
    [NUMEFOLIO]                                  CHAR (10)       NOT NULL,
    [IPCODPACI]                                  VARCHAR (25)    NOT NULL,
    [NUMINGRES]                                  CHAR (10)       NOT NULL,
    [CODCENATE]                                  CHAR (10)       NOT NULL,
    [UFUCODIGO]                                  CHAR (10)       NOT NULL,
    [CODPROSAL]                                  CHAR (20)       NOT NULL,
    [CODPRODUC]                                  CHAR (20)       NOT NULL,
    [CODVIAADM]                                  VARCHAR (20)    NOT NULL,
    [CODFORMED]                                  VARCHAR (20)    NOT NULL,
    [DOSISPROD]                                  NUMERIC (18, 2) NULL,
    [CODUNIMED]                                  VARCHAR (20)    NULL,
    [FRECUENCI]                                  INT             NULL,
    [UNIFRECUE]                                  CHAR (1)        NULL,
    [FECINIDOS]                                  DATETIME        NOT NULL,
    [TIPFORMED]                                  CHAR (1)        NOT NULL,
    [DURACIDOS]                                  CHAR (20)       NOT NULL,
    [VALDURFIJ]                                  INT             NULL,
    [UNIDURFIJ]                                  CHAR (1)        NULL,
    [FECFINDOS]                                  DATETIME        NULL,
    [PREFECANT]                                  BIT             NOT NULL,
    [MOTPREANT]                                  CHAR (250)      NULL,
    [PREESTADO]                                  INT             NOT NULL,
    [CODCONCEC]                                  NUMERIC (18)    NOT NULL,
    [CODDIAGNO]                                  CHAR (4)        NOT NULL,
    [INDAPLMED]                                  VARCHAR (MAX)   NULL,
    [MOTSUSMED]                                  VARCHAR (2000)  NULL,
    [NIVRIEPAC]                                  CHAR (1)        NULL,
    [OBSNIVRIE]                                  VARCHAR (1000)  NULL,
    [MANEXTPRO]                                  BIT             NOT NULL,
    [CANPEDPRO]                                  INT             NOT NULL,
    [NUMFOLSUS]                                  CHAR (10)       NULL,
    [TOTPROUNI]                                  NUMERIC (18, 2) NULL,
    [MEDPENAGE]                                  BIT             NULL,
    [FORMUMANU]                                  BIT             NULL,
    [DESADMINI]                                  VARCHAR (MAX)   NULL,
    [INDAUDFOR]                                  NUMERIC (18)    NOT NULL,
    [DOSISPRFN]                                  NUMERIC (18, 2) NULL,
    [CODUNIMFN]                                  CHAR (3)        NULL,
    [OBSJUSMEE]                                  VARCHAR (500)   NULL,
    [CODJUMEES]                                  CHAR (3)        NULL,
    [IDESQUEMAONC]                               INT             NULL,
    [MEDICACUSTODIA]                             BIT             NULL,
    [FORMAPRESCRIBE]                             TINYINT         NULL,
    [NUMERODOSIS]                                TINYINT         NULL,
    [DOSISPROD1]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD2]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD3]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD4]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD5]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD6]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD7]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD8]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD9]                                 NUMERIC (18, 2) NULL,
    [DOSISPROD10]                                NUMERIC (18, 2) NULL,
    [HORA1]                                      DATETIME        NULL,
    [HORA2]                                      DATETIME        NULL,
    [HORA3]                                      DATETIME        NULL,
    [HORA4]                                      DATETIME        NULL,
    [HORA5]                                      DATETIME        NULL,
    [HORA6]                                      DATETIME        NULL,
    [HORA7]                                      DATETIME        NULL,
    [HORA8]                                      DATETIME        NULL,
    [HORA9]                                      DATETIME        NULL,
    [HORA10]                                     DATETIME        NULL,
    [JUSTIFICARB]                                VARCHAR (500)   NULL,
    [TraceabilityPaperworkEventsId]              INT             NULL,
    [TraceabilityPaperworkId]                    INT             NULL,
    [JUSTIFICACIONPBS]                           VARCHAR (2000)  NULL,
    [IDHCORDQUIMIO]                              INT             NULL,
    [ID]                                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ReasonDiscontinuationOfDrug]                INT             NULL,
    [FormulaSairy]                               BIT             NULL,
    [StatusFormulaSairy]                         INT             NULL,
    [FeedingsPerDay]                             INT             NULL,
    [OuncesPerDose]                              DECIMAL (4, 1)  NULL,
    [ConcentrationPerOunce]                      DECIMAL (4, 1)  NULL,
    [DairyComponentCancellationCode]             CHAR (4)        NULL,
    [DairyComponentCancellationJustification]    VARCHAR (500)   NULL,
    [DairyComponentProfessionalCancellationCode] CHAR (20)       NULL,
    [DairyComponentCancellationDate]             DATETIME        NULL,
    CONSTRAINT [PK_HCPRESCRA_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPRESCRA_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPRESCRA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPRESCRA_DairyComponentCancellationCode_BreastMilkIntakeRecords] FOREIGN KEY ([DairyComponentCancellationCode]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCPRESCRA_DairyComponentProfessionalCancellationCode_BreastMilkIntakeRecords] FOREIGN KEY ([DairyComponentProfessionalCancellationCode]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPRESCRA_HCPRESCRC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCPRESCRC] ([CODCONCEC]),
    CONSTRAINT [FK_HCPRESCRA_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCPRESCRA_IHFORMEDI] FOREIGN KEY ([CODFORMED]) REFERENCES [dbo].[IHFORMEDI] ([CODFORMED]),
    CONSTRAINT [FK_HCPRESCRA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCPRESCRA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPRESCRA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPRESCRA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCPRESCRA_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_HCPRESCRA_Schemes] FOREIGN KEY ([IDESQUEMAONC]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_ADcenaten];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_HCPRESCRC];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_HCVIAADMI];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_IHFORMEDI];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_INPacient];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_INUNIMEDI];


GO
ALTER TABLE [dbo].[HCPRESCRA] NOCHECK CONSTRAINT [FK_HCPRESCRA_Schemes];


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRA_CODPRODUC_CODCONCEC]
    ON [dbo].[HCPRESCRA]([CODPRODUC] ASC, [CODCONCEC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRA_CODCENATE_FECINIDOS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODPROSAL_CODPRODUC_CODDIAGNO_CANPEDPRO]
    ON [dbo].[HCPRESCRA]([CODCENATE] ASC, [FECINIDOS] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODPROSAL], [CODPRODUC], [CODDIAGNO], [CANPEDPRO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de cancelacion de la formula lactea en el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del profesional que suspendio la formula lactea desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentProfessionalCancellationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la justificacion de anulacion que se registro en el dashboard lactario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el motivo de anulacion registrado desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración por onza
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lacteo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ConcentrationPerOunce';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de onzas por dosis
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lacteo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OuncesPerDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tomas por día 
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lácteo---', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FeedingsPerDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de Formula lactea:

1- Solicitado por parte del medico / orden medica
2- Solicitud  Asignada por parte de lactario desde dashboard lactario (pasa a la pestaña corresponente en el dashboard)
3 - Anulada leche materna desde el medico / orden medica  (Es lo Primero que se hace porque lactario no deja anular si no esta anulado por medico)
4 - Anulada leche materna desde dashboard lactario (Se espra que este anulada por medico)
5 -Completado, todas las cantidades fueron entregadas 


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'StatusFormulaSairy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formula lactea:

Me indica si el medicamento esta marcado como formula lactea y por ende se solicitan nuevos campos, esto se parametriza en ERP en productos.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FormulaSairy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento Pendiente de Agendar en la Hoja de Medicamentos:  True => pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Medicamento  
1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez 
2: Ciclo Completado  
3: Tratamiento descontinuado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia  
4: Tratamiento Suspendido: Cuando el Medicamento es Suspendido 
5: Plan de Manejo Externo: Cuando los Medicamentos son entregados por un Tercero  
6: Medicamentos Solicitados sin Existencia Actual en el Kardex.  
7: Tratamiento Terminado por Salida del Paciente
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias 4:Semanas 5:meses 6:Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias 4:Semanas 5:meses 6:Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';

