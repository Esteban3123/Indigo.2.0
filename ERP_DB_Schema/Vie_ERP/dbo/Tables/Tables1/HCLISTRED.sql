CREATE TABLE [dbo].[HCLISTRED] (
    [CODCONSEC]  INT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONSECC] INT        NULL,
    [CODCONSECD] NCHAR (10) NULL,
    [CODRESPES]  INT        NOT NULL,
    CONSTRAINT [PK_HCLISTRED] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_HCLISTRED_HCLISTRED] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[HCLISTRED] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[HCLISTRED] NOCHECK CONSTRAINT [FK_HCLISTRED_HCLISTRED];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- chequeada      0 - no chequeado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODRESPES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo detalle de lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo tabla Cabecera de Respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTRED', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

