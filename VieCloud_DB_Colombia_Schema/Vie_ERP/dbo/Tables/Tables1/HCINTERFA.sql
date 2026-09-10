CREATE TABLE [dbo].[HCINTERFA] (
    [CONSECUTI] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCONCEC] NUMERIC (18)                                                                     NULL,
    [CODCONCES] CHAR (10)                                                                        NULL,
    [CODENTIDA] CHAR (9)                                                                         NULL,
    [CODCONTRA] CHAR (6)                                                                         NULL,
    [CODPANATE] CHAR (2)                                                                         NULL,
    [NUMSUMINI] CHAR (20)                                                                        NOT NULL,
    [INTFECHAD] DATETIME                                                                         NOT NULL,
    [INTUSUIND] CHAR (20)                                                                        NULL,
    [INTUSUDGH] CHAR (20)                                                                        NULL,
    [INTUSUWIN] CHAR (60)                                                                        NOT NULL,
    [INTESTRED] CHAR (60)                                                                        NOT NULL,
    [INTORIGEN] CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCINTERFA] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCINTERFA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINTERFA_HCPRESCRC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCPRESCRC] ([CODCONCEC]),
    CONSTRAINT [FK_HCINTERFA_INENTIDAD] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTIDAD] ([CODENTIDA]),
    CONSTRAINT [FK_HCINTERFA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINTERFA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
CREATE NONCLUSTERED INDEX [_dta_index_HCINTERFA_8_1872725724__K2_3_9]
    ON [dbo].[HCINTERFA]([IPCODPACI] ASC)
    INCLUDE([NUMINGRES], [NUMSUMINI]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18). Identificador único para rastreo y control de cambios en registros de interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la interfaz (CHAR 1): H=Historias Clínicas, E=Enfermería, I=Inventarios. Fuente del documento en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen de la Interfaz:  H: Historias Clinicas  E: Enfermeria  I: Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estación de trabajo (CHAR 60). Nombre o identificación del equipo desde donde se creó el documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estacion de Trabajo desde donde se creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Windows (CHAR 60, requerido). Credencial de Windows que autenticó la creación del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Windows que creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario DGH (CHAR 20). Identificación del usuario en sistema DGH que generó el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de DGH que creo el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUDGH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUDGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Indigo (CHAR 20). Identificación del usuario en Indigo Vie Cloud que creó el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Indigo que creo el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación (DATETIME, requerida). Marca temporal de cuándo se registró el documento en la interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion del Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número suministro DGH (CHAR 20, requerido). Identificador único del suministro/insumo en sistema DGH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'NUMSUMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Suministro en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'NUMSUMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'NUMSUMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de beneficio (CHAR 2). Código del plan de cobertura/beneficios asignado al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plan de Beneficio del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODPANATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODPANATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código contrato EAPB (CHAR 6). Identificador del contrato con la entidad aseguradora del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato de la EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código entidad (CHAR 9, FK INENTIDAD). Identificador de la institución/EPS por la que ingresa el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Entidad por la que ingresa el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo insumos (CHAR 10). Identificador del consecutivo de insumos/suministros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONCES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONCES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONCES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo prescripciones (NUMERIC 18, FK HCPRESCRC). Referencia a la prescripción relacionada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de prescripciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10, FK ADINGRESO, requerido). Identificador del ingreso/atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código paciente (VARCHAR 25, PII ofuscado, FK INPACIENT, requerido). Identificación/cédula/documento del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de registro (CHAR 10, PK, requerido). Identificador único secuencial del registro de interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de registro  Identificador: 00000015', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de interfaz o integración de ingresos/atenciones con entidades externas (aseguradoras, EPS, contratos). Guarda el trazado de cada transacción enviada o recibida, incluyendo el paciente, el ingreso, la entidad pagadora y los datos de auditoría del proceso de interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERFA';
