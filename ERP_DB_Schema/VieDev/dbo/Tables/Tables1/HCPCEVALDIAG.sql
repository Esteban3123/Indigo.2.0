CREATE TABLE [dbo].[HCPCEVALDIAG] (
    [IDVALDIAG]      INT         IDENTITY (1, 1) NOT NULL,
    [IDPLANVAL]      INT         NOT NULL,
    [IDHCCLASENF]    INT         NULL,
    [CODDIAGENF]     VARCHAR (5) NOT NULL,
    [CODESCMEDPCE]   INT         NULL,
    [IDHCPCECONTROL] INT         NOT NULL,
    CONSTRAINT [PK_HCPCEVALDIAG] PRIMARY KEY CLUSTERED ([IDVALDIAG] ASC),
    CONSTRAINT [FK_HCPCEVALDIAG_HCDIAGENF] FOREIGN KEY ([CODDIAGENF]) REFERENCES [dbo].[HCDIAGENF] ([CODDIAGENF]),
    CONSTRAINT [FK_HCPCEVALDIAG_HCPCEPLANVAL] FOREIGN KEY ([IDPLANVAL]) REFERENCES [dbo].[HCPCEPLANVAL] ([IDPLANVAL])
);


GO
ALTER TABLE [dbo].[HCPCEVALDIAG] NOCHECK CONSTRAINT [FK_HCPCEVALDIAG_HCDIAGENF];


GO
ALTER TABLE [dbo].[HCPCEVALDIAG] NOCHECK CONSTRAINT [FK_HCPCEVALDIAG_HCPCEPLANVAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el id del control del plan de cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Codigo del registo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODESCMEDPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'id valor diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEVALDIAG', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';

