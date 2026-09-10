CREATE TABLE [dbo].[HCVACADIPAC] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDVACUNA]      INT                                                                              NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [FECHAPLICAVAC] DATETIME                                                                         NULL,
    [DOSIS]         VARCHAR (60)                                                                     NULL,
    [OBSVACUNA]     CHAR (200)                                                                       NULL,
    [MOTNOAPLICA]   TINYINT                                                                          NULL,
    [CODCENATE]     CHAR (10)                                                                        NULL,
    [CODENTIDA]     CHAR (9)                                                                         NULL,
    [GENCONENTITY]  INT                                                                              NULL,
    [LOTEDOSIS]     VARCHAR (20)                                                                     NULL,
    [LOTEJERINGA]   VARCHAR (20)                                                                     NULL,
    [ADMOTRINS]     BIT                                                                              NULL,
    [APLICADA]      BIT                                                                              NULL,
    [NUMINGRES]     CHAR (10)                                                                        NULL,
    [USUREGISTRA]   VARCHAR (200)                                                                    NULL,
    CONSTRAINT [PK_HCVACADIPAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCVACADIPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCVACADIPAC_HCVACUADI] FOREIGN KEY ([IDVACUNA]) REFERENCES [dbo].[HCVACUADI] ([ID]),
    CONSTRAINT [FK_HCVACADIPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCVACADIPAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró el evento de vacunación; nombre o login del profesional/personal que documentó la aplicación o no aplicación de la vacuna en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario resgistrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'USUREGISTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente a la institución; identificador que se guarda solo cuando el registro se realiza desde Historia Clínica en el contexto de una atención/hospitalización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso que se guarda solo cuando el registro se hace  dede una Historia clinica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de administración efectiva de la vacuna (1=Aplicada, 0=No Aplicada); booleano que refleja si la dosis fue realmente inoculada al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'APLICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica si la vacuna fue aplicada 1=Si 0=NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'APLICADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'APLICADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de administración por tercera institución (1=Sí administrada en otra IPS, 0=No); registra si la vacunación fue realizada en un centro de atención diferente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ADMOTRINS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica si la vacuna fue administrada por otra institución 1=Si 0=NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ADMOTRINS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ADMOTRINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote de la jeringa utilizada en la inoculación; código de identificación del dispositivo de inyección para trazabilidad y control de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEJERINGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote de la jeringa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEJERINGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEJERINGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del biológico vacunal; identificador del lote de la dosis administrada, requerido para farmacovigilancia y recall de vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote de Dosis de la vacuna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'LOTEDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empresa o entidad administradora nativa que gestiona el contrato de vacunación; ID de la institución prestadora de salud responsable del programa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Empresa administradora nativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora o administradora del riesgo en salud (EPS/ARS); identificador único de la asegurador/pagador del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro, sede o unidad funcional donde se aplicó la vacuna; identificador de la IPS, clínica, puesto de vacunación o unidad médica específica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de no aplicación de la vacuna con categorías: 0=Aplicada, 1=Tradición/costumbre, 2=Contraindicación médica/salud, 3=Rechazo del usuario, 4=Datos de contacto no actualizado, 5=Otras razones; clasificación de barreras a la inmunización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'MOTNOAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo no Aplicación  0- Aplicada  1- No se Administra por una Tradición,   2- No se Administra por una Condición de Salud,  3- No se Administra por Negación del Usuario,  4- No se Administra por tener datos del contacto del usuario no actualizado,   5- No se administra por otras razones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'MOTNOAPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'MOTNOAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas clínicas o comentarios relevantes sobre la aplicación, efectos adversos, reacciones o circunstancias especiales de la vacunación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción/identificación de la dosis aplicada (ej: dosis 1, dosis 2, refuerzo, esquema); especificación del número o tipo de dosis en el esquema de vacunación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta de aplicación o intento de aplicación de la vacuna; timestamp crítico para cronograma de vacunación y seguimiento epidemiológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'FECHAPLICAVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de aplicacion de la vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'FECHAPLICAVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'FECHAPLICAVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, documento de identidad, número de afiliación); identificación PII ofuscada para vincular el registro con la historia clínica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del tipo, marca o esquema de vacuna en el catálogo institucional; llave foránea que referencia el registro de definición de biológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'IDVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo/secuencial) del registro de aplicación de vacuna a paciente; clave primaria de la tabla HCVACADIPAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del esquema de vacunación aplicado a cada paciente: qué vacuna se administró, cuándo, la dosis, el lote utilizado y si fue efectivamente aplicada. Permite hacer seguimiento al historial de inmunización del paciente durante sus atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVACADIPAC';
