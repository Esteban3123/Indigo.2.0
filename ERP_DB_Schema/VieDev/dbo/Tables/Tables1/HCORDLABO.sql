CREATE TABLE [dbo].[HCORDLABO] (
    [AUTO]                          INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                     CHAR (9)                                                                          NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                         NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                         NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [FECORDMED]                     DATETIME                                                                          NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                         NOT NULL,
    [CANSERIPS]                     INT                                                                               NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                    NULL,
    [PRISERIPS]                     CHAR (1)                                                                          NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                          NULL,
    [MANEXTPRO]                     BIT                                                                               NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')      NULL,
    [INTERPRET]                     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                         NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                        NULL,
    [SERREAINT]                     BIT                                                                               NOT NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                      NOT NULL,
    [ESTALELAB]                     BIT                                                                               NOT NULL,
    [FECRECMUE]                     DATETIME                                                                          NULL,
    [NOMARCLAB]                     CHAR (250)                                                                        NULL,
    [CONCURRE]                      ROWVERSION                                                                        NULL,
    [USURECMUE]                     CHAR (20)                                                                         NULL,
    [EXMREASIT]                     BIT                                                                               CONSTRAINT [DF_HCORDLABO_EXMREASIT] DEFAULT ((0)) NOT NULL,
    [GENSERVICEORDER]               INT                                                                               NULL,
    [RESANTSUPH]                    TINYINT MASKED WITH (FUNCTION = 'default()')                                      NULL,
    [FECRESANTSUPH]                 DATETIME                                                                          NULL,
    [RESSERSIF]                     TINYINT MASKED WITH (FUNCTION = 'default()')                                      NULL,
    [FECRESSERSIF]                  DATETIME                                                                          NULL,
    [RESELIVIH]                     TINYINT                                                                           NULL,
    [FECRESELIVIH]                  DATETIME MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [RESTSHNEO]                     TINYINT                                                                           NULL,
    [FECRESTSHNEO]                  DATETIME                                                                          NULL,
    [RESHEMOGLO]                    DECIMAL (18, 2)                                                                   NULL,
    [FECRESHEMOGLO]                 DATETIME                                                                          NULL,
    [FECGLISBASAL]                  DATETIME MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [RESCREATININA]                 DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECRESCREATININA]              DATETIME                                                                          NULL,
    [RESHEMGLO]                     DECIMAL (18, 2)                                                                   NULL,
    [FECRESHEMGLO]                  DATETIME                                                                          NULL,
    [FECMICROALBU]                  DATETIME                                                                          NULL,
    [FECHDL]                        DATETIME                                                                          NULL,
    [RESBASDIAG]                    TINYINT                                                                           NULL,
    [FECRESBASDIAG]                 DATETIME                                                                          NULL,
    [RESHDL]                        DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECCREATINUR]                  DATETIME                                                                          NULL,
    [RESCREATINUR]                  DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECCOLESTOTAL]                 DATETIME                                                                          NULL,
    [RESCOLESTOTAL]                 DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECLDL]                        DATETIME                                                                          NULL,
    [RESLDL]                        DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECPTH]                        DATETIME                                                                          NULL,
    [RESPTH]                        DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECALBUSERICA]                 DATETIME                                                                          NULL,
    [RESALBUSERICA]                 DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECFOSFOR]                     DATETIME                                                                          NULL,
    [RESFOSFORO]                    DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [RESMICROALBU]                  DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [PROFRESULT]                    NCHAR (20)                                                                        NULL,
    [ESPERESULT]                    CHAR (3)                                                                          NULL,
    [FECHARESULT]                   DATETIME                                                                          NULL,
    [CODMOTIVOMUESTRANOCONFORME]    CHAR (4)                                                                          NULL,
    [OBSERMUESTRANOCONFORME]        VARCHAR (MAX)                                                                     NULL,
    [TIPOFACTURACION]               INT                                                                               NULL,
    [IDRIASCUPS]                    INT                                                                               NULL,
    [CORRELACION]                   TINYINT                                                                           CONSTRAINT [DF_CORRELACION] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000)                                                                    NULL,
    [FECHASUGE]                     DATETIME                                                                          NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                               NULL,
    [TraceabilityPaperworkEventsId] INT                                                                               NULL,
    [TraceabilityPaperworkId]       INT                                                                               NULL,
    [MICROBIOLOGIA]                 BIT                                                                               NULL,
    [SYNCMIRTH]                     BIT                                                                               NULL,
    [CUPSEntityPanelId]             INT                                                                               NULL,
    [IdServerOrderDetail]           INT                                                                               NULL,
    CONSTRAINT [PK_HCORDLABO] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [Fk_ADINGRESO_NUMINGRES] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AMBORDLABO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDLABO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDLABO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_AMBORDLABO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDLABO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDLABO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDLABO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDLABO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDLABO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDLABO_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID]),
    CONSTRAINT [FK_IdServerOrderDetail_HCORDLABO] FOREIGN KEY ([IdServerOrderDetail]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [Fk_INPACIENT_IPCODPACI] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [Fk_ADINGRESO_NUMINGRES];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_RIASCUPS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [Fk_INPACIENT_IPCODPACI];


GO
CREATE NONCLUSTERED INDEX [IDX_HCORDLABO_ESTSERIPS]
    ON [dbo].[HCORDLABO]([ESTSERIPS] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_CODCENATE_ESTSERIPS_CODPROSAL_CODSERIPS_FECORDMED_IPCODPACI_NOMARCLAB_NUMEFOLIO_NUMINGRES]
    ON [dbo].[HCORDLABO]([CODCENATE] ASC, [ESTSERIPS] ASC)
    INCLUDE([CODPROSAL], [CODSERIPS], [FECORDMED], [IPCODPACI], [NOMARCLAB], [NUMEFOLIO], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_ESTSERIPS_UFUCODIGO_CODSERIPS]
    ON [dbo].[HCORDLABO]([ESTSERIPS] ASC, [UFUCODIGO] ASC, [CODSERIPS] ASC)
    INCLUDE([IPCODPACI], [NUMEFOLIO], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_CODSERIPS_MANEXTPRO_IDDESCRIPCIONRELACIONADA]
    ON [dbo].[HCORDLABO]([CODSERIPS] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [FECORDMED], [CANSERIPS], [OBSSERIPS], [CODDIAGNO], [TraceabilityPaperworkEventsId], [TraceabilityPaperworkId]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_NUMINGRES_CUPSEntityPanelId_CANSERIPS_CODPROINT_CODPROSAL_CODSERIPS_ESTSERIPS_FECHARESULT_FECHASUGE_FECORDMED_GENSE]
    ON [dbo].[HCORDLABO]([NUMINGRES] ASC, [CUPSEntityPanelId] ASC)
    INCLUDE([CANSERIPS], [CODPROINT], [CODPROSAL], [CODSERIPS], [ESTSERIPS], [FECHARESULT], [FECHASUGE], [FECORDMED], [GENSERVICEORDER], [IDDESCRIPCIONRELACIONADA], [INTERPRET], [IPCODPACI], [MANEXTPRO], [NUMEFOLIO], [NUMFOLINT], [OBSSERIPS], [PROFRESULT], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_CODSERIPS_FECRECMUE_SYNCMIRTH]
    ON [dbo].[HCORDLABO]([CODSERIPS] ASC, [FECRECMUE] ASC, [SYNCMIRTH] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [FECORDMED], [CANSERIPS], [OBSSERIPS], [PRISERIPS], [ESTSERIPS], [CODDIAGNO], [USURECMUE], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_UFUCODIGO_EXMREASIT_IDETIPHIS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODCENATE_CODPROSAL_FECORDMED]
    ON [dbo].[HCORDLABO]([UFUCODIGO] ASC, [EXMREASIT] ASC)
    INCLUDE([IDETIPHIS], [NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [CODPROSAL], [FECORDMED], [CODSERIPS], [CANSERIPS], [OBSSERIPS], [PRISERIPS], [ESTSERIPS], [CODDIAGNO], [SERREAINT], [ESTALELAB], [FECRECMUE], [CONCURRE], [CODMOTIVOMUESTRANOCONFORME], [FECHASUGE], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_UFUCODIGO_CUPSEntityPanelId_CANSERIPS_CODPROINT_CODPROSAL_CODSERIPS_ESTSERIPS_FECHARESULT_FECHASUGE_FECORDMED]
    ON [dbo].[HCORDLABO]([UFUCODIGO] ASC, [CUPSEntityPanelId] ASC)
    INCLUDE([CANSERIPS], [CODPROINT], [CODPROSAL], [CODSERIPS], [ESTSERIPS], [FECHARESULT], [FECHASUGE], [FECORDMED], [FECRECMUE], [GENSERVICEORDER], [IDDESCRIPCIONRELACIONADA], [INTERPRET], [IPCODPACI], [MANEXTPRO], [NUMEFOLIO], [NUMFOLINT], [NUMINGRES], [OBSSERIPS], [PROFRESULT], [USURECMUE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ServiceOrderDetail (Id)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el laboratoio es de microbiologia:  lo envia la interfaz y si es microbiologia es porque recibe resultados   preliminares.  0 - No es microbiologia  1 - Si es microbiologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'observacion de la muestra no conforme (laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'codigo del motivo de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'espere resultado del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESPERESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del profesional (laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PROFRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural  8:Muestra Recolectada Parcialmente  9: Muestra No Conforme', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

