CREATE TABLE [dbo].[HCDIAGCAUPCE] (
    [IDDIAGCAU]    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCAUSAPCE]  INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NULL,
    [CODVALORAPCE] INT         NULL,
    [CODDIAGENF]   VARCHAR (5) NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [FK_HCDIAGCAUPCE] PRIMARY KEY CLUSTERED ([IDDIAGCAU] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCDIAGCAUPCE]
    ON [dbo].[HCDIAGCAUPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODCAUSAPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Código valoración  diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'código de valoración plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo de la causa plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Id del daignostico de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGCAUPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGCAU';

