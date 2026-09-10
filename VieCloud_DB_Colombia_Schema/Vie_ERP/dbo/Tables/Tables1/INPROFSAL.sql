CREATE TABLE [dbo].[INPROFSAL] (
    [CODPROSAL]                        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [CODIGONIT]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')              NULL,
    [NOMMEDICO]                        CHAR (60) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [MEDPRINOM]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')        NOT NULL,
    [MEDSEGNOM]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')       NOT NULL,
    [MEDPRIAPEL]                       CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')     NOT NULL,
    [MEDSEGAPEL]                       CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')    NOT NULL,
    [CODESPEC1]                        CHAR (3)                                                                        NOT NULL,
    [CODESPEC2]                        CHAR (3)                                                                        NULL,
    [CODESPEC3]                        CHAR (3)                                                                        NULL,
    [IMDIRECCI]                        CHAR (100) MASKED WITH (FUNCTION = 'default()')                                 NOT NULL,
    [IMTELEFON]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')            NOT NULL,
    [IMTELMOVI]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "CellPhone_Ofuscado", 0)')        NOT NULL,
    [TARJETAPR]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "ProfessionalCard_Ofuscado", 0)') NULL,
    [ESTADOMED]                        INT                                                                             NOT NULL,
    [MEDTIPVIN]                        INT                                                                             NOT NULL,
    [CODUSUARI]                        CHAR (20)                                                                       NULL,
    [TIPPROFES]                        INT                                                                             NOT NULL,
    [MEDICFOTO]                        VARBINARY (MAX)                                                                 NULL,
    [MEDIFIRMA]                        VARBINARY (MAX)                                                                 NULL,
    [MEDIHUELL]                        VARBINARY (MAX)                                                                 NULL,
    [MEDPERCIR]                        CHAR (1)                                                                        NULL,
    [INDAUDFOR]                        NUMERIC (18)                                                                    NOT NULL,
    [REACONEXT]                        BIT                                                                             NOT NULL,
    [GENCONTRA]                        INT                                                                             NULL,
    [FECULTLIQ]                        DATETIME                                                                        NULL,
    [FECULTLIQTMP]                     DATETIME                                                                        NULL,
    [GENPROVEE]                        INT                                                                             NULL,
    [GENLINDIST]                       INT                                                                             NULL,
    [AUUBICACI]                        CHAR (20)                                                                       NULL,
    [MOSTRARWEB]                       BIT                                                                             NULL,
    [AssistedConsultation]             BIT                                                                             NULL,
    [PerformAssistedInterconsultation] BIT                                                                             NULL,
    [IDADTIPOIDENTIFICA]               INT                                                                             NULL,
    [RequiresElectronicSignature]      BIT                                                                             CONSTRAINT [DF_INPROFSAL_ElectronicSignature] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_INMEDICOS] PRIMARY KEY CLUSTERED ([CODPROSAL] ASC),
    CONSTRAINT [FK_INPROFSAL_ADTIPOIDENTIFICA_IDADTIPOIDENTIFICA] FOREIGN KEY ([IDADTIPOIDENTIFICA]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID]),
    CONSTRAINT [FK_INPROFSAL_INESPECIA] FOREIGN KEY ([CODESPEC1]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_INPROFSAL_INESPECIA1] FOREIGN KEY ([CODESPEC2]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_INPROFSAL_INESPECIA2] FOREIGN KEY ([CODESPEC3]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[CODIGONIT]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[NOMMEDICO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[MEDPRINOM]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[MEDSEGNOM]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[MEDPRIAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[MEDSEGAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[IMDIRECCI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[IMTELEFON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[IMTELMOVI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPROFSAL].[TARJETAPR]
    WITH (LABEL = 'Confidential - Credentials', INFORMATION_TYPE = 'Credentials');



GO
CREATE NONCLUSTERED INDEX [IX_INPROFSAL_ESTADOMED_CODESPEC1]
    ON [dbo].[INPROFSAL]([ESTADOMED] ASC)
    INCLUDE([CODESPEC1]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INPROFSAL_6_1233491523__K1_K3]
    ON [dbo].[INPROFSAL]([CODPROSAL] ASC, [NOMMEDICO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INPROFSAL_ESTADOMED]
    ON [dbo].[INPROFSAL]([ESTADOMED] ASC);


GO
CREATE NONCLUSTERED INDEX [IndiceAgendamiento]
    ON [dbo].[INPROFSAL]([REACONEXT] ASC)
    INCLUDE([CODESPEC1], [CODESPEC2], [CODESPEC3], [CODPROSAL], [NOMMEDICO]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INPROFSAL_8_1233491523__K8_K1_3_1912]
    ON [dbo].[INPROFSAL]([CODESPEC1] ASC, [CODPROSAL] ASC)
    INCLUDE([NOMMEDICO]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INPROFSAL_7_1233491523__K1_K8_3]
    ON [dbo].[INPROFSAL]([CODPROSAL] ASC, [CODESPEC1] ASC)
    INCLUDE([NOMMEDICO]);


GO
CREATE COLUMNSTORE INDEX [IX_INPROFSAL]
    ON [dbo].[INPROFSAL]([CODPROSAL]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de documento de identificación del profesional (cédula, pasaporte, etc.). FK a ADTIPOIDENTIFICA. PII Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IDADTIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tipo de identificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IDADTIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IDADTIPOIDENTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el profesional de salud realiza interconsultación asistida (True=Sí, False=No). Bit booleano para interconsultación mediada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'PerformAssistedInterconsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que almacena si el profesional "Realiza interfconsulta asistida".  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'PerformAssistedInterconsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'PerformAssistedInterconsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional de salud habilitado para atención en modalidad consulta asistida (True=Sí, False=No). Bit booleano para consulta mediada/telemedicina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'AssistedConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional de la salud que realiza la atención médica en modalidad de consulta asistida :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'AssistedConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'AssistedConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visibilidad del profesional en portal web público (True=Visible, False=Oculto). Bit booleano para publicación digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en la Web:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ubicación geográfica y municipio del profesional. CHAR(20) para localización, domicilio laboral, jurisdicción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Ubicacion y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'AUUBICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de línea de distribución del proveedor (solo contratos Estándar). INT. Utilizado en liquidación de honorarios médicos, interlinking GENESIS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENLINDIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la linea de distribucion del proveedor, Este campo solo se llena si el contrato que se le asigno es de tipo Estandar     Los campos de proveedores se utilizan en la liquidacion de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENLINDIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENLINDIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del proveedor asignado al profesional (solo contratos Estándar). INT. Referencia para liquidación de causación de honorarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del proveedor, Este campo solo se habilita cuando el contrato que se asigna al profesional es de tipo Estandar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última liquidación temporal (no confirmada). DATETIME. Se actualiza al guardar liquidación; se anula al confirmar o anular documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'FECULTLIQTMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de la ultima liquidaicon de un documento cuando no esta confirmado, Esta fecha se actualiza cuando se guarda un documento de liquidacion y cuando se anula o se confirma este campo vuelve a quedar nulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'FECULTLIQTMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'FECULTLIQTMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última liquidación confirmada. DATETIME. No nula, entre fecha inicial contrato y actual. Se actualiza en confirmación de liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'FECULTLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de la ultima liquidaicon, no puede ser nula, superior a la fecha inicial del contrato e inferior o igual a la fecha actual, se actualiza en cada confirmacion de liquidacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'FECULTLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'FECULTLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del contrato de causación de honorarios médicos. INT FK a GENESIS.MedicalFeesContract. Referencia principal para liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del contrato de causacion de honorarios medicos esto se saca de GENESIS de la tabla MedicalFeesContract', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional realiza consulta externa / agendamiento (True=Sí, False=No). BIT. Indica disponibilidad para citas ambulatorias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'REACONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el medico realiza consulta externa (Agendamiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'REACONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'REACONEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría interna. NUMERIC(18). Identificador de auditoría forense o control interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil quirúrgico del profesional: 0=Ninguno, 1=Cirujano, 2=Anestesiólogo, 3=Ayudante, 4=Anestesiólogo/Cirujano. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPERCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perfil de Cirugia:  0. Ninguno  1. Cirujano  2. Anestesiologos  3. Ayudantes  4. Anestesiologos/ Cirujano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPERCIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPERCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella dactilar del profesional. VARBINARY(MAX). Datos biométricos PII para identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDIHUELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Huella Dactilar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDIHUELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDIHUELL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firma digital del profesional de salud. VARBINARY(MAX). Documento biométrico para autorización legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDIFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Firma del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDIFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDIFIRMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fotografía del profesional de salud. VARBINARY(MAX). Imagen PII para identificación visual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDICFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDICFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDICFOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de profesional: 1=Médico General, 2=Especialista, 3=Enfermera, 4=Auxiliar Enfermería, 5=Odontólogo, 6=Odontólogo Especialista, 7=Nutricionista, 8=Higienista, 9=Psicólogo, 10=Trabajadora Social, 11=Promotor Saneamiento, 12=Ingeniero Sanitario, 13=Médico Veterinario, 14=Ingeniero Alimentos, 15=Auxiliar Bacteriólogo, 16=Terapeuta, 17=Optómetra, 18=Químico Farmacéutico, 19=Radiólogo, 20=Tecnólogo Radiólogo, 21=Instrumentador Qx, 22=Auxiliar Patología, 23=Otros, 24=Médico Interno, 25=Bacteriólogo, 26=Patólogo, 27=Médico Residente. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'TIPPROFES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 Medico General  2 Medico Especialista  3 Enfermera  4 Auxiliar Enfermeria  5 Odontologo General  6 Odontologo Especialista  7 Nutricionista  8 Higienista  9 Psicologo  10 Trabajadora Social  11 Promotor de Saneamiento  12 Ingeniero Sanitario  13 Medico Veterinario  14 Ingeniero Alimento  15 Auxiliar Bacteriologo  16 Terapeuta  17 Optometra  18 Quimico Farmaceutico  19 Radiologo  20 Tecnologo Radiologo  21 Instrumentador Qx  22 Auxiliar Patologia  23 Otros  24 Medico Interno  25 Bacteriologo(a)  26 Patólogo(a)  27               Médico residente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'TIPPROFES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'TIPPROFES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario Indigo Crystal (sistema ERP). CHAR(20). Referencia a cuenta de acceso del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario Indigo Crystal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vinculación laboral: 1=Planta, 2=Contrato, 3=Residente, 4=Interno, 5=Externo. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDTIPVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Vinculacion  1. Planta  2. Contrato  3. Residente  4. Interno  5. Externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDTIPVIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDTIPVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del profesional: 1=Activo, 2=Inactivo. INT. Bandera de activación/desactivación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'ESTADOMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Profesional de la Salud:  1: Activo  2: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'ESTADOMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'ESTADOMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tarjeta profesional (cédula profesional, licencia). CHAR(15) PII Professional_Card_Ofuscado. Credencial sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'TARJETAPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Tarjeta Profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'TARJETAPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'TARJETAPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono móvil del profesional. CHAR(15) PII CellPhone_Ofuscado. Contacto celular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMTELMOVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Telefono Movil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMTELMOVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMTELMOVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono fijo del profesional. CHAR(15) PII Phone_Ofuscado. Contacto telefónico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Telefono Fijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMTELEFON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de domicilio del profesional. CHAR(100) PII default(). Ubicación postal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMDIRECCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMDIRECCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IMDIRECCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tercera especialidad médica. CHAR(3) FK a INESPECIA.CODESPECI. Subespecialización adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Tercera Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de segunda especialidad médica. CHAR(3) FK a INESPECIA.CODESPECI. Especialidad secundaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Segunda Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de primera especialidad médica. CHAR(3) FK a INESPECIA.CODESPECI. Especialidad principal requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Primer Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODESPEC1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del profesional de salud. CHAR(15) PII Name_Ofuscado. Identificación nominal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Apellido del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del profesional de salud. CHAR(15) PII FirstSurname_Ofuscado. Identificación nominal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del profesional de salud. CHAR(15) PII SecondName_Ofuscado. Identificación nominal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDSEGNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDSEGNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDSEGNOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del profesional de salud. CHAR(15) PII FirstName_Ofuscado. Identificación nominal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPRINOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Nombre del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPRINOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPRINOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del profesional de salud. CHAR(60) PII Name_Ofuscado. Identificación oficial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'NOMMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Completo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'NOMMEDICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'NOMMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código NIT (número de identificación tributaria) del profesional. CHAR(15) PII Nit_Ofuscado. Identificación fiscal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de salud. CHAR(20) PK PII Identification_Ofuscado. Identificador principal, equivalente a ID profesional, cédula o documento de identificación en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si el profesional puede o no usar firma electronica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'RequiresElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maestro de profesionales de la salud (médicos, enfermeros, especialistas y otros prestadores). Contiene la identificación, nombre completo, especialidades, datos de contacto, tarjeta profesional, firma, huella y parámetros de vinculación de cada profesional habilitado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL';
