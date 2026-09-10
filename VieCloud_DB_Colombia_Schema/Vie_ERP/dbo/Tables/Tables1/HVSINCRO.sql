CREATE TABLE [dbo].[HVSINCRO] (
    [ID]         INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDPREGUNTA] INT                                                                              NULL,
    [CORREO]     VARCHAR (200) MASKED WITH (FUNCTION = 'email()')                                 NULL,
    [RESPUESTA]  VARCHAR (500)                                                                    NULL,
    CONSTRAINT [PK_HVSINCRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HVSINCRO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HVSINCRO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HVSINCRO].[CORREO]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta a pregunta de seguridad del paciente para autenticación en Health Vault. Texto libre (VARCHAR 500) que valida identidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'RESPUESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'RESPUESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'RESPUESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del paciente encriptado/enmascarado. Dato PII sensible con función email() para ofuscación en reportes y búsquedas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo del paciente Encriptado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (1-10) de pregunta de seguridad predefinida: mascota, abuelos, apodo, color favorito, colegio primaria/secundaria, equipo fútbol, ciudad nacimiento padres. Usado para autenticación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IDPREGUNTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preguntas de seguridad predefinidas:  1. "Nombre de su primera mascota",  2. "Nombre de su abuelo paterno",  3. "Apodo de la infancia",  4. "Color favorito",  5. "Colegio al cual asistio en la primaria",  6. "Equipo de futbol al cual anima",  7. "Ciudad de nacimiento de su padre",  8. "Nombre de su abuela materna",  9. "Colegio al cual asistio en la secundaria",  10. "Ciudad de nacimiento de su madre"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IDPREGUNTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IDPREGUNTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, documento, equivalente). Referencia FK a INPACIENT. Dato PII ofuscado. Sincronizado con Health Vault.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente que se sincroniza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (identity) de registro de sincronización Health Vault. Clave primaria auditoria de integraciones de historias clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Sincronizaciones Health Vault', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de sincronización de respuestas a preguntas de historia de vida o encuestas del paciente, vinculando cada respuesta al paciente identificado por su cédula y al correo electrónico informado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HVSINCRO';
