CREATE TABLE [dbo].[HCREFRECDE] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECREGIS]      DATETIME      NOT NULL,
    [OBSERVACION]   VARCHAR (200) NULL,
    [FUNCREG]       CHAR (20)     NOT NULL,
    [ENVIAR]        BIT           NULL,
    [FECSISTEMA]    DATETIME      NULL,
    [MEDRECIB]      CHAR (20)     NULL,
    [HCMOTREFRECID] INT           NULL,
    [HCREFRECEPID]  INT           NOT NULL,
    [ESTADO]        NCHAR (10)    NULL,
    [ASUNTO]        VARCHAR (100) NULL,
    [MENSAJE]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCREFRECDE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFRECDE_HCMOTREFREC] FOREIGN KEY ([HCMOTREFRECID]) REFERENCES [dbo].[HCMOTREFREC] ([ID]),
    CONSTRAINT [FK_HCREFRECDE_HCREFRECDE] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCREFRECDE] ([ID]),
    CONSTRAINT [FK_HCREFRECDE_HCREFRECEP] FOREIGN KEY ([HCREFRECEPID]) REFERENCES [dbo].[HCREFRECEP] ([ID])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido del mensaje de correo electrónico a enviar en la respuesta de referencia/interconsulta. Texto descriptivo de la comunicación al profesional de salud receptor. VARCHAR(MAX), puede contener observaciones clínicas, justificación de aceptación/rechazo o instrucciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna me almacena el Mensaje que se debe enviar en el correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'MENSAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea de asunto del correo electrónico a enviar. Identificador temático de la referencia/interconsulta (ej: ''''Respuesta Referencia Paciente'''', ''''Rechazo Interconsulta''''). VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ASUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna me almacena el Asunto que se debe enviar en el correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ASUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ASUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la respuesta de referencia/interconsulta: 1=Aceptado, 2=No Aceptado/Rechazado, 3=Sin Definir Conducta/Pendiente, 4=Cancelado. NCHAR(10), controla el flujo clínico del proceso de derivación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Aceptado  2- No Aceptado  3- Sin Definir Conducta  4- Cancelado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la referencia/interconsulta padre en tabla HCREFRECEP. Vincula la respuesta con la solicitud original de derivación entre centros o profesionales. INT, clave foránea requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) opcional del motivo de rechazo de la referencia en tabla HCMOTREFREC. Clasifica la razón por la cual se rechaza la derivación (ej: paciente fuera de rango, recurso no disponible). INT, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'HCMOTREFRECID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del motivo de rechazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'HCMOTREFRECID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'HCMOTREFRECID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o cédula del profesional de salud (médico/especialista) que recibe y responde la referencia/interconsulta. CHAR(20), profesional destino de la derivación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'MEDRECIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medico que recibe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'MEDRECIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'MEDRECIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro automático por el sistema ERP/EHR. DATETIME, marca de auditoría generada al guardar el registro en base de datos. Puede diferir de FECREGIS si hay retrasos de procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FECSISTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FECSISTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FECSISTEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de control de envío: 1=Debe enviar correo electrónico, 0=No enviar correo. BIT, determina si se dispara la acción de notificación al profesional receptor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ENVIAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-> Debe enviar correo    0-> No debe enviar correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ENVIAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ENVIAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del funcionario/usuario que registra la respuesta de referencia en el sistema. CHAR(20), trazabilidad de quién carga la información (médico, secretaria, auxiliar administrativo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FUNCREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Funcionario que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FUNCREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FUNCREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto adicional con notas, comentarios clínicos o administrativos sobre la respuesta de referencia/interconsulta. VARCHAR(200), nullable, complementa MENSAJE con información contextual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora seleccionada por el usuario para registrar la respuesta de referencia/interconsulta. DATETIME, puede no coincidir con FECSISTEMA si hay entrada retroactiva o programada. Refleja el momento clínico relevante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FECREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que el usuario selecciono para Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FECREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'FECREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincremental de la respuesta de referencia/interconsulta. INT IDENTITY(1,1), clave primaria de la tabla HCREFRECDE. Secuencial para todas las respuestas registradas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de recepción de referencias y contrareferencias clínicas: documenta cuándo y por quién se recibió cada referencia de paciente, con observaciones, motivo, mensajes y estado del proceso de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFRECDE';
