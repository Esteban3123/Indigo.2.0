CREATE TABLE [dbo].[HCANESTQX] (
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                CHAR (10)                                                                        NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NULL,
    [FECREGSIS]                DATETIME                                                                         CONSTRAINT [DF_HCANESTQX_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    CONSTRAINT [PK_HCANESTQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCANESTQX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANESTQX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANESTQX_HCANESTQX] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCANESTQX] ([ID]),
    CONSTRAINT [FK_HCANESTQX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCANESTQX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANESTQX_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANESTQX_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTQX].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANESTQX].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción de relación con VIE ERP (contract.CUPSEntityContractDescriptions); vinculación de contrato y descripción de servicios CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en el sistema; timestamp de inserción automática', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (Código Único de Procedimientos y Servicios); identificador nacional de procedimiento o servicio de salud; máscara PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero, especialista); identificación del proveedor de atención; FK a INPROFSAL; máscara PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional; departamento, servicio o área clínica de atención; FK a INUNIFUNC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; identificador de clínica, hospital o IPS prestadora; FK a ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente; referencia a atención, consulta o hospitalización; FK a ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; cédula, documento de identidad u otro identificador único; PII ofuscado; FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio; identificador secuencial de documento o trámite administrativo de la atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica; clasificación de registro de atención (urgencia, consulta, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincremental; identificador único del registro de estimación de cantidad de historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de anestesia en quirófano (anestesia quirúrgica). Guarda la información de cada acto anestésico realizado durante una cirugía, vinculando al paciente, el ingreso, el profesional anestesiólogo, el servicio prestado y el centro de atención donde se realizó el procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTQX';
