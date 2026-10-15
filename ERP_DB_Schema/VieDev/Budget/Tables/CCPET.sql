CREATE TABLE [Budget].[CCPET] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ItemType]         TINYINT       NOT NULL,
    [Code]             VARCHAR (30)  NOT NULL,
    [Name]             VARCHAR (300) NOT NULL,
    [CCPETOwnerId]     INT           NULL,
    [AccountType]      BIT           NOT NULL,
    [LinkAccount]      BIT           NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CCPET__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CCPET_CCPET] FOREIGN KEY ([CCPETOwnerId]) REFERENCES [Budget].[CCPET] ([Id])
);


GO
ALTER TABLE [Budget].[CCPET] NOCHECK CONSTRAINT [FK_CCPET_CCPET];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro, (activo  = true, inactivo = false)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vincula Cuenta CPC: Si / No .Esta opción debe estar disponible si el campo Tipo de Cuenta es (C). dejar todas en No.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'LinkAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cuenta: Agregación. (A). Cuentas de Captura (C)  (Solo permite vincular a otras funcionalidades los marcados como tipo C.)  False = Agregacion  True = Captura', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'AccountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Conceptos CCPET padre', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'CCPETOwnerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Conceptos CCPET', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Conceptos CCPET', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OBTIENE Ó ESTABLECE EL TIPO DE RUBRO (INGRESO=1,GASTO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Conceptos CCPET', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CCPET', @level2type = N'COLUMN', @level2name = N'Id';

