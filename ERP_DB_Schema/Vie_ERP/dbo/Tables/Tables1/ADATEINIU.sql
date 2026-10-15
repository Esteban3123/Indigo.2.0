CREATE TABLE [dbo].[ADATEINIU] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINFORM] CHAR (4)                                                                         NOT NULL,
    [FECINFORM] DATETIME                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [CODENTIDA] CHAR (9)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODORIATE] CHAR (2)                                                                         NOT NULL,
    [FECINGURG] DATETIME                                                                         NOT NULL,
    [CODCLATRI] CHAR (1)                                                                         NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [MOTCONATE] CHAR (1000)                                                                      NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [CODDESPAC] CHAR (1)                                                                         NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [ESTTRANSA] CHAR (1)                                                                         NOT NULL,
    [DESFINENV] CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADATEINIU] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADATEINIU_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADATEINIU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADATEINIU_INENTIDAD] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTIDAD] ([CODENTIDA]),
    CONSTRAINT [FK_ADATEINIU_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADATEINIU] NOCHECK CONSTRAINT [FK_ADATEINIU_ADCENATEN];


GO
ALTER TABLE [dbo].[ADATEINIU] NOCHECK CONSTRAINT [FK_ADATEINIU_ADINGRESO];


GO
ALTER TABLE [dbo].[ADATEINIU] NOCHECK CONSTRAINT [FK_ADATEINIU_INENTIDAD];


GO
ALTER TABLE [dbo].[ADATEINIU] NOCHECK CONSTRAINT [FK_ADATEINIU_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADATEINIU_FECINFORM]
    ON [dbo].[ADATEINIU]([FECINFORM] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_AIU]
    ON [dbo].[ADATEINIU]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el destino de envio final:  1: a la EAPB  2: a la DTL  3: a la DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'DESFINENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Transaccion:  1: En Proceso de Envio  2: Envio 1 Fallido  3: Envio 2 Fallido  4: Envio 3 Fallido  5: Envio DTL Fallido  6: Envio DTD Fallido  7: Envio Satisfactorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Destino del Paciente - Homologado con Destino de Historias  1: Domicilio          2: Observación     3: Internación      4: Remisión          5: Contrarremisión  6: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODDESPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio de Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de Consulta - Se Extrae los primeros 200 Caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'MOTCONATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    Nota: Codigo 00000001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Nivel de TRIAGE  1: Rojo   2: Amarillo  3: Verde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCLATRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINGURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Origen de la Atencion - Homologado al momento de crear el Ingreso.  01: Accidente de trabajo              02: Accidente de tránsito             06: Evento catastrófico           13: Enfermedad general                14: Enfermedad profesional             16: Accidente de trabajo y Accidente de Transito        17 = Evento catastrófico y Accidente de trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODORIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Atencion Inicial de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'FECINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Atencion Inicial de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADATEINIU', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

