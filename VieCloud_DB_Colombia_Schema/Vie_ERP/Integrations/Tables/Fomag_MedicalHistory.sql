CREATE TABLE [Integrations].[Fomag_MedicalHistory] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Identificador] VARCHAR (50)    NOT NULL,
    [FechaEnvio]    DATETIME2 (7)   NOT NULL,
    [Estado]        INT             NOT NULL,
    [Resultado]     NVARCHAR (4000) NULL,
    CONSTRAINT [PK__Fomag_Me__3214EC0723DBF3D9] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de envíos de historias clínicas a FOMAG (Fondo de Prestaciones Sociales del Magisterio). Guarda el estado y resultado de cada transmisión realizada hacia esa entidad.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de envío.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o referencia del registro de historia clínica enviado a FOMAG; puede corresponder al número de ingreso, episodio o documento asociado.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Identificador';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Identificador';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó el envío o transmisión del registro a FOMAG.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'FechaEnvio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'FechaEnvio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del envío: indica si fue exitoso, pendiente, fallido u otro resultado del proceso de integración.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Estado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta o mensaje detallado devuelto por FOMAG tras el envío; puede incluir errores, confirmaciones o códigos de respuesta.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Resultado';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'Fomag_MedicalHistory', @level2type = N'COLUMN', @level2name = N'Resultado';
