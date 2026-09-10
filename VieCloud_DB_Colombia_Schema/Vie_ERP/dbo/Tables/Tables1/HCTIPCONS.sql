CREATE TABLE [dbo].[HCTIPCONS] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NULL,
    [TIPOCONSU]   TINYINT                                                                          NOT NULL,
    [FECHAREG]    DATETIME                                                                         NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [AGASICITAID] INT                                                                              NULL,
    [REGCONF]     BIT                                                                              CONSTRAINT [DF_HCTIPCONS_REGCONF] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_HCTIPCONS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCTIPCONS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCTIPCONS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCTIPCONS_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO


GO
ALTER TABLE [dbo].[HCTIPCONS] NOCHECK CONSTRAINT [FK_HCTIPCONS_INPACIENT];


GO
ALTER TABLE [dbo].[HCTIPCONS] NOCHECK CONSTRAINT [FK_HCTIPCONS_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCTIPCONS].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCTIPCONS].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCTIPCONS__IPCODPACI]
    ON [dbo].[HCTIPCONS]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro Confirmado; indicador booleano (BIT) que marca si la consulta/atención ha sido validada y confirmada en el sistema. Valores: 1=Confirmado, 0=Pendiente confirmación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'REGCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'REGCONF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'REGCONF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de cita (FK a AGASICITA.CODAUTONU); relaciona la consulta con el agendamiento/cita programada en la agenda de servicios y citas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'AGASICITAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID que relaciona la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'AGASICITAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'AGASICITAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero, nutricionista, psicólogo, oftalmólogo) que realiza la atención. PII Ofuscado. FK a INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la consulta en el sistema; momento en que se documenta la atención prestada al paciente (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se hace el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de consulta/atención: 1=Planificación Familiar Primera Vez, 2=Control Prenatal Primera Vez, 3=Control Prenatal Subsecuente, 4=Crecimiento y Desarrollo Primera Vez, 5=Consulta Joven Primera Vez, 6=Consulta Adulto Primera Vez, 7=Oftalmología, 8=Nutrición, 9=Psicología, 10=No Aplica. Clasificación clínica por programa y edad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'TIPOCONSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de consulta que realiza el paciente:  1-> Planificación Familiar Primera vez(Habilitar si la edad del paciente es >= a 10 y < a 60 años y si no tiene registro en la tabla de consultas de este mismo tipo)    2->Control Prenatal Primera Vez (Habilitar si la paciente está en estado de embarazo y si la edad del paciente es >= a 10 y < a 60 años. si tiene registro en la tabla de consultas de este mismo tipo con fecha menor a 10 meses no se le muestra la opción)    3-> Control Prenatal (No Primera vez) (Habilitar si la paciente está en estado de embarazo y si la edad del paciente es >= a 10 y < a 60 años. Excluye control prenatal primera vez)    4-> Consulta de Crecimiento y Desarrollo Primera vez (Habilitar si la edad del paciente es < a 10 años y si no tiene registro en la tabla de consultas de este mismo tipo.)    5-> Consulta de Joven Primera vez (Habilitar si la edad del paciente es >= a 10 y < a 30 años y si no tiene registro en la tabla de consultas de este mismo tipo.)    6-> Consulta de Adulto Primera vez (Habilitar si la edad del paciente es >= a 45 años y si no tiene registro en la tabla de consultas de este mismo tipo.)    7.  Consulta por Oftalmología:  8. Consulta de Nutrición:  9.  Consulta de Psicología  10. No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'TIPOCONSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'TIPOCONSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente en el centro de atención (FK a ADINGRESO). Identifica la atención o urgencia asociada a esta consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, documento de identidad). PII Ofuscado. FK a INPACIENT. Búsqueda: paciente, cédula, identificación, documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de cada registro de consulta en la tabla HCTIPCONS. Clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del tipo de consulta o atención realizada al paciente dentro de su historia clínica. Relaciona cada evento de consulta (presencial, telefónica, urgencia, etc.) con el paciente, el ingreso, el profesional de salud y la cita agendada correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS';
