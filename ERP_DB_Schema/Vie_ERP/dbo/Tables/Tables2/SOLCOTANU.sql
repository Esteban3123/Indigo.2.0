CREATE TABLE [dbo].[SOLCOTANU] (
    [AUTOANULA] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECHANULA] DATETIME      NOT NULL,
    [USUAANULA] CHAR (20)     NOT NULL,
    [COTIANULA] INT           NOT NULL,
    [OBSEANULA] VARCHAR (500) NULL,
    CONSTRAINT [PK_SOLCOTANU] PRIMARY KEY CLUSTERED ([AUTOANULA] ASC),
    CONSTRAINT [FK_SOLCOTANU_SOLCOTI] FOREIGN KEY ([COTIANULA]) REFERENCES [dbo].[SOLCOTI] ([AUTO])
);


GO
ALTER TABLE [dbo].[SOLCOTANU] NOCHECK CONSTRAINT [FK_SOLCOTANU_SOLCOTI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene las observaciones de la anulacion de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'OBSEANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la cotizacion que anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'COTIANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el usuario que anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'USUAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la fecha de anulacion de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'FECHANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTANU', @level2type = N'COLUMN', @level2name = N'AUTOANULA';

