CREATE TABLE [dbo].[HCVERFVEN] (
    [AUTO]      NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECUTI] NUMERIC (18) NOT NULL,
    [FECHCFENF] DATETIME     NOT NULL,
    [CODUSUARI] CHAR (20)    NOT NULL,
    CONSTRAINT [PK_HCVENPSAL] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCVERFVEN_HCCTRVENP] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCCTRVENP] ([CONSECUTI])
);


GO
ALTER TABLE [dbo].[HCVERFVEN] NOCHECK CONSTRAINT [FK_HCVERFVEN_HCCTRVENP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que verifica la vena', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVERFVEN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la historia clinica de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVERFVEN', @level2type = N'COLUMN', @level2name = N'FECHCFENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Autornumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVERFVEN', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVERFVEN', @level2type = N'COLUMN', @level2name = N'AUTO';

