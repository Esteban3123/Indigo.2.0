CREATE TABLE [dbo].[HCHOJAGASTOQX] (
    [ID]                 INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECUTIVO]        VARCHAR (20)                                                                     NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [IDAGEPROGQX]        INT                                                                              NOT NULL,
    [FECHAREGISTRO]      DATETIME                                                                         NOT NULL,
    [ESTADO]             INT                                                                              NOT NULL,
    [USUARIODESCONFIRMA] CHAR (20)                                                                        NULL,
    [FECHADESCONFIRMA]   DATETIME                                                                         NULL,
    [ConcurrencyControl] ROWVERSION                                                                       NULL,
    [UserTryingConfirm]  CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCHOJAGASTOQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOJAGASTOQX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHOJAGASTOQX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHOJAGASTOQX].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario que intenta confirmar la hoja de gasto QX, registra quién inicia el proceso de aceptación/validación de gastos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'UserTryingConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de usuario que trata de realizar la confirmacion de la Hoja gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'UserTryingConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'UserTryingConfirm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de versión TIMESTAMP (Optimistic Locking) que gestiona accesos concurrentes múltiples a la hoja de gasto QX, evita sobrescrituras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'controla la concurrencia multiple de una hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se ejecutó la desconfirmación (reversión) del estado confirmado en la hoja de gasto quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHADESCONFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de desconfirmacion Hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHADESCONFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHADESCONFIRMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario que revierte/desconfirma la hoja de gasto QX, anulando confirmación previa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'USUARIODESCONFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario desconfirma Hoja de gasto QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'USUARIODESCONFIRMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'USUARIODESCONFIRMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del procesamiento de la hoja de gasto quirúrgico: 1=Sin confirmar (hoja sin procesar), 2=En espera de aceptación de devolutivo (bloqueada), 3=Devolución parcial aceptada, 4=Confirmada (todo aceptado/gastado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Sin confirmar (hoja de gasto QX  sin procesar)  2- En espera aceptacion devolutivo(bloquea hoja gasto QX a la espera de proceso de devoluciones)  3 -Devolucion parcial aceptada (cuando se acepta una parte)  4 - Confirmada (cuando se acepta todo devolutivo y/o cuando se gasto todo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/generación del registro de la hoja de gasto QX en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la programación quirúrgica principal (agendamiento de cirugía), vinculada a AGEPROGQX para relacionar gastos con el procedimiento quirúrgico específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la programacion de cirugia principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente en el evento clínico, referencia a ADINGRESO para asociar la hoja de gasto al episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula/documento de identificación del paciente (PII ofuscado), vinculada a la tabla INPACIENT para identificar al beneficiario de la atención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cedula del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo secuencial de la hoja de gasto QX, referencia INCONSECU para rastreo de gastos quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo INCONSECU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) de la hoja de gasto quirúrgico QX en la tabla HCHOJAGASTOQX.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de hojas de gastos de cirugía (quirófano) asociadas a cada paciente e ingreso. Controla el consecutivo del documento, su estado de confirmación y los usuarios que intervienen en el proceso de confirmación o desconfirmación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQX';
