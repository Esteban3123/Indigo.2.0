CREATE TABLE [dbo].[SOLANULA] (
    [AUTO]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTON]  INT           NOT NULL,
    [FECHANUL]  SMALLDATETIME NOT NULL,
    [MOTIVOS]   VARCHAR (800) NOT NULL,
    [CODUSUARI] CHAR (20)     NOT NULL,
    CONSTRAINT [PK_SOLANULA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOLANULA_SOLCOMPRA] FOREIGN KEY ([COMAUTON]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON])
);


GO
ALTER TABLE [dbo].[SOLANULA] NOCHECK CONSTRAINT [FK_SOLANULA_SOLCOMPRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del usuario que realiza la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'motivos de la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'MOTIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'FECHANUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumerico de la solicitud que se anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'COMAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLANULA', @level2type = N'COLUMN', @level2name = N'AUTO';

