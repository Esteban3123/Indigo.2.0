CREATE TABLE [dbo].[AGCAUCANC] (
    [CODCAUCAN]  CHAR (3)       NOT NULL,
    [DESCAUCAN]  NVARCHAR (150) NOT NULL,
    [ESTCAUCAN]  BIT            NOT NULL,
    [MOSTRARWEB] BIT            NULL,
    CONSTRAINT [PK_AGCAUCANC] PRIMARY KEY CLUSTERED ([CODCAUCAN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de visibilidad de citas canceladas en portal web (1=Visible, 0=Oculto). Controla si la causa de cancelación se muestra al paciente en autogestión web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo de la causa de cancelación. Bit booleano: 1=Activa (disponible para cancelar citas), 0=Inactiva (descontinuada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado(Activo o inactivo) de la Causa de cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la causa de cancelación de citas o atenciones. Ejemplo: ''''Paciente solicita'''', ''''Médico cancela'''', ''''No asistencia'''', ''''Urgencia''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Causa de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único alfanumérico (3 caracteres) identificador de la causa de cancelación. Clave primaria para registrar motivos de cancelación de citas, procedimientos o atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Causa de Cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de causas de cancelación de citas o agendamientos. Registra los motivos por los cuales se puede cancelar una cita médica, su estado activo/inactivo y si se muestra en el portal web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC';
