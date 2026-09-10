CREATE TABLE [dbo].[ADACOMPAN] (
    [CONSEACOM] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDACOMPAN] VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [TIPIDACOM] CHAR (2)                                                                         NULL,
    [PRINOMBRE] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')      NULL,
    [SEGNOMBRE] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')     NULL,
    [PRIAPELLI] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')   NULL,
    [SEGAPELLI] VARCHAR (60) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')  NULL,
    [DIRACOMPA] VARCHAR (100) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [TELACOMPA] VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')          NULL,
    [PARACOMPA] CHAR (2)                                                                         NULL,
    [FECHAREGI] DATETIME                                                                         NOT NULL,
    [CODUSUREG] CHAR (20)                                                                        NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NULL,
    [RESPONSAB] CHAR (1)                                                                         NULL,
    CONSTRAINT [PK_ADACOMPAN] PRIMARY KEY CLUSTERED ([CONSEACOM] ASC),
    CONSTRAINT [FK_ADACOMPAN_ADACOMPAN] FOREIGN KEY ([CONSEACOM]) REFERENCES [dbo].[ADACOMPAN] ([CONSEACOM]),
    CONSTRAINT [FK_ADACOMPAN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADACOMPAN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[IDACOMPAN]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[PRINOMBRE]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[SEGNOMBRE]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[PRIAPELLI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[SEGAPELLI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[DIRACOMPA]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADACOMPAN].[TELACOMPA]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
CREATE NONCLUSTERED INDEX [IX_ADACOMPAN_IPCODPACI]
    ON [dbo].[ADACOMPAN]([IPCODPACI] ASC);


GO
ALTER INDEX [IX_ADACOMPAN_IPCODPACI]
    ON [dbo].[ADACOMPAN] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ADACOMPAN_IPCODPACI_PARACOMPA_PRIAPELLI_PRINOMBRE_SEGAPELLI_SEGNOMBRE]
    ON [dbo].[ADACOMPAN]([IPCODPACI] ASC)
    INCLUDE([PARACOMPA], [PRIAPELLI], [PRINOMBRE], [SEGAPELLI], [SEGNOMBRE]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_ADACOMPAN_9_1420636204__K14]
    ON [dbo].[ADACOMPAN]([NUMINGRES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de rol: acompañante (1) o responsable legal/tutor (2) del paciente en atención. CHAR(1), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'RESPONSAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es acompañante (1) o responsable (2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'RESPONSAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'RESPONSAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso/admisión del paciente a la institución. FK → ADINGRESO. CHAR(10), enlaza a atención/urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario/profesional que registró el acompañante en el sistema. CHAR(20), auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'CODUSUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que Creo la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'CODUSUREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'CODUSUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del acompañante en la base de datos. DATETIME, trazabilidad de carga.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'FECHAREGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'FECHAREGI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'FECHAREGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de parentesco: 1=Padre, 2=Madre, 3=Esposo, 4=Esposa, 5=Hijo(a), 6=Hermano(a), 7=Abuelo(a), 8=Tío(a), 9=Primo(a), 10=Sobrino(a), 11=Amigo(a), 12=Otro. CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PARACOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parentesco del Acompañante    1: Padre  2: Madre  3: Esposo  4: Esposa  5: Hijo(a)  6: Hermano(a)  7: Abuelo(a)  8: Tío(a)  9: Primo(a)  10: Sobrino(a)  11: Amigo(a)  12: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PARACOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PARACOMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto del acompañante/responsable. VARCHAR(50), PII Phone_Ofuscado, búsqueda por número.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'TELACOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'TELACOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'TELACOMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección domiciliaria del acompañante. VARCHAR(100), PII default(), ubicación de contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'DIRACOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del Acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'DIRACOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'DIRACOMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del acompañante. VARCHAR(60), PII FirstSurname_Ofuscado, componente nombre completo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'SEGAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo apellido acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'SEGAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'SEGAPELLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del acompañante. VARCHAR(60), PII FirstSurname_Ofuscado, componente nombre completo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PRIAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PRIAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PRIAPELLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del acompañante. VARCHAR(60), PII SecondName_Ofuscado, componente nombre completo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'SEGNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo nombre acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'SEGNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'SEGNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del acompañante. VARCHAR(60), PII FirstName_Ofuscado, componente nombre completo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PRINOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer nombre acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PRINOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'PRINOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento identificación acompañante: 1=Cédula Ciudadanía, 2=Cédula Extranjería, 3=Tarjeta Identidad, 4=Registro Civil, 5=Pasporte, 6=Adulto Sin ID, 7=Menor Sin ID. CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'TIPIDACOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo identificacion acompañante  1-Cédula de Ciudadanía  2-Cédula de Extranjería  3-Tarjeta de Identidad  4-Registro Civil  5-Pasporte  6-Adulto Sin Identificación  7-Menor Sin Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'TIPIDACOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'TIPIDACOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación (cédula, pasaporte, documento) del acompañante. VARCHAR(15), PII Identification_Ofuscado, búsqueda por cédula/documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'IDACOMPAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'IDACOMPAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'IDACOMPAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula/identificación). VARCHAR(25), PII Identification_Ofuscado, FK → INPACIENT, clave relacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado de registro de acompañante. INT IDENTITY, clave primaria PK_ADACOMPAN, sin FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'CONSEACOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de Acompañantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'CONSEACOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN', @level2type = N'COLUMN', @level2name = N'CONSEACOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de acompañantes y responsables de los pacientes durante su ingreso o atención. Guarda los datos de identificación, nombre, contacto y parentesco de la persona que acompaña al paciente hospitalizado o en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADACOMPAN';
