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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Consecutivo Cita en Interfaz    Campo que se registra desde vie cuando se factura la cita de Laboratorios que va a contener el ID de la cita es decir de la tabla AGASISITA.    Por medio de este campo voy a tener la relacion de la orden con la cita que esta asociada.      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMCONCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo Unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda  el Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDLAB', @level2type = N'COLUMN', @level2name = N'AUTO';

