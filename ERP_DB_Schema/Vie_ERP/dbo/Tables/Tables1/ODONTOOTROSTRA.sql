CREATE TABLE [dbo].[ODONTOOTROSTRA] (
    [ID]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL] INT            NOT NULL,
    [CONSECTRA]       INT            NOT NULL,
    [OBSERVTRA]       NVARCHAR (250) NULL,
    [CODSERIPS]       CHAR (20)      CONSTRAINT [DF_ODONTOOTROSTRA_CODSERIPS] DEFAULT ((232101)) NULL,
    [CANTIDAD]        INT            NULL,
    CONSTRAINT [PK_ODONTOOTROSTRA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOOTROSTRA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ODONTOOTROSTRA_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID]),
    CONSTRAINT [FK_ODONTOOTROSTRA_ODOPARTRA1] FOREIGN KEY ([CONSECTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);


GO
ALTER TABLE [dbo].[ODONTOOTROSTRA] NOCHECK CONSTRAINT [FK_ODONTOOTROSTRA_INCUPSIPS];


GO
ALTER TABLE [dbo].[ODONTOOTROSTRA] NOCHECK CONSTRAINT [FK_ODONTOOTROSTRA_ODONTOCONTROL];


GO
ALTER TABLE [dbo].[ODONTOOTROSTRA] NOCHECK CONSTRAINT [FK_ODONTOOTROSTRA_ODOPARTRA1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me especifica la cantidad de tratamiento que se envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CUPS que selecciono el medico para realizar la facturación, en ocasiones es vacio ya que el tratamiento que se agrega NO tiene parametrizado algun CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de observacion de cada tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'OBSERVTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de relacion con la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de relacion con la tabla de control de odontograma ODONTOGCTR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'ID';

