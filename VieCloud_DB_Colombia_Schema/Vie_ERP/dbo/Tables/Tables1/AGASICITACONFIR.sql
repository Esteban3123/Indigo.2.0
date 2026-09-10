CREATE TABLE [dbo].[AGASICITACONFIR] (
    [ID]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGASICITA]         INT                                                                              NULL,
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ESTADOCONFIRMAR]     CHAR (1)                                                                         NOT NULL,
    [FECHAHORA]           DATETIME                                                                         NOT NULL,
    [CONTACTOPACIENTE]    BIT                                                                              NOT NULL,
    [NOMBRECONTACTO]      VARCHAR (80)                                                                     NULL,
    [PARENTESCO]          VARCHAR (80)                                                                     NULL,
    [OBSERVACION]         VARCHAR (500)                                                                    NULL,
    [FECHACREACION]       DATETIME                                                                         NOT NULL,
    [USUARIOCREACION]     CHAR (20)                                                                        NOT NULL,
    [FECHAMODIFICACION]   DATETIME                                                                         NULL,
    [USUARIOMODIFICACION] CHAR (20)                                                                        NULL,
    [IDAGEPROGQX]         INT                                                                              NULL,
    CONSTRAINT [PK_AGASICITACONFIR] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGASICITACONFIR_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGASICITACONFIR].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de la tabla AGEPROGQX (programa quirúrgico). FK a programa de cirugía/procedimiento programado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID o Codigo Autonumerico de la tabla AGEPROGQX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación del registro de confirmación de cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de confirmación de asistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro inicial de confirmación de cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que crea el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de confirmación de asistencia a cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre para notas, comentarios o detalles adicionales sobre la confirmación de asistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación familiar o vínculo del contacto respecto al paciente (padre, madre, cónyuge, hijo, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'PARENTESCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parentesco del contacto con el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'PARENTESCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'PARENTESCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona de contacto o acudiente del paciente para confirmación de cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'NOMBRECONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'NOMBRECONTACTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'NOMBRECONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: si se logró contactar al paciente (1=Sí contactado, 0=No contactado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'CONTACTOPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contacto al Paciente ? --> 0=No , 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'CONTACTOPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'CONTACTOPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora programada de la cita u atención a confirmar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora de Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'FECHAHORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de confirmación de asistencia: 1=Confirmado, 2=Cancelado, 3=Sin definir/pendiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ESTADOCONFIRMAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de Confirmacion de Asistencia --> 1= Confirmado , 2 =Cancelado , 3= Sin Definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ESTADOCONFIRMAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ESTADOCONFIRMAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente (cédula, documento de identidad). PII Ofuscado. FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de la tabla AGASICITA (agendamiento de cita). FK a agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID o Codigo Autonumerico de la tabla AGASICITA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGASICITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'IDAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de confirmación de asistencia a cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de confirmaciones de citas médicas agendadas. Guarda el historial de intentos de confirmación de cada cita, indicando si el paciente o un contacto familiar respondió, el estado de la confirmación y las observaciones del proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITACONFIR';
