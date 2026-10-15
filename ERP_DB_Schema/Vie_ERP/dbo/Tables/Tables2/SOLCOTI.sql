CREATE TABLE [dbo].[SOLCOTI] (
    [AUTO]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODUSUARI]  CHAR (20)     NOT NULL,
    [FECHCOTI]   DATETIME      NOT NULL,
    [FECHREGIS]  DATE          NOT NULL,
    [VIGENCOTI]  DATE          NOT NULL,
    [PLAZOCOTI]  INT           NOT NULL,
    [FORMACOTI]  TINYINT       NOT NULL,
    [OBSERCOTI]  VARCHAR (500) NULL,
    [PROVECOTI]  NUMERIC (18)  NOT NULL,
    [ESTADCOTI]  CHAR (1)      NULL,
    [NOMARCHIVO] VARCHAR (MAX) NULL,
    [CODCENATE]  CHAR (10)     NULL,
    CONSTRAINT [PK_SOLCOTI_1] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOLCOTI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_SOLCOTI_SOLCOTI] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[SOLCOTI] ([AUTO]),
    CONSTRAINT [FK_SOLCOTI_SOLPROVEE] FOREIGN KEY ([PROVECOTI]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO]),
    CONSTRAINT [FK_SOLCOTI_SOLTIPAGO] FOREIGN KEY ([FORMACOTI]) REFERENCES [dbo].[SOLTIPAGO] ([Autonumerico])
);


GO
ALTER TABLE [dbo].[SOLCOTI] NOCHECK CONSTRAINT [FK_SOLCOTI_ADCENATEN];


GO
ALTER TABLE [dbo].[SOLCOTI] NOCHECK CONSTRAINT [FK_SOLCOTI_SOLCOTI];


GO
ALTER TABLE [dbo].[SOLCOTI] NOCHECK CONSTRAINT [FK_SOLCOTI_SOLPROVEE];


GO
ALTER TABLE [dbo].[SOLCOTI] NOCHECK CONSTRAINT [FK_SOLCOTI_SOLTIPAGO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el nombre de de los archivos adjuntos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'NOMARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el estado de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'ESTADCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PROVECOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene las observaciones de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'OBSERCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la el autonumerico de la forma de pago de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FORMACOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el plazo de entrega de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'PLAZOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la vigencia de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'VIGENCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la fecha que se registra la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la fecha de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'FECHCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo de usuario que realiza la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTI', @level2type = N'COLUMN', @level2name = N'AUTO';

