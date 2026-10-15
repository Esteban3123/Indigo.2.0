CREATE TABLE [dbo].[HCCLASPCE] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [CODPLANCENF]  VARCHAR (50) NOT NULL,
    [CODVALORAPCE] INT          NOT NULL,
    [IDHCCLASENF]  INT          NOT NULL,
    CONSTRAINT [PK_HCCLASPCE] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_HCCLASPCE_HCCLASENF] FOREIGN KEY ([IDHCCLASENF]) REFERENCES [dbo].[HCCLASENF] ([Id])
);


GO
ALTER TABLE [dbo].[HCCLASPCE] NOCHECK CONSTRAINT [Fk_HCCLASPCE_HCCLASENF];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCCLASPCE]
    ON [dbo].[HCCLASPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HCCLASENF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCLASPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la valoración del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCLASPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCLASPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCLASPCE', @level2type = N'COLUMN', @level2name = N'Id';

