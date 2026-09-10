CREATE TABLE [dbo].[INPACIENTPYP] (
    [ID]             INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [TIPOCOTIZANTE]  VARCHAR (100)                                                                    NULL,
    [IDCOTIZANTE]    VARCHAR (100)                                                                    NULL,
    [EMPCOTIZANTE]   VARCHAR (100)                                                                    NULL,
    [EPS]            VARCHAR (100)                                                                    NULL,
    [FECHAAFILIA]    DATE                                                                             NULL,
    [SEMCOTIZADAS]   VARCHAR (100)                                                                    NULL,
    [NUMCARNET]      VARCHAR (100)                                                                    NULL,
    [TIPODOCUMEN]    VARCHAR (100)                                                                    NULL,
    [FECEXPEDI]      DATE                                                                             NULL,
    [PRIMERNOM]      VARCHAR (100)                                                                    NULL,
    [SEGNOMBRE]      VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')    NULL,
    [PRIMAPELLI]     VARCHAR (100)                                                                    NULL,
    [SEGAPELLI]      VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)') NULL,
    [PARENTECOTIZA]  VARCHAR (100)                                                                    NULL,
    [FECHNACI]       DATE                                                                             NULL,
    [SEXO]           VARCHAR (100)                                                                    NULL,
    [ESTADOCIVIL]    VARCHAR (100)                                                                    NULL,
    [DISOACIDAD]     VARCHAR (100)                                                                    NULL,
    [GRUPOPOBLA]     VARCHAR (100)                                                                    NULL,
    [GRUPOETNICO]    VARCHAR (100)                                                                    NULL,
    [DIRECCION]      VARCHAR (100) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [TEL1]           VARCHAR (100)                                                                    NULL,
    [TEL2]           VARCHAR (100)                                                                    NULL,
    [EMAIL]          VARCHAR (100)                                                                    NULL,
    [URBANDO]        VARCHAR (100)                                                                    NULL,
    [MUNICIPIO]      VARCHAR (100)                                                                    NULL,
    [DEPARTAMEN]     VARCHAR (100)                                                                    NULL,
    [NUMFICHSISBEN]  VARCHAR (100)                                                                    NULL,
    [NIVELSISBEN]    VARCHAR (100)                                                                    NULL,
    [TIPOAFILIACION] VARCHAR (100)                                                                    NULL,
    [ESTADOAFILIA]   VARCHAR (100)                                                                    NULL,
    [IPSASIGNADA]    VARCHAR (100)                                                                    NULL,
    [SEXUALACTIVA]   VARCHAR (100)                                                                    NULL,
    [GRAVIDEZ]       VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_INPACIENTPYP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_INPACIENTPYP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENTPYP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENTPYP].[SEGNOMBRE]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENTPYP].[SEGAPELLI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENTPYP].[DIRECCION]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de gravidez o embarazo de la paciente (VARCHAR 100). Indicador de si la paciente se encuentra en gestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRAVIDEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la gravidez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRAVIDEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRAVIDEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de vida sexual activa del paciente (VARCHAR 100). Antecedente epidemiológico relevante para atención en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXUALACTIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda  la sexual activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXUALACTIVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXUALACTIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Institución Prestadora de Servicios (IPS) asignada al paciente para atención (VARCHAR 100). Centro de salud o entidad prestadora de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IPSASIGNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la ip asignada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IPSASIGNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IPSASIGNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la afiliación del paciente a su plan de salud: activo, inactivo, suspendido (VARCHAR 100). Condición del vínculo con EPS/asegurador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOAFILIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la afiliación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOAFILIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOAFILIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o modalidad de afiliación del paciente: contributivo, subsidiado, especial, transitorio (VARCHAR 100). Categoría de vinculación al sistema de seguridad social.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOAFILIACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo de afiliación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOAFILIACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOAFILIACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel socioeconómico del paciente según clasificación SISBEN (1-8) (VARCHAR 100). Puntaje de estratificación para beneficiarios de programas sociales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NIVELSISBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el nivel del sisben', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NIVELSISBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NIVELSISBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ficha o radicado de registro en el Sistema de Identificación de Potenciales Beneficiarios SISBEN (VARCHAR 100). Identificador del paciente en base de datos de afiliación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMFICHSISBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el número de la ficha del sisben ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMFICHSISBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMFICHSISBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Departamento o región administrativa donde reside el paciente (VARCHAR 100). División territorial de domicilio para ubicación geográfica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DEPARTAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DEPARTAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DEPARTAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Municipio o ciudad de residencia del paciente (VARCHAR 100). Localidad de domicilio para contacto y derivación de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'se guarda el municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación urbano/rural del domicilio del paciente (VARCHAR 100). Zona de residencia: área urbana o zona rural.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'URBANDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda urbano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'URBANDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'URBANDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del paciente para contacto (VARCHAR 100). Dirección de correo electrónico para comunicación digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo número telefónico del paciente (VARCHAR 100). Teléfono alterno para contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el teledono 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer número telefónico del paciente (VARCHAR 100). Teléfono principal para contacto y comunicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el telefono 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Domicilio del paciente (VARCHAR 100, MASKED con ''''default()''''). Dirección de residencia; datos PII enmascarados por privacidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DIRECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo étnico o comunidad indígena del paciente (VARCHAR 100). Pertenencia a población especial o etnia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOETNICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOETNICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOETNICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo poblacional al que pertenece: adulto mayor, gestante, desplazado, LGBTIQ+ (VARCHAR 100). Clasificación de población vulnerable o especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOPOBLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el grupo poblacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOPOBLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOPOBLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición de discapacidad del paciente (VARCHAR 100). Códigos: 1=Sí (personas con discapacidad), 2=No. Indicador de dependencia o limitación funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DISOACIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Discapacitado (1. Si, 2. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DISOACIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DISOACIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil del paciente: soltero, casado, divorciado, viudo, unión libre (VARCHAR 100). Situación legal de relación matrimonial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el estado civil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo biológico o género del paciente: Masculino, Femenino, Otro (VARCHAR 100). Característica demográfica para registros epidemiológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el genero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (DATE). Dato esencial para cálculo de edad, cronograma de vacunación y atención por grupos etarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación de parentesco entre el paciente y el cotizante principal (VARCHAR 100): cónyuge, hijo, padre, etc. Vínculo familiar para clasificación de beneficiario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PARENTECOTIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parentesco con el cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PARENTECOTIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PARENTECOTIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente (VARCHAR 100, MASKED). Parte del nombre legal; datos PII enmascarados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGAPELLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente (VARCHAR 100). Componente del nombre legal para identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMAPELLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMAPELLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente (VARCHAR 100, MASKED). Parte del nombre legal; datos PII enmascarados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente (VARCHAR 100). Componente principal del nombre para identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMERNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMERNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMERNOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expedición o emisión del documento de identidad (DATE). Dato de vigencia y control del documento identificatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECEXPEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la fecha de expedición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECEXPEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECEXPEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad: cédula, pasaporte, tarjeta de identidad, documento de extranjería (VARCHAR 100). Clase de identificación oficial del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPODOCUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el tipo de documento de identidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPODOCUMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPODOCUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del carné de afiliación a EPS o asegurador (VARCHAR 100). Identificador de cobertura y beneficiario en plan de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMCARNET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el número del carnet', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMCARNET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMCARNET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de semanas cotizadas por el paciente al sistema de seguridad social (VARCHAR 100). Semanas de aporte y cotización acumuladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEMCOTIZADAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas cotizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEMCOTIZADAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEMCOTIZADAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de afiliación inicial del paciente al sistema de salud (DATE). Inicio del vínculo con EPS o asegurador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHAAFILIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de afiliación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHAAFILIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHAAFILIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entidad Prestadora de Servicios de Salud (EPS) a la que está afiliado el paciente (VARCHAR 100). Asegurador de salud o plan de cobertura médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la entidad prestadora de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empresa o empleador del cotizante principal (VARCHAR 100). Razón social de la organización empleadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMPCOTIZANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Empleado cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMPCOTIZANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMPCOTIZANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única del cotizante principal (cédula, NIT) (VARCHAR 100). Documento del aportante o afiliador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IDCOTIZANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el Id cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IDCOTIZANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IDCOTIZANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cotizante: empleado, independiente, pensionado, otra (VARCHAR 100). Categoría laboral de afiliación al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOCOTIZANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Se guarda el tipo de cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOCOTIZANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOCOTIZANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente en el sistema Indigo Vie Cloud (VARCHAR 25, MASKED con ''''Identification_Ofuscado''''). Identificación PII del paciente; enmascarada por seguridad. FK a INPACIENT.IPCODPACI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla INPACIENTPYP (INT IDENTITY). Clave primaria generada automáticamente para cada registro de plan y prestador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de afiliación y datos sociodemográficos de pacientes para programas de Promoción y Prevención (PyP). Consolida datos del cotizante, EPS, parentesco, ficha SISBEN, grupo poblacional y estado de afiliación, usada para gestión de riesgo y seguimiento en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP';
