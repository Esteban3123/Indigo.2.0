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
    [Principal] BIT NULL,
    [AuthorizationEventId]          INT            NULL,
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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPRESCRA].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
CREATE NONCLUSTERED INDEX [Productos_Pacientes]
    ON [dbo].[HCPRESCRA]([CODPRODUC] ASC, [PREESTADO] ASC);


GO
ALTER INDEX [Productos_Pacientes]
    ON [dbo].[HCPRESCRA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [HCPRESCRA_Ix]
    ON [dbo].[HCPRESCRA]([PREESTADO] ASC)
    INCLUDE([CODPRODUC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRA_CODPRODUC_CODCONCEC]
    ON [dbo].[HCPRESCRA]([CODPRODUC] ASC, [CODCONCEC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRA_CODCENATE_FECINIDOS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODPROSAL_CODPRODUC_CODDIAGNO_CANPEDPRO]
    ON [dbo].[HCPRESCRA]([CODCENATE] ASC, [FECINIDOS] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODPROSAL], [CODPRODUC], [CODDIAGNO], [CANPEDPRO]);


GO
ALTER INDEX [IX_HCPRESCRA_CODCENATE_FECINIDOS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODPROSAL_CODPRODUC_CODDIAGNO_CANPEDPRO]
    ON [dbo].[HCPRESCRA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRA_NUMINGRES_CODPRODUCT_NUMEFOLIO]
    ON [dbo].[HCPRESCRA]([NUMINGRES] ASC, [CODPRODUC] ASC, [NUMEFOLIO] ASC)
    INCLUDE([CODDIAGNO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRA]
ON [dbo].[HCPRESCRA]([IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC, [PREESTADO] ASC) INCLUDE([NUMEFOLIO],[CODPRODUC],[Principal],[UFUCODIGO]);

GO
CREATE NONCLUSTERED INDEX [_dta_index_HCPRESCRA_8_474484769__K15_K5_K8_K3_K30_K1_K2_K6_K4_7]
    ON [dbo].[HCPRESCRA]([FECINIDOS] ASC, [CODCENATE] ASC, [CODPRODUC] ASC, [IPCODPACI] ASC, [MANEXTPRO] ASC, [IDETIPHIS] ASC, [NUMEFOLIO] ASC, [UFUCODIGO] ASC, [NUMINGRES] ASC)
    INCLUDE([CODPROSAL]);


GO
CREATE NONCLUSTERED INDEX [IX_HCPRESCRA_DashboardMedicalOrders_CareCenter_RequestDate]
    ON [dbo].[HCPRESCRA]([CODCENATE] ASC, [FECINIDOS] DESC)
    INCLUDE([ID], [UFUCODIGO], [NUMINGRES], [NUMEFOLIO], [IPCODPACI], [CODPROSAL], [CANPEDPRO], [CODPRODUC])
    WHERE [MANEXTPRO] = 1 AND [FECINIDOS] >= '20260102';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de cancelación de fórmula láctea registrada en dashboard lactario (DATETIME). PII: fecha asociada a paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de cancelacion de la formula lactea en el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que suspendió fórmula láctea desde dashboard lactario (FK a INPROFSAL). PII: identificación de profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentProfessionalCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del profesional que suspendio la formula lactea desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentProfessionalCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentProfessionalCancellationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica de anulación de componente lácteo registrada en dashboard lactario (VARCHAR 500). Trazabilidad lactario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la justificacion de anulacion que se registro en el dashboard lactario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de anulación de fórmula láctea desde dashboard lactario (FK a HCMOANULB). Cataloga razón de suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el motivo de anulacion registrado desde el dashboard lactario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DairyComponentCancellationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración por onza (DECIMAL 4,1). Campo activo solo para componentes lácteos. Dosificación láctea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ConcentrationPerOunce';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentración por onza
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lacteo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ConcentrationPerOunce';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ConcentrationPerOunce';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de onzas por dosis (DECIMAL 4,1). Habilitado solo para productos tipo componente lácteo. Volumen lactario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OuncesPerDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de onzas por dosis
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lacteo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OuncesPerDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OuncesPerDose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tomas por día (INT). Campo específico para fórmula láctea, lactario. Frecuencia diaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FeedingsPerDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de tomas por día 
---Habilitado y diligenciado solamente cuando el producto es de tipo componente lácteo---', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FeedingsPerDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FeedingsPerDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de fórmula láctea (INT): 1=Solicitado por médico, 2=Asignado por lactario, 3=Anulado por médico, 4=Anulado por lactario, 5=Completado. Ciclo de vida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'StatusFormulaSairy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Formula lactea:

1- Solicitado por parte del medico / orden medica
2- Solicitud  Asignada por parte de lactario desde dashboard lactario (pasa a la pestaña corresponente en el dashboard)
3 - Anulada leche materna desde el medico / orden medica  (Es lo Primero que se hace porque lactario no deja anular si no esta anulado por medico)
4 - Anulada leche materna desde dashboard lactario (Se espra que este anulada por medico)
5 -Completado, todas las cantidades fueron entregadas 


', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'StatusFormulaSairy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'StatusFormulaSairy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si medicamento es fórmula láctea (BIT). Parametrizado en productos ERP. Habilita campos lactarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FormulaSairy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formula lactea:

Me indica si el medicamento esta marcado como formula lactea y por ende se solicitan nuevos campos, esto se parametriza en ERP en productos.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FormulaSairy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FormulaSairy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón descontinuación de medicamento (INT): 1=Reacciones adversas/riesgos, 2=Otra razón. Causa suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razon descontinuacion de medicamento 1. Riesgos y reacciones adversas de medicamentos 2. Otra Razón o motivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro (INT IDENTITY). Clave primaria de prescripción en farmacoterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con orden de quimioterapia (INT, FK EHR.Schemes). Vincula prescripción a esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de Orden de Quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS/medicamentos especiales (VARCHAR 2000). Requiere argumento para aprobación farmacoterapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de trámite (INT). Puede no tener eventos si trámite fue cancelado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del último evento registrado en trámite (INT). Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de medicamento con resistencia bacteriana (VARCHAR 500). Soporte antibiótico selectivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de medicamento con resistencia bacteriana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'JUSTIFICARB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de décima dosis (DATETIME). Programación horaria de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de novena dosis (DATETIME). Calendario de administración farmacoterapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de octava dosis (DATETIME). Horario de administración medicamentosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de séptima dosis (DATETIME). Programación dosis siete.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de sexta dosis (DATETIME). Programación dosis seis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de quinta dosis (DATETIME). Programación dosis cinco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de cuarta dosis (DATETIME). Programación dosis cuatro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de tercera dosis (DATETIME). Programación dosis tres.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de segunda dosis (DATETIME). Programación dosis dos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de aplicación de primera dosis (DATETIME). Inicio de administración farmacoterapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de aplicación de la primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'HORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Décima dosis del medicamento (NUMERIC 18,2). Cantidad unitaria de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Decima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novena dosis del medicamento (NUMERIC 18,2). Valor de dosis nueve.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Novena dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Octava dosis del medicamento (NUMERIC 18,2). Valor de dosis ocho.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Octava dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Séptima dosis del medicamento (NUMERIC 18,2). Valor de dosis siete.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Septima dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexta dosis del medicamento (NUMERIC 18,2). Valor de dosis seis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Quinta dosis del medicamento (NUMERIC 18,2). Valor de dosis cinco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quinta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta dosis del medicamento (NUMERIC 18,2). Valor de dosis cuatro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuarta dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera dosis del medicamento (NUMERIC 18,2). Valor de dosis tres.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda dosis del medicamento (NUMERIC 18,2). Valor de dosis dos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segunda dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera dosis del medicamento (NUMERIC 18,2). Cantidad inicial de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primera dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de dosis programadas (TINYINT). Cantidad de administraciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMERODOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de prescripción (TINYINT): 0=Estándar, 1=Personalizada. Modo de formulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de prescripción: 0:Estandar, 1:Personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador medicamento de custodia (BIT). Solo intrahospitalaria, requiere guarda especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me identifica si el medicamento es de custodia, solo para la parte Intrahospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador esquema oncológico (INT, FK EHR.Schemes). Vinculación a quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id esquema oncologico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de justificación medicamentos especiales (CHAR 3). Cataloga argumento de prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODJUMEES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODJUMEES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de justificación medicamentos especiales (VARCHAR 500). Sustento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la justificacion de medicamentos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OBSJUSMEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad medida final post-conversión (CHAR 3). Conversiones: g/mg/mcg.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la Unidad Medida final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis final post-conversión de unidades (NUMERIC 18,2). Valor ajustado después de conversión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la dosis final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de control de auditoría (NUMERIC 18). Trazabilidad de cambios formulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción administración manual del medicamento (VARCHAR MAX). Indicaciones de aplicación manual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion del medicamento (Dosis Manual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador formulación manual (BIT). Flag si prescripción fue ingresada manualmente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formulacion Manual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FORMUMANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FORMUMANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento pendiente de agendar (BIT): true=pendiente en hoja medicamentos. Estatus de programación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento Pendiente de Agendar en la Hoja de Medicamentos:  True => pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MEDPENAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del producto por unidad (NUMERIC 18,2). Sumatoria de dosis, cantidad total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del producto por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio desde donde se suspendió medicamento (CHAR 10). Trazabilidad de suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio desde Donde se Suspendio el Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pedida del producto (INT). Número de unidades solicitadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Producto de plan de manejo externo (BIT). Medicamentos suministrados por tercero/farmacia externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de niveles de riesgo (VARCHAR 1000). Descripción clínica de riesgos identificados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OBSNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de los niveles de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OBSNIVRIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'OBSNIVRIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de riesgo del paciente (CHAR 1). Cataloga severidad: bajo/medio/alto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NIVRIEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NIVRIEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NIVRIEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de suspensión del medicamento (VARCHAR 2000). Razón clínica de discontinuación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de suspendsion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de aplicación de medicamentos (VARCHAR MAX). Instrucciones técnicas de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico principal (CHAR 4, FK INDIAGNOS). Razón clínica de prescripción. Ofuscado CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico principal o razon principal por la solicitud del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de orden médica (NUMERIC 18, FK HCPRESCRC). Referencia a orden matriz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de la Orden Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de prescripción (INT): 1=Iniciado, 2=Ciclo completado, 3=Descontinuado, 4=Suspendido, 5=Manejo externo, 6=Sin existencia, 7=Terminado por egreso. Ciclo de medicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Medicamento  
1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez 
2: Ciclo Completado  
3: Tratamiento descontinuado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia  
4: Tratamiento Suspendido: Cuando el Medicamento es Suspendido 
5: Plan de Manejo Externo: Cuando los Medicamentos son entregados por un Tercero  
6: Medicamentos Solicitados sin Existencia Actual en el Kardex.  
7: Tratamiento Terminado por Salida del Paciente
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de prescripción anterior (CHAR 250). Razón de cambio previo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo Prescripcion Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MOTPREANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'MOTPREANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prescripción con fecha anterior (BIT). Indica si hay prescripción previa vigente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prescripcion Fecha Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'PREFECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'PREFECANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de dosis (DATETIME). Término programado del tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de duración fija (CHAR 1): 1=Minutos, 2=Horas, 3=Días, 4=Semanas, 5=Meses, 6=Años. Escala temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias 4:Semanas 5:meses 6:Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de duración fija (INT). Número en la unidad especificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la dosis (CHAR 20). Período total de tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulación (CHAR 1): 1=Peso, 2=Volumen, 3=Peso-Volumen, 4=Unidad de administración. Modo de cuantificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de dosis (DATETIME). Inicio de tratamiento farmacoterapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FECINIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FECINIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de frecuencia (CHAR 1): 1=Minutos, 2=Horas, 3=Días, 4=Semanas, 5=Meses, 6=Años. Intervalo temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias 4:Semanas 5:meses 6:Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración (INT). Número de dosis en la unidad especificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad de medida (VARCHAR 20, FK INUNIMEDI). Referencia a catálogo de unidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento (NUMERIC 18,2). Cantidad por aplicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código forma de presentación (VARCHAR 20, FK IHFORMEDI). Referencia a tableta/ampolla/jarabe.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía de administración (VARCHAR 20, FK HCVIAADMI). Referencia a oral/IV/IM/tópica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/medicamento (CHAR 20). Referencia a catálogo de fármacos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (VARCHAR 20, FK INPROFSAL). PII: identificación de prescriptor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10, FK INUNIFUNC). Referencia a departamento/área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK ADCENATEN). Referencia a institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión (CHAR 10, FK ADINGRESO). Identifica episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, FK INPACIENT). Cédula/documento identificación. PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de prescripción (CHAR 10). Consecutivo de hoja de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno de tipo de historia clínica (CHAR 9). Clasificación de registro médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Indica cual es el medicamento principal de la atención - Solo aplica para unidad de urgencias',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'HCPRESCRA',
    @level2type = N'COLUMN',
    @level2name = N'Principal'


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prescripciones médicas de medicamentos realizadas durante un ingreso hospitalario o atención ambulatoria. Registra el detalle de cada fórmula médica: producto prescrito, dosis, vía de administración, frecuencia, duración del tratamiento, estado de la prescripción y datos del profesional que prescribe, incluyendo soporte para esquemas oncológicos y fórmulas de nutrición láctea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta prescripción. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPRESCRA', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';
