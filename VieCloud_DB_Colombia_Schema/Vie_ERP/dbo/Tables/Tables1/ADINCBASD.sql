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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[CODIGONIT]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPPRIAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPSEGAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPPRINOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPSEGNOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPFECNACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPPRIAPCO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPSEGAPCO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPPRINOCO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPSEGNOCO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADINCBASD].[IPFECNACO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');



GO
CREATE NONCLUSTERED INDEX [IX_ADINCBASD_IPCODPACI]
    ON [dbo].[ADINCBASD]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18), identificador único del registro auditado para trazabilidad y control de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la transacción (CHAR 1): 1=En proceso de envío, 2=Envío satisfactorio, 3=Envío fallido. Indica etapa de sincronización del reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Transaccion:  1: En Proceso de Envio  2: Envio Satisfactorio  3: Envio Fallido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'ESTTRANSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que reporta (CHAR 20), profesional de la salud o personal administrativo responsable del registro de inconsistencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Reporta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales (CHAR 200), notas descriptivas adicionales sobre la inconsistencia detectada en datos del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'OBSGENINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones Generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'OBSGENINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'OBSGENINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente corregido (DATETIME, PII ofuscado), dato rectificado tras validación de inconsistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nacimiento del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente corregido (CHAR 20, PII ofuscado), dato rectificado en registro de inconsistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente corregido (CHAR 20, PII ofuscado), dato rectificado tras validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Nombre del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente corregido (CHAR 20, PII ofuscado), dato rectificado en base de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Apellido del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente corregido (CHAR 20, PII ofuscado), dato rectificado tras inconsistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente Corregido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT tributario corregido (VARCHAR 25, PII ofuscado), número de identificación contable del paciente rectificado por la interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Identificacion tributaria - Este el el numero que genera la interfaz contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento corregido (INT): 1=Cédula Ciudadanía, 2=Cédula Extranjería, 3=Tarjeta Identidad, 4=Registro Civil, 5=Pasporte, 6=Adulto Sin ID, 7=Menor Sin ID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento  1=Cédula de Ciudadanía  2=Cédula de Extranjería  3=Tarjeta de Identidad  4=Registro Civil  5=Pasporte  6=Adulto Sin Identificación  7=Menor Sin Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (DATETIME, PII ofuscado), dato original en registro de inconsistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente (CHAR 20, PII ofuscado), dato original reportado en inconsistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente (CHAR 20, PII ofuscado), dato original en registro de inconsistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente (CHAR 20, PII ofuscado), dato original reportado en inconsistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente (CHAR 20, PII ofuscado), dato original en inconsistencia de base de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT tributario (VARCHAR 25, PII ofuscado), número de identificación contable del paciente generado por interfaz contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Identificacion tributaria - Este el el numero que genera la interfaz contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento (INT): 1=Cédula Ciudadanía, 2=Cédula Extranjería, 3=Tarjeta Identidad, 4=Registro Civil, 5=Pasporte, 6=Adulto Sin ID, 7=Menor Sin ID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento  1=Cédula de Ciudadanía  2=Cédula de Extranjería  3=Tarjeta de Identidad  4=Registro Civil  5=Pasporte  6=Adulto Sin Identificación  7=Menor Sin Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de inconsistencia (CHAR 1): 1=Paciente no existe en base de datos, 2=Datos del paciente no coinciden. Clasificación del tipo de fallo detectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'TIPINCBAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Inconsistencia  1: El paciente no existe en la base de datos  2: El paciente no coincide con los datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'TIPINCBAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'TIPINCBAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII ofuscado), identificación única del paciente vinculado a la inconsistencia detectada. FK→INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad (CHAR 9), prestador de salud u organización responsable del reporte. FK→INENTIDAD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10), unidad funcional, sede o punto de servicio donde se originó la inconsistencia. FK→ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del informe (DATETIME), timestamp de generación del reporte de inconsistencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'FECINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Hora del Informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'FECINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'FECINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del informe (CHAR 4), consecutivo correlativo del reporte de inconsistencias (ej: 0020).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Informe de Inconsistencias - Consecutivo 00000020', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'NUMINFORM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno (NUMERIC 18, IDENTITY 1,1), identificador único (PK) del registro en tabla ADINCBASD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno de Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de incidencias o inconsistencias de base de datos de afiliados detectadas durante el proceso de admisión. Guarda los datos del paciente titular y del cotizante o responsable, junto con el tipo de inconsistencia reportada, la entidad y el centro de atención involucrados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINCBASD';
