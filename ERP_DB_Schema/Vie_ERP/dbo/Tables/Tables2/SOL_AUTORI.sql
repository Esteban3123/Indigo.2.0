CREATE TABLE [dbo].[SOL_AUTORI] (
    [AUTO]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTO]    INT           NOT NULL,
    [FECHAUTORI] SMALLDATETIME NOT NULL,
    [CODUSUARI]  CHAR (20)     NOT NULL,
    [NOVEDADES]  BIT           NULL,
    CONSTRAINT [PK_SOL_AUTORI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOL_AUTORI_SOLCOMPRA] FOREIGN KEY ([COMAUTO]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON])
);


GO
ALTER TABLE [dbo].[SOL_AUTORI] NOCHECK CONSTRAINT [FK_SOL_AUTORI_SOLCOMPRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene las novedades de la utorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'NOVEDADES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene el codigo del usuario que autoriza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene la fecha de autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'FECHAUTORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene el autonumerico de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'COMAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOL_AUTORI', @level2type = N'COLUMN', @level2name = N'AUTO';

