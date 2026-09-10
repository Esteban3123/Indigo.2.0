CREATE TABLE [dbo].[ADFURIPSU] (
    [NUMRADANT]                      CHAR (10)                                                                        NULL,
    [RESGLO]                         CHAR (1)                                                                         NULL,
    [NUMFAC]                         CHAR (20)                                                                        NULL,
    [NUMCONREC]                      CHAR (50)                                                                        NULL,
    [CODHABPRE]                      CHAR (12)                                                                        NULL,
    [PRIAPEVIC]                      VARCHAR (50)                                                                     NULL,
    [SEGAPEVIC]                      VARCHAR (50)                                                                     NULL,
    [PRINOMVIC]                      VARCHAR (50)                                                                     NULL,
    [SEGNOMVIC]                      VARCHAR (50)                                                                     NULL,
    [TIPDOCVIC]                      CHAR (2)                                                                         NULL,
    [NUMDOCVIC]                      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "identification_Ofuscado", 0)') NULL,
    [FECNACVIC]                      DATETIME                                                                         NULL,
    [SEXVIC]                         CHAR (1)                                                                         NULL,
    [DIRRESVIC]                      CHAR (40)                                                                        NULL,
    [CODDEPVIC]                      CHAR (2)                                                                         NULL,
    [CODMUNVIC]                      CHAR (3)                                                                         NULL,
    [TELVIC]                         CHAR (10)                                                                        NULL,
    [CONVIC]                         CHAR (1)                                                                         NULL,
    [NATEVE]                         CHAR (2)                                                                         NULL,
    [DESEVE]                         VARCHAR (50)                                                                     NULL,
    [DIROCUEVE]                      VARCHAR (250)                                                                    NULL,
    [FECOCUEVE]                      DATETIME                                                                         NULL,
    [HOROCUEVE]                      CHAR (5)                                                                         NULL,
    [CODDEPEVE]                      CHAR (2)                                                                         NULL,
    [CODMUNEVE]                      CHAR (3)                                                                         NULL,
    [ZONOCUEVE]                      CHAR (1)                                                                         NULL,
    [ESTASE]                         CHAR (1)                                                                         NULL,
    [MARCA]                          CHAR (15)                                                                        NULL,
    [PLAVEHACC]                      CHAR (6) MASKED WITH (FUNCTION = 'partial(0, "LicensePlate_Ofuscado", 0)')       NULL,
    [TIPVEH]                         CHAR (2)                                                                         NULL,
    [CODASE]                         CHAR (12) MASKED WITH (FUNCTION = 'partial(0, "InsuranceCode_Ofuscado", 0)')     NULL,
    [NUMSOA]                         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Soat_Ofuscado", 0)')              NULL,
    [FECINIPOL]                      DATETIME                                                                         NULL,
    [FECFINPOL]                      DATETIME                                                                         NULL,
    [INTAUT]                         CHAR (1)                                                                         NULL,
    [COBEXCPOL]                      CHAR (1)                                                                         NULL,
    [PLASEGVEH]                      CHAR (6) MASKED WITH (FUNCTION = 'partial(0, "LicensePlate_Ofuscado", 0)')       NULL,
    [TIPDOCSEGINV]                   CHAR (2)                                                                         NULL,
    [NUMDOCSEGINV]                   CHAR (16) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [PLATERVEH]                      CHAR (6) MASKED WITH (FUNCTION = 'partial(0, "LicensePlate_Ofuscado", 0)')       NULL,
    [TIPDOCTERINV]                   CHAR (2)                                                                         NULL,
    [NUMDOCTERINV]                   CHAR (16) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [TIPDOCPRO]                      CHAR (2)                                                                         NULL,
    [NUMDOCPRO]                      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [PRIAPEPRO]                      CHAR (40) MASKED WITH (FUNCTION = 'partial(0, "FisrtSurname_Ofuscado", 0)')      NULL,
    [SEGAPEPRO]                      CHAR (30) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NULL,
    [PRINOMPRO]                      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NULL,
    [SEGNOMPRO]                      CHAR (30) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NULL,
    [DIRRESPRO]                      CHAR (40) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [TELRESPRO]                      CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')             NULL,
    [CODDEPPRO]                      CHAR (2)                                                                         NULL,
    [CODMUNPRO]                      CHAR (3)                                                                         NULL,
    [PRIAPECON]                      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NULL,
    [SEGAPECON]                      CHAR (30)                                                                        NULL,
    [PRINOMCON]                      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NULL,
    [SEGNOMCON]                      CHAR (30)                                                                        NULL,
    [TIPDOCCON]                      CHAR (2)                                                                         NULL,
    [NUMDOCCON]                      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [DIRRESCON]                      CHAR (40) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [CODDEPCON]                      CHAR (2)                                                                         NULL,
    [CODMUNCON]                      CHAR (3)                                                                         NULL,
    [TELRESCON]                      CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')             NULL,
    [TIPREF]                         CHAR (1)                                                                         NULL,
    [FECREM]                         DATETIME                                                                         NULL,
    [HORSAL]                         CHAR (5)                                                                         NULL,
    [CODHABLENV]                     CHAR (12)                                                                        NULL,
    [PROREM]                         CHAR (60)                                                                        NULL,
    [CARPERREM]                      CHAR (30)                                                                        NULL,
    [FECING]                         DATETIME                                                                         NULL,
    [HORING]                         CHAR (5)                                                                         NULL,
    [CODHABREC]                      CHAR (12)                                                                        NULL,
    [PROREC]                         CHAR (60)                                                                        NULL,
    [CARPERREC]                      CHAR (30)                                                                        NULL,
    [PLATRAVIC]                      CHAR (6)                                                                         NULL,
    [TRASITEVE]                      VARCHAR (50)                                                                     NULL,
    [TRAFINREC]                      VARCHAR (50)                                                                     NULL,
    [TIPSERAMB]                      CHAR (1)                                                                         NULL,
    [ZONRECVIC]                      CHAR (1)                                                                         NULL,
    [FECDEING]                       DATETIME                                                                         NULL,
    [HORDEING]                       CHAR (5)                                                                         NULL,
    [FECEGR]                         DATETIME                                                                         NULL,
    [HOREGR]                         CHAR (5)                                                                         NULL,
    [CODDIAING]                      CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [CODINGAS1]                      CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [CODINGAS2]                      CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [CODDIAEGR]                      CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [CODEGRAS1]                      CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [CODEGRAS2]                      CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [PRIAPEMED]                      VARCHAR (50)                                                                     NOT NULL,
    [SEGAPEMED]                      VARCHAR (50)                                                                     NULL,
    [PRINOMMED]                      VARCHAR (50)                                                                     NULL,
    [SEGNOMMED]                      VARCHAR (50)                                                                     NULL,
    [TIPDOCMED]                      CHAR (2)                                                                         NULL,
    [NUMDOCMED]                      CHAR (16)                                                                        NULL,
    [NUMREGMED]                      CHAR (16)                                                                        NULL,
    [FACGASMED]                      CHAR (20)                                                                        NULL,
    [RECGASMED]                      CHAR (15)                                                                        NULL,
    [FACGASTRA]                      CHAR (20)                                                                        NULL,
    [RECGASTRA]                      CHAR (15)                                                                        NULL,
    [TOTFOL]                         CHAR (3)                                                                         NULL,
    [NUMINGRES]                      CHAR (10)                                                                        NOT NULL,
    [DESEVEACC]                      VARCHAR (8000)                                                                   NULL,
    [USUCRE]                         CHAR (15)                                                                        NULL,
    [FECCRE]                         DATETIME                                                                         NULL,
    [USUMOD]                         CHAR (20)                                                                        NULL,
    [FECMOD]                         DATETIME                                                                         NULL,
    [FECRADFUR]                      DATETIME                                                                         NULL,
    [ESCONFIRM]                      BIT                                                                              NOT NULL,
    [NUMEFOLIO]                      NVARCHAR (10)                                                                    NULL,
    [IPCODPACI]                      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [PREREMCODIPS]                   CHAR (100)                                                                       NULL,
    [PRERECICODIPS]                  CHAR (100)                                                                       NULL,
    [Id]                             INT                                                                              IDENTITY (1, 1) NOT NULL,
    [AmbulancePlate]                 VARCHAR (6)                                                                      NULL,
    [ServiceUCI]                     BIT                                                                              NULL,
    [DaysStayUCI]                    INT                                                                              NULL,
    [ComplexitySurgicalProcedure]    INT                                                                              NULL,
    [FiledSIRAS]                     VARCHAR (20)                                                                     NULL,
    [ManifestationService]           BIT                                                                              NULL,
    [MainHospitalizationServiceCode] CHAR (20)                                                                        NULL,
    [MainSurgicalService]            CHAR (20)                                                                        NULL,
    [SecondarySurgicalProcedure]     CHAR (20)                                                                        NULL,
    [EntityToClaim]                  INT                                                                              NULL,
    [IdInvoice]                      INT                                                                              NULL,
    [NoOwnerInformation]             BIT                                                                              NULL,
    [NoDriverInformation]            BIT                                                                              NULL,
    CONSTRAINT [PK_ADFURIPSU] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ADFURIPSU_ADCONTIPS] FOREIGN KEY ([PRERECICODIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_ADFURIPSU_ADCONTIPS1] FOREIGN KEY ([PREREMCODIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_ADFURIPSU_Billing.Invoice] FOREIGN KEY ([IdInvoice]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_ADFURIPSU_INCUPSIPS] FOREIGN KEY ([MainHospitalizationServiceCode]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ADFURIPSU_INCUPSIPS1] FOREIGN KEY ([MainSurgicalService]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ADFURIPSU_INCUPSIPS2] FOREIGN KEY ([SecondarySurgicalProcedure]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ADFURIPSU_INPACIENT] FOREIGN KEY ([NUMDOCVIC]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[NUMDOCVIC]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[PLAVEHACC]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[CODASE]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[NUMSOA]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[PLASEGVEH]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[NUMDOCSEGINV]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[PLATERVEH]
    WITH (LABEL = 'Confidential', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[NUMDOCTERINV]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[NUMDOCPRO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[PRIAPEPRO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[SEGAPEPRO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[PRINOMPRO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[SEGNOMPRO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[DIRRESPRO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[TELRESPRO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[PRIAPECON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[PRINOMCON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[NUMDOCCON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[DIRRESCON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[TELRESCON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[CODDIAING]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[CODINGAS1]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[CODINGAS2]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[CODDIAEGR]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[CODEGRAS1]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[CODEGRAS2]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURIPSU].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_ADFURIPSU_NUMINGRES_NUMFAC]
    ON [dbo].[ADFURIPSU]([NUMINGRES] ASC)
    INCLUDE([NUMFAC]);


GO
CREATE NONCLUSTERED INDEX [IX_ADFURIPSU__NUMDOCVIC]
    ON [dbo].[ADFURIPSU]([NUMDOCVIC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sin informacion conductor : en esta columna se almacenara true o false ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NoDriverInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'sin informacion del propietario: en este campo se guarda true o false para el check ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NoOwnerInformation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'en esta columna se almacena el id de la factura desde liquidacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'IdInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'entidad a reclamar: en esta columna se almacenara los valores 1 : adress 2: aseguradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'EntityToClaim';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cups Procedimiento Quirúrgico Secundario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SecondarySurgicalProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cups Servicio Quirúrgico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'MainSurgicalService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cups Servicio Principal de Hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'MainHospitalizationServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'servicios habilitados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'ManifestationService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo del radicado siras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FiledSIRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'complejidad quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'ComplexitySurgicalProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda los dias estancia uci', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'DaysStayUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'servicio uci', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'ServiceUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo para especificar la placa de la ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'AmbulancePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el cosecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prestador que Remite se llena Solo si la tipo de referencia es de Remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRERECICODIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prestador que Remite se llena Solo si la tipo de referencia es de Remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PREREMCODIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'ESCONFIRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Radicacion del FURIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECRADFUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'USUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'USUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BREBE DESCRIPCION DEL EVENTO. NO VA EN LA INFORMACION DEL ARCHIVO PLANO PERO SI SE TIENE QUE MOSTRAR EL REPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'DESEVEACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TOTAL FOLIOS:  CAMPO OBLIGATORIO.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TOTFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TOTAL RECLAMADO POR AMPARO DE GASTOS DE TRANSPORTE Y MOVILIZACIÓN DE LA VICTIMA:  CAMPO OBLIGATORIO.  NO UTILIZAR NINGÚN TIPO DE SEPARADOR DE MILES Y NO DEBE INCLUIR DECIMALES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'RECGASTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TOTAL FACTURADO POR AMPARO DE GASTOS DE TRANSPORTE Y MOVILIZACIÓN DE LA VÍCTIMA:  CAMPO OBLIGATORIO.  NO UTILIZAR NINGÚN TIPO DE SEPARADOR DE MILES Y NO DEBE INCLUIR DECIMALES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FACGASTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TOTAL RECLAMADO POR AMPARO DE GASTOS MÉDICOS QUIRÚRGICOS:  CAMPO OBLIGATORIO.  NO UTILIZAR NINGÚN TIPO DE SEPARADOR DE MILES Y NO DEBE INCLUIR DECIMALES. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'RECGASMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TOTAL FACTURADO POR AMPARO DE GASTOS MÉDICOS QUIRÚRGICOS:  CAMPO OBLIGATORIO.  NO UTILIZAR NINGÚN TIPO DE SEPARADOR DE MILES Y NO DEBE INCLUIR DECIMALES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FACGASMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE REGISTRO DEL MÉDICO: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMREGMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE DOCUMENTO DE IDENTIDAD DEL MÉDICO O PROFESIONAL DE LA SALUD: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMDOCMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE DOCUMENTO DE IDENTIDAD DEL MÉDICO O PROFESIONAL DE LA SALUD:  CAMPO OBLIGATORIO.  CC= CÉDULA DE CIUDADANÍA  CE= CÉDULA DE EXTRANJERIA   PA= PASAPORTE ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPDOCMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO NOMBRE DEL MÉDICO O PROFESIONAL DE LA SALUD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGNOMMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER NOMBRE DEL MÉDICO O PROFESIONAL DE LA SALUD:  CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRINOMMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO APELLIDO DEL MÉDICO O PROFESIONAL DE LA SALUD.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGAPEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER APELLIDO DEL MÉDICO O PROFESIONAL DE LA SALUD:   CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRIAPEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE DIAGNOSTICO DE EGRESO ASOCIADO 2: CAMPO OBLIGATORIO.  CÓDIGO DEL DIAGNÓSTICO AL INGRESO DE LA VICTIMA, SEGÚN LA CLASIFICACIÓN INTERNACIONAL DE ENFERMEDADES VIGENTE. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODEGRAS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE DIAGNOSTICO DE EGRESO ASOCIADO 1: CAMPO OBLIGATORIO.  CÓDIGO DEL DIAGNÓSTICO AL INGRESO DE LA VICTIMA, SEGÚN LA CLASIFICACIÓN INTERNACIONAL DE ENFERMEDADES VIGENTE.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODEGRAS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DIAGNÓSTICO PRINCIPAL DE EGRESO: CAMPO OBLIGATORIO.  CÓDIGO DEL DIAGNÓSTICO AL INGRESO DE LA VICTIMA, SEGÚN LA CLASIFICACIÓN INTERNACIONAL DE ENFERMEDADES VIGENTE. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODDIAEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE DIAGNÓSTICO DE INGRESO ASOCIADO 2.  CÓDIGO DEL DIAGNÓSTICO AL INGRESO DE LA VICTIMA, SEGÚN LA CLASIFICACIÓN INTERNACIONAL DE ENFERMEDADES VIGENTE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODINGAS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE DIAGNÓSTICO DE INGRESO ASOCIADO 1.  CÓDIGO DEL DIAGNÓSTICO AL INGRESO DE LA VICTIMA, SEGÚN LA CLASIFICACIÓN INTERNACIONAL DE ENFERMEDADES VIGENTE.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODINGAS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE DIAGNÓSTICO PRINCIPAL DE INGRESO: CAMPO OBLIGATORIO.  CÓDIGO DEL DIAGNÓSTICO AL INGRESO DE LA VICTIMA, SEGÚN LA CLASIFICACIÓN INTERNACIONAL DE ENFERMEDADES VIGENTE.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODDIAING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HORA DE EGRESO: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'HOREGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE EGRESO: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HORA DE INGRESO: CAMPO OBLIGATORIO. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'HORDEING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE INGRESO: CAMPO OBLIGATORIO. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECDEING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ZONA DONDE RECOGE VÍCTIMA: CAMPO OBLIGATORIO.  U= URBANA  R= RURAL  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'ZONRECVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE SERVICIO DE LA AMBULANCIA: CAMPO OBLIGATORIO, DE ACUERDO AL ANEXO TÉCNICO NO. 1 DE LA RESOLUCIÓN 1439 DE 2002.  1= AMBULANCIA BÁSICA  2= AMBULANCIA MEDICALIZADA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPSERAMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRANSPORTE DE LA VÍCTIMA HASTA EL FIN DEL RECORRIDO: CAMPO OBLIGATORIO.  ESCRIBIR CON CLARIDAD EL LUGAR O DIRECCIÓN FINAL DE RECORRIDO. SOLO SE ADMITE LUGAR CUANDO EN EL SITIO NO EXISTE NOMENCLATURA. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TRAFINREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRANSPORTE DE LA VICTIMA DESDE EL SITIO DEL EVENTO: CAMPO OBLIGATORIO.  ESCRIBIR CON CLARIDAD EL LUGAR O DIRECCIÓN INICIAL DEL RECORRIDO.  SOLO SE ADMITE LUGAR CUANDO EN EL SITIO NO EXISTE NOMENCLATURA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TRASITEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PLACA: CAMPO OBLIGATORIO.  NÚMERO DE PLACA DEL VEHÍCULO QUE TRANSPORTA LA VICTIMA.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PLATRAVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CARGO DE LA PERSONA QUE RECIBE: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CARPERREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PROFESIONAL QUE RECIBE: CAMPO OBLIGATORIO. NOMBRE Y APELLIDO DE LA PERSONA QUE REALIZA LA REMISIÓN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PROREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE HABILITACIÓN DEL PRESTADOR DE SERVICIOS DE SALUD:  CAMPO OBLIGATORIO.  EL CÓDIGO DE HABILITACIÓN ASIGNADO POR LA DIRECCIÓN DEPARTAMENTAL DE SALUD.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODHABREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HORA DE INGRESO: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'HORING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE INGRESO: CAMPO OBLIGATORIO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CARGO DE LA PERSONA QUE REMITE: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CARPERREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PROFESIONAL QUE REMITE: CAMPO OBLIGATORIO. NOMBRE Y APELLIDO DE LA PERSONA QUE REALIZA LA REMISIÓN. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PROREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE HABILITACIÓN DEL PRESTADOR DE SERVICIOS DE SALUD:   CAMPO OBLIGATORIO.  EL CÓDIGO DE HABILITACIÓN ASIGNADO POR LA DIRECCIÓN DEPARTAMENTAL DE SALUD. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODHABLENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HORA DE SALIDA: CAMPO OBLIGATORIO. HORA DE SALIDA DE LA IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'HORSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE REMISÓN: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE REFERENCIA: CAMPO OBLIGATORIO.   1= REMISIÓN   2= ORDEN DE SERVICIO  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPREF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TELÉFONO DE RESIDENCIA DEL CONDUCTOR: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TELRESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL MUNICIPIO DE RESIDENCIA DEL CONDUCTOR:  CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODMUNCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL DEPARTAMENTO DE RESIDENCIA DEL CONDUCTOR:   CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODDEPCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DIRECCIÓN DE RESIDENCIA DEL CONDUCTOR: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'DIRRESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE DOCUMENTO DE IDENTIDAD DEL CONDUCTOR:   CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.  REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD O EN LA LICENCIA DE CONDUCCIÓN.    PARA EL TIPO AS VER ESPECIFICACIÓN RESOLUCIÓN 812 DE 2007, PARA LOS CASOS EN QUE NO PRESENTE IDENTIFICACIÓN SE DEBE DILIGENCIAR:      DEPARTAMENTO SEGÚN CODIFICACIÓN DANE + MUNICIPIO SEGÚN CODIFICACIÓN DANE + NN + NÚMERO DE HISTORIA CLÍNICA  (ALFANUMERICO DE 9).    EJEMPLO: 05001NN890123456   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMDOCCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE DOCUMENTO DE IDENTIDAD DEL CONDUCTOR: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.    CC = CÉDULA DE CIUDADANÍA.  CE = CÉDULA DE EXTRANJERIA.  PA = PASAPORTE.  TI = TARJETA DE IDENTIDAD.    AS = ADULTO SIN IDENTIFICAR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPDOCCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO NOMBRE DEL CONDUCTOR: REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD O EN LA LICENCIA DE CONDUCCIÓN. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGNOMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER NOMBRE DEL CONDUCTOR: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.    REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD O EN LA LICENCIA DE CONDUCCIÓN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRINOMCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO APELLIDO DEL CONDUTOR: REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD O EN LA LICENCIA DE CONDUCCIÓN. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGAPECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER APELLIDO DEL CONDUCTOR: CAMPO OBLIGATORIO. SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4, REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD O EN LA LICENCIA DE CONDUCCIÓN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRIAPECON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL MUNICIPIO DE RESIDENCIA DEL PROPIETARIO:  CAMPO OBLIGATORIO, SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODMUNPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL DEPARTAMENTO DE RESIDENCIA DEL PROPIETARIO:  CAMPO OBLIGATORIO, SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODDEPPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TELÉFONO DE RESIDENCIA DEL PROPIETARIO: CAMPO OBLIGATORIO, SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TELRESPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DIRECCIÓN DE RESIDENCIA DEL PROPIETARIO: CAMPO OBLIGATORIO, SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'DIRRESPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO NOMBRE DEL PROPIETARIO: REGISTRAR COMO APARECE EN LA TARJETA DE PROPIEDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGNOMPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER NOMBRE DEL PROPIETARIO: CAMPO OBLIGATORIO. PARA EL CASO DE PERSONA NATURAL, SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4, REGISTRAR COMO APARECE EN LA TARJETA DE PROPIEDAD.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRINOMPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO APELLIDO DEL PROPIETARIO: REGISTRAR COMO APARECE EN LA TARJETA DE PROPIEDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGAPEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER APELLIDO DEL PROPIETARIO O RAZÓN  SOCIAL EN CASO DE EMPRESA: CAMPO OBLIGATORIO. SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4, REGISTRAR COMO APARECE EN LA TARJETA DE PROPIEDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRIAPEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE DOCUMENTO DE IDENTIDAD DEL PROPIETARIO: CAMPO OBLIGATORIO. REGISTRAR COMO APARECE EN LA TARJETA DE PROPIEDAD. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMDOCPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE DOCUMENTO DE IDENTIDAD DEL PROPIETARIO: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.  CC = CÉDULA DE CIUDADANÍA.  CE = CÉDULA DE EXTRANJERIA.  PA = PASAPORTE.  TI = TARJETA DE IDENTIDAD  RC = REGISTRO CIVIL  NI = NÚMERO DE IDENTIFICACIÓN TRIBUTARIA   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPDOCPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE DOCUMENTO DE IDENTIDAD DEL PROPIETARIO DEL TERCER VEHÍCULO INVOLUCRADO: SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4, REGISTRAR COMO APARECE EN LA TARJETA DE PROPIEDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMDOCTERINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE DOCUMENTO DE IDENTIDAD DEL PROPIETARIO DE TERCER VEHÍCULO INVOLUCRADO: SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4  CC = CÉDULA DE CIUDADANÍA.  CE = CÉDULA DE EXTRANJERIA.  PA = PASAPORTE.  TI = TARJETA DE IDENTIDAD  RC = REGISTRO CIVIL  NI = NÚMERO DE IDENTIFICACIÓN TRIBUTARIA   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPDOCTERINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PLACA DEL TERCER VEHÍCULO INVOLUCRADO: SI EL ESTADO DE ASEGURAMIENTO ES 1,2, 4 O 5, SI NO EXISTE TERCER VEHÍCULO ES UN CAMPO VACÍO. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PLATERVEH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE DOCUMENTO DE IDENTIDAD DEL PROPIETARIO DEL SEGUNDO VEHICULO INVOLUCRADO: SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4, REGISTRAR COMO APARECE EN LA TARJETA DE PROPIEDAD. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMDOCSEGINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE DOCUMENTO DE IDENTIDAD DEL PROPIETARIO DEL SEGUNDO VEHÍCULO INVOLUCRADO: SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.  CC = CÉDULA DE CIUDADANÍA.  CE = CÉDULA DE EXTRANJERIA.  PA = PASAPORTE.  TI = TARJETA DE IDENTIDAD  RC = REGISTRO CIVIL  NI = NÚMERO DE IDENTIFICACIÓN TRIBUTARIA   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPDOCSEGINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PLACA DEL SEGUNDO VEHÍCULO INVOLUCRADO: SI EL ESTADO DE ASEGURAMIENTO ES 1,2, 4 O 5, SI NO EXISTE SEGUNDO VEHÍCULO ES UN CAMPO VACÍO. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PLASEGVEH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COBRO POR EXCEDENTE DE LA PÓLIZA: SE DEBE ESPECIFICAR SI LA RECLAMACIÓN CORRESPONDE A UN COBRO DE EXCEDENTES DE GASTOS MÉDICOS UNA VEZ SUPERADO LOS TOPES DE COBERTURA RECONOCIDOS POR LAS ASEGURADORAS.  0 = NO  1 = SI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'COBEXCPOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'INTERVENCIÓN DE LA AUTORIDAD: CAMPO OBLIGATORIO.  SE IDENTIFICA SI HACE PRESENCIA O NO UNA AUTORIDAD COMPETENTE.  0= NO  1= SI ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'INTAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA FINAL DE VIGENCIA DE LA POLIZA: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1 O 4. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECFINPOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE INICIO DE VIGENCIA DE LA PÓLIZA: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1 O 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECINIPOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE POLIZA SOAT: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1 O 4.  COMO SE REGISTRA EN LA PÓLIZA SOAT ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMSOA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DE LA ASEGURADORA: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1 O 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE VEHÍCULO: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2, 4 O 5.    DE ACUERDO CON LAS DEFINICIONES DEL CÓDIGO NACIONAL DE TRÁNSITO TERRESTRE (LEY 769 DE 2002)    3 = PARTICULAR  4 = PÚBLICO  5 = OFICIAL  6 = DE EMERGENCIA  7 = DIPLOMÁTICO O CONSULAR  8 = TRANSPORTE MASIVO  9 = ESCOLAR    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPVEH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PLACA: CAMPO OBLIGATORIO, SI EL ESTADO DE ASEGURAMIENTO ES 1,2, 4 O 5.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PLAVEHACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MARCA: CAMPO OBLIGATORIO SI EL ESTADO DE ASEGURAMIENTO ES 1,2 O 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'MARCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ESTADO DE ASEGURAMIENTO: CAMPO OBLIGATORIO.   1=ASEGURADO  2=NO ASEGURADO  3=VEHÍCULO FANTASMA  4=PÓLIZA FALSA  5=VEHÍCULO EN FUGA ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'ESTASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ZONA DE OCURRENCIA DEL EVENTO: CAMPO OBLIGATORIO.  U= URBANA  R= RURAL ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'ZONOCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL MUNICIPIO DE OCURRENCIA DEL EVENTO: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODMUNEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL DEPARTAMENTO DE OCURRENCIA DEL EVENTO: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODDEPEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HORA DE OCURRENCIA DEL EVENTO: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'HOROCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE OCURRENCIA DEL EVENTO: CAMPO OBLIGATORIO. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECOCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DIRECCIÓN DE OCURRENCIA DEL EVENTO: CAMPO OBLIGATORIO. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'DIROCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DESCRIPCIÓN DEL OTRO EVENTO: CAMPO OBLIGATORIO SI LA NATURALEZA DEL EVENTO ES OTRO (17)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'DESEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NATURALEZA DEL EVENTO: CAMPO OBLIGATORIO.  INDICA LA NATURALEZA QUE GENERA EL EVENTO.  01=ACCIDENTE DE TRÁNSITO  02=SISMO  03=MAREMOTO  04=ERUPCIÓN VOLCÁNICA  05=DESLIZAMIENTO DE TIERRA  06=INUNDACIÓN  07=AVALANCHA  08=INCENDIO NATURAL  09=EXPLOSIÓN TERRORISTA  10=INCENDIO TERRORISTA   11=COMBATE  12=ATAQUES A MUNICIPIOS  13=MASACRE  14=DESPLAZADOS  15=MINA ANTIPERSONAL   16=HURACÁN  17=OTRO  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NATEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CONDICIÓN DE LA VÍCTIMA: CAMPO OBLIGATORIO PARA EL TIPO DE EVENTO 01 ACCIDENTES DE TRÁNSITO:  1=CONDUCTOR  2=PEATÓN  3=OCUPANTE  4=CICLISTA  5=Envetos catastroficos   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CONVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TELÉFONO DE LA VÍCTIMA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TELVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL MUNICIPIO DE RESIDENCIA DE LA VÍCTIMA: CAMPO OBLIGATORIO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODMUNVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO DEL DEPARTAMENTO DE RESIDENCIA DE LA VÍCTIMA: CAMPO OBLIGATORIO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODDEPVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DIRECCIÓN DE RESIDENCIA DE LA VÍCTIMA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'DIRRESVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEXO DE LA VÍCTIMA: CAMPO OBLIGATORIO.   F=FEMENINO  M=MASCULINO ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEXVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE NACIMIENTO DE LA VÍCTIMA: CAMPO OBLIGATORIO.   CORRESPONDE A LA FECHA DE NACIMIENTO DE LA VÍCTIMA.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'FECNACVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NÚMERO DE DOCUMENTO DE IDENTIDAD DE LA VÍCTIMA: CAMPO OBLIGATORIO.  CORRESPONDE AL NUMERO DE IDENTIFICACIÓN DE LA VICTIMA.    PARA LAS VÍCTIMAS CON IDENTIFICACIÓN MS O AS DEBE APLICARSE LAS ESPECIFICACIONES DE LA RESOLUCIÓN 812 DEL 21 DE MARZO DE 2007.    PARA LOS CASOS EN QUE NO PRESENTE IDENTIFICACIÓN SE DEBE DILIGENCIAR:  DEPARTAMENTO SEGÚN CODIFICACIÓN DANE + MUNICIPIO SEGÚN CODIFICACIÓN DANE + NN + NÚMERO DE HISTORIA CLÍNICA   (ALFANUMÉRICO DE 9).  EJEMPLO: 05001NN890123456    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE DOCUMENTO DE IDENTIDAD DE LA VÍCTIMA: CAMPO OBLIGATORIO.    CC=CÉDULA DE CIUDADANIA.   CE=CÉDULA DE EXTRANJERÍA.  PA=PASAPORTE.  TI=TARJETA DE IDENTIDAD.  RC=REGISTRO CIVIL.  AS=ADULTO SIN IDENTIFICAR.  MS=MENOR SIN IDENTIFICAR.      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO NOMBRE DE LA VÍCTIMA: REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER NOMBRE DE LA VÍCTIMA: CAMPO OBLIGATORIO.   REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD, EN EL CASO DE NO IDENTIFICARSE USAR NN  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SEGUNDO APELLIDO DE LA VÍCTIMA: REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRIMER APELLIDO DE LA VÍCTIMA: CAMPO OBLIGATORIO.  REGISTRAR COMO APARECE EN EL DOCUMENTO DE IDENTIDAD. EN EL CASO DE NO IDENTIFICARSE USAR NN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CODIGO DE HABILITACION DEL PRESTADOR DEL SERVICIO DE SALUD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'CODHABPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NUMERO CONSECUTIVO DE LA RECLAMACION: CAMPO OBLIGATORIO. CORRESPONDE AL NUMERO CONSECUTIVO DE LA RECLAMACIO ESTABLECIDO POR LA ENTIDAD RECLAMANTE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMCONREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta columa queda obsoleta desde 11/08/2023, la cual solo se almacenara NULL, por cambios solicitados en el PBI 10947', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RG:RESPUESTA A GLOSAS. DILIGENCIAR EN LOS SIGUIENTES CASOS 0= GLOSAS TOTAL Y 1= PAGO PARCIAL. SI LA RECLAMACION ES NUEVA EL CAMPO ES VACIO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'RESGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NUMERO DE RADICADO ANTERIOR: CAMPO OBLIGATORIO EN CASO DE DILIGIGENCIARSE  RG (RESPUESTA A GLOSAS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU', @level2type = N'COLUMN', @level2name = N'NUMRADANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del formulario FURIPS (Informe de Atención de Urgencias por Accidente de Tránsito) para RIPS. Contiene la información de la víctima, el evento del accidente, los vehículos involucrados, el propietario y conductor, datos del traslado, diagnósticos de ingreso y egreso, y datos del médico tratante, asociados a un número de ingreso y factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURIPSU';

GO
CREATE NONCLUSTERED INDEX [IX_ADFURIPSU_NUMINGRES]
    ON [dbo].[ADFURIPSU]([NUMINGRES] ASC)
    INCLUDE([NUMFAC], [NUMDOCVIC], [NUMSOA]);
