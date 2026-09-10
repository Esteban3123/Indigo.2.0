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
ALTER TABLE [dbo].[AMBORDPAT] NOCHECK CONSTRAINT [FK_AMBORDPAT_INPacient];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDPAT].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDPAT].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDPAT].[RESULTADO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDPAT].[BIOSENCER]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDPAT].[BIOSENBACAF]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medio de recolección de muestra (INT, FK a ClinicalParameters.DefinitionSamples). Campo solicitado en orden médica de patología; valores predefinidos: suero, plasma, sangre total, orina, tejido, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CollectionMedium';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medio de recolección campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Medio de recolección    Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CollectionMedium';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CollectionMedium';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de análisis aplicada a la muestra (INT, FK a ClinicalParameters.DefinitionSamples). Campo solicitado en orden médica de patología; especifica método: histología, citología, cultivo, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Technique';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tecnica: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Tecnica    Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Technique';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Technique';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especimen o tipo de muestra (INT, FK a ClinicalParameters.DefinitionSamples). Campo solicitado en orden médica de patología; identifica la naturaleza del espécimen: biopsia, frotis, aspirado, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Specimen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especimen: campo que solicita en la orden medica de patologias y valor que viene de un maestro Definicion de muestras del tipo Especimen     Relacion con la tabla:  ClinicalParameters.DefinitionSamples', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Specimen';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Specimen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lateralidad anatómica (INT). 0=No aplica, 1=Izquierda, 2=Derecha, 3=Bilateral, 4=Multilateral. Usado en órdenes con ubicación corporal (seno, cervical, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Laterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0 = No aplica   1 = Izquierda   2 = Derecha   3 = Bilateral  4 = Multilateral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Laterality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'Laterality';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sincronización interfaz Mirth Connect (BIT). Estado de sincronización con laboratorio: NULL/0=Sin sincronizar, 1=Sincronizado. Aplica a registros con estado 2 (muestra recolectada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cumplimiento criterios de calidad en patología (DATE, Pregunta 141 CAC). Indicador booleano de validación de estándares de calidad en el informe patológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC141';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La patología cumple con lo criterios de calidad (Pregunta 141).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC141';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC141';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ganglios positivos reportados (INT, Pregunta 140 CAC). Conteo de ganglios linfáticos con resultado positivo en análisis patológico (oncología).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC140';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ganglios positivos. (Pregunta 140).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC140';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC140';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ganglios extraídos/reportados (INT, Pregunta 139 CAC). Total de ganglios linfáticos identificados y procesados en informe patológico (oncología).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC139';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ganglios extraídos reportados en el informe. (Pregunta 139)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC139';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC139';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de diferenciación tumoral (INT, Pregunta 28 CAC). Clasificación histológica: G1 bien diferenciado, G2 moderadamente diferenciado, G3 pobremente diferenciado (oncología).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grado de diferenciación del tumor (pregunta 28 CAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Histología del tumor (INT, Pregunta 27 CAC). Tipo histológico del neoplasma: adenocarcinoma, carcinoma escamocelular, etc. (clasificación patológica oncológica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Histologia del tumor (pregunta 27)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del informe patológico (DATE, Pregunta 24 CAC). Fecha de emisión del reporte. Se obtiene de tabla de integración ALULA si existe resultado disponible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del informe - No se hace la pregunta si existe resultado en ALULA y el dato se saca de la tabla de integración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC24';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recolección de muestra(s) (DATE, Pregunta 23 CAC). Timestamp de toma de especimen, requisito de trazabilidad en patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recolección de muestra(s) (Pregunta 23).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CAC23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID descripción CUPS relacionada (INT, FK a contract.CUPSEntityContractDescriptions). Vincula procedimiento a maestro contractual en Indigo Vie ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad que realiza el servicio (CHAR(3), FK a INESPECIA). Identifica profesional especializado ejecutor: patología, cirugía oncológica, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad Realizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESPEREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS real ejecutado (CHAR(20), FK a INCUPSIPS). Procedimiento/servicio efectivamente realizado, diferente del solicitado si hay variación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPSREAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios Realizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPSREAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPSREAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha recepción de muestra en laboratorio (DATETIME). Timestamp de ingreso de especimen al laboratorio de patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEPMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recepción de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEPMUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEPMUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de factura en Indigo Vie (INT). Identificador único de documento de facturación emitido por el ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura Indigo Vie (VARCHAR(20)). Número de comprobante fiscal/factura generado en sistema de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Numero de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID entidad EAPB del contrato (INT, FK a INENTIDAD). Identifica aseguradora cuando grupo de atención es EAPB sin contrato específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la Entidad Administradora de salud del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID grupo de atención (INT). Referencia al grupo de atención en base datos Indigo Vie; agrupa servicios por beneficiario/contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del grupo de atencion de la base de datos de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT, PK). Clave primaria para registro único de orden patológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID orden de servicio (INT). Referencia a orden de servicio en módulo de Control de Cuenta donde se incluye el servicio de patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Patologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que reporta resultado (CHAR(10), FK a INPROFSAL). Médico patólogo u especialista responsable de interpretación y firma del informe.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'PROFRESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que devuelve el resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'PROFRESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'PROFRESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha resultado biopsia seno por BACAF (DATETIME). Timestamp de emisión de resultado de biopsia por aspiración con aguja fina de mama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado de Biopsia de seno por bacaf', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha resultado biopsia cervical (DATETIME). Timestamp de emisión de resultado de biopsia cervical o citología cervical.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado de Biopsia Seno Cervical', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado biopsia seno BACAF (TINYINT, PII_ofuscado). 1=Benigna, 2=Atípica/Indeterminada, 3=Malignidad sospechosa, 4=Maligna, 5=No satisfactoria, 0=No aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'resultado de biopsia de seno por bacaf  1-> Benigna  2-> Atípica (Interdeterminada)  3-> Malignidad Sospechosa/Probable  4-> Maligna  5-> No Satisfactoria  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENBACAF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado biopsia cervical (TINYINT, PII_ofuscado). 1=Negativo neoplasia, 2=VPH+, 3=NIC I, 4=NIC II-III, 5=Neoplasia microinfiltrante, 6=Neoplasia infiltrante, 0=No aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de Biopsia cervical  1-> Negativo para Neoplasia  2-> Infección por VPH  3-> NIC de Bajo Grado - NIC I  4-> NIC de Alto Grado: NIC II - NIC III  5-> Neoplasia Micro infiltrante:   Escamocelular o Adenocarcinoma  6-> Neoplasia Infiltrante: Escamocelular o Adenocarcinoma  0-> No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'BIOSENCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha recepción informe patológico (DATETIME). Timestamp de entrega/disponibilidad del resultado final de patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción del informe patológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado patológico (VARCHAR(MAX), PII_ofuscado). Texto completo del informe de patología retornado por interfaz de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de patología que devuelve la interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas/administrativas (VARCHAR(2000)). Notas adicionales sobre la orden, muestra, resultado o circunstancias relevantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de cita en interfaz (CHAR(20)). Identificador de cita asignado por sistema de laboratorio externo/interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Consecutivo Cita en Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de consulta/orden (DATETIME). Timestamp del acto médico o solicitud de orden de patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPFECHACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código plan de beneficios (CHAR(2)). Clasificación del plan de cobertura del paciente afiliado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato (CHAR(6)). Identificador de acuerdo comercial entre institución y aseguradora/entidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código entidad aseguradora (CHAR(9), FK a INENTIDAD). Identificador de EAPB, ARS o aseguradora responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código usuario receptor (CHAR(20)). Identificador del operario que recibe/documenta la muestra o resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'USURECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'USURECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concurrencia (TIMESTAMP). Control de versión para optimista locking; detecta cambios simultáneos en registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrecia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre archivo adjunto patología (CHAR(250)). Nombre de documento digitalizado, PDF o imagen del informe patológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NOMARCPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha creación/registro (DATETIME). Timestamp de inserción del registro de orden patológica en base datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEXA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECRECEXA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado alerta patología (BIT). Indicador de alerta/flag: 0=Sin alerta, 1=Alerta activa en monitoreo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTALEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTALEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTALEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código auditoria (NUMERIC(18)). Identificador de auditoría interna para trazabilidad de cambios y control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado servicio CUPS/IPS (CHAR(1)). 1=Solicitado, 2=Muestra recolectada, 3=Resultado entregado, 4=Examen interpretado, 5=Remitido, 6=Anulado, 7=Extramural.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad servicio CUPS (INT). Número de unidades solicitadas del procedimiento/servicio de patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS procedimiento (CHAR(20), FK a INCUPSIPS). Código único de procedimiento/servicio solicitado según catálogo nacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha solicitud de orden (DATETIME). Timestamp de generación/solicitud de orden médica de patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de salud (CHAR(20), FK a INPROFSAL, PII_ofuscado). Identificador del médico solicitante (cédula, registro profesional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional (CHAR(10), FK a ADUFUNCIO). Identificador del departamento/servicio solicitante (urgencias, consulta externa, hospitalización).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención (CHAR(10), FK a ADCENATEN). Identificador de sede/institución donde se solicita el servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso/admisión (CHAR(10), FK a ADINGRESO). Identificador único del episodio de atención o internación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR(25), FK a INPACIENT, PII_ofuscado). Identificación del paciente (cédula, pasaporte, documento nacional de identidad).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas ambulatorias de servicios o exámenes solicitados a un paciente durante un ingreso o atención. Registra cada servicio (procedimiento, laboratorio, imagen u otro) ordenado por un profesional de la salud, junto con su estado, resultado, datos de recepción de muestra y trazabilidad de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDPAT';
