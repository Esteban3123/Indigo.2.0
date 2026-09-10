CREATE TABLE [dbo].[HCREFCONTACTOS] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCREFCONTD] INT           NOT NULL,
    [EMAIL]        VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_HCREFCONTACTOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFCONTACTOS_HCREFCONTD] FOREIGN KEY ([IDHCREFCONTD]) REFERENCES [dbo].[HCREFCONTD] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico, dirección de email para contacto y notificaciones de la referencia (PII - datos personales sensibles)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la gestión de referencia, clave foránea que vincula el contacto con el registro principal de referencia/derivación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONTD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la gestion de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONTD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONTD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial de la tabla, clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de correos electrónicos de contacto asociados a referencias o remisiones de historia clínica. Cada fila representa un email vinculado a un detalle de contacto de referencia del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTACTOS';
