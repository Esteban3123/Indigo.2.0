CREATE TABLE [Payroll].[Fund] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)  NOT NULL,
    [ThirdPartyId]            INT           NOT NULL,
    [Name]                    VARCHAR (100) NOT NULL,
    [Unemployment]            BIT           NOT NULL,
    [Health]                  BIT           NOT NULL,
    [Pension]                 BIT           NOT NULL,
    [Risk]                    BIT           NOT NULL,
    [CompensationFund]        BIT           NOT NULL,
    [Type]                    TINYINT       NOT NULL,
    [MinistryCode]            VARCHAR (6)   NOT NULL,
    [State]                   BIT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    [Term]                    INT           NULL,
    [MainAccountReceivableId] INT           NULL,
    [PensionACCAI]            BIT           DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Fund__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Fund_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Fund__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Payroll].[Fund] NOCHECK CONSTRAINT [FK_Fund_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_Fund__Code__State]
    ON [Payroll].[Fund]([Code] ASC, [State] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Fund__ThirdPartyId]
    ON [Payroll].[Fund]([ThirdPartyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pension ACCAI segun  Ley_2381_de_2024', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'PensionACCAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por cobrar del cliente, este campo se usa cuando se hace un documento de Cuenta por Cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plazo (Días)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del fondo  1- Activo 0 - Inactivo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de MiniSalud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MinistryCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de ambito del fondo 0-Publico 1- Privado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Caja de compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CompensationFund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Riesgo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Risk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Pension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Health';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Unemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del fondo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Fondo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico para los fondos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Id';

