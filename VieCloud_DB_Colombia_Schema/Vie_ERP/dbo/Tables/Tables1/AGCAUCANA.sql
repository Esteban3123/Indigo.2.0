CREATE TABLE [dbo].[AGCAUCANA] (
    [CODCAUCAN] CHAR (3)       NOT NULL,
    [DESCAUCAN] NVARCHAR (150) NOT NULL,
    [ESTCAUCAN] BIT            NOT NULL,
    CONSTRAINT [PK_AGCAUCANA] PRIMARY KEY CLUSTERED ([CODCAUCAN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la causa de cancelación de agenda (Activo/Inactivo); indicador BIT que determina si la razón de cancelación está disponible para uso en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado(Activo o inactivo) de la Causa de cancelación de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la causa de cancelación de agenda (ej: enfermedad del paciente, ausencia profesional, urgencia médica); texto hasta 150 caracteres que explica el motivo de cancelación de citas agendadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Causa de cancelacion de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único de la causa de cancelación de agenda (3 caracteres alfanuméricos); clave primaria que vincula razones de cancelación en registros de agendamientos cancelados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Causa de Cancelacion de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de causas de cancelación o anulación utilizadas en el módulo de agendamiento. Cada registro define un motivo válido por el cual se puede cancelar una cita o agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANA';
