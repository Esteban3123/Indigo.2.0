CREATE TABLE [dbo].[HCINTACTPCE] (
    [IDINTACT]     INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODINTPCE]    INT         NOT NULL,
    [CODACTPCE]    INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NOT NULL,
    [CODVALORAPCE] INT         NOT NULL,
    [CODDIAGENF]   VARCHAR (5) NOT NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [PK_HCINTACTPCE] PRIMARY KEY CLUSTERED ([IDINTACT] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCINTACTPCE]
    ON [dbo].[HCINTACTPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODINTPCE] ASC, [CODACTPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la clase', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código dominio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad PCE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODACTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTACTPCE', @level2type = N'COLUMN', @level2name = N'IDINTACT';

