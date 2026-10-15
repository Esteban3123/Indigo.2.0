CREATE TABLE [dbo].[HCVALDIAGPCE] (
    [IDVALDIAG]    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGENF]   VARCHAR (5) NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NULL,
    [CODVALORAPCE] INT         NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [PK_HCVALDIAGPCE] PRIMARY KEY CLUSTERED ([IDVALDIAG] ASC),
    CONSTRAINT [FK_HCVALDIAGPCE_HCDIAGENF] FOREIGN KEY ([CODDIAGENF]) REFERENCES [dbo].[HCDIAGENF] ([CODDIAGENF])
);


GO
ALTER TABLE [dbo].[HCVALDIAGPCE] NOCHECK CONSTRAINT [FK_HCVALDIAGPCE_HCDIAGENF];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCVALDIAGPCE]
    ON [dbo].[HCVALDIAGPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [CODDIAGENF] ASC, [IDHCCLASENF] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo valoracion plac cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo Plan Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la identidad diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALDIAGPCE', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';

