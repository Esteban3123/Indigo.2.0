CREATE TABLE [dbo].[AMBORDPAT] (
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECORDMED]                DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                INT                                                                              NOT NULL,
    [ESTSERIPS]                CHAR (1)                                                                         NULL,
    [INDAUDFOR]                NUMERIC (18)                                                                     NOT NULL,
    [ESTALEPAT]                BIT                                                                              NOT NULL,
    [FECRECEXA]                DATETIME                                                                         NULL,
    [NOMARCPAT]                CHAR (250)                                                                       NULL,
    [CONCURRE]                 ROWVERSION                                                                       NULL,
    [USURECEXA]                CHAR (20)                                                                        NULL,
    [CODENTIDA]                CHAR (9)                                                                         NULL,
    [CODCONTRA]                CHAR (6)                                                                         NULL,
    [CODPANATE]                CHAR (2)                                                                         NULL,
    [IPFECHACO]                DATETIME                                                                         NOT NULL,
    [NUMCONCIT]                CHAR (20)                                                                        NULL,
    [OBSERVACI]                VARCHAR (2000)                                                                   NULL,
    [RESULTADO]                VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')        NULL,
    [FECHARESULT]              DATETIME                                                                         NULL,
    [BIOSENCER]                TINYINT MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [BIOSENBACAF]              TINYINT MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [FECSENCER]                DATETIME                                                                         NULL,
    [FECSENBACAF]              DATETIME                                                                         NULL,
    [PROFRESULTADO]            CHAR (10)                                                                        NULL,
    [GENSERVICEORDER]          INT                                                                              NULL,
    [AUTO]                     INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [GENINVOICE]               VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]             INT                                                                              NULL,
    [FECRECEPMUES]             DATETIME                                                                         NULL,
    [CODSERIPSREAL]            CHAR (20)                                                                        NULL,
    [ESPEREALIZA]              CHAR (3)                                                                         NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [CAC23]                    DATE                                                                             NULL,
    [CAC24]                    DATE                                                                             NULL,
    [CAC27]                    INT                                                                              NULL,
    [CAC28]                    INT                                                                              NULL,
    [CAC139]                   INT                                                                              NULL,
    [CAC140]                   INT                                                                              NULL,
    [CAC141]                   INT                                                                              NULL,
    [SYNCMIRTH]                BIT                                                                              NULL,
    [Laterality]               INT                                                                              NULL,
    [Specimen]                 INT                                                                              NULL,
    [Technique]                INT                                                                              NULL,
    [CollectionMedium]         INT                                                                              NULL,
    CONSTRAINT [PK_AMBORDPAT] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_AMBORDPAT_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDPAT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_CollectionMedium] FOREIGN KEY ([CollectionMedium]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Specimen] FOREIGN KEY ([Specimen]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Technique] FOREIGN KEY ([Technique]) REFERENCES [ClinicalParameters].[DefinitionSamples] ([Id]),
    CONSTRAINT [FK_AMBORDPAT_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDPAT_INCUPSIPS1] FOREIGN KEY ([CODSERIPSREAL]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDPAT_INESPECIA] FOREIGN KEY ([ESPEREALIZA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AMBORDPAT_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AMBORDPAT_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_ADcenaten];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_ADINGRESO];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_CollectionMedium];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Specimen];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_DefinitionSamples_Technique];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INCUPSIPS];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INCUPSIPS1];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INESPECIA];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INPacient];


GO
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medio de recolección campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Medio de recolección    Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CollectionMedium';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tecnica: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Tecnica    Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Technique';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especimen: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Especimen     Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Specimen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 = No aplica   1 = Izquierda   2 = Derecha   3 = Ambos       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Laterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'resultado de biopsia de seno por bacaf  1-> Benigna  2-> Atípica (Interdeterminada)  3-> Malignidad Sospechosa/Probable  4-> Maligna  5-> No Satisfactoria  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de Biopsia cervical  1-> Negativo para Neoplasia  2-> Infección por VPH  3-> NIC de Bajo Grado - NIC I  4-> NIC de Alto Grado: NIC II - NIC III  5-> Neoplasia Micro infiltrante:   Escamocelular o Adenocarcinoma  6-> Neoplasia Infiltrante: Escamocelular o Adenocarcinoma  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';

