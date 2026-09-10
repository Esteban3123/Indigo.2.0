CREATE TABLE [dbo].[HCREFCONTDET] (
    [ID]            INT                                                                          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCREFCONTAUTO] INT                                                                          NOT NULL,
    [CODDIAGNO]     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HCREFCONTDET] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFCONTDET_HCREFCONT] FOREIGN KEY ([HCREFCONTAUTO]) REFERENCES [dbo].[HCREFCONT] ([AUTO]),
    CONSTRAINT [FK_HCREFCONTDET_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCREFCONTDET] NOCHECK CONSTRAINT [FK_HCREFCONTDET_HCREFCONT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREFCONTDET].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CIE-10), con enmascaramiento de datos sensibles. Referencia a catálogo de diagnósticos. Sinónimos: código diagnóstico, diagnóstico, patología, enfermedad, condición clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la referencia de contención en historia clínica. Clave foránea que vincula el detalle con el registro principal de referencia y control (tabla HCREFCONT). Sinónimos: referencia de contención, control de referencia, número de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'HCREFCONTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de referencia HCREFCONT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'HCREFCONTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'HCREFCONTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) del registro de detalle de referencia de contención. Clave primaria de la tabla HCREFCONTDET. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de diagnósticos asociados a una referencia o remisión clínica. Registra cada código de diagnóstico (CIE-10) vinculado a un encabezado de referencia/contrareferencia de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET';
