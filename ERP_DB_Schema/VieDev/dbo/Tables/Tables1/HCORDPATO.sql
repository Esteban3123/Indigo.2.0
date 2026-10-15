CREATE TABLE [dbo].[HCORDPATO] (
    [IDETIPHIS]                     CHAR (9)                                                                           NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                         NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                          NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                          NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                          NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [FECORDMED]                     DATETIME                                                                           NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                          NOT NULL,
    [CANSERIPS]                     INT                                                                                NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                     NULL,
    [PRISERIPS]                     CHAR (1)                                                                           NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                           NULL,
    [MANEXTPRO]                     BIT                                                                                NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')       NULL,
    [INTERPRET]                     VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                          NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                         NULL,
    [SERREAINT]                     BIT                                                                                NOT NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                       NOT NULL,
    [ESTALEPAT]                     BIT                                                                                NOT NULL,
    [FECRECEXA]                     DATETIME                                                                           NULL,
    [NOMARCPAT]                     CHAR (250)                                                                         NULL,
    [CONCURRE]                      ROWVERSION                                                                         NULL,
    [USURECEXA]                     CHAR (20)                                                                          NULL,
    [AUTO]                          INT                                                                                IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENSERVICEORDER]               INT                                                                                NULL,
    [RESULTADO]                     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')          NULL,
    [FECHARESULT]                   DATETIME                                                                           NULL,
    [BIOSENCER]                     TINYINT MASKED WITH (FUNCTION = 'default()')                                       NULL,
    [BIOSENBACAF]                   TINYINT MASKED WITH (FUNCTION = 'default()')                                       NULL,
    [FECSENCER]                     DATETIME                                                                           NULL,
    [FECSENBACAF]                   DATETIME                                                                           NULL,
    [PROFRESULTADO]                 CHAR (20)                                                                          NULL,
    [FECRECEPMUES]                  DATETIME                                                                           NULL,
    [ESPEREALIZA]                   CHAR (3)                                                                           NULL,
    [CODSERIPSREAL]                 CHAR (20)                                                                          NULL,
    [TIPOFACTURACION]               INT                                                                                NULL,
    [IDRIASCUPS]                    INT                                                                                NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                                NULL,
    [CORRELACION]                   TINYINT                                                                            CONSTRAINT [DF__HCORDPATO__CORRE__7BB77E15] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000)                                                                     NULL,
    [TraceabilityPaperworkEventsId] INT                                                                                NULL,
    [TraceabilityPaperworkId]       INT                                                                                NULL,
    [CAC23]                         DATE                                                                               NULL,
    [CAC24]                         DATE                                                                               NULL,
    [CAC27]                         INT                                                                                NULL,
    [CAC28]                         INT                                                                                NULL,
    [CAC139]                        INT                                                                                NULL,
    [CAC140]                        INT                                                                                NULL,
    [CAC141]                        INT                                                                                NULL,
    [SYNCMIRTH]                     BIT                                                                                NULL,
    [Laterality]                    INT                                                                                NULL,
    [Specimen]                      INT                                                                                NULL,
    [Technique]                     INT                                                                                NULL,
    [CollectionMedium]              INT                                                                                NULL,
    CONSTRAINT [PK_HCORDPATO] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPATO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPATO_DefinitionSamples_CollectionMedium] FOREIGN KEY ([CollectionMedium]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_HCORDPATO_DefinitionSamples_Specimen] FOREIGN KEY ([Specimen]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_HCORDPATO_DefinitionSamples_Technique] FOREIGN KEY ([Technique]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_HCORDPATO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCORDPATO] NOCHECK CONSTRAINT [FK_HCORDPATO_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDPATO] NOCHECK CONSTRAINT [FK_HCORDPATO_DefinitionSamples_CollectionMedium];


GO
ALTER TABLE [dbo].[HCORDPATO] NOCHECK CONSTRAINT [FK_HCORDPATO_DefinitionSamples_Specimen];


GO
ALTER TABLE [dbo].[HCORDPATO] NOCHECK CONSTRAINT [FK_HCORDPATO_DefinitionSamples_Technique];


GO
ALTER TABLE [dbo].[HCORDPATO] NOCHECK CONSTRAINT [FK_HCORDPATO_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medio de recolección campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Medio de recolección    Relacion con la tabla:  ClinicalParameters.DefinitionSamples    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CollectionMedium';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tecnica: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Tecnica    Relacion con la tabla:  ClinicalParameters.DefinitionSamples      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Technique';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especimen: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Especimen     Relacion con la tabla:  ClinicalParameters.DefinitionSamples      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Specimen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 = No aplica   1 = Izquierda   2 = Derecha   3 = Ambos       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'Laterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'resultado de biopsia de seno por bacaf  1-> Benigna  2-> Atípica (Interdeterminada)  3-> Malignidad Sospechosa/Probable  4-> Maligna  5-> No Satisfactoria  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de Biopsia cervical  1-> Negativo para Neoplasia  2-> Infección por VPH  3-> NIC de Bajo Grado - NIC I  4-> NIC de Alto Grado: NIC II - NIC III  5-> Neoplasia Micro infiltrante:   Escamocelular o Adenocarcinoma  6-> Neoplasia Infiltrante: Escamocelular o Adenocarcinoma  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'BIOSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1: Solicitado 
2: Muestra Recolectada 
3: Resultado Entregado 
4: Examen Interpretado 
5: Remitido 
6: Anulado 
7: Extramural

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPATO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

