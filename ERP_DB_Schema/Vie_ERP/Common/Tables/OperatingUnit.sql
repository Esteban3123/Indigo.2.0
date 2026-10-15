CREATE TABLE [Common].[OperatingUnit] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUnit]           INT           NULL,
    [UnitName]         VARCHAR (100) NOT NULL,
    [UnitCode]         VARCHAR (5)   NULL,
    [IPSCode]          VARCHAR (20)  NULL,
    [Address]          VARCHAR (100) NULL,
    [Phone]            VARCHAR (20)  NULL,
    [Email]            VARCHAR (100) NULL,
    [EmailAudit]       VARCHAR (100) NULL,
    [IdCity]           INT           NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_OperatingUnit_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_OperatingUnit_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_OperatingUnit__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OperatingUnit_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_OperatingUnit_OperatingUnit] FOREIGN KEY ([IdUnit]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Common].[OperatingUnit] NOCHECK CONSTRAINT [FK_OperatingUnit_City];


GO
ALTER TABLE [Common].[OperatingUnit] NOCHECK CONSTRAINT [FK_OperatingUnit_OperatingUnit];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OperatingUnit__UnitCode]
    ON [Common].[OperatingUnit]([UnitCode] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la ciudad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Email del auditor', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'EmailAudit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la IPS', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IPSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'UnitName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'IdUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'OperatingUnit', @level2type = N'COLUMN', @level2name = N'Id';

