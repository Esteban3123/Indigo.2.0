CREATE TABLE [dbo].[SOLPRODUC] (
    [PRODAUTON]  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PRODCODIGO] VARCHAR (20)  NOT NULL,
    [PRODNOMBRE] VARCHAR (255) NOT NULL,
    [PRODESTADO] BIT           NULL,
    [PRODOBSER]  VARCHAR (500) NULL,
    [PRODCODCAT] INT           NOT NULL,
    [PRODUCIVA]  TINYINT       NULL,
    [PROUNIMED]  INT           CONSTRAINT [DFX_0B2D2080] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SOLPRODUC] PRIMARY KEY CLUSTERED ([PRODAUTON] ASC),
    CONSTRAINT [FK_SOLPRODUC_SOLCATEGO] FOREIGN KEY ([PRODCODCAT]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON]),
    CONSTRAINT [FK_SOLPRODUC_SOLIVAPRO] FOREIGN KEY ([PRODUCIVA]) REFERENCES [dbo].[SOLIVAPRO] ([SOAUTOIVA]),
    CONSTRAINT [FK_SOLPRODUC_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON]),
    CONSTRAINT [FK_SOLPRODUC_SOLUNIMED] FOREIGN KEY ([PROUNIMED]) REFERENCES [dbo].[SOLUNIMED] ([UNIMEDAUT])
);


GO
ALTER TABLE [dbo].[SOLPRODUC] NOCHECK CONSTRAINT [FK_SOLPRODUC_SOLCATEGO];


GO
ALTER TABLE [dbo].[SOLPRODUC] NOCHECK CONSTRAINT [FK_SOLPRODUC_SOLIVAPRO];


GO
ALTER TABLE [dbo].[SOLPRODUC] NOCHECK CONSTRAINT [FK_SOLPRODUC_SOLPRODUC];


GO
ALTER TABLE [dbo].[SOLPRODUC] NOCHECK CONSTRAINT [FK_SOLPRODUC_SOLUNIMED];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la unidad de medida del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PROUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del iva del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODUCIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODCAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene las observaciondes del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODOBSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el estado del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el nombre del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPRODUC', @level2type = N'COLUMN', @level2name = N'PRODAUTON';

