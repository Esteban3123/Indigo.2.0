CREATE TABLE [dbo].[SOLCOMPRA] (
    [COMAUTON]  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTO]      INT            NULL,
    [COMFECHA]  SMALLDATETIME  NOT NULL,
    [COMESTADO] INT            NOT NULL,
    [COMOBSERV] VARCHAR (1000) NULL,
    [COMPRIORI] TINYINT        NULL,
    [CODUSUARI] CHAR (20)      NOT NULL,
    [UFUCODIGO] CHAR (10)      NOT NULL,
    [TITUAGRUS] VARCHAR (50)   NULL,
    [TIPOAGRUP] CHAR (1)       NULL,
    CONSTRAINT [PK_SOLCOM] PRIMARY KEY CLUSTERED ([COMAUTON] ASC),
    CONSTRAINT [FK_SOLCOMPRA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_SOLCOMPRA_SOLCOMPRA] FOREIGN KEY ([COMAUTON]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON]),
    CONSTRAINT [FK_SOLCOMPRA_SOLTIPSOL] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[SOLTIPSOL] ([AUTO])
);


GO
ALTER TABLE [dbo].[SOLCOMPRA] NOCHECK CONSTRAINT [FK_SOLCOMPRA_INUNIFUNC];


GO
ALTER TABLE [dbo].[SOLCOMPRA] NOCHECK CONSTRAINT [FK_SOLCOMPRA_SOLCOMPRA];


GO
ALTER TABLE [dbo].[SOLCOMPRA] NOCHECK CONSTRAINT [FK_SOLCOMPRA_SOLTIPSOL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el tipo de agrupacion de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TIPOAGRUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el titulo de la agrupacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TITUAGRUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo de la unidad funcional que realiza la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el nivel de prioridad de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMPRIORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene las observaciones de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el estado de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la fecha de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la salicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMAUTON';

