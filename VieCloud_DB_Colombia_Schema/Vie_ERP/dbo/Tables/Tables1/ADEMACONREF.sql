CREATE TABLE [dbo].[ADEMACONREF] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]    INT           NOT NULL,
    [NOMBRE]        VARCHAR (100) NULL,
    [NUMTELE]       VARCHAR (25)  NULL,
    [EMAILCONTACTO] CHAR (100)    NULL,
    [OBSERVA]       VARCHAR (500) NULL,
    [CORRCONTRARE]  BIT           NULL,
    CONSTRAINT [PK_ADEMACONREF] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADEMACONREF_ADPA3047E] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[ADPA3047E] ([AUTO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que señala si el correo electrónico es de contrarreferencia: true=Sí, false=No. Útil para filtrar contactos según tipo de referencia/contrarreferencia en procesos de derivación entre centros de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me identifica si el correo es de contrarreferencia:  true -> Si  false -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo (VARCHAR 500) con observaciones, notas adicionales o comentarios relevantes sobre el contacto de referencia/contrarreferencia y la gestión de la derivación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'OBSERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'OBSERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'OBSERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico (CHAR 100) del contacto de referencia/contrarreferencia. Campo PII ofuscado. Usado para notificaciones y comunicación sobre derivaciones entre instituciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Email Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico (VARCHAR 25) del contacto de referencia/contrarreferencia. Permite localizar rápidamente al profesional o unidad de destino para consultas sobre el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NUMTELE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número Telefono del contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NUMTELE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NUMTELE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo (VARCHAR 100) del contacto, persona responsable o profesional de la salud que gestiona la referencia/contrarreferencia en la institución de destino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador entero (INT) que establece relación FK con el registro cabecera en tabla ADPA3047E (campo AUTO). Agrupa todos los contactos asociados a un proceso de referencia/contrarreferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera; campo que me tiene la relacion con la cabecera en este caso el campo AUTO de la tabla [ADPA3047E].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'IDCABECERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT IDENTITY) que actúa como clave primaria. Consecutivo autoincrementable para cada registro de contacto de referencia/contrarreferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contactos de referencia asociados a una admisión o ingreso del paciente. Registra las personas o entidades (familiares, remitentes, contactos de emergencia) vinculadas a un ingreso específico, con sus datos de comunicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF';
