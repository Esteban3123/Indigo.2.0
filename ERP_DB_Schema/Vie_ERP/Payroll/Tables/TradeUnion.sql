CREATE TABLE [Payroll].[TradeUnion] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdThirdParty]     INT           NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Description]      VARCHAR (MAX) NULL,
    [PayrollConceptId] INT           NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_TradeUnion__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TradeUnion_Concept] FOREIGN KEY ([PayrollConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_TradeUnion_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[TradeUnion] NOCHECK CONSTRAINT [FK_TradeUnion_Concept];


GO
ALTER TABLE [Payroll].[TradeUnion] NOCHECK CONSTRAINT [FK_TradeUnion_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TradeUnion__Code]
    ON [Payroll].[TradeUnion]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del sindicato  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de nomina que se le va a liquidar al empleado que este afiliado a este Sindicato, Los conceptos que se pueden seleccionar son los que sean de Clase 044 el cual especifica que son conceptos de sindicato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'PayrollConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la descripcion del sindicato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Nombre del sindicato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Sindicato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'TradeUnion', @level2type = N'COLUMN', @level2name = N'Id';

