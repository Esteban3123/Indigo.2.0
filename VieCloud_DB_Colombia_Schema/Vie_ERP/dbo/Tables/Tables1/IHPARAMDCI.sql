CREATE TABLE [dbo].[IHPARAMDCI] (
    [ID]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDCIMEDPADRE] VARCHAR (20) NOT NULL,
    [CODDCIMED]      VARCHAR (20) NOT NULL,
    [TIPO]           INT          NOT NULL,
    [FECHAREG]       DATETIME     NOT NULL,
    [USUARIOREG]     CHAR (20)    NOT NULL,
    CONSTRAINT [PK_IHPARAMDCI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IHPARAMDCI_IHDCIMEDI] FOREIGN KEY ([CODDCIMED]) REFERENCES [dbo].[IHDCIMEDI] ([CODDCIMED]),
    CONSTRAINT [FK_IHPARAMDCI_IHDCIMEDI1] FOREIGN KEY ([CODDCIMEDPADRE]) REFERENCES [dbo].[IHDCIMEDI] ([CODDCIMED]),
    CONSTRAINT [UK_IHPARAMDCI] UNIQUE NONCLUSTERED ([CODDCIMEDPADRE] ASC, [CODDCIMED] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Registro (VARCHAR 20). Identificación del usuario que registró la relación entre medicamentos DCI. Auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'USUARIOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'USUARIOREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'USUARIOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Registro (DATETIME). Timestamp del momento en que se registró la relación entre medicamentos DCI padre e hijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'FECHAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Relación DCI (INT). Clasificación: 1=Suena igual, 2=Se parece, 3=Suena igual y se parece. Define similitud entre medicamentos DCI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Suena Igual a  2- Se parece a  3- Suena igual y se parece a', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código Medicamento DCI - Detalle (VARCHAR 20, FK IHDCIMEDI). Identificador del medicamento DCI hijo en la relación jerárquica. Referencia a catálogo de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Medicamento DCI - Detalle', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código DCI Padre (VARCHAR 20, FK IHDCIMEDI). Identificador del medicamento DCI padre en la relación jerárquica. Define medicamento principal o genérico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMEDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo DCI padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMEDPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'CODDCIMEDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador Único (INT IDENTITY). Clave primaria auto-incremental de la tabla de parámetros de relaciones DCI medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de relaciones entre medicamentos DCI (Denominación Común Internacional), registrando la asociación entre un medicamento padre y sus medicamentos relacionados, con el tipo de relación, fecha y usuario que realizó el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAMDCI';
