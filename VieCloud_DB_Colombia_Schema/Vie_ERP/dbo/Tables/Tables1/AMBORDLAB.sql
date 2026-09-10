CREATE TABLE [dbo].[AMBORDLAB] (
    [AUTO]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                  CHAR (10)                                                                        NULL,
    [CODPROSAL]                  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECORDMED]                  DATETIME                                                                         NOT NULL,
    [CODSERIPS]                  CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]                  INT                                                                              NOT NULL,
    [ESTSERIPS]                  CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR]                  NUMERIC (18)                                                                     NOT NULL,
    [ESTALELAB]                  BIT                                                                              NOT NULL,
    [FECRECMUE]                  DATETIME                                                                         NULL,
    [NOMARCLAB]                  CHAR (250)                                                                       NULL,
    [CONCURRE]                   ROWVERSION                                                                       NULL,
    [USURECMUE]                  CHAR (20)                                                                        NULL,
    [CODENTIDA]                  CHAR (9)                                                                         NULL,
    [CODCONTRA]                  CHAR (6)                                                                         NULL,
    [CODPANATE]                  CHAR (2)                                                                         NULL,
    [IPFECHACO]                  DATETIME                                                                         NOT NULL,
    [NUMCONCIT]                  CHAR (20)                                                                        NULL,
    [OBSERVACI]                  VARCHAR (2000)                                                                   NULL,
    [GENCAREGROUP]               INT                                                                              NULL,
    [GENCONENTITY]               INT                                                                              NULL,
    [GENINVOICE]                 VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]               INT                                                                              NULL,
    [RESANTSUPH]                 TINYINT MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [FECRESANTSUPH]              DATETIME                                                                         NULL,
    [RESSERSIF]                  TINYINT MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [FECRESSERSIF]               DATETIME                                                                         NULL,
    [RESELIVIH]                  TINYINT                                                                          NULL,
    [FECRESELIVIH]               DATETIME MASKED WITH (FUNCTION = 'default()')                                    NULL,
    [RESTSHNEO]                  TINYINT                                                                          NULL,
    [FECRESTSHNEO]               DATETIME                                                                         NULL,
    [RESHEMOGLO]                 DECIMAL (18, 2)                                                                  NULL,
    [FECRESHEMOGLO]              DATETIME                                                                         NULL,
    [FECGLISBASAL]               DATETIME MASKED WITH (FUNCTION = 'default()')                                    NULL,
    [RESCREATININA]              DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [FECRESCREATININA]           DATETIME                                                                         NULL,
    [RESHEMGLO]                  DECIMAL (18, 2)                                                                  NULL,
    [FECRESHEMGLO]               DATETIME                                                                         NULL,
    [FECMICROALBU]               DATETIME                                                                         NULL,
    [FECHDL]                     DATETIME                                                                         NULL,
    [RESBASDIAG]                 TINYINT                                                                          NULL,
    [FECRESBASDIAG]              DATETIME                                                                         NULL,
    [RESMICROALBU]               DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [RESHDL]                     DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [FECCREATINUR]               DATETIME                                                                         NULL,
    [RESCREATINUR]               DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [FECCOLESTOTAL]              DATETIME                                                                         NULL,
    [RESCOLESTOTAL]              DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [FECLDL]                     DATETIME                                                                         NULL,
    [RESLDL]                     DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [FECPTH]                     DATETIME                                                                         NULL,
    [RESPTH]                     DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [FECALBUSERICA]              DATETIME                                                                         NULL,
    [RESALBUSERICA]              DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [FECFOSFOR]                  DATETIME                                                                         NULL,
    [RESFOSFORO]                 DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                             NULL,
    [INTERPRET]                  VARCHAR (4000)                                                                   NULL,
    [CODPROINT]                  CHAR (20)                                                                        NULL,
    [NUMFOLINT]                  CHAR (10)                                                                        NULL,
    [PROFRESULT]                 NCHAR (20)                                                                       NULL,
    [ESPERESULT]                 CHAR (3)                                                                         NULL,
    [FECHARESULT]                DATETIME                                                                         NULL,
    [CODMOTIVOMUESTRANOCONFORME] CHAR (4)                                                                         NULL,
    [OBSERMUESTRANOCONFORME]     VARCHAR (MAX)                                                                    NULL,
    [IDDESCRIPCIONRELACIONADA]   INT                                                                              NULL,
    [MICROBIOLOGIA]              BIT                                                                              NULL,
    [SYNCMIRTH]                  BIT                                                                              NULL,
    [IdServerOrderDetail]        INT                                                                              NULL,
    [GENSERVICEORDER]            INT                                                                              NULL,
    CONSTRAINT [PK_AMBORDLABO] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [Fk_ADINGRESO_NUMINGRES_AMBORDLAB] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AMBORDLAB_INENTIDAD] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTIDAD] ([CODENTIDA]),
    CONSTRAINT [FK_AMBORDLAB_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDLABO_ADCENATEN1] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDLABO_INCUPSIPS1] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDLABO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_IdServerOrderDetail_AMBORDLAB] FOREIGN KEY ([IdServerOrderDetail]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [Fk_INPACIENT_IPCODPACI_AMBORDLAB] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESANTSUPH]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESSERSIF]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[FECRESELIVIH]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[FECGLISBASAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESCREATININA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESMICROALBU]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESHDL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESCREATINUR]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESCOLESTOTAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESLDL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESPTH]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESALBUSERICA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AMBORDLAB].[RESFOSFORO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO

CREATE NONCLUSTERED INDEX [UX_AMBORLAB_CODSERIPS_SYNCMIRTH]
    ON [dbo].[AMBORDLAB]([CODSERIPS] ASC, [SYNCMIRTH] ASC)
    INCLUDE([IPCODPACI], [CODPROSAL], [ESTSERIPS]);


GO
CREATE NONCLUSTERED INDEX [IX_AMBORDLAB_NUMINGRES_CODSERIPS]
    ON [dbo].[AMBORDLAB]([NUMINGRES] ASC, [CODSERIPS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la orden de servicio de laboratorio desde VIE ERP. Se asigna al facturar desde Control de Cuenta. Tipo: INT. Vinculación con gestión de órdenes de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Imagenologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de orden de servicios laboratoriales. Tipo: INT. FK a Billing.ServiceOrderDetail.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica estado de sincronización con interfaz Mirth Connect para recepción de resultados. Valores: NULL/0=Sin sincronizar, 1=Sincronizado, aplica a registros en estado 2 (Muestra recolectada). Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el análisis pertenece a laboratorio de microbiología. Valores: 0=No, 1=Sí (recibe resultados preliminares). Proveniente de interfaz. Tipo: BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el laboratoio es de microbiologia:  lo envia la interfaz y si es microbiologia es porque recibe resultados   preliminares.  0 - No es microbiologia  1 - Si es microbiologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de descripción de relación con VIE ERP (contract.CUPSEntityContractDescriptions). Tipo: INT. Vinculación contractual de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones detalladas sobre no conformidad de la muestra biológica recolectada. Tipo: VARCHAR(MAX). Documentación de incidencias en muestreo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'observacion de la muestra no conforme', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código que identifica el motivo específico de rechazo de la muestra por no conformidad. Tipo: CHAR(4). Clasificación de defectos en recolección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del motivo de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de generación del resultado del examen de laboratorio. Tipo: DATETIME. Trazabilidad de resultados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha del resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de estado: esperar resultado pendiente. Tipo: CHAR(3). Indicador de entrega de resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'espere resultado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESPERESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula/ID del profesional de la salud que genera o autoriza el resultado. Tipo: NCHAR(20). Responsabilidad del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'profesional queda resutado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'PROFRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo de la interpretación médica del resultado. Tipo: CHAR(10). Trazabilidad de interpretación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de folio de interpretación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o cédula del médico especialista que interpreta el resultado. Tipo: CHAR(20). FK a INPROFSAL. Responsabilidad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción narrativa de la interpretación clínica del resultado por parte del médico. Tipo: VARCHAR(4000). Análisis y conclusiones médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la interpretación del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de fósforo en suero. Unidades: mg/dL. Rango: 5 caracteres con decimales. Tipo: DECIMAL(18,2). Bioquímica renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado  Fosforo (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento del análisis de fósforo sérico. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Fosforo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de albúmina sérica. Unidades: g/dL. Rango: 5 caracteres. Tipo: DECIMAL(18,2). Proteína plasmática, evaluación nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado Albumina Serica (Longitud 5) (Unidades: g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de albúmina sérica. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Albumina Serica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de hormona paratiroidea (PTH). Tipo: DECIMAL(18,2). Marcador de metabolismo óseo-mineral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de PTH. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de colesterol LDL. Unidades: mg/dL. Rango: 5 caracteres. Tipo: DECIMAL(18,2). Lipidemia, riesgo cardiovascular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado LDL (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de LDL. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha LDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de colesterol total sérico. Unidades: mg/dL. Rango: 5 caracteres. Tipo: DECIMAL(18,2). Perfil lipídico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de Colesterol Total (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de colesterol total. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Colesterol Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de creatinina en orina 24h. Unidades: mg/dL. Rango: 5 caracteres. Tipo: DECIMAL(18,2). Función renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la Creatinuria (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de creatinuria. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creatinuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de colesterol HDL. Unidades: mg/dL. Rango: 5 caracteres. Tipo: DECIMAL(18,2). Lipidemia, colesterol protector.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado HDL  (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de microalbuminuria en orina. Tipo: DECIMAL(18,2). Marcador de daño renal temprano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de baciloscopia de diagnóstico (TB). Tipo: DATETIME. Trazabilidad de TBC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE Baciloscopia de Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de baciloscopia de diagnóstico. Valores: 1=Negativa (sin BAAR), 2=Positiva (presencia de BAAR). Tipo: TINYINT. Diagnóstico de tuberculosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Baciloscopia de Diagnóstico: 1. Negativa, 2. Positiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de HDL. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE RESULTADO DE HDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de microalbuminuria. Tipo: DATETIME. Trazabilidad de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de hemoglobina glicosilada (HbA1c). Tipo: DATETIME. Trazabilidad de glucemia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Hemoglobina Glicosilada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de hemoglobina glicosilada (HbA1c) para control glucémico. Rango: 5-20 con decimales. Unidades: %. Tipo: DECIMAL(18,2). Control diabético.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoglobina Glicosilada: Valor mínimo 5 y máximo 20, permitir decimales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de creatinina sérica. Tipo: DATETIME. Trazabilidad de función renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de creatinina en suero. Tipo: DECIMAL(18,2). Marcador de función renal, filtración glomerular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de glicemia basal en ayunas. Tipo: DATETIME. Trazabilidad de glucemia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO DE GLISEMIA BASAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de hemoglobina total. Tipo: DATETIME. Trazabilidad de hematología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado cuantitativo de hemoglobina total. Rango: 1.5-20 g/dL con decimales. Tipo: DECIMAL(18,2). Anemia, transporte oxígeno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoglobina: Valor mínimo 1.5 y máximo 20', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de TSH neonatal (tamizaje congénito). Tipo: DATETIME. Trazabilidad de hipotiroidismo congénito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TSH Neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de TSH neonatal en sangre de talón. Valores: 1=Normal, 2=Anormal (requiere confirmación). Tipo: TINYINT. Tamizaje de hipotiroidismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TSH Neonatal: 1. Normal, 2. Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de prueba ELISA para VIH. Tipo: DATETIME. Trazabilidad de VIH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de prueba ELISA para virus de inmunodeficiencia humana. Valores: 1=Negativo, 2=Positivo (requiere confirmación Western blot). Tipo: TINYINT. Detección VIH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de serología para sífilis (RPR/VDRL/FTA). Tipo: DATETIME. Trazabilidad de sífilis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA RESULTADO Serología para Sífilis:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de serología para sífilis. Valores: 1=No reactiva (negativa), 2=Reactiva (positiva). Tipo: TINYINT. Detección de treponema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO Serología para Sífilis: 1. No Reactiva, 2. Reactiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de procesamiento de antígeno de superficie HBsAg en gestantes. Tipo: DATETIME. Trazabilidad de hepatitis B.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA RESULTADO Antígeno de Superficie Hepatitis B en Gestantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de antígeno de superficie hepatitis B (HBsAg) en gestantes. Valores: 1=Negativo, 2=Positivo. Tipo: TINYINT. Tamizaje perinatal HBV.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO Antígeno de Superficie Hepatitis B en Gestantes: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la factura generada en Indigo Vie ERP por los servicios de laboratorio. Tipo: INT. Vinculación facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de la factura emitida en Indigo Vie para estos servicios. Tipo: VARCHAR(20). Referencia de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Numero de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la Entidad Administradora de Salud (EPS/EAPB) del contrato. Se llena si grupo de atención es sin contrato. Tipo: INT. Identificación entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la Entidad Administradora de salud del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del grupo de atención de VIE ERP asignado al paciente. Tipo: INT. Vinculación de cobertura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del grupo de atencion de la base de datos de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre para anotaciones clínicas, administrativas o técnicas relevantes sobre la orden. Tipo: VARCHAR(2000). Documentación complementaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de la cita en interfaz VIE (ID de tabla AGASISITA). Vincula la orden con la atención. Tipo: CHAR(20). Trazabilidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Consecutivo Cita en Interfaz    Campo que se registra desde vie cuando se factura la cita de Laboratorios que va a contener el ID de la cita es decir de la tabla AGASISITA.    Por medio de este campo voy a tener la relacion de la orden con la cita que esta asociada.      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de realización de la consulta o atención donde se ordenó el laboratorio. Tipo: DATETIME. Contexto de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPFECHACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPFECHACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios/cobertura del paciente en el contrato. Tipo: CHAR(2). Identificación de beneficios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato vigente entre la institución y entidad pagadora. Tipo: CHAR(6). Referencia contractual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad (EPS, ARL, medicina prepagada). Tipo: CHAR(9). FK a INENTIDAD. Identificación pagadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o usuario del operario que recibió/registró la muestra biológica. Tipo: CHAR(20). Responsabilidad de recolección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'USURECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de concurrencia/versión para control de cambios simultáneos (optimistic locking). Tipo: TIMESTAMP. Control de integridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo de resultados o documentación adjunta generada por el laboratorio. Tipo: CHAR(250). Documentación digitalizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recolección física de la muestra biológica del paciente. Tipo: DATETIME. Inicio de proceso analítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Recoleccion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera que indica si existe alerta o evento anómalo en el procesamiento. Valores: 0=Sin alerta, 1=Con alerta. Tipo: BIT. Control de calidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTALELAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o índice de auditoría para seguimiento y control normativo. Tipo: NUMERIC(18). Trazabilidad regulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del servicio en el proceso RIPS. Valores: 1=Solicitado, 2=Muestra recolectada, 3=Resultado entregado, 4=Examen interpretado, 5=Remitido, 6=Anulado, 7=Extramural. Tipo: CHAR(1). Flujo de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de unidades del servicio solicitado. Tipo: INT. Volumen de prestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único CUPS (Catálogo de Procedimientos y Servicios) del examen de laboratorio. Tipo: CHAR(20). FK a INCUPSIPS. Identificación del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de generación de la orden médica de laboratorio. Tipo: DATETIME. Solicitud de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o cédula del profesional de la salud que ordena el laboratorio. Tipo: CHAR(20), MASKED. FK a INPROFSAL. Responsabilidad médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se atiende al paciente (consulta externa, urgencias, hospitalización). Tipo: CHAR(10). Ubicación de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Codigo Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/sede donde se solicita el laboratorio. Tipo: CHAR(10). FK a ADCENATEN. Ubicación institucional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso hospitalario o episodio de atención del paciente. Tipo: CHAR(10). FK a ADINGRESO. Vínculo con admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  el Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación única del paciente (cédula, pasaporte, documento). Tipo: VARCHAR(25), MASKED PII. FK a INPACIENT. Identificador paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico/identificador único de la fila en tabla AMBORDLAB. Clave primaria. Tipo: INT IDENTITY. Integridad referencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
CREATE NONCLUSTERED INDEX [IX_AMBORDLAB_Paciente_CodSer_Est_Fec]
    ON [dbo].[AMBORDLAB]([IPCODPACI] ASC, [CODSERIPS] ASC, [ESTSERIPS] ASC, [FECRECMUE] ASC)
    INCLUDE([AUTO], [NUMINGRES], [CODCENATE], [NOMARCLAB], [ESTALELAB]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de laboratorio ambulatorio: registra cada orden médica de exámenes de laboratorio solicitados para un paciente, incluyendo el estado de la orden, resultados de exámenes específicos (hemoglobina, creatinina, colesterol, microalbuminuria, entre otros), recepción de muestras e integración con facturación y sistemas externos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB';
