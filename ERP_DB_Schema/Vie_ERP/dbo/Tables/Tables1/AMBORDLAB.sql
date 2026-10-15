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
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [Fk_ADINGRESO_NUMINGRES_AMBORDLAB];


GO
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [FK_AMBORDLAB_INENTIDAD];


GO
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [FK_AMBORDLAB_INPROFSAL];


GO
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [FK_AMBORDLABO_ADCENATEN1];


GO
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [FK_AMBORDLABO_INCUPSIPS1];


GO
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [FK_AMBORDLABO_INPACIENT];


GO
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [FK_IdServerOrderDetail_AMBORDLAB];


GO
ALTER TABLE [dbo].[AMBORDLAB] NOCHECK CONSTRAINT [Fk_INPACIENT_IPCODPACI_AMBORDLAB];


GO
CREATE NONCLUSTERED INDEX [UX_AMBORLAB_CODSERIPS_SYNCMIRTH]
    ON [dbo].[AMBORDLAB]([CODSERIPS] ASC, [SYNCMIRTH] ASC)
    INCLUDE([IPCODPACI], [CODPROSAL], [ESTSERIPS]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de Imagenologia, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el laboratoio es de microbiologia:  lo envia la interfaz y si es microbiologia es porque recibe resultados   preliminares.  0 - No es microbiologia  1 - Si es microbiologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'observacion de la muestra no conforme', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'codigo del motivo de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'espere resultado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESPERESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'profesional queda resutado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'PROFRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de folio de interpretación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la interpretación del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado  Fosforo (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Fosforo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Albumina Serica (Longitud 5) (Unidades: g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Albumina Serica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado LDL (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha LDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de Colesterol Total (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Colesterol Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la Creatinuria (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creatinuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado HDL  (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE Baciloscopia de Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Baciloscopia de Diagnóstico: 1. Negativa, 2. Positiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE RESULTADO DE HDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Hemoglobina Glicosilada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina Glicosilada: Valor mínimo 5 y máximo 20, permitir decimales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO DE GLISEMIA BASAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina: Valor mínimo 1.5 y máximo 20', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TSH Neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TSH Neonatal: 1. Normal, 2. Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA RESULTADO Serología para Sífilis:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO Serología para Sífilis: 1. No Reactiva, 2. Reactiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA RESULTADO Antígeno de Superficie Hepatitis B en Gestantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO Antígeno de Superficie Hepatitis B en Gestantes: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Numero de la factura con la que se facturo en Indigo Vie', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENINVOICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la Entidad Administradora de salud del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del grupo de atencion de la base de datos de VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Consecutivo Cita en Interfaz    Campo que se registra desde vie cuando se factura la cita de Laboratorios que va a contener el ID de la cita es decir de la tabla AGASISITA.    Por medio de este campo voy a tener la relacion de la orden con la cita que esta asociada.      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPFECHACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Plan de Beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'USURECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Recoleccion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTALELAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  el Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'AUTO';

