CREATE TABLE [dbo].[ADINCBASD] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINFORM] CHAR (4)                                                                         NOT NULL,
    [FECINFORM] DATETIME                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [CODENTIDA] CHAR (9)                                                                         NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [TIPINCBAS] CHAR (1)                                                                         NOT NULL,
    [IPTIPODOC] INT                                                                              NOT NULL,
    [CODIGONIT] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')            NOT NULL,
    [IPPRIAPEL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NOT NULL,
    [IPSEGAPEL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NOT NULL,
    [IPPRINOMB] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NOT NULL,
    [IPSEGNOMB] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NOT NULL,
    [IPFECNACI] DATETIME MASKED WITH (FUNCTION = 'default()')                                    NOT NULL,
    [IPTIPODCO] INT                                                                              NULL,
    [CODIGONCO] VARCHAR (25)                                                                     NULL,
    [IPPRIAPCO] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NULL,
    [IPSEGAPCO] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NULL,
    [IPPRINOCO] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NULL,
    [IPSEGNOCO] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NULL,
    [IPFECNACO] DATETIME MASKED WITH (FUNCTION = 'default()')                                    NULL,
    [OBSGENINC] CHAR (200)                                                                       NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [ESTTRANSA] CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADINCBASD] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADINCBASD_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADINCBASD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADINCBASD] NOCHECK CONSTRAINT [FK_ADINCBASD_ADCENATEN];


GO
ALTER TABLE [dbo].[ADINCBASD] NOCHECK CONSTRAINT [FK_ADINCBASD_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADINCBASD_IPCODPACI]
    ON [dbo].[ADINCBASD]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Transaccion:  1: En Proceso de Envio  2: Envio Satisfactorio  3: Envio Fallido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que Reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones Generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'OBSGENINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Nacimiento del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Nombre del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Apellido del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Identificacion tributaria - Este el el numero que genera la interfaz contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento  1=Cédula de Ciudadanía  2=Cédula de Extranjería  3=Tarjeta de Identidad  4=Registro Civil  5=Pasporte  6=Adulto Sin Identificación  7=Menor Sin Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Identificacion tributaria - Este el el numero que genera la interfaz contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento  1=Cédula de Ciudadanía  2=Cédula de Extranjería  3=Tarjeta de Identidad  4=Registro Civil  5=Pasporte  6=Adulto Sin Identificación  7=Menor Sin Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Inconsistencia  1: El paciente no existe en la base de datos  2: El paciente no coincide con los datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'TIPINCBAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Hora del Informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'FECINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Informe de Inconsistencias - Consecutivo 00000020', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

