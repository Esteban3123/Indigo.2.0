CREATE TABLE [dbo].[HCRECEPCONTACTOS] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCREFRECDE] INT           NULL,
    [EMAIL]        VARCHAR (100) NULL,
    [CORRENVIADO]  BIT           NULL,
    CONSTRAINT [PK_HCRECEPCONTACTOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (bit) que identifica si el correo electrónico de recepción de referencia ya fue enviado a través del servicio Windows; bandera de control de envío', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me Identifica si el Correo ya se ha enviado mediante el Servicio Windows', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico (varchar 100) destinataria para el envío de notificaciones de recepción de referencia médica; contacto de destino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo Electronico al cual se le va enviar mensaje de Recepcion de Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (int) que establece relación con la tabla HCREFRECDE; clave foránea hacia registro de referencia recibida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFRECDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla  HCREFRECDE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFRECDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFRECDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (int identity) y clave primaria de la tabla; consecutivo secuencial de registros de contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de contactos de correo electrónico asociados a las referencias o derivaciones de la historia clínica, indicando si el correo de notificación fue enviado exitosamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPCONTACTOS';
