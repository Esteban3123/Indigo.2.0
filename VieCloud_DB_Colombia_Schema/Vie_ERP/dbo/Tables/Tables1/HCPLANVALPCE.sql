CREATE TABLE [dbo].[HCPLANVALPCE] (
    [IDPLANVAL]    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NOT NULL,
    [CODVALORAPCE] INT         NOT NULL,
    CONSTRAINT [PK_HCPLANVALPCE] PRIMARY KEY CLUSTERED ([IDPLANVAL] ASC),
    CONSTRAINT [FK_HCPLANVALPCE_HCPLANCUIENF] FOREIGN KEY ([CODPLANCENF]) REFERENCES [dbo].[HCPLANCUIENF] ([CODPLANCENF]),
    CONSTRAINT [FK_HCVALDIAGPCE_HCPLANVALPCE] FOREIGN KEY ([IDPLANVAL]) REFERENCES [dbo].[HCPLANVALPCE] ([IDPLANVAL]),
    CONSTRAINT [FK_PLANVALPCE_HCVALORAPCE] FOREIGN KEY ([CODVALORAPCE]) REFERENCES [dbo].[HCVALORAPCE] ([CODVALORAPCE])
);




GO



GO





GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCPLANVALPCE]
    ON [dbo].[HCPLANVALPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la valoración de enfermería; identificador que referencia el registro de valoración clínica del paciente en la tabla HCVALORAPCE (INT, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la valoracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de cuidado de enfermería; identificador del plan asistencial vinculado desde tabla HCPLANCUIENF (VARCHAR 3, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la fila; clave primaria auto-incremental que relaciona valoraciones con planes de cuidado de enfermería (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Icentificador de la fila', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los planes de cuidado de enfermería con los valores o parámetros de valoración asignados a cada plan, permitiendo configurar qué ítems de evaluación aplican a cada plan de cuidado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVALPCE';
