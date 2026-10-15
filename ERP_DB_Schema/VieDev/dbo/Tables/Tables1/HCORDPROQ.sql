CREATE TABLE [dbo].[HCORDPROQ] (
    [IDETIPHIS]                     CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]                     DATETIME                                                                         NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                     INT                                                                              NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                   NULL,
    [PRISERIPS]                     CHAR (1)                                                                         NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                         NULL,
    [MANEXTPRO]                     BIT                                                                              NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                     NOT NULL,
    [LATERALIDAD]                   TINYINT                                                                          NULL,
    [SOLICITASALA]                  BIT                                                                              NULL,
    [GENSERVICEORDER]               INT                                                                              NULL,
    [AUTO]                          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OTROSMATERIALES]               VARCHAR (5000)                                                                   NULL,
    [SOLICITAMATOST]                TINYINT                                                                          NULL,
    [VIEPSUPPLIER]                  INT                                                                              NULL,
    [AUTOCITA]                      INT                                                                              NULL,
    [CODANULA]                      CHAR (3)                                                                         NULL,
    [FECHAANULA]                    DATETIME                                                                         NULL,
    [PROFESIONALANULA]              CHAR (20)                                                                        NULL,
    [MOTIVOANULA]                   VARCHAR (1000)                                                                   NULL,
    [TIPOFACTURACION]               INT                                                                              NULL,
    [IDRIASCUPS]                    INT                                                                              NULL,
    [CODALMACEN]                    NVARCHAR (10)                                                                    NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                              NULL,
    [TraceabilityPaperworkEventsId] INT                                                                              NULL,
    [TraceabilityPaperworkId]       INT                                                                              NULL,
    [IDADRADICACIONQX]              INT                                                                              NULL,
    [OBSANULADO]                    VARCHAR (300)                                                                    NULL,
    [PROANULADO]                    CHAR (20)                                                                        NULL,
    [FECHAANULADO]                  DATETIME                                                                         NULL,
    [IDAREAPRO]                     INT                                                                              NULL,
    [CODCENATEPRO]                  CHAR (10)                                                                        NULL,
    [UFUCODIGOPRO]                  CHAR (10)                                                                        NULL,
    [CODUSUPRO]                     CHAR (20)                                                                        NULL,
    [FECHAPRO]                      DATETIME                                                                         NULL,
    [MEDREALI]                      CHAR (20)                                                                        NULL,
    [ESPEREALI]                     CHAR (3)                                                                         NULL,
    [CODMOTANU]                     CHAR (4)                                                                         NULL,
    CONSTRAINT [PK_HCORDPROQ] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPROQ_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPROQ_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPROQ_Cirugia] FOREIGN KEY ([AUTOCITA]) REFERENCES [dbo].[AGEPROGQX] ([CODAUTONU]),
    CONSTRAINT [FK_HCORDPROQ_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]) ON UPDATE CASCADE,
    CONSTRAINT [FK_HCORDPROQ_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDPROQ_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPROQ_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDPROQ_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDPROQ_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_Cirugia];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INPacient];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCORDPROQ] NOCHECK CONSTRAINT [FK_HCORDPROQ_RIASCUPS];


GO
CREATE NONCLUSTERED INDEX [IX_HCORDPROQ_CODSERIPS_ESTSERIPS_SOLICITASALA_FECORDMED]
    ON [dbo].[HCORDPROQ]([CODSERIPS] ASC, [ESTSERIPS] ASC, [SOLICITASALA] ASC, [FECORDMED] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [CODPROSAL], [OBSSERIPS], [PRISERIPS], [CODDIAGNO], [IDDESCRIPCIONRELACIONADA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del motivo de anulacion (codigo de la tabla HCMOANULB) -->23-05-2025 - PBI 27070 Crear modal Justificacion anulación para las ordenes de Procedimientos no QX <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si en el procedimiento qx se solicitó material de osteosintesis  0-> No solicitó  1-> Solicitado  2-> Entregado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'SOLICITAMATOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En caso de que aplique, especifica la lateralidad de la extremidad o parte del cuerpo del procedimiento a realizar    0->No Aplica  1->Izquierda  3->Derecha  4->Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Sala Programada  3: Cancelado  4: Resultado Revisado  5: Anulado  6: Programado no realizado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado:   1: Emergencia   2: Urgencia   3: Normal   4: Definir Conducta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQ', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

