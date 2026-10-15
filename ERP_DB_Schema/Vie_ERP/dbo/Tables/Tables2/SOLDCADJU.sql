CREATE TABLE [dbo].[SOLDCADJU] (
    [Autonumerico] TINYINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FechCre]      DATE            NOT NULL,
    [CodUsuCrea]   TINYINT         NULL,
    [DocAdju]      VARBINARY (MAX) NOT NULL,
    [PROVAUTO]     NUMERIC (18)    NULL,
    [COTIAUTON]    INT             NULL,
    [PRODAUTON]    INT             NULL,
    CONSTRAINT [PK_SOLDOCADJU] PRIMARY KEY CLUSTERED ([Autonumerico] ASC),
    CONSTRAINT [FK_SOLDCADJU_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON]),
    CONSTRAINT [FK_SOLDCADJU_SOLPROVEE] FOREIGN KEY ([PROVAUTO]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);


GO
ALTER TABLE [dbo].[SOLDCADJU] NOCHECK CONSTRAINT [FK_SOLDCADJU_SOLPRODUC];


GO
ALTER TABLE [dbo].[SOLDCADJU] NOCHECK CONSTRAINT [FK_SOLDCADJU_SOLPROVEE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'COTIAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PROVAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el documento adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'DocAdju';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'CodUsuCrea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'FechCre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'Autonumerico';

