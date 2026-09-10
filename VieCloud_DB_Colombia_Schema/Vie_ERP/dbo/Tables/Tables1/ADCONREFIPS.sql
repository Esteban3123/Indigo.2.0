CREATE TABLE [dbo].[ADCONREFIPS] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]    CHAR (100)    NOT NULL,
    [CODCENATE]     CHAR (10)     NOT NULL,
    [NOMBRE]        VARCHAR (100) NULL,
    [NUMTELE]       VARCHAR (25)  NULL,
    [EMAILCONTACTO] CHAR (100)    NULL,
    [OBSERVA]       VARCHAR (500) NULL,
    [CORRCONTRARE]  BIT           NULL,
    CONSTRAINT [PK_ADCONREFIPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADCONREFIPS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADCONREFIPS_ADCONREFIPS] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que marca si el correo/contacto es específicamente para gestión de contrarreferencias (true=sí, false=no); controla enrutamiento de respuestas de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me identifica si el correo es de contrarreferencia:  true -> Si   false -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios adicionales sobre el contacto, disponibilidad, procedimientos especiales o detalles de coordinación de referencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'OBSERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'OBSERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'OBSERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del contacto de referencia/contrarreferencia (PII); usado para envío de notificaciones, solicitudes o documentación RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Email Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico del contacto (VARCHAR 25); dato de comunicación para seguimiento de referencias, contrarreferencias o coordinación de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NUMTELE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número Telefono del contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NUMTELE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NUMTELE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del contacto responsable de gestionar referencias, contrarreferencias o coordinación de atención entre centros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o institución prestadora (FK a ADCENATEN); identifica dónde se origina el contacto de referencia o contrarreferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera RIPS (FK a ADCONTIPS.CODIGOIPS); vincula el contacto a la referencia/contrarreferencia principal del proceso de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera; campo que me tiene la relacion con la cabecera en este caso el campo  de la tabla [ADCONTIPS].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) y consecutivo de registro de contacto RIPS en la tabla ADCONREFIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contactos de referencia IPS asociados a contratos o cabeceras de admisión por centro de atención. Registra las personas o entidades de contacto (nombre, teléfono, correo) para la coordinación de referencias entre instituciones de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS';
