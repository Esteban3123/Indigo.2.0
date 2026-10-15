CREATE TABLE [dbo].[HCDIAGINTPCE] (
    [IDDIAGINT]    INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODINTPCE]    INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NOT NULL,
    [CODVALORAPCE] INT         NOT NULL,
    [CODDIAGENF]   VARCHAR (5) NOT NULL,
    [IDHCCLASENF]  INT         NULL,
    CONSTRAINT [PK_HCDIAGINTPCE] PRIMARY KEY CLUSTERED ([IDDIAGINT] ASC),
    CONSTRAINT [FK_HCDIAGINTPCE_HCINTERVENPCE] FOREIGN KEY ([CODINTPCE]) REFERENCES [dbo].[HCINTERVENPCE] ([CODINTPCE])
);


GO
ALTER TABLE [dbo].[HCDIAGINTPCE] NOCHECK CONSTRAINT [FK_HCDIAGINTPCE_HCINTERVENPCE];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCDIAGINTPCE]
    ON [dbo].[HCDIAGINTPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODINTPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda los id de clases de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Código diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'código de valoración plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda codigo de plan de cuidad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la intervención plan cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'CODINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el diagnostico de intervención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGINTPCE', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';

