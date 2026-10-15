CREATE TABLE [dbo].[SOLTIPAGO] (
    [Autonumerico] TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]       VARCHAR (3)  NOT NULL,
    [Descripcion]  VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SOLTIPAG] PRIMARY KEY CLUSTERED ([Autonumerico] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la descripcion del tipo de pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Descripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del tipo de pago', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLTIPAGO', @level2type = N'COLUMN', @level2name = N'Autonumerico';

