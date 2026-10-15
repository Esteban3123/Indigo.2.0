CREATE TABLE [dbo].[HCVALOBJPCE] (
    [IDVALOBJ]     INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODOBJPCE]    INT         NOT NULL,
    [CODPLANCENF]  VARCHAR (3) NULL,
    [CODVALORAPCE] INT         NULL,
    [IDHCCLASENF]  INT         NULL,
    [CODDIAGENF]   VARCHAR (5) NULL,
    CONSTRAINT [FK_HCVALOBJPCE] PRIMARY KEY CLUSTERED ([IDVALOBJ] ASC),
    CONSTRAINT [FK_HCVALOBJPCE_HCOBJPCE] FOREIGN KEY ([CODOBJPCE]) REFERENCES [dbo].[HCOBJPCE] ([CODOBJPCE])
);


GO
ALTER TABLE [dbo].[HCVALOBJPCE] NOCHECK CONSTRAINT [FK_HCVALOBJPCE_HCOBJPCE];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCVALOBJPCE]
    ON [dbo].[HCVALOBJPCE]([CODPLANCENF] ASC, [CODVALORAPCE] ASC, [IDHCCLASENF] ASC, [CODDIAGENF] ASC, [CODOBJPCE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  codigo de diagnostico enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODDIAGENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id clases enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'codigo de valoracion plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODVALORAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda codigo de plan de cuidad de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODPLANCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo objtos de planes  de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'CODOBJPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id valoresobjetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALOBJPCE', @level2type = N'COLUMN', @level2name = N'IDVALOBJ';

