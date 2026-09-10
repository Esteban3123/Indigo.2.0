CREATE TABLE [dbo].[CHINTERFA] (
    [CONSECUTI] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [FECINIEST] DATETIME                                                                         NOT NULL,
    [FECFINEST] DATETIME                                                                         NOT NULL,
    [REGDIAEST] TINYINT                                                                          NOT NULL,
    [NUMORDSER] CHAR (10)                                                                        NOT NULL,
    [INTFECHAD] DATETIME                                                                         NOT NULL,
    [INTUSUARI] CHAR (3)                                                                         NOT NULL,
    [INTUSUIND] CHAR (3)                                                                         NOT NULL,
    [INTUSUWIN] CHAR (60)                                                                        NOT NULL,
    [INTESTRED] CHAR (60)                                                                        NOT NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_CHINTERFA] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_CHINTERFA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CHINTERFA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHINTERFA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC). Identificador único para rastreo y control de cambios en registros de interfaz, referencia a eventos auditados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estación de trabajo (CHAR 60). Nombre o identificador del equipo/computador desde donde se creó el documento en la red corporativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estacion de Trabajo desde donde se creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTESTRED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Windows (CHAR 60). Credencial de usuario del sistema operativo que creó el registro en la interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Windows que creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUWIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Indigo (CHAR 3). Código de usuario registrado en el ERP/EHR Indigo que generó el documento o interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Indigo que creo el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó documento (CHAR 3). Identificador del usuario responsable de la creación del registro en la interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo el documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del documento (DATETIME). Timestamp exacto de generación y registro en el sistema de la interfaz.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion del Documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'INTFECHAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden de servicio (CHAR 10). Identificador de la orden u orden de servicio asociada al ingreso, referencia a procedimiento o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Orden de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMORDSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de estancia (TINYINT). Cantidad total de días de hospitalización o permanencia del paciente en la unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Dias de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'REGDIAEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de estancia (DATETIME). Fecha de egreso, alta o cierre del período de hospitalización del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECFINEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECFINEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECFINEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de estancia (DATETIME). Fecha de ingreso, admisión o comienzo del período de hospitalización del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECINIEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECINIEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'FECINIEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10). Identificador único del ingreso hospitalario o atención, clave foránea a tabla ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII enmascarado). Cédula, identificación o documento del paciente, clave foránea a tabla INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de registro (CHAR 10). Identificador único secuencial de la interfaz, clave primaria. Ej: 00000010.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de registro  Identificador: 00000010', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de interfaz o integración entre sistemas: almacena los eventos de sincronización o intercambio de datos de órdenes de servicio por paciente e ingreso, incluyendo los períodos de ejecución, el usuario que intervino y los datos de auditoría de red y sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHINTERFA';
