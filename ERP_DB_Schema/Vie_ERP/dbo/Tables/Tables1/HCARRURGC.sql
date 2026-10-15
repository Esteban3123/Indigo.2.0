CREATE TABLE [dbo].[HCARRURGC] (
    [AUTOARRIB] NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODARRURG] CHAR (3)     NOT NULL,
    [DESARRURG] CHAR (60)    NOT NULL,
    [TIPARRURG] CHAR (1)     NOT NULL,
    CONSTRAINT [PK_HCARRIURG] PRIMARY KEY CLUSTERED ([AUTOARRIB] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Arribo Urgencias   1:Medio de Transporte  2:Estado Fisico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'TIPARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'DESARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'CODARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGC', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';

